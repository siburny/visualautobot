﻿using System;

namespace VisualAutoBot.Expressions
{
    // NodeUnary for unary operations such as Negate
    class NodeUnary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeUnary(Node rhs, Func<double, double> op)
        {
            _rhs = rhs;
            _op = op;
        }

        Node _rhs;                              // Right hand side of the operation
        Func<double, double> _op;               // The callback operator

        public override double EvalDouble(IContext ctx)
        {
            // Evaluate RHS
            var rhsVal = _rhs.EvalDouble(ctx);

            // Evaluate and return
            var result = _op(rhsVal);
            return result;
        }

        public override bool EvalBoolean(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval boolean node as double");
        }

    }
}
