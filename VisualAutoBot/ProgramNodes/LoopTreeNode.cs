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
        public override void Init()
        {
            Parameters.Add("WindowName", "TrainStation - Pixel");
        }

        public override void Run()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                (Nodes[i] as IRunnableTreeNode).Run();
            }
        }
    }
}
