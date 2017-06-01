using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathToWords
{
    public static class InfixToPostfix
    {
        public enum ASSOCIATIVITY { Left, Right };
        public static readonly Dictionary<string, ASSOCIATIVITY> OperatorAssoc = new Dictionary<string, ASSOCIATIVITY>()
        {
            { "+", ASSOCIATIVITY.Left },
            { "-", ASSOCIATIVITY.Left },
            { "*", ASSOCIATIVITY.Left },
            { "/", ASSOCIATIVITY.Left },
            { "^", ASSOCIATIVITY.Right },
            { "%", ASSOCIATIVITY.Left },
        };
        public static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>()
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 },
            { "%", 2 },
        };

        /// <summary>
        /// Implements the shunting yard algorithm to convert the given expression from infix notation to prefix notation.
        /// </summary>
        /// <param name="expression">Infix expression to convert.</param>
        /// <returns>Given expression converted to prefix notation.</returns>
        public static string Convert(string expression)
        {
            expression = Regex.Replace(expression, @"\s{0,}([\(\)\+\-\*\/\^])\s{0,}", " $1 ");
            string[] tokens = expression.Split(' ');
            Queue<string> output = new Queue<string>();
            Stack<string> operStack = new Stack<string>();
            
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                if (Int32.TryParse(token, out int parsed))
                {
                    output.Enqueue(token);
                }
                else if (Utility.IsOperator(token))
                {
                    try
                    {
                        while (Utility.IsOperator(operStack.Peek()))
                        {
                            if (OperatorAssoc[token] == ASSOCIATIVITY.Left && OperatorPrecedence[token] <= OperatorPrecedence[operStack.Peek()])
                            {
                                output.Enqueue(operStack.Pop());
                            }
                            else if (OperatorAssoc[token] == ASSOCIATIVITY.Right && OperatorPrecedence[token] < OperatorPrecedence[operStack.Peek()])
                            {
                                output.Enqueue(operStack.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    { }
                    operStack.Push(token);
                }
                else if (token == ")")
                {
                    try
                    {
                        while (operStack.Peek() != "(")
                        {
                            output.Enqueue(operStack.Pop());
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        throw new ArgumentException("The parentheses are mismatched in the given expression.", nameof(expression));
                    }
                    operStack.Pop();
                }
                else if (token == "(")
                {
                    operStack.Push(token);
                }
            }

            while (operStack.Count > 0)
            {
                string token = operStack.Pop();
                if (token == "(" || token == ")")
                {
                    throw new ArgumentException("The parentheses are mismatched in the given expression.", nameof(expression));
                }
                output.Enqueue(token);
            }

            return string.Join(" ", output);
        }
    }
}
