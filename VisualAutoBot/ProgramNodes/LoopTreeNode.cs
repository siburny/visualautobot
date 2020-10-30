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
            if(window == default(IntPtr))
            {
                throw new ScriptException($"Cannot find game window: {Parameters["WindowName"].ToString()}", this);
            }
            
            SetVariable("WindowHandle", window);

            foreach (var node in Nodes)
            {
                //if (SignalToExit) break;
                (node as BaseTreeNode).Run();
            }
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
