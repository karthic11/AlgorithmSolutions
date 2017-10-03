using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.SystemDesign
{
    public static class KarthicAtoi
    {

        // A simple atoi() function
        //public static int myAtoi(char[] str)
        //{
        //    int res = 0; // Initialize result

        //    // Iterate through all characters of input string and update result
        //    for (int i = 0; i < str.Length; ++i)
        //    {
        //        res = res * 10 + str[i] - '0';
        //    }

        //    // return result.
        //    return res;
        //}


        // A utility function to check whether x is numeric
        private static bool isNumericChar(char x)
        {
            return (x >= '0' && x <= '9') ? true : false;
        }

        // A simple atoi() function. If the given string contains
        // any invalid character, then this function returns 0
        public static int myAtoi(char[] str)
        {
            if (str.Length == 0)
            {
                return 0;
            }

            int res = 0;  // Initialize result
            int sign = 1;  // Initialize sign as positive
            int i = 0;  // Initialize index of first digit

            // If number is negative, then update sign
            if (str[0] == '-')
            {
                sign = -1;
                i++;  // Also update index of first digit
            }

            // Iterate through all digits of input string and update result
            for (int j = i; j < str.Length; ++j)
            {
                if (isNumericChar(str[j]) == false)
                    return 0; // You may add some lines to write error message
                // to error stream

                res = res * 10 + str[j] - '0';
            }

            // Return result with sign
            return sign * res;
        }

        public static string ConvertIntToString(int number)
        {
            StringBuilder sb = new StringBuilder();
            int value = number;
            while (value > 0)
            {
                int digit = value % 10;

                sb.Insert(0, (char)((int)'0' + digit));

                value = value / 10;
            }

            return sb.ToString();
        }

    }
}
