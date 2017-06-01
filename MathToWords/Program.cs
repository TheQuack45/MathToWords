using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MathToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            //string expr = Console.ReadLine();
            //// Ensures that every parenthesis or operator has a single space on either side of it
            //expr = Regex.Replace(expr, @"\s{0,}([\(\)\+\-\*\/\^])\s{0,}", " $1 ");
            //string converted = InfixToPrefix(expr);

            //string output = "";
            //foreach (string token in converted.Split(' '))
            //{

            //}

            Console.WriteLine("Expression:");
            string infix = Console.ReadLine();

            string postfix = InfixToPostfix.Convert(infix);
            string words = ExprToWords.Convert(postfix);
            Console.WriteLine(words);

            //string num = Console.ReadLine();
            //string words = NumToWords.Convert(num);
            //Console.WriteLine(words);

            Console.ReadKey();
        }
    }
}
