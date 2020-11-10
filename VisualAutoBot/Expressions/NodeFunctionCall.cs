using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAutoBot.Expressions
{
    public class NodeFunctionCall : Node
    {
        public NodeFunctionCall(string functionName, Node[] arguments)
        {
            _functionName = functionName;
            _arguments = arguments;
        }

        string _functionName;
        Node[] _arguments;

        public override double EvalDouble(IContext ctx)
        {
            // Evaluate all arguments
            var argVals = new double[_arguments.Length];
            for (int i = 0; i < _arguments.Length; i++)
            {
                argVals[i] = _arguments[i].EvalDouble(ctx);
            }

            // Call the function
            return ctx.CallFunction(_functionName, argVals);
        }

        public override bool EvalBoolean(IContext ctx)
        {
            throw new ArithmeticException("Cannot eval boolean node as double");
        }

    }
}
