using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToWords
{
    public static class Utility
    {
        public static bool IsOperator(string token)
        {
            List<string> operations = ConfigReader.GetCharacters();
            if (operations.Contains(token))
            {
                return true;
            }

            return false;
        }
    }
}
