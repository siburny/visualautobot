using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAutoBot.Expressions
{
    // Represents a variable (or a constant) in an expression.  eg: "2 * pi"
    public class NodeVariable : Node
    {
        public NodeVariable(string variableName)
        {
            _variableName = variableName;
        }

        string _variableName;

        public string Name
        {
            get
            {
                return _variableName;
            }
        }

        public override double EvalDouble(IContext ctx)
        {
            return ctx.ResolveVariable(_variableName);
        }
        public override bool EvalBoolean(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval variable as boolean");
        }
    }
}
