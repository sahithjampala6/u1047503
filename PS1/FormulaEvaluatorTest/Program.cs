using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace FormulaEvaluatorTest
{
    /// <author> Sahith Jampala
    /// 
    /// </summary> This class contains all the methods required to build the Infix Expression Evaluator.
    public static class Evaluator
    {
        public delegate int Lookup(String v);

        //Value and Operator stacks used to store data from expressions given.
        public static Stack<int> valueStack = new Stack<int>();
        public static Stack<String> operatorStack = new Stack<String>();

        /// <summary> Actual method that takes in an expression given as well as the delegate for variables, and gives the correct output of the expression.
        /// 
        /// </summary>
        /// <param name="exp"></param> expression to be evaluated.
        /// <param name="variableEvaluator"></param> delegate used to swap given variable with its value.
        /// <returns></returns> correct value for expression, or exception if expression is invalid.
        public static int Evaluate(String exp, Func<String, int> variableEvaluator)
        {
            //expression split into an array to iterate through.
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)", RegexOptions.IgnorePatternWhitespace);

            foreach (string currentToken in substrings)
            {
                //trims token for white space
                String token = currentToken.Trim();
                if (token == "" )
                  {
                    continue;
                   }
                
                    
                    if (isNumber(token))
                    {
                        processInteger(Convert.ToInt16(token));
                    }

                    if (isVariable(token))
                    {
                        //converts variable to correct value or throws exception if said variable does not exist.
                        try { processInteger(variableEvaluator(token)); }

                        catch { throw new KeyNotFoundException(); }
                    }


                    if (isPlusOrMinus(token))
                    {
                        processAddOrSubtract(token);
                        operatorStack.Push(token);
                    }

                    if (token == "*" || token == "/" || token == "(")
                    {
                        operatorStack.Push(token);
                    }

                    if (token == ")")
                    {
                        if (isPlusOrMinus(operatorStack.Peek()))
                        {
                            processAddOrSubtract(token);
                        }
                        if (operatorStack.Peek() != "(")
                        {
                            throw new InvalidOperationException();
                        }
                        operatorStack.Pop();
                        if (operatorStack.Count > 0)
                        {
                            if (operatorStack.Peek() == "*" || operatorStack.Peek() == "/")
                            {
                                int result;
                                try
                                {
                                    int val1 = valueStack.Pop();
                                    int val2 = valueStack.Pop();
                                    string tempOp = operatorStack.Pop();
                                    if (tempOp == "*")
                                    {
                                        result = val1 * val2;
                                    }
                                    else
                                    {
                                        result = val1 / val2;
                                    }
                                }
                                catch
                                {
                                    throw new InvalidOperationException();
                                }
                                valueStack.Push(result);
                            }
                        }
                    }
                }

                if (isPlusOrMinus(operatorStack.Peek()))
                {
                    int val1 = valueStack.Pop();
                    int val2 = valueStack.Pop();
                    String tempOp = operatorStack.Pop();

                    if (tempOp == "+")
                    {
                        return val1 + val2;
                    }
                    else
                    {
                        return val2 - val1;
                    }

                }
                else
                {
                    return valueStack.Pop();
                }
            
        }
        /// <summary> Helper method to prioritize multiplication or division.
        /// 
        /// </summary> If there are no operators in the operator stack, skips.
        /// <param name="cvalue"></param> integer value of token.
        static void processInteger(int cvalue)
        {
            if (operatorStack.Count > 0)
            {

                if (operatorStack.Peek() == "*" || operatorStack.Peek() == "/")
                {
                    int tempResult;
                    try
                    {
                        int tempVal = valueStack.Pop();
                        string tempOp = operatorStack.Pop();
                        if (tempOp == "*")
                        {
                            tempResult = tempVal * cvalue;
                        }
                        else
                        {
                            //if division by zero occurs, throw an exception.
                            if (cvalue == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            tempResult = tempVal / cvalue;
                        }
                    }
                    catch
                    {
                        throw new InvalidOperationException();
                    }
                    valueStack.Push(tempResult);
                } else
                {
                    valueStack.Push(cvalue);
                }
            }
            else
            {
                valueStack.Push(cvalue);
            }
            
        }

        /// <summary> Helper method which takes current token of the expression and returns whether or not it is a number value.
        /// 
        /// </summary>
        /// <param name="x"></param> Current token.
        /// <returns></returns> True if token is a number, False if it is not.
        static bool isNumber(String x)
        {
            int outcome;
            return int.TryParse(x, out outcome);
        }

        /// <summary> Helper method which determines if the current token is a variable expression.
        ///
        /// <param name="x"></param> Current Token
        /// <returns></returns> True if token is a variable expression, false if not.
        static bool isVariable(String x)
        {
            char[] varArray = x.ToUpper().ToCharArray();
            char firstLetter = varArray[0];
            if (firstLetter >= 'A' && firstLetter <= 'Z')
            {
                return true;
            }
            return false;
        }

        /// <summary> Helper method which determines if current token is a '+' or '-' sign.
        /// 
        /// <param name="x"></param> Current token.
        /// <returns></returns> True if it is '+' or '-', and false if not.
        static bool isPlusOrMinus(String x)
        {
            return x == "+" || x == "-";
        }

        /// <summary> Helper method which adds or subtracts the two most recent values on the stack.
        /// 
        /// </summary> If there is less than two values on the value stack or no operators in the operator stack, this will throw an invalid operation exception.
        /// <param name="token"></param> Current token to be evaluated.
        static void processAddOrSubtract(String token)
        {
            if (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == "+" || operatorStack.Peek() == "-")
                {
                    try
                    {
                        int val1 = valueStack.Pop();
                        int val2 = valueStack.Pop();


                        int result;
                        String tempOp = operatorStack.Pop();

                        if (tempOp == "+")
                        {
                            result = val1 + val2;
                        }
                        else
                        {
                            result = val2 - val1;
                        }
                        valueStack.Push(result);

                    }
                    catch
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        /// <summary> Main method used for testing purposes and to create delegate.
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
           var f = Evaluator.Evaluate("2+4 + (3-1)", s => 0);
            Console.WriteLine(f);
        }

    }

    }
    



