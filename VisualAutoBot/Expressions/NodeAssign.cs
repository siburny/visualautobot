using System;
using VisualAutoBot.ProgramNodes;

namespace VisualAutoBot.Expressions
{
    // NodeBinary for binary operations such as Add, Subtract etc...
    public class NodeAssign : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeAssign(Node lhs, Node rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
        }

        Node _lhs;
        Node _rhs;

        public override double EvalDouble(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval script node as double");
        }

        public override bool EvalBoolean(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval script node as boolean");
        }

        public double EvalAssign(IContext ctx)
        {
            if(_lhs is NodeVariable variable)
            {
                var rhsVal = _rhs.EvalDouble(ctx);
                
                if (_rhs is NodeFunctionCall function && function.FunctionName.ToLower() == "define")
                {
                    if (!BaseTreeNode.VariableExists(variable.Name))
                    {
                        BaseTreeNode.SetVariable(variable.Name, rhsVal);
                    }
                }
                else
                {
                    BaseTreeNode.SetVariable(variable.Name, rhsVal);
                }

                return rhsVal;
            }
            else
            {
                throw new ArithmeticException("Cannot eval script node");
            }
        }
    }
}
