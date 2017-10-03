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
    public partial class TechCompanyPage6 : Form
    {
        public TechCompanyPage6()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Logic:
            //There are 2^32 possible intergers = 4 Billion integers
            //Given memory 1 GB = 1023 MB = 10 pow 3 mb = 10 pow 6 kb = 10 pow 9 bytes = 8 * 10 pow 9 = 8 billion bits
            //We are not going to store int32 value as you know each int needs 32 bits which willbe 32 * 4 billion bits
            //We can create 4 billion bits and represent each bit for each int  
            //Creating 4 billion bits will only take 512mb memory

            //Initialize all 4 billion bits to 0 (by default)
            //Read the file and for each number n..find the nth bit set it to 1..duplicates shouldn't be a problem
            // after done
            //read the bits and return the first bit that has 0

            //update:
            //Unfortunately we don't have datatype to store bits but we have byte which is of 8 bits
            //so consider 1 bit  as index 1 in byte 1  000000001
            //byte[0] will have  0 to 7
            //byte[1] will have  8 to 15
            //byte[2] will have  16 to 23
            //....untill byte[4billion/8] will have x---4billion
            //for simplity imagine the file has number 0 to 10

            //we should have 4 billion positive interger..C# has only 2 billion integers
            //+1 is bcoz index are 0 based .. number 8 will have 2 byte[0] and byte[1]
            long numberofints = (long)Int32.MaxValue + 1;
            //we are using bytes array not bit..so update the nth bits on the bytes array
            //4 billion number need 4 billion bits = 4 billion/8 bytes
            //we can get number bytes array length can be set like this
            //byte[] bitfield = new byte[(int)(numberofints / 8)];


            //But here we are doing this logic test with small set of numbers

            int size = Convert.ToInt32(this.textBox16.Text);
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);

            //this.textBox8.Text = maxvalue.ToString(); we need to do extra logic to do value and its easy..no time now
            List<int> missingnums = FindMissingNumberUsingBytesLogic(size, input);

             StringBuilder sb = new StringBuilder();
             foreach (int number in missingnums)
             {
                 sb.Append(number).Append(",");
             }


             this.textBox13.Text = sb.ToString();
          
        }



        public List<int> FindMissingNumberUsingBytesLogic(int size, int[] input)
        {
            List<int> missingnums = new List<int>();

            //we are using bytes array not bit..so update the nth bits on the bytes array
            //4 billion number need 4 billion bits = 4 billion/8 bytes
            byte[] bitfield = new byte[(int)Math.Ceiling((double)size / 8)];
     
            //parse the file and read input...for simplity imagine the file has number 0 to 10
            //Iterate from number 1 to 20
            for (int i = 0; i < input.Length; i++)
            {
                /*
                here we need to set the ith bit to 1...
                Example no: 10    we know byte[0] will have  0 to 7 and byte[1] will have  8 to 15
                we need to byte[1] and updates it's 2 index byte from right
                0000 0000  we need to change this to 
                0000 0100  
                
                To do this 
                 1<< (2) = shifts 2 index in left will be 0000 0100  
                we are making or operating with the existing value
                0000 0100 or
                existingvalue

                update
                 we need [i/8] to find the which array of bytes has the value we need
                 we need (i % 8) to find the bit of the byte array that need to be set
                */


                int number = input[i];
                int bitposition = number - 1;//bit position is used to store the number 1 is oth position so that we don't waste oth position else it will be stored in 1st position

                int value = bitfield[bitposition / 8] | 1 << (bitposition % 8);
                //i/8 to get the right byte
                bitfield[bitposition / 8] = (byte)value;
            }

            //we are done reading the file..return the first empty or zero value


            //here iterate from 0 to 4billion/8
            for (int num = 0; num < size; num++)
            {
                int i = num / 8;  //this will give  byte array index
                string bitdigits = Convert.ToString(bitfield[i], 2);

                for (int j = 0; j < 8; j++)
                {


                    //retrive the individual bits of each byte
                    //when 0 if found retur the correspoding bit
                    if ((bitfield[i] & (1 << j)) == 0)
                    {

                        //get the actual int value
                        int missingnumber = i * 8 + j + 1;
                        missingnums.Add(missingnumber);


                    }

                }
            }

            //because we store the number 1 in oth index
            return missingnums;

        }
    }
}
