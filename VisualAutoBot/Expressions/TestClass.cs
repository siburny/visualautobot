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

        [TestMethod]
        public void TestBinary()
        {
            Assert.AreEqual(Parser.ParseBoolean("2 % 2").EvalDouble(this), 0);
            Assert.AreEqual(Parser.ParseBoolean("2 % 0").EvalDouble(this), double.NaN);
            Assert.AreEqual(Parser.ParseBoolean("6 % 5").EvalDouble(this), 1);
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
