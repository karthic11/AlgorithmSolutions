using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public static class BitHelper
    {

        //This method gets the (i +1)th bit of the given number..
        //Here we are returning boolean..If true means the bit is 0 else 1
        //Input: number: 00001000 i = 4
        public static Boolean GetBit(int number, int i)
        {

            //1<<i will shift 1 by i bits
            //00000001 will become 00010000
            //perform and with the result
            //00001000  and
            //00010000
            //--------
            //00000000 
            //return true
            return ((number & (1 << i)) != 0);
        }


        //This method sets the (i +1)th bit of the number to 1
        //Input: number: 00001000 i = 4
        public static int SetBit(int number, int i)
        {

            //1 << i shifts the number by i digits... 00000001..If i is 4 then the number i is shifter to 4 positions on the left 00010000
            //Now perform or operation with the result
            //00001000
            //00010000
            //--------
            //00011000
            //By doing this we set the 5th digit of the given number to 1

            return number | (1 << i);
        }

        //This method will clear the (i +1)th bit and keep the remaining bit unchanged...looks like this is to cleat 1 bit on the (i+1)th bit
        //Input: number: 00001000 i = 4
        public static int ClearBit(int number, int i)
        {
            //00000001  <<  4 will be
            //00010000... we reverse the output so that will be
            //11101111 ...and then we perfomr add with num
            //00001000
            //---------
            //00001000

            int mask = ~(1 << i);
            return number & mask;
        }

        //This method will clear the (i +1)th bit and keep the remaining bit unchanged...looks like this is to cleat 1 bit on the (i+1)th bit
        //Input: number: 00001000 i = 4
        public static int ClearBitMSB(int number, int i)
        {
            //00000001  <<  4 will be
            //00010000... we reverse the output so that will be
            //11101111 ...and then we perfomr add with num
            //00001000
            //---------
            //00001000

            int mask = ~(1 << i);
            return number & mask;
        }



         //Bit Shift Operators
         //1) <<  - Left shift - both logical and arithmetic shifts
         //2) >>  - Right shift - Arithmetic signed shifts
         //3) >>> - Logical Unsigned shift

        //Shifts are not cirulating The digit that gets shifted "off the end" is lost
        //example: 11100000 00000000 00000000 00000000 << 1
        // output: 11000000 00000000 00000000 00000000


        public static int leftshift(int number, int shiftnumber)
        {
          //Integers are stored, in memory, as a series of bits. For example, the number 6 stored as a 32-bit int would be:
          //Input Number: 6
          //00000000 00000000 00000000 00000110
          //6 << 1 will shift 1 bits to the left
          //output: 00000000 00000000 00000000 00001100  Ans: 12   (6 * 2^1) = 12
          //6 << 2 will shift 2 bits to the left
          // output: 00000000 00000000 00000000 00011000    Ans:24    (6 * 2^2) = 24

          return number << shiftnumber;
           
            

        }

        public static int Rightshift(int number, int shiftnumber)
        {
          //move bits to the right
          //Integers are stored, in memory, as a series of bits. For example, the number 6 stored as a 32-bit int would be:
          //Input Number: 6
          //00000000 00000000 00000000 00000110
          //6 >> 1 will shift 1 bits to the right
          //output: 00000000 00000000 00000000 00000011 Ans: 3  (6 / 2^1) = 3
          //6 >> 2 will shift 2 bits to the right
          // output: 00000000 00000000 00000000 00000001    Ans:1   (6 / 2^2) = 4

          return number >> shiftnumber;



        }
    }
}
