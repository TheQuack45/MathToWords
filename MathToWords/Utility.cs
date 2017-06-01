using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToWords
{
    public static class Utility
    {
        public static Dictionary<string, int> OperandCount = new Dictionary<string, int>()
        {
            { "+", 2 },
            { "-", 2 },
            { "*", 2 },
            { "/", 2 },
            { "^", 2 },
            { "%", 2 },
        };

        public static bool IsOperator(string token)
        {
            switch (token)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "%":
                    return true;
                default:
                    return false;
            }
        }
    }
}
