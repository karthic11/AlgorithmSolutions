using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class Page2 : Form
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Input array:
            //1 5  9  13
            //2 6  10 14 
            //3 7  11 15 
            //4 8  12 16
          
            //After rotating 90 degree the resulting array will be

            // Result array:
            // 4  3  2  1
            // 8  7  6  5
            //12 11  10 9
            //16 15  14 13

            int[,] input = new int[,]{{1, 5,  9,  13},
                                      {2, 6, 10, 14 },
                                      {3,7, 11, 15 },
                                      {4, 8,  12 ,16},
                                   
            };



            //we have to approcah via the ring and widht method
            //n is the size of the matrix and the rings will be n/2
            //iterate through rings..For 4x4 matrix the ring number is 0 and 1 (2 rings)
            for (int ringno = 0; ringno < input.GetLength(0)/2; ringno++)
            {
                //second loop is for iternation along the width of the matrix
                //width changes for each ring
                int widthstartvalue = ringno;//for ring 0 value is 0
                int widthendvalue = (input.GetLength(0) - 1) - ringno;

                for (int i = widthstartvalue; i < widthendvalue; i++)  //Important here i < widthendvalue not i <= widthendvalue bocz the last item is already modified
                {
                    int offset = i - widthstartvalue;

                    //since the values in the matrix is int
                    //swap the values based on the degree of rotation
                    //for 90 degree

                    //put top on temp
                    int temp = input[widthstartvalue, i];
                    //put left on top
                   //using offset value instead of i is important here
                    input[widthstartvalue, i] = input[widthendvalue - offset, widthstartvalue];
                    //put bottom on left

                    input[widthendvalue - offset, widthstartvalue] = input[widthendvalue, widthendvalue - offset];

                    //put right on bottom

                    input[widthendvalue, widthendvalue - offset] = input[i, widthendvalue];

                    //put top in temp to the right
                    input[i, widthendvalue] = temp;

                    //Important: we are using offset instead of (Matrix.Length -1) - cell
                    //Offset is only used when the range goes from 03 02 01 00 bcoz the 2nd right 22 21 (widthend starts from 2 and the required values are 22 21 so the offset will be 0 or 1)

                }
            }

            //test
            int[,] result = input;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Input array:
            //1 0 1 1 0
            //0 1 1 1 0
            //1 1 1 1 1
            //1 0 1 1 1
            //1 1 1 1 1


            // Result array:
            //0 0 0 0 0
            //0 0 0 0 0
            //0 0 1 1 0
            //0 0 0 0 0
            //0 0 1 1 0


            int[,] input = new int[,]{{1,0,1,1,0},
                                      {0,1,1,1,0},
                                      {1,1,1,1,1},
                                      {1,0,1,1,1},
                                      {1,1,1,1,1}
            };


            int[] rows = new int[input.GetLength(0)];
            int[] columns = new int[input.GetLength(1)];


            //Iterare through each row
            for (int i = 0; i < input.GetLength(0); i++)
            {

                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i,j] == 0)
                    {
                        //when we encounter value o on the iteration set the row and column value to 1 or any..
                        //later we change the value 1 to 0

                        if (rows[i] != 1)
                        {
                            rows[i] = 1;
                        }

                        if (columns[j] != 1)
                        {
                            columns[j] = 1;
                        }

                    }
                }
                
            }

          //Now the rows no that has o are in row[] with the value of 1
            //the columns with the 0 are in column[] with the value of 1

            //check the single d array and set the matrix value to zero

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (rows[i] == 1 || columns[j] == 1)
                    {
                        input[i, j] = 0;
                    }
                }
            }



            // Result array:
            //0 0 0 0 0
            //0 0 0 0 0
            //0 0 1 1 0
            //0 0 0 0 0
            //0 0 1 1 0


        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Input array:
            // 1 2 3
            // 4 9 6
            // 5 8 7

            //output 1,2,3,4...9

            int[,] input = new int[,] { { 1, 2, 3 }, { 4, 9, 6 }, { 5, 8, 7 } };

            int[] ouptut = new int[input.GetLength(0) * input.GetLength(1)];

            int outindex =0;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {

                    ouptut[outindex] = input[i, j];
                    outindex++;
                }
            }

            string s = "test";
            Array.Sort(ouptut);
            
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.textBox7.Text = IsRotatated(this.textBox8.Text, this.textBox9.Text) ? "Yes" : "No";

        }


        private bool IsRotatated(string s1, string s2)
        {
            //To find a string is rotation of another
            //If the string is rotated, there will be for sure one occurence of the rotated string in the concatenated string

            if (s1.Length != s2.Length)
            {
                return false;
            }
            else
            {
                //concat s1
                s1 = s1 + s1;
               //As per assumption check the method


                return s1.Contains(s2);

                //return IsSubstring(s1, s2)

              
                return true;

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            ArrayAndStringP3 frm = new ArrayAndStringP3();
            frm.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
