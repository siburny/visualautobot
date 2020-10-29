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
            for (int i = 0; i < Nodes.Count; i++)
            {
                (Nodes[i] as BaseTreeNode).Run();
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
