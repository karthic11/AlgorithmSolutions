using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.SystemDesign
{
    public class KarthicCeaserCipher
    {
            public string Shift(string plainIn, int shift)
            {
                //int z = (int)'Z'; // Ascii value  of z will be 90
                //int a = (int)'A'; // Ascii value of a is 65
                char[] output = new char[plainIn.Length];

                for (int i = 0; i < plainIn.Length; i++)
                {
                    int num = plainIn[i] + shift;
                    output[i] = Convert.ToChar(num > 90 ? num = num - 26 : (num < 65 ? num = num + 26: num));
                }
                return new string(output);
            }

            public string Encrypt(string message, int shiftN)
            {
                message = message.TrimEnd(' ').ToUpper();
                return Shift(message, shiftN);
            }

            public string Decrypt(string message, int amountShift)
            {
                return Shift(message, -amountShift);
            }
        }
    }


