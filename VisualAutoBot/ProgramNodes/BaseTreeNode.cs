﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    abstract class BaseTreeNode : TreeNode, IRunnableTreeNode
    {
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
                    Parameters[param.Name] = (param.Value as JValue).Value;
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
                json.Add(param.Key, new JValue(param.Value));
            }


            return json;
        }

        public virtual void Refresh() { }

        public abstract void Execute();

        public void Run()
        {
            BackColor = Color.LightGreen;

            Execute();

            BackColor = Color.Empty;
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
    }
}
