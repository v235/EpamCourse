using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Task.BL
{
    public class Parser
    {
        public char GetFirstSymbol(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Incorrect input data! Value:'" + str + "' - is Bad Format");
            return str[0];
        }

        private int ConvertCharToInt(char c)
        {
            return c - '0';
        }

        private void Negative(char c, ref bool isNegative, ref int start)
        {
            switch (c)
            {
                case '-':
                {
                    isNegative = true;
                    start = 1;
                    break;
                }
                case '+':
                {
                    isNegative = false;
                    start = 1;
                    break;
                }

            }
        }

        public int ConvertStringToInt(string str)
        {

                bool isNegative = false;
                int start = 0;
                if (string.IsNullOrEmpty(str))
                    throw new ArgumentException("Incorrect input data! Value:'" + str + "' - is Bad Format");
                if (str.Length > 1)
                {
                    Negative(str[0], ref isNegative, ref start);
                }
                int result = 0;
                for (int i = start; i < str.Length; i++)
                {
                    if (str[i] < '0' || str[i] > '9')
                    {
                        throw new ArgumentException("Incorrect input data! Value:'" + str + "' - is Bad Format");
                    }
                    try
                    {
                    result = checked(result * 10 + ConvertCharToInt(str[i]));
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("Your value must be < " + int.MaxValue);
                    }
            }
                return isNegative ? -result : result;
        }
    }
}
