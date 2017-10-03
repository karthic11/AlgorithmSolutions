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
    public partial class LogicalProblems : Form
    {
        public LogicalProblems()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            int input = Convert.ToInt32(this.textBox1.Text);
            this.textBox2.Text = GetExcelColumnName(input);


        }

        //Things to know Every key board character has integer value. 
        //The value of o means "", 1 means and the capital alphabets starts from 65th 65 = A, 66 = B etc 100 = 'a'
        //Convert.ToChar(value)...if 
        //Assumption A = 1..ei col starts with A..if one is passed we return A
        //Logic:  A, B, C...Z, AA,AB,AC...AZ, BA...Bz
        //ABC vlaue will be  1 *(26^2) + 2 * (26 ^1) + 3 * (26^0)
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                //65 stands for A..since we have to add from 65..the calcuation is based on excluding A (1)
                //get the reminder
                modulo = (dividend - 1) % 26;
                //Assumption A = 1
                //The reason we have 65 + modulo..modulo value might be zero and at those cases we need to have A
                //get the char value of the reminder
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                //since the excel column goes from A...Z and then AA....AZ and BA....BZ...The highest 26 divisble of (dividend - mod) will be the next last
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string input = this.textBox4.Text;
            this.textBox3.Text = GetColumnNumber(input).ToString();


        }


        //Logic:  A, B, C...Z, AA,AB,AC...AZ, BA...Bz
        //ABC vlaue will be  1 *(26^2) + 2 * (26 ^1) + 3 * (26^0)
        //The value will be calculated to the power of 26 and the name should be substituted by the equivalent col int value..
        //And we know that in .NET char capital 'A' starts from 65..From this we can calculate the any alphabets equivalednt excel col value
        public static int GetColumnNumber(string name)
        {
            int number = 0;
            int pow = 1;

            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                //This is for every iteration, the pow is mulitpled by 26
                pow *= 26;
            }

            return number;
        }



        public static int NumberFromExcelColumn(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        private void LogicalProblems_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int n = Convert.ToInt16(this.textBox6.Text);
            //this.textBox5.Text = GetTrailingZeroNotEffecient(n).ToString();

            this.textBox5.Text = GetTrailingZeroCountEffecient1(n).ToString();

        }

        public int GetTrailingZeroCountEffecient1(int number)
        {
            //Logic: Trailing Zero  1000 has 3 trailing zero...100 has 2 trailing zero...
            //Number of zero at the end is called trailing zero
            //Find no of trailing zero in 20!- 
            //20! = 20 * 19 * 18 * 17.....* 2 *1
            //Here the trailing zero is formed by facotors of 10. Multiples of 10 are factors of both of 5 and 2..
            //so we can find the factory of 5 and 2 and the intersection that is the minumun of the 5 or 2 will be the no of trailing aero
            //Imprtant factors are different from multiples..

            //so loop throug the each number and count the number of factory of each number


            //update 6/25/2015
            //To find number of factors of 5 in a number..
            //First see whether a number is divisble by 5
            //if it is. then find number of time it can be divisible 
            //eg  5 = 1, 10 (5*2) = 1, 25 = 2, 125 = 3, 50 = 2
            int sum = 0;
            for (int i = number; number > 0; number--)
            {
                sum = sum + FindFactorof5(number);

            }

            return sum;

        }

        //Find number of factors of given number
        public int FindFactorof5(int number)
        {
            //logic for factor 5:  5 has one factor (5 * 1) , 10 ( 5 * 2)..25 has two factors 

            int count = 0;

            if (number % 5 == 0)
            {
                count++;
                number = number / 5;
            }

            return count;
        }

        public int GetTrailingZeroNotEffecient(int number)
        {

            //calculate the factorial of the number 

            int sum = number;
            //get factorial of a number

            for (int i = number; i > 1; i--)
            {
                sum = sum * (i - 1);
            }

            //Note: int can store only the max of 2,147,483,647
            int factorialvalue = sum;
            //find the no of trailing zero in the result

            //now see how many times this number cab be divisible by eg 1200  result =2
            int count = 0;
            while (factorialvalue % 10 == 0)
            {
                count++;
                factorialvalue = factorialvalue / 10;

            }

            return count;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt16(this.textBox6.Text);
            this.textBox5.Text = GetTrailingZeroNotEffecient(n).ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt16(this.textBox6.Text);
            this.textBox5.Text = GetTrailingZeroMostEfficient(n).ToString();
        }

        public int GetTrailingZeroMostEfficient(int number)
        {
            int count = 0;

            for (int i = 5; number / i > 0; i *= 5)
            {
                count += number / i;
            }

            return count;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.textBox8.Text);
            //pass the highest denom available
            this.textBox7.Text = FindNoOfWaysToMakeChange(value, 25).ToString();
        }


        public int FindNoOfWaysToMakeChange(int value, int denom)
        {
            int nextHighDenom = 0;
            switch (denom)
            {
                case 25:
                    nextHighDenom = 10;
                    break;
                case 10:
                    nextHighDenom = 5;
                    break;
                case 5:
                    nextHighDenom = 1;
                    break;
                case 1:
                    //Important: think 100, 1 if u want to make 100 cents with only 1 cents..there is only 1 way  1 * 100 = 100 cents
                    return 1;

            }

            int ways = 0;

            if (value > 0)
            {
                //based case is called fully reduced ...i * denom <= value
                for (int i = 0; i * denom <= value; i++)
                {
                    //imagine 100 using 25 
                    //Make 100 using 0 quater +  //this will call another recursion make  (100, nexhighdenom (10))
                    //make 100 using 1 quater + //this will call another recursion make  (75, nexhighdenom (10))
                    // make 100 using 2 quater + //this will call another recursion make  (50, nexhighdenom (10))
                    //make 100 using 3 quater //this will call another recursion make  (25, nexhighdenom (10))
                    //+ make 100 using 4 quater + //this will call another recursion make  (0, nexhighdenom (10))
                    ways += FindNoOfWaysToMakeChange(value - (i * denom), nextHighDenom);
                }
            }

            return ways;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.textBox8.Text);
            int[] denom = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox9.Text);
            //pass the highest denom available
            this.textBox7.Text = CountWays(denom, denom.Length -1, value).ToString();
        }

        public int CountWays(int[] denom, int denomindex, int value)
        {
            // If n is 0 then there is 1 solution (do not include any coin)
            if (value == 0)
            {
                return 1;
            }

            // If n is less than 0 then no solution exists
            if (value < 0)
            {
                return 0;
            }
            // If there are no coins and n is greater than 0, then no solution exist
            if (denomindex < 0 && value > 0)
            {
                return 0;
            }

            //important..for an index or denomination there are two possible ways
            //1) one ways is to include the denom and make the value from it
            //2) another way is to exclude the denom and still make the value from it

            //include the denom and subtract the denomvalue frm value 
            int ways_include = CountWays(denom, denomindex, value - denom[denomindex]);
            //we don't want to include this so skip this index and move to another to find the value with other denom
            int ways_exclude = CountWays(denom, denomindex - 1, value);

            return ways_include + ways_exclude;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.textBox8.Text);
            int[] denom = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox9.Text);
            //pass the highest denom available
            this.textBox7.Text = CountWaysDP(denom, value).ToString();
        }

        public int CountWaysDP(int[] denom, int value)
        {
            //length is +1 of index
            int[,] table = new int[ value + 1, denom.Length];


            // Fill the enteries for 0 value case (n = 0)
            for (int i = 0; i < denom.Length -1; i++)
            {
                // If n is 0 then there is 1 solution (do not include any coin)
                table[0,i] = 1;
            }

      
            //value as row and denom as col
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    //here we have two choice for coins

                    //includin the coin for the denom value
                    int way1 = (i - denom[j] >= 0) ? table[i - denom[j], j] : 0;
                    //excluding this coin for denom value
                     // If there are no coins and n is greater than 0, then no solution exist
                    int way2 = (j > 0) ? table[i, j - 1] : 0;

                    table[i, j] = way1 + way2;
                }
            }


             return table[value, denom.Length -1];


        }

    }
}
