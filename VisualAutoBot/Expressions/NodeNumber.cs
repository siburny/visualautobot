using System;

namespace VisualAutoBot.Expressions
{
    // NodeNumber represents a literal number in the expression
    class NodeNumber : Node
    {
        public NodeNumber(double number)
        {
            _number = number;
        }

        double _number;             // The number

        public override double EvalDouble(IContext ctx)
        {
            // Just return it.  Too easy.
            return _number;
        }
        public override bool EvalBoolean(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval boolean node as double");
        }
    }
}
