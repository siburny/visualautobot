using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    abstract class BaseTreeNode : TreeNode, IRunnableTreeNode
    {
        public BaseTreeNode() : base()
        {
            Init();
        }

        internal Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public void Save()
        {

        }

        public abstract void Init();
        public abstract void Run();
    }
}
