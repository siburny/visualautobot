using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualAutoBot.Expressions
{
    [TestClass]
    public class TestClass : IContext
    {
        [TestMethod]
        public void TestBoolean()
        {
            Assert.IsTrue(Parser.Parse("2 > 1", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 > 2", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 > 3", true).EvalBoolean(this));

            Assert.IsTrue(Parser.Parse("2 < 3", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 < 2", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 < 1", true).EvalBoolean(this));

            Assert.IsTrue(Parser.Parse("2 >= 1", true).EvalBoolean(this));
            Assert.IsTrue(Parser.Parse("2 >= 2", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 >= 3", true).EvalBoolean(this));

            Assert.IsTrue(Parser.Parse("2 <= 3", true).EvalBoolean(this));
            Assert.IsTrue(Parser.Parse("2 <= 2", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 <= 1", true).EvalBoolean(this));

            Assert.IsTrue(Parser.Parse("2 == 2", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 == 1", true).EvalBoolean(this));

            Assert.IsTrue(Parser.Parse("2 != 1", true).EvalBoolean(this));
            Assert.IsFalse(Parser.Parse("2 != 2", true).EvalBoolean(this));
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
