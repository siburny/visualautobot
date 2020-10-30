using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualAutoBot.ProgramNodes;

namespace VisualAutoBot
{
    class ScriptException : Exception
    {
        public BaseTreeNode Node { get; set; }
        public bool IsFatal { get; set; }

        public ScriptException(string message, BaseTreeNode _node, bool _isFatal = true) : base(message)
        {
            Node = _node;
            IsFatal = _isFatal;
        }
    }
}
