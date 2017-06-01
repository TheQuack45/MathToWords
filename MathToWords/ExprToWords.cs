using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToWords
{
    public static class ExprToWords
    {
        private static readonly Dictionary<string, string> OperWords = new Dictionary<string, string>()
        {
            { "+", "the sum of {0} and {1}" },
            { "-", "the difference of {0} and {1}" },
            { "*", "the product of {0} and {1}" },
            { "/", "the quotient of {0} and {1}" },
            { "^", "the result of {0} to the power of {1}" },
            { "%", "the result of {0} modulus {1}" },
        };

        public static string ConvertOld(string expr)
        {
            string words = "";
            string[] tokens = expr.Split(' ');
            string operatorWaiting = "";
            Queue<string> operands = new Queue<string>();

            foreach (string token in tokens)
            {
                if (Utility.IsOperator(token))
                {
                    operatorWaiting = token;
                }
                else if (Int32.TryParse(token, out int p))
                {
                    operands.Enqueue(NumToWords.Convert(token));
                    if (operands.Count == Utility.OperandCount[operatorWaiting])
                    {
                        words += String.Format(OperWords[operatorWaiting], operands.ToArray<string>()) + " ";
                    }
                }
                else
                {
                    throw new ArgumentException("An invalid token was found in the given expression: " + token, nameof(expr));
                }
            }

            return words;
        }

        public static string Convert(string expr)
        {
            string[] tokens = expr.Split(' ');
            Stack<string> operands = new Stack<string>();

            foreach (string token in tokens)
            {
                if (Int32.TryParse(token, out int p))
                {
                    operands.Push(NumToWords.Convert(token));
                }
                else if (Utility.IsOperator(token))
                {
                    List<string> opers = new List<string>();
                    while (opers.Count < Utility.OperandCount[token])
                    {
                        opers.Add(operands.Pop());
                    }
                    opers.Reverse();
                    string str = String.Format(OperWords[token], opers.ToArray<string>());
                    operands.Push(str);
                }
            }

            return operands.Pop();
        }
    }
}
