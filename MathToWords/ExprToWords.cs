using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToWords
{
    public static class ExprToWords
    {
        //public static string ConvertOld(string expr)
        //{
        //    string words = "";
        //    string[] tokens = expr.Split(' ');
        //    string operatorWaiting = "";
        //    Queue<string> operands = new Queue<string>();

        //    foreach (string token in tokens)
        //    {
        //        if (Utility.IsOperator(token))
        //        {
        //            operatorWaiting = token;
        //        }
        //        else if (Int32.TryParse(token, out int p))
        //        {
        //            operands.Enqueue(NumToWords.Convert(token));
        //            if (operands.Count == Utility.OperandCount[operatorWaiting])
        //            {
        //                words += String.Format(OperWords[operatorWaiting], operands.ToArray<string>()) + " ";
        //            }
        //        }
        //        else
        //        {
        //            throw new ArgumentException("An invalid token was found in the given expression: " + token, nameof(expr));
        //        }
        //    }

        //    return words;
        //}

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
                    while (opers.Count < ConfigReader.GetOperandCountFor(token))
                    {
                        opers.Add(operands.Pop());
                    }
                    opers.Reverse();
                    string str = String.Format(ConfigReader.GetWordsFor(token), opers.ToArray<string>());
                    operands.Push(str);
                }
            }

            return operands.Pop();
        }
    }
}
