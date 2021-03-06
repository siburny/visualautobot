﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VisualAutoBot.Expressions;

namespace VisualAutoBot.ProgramNodes
{
    abstract class BaseTreeNode : TreeNode, IRunnableTreeNode, IContext
    {
        public static bool SignalToExit = false;
        public static int Delay = 0;

        private string _nodeText = "";
        internal Color _backColor = Color.Empty;
        public string NodeText
        {
            get
            {
                return _nodeText;
            }
            set
            {
                Text = _nodeText = value;
            }
        }

        public double Timing { get; set; }

        public static Dictionary<string, Type> AvailableTypes = new Dictionary<string, Type>()
        {
            { "WaitTreeNode", typeof(WaitTreeNode) },
            { "ScriptTreeNode", typeof(ScriptTreeNode) },
            { "ScreenshotTreeNode", typeof(ScreenshotTreeNode) },
            { "MouseClickTreeNode", typeof(MouseClickTreeNode) },
            { "MatchTemplateTreeNode", typeof(MatchTemplateTreeNode) },
            { "IfTreeNode", typeof(IfTreeNode) },
            { "ElseTreeNode", typeof(ElseTreeNode) },
            { "CommentTreeNode", typeof(CommentTreeNode) },
            { "ProgramTreeNode", typeof(ProgramTreeNode) },
        };

        public static Dictionary<Type, Type[]> NestedTypes = new Dictionary<Type, Type[]>()
        {
            {
                typeof(WaitTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(ScriptTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(ScreenshotTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(MouseClickTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(MatchTemplateTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(IfTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(ElseTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(CommentTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(ProgramTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
            {
                typeof(ProgramTreeNode),
                new Type[] {
                    typeof(LoopTreeNode), typeof(IfTreeNode), typeof(ElseTreeNode),
                }
            },
        };

        internal Dictionary<string, object> Parameters = new Dictionary<string, object>();

        public BaseTreeNode() : base()
        {
            NodeText = GetType().Name;
        }

        public virtual void Save(Dictionary<string, object> _data)
        {
            foreach (var item in _data)
            {
                if (Parameters.ContainsKey(item.Key))
                {
                    Parameters[item.Key] = item.Value;
                }
            }
            Refresh();
        }

        internal static void Create(JObject json, TreeNodeCollection nodes)
        {
            if (!json.ContainsKey("Type"))
            {
                return;
            }

            string type = ((JValue)json["Type"]).ToString();

            if (type == "LoopTreeNode")
            {
                (nodes[0] as BaseTreeNode).FromJSON(json);
            }
            else if (AvailableTypes.ContainsKey(type))
            {
                var node = Activator.CreateInstance(AvailableTypes[type]) as BaseTreeNode;
                nodes.Add(node);

                node.FromJSON(json);
            }
        }

        public virtual void FromJSON(JObject json)
        {
            foreach (var param in json.Properties())
            {
                if (Parameters.ContainsKey(param.Name))
                {
                    if ((param.Value as JValue).Value == null)
                    {
                        Parameters[param.Name] = null;
                    }
                    else
                    {
                        string value = (param.Value as JValue).Value.ToString();

                        if (value.StartsWith("PNG:"))
                        {
                            MemoryStream m = new MemoryStream(Convert.FromBase64String(value.Substring(4)));
                            Parameters[param.Name] = Bitmap.FromStream(m);
                        }
                        else
                        {
                            Parameters[param.Name] = (param.Value as JValue).Value;
                        }
                    }
                }
            }

            Refresh();
        }

        public virtual JObject ToJSON()
        {
            JObject json = new JObject
            {
                { "Type", GetType().Name }
            };

            foreach (var param in Parameters)
            {
                if (param.Value != null && param.Value is Bitmap)
                {
                    Bitmap bitmap = param.Value as Bitmap;
                    MemoryStream m = new MemoryStream();
                    bitmap.Save(m, ImageFormat.Png);
                    json.Add(param.Key, new JValue("PNG:" + Convert.ToBase64String(m.ToArray())));
                }
                else
                {
                    json.Add(param.Key, new JValue(param.Value));
                }
            }

            return json;
        }

        public virtual void Refresh() { }

        public abstract void Execute();

        public void Run()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool highlight = !(this is LoopTreeNode || this is IfTreeNode || this is ElseTreeNode || this is CommentTreeNode);

            if (highlight)
            {
                BackColor = Color.LightGreen;
            }

            try
            {
                Execute();

                if (Delay > 0)
                {
                    Thread.Sleep(Delay);
                }
            }
            catch (ScriptException e)
            {
                BackColor = Color.Red;
                ToolTipText = e.Message;
                throw;
            }

            if (highlight)
            {
                BackColor = _backColor;
            }

            watch.Stop();
            Timing = watch.ElapsedMilliseconds;
        }

        public void ClearError()
        {
            if (!(this is CommentTreeNode))
            {
                ToolTipText = "";
                BackColor = _backColor;

                foreach (var node in Nodes)
                {
                    (node as BaseTreeNode).ClearError();
                }
            }
        }

        private static Dictionary<string, object> _variables = new Dictionary<string, object>();
        public static object GetVariable(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }
            else
            {
                return null;
            }
        }

        public static T GetVariable<T>(string name)
        {
            if (_variables.ContainsKey(name))
            {
                if (_variables[name] is T)
                    return (T)_variables[name];
                else
                {
                    return default;
                }
            }
            else
            {
                return default;
            }
        }

        public static void SetVariable(string name, object value)
        {
            if (_variables.ContainsKey(name))
            {
                if (_variables[name] != null && _variables[name] is IDisposable)
                {
                    (_variables[name] as IDisposable).Dispose();
                }
                _variables[name] = value;
            }
            else
            {
                _variables.Add(name, value);
            }
        }

        public static bool VariableExists(string name)
        {
            return _variables.ContainsKey(name);
        }

        public static void ClearVariables()
        {
            _variables.Clear();
        }

        #region Expression execution
        double IContext.ResolveVariable(string name)
        {
            if (VariableExists(name))
            {
                return Convert.ToDouble(GetVariable(name));
            }
            else
            {
                throw new ScriptException($"Vriable '{name}' is not found.", this, false);
            }
        }

        private static readonly Random random = new Random();
        double IContext.CallFunction(string name, double[] arguments)
        {
            switch (name.ToLower())
            {
                case "random":
                    if (arguments.Length == 1)
                    {
                        return random.Next(Convert.ToInt32(arguments[0]));
                    }
                    else if (arguments.Length == 2)
                    {
                        return random.Next(Convert.ToInt32(arguments[0]), Convert.ToInt32(arguments[1]));
                    }
                    else
                    {
                        throw new ScriptException($"Function '{name}' accepts one or two arguments only ({arguments.Length} passed)", this);
                    }
                case "define":
                    if (arguments.Length == 1)
                    {
                        return arguments[0];
                    }
                    else
                    {
                        throw new ScriptException($"Function '{name}' accepts one argument only ({arguments.Length} passed)", this);
                    }
                default:
                    throw new ScriptException($"Call to an unknown function '{name}'", this);
            }
        }
        #endregion
    }
}
