using FormulaEvaluator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PS1UnitTest
{
    [TestClass]
    public class FormulaEvaluatorTests
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("4 + (2*3)", s => 0));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(13, Evaluator.Evaluate("5 + X - (2*3)", s => 14));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual(2, Evaluator.Evaluate("(25 / 5) - 3", s => 0));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.AreEqual(50, Evaluator.Evaluate("(5*4*2) + (2+9+X)", s => -1));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("2+4 + (3-1)", s => 0));
        }

        [TestMethod]
        public void Test6()
        {
            Assert.AreEqual(5, Evaluator.Evaluate("(2/2) - 1 + 5", s => 0));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        //Parenthesis do not match up.
        public void Test7()
        {
            Evaluator.Evaluate("4 + 4+3)", s=>0);
        }

        [TestMethod]
        public void Test8()
        {

        }

        
    }
}
