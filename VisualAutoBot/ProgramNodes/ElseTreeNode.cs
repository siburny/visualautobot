using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class ElseTreeNode : BaseTreeNode
    {
        //private 
        public ElseTreeNode()
        {
            NodeText = "Else";
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
            Text = NodeText;
        }

        public override void Execute()
        {
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
