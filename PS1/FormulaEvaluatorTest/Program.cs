using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluatorTest
{
    public static class Evaluator
    {
        public delegate int Lookup(String v);


        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            Stack<String> valueStack = new Stack<String>();
            Stack<String> operatorStack = new Stack<String>();
            int returnVal = 0;

            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            
            foreach(String s in substrings)
            {
                if(s.Equals("*") || s.Equals("/"))
                {
                    
                }
            }

            return returnVal;
        }
       
        private static bool isNumber(String x)
        {
            int outcome;
           return int.TryParse(x, out outcome);
        }
    }
    
    }

