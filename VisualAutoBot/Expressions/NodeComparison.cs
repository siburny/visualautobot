using System;

namespace VisualAutoBot.Expressions
{
    // NodeBinary for binary operations such as Add, Subtract etc...
    class NodeComparison : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeComparison(Node lhs, Node rhs, Func<double, double, bool> op)
        {
            _lhs = lhs;
            _rhs = rhs;
            _op = op;
        }

        Node _lhs;                              // Left hand side of the operation
        Node _rhs;                              // Right hand side of the operation
        Func<double, double, bool> _op;       // The callback operator

        public override double Eval(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval boolean node as double");
        }

        public override bool EvalBoolean(IContext ctx)
        {
            // Evaluate both sides
            var lhsVal = _lhs.Eval(ctx);
            var rhsVal = _rhs.Eval(ctx);

            // Evaluate and return
            var result = _op(lhsVal, rhsVal);
            return result;
        }
    }
}
