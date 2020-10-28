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
    }
}
