using Puzzles.DataStructures.Games;
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
    public partial class MicrosoftOTSPage1 : Form
    {
        public MicrosoftOTSPage1()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          //  int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox15.Text);
            int firstnumber = Convert.ToInt32(this.textBox15.Text);
            int secondnumber = Convert.ToInt32(this.textBox1.Text);

            //Test Cases
            //this.textBox12.Text = ReverseFibonacciSeries(21, 13);
            //this.textBox12.Text = ReverseFibonacciSeries(13, 21);
            //this.textBox12.Text = ReverseFibonacciSeries(21, 21);
            //this.textBox12.Text = ReverseFibonacciSeries(1, 1);
            //this.textBox12.Text = ReverseFibonacciSeries(-21, -13);
            //this.textBox12.Text = ReverseFibonacciSeries(0, -1);
            //this.textBox12.Text = ReverseFibonacciSeries(0, 0);
            //this.textBox12.Text = ReverseFibonacciSeries(7, 5);
            //this.textBox12.Text = ReverseFibonacciSeries(6, 5);

            this.textBox12.Text = ReverseFibonacciSeries(firstnumber, secondnumber);
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox15.Text);
            int firstnumber = Convert.ToInt32(this.textBox15.Text);
            int secondnumber = Convert.ToInt32(this.textBox1.Text);
            this.textBox12.Text = ReverseFibonacciSeriesByIteration(firstnumber, secondnumber);

  

        }

    

        private string ReverseFibonacciSeries(int firstnum, int secondnum)
        {
            //check if the given two numbers given are positive numbers
            if (firstnum < 0 || secondnum < 0)
            {
                throw new Exception("Invalid Input: Input cannot be negative");
            }
            //check to make sure first number is not smaller than second number
            else if (firstnum < secondnum)  
            {
                throw new Exception("Invalid Input: First number cannot be lesser than Second number ");
            }
           
            StringBuilder sb = new StringBuilder();
            sb.Append(firstnum.ToString()).Append(',');
            sb.Append(secondnum.ToString()).Append(',');
            ReverseFibonacciSeriesRecursion(firstnum, secondnum, sb);

            return sb.ToString();
        }

        //Here x is lastno and y is lastbeforenumber
        private void ReverseFibonacciSeriesRecursion(int x, int y, StringBuilder sb)
        {
            if (y < 1)
            {
                return;
            }
            else
            {
                int z = x - y;
                sb.Append(z.ToString()).Append(',');
                x = y;
                y = z;
                ReverseFibonacciSeriesRecursion(x, y, sb);

            }
        }



        //Assumption: 
        //1) Based on the given example, I have assumed that given number can be any two numbers not necessarily  valid fibonacci numbers
        //2) Output will not have negative numbers

        //Note: This algorithm can also be modified to enter a valid fibonacci number as first number and second number using the fibonnaci property
        //A number is Fibonacci if and only if one or both of (5*n2 + 4) or (5*n2 – 4) is a perfect square
        //If we check for the above property for the given input (first and second number), then all the number will be valid fibonnaci numbers

        //test cases
        //ReverseFibonacciSeriesByIteration(80, 50);
        //ReverseFibonacciSeriesByIteration(50, 80);
        //ReverseFibonacciSeriesByIteration(50, 50);
        //ReverseFibonacciSeriesByIteration(6, 5);
        //ReverseFibonacciSeriesByIteration(1, 1);
        //ReverseFibonacciSeriesByIteration(0, 0);
        //ReverseFibonacciSeriesByIteration(5, -4);
        //Time Complexity: T(n) = T(n) - T(n-1)
        //Space: O(n)


        private string ReverseFibonacciSeriesByIteration(int firstnum, int secondnum)
        {
            try
            {
                //check if the given two numbers are positive numbers
                if (firstnum < 0 || secondnum < 0)
                {
                    throw new Exception("Invalid Input: Input cannot be negative");
                }
                //check to make sure first number is not smaller than second number
                else if (firstnum < secondnum)
                {
                    throw new Exception("Invalid Input: First number cannot be lesser than Second number ");
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(firstnum.ToString()).Append(',');
                sb.Append(secondnum.ToString()).Append(',');

                //Assuming we need only positive numbers
                while (secondnum > 0 && (firstnum - secondnum >= 0))
                {
                    //difference between first and second number
                    int nextnum = firstnum - secondnum;
                    sb.Append(nextnum).Append(',');
                    firstnum = secondnum;
                    secondnum = nextnum;
                }

                return sb.ToString();

            }
            catch (Exception)
            {
                //Hnadling exception code here
                throw;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //construct a tw array
            // 00 01 02 03
            // 10 11 12 13
            // 20 21 22 23
            // 30 31 32 33
            // 40 41 42 43

            //Imagine black is in 12, 21 23 and 32 position
            //White cannot be place in 22 bcoz the neightbors belong to black/enemy and it will not have any libery

           //for simplicity
            //int[,] matrix = new int[,] {  {0,0,0,0},
            //                              {0,0,2,0},
            //                              {0,2,0,2},
            //                              {0,0,2,0},
            //                              {0,0,0,0}};

            ////we are trying to place white in the middle of 2 that is in the position 22
            //bool[,] blackstones = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            ////set black stones
            //blackstones[1, 2] = true;
            //blackstones[2, 3] = true;
            //blackstones[2, 1] = true;
            //blackstones[3, 2] = true;
            ////set white stones
            //bool[,] whitestones = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            ////set
            ////blackstones[1, 2] = true;
            ////blackstones[2, 3] = true;
            ////blackstones[2, 1] = true;
            ////blackstones[3, 2] = true;
            //////set white stones
            ////point is 2,2
            //bool result = IsLegalMove(false, 2, 2, blackstones, whitestones);

            GoGame obj = new GoGame(4, 5);


        }

        //isblackstore = true for black and false for white
        //x and y are co-ordinates of the  stone being placed
        //bool[,] blackstones current state of blackstones on the board
        //similarly
        
        //public bool IsLegalMove(bool isBlackStone, int x, int y, bool[,] blackStones, bool[,] whitestones)
        //{
        //    //check for outside bonds
        //    if(x < 0 || x >= 

        //}


        public bool FindPath(int startx, int starty, int size, char endgoal, char[,] maze)
        {

            //Base case
            //check for outside bounds
            if (startx < 0 || startx >= size || starty < 0 || starty >= size)
            {
                return false;
            }

            //check for obstacle
            if (maze[startx, starty] == '#')
            {
                return false;
            }
            //check if already visited..to prevent loop
            if (maze[startx, starty] == '+')
            {
                return false;
            }

            //check for goal
            if (maze[startx, starty] == endgoal)
            {
                return true;
            }

            //when the code comes means the path is free and goal not found so traverse in all direction

            //Mark the current point as visited
            maze[startx, starty] = '+';

            //search in north of the point
            if (FindPath(startx - 1, starty, size, endgoal, maze) == true)
            {
                return true;
            }
            //search in the east of the point
            if (FindPath(startx, starty + 1, size, endgoal, maze) == true)
            {
                return true;
            }
            //search in the south of the point
            if (FindPath(startx + 1, starty, size, endgoal, maze) == true)
            {
                return true;
            }
            //search in the west of the point
            if (FindPath(startx, starty - 1, size, endgoal, maze) == true)
            {
                return true;
            }

            //goal not found ..unmark the point
            //This is called backtracking bcoz we come back to this where we mared as '+' and update it to '.' bcoz the position doesn't lead to the destination
            maze[startx, starty] = '.';

            return false;


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

       

    }
}
