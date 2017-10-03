using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Puzzles.DataStructures;
using Puzzles.DataStructures.Array;

namespace Puzzles
{
    public partial class NGU2017QuestionsPage1 : Form
    {
        public NGU2017QuestionsPage1()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            char[] input1 = AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox1.Text);

            this.textBox4.Text = FindSubSequenceOfLengthTwo(input1).ToString();

            this.textBox5.Text = FindSubSequenceOfLengthTwo(AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox2.Text)).ToString();


            this.textBox6.Text = FindSubSequenceOfLengthTwo(AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox3.Text)).ToString();

        }

        // The idea is to remove all the non-repeated characters from the string and check if the resultant string is palindrome or not. If the remaining string is palindrome then it is not repeated, else there is a repetition

        private bool FindSubSequenceOfLengthTwo(char[] array)
        {
            //Logic
            // Parse the input and Find the occurenace of each character in the given string
            // If any character occurenace is more than 3 then we know that we found subse of length 2 eg: AAAA, "BBBB"
            // If any character occureed only once then we know that character won't be in the subsequence. A susbsequence is pair of 2 sequence
            // Build a string with on

            Dictionary<char, int> OccuranceOfChar = new Dictionary<char, int>();

            for (int i = 0; i < array.Length; i++)
            {
                char c = array[i];

                if (OccuranceOfChar.ContainsKey(c))
                {
                    OccuranceOfChar[c] += 1;

                    // if the occurance of any character is more than 3 then we found the subsequence of length 2
                    if (OccuranceOfChar[c] > 3)
                    {
                        return true;
                    }
                }
                else
                {
                    OccuranceOfChar.Add(c, 1);
                }
            }

            // we will have the key and the occurance
            char[] subseqChars = new char[array.Length];
            int index = 0;


            for (int i = 0; i < array.Length; i++)
            {
                char c = array[i];
                
                // consider the char only for which the occurance is greater than 1
                if (OccuranceOfChar.ContainsKey(c) && OccuranceOfChar[c] > 1)
                {
                    subseqChars[index] = c;
                    index++;
                }
            }

            string subseqenceString = new string(subseqChars, 0, index);

            // Find whether the given string is palindrome or not
            bool isPalindromeResult = IsPalindrome(subseqenceString);

            // if it is a palindrom like  "A B C C B A" (here only cc one occurenace) or "A B C B A" then there won't be any sub sequence
            // All other combination excluding palindrom will have atleast pair of sub sequence of length 2

            return !isPalindromeResult;

        }

        private bool IsPalindrome(string input)
        {
            int low = 0;
            int high = input.Length - 1;

            while (low < high)
            {
                if (input[low] != input[high])
                {
                    return false;
                }

                low++;
                high--;
            }

           // we scanned and everything is equal

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string input = this.textBox12.Text;
            this.textBox9.Text = RemoveUnBalancedParenthesis(input);

            string input2 = this.textBox13.Text;
            this.textBox8.Text = RemoveUnBalancedParenthesis(input2);

            string input3 = this.textBox14.Text;
            this.textBox7.Text = RemoveUnBalancedParenthesis(input3);



        }

        private string RemoveUnBalancedParenthesis(string input)
        {
            Stack<int> openParenStack = new Stack<int>();
          
            HashSet<int> indexToBeRemoved = new HashSet<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '(')
                {
                    openParenStack.Push(i);
                }
                else if (c == ')')
                {
                    
                    if (openParenStack.Count == 0)
                    {
                        indexToBeRemoved.Add(i);
                    }
                    else
                    {
                        openParenStack.Pop();
                    }
                }
            }

            // scan the stack to find the unbalanced open parenthesis
            while (openParenStack.Count != 0)
            {
                indexToBeRemoved.Add(openParenStack.Pop());
            }

            char[] output = new char[input.Length - indexToBeRemoved.Count];
            int outputIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (!indexToBeRemoved.Contains(i))
                {
                    output[outputIndex] = input[i];
                    outputIndex++;
                }
            }

            return new string(output);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int matrixSize = Convert.ToInt32(this.textBox18.Text);

            NQueenProblem queens = new NQueenProblem(matrixSize);

            var positions = queens.FindPositionsForQueen();

            int[,] matrix = new int[matrixSize, matrixSize];

            foreach (var position in positions)
            {
                matrix[position.Row, position.Col] = 1;
            }

            // print the matrix
            string result = matrix.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[,] matrix = {     {1, 1, 0, 0, 0},
                                  {0, 1, 0, 0, 1},
                                  {1, 0, 0, 1, 1},
                                  {0, 0, 0, 0, 0},
                                  {1, 0, 1, 0, 1}
                               };

            int count = CountIslands(matrix);
            this.textBox10.Text = count.ToString();
        }

        // https://www.youtube.com/watch?v=R4Nh-EgWjyQ
        private int CountIslands(int[,] matrix)
        {
            // if we need to find max size of island
            int maxSize = Int32.MinValue;
            int islandCount = 0;
            bool[,] visitedMatrix = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 1 && visitedMatrix[row, col] == false)
                    {
                        islandCount++;

                        int size = GetIslandSize(matrix, row, col, visitedMatrix);

                        if (size > maxSize)
                        {
                            maxSize = size;
                        }
                    }
                }
            }

            return islandCount;
        }

        private int GetIslandSize(int[,] matrix, int row, int col, bool[,] visitedMatrix)
        {
            // check for outside bonds
            if (row < 0 || row == matrix.GetLength(0) ||
               col < 0 || col == matrix.GetLength(1))
            {
                return 0;
            }

            // check for visited and the matrix value
            if (matrix[row, col] == 0 || visitedMatrix[row, col])
            {
                //already visited
                return 0;
            }

            // for itself
            int size = 1;
            visitedMatrix[row, col] = true;

            // Go for all the 8 neightbours
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    //exclude current
                    if (i != row || j != col)
                    {
                        size += GetIslandSize(matrix, i, j, visitedMatrix);
                    }
                }
            }

            return size;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);

            MedianOfMedian med = new MedianOfMedian(input);
            int median = med.FindMiddleElement(input, 0, input.Length - 1);
            this.textBox15.Text = median.ToString();
        }
    }
}
