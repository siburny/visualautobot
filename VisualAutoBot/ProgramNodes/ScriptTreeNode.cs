using System.Collections.Generic;
using System.Threading;
using VisualAutoBot.Expressions;

namespace VisualAutoBot.ProgramNodes
{
    class ScriptTreeNode : BaseTreeNode
    {
        public ScriptTreeNode()
        {
            NodeText = "Script";

            Parameters.Add("Expression", "");
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if(_data.ContainsKey("Expression"))
            {
            }

            base.Save(_data);
        }

        public override void Execute()
        {
            if (!string.IsNullOrEmpty(Parameters["Expression"].ToString()))
            {
                Parser.ParseAssign(Parameters["Expression"].ToString()).EvalAssign(this);
            }
        }
    }
}
