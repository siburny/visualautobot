using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualAutoBot.Expressions;

namespace VisualAutoBot.ProgramNodes
{
    class IfTreeNode : BaseTreeNode
    {
        public IfTreeNode()
        {
            NodeText = "IF";

            Parameters.Add("Condition", "");
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if (_data.ContainsKey("Condition"))
            {
            }

            base.Save(_data);
        }

        public override void Refresh()
        {
            Text = $"{NodeText} ({Parameters["Condition"]})";
        }

        public override void Execute()
        {
            try
            {
                string expr = Parameters["Condition"].ToString();
                if (string.IsNullOrEmpty(expr))
                {
                    throw new ScriptException("Empty If condition", this);
                }

                bool result = Parser.Parse(expr, true).EvalBoolean(this);

                BackColor = Color.Empty;

                if(result)
                {
                    foreach (var node in Nodes)
                    {
                        if (SignalToExit) break;
                        (node as BaseTreeNode).Run();
                    }
                }
                else
                {
                    if(NextNode != null && NextNode is ElseTreeNode)
                    {
                        foreach (var node in NextNode.Nodes)
                        {
                            if (SignalToExit) break;
                            (node as BaseTreeNode).Run();
                        }
                    }
                }
            }
            catch(SyntaxException e)
            {
                throw new ScriptException($"Parser exception: {e.Message}", this);
            }
        }

        public override void FromJSON(JObject json)
        {
            base.FromJSON(json);

            if (json.ContainsKey("Nodes"))
            {
                foreach (JObject obj in json["Nodes"])
                {
                    Create(obj, Nodes);
                }
            }
        }

        public override JToken ToJSON()
        {
            JToken json = base.ToJSON();
            JArray array = new JArray();

            json["Nodes"] = array;
            foreach (var node in Nodes)
            {
                array.Add(((BaseTreeNode)node).ToJSON());
            }

            return json;
        }
    }
}
