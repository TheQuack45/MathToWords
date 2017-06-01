using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MathToWords
{
    public static class NumToWords
    {
        private static readonly Dictionary<string, string> Words = new Dictionary<string, string>()
        {
            { "00", "" },
            { "0", "" },
            { "1", "one" },
            { "2", "two" },
            { "3", "three" },
            { "4", "four" },
            { "5", "five" },
            { "6", "six" },
            { "7", "seven" },
            { "8", "eight" },
            { "9", "nine" },
            { "10", "ten" },
            { "11", "eleven" },
            { "12", "twelve" },
            { "13", "thirteen" },
            { "14", "fourteen" },
            { "15", "fifteen" },
            { "16", "sixteen" },
            { "17", "seventeen" },
            { "18", "eighteen" },
            { "19", "nineteen" },
            { "20", "twenty" },
            { "30", "thirty" },
            { "40", "fourty" },
            { "50", "fifty" },
            { "60", "sixty" },
            { "70", "seventy" },
            { "80", "eighty" },
            { "90", "ninety" },
        };

        private static readonly Dictionary<int, string> Denominations = new Dictionary<int, string>()
        {
            { 0, "" },
            { 1, "thousand" },
            { 2, "million" },
            { 3, "billion" },
        };

        public static string Convert(string num)
        {
            string words = "";
            //if (num < 0)
            //{
            //    throw new NotImplementedException("Negatives not supported yet.");
            //}
            //else if (num <= 20)
            //{
            //    words += Words[num];
            //}
            //else if (num < 100)
            //{
            //    int ones = num % 10;
            //    int tens = num - ones;
            //    words += Words[tens];

            //    if (ones != 0)
            //    {
            //        words += " " + Words[ones];
            //    }
            //}
            //else if (num < 1000)
            //{

            //}

            //if (num > 1000000000)
            //{
            //    int billions = num - (num % 100000000);
            //    words += Words[billions];
            //    num -= billions;
            //}

            //string[] splitBad = Regex.Split(num, "");
            //string[] digits = new string[splitBad.Length - 2];
            //for (int i = 1; i < splitBad.Length - 1; i++)
            //{
            //    digits[i - 1] = splitBad[i];
            //}

            if (!Int32.TryParse(num, out int p))
            {
                throw new ArgumentException("The given string must be a valid integer.", nameof(num));
            }

            if (num.StartsWith("-"))
            {
                // Negative number
                words += "negative ";
                num = new string(num.Skip(1).ToArray<char>());
            }

            if (num == "0")
            {
                words = "zero";
                return words;
            }

            char[] rev = num.ToCharArray();
            Array.Reverse(rev);
            string[] groups = Regex.Matches(new string(rev), ".{1," + 3 + "}").Cast<Match>().Select(m => m.Value).ToArray<string>();
            for (int i = 0; i < groups.Length; i++)
            {
                char[] arr = groups[i].ToCharArray();
                Array.Reverse(arr);
                groups[i] = new string(arr);
            }
            Array.Reverse(groups);
            
            for (int i = 0; i < groups.Length; i++)
            {
                string group = groups[i];

                if (Int32.Parse(group) == 0)
                {
                    continue;
                }

                if (group.Length == 3)
                {
                    string hundreds = group.First().ToString();
                    if (hundreds != "0")
                    {
                        words += Words[hundreds] + " hundred ";
                    }

                    if (Int32.Parse(group.Substring(1, 2)) < 20)
                    {
                        words += Words[Int32.Parse(group.Substring(1, 2)).ToString()];
                    }
                    else
                    {
                        string tens = group.Substring(1, 1);
                        if (tens != "0")
                        {
                            // TODO: Could change to add "ty" suffix in this code instead of in dict
                            words += Words[tens + "0"] + " ";
                        }

                        string ones = group.Last().ToString();
                        if (ones != "0")
                        {
                            words += Words[ones] + " ";
                        }
                    }
                }
                else if (group.Length == 2)
                {
                    if (Int32.Parse(group.Substring(0, 2)) < 20)
                    {
                        words += Words[group.Substring(0, 2)];
                    }
                    else
                    {
                        string tens = group.Substring(0, 1);
                        if (tens != "0")
                        {
                            // TODO: Could change to add "ty" suffix in this code instead of in dict
                            words += Words[tens + "0"] + " ";
                        }

                        string ones = group.Last().ToString();
                        if (ones != "0")
                        {
                            words += Words[ones] + " ";
                        }
                    }
                }
                else
                {
                    string ones = group.Last().ToString();
                    if (ones != "0")
                    {
                        words += Words[ones] + " ";
                    }
                }

                words += Denominations[groups.Length - i - 1] + " ";
            }

            words = Regex.Replace(words, " {1,}$", "");

            return words;
        }
    }
}
