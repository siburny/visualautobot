namespace VisualAutoBot.Expressions
{
    // Node - abstract class representing one node in the expression 
    public abstract class Node
    {
        public abstract double Eval(IContext ctx);
        public abstract bool EvalBoolean(IContext ctx);
    }
}
