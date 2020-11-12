using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class LoopTreeNode : BaseTreeNode
    {
        public LoopTreeNode() : base()
        {
            NodeText = "Loop";

            Parameters.Add("WindowName", "TrainStation - Pixel");
        }

        public override void Execute()
        {
            var window = ScreenUtilities.GetWindowByName(Parameters["WindowName"].ToString());
            if(window == default)
            {
                throw new ScriptException($"Cannot find game window: {Parameters["WindowName"]}", this);
            }

            SetVariable("WindowName", "TrainStation - Pixel");

            foreach (var node in Nodes)
            {
                if (SignalToExit) break;
                (node as BaseTreeNode).Run();
            }
        }

        public override void Refresh()
        {
            SetVariable("WindowName", Parameters["WindowName"]);
        }

        public override void FromJSON(JObject json)
        {
            base.FromJSON(json);

            if(json.ContainsKey("Nodes"))
            {
                foreach(JObject obj in json["Nodes"])
                {
                    Create(obj, Nodes);
                }
            }
        }

        public override JObject ToJSON()
        {
            JObject json = base.ToJSON();
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
