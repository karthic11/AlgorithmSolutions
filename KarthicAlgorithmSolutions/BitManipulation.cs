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
    public partial class BitManipulation : Form
    {
        public BitManipulation()
        {
            InitializeComponent();
        }

        private void BitManipulation_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int number = Convert.ToInt16(this.textBox7.Text, 2);
            string test = Convert.ToString(number, 2);
            int index = Convert.ToInt16(this.textBox8.Text);

            bool result = BitHelper.GetBit(number, index);

            this.textBox9.Text = result ? "1" : "0";

        }

        

      

        private void button3_Click(object sender, EventArgs e)
        {

            int number = Convert.ToInt16(this.textBox10.Text, 2);
            //00001000
            string test = Convert.ToString(number, 2);
            //4
            int index = Convert.ToInt16(this.textBox6.Text);

            int output = BitHelper.SetBit(number, index);

            this.textBox5.Text = Convert.ToString(output, 2);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt16(this.textBox13.Text, 2);
            //00001000
            string test = Convert.ToString(number, 2);
            //4
            int index = Convert.ToInt16(this.textBox12.Text);

            int output = BitHelper.ClearBit(number, index);

            this.textBox11.Text = Convert.ToString(output, 2);

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt16(this.textBox13.Text, 2);
            //00001000
            string test = Convert.ToString(number, 2);
            //4
            int index = Convert.ToInt16(this.textBox12.Text);

            int output = BitHelper.ClearBit(number, index);

            this.textBox11.Text = Convert.ToString(output, 2);


        }

        private void button7_Click(object sender, EventArgs e)
        {

          int input = Convert.ToInt32(this.textBox2.Text, 2);

          this.textBox1.Text = Convert.ToString(BitHelper.leftshift(input, Convert.ToInt32(this.textBox3.Text)), 2);


        }

        private void button8_Click(object sender, EventArgs e)
        {
          int input = Convert.ToInt32(this.textBox2.Text, 2);

          this.textBox1.Text = Convert.ToString(BitHelper.Rightshift(input, Convert.ToInt32(this.textBox3.Text)), 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        // Negation: The conversion of a binary digit, or bit, into its opposite. For example, a 1 converted into a 0 is considered a negation. A complete negation of an 8-bit binary number may look similar to the example below.
        //Binary Number: 01100101
        //Negation: 10011010

        /*Negation in the Binary System
         *1) Signed Magnitude:
         *    The simplest way to indicate negation is signed magnitude. In signed magnitude, the left-most bit is not actually part of the number, but is just the equivalent of a +/- sign.
         *    "0" indicates that the number is positive, "1" indicates negative.
         *    Eg: In binary 12 is represented as 0000 1100
         *    -12 will be represented as         1000 1100  (the left most bit or Most significant bit represent the sign 0 mean positive number and 1 means negative number
         *2) One's Complement:
         *     In one's complement, positive numbers are represented as usual in regular binary. However, negative numbers are represented differently.
         *     To negate a number, replace all zeros with ones, and ones with zeros. It means flip the bits. 
         *     As in signed magnitude, the leftmost bit indicates the sign (1 is negative, 0 is positive). To compute the value of a negative number, flip the bits and translate as before.
         *     Thus, 12 would be 00001100, and -12 would be 11110011. 
         *3) Two Complement:
         *      Same as One complement and we add 1 if the number is negative
         *       -12 will be (1111 0011) + 1 = 1111 0011
         *                                     0000 0001  (+)
         *                                    -----------
         *                                     1111 0100
         *
         * 
         * Most Significant Bit (MSB also called the high-order bit): It is the bit position in a binary number having the greatest value. Leftmost bit
         * 
         *      
         * Least Significant Bit (LSB or Rightmost Bit): It is the bit position in a binary integer giving the units value, that is, determining whether the number is even or odd. 
         *     To get LSB we do x &= ~x + 1 
         */

        private void button9_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt16(this.textBox16.Text, 2);
            this.textBox17.Text = GetLeastSignificantBitPosition(number).ToString();

        }

        private int GetLeastSignificantBitPosition(int number)
        {
            //return (number & ~number) + 1;
            return number & ~ (number - 1);
        }
    }
}
