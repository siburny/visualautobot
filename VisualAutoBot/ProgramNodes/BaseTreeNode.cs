using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    abstract class BaseTreeNode : TreeNode, IRunnableTreeNode
    {
        private string _nodeText = "";
        public string NodeText { 
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
        };

        internal Dictionary<string, object> Parameters = new Dictionary<string, object>();

        public BaseTreeNode() : base() {
            NodeText = GetType().Name;
        }

        public virtual void Save(Dictionary<string, object> _data)
        {
            foreach(var item in _data)
            {
                if(Parameters.ContainsKey(item.Key))
                {
                    Parameters[item.Key] = item.Value;
                }
            }
        }

        public abstract void Execute();

        public void Run()
        {
            BackColor = Color.LightGreen;

            Execute();

            BackColor = Color.Empty;
        }
    }
}
