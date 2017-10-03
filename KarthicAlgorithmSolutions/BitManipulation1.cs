using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class BitManipulation1 : Form
    {
        public BitManipulation1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number1 = Convert.ToInt32(this.textBox5.Text);
            int number2 = Convert.ToInt32(this.textBox6.Text);
            this.textBox7.Text = AddTwoNumbers(number1, number2).ToString();

        }

        //Logic Think about the base 10 addition in a different way, we can add just the number without carry and just the carry. The sum of the above two will give the actula sum.
        //likewise we can do the addition of base 2..Addition of two binary without carry is done by XOR. Addition of two binary number carry is done by && ( x && y)..
        //when we add the result make sure to left 1 of the carry result ..bcoz  ex 759 + 764 of just carry will be 111 and actullay the first digit doesn't have carry so the result is 1110
        //http://www.geeksforgeeks.org/add-two-numbers-without-using-arithmetic-operators/

        public int AddTwoNumbers(int x, int y)
        {

            // Iterate till there is no carry  
            while (y != 0)
            {
                // sum of carry
                int carry = x & y;

                // Sum of bits of x and y without carry
                x = x ^ y;

                // Carry is shifted by one so that adding it to x gives the required sum
                y = carry << 1;
            }
            return x;

        }

        public int Add(int x, int y)
        {
            if (y == 0)
            {
                return x;
            }
            else
            {
                return Add(x ^ y, (x & y) << 1);
            }
        }

        public uint f(uint a, uint b)
        {
            return (a != 0) ? f((a & b) << 1, a ^ b) : b;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int num1 = Convert.ToInt32(this.textBox10.Text);
            int num2 = Convert.ToInt32(this.textBox9.Text);

            this.textBox8.Text = MaxofTwoNumbers(num1, num2).ToString();

        }

        private int MaxofTwoNumbers(int num1, int num2)
        {
            int k = Sign(num1 - num2);
            int q = flip(k);
            return num1 * k + num2 * q;

            //int s = (A - B) >> 31;
            //return (A & ~s) | (B & s);

        }

        //This is an important function if the a value is positive it returns 1
        // and if the a value is negative it returns 0
        public int Sign(int a)
        {
            //Note here x is bit multiplication 
            int value = (a >> 31) & 0x1;
            return flip(value);
        }

        public int flip(int bit)
        {
            return 1 ^ bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Imagin with num1 is IntMax -2 and num2 is -15 the  (a -b ) will be IntMax + 17 and that value cannot be hanled by 32-bit int
            //so we need to handle the overflow 
            //overflow happens only when one number is positive and the other is negative
            //so find whether num1 and num2 are different signs if so

            //pseducode
            //if num1 and num2 are different sign
            //   k = sign(a); --here if a > 0 and b < 0 k will be 1
            //                     or   a < 0 and b > 0 k will be 0
            //else (if both have same sign then overflow won't happen
            // k = sign(a-b)


            int num2 = Convert.ToInt32(this.textBox9.Text);
            int num1 = Convert.ToInt32(this.textBox10.Text);

            this.textBox8.Text = MaxofTwoNumbersWithOverflowHandle(num1, num2).ToString();
        }



        private int MaxofTwoNumbersWithOverflowHandle(int a, int b)
        {
            int sa = Sign(a); //if a is postive then 1 else 0
            int sb = Sign(b); //if b is positive then 1 else 0
            int c = a - b;
            int sc = Sign(c);

            //we need to set the value of k . if both are different sigh k = sign(a) else k = sign(a-b)
            int use_sign_ofa = sa ^ sb;  //xor if both are same the value will be 0 else 1
            int use_sign_ofc = flip(use_sign_ofa);

            int k = use_sign_ofa * sa + use_sign_ofc * sc;

            int q = flip(k);

            return a * k + b * q;
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int input = Convert.ToInt16(this.textBox13.Text);

            string output = CheckIfDivisbleBy8(input) ? "Divisible" : "Not Divisible";

            this.textBox11.Text = output;


        }

        private bool CheckIfDivisbleBy8(int input)
        {
            //only if last three binary bits are 0..this will be true
            if ((input >> 3) << 3 == input)
            {
                return true;
            }

            return false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(this.textBox14.Text);

            this.textBox12.Text = IsPowerof2(number) ? "True- Power of 2" : "False - Not power of 2";
        }

        private bool IsPowerof2(int number)
        {
            return ((number & (number - 1)) == 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //http://www.geeksforgeeks.org/count-set-bits-in-an-integer/

            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);

            int count = 0;
            foreach (int value in array)
            {
                count += CountNumberofBits(value);
            }
            this.textBox15.Text = count.ToString();

        }

        // Brian Kernighan’s Algorithm:

        //1  Initialize count: = 0
        //2  If integer n is not zero
        //   (a) Do bitwise & with (n-1) and assign the value back to n
        //       n: = n&(n-1)
        //   (b) Increment count by 1
        //   (c) go to step 2
        //3  Else return count
        //Time Complexity: log n where n is number of bits
        private int CountNumberofBits(int number)
        {
            int count = 0;
            while (number > 0)
            {
                number = number & (number - 1);
                count++;
            }

            return count;
        }


        //Time o(n) where n is number of bits
        private int CountNumberofBitsNaive(int number)
        {
            int count = 0;
            while (number > 0)
            {

                count = count + (number & 1);
                number = number >> 1;
            }

            return count;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }



    }
}
