using FormulaEvaluator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PS1UnitTest
{
    [TestClass]
    public class FormulaEvaluatorTests
    {
        [TestMethod]
        public void TestAddAndMultiply()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("4 + (2*3)", s => 0));
        }

        [TestMethod]
        public void TestAddAndMultiplyWithDelegate()
        {
            Assert.AreEqual(13, Evaluator.Evaluate("5 + X - (2*3)", s => 14));
        }

        [TestMethod]
        public void TestDivisionAndSubtraction()
        {
            Assert.AreEqual(2, Evaluator.Evaluate("(25 / 5) - 3", s => 0));
        }

        [TestMethod]
        public void TestAddAndMultiplyWithDelegate2()
        {
            Assert.AreEqual(50, Evaluator.Evaluate("(5*4*2) + (2+9+X)", s => -1));
        }

        [TestMethod]
        public void TestAddAndSubtract()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("2+4 + (3-1)", s => 0));
        }

        [TestMethod]
        public void TestDivisionSubtractionAndAddition()
        {
            Assert.AreEqual(5, Evaluator.Evaluate("(2/2) - 1 + 5", s => 0));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        //Parenthesis do not match up.
        public void TestInvalidParanthesis()
        {
            Evaluator.Evaluate("4 + 4+3)", s=>0);
        }

        [TestMethod]
        public void TestDivision()
        {
            Assert.AreEqual(5, Evaluator.Evaluate("(20 / 4)", s => 0));
        }

        [TestMethod]
        public void TestComplicatedDivision()
        {
            Assert.AreEqual(20, Evaluator.Evaluate("(80/2) / (8/4)", s => 0));
        }

        [TestMethod]
        public void TestDivisionWithDelegate()
        {
            Assert.AreEqual(13, Evaluator.Evaluate("39 / X5", s => 3));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDivisionByZero()
        {
            Evaluator.Evaluate("4 / 0", s => 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestComplicatedDivisionByZero()
        {
            Evaluator.Evaluate("4 * 3 - 5 + (3 / 0)", s => 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestInvalidExpression()
        {
            Evaluator.Evaluate("4 * +", s => 0);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Assert.AreEqual(20, Evaluator.Evaluate("5 * 4", s => 0));
        }
    }
}
