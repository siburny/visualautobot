using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    abstract class BaseTreeNode : TreeNode, IRunnableTreeNode
    {
        public static bool SignalToExit = false;
        public static int Delay = 0;

        private string _nodeText = "";
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

        public static Dictionary<string, Type> AvailableTypes = new Dictionary<string, Type>()
        {
            { "WaitTreeNode", typeof(WaitTreeNode) },
            { "CalcTreeNode", typeof(CalcTreeNode) },
            { "ScreenshotTreeNode", typeof(ScreenshotTreeNode) },
            { "MouseClickTreeNode", typeof(MouseClickTreeNode) },
            { "MatchTemplateTreeNode", typeof(MatchTemplateTreeNode) },
            { "IfTreeNode", typeof(IfTreeNode) },
            { "ElseTreeNode", typeof(ElseTreeNode) },
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

        public virtual JToken ToJSON()
        {
            JObject json = new JObject();

            json.Add("Type", GetType().Name);
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
            Boolean highlight = !(this is LoopTreeNode || this is IfTreeNode || this is ElseTreeNode);

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
            catch(ScriptException e)
            {
                BackColor = Color.Red;
                ToolTipText = e.Message;
                throw;
            }

            if (highlight)
            {
                BackColor = Color.Empty;
            }
        }

        public void ClearError()
        {
            ToolTipText = "";
            BackColor = Color.Empty;

            foreach(var node in Nodes)
            {
                (node as BaseTreeNode).ClearError();
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
                if(_variables[name] is T)
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
    }
}
