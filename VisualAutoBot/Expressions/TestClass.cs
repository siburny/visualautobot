using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualAutoBot.Expressions
{
    [TestClass]
    public class TestClass : IContext
    {
        [TestMethod]
        public void TestBoolean()
        {
            Assert.IsTrue(Parser.ParseBoolean("2 > 1").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 > 2").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 > 3").EvalBoolean(this));

            Assert.IsTrue(Parser.ParseBoolean("2 < 3").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 < 2").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 < 1").EvalBoolean(this));

            Assert.IsTrue(Parser.ParseBoolean("2 >= 1").EvalBoolean(this));
            Assert.IsTrue(Parser.ParseBoolean("2 >= 2").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 >= 3").EvalBoolean(this));

            Assert.IsTrue(Parser.ParseBoolean("2 <= 3").EvalBoolean(this));
            Assert.IsTrue(Parser.ParseBoolean("2 <= 2").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 <= 1").EvalBoolean(this));

            Assert.IsTrue(Parser.ParseBoolean("2 == 2").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 == 1").EvalBoolean(this));

            Assert.IsTrue(Parser.ParseBoolean("2 != 1").EvalBoolean(this));
            Assert.IsFalse(Parser.ParseBoolean("2 != 2").EvalBoolean(this));
        }

        double IContext.CallFunction(string name, double[] arguments)
        {
            throw new System.NotImplementedException();
        }

        double IContext.ResolveVariable(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
