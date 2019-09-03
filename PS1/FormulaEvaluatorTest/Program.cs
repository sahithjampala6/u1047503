using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace FormulaEvaluatorTest
{

    public static class Evaluator
    {
        public delegate int Lookup(String v);


        public static Stack<int> valueStack = new Stack<int>();
        public static Stack<String> operatorStack = new Stack<String>();

        public static int Evaluate(String exp, Func<String, int> variableEvaluator)
        {

            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)", RegexOptions.IgnorePatternWhitespace);

            foreach (string currentToken in substrings)
            {
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

        static bool isNumber(String x)
        {
            int outcome;
            return int.TryParse(x, out outcome);
        }

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

        static bool isPlusOrMinus(String x)
        {
            return x == "+" || x == "-";
        }

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
        public static void Main(String[] args)
        {
           var f = Evaluator.Evaluate("2+4 + (3-1)", s => 0);
            Console.WriteLine(f);
        }

    }

    }
    



