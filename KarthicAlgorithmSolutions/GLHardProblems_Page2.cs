using Puzzles.DataStructures.Array;
using System;
using System.Collections;
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
    public partial class GLHardProblems_Page2 : Form
    {
        public GLHardProblems_Page2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = AlgorithmHelper.ConvertCommaSeparetedStringToStringArray(this.textBox3.Text);

            string output = FindLongestWordMadeofotherwords(input);
            this.textBox1.Text = output;
        }

        //This function will return the longest word that can be formed with other words in the array
        private string FindLongestWordMadeofotherwords(string[] words)
        {
            //create a hash table with the words
            Hashtable ht = new Hashtable();
            foreach (string word in words)
            {
                if (!ht.ContainsKey(word))
                {
                    ht.Add(word, true);
                }
            }

            ht.Add(string.Empty, true);
            //To find the longest word..sort the string[] by length and start from last
            Array.Sort(words, new ArrayLengthComparer());
            //array will be sorted by length

            for (int i = words.Length - 1; i >= 0; i--)
            {
               //check whether words can be formed by the given word
                string word = words[i];
                //The sec parm IsOrininalword is used to ignore the real word..eg: dogwalker..dogwalker will be there in ht..we need to find the other words that can form dogwalker so ignore the actual word
                if(CanBuildWord(word, true, ht))
                {
                    return word;
                }
            }


            return "Cannot be found";

        }


        private bool CanBuildWord(string word, bool isOriginalword, Hashtable ht)
        {
            //base case
            if (ht.ContainsKey(word) && !isOriginalword)
            {
                //we used dynamic programming to cache the result..so ht value might be true or false
                return (bool)ht[word];
            }

            //split the string into all possible combination..all possible prefix starting from smallest
            //eg: dogwalker
            for (int i = 1; i < word.Length; i++)
            {
                string left = word.Substring(0, i);
                //update: 6/13/2015.. Don't make the left part to be the full word bcoz it may exist in dictionary which contains empty string
                //The whole word is alredy check at the beginning so not required here

                //testing
                if (i == 11)
                {
                    int test = -1;
                }
                string right = word.Substring(i);
                //important check whether left has false in ht..since we cache the result..somewhere in the recursion we would have found that word is false
                if(ht.ContainsKey(left) &&  (bool)ht[left] == true &&  CanBuildWord(right, false, ht))
                {
                    return true;
                }
            }

            //cache the result..if we repeatedly want to find a same word..we have to compute it once
            if(ht.ContainsKey(word))
            {
                ht[word] = false;

            }
            else
            {
                ht.Add(word, false);
            }


            return false;

        }

        public class ArrayLengthComparer : IComparer<string>
        {
            int IComparer<string>.Compare(string x, string y)
            {
                return x.Length.CompareTo(y.Length);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string word1 = this.textBox4.Text;
            string word2 = this.textBox5.Text;
            //we are given a dictionary words
            Hashtable ht = new Hashtable();
            ht.Add("LAMP", true);
            ht.Add("LIMP", true);
            ht.Add("LIME", true);
            ht.Add("LIKE", true);
            ht.Add("DAMP", true);
            ht.Add("CAT", true);
            ht.Add("BAT", true);
            ht.Add("BET", true);
            ht.Add("BOT", true);
            ht.Add("BOG", true);
            ht.Add("DOG", true);

            ht.Add("RAMP", true); //THIS won't work
            //cat -> bat -> bet -> bot -> bog -> dog
            string output = TransformOneWordToAnother(word1.ToUpper(), word2.ToUpper(), ht);

            this.textBox2.Text = output;

        }


        private string TransformOneWordToAnother(string startword, string endword, Hashtable ht)
        {
            Queue<string> transformationlist = new Queue<string>();
            //we can travel backwords with this ht..key is newword and value is oldword
            Hashtable backtrackmap = new Hashtable();
            List<string> visitedset = new List<string>();
            List<string> possiblewordslist = new List<string>();

            //add given word to queue
            transformationlist.Enqueue(startword);
            visitedset.Add(startword);

            while (transformationlist.Count != 0)
            {
                string word = transformationlist.Dequeue();
                //update: we can filter the getall to have only dictionary words
                possiblewordslist = GetAllPossibleWordsWithOneLetterChange(word);

                foreach (string possibleword in possiblewordslist)
                {

                    if (possibleword.ToUpper() == endword.ToUpper())
                    {
                        backtrackmap.Add(possibleword, word);
                        //
                        string output = GetBackTrackMapWord(backtrackmap, endword);
                        return output;
                    }

                    //check the dictionary
                    if (ht.ContainsKey(possibleword))
                    {
                        if (!visitedset.Contains(possibleword))
                        {
                            transformationlist.Enqueue(possibleword);
                            //mark this word as visited so we don't play with the words we already played
                            visitedset.Add(possibleword);
                            //The string "word" has been changed to string "possibleword"
                            //the key should be the new word and the value should be the word before transformation
                            backtrackmap.Add(possibleword, word);

                            //not sure whehther we need to break the loop..
                            //how about have DAMP, LAMP, RAMP in the list RAMP is one leter change after LAMP and might lead to endword

                           // break; //break the for loop and go for the next word in the queue
                            //update: fuck don't break the loop then it makes the whole bfs not work..the shortest path may be from another word
                        }

                    }
                  

                }


            }

            return null;


        }

        //eg DAMP   { AAMP, BAMP, CAMP, EAMP.....ZAMP, DBMP, DCMP...DZMP, DAAP...}
        private List<String> GetAllPossibleWordsWithOneLetterChange(string word)
        {
            List<String> words = new List<string>();
            char[] letters = word.ToCharArray();
            //loop through every character in the input
            for (int i = 0; i < letters.Length; i++)
            {

                //test
                int value = i;
                //for every character get all the possible alphabet excluding the actual letter
                char original = letters[i];

                for (char c = 'A'; c <= 'Z'; c++)
                {
                    if (c != original)
                    {
                        letters[i] = c;
                        words.Add(new string(letters));

                    }
                }

                letters[i] = original;

            }

            return words;

        }

        private string GetBackTrackMapWord(Hashtable backtrackmap, string lastword)
        {
            LinkedList<string> list = new LinkedList<string>();
            list.AddFirst(lastword);
            string word = lastword;
            while (word != null && word != string.Empty)
            {
                word = (string) backtrackmap[word];
                list.AddFirst(word);
            }

            return ConvertLinkedListTostring(list);
        }

        private string ConvertLinkedListTostring(LinkedList<string> list)
        {
            StringBuilder sb = new StringBuilder();

            LinkedListNode<string> node = list.First;

            while (node != null)
            {
                sb.Append(node.Value).Append(",");
                node = node.Next;
                
            }


            return sb.ToString();
           

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Given a nxn square matrix
            //construct a 5*5 square matrix
            int[,] matrix = new int[,] {{0,1,1,0,1},
                                        {1,1,0,1,0}, 
                                        {0,1,1,1,0},
                                        {1,1,1,1,0}, 
                                        {1,1,1,1,1}};

            SubSquare result = FindMaxSubSquare(matrix);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
                sb.Append("Row vaue: ").Append(result.Row).Append(" , ");
                sb.Append("Col vaue: ").Append(result.Col).Append(" , ");
                sb.Append("Size: ").Append(result.Size);


            }
            else
            {
                sb.Append("No squares found");
            }

            this.textBox8.Text = sb.ToString();
                       
        }

        public class SubSquare
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int Size { get; set; }

            public SubSquare(int row, int col, int size)
            {
                this.Row = row;
                this.Col = col;
                this.Size = size;
            }

        }

        public enum SquareBorder
        {
           White = 0,
           Black = 1
        }


        public SubSquare FindMaxSubSquare(int[,] matrix)
        {
            //Logic is go from max size to 0..we need to find the max subsquare
            //For a given 5 * 5 matrix, max square is of size 5 and min is 1*1
            //size 5,4,3,2,1
            for (int i = matrix.GetLength(0); i >= 1; i--)
            {
                SubSquare result = FindSquareBySize(matrix, i);
                if (result != null)
                {
                    return result;
                }

            }

            return null;
        }

        //This function searches for all the possible square in the given matrix for the required size and returns the first found result
        public SubSquare FindSquareBySize(int[,] matrix, int size)
        {

            //For a given square of size n, there are (n - sz + 1) squares of length sz
            //that is for n = 5 there will be only 1 square..for n = 4 there will be two on first row..two on second row.. 
            //count is no of possible square for the given size on a particualr row or column..count is same for row and column bcoz  square row and col length is same
            int count = matrix.GetLength(0) - size + 1;
            //size =5 count = 1
            //size =4 count = 2

            //loop through rows
            for (int i = 0; i < count; i++)
            {

                //if (size == 3 && i == 2)
                //{
                //    string test = "";
                //}
                //loop through colums
                for (int j = 0; j < count; j++)
                {
               
                    if(IsSquareWithBorders(matrix, i, j, size, SquareBorder.Black))
                    {
                        return new SubSquare(i, j, size);
                    }
                }
            }

            return null;

        }


        public bool IsSquareWithBorders(int[,] matrix, int row, int col, int size, SquareBorder bordervalue)
        {

            //Important: Formula for top, bottom, left and right

            //check the top and bottom border of the square in the matrix for the given size starting at given row and colm
            //for size = 5, j = 0, 1,2,3,4 
            //matrix is xero based index
            // 00  01 02 03 04
            // 10  11 12 13 14
            // 20  21 22 23 24
            // 30  31 32 33 34 
            // 40  41 42 43 44
            //row and col will tell where to start the iteration
            for (int j = 0; j < size; j++)
            {
                //check top border
                if (matrix[row, col + j] != (int)bordervalue)
                {
                    return false;
                }

                //check bottom border

                if (matrix[(size - 1) + row, col + j] != (int)bordervalue)
                {
                    return false;
                }

            }

            //here k is from 1 to size -1 bcoz the top and bottom border already covered the first and last of left and right border
            for (int k = 1; k < size - 1; k++)
            {
                //check left border
                if(matrix[k + row,col] != (int) bordervalue)
                {

                    return false;
                }

                //check right border
                if (matrix[k + row, (size - 1) + col] != (int)bordervalue)
                {
                    return false;
                }

            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Given a nxn square matrix
            //construct a 5*5 square matrix
            //This sln is an continuation of previous sln where isSquare() has special logic to check square that has specified border value
            int[,] matrix = new int[,] {{0,1,1,0,1},
                                        {1,1,0,1,0}, 
                                        {0,1,1,1,0},
                                        {1,1,1,1,0}, 
                                        {1,1,1,1,1}};

            //update: we can do this same for rectangel..just take care for max area and then count formula for both row and colum

            //int[,] matrix = new int[,] {{0,1,0},
            //                            {1,1,0}, 
            //                            {1,1,0}};


            SubSquare result = FindMaxSubSquare2(matrix);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
                sb.Append("Row vaue: ").Append(result.Row).Append(" , ");
                sb.Append("Col vaue: ").Append(result.Col).Append(" , ");
                sb.Append("Size: ").Append(result.Size);


            }
            else
            {
                sb.Append("No squares found");
            }

            this.textBox8.Text = sb.ToString();

        }



        public SubSquare FindMaxSubSquare2(int[,] matrix)
        {
            //Logic is go from max size to 0..we need to find the max subsquare
            //For a given 5 * 5 matrix, max square is of size 5 and min is 1*1
            //size 5,4,3,2,1

            //process the matrix
            SquareCell[,] processedmatrix = ProcessMatrixToSquareCells(matrix, SquareBorder.Black);

            for ( int i = matrix.GetLength(0); i >= 1; i--)
            {
                SubSquare result = FindSquareBySize2(processedmatrix, i);
                if (result != null)
                {
                    return result;
                }

            }

            return null;
        }

        //This function searches for all the possible square in the given matrix for the required size and returns the first found result
        public SubSquare FindSquareBySize2(SquareCell[,] processedcells, int size)
        {

            //For a given square of size n, there are (n - sz + 1) squares of length sz
            //that is for n = 5 there will be only 1 square..for n = 4 there will be two on first row..two on second row.. 
            //count is no of possible square for the given size on a particualr row or column..count is same for row and column bcoz  square row and col length is same
            int count = processedcells.GetLength(0) - size + 1;
            //size =5 count = 1
            //size =4 count = 2

            //loop through rows
            for (int i = 0; i < count; i++)
            {

                //loop through colums
                for (int j = 0; j < count; j++)
                {
                    if (IsSquareOptimized(processedcells, i, j, size))
                    {
                        return new SubSquare(i, j, size);
                    }
                }
            }

            return null;

        }


        public class SquareCell
        {
            public int RightCount { get; set; }
            public int BottomCount{ get; set; }

            public SquareCell(int rightcount, int bottomcount)
            {
                this.RightCount = rightcount;
                this.BottomCount = bottomcount;
            }


        }

        //Logic: If we want to check for black or white cell on the borders
        //When we do the regular iteration of the matrix, we need to know no of  black cells on the right and bottom
        //so that we can decide whether that particular cell for the given size has a square with black or white borders
        //This optimized issquare will perform in o(1)
        public bool IsSquareOptimized(SquareCell[,] processedmatrix, int row, int col, int size)
        {
            //we need three celss topleft, topright and bottomleft
            SquareCell topleft = processedmatrix[row, col];

            SquareCell topright = processedmatrix[row, (size - 1) + col];

            SquareCell bottomleft = processedmatrix[(size - 1) + row, col];

            //check top edge
            if (topleft.RightCount < size)
            {
                return false;
            }

            //check left edge
            if (topleft.BottomCount < size)
            {
                return false;
            }

            //check bottom edge
            if (bottomleft.RightCount < size)
            {
                return false;
            }

            //check right edge
            if (topright.BottomCount < size)
            {
                return false;
            }

            //the code will come here if all the edges has equal or greater no of black cells compared to size
            return true;
        }


        //conver the matrix into squarecell [,] where squarecell[i,j] represents a cell and
        //i has count of no of black cells consecutively (important) on the right
        //j has count of no of black cells consecutively (important) on the bottom
        public SquareCell[,] ProcessMatrixToSquareCells(int[,] matrix, SquareBorder bordervalue)
        {
            SquareCell[,] cells = new SquareCell[matrix.GetLength(0), matrix.GetLength(1)];

            //loop through row from last to first  ie bottom to top
            for (int row = matrix.GetLength(0) - 1; row >= 0; row--)
            {

                //loop through col from last to first ie right to left
                for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
                {
                    int rightcount = 0;
                    int bottomcount = 0;
                    //only process the count if the cell has the required border value
                    //If the cell doesn't have the required border value then reset the count as 0 bcoz we are looking for consecutive border value
                    if (matrix[row, col] == (int)bordervalue)
                    {
                        //increment the value if cell has the bordervalue
                        rightcount++;
                        bottomcount++;

                        //find the no of bordervalue on the right cell of the current cell
                        if (col + 1 < matrix.GetLength(1))
                        {
                            SquareCell previouscell = cells[row, col + 1];
                            rightcount += previouscell.RightCount;
                        }

                        //find the no of bordevalue on the bottom cell of the current cell
                        if (row + 1 < matrix.GetLength(0))
                        {
                            SquareCell bottomcell = cells[row + 1, col];
                            bottomcount += bottomcell.BottomCount;
                        }
                    }

                    cells[row, col] = new SquareCell(rightcount, bottomcount);
                }

            }

            return cells;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //construct a square
            int[,] matrix = new int[,] { {2,-1,-4,-20},
                                         {-3,4,2,1},
                                         {8,10,1,3},
                                         {-1,1,7,-6}};


            SubSquareSum result = FindSubSquareWithMaxSum(matrix);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
                sb.Append("Row vaue: ").Append(result.Row).Append(" , ");
                sb.Append("Col vaue: ").Append(result.Col).Append(" , ");
                sb.Append("Size: ").Append(result.Size).Append(" , ");
                sb.Append("Sum :  ").Append(result.Sum);


            }
            else
            {
                sb.Append("No squares found");
            }

            this.textBox6.Text = sb.ToString();

           
        }


        public SubSquareSum FindSubSquareWithMaxSum(int[,] matrix)
        {

            SubSquareSum max = new SubSquareSum(-1, -1, -1, Int32.MinValue);
            //To get all possible square go with the window approach n, n-1,,n-2 to 1
            //n = 4  4,3,2,1
            for (int i = matrix.GetLength(0); i >= 1; i--)
            {
                SubSquareSum resultbysize = FindAllPossibleSubSquareBySize(matrix, i);
                if (resultbysize.Sum > max.Sum)
                {
                    max = resultbysize;
                }
            }

            return (max.Sum == Int32.MinValue) ? null : max;    
        }

        //This methods finds all the possible subsquares by size basis and compute the sum for each subsuare and return the max
        public SubSquareSum FindAllPossibleSubSquareBySize(int[,] matrix, int size)
        {
   
            SubSquareSum max = new SubSquareSum(-1, -1, -1, Int32.MinValue);
         
            //For a given square of size n, there are (n - sz + 1) squares of length sz
            //that is for n = 5 there will be only 1 square..for n = 4 there will be two on first row..two on second row.. 
            //count is no of possible square for the given size on a particualr row or column..count is same for row and column bcoz  square row and col length is same
            int count = matrix.GetLength(0) - size + 1;
            //size =5 count = 1
            //size =4 count = 2

            //loop through rows
            for (int i = 0; i < count; i++)
            {

                //loop through colums
                for (int j = 0; j < count; j++)
                {
                    int sum = FindSumofCellBySize(matrix, i, j, size);
                    if (sum > max.Sum)
                    {
                        max = new SubSquareSum(i, j, size, sum);

                    }
                }
            }

            return (max.Sum == Int32.MinValue) ? null : max;          
        }




        public int FindSumofCellBySize(int[,] matrix, int row, int col, int size)
        {
            int sum = 0;
            //we have to iterate through every cell for the size
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                   sum += matrix[row + i, col + j];

                }
            }

            return sum;
        }

        public class SubSquareSum
        {

            public int Row { get; set; }
            public int Col { get; set; }
            public int Size { get; set; }
            public int Sum { get; set; }

            public SubSquareSum(int row, int col, int size, int sum)
            {
                this.Row = row;
                this.Col = col;
                this.Size = size;
                this.Sum = sum;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=SAiTh4F401k
            //construct a square
            //update 5/13/2015 watch the video before understanding the logic
            int[,] matrix = new int[,] { {2,-1,-4,-20},
                                         {-3,4,2,1},
                                         {8,10,1,3},
                                         {-1,1,7,-6}};

            //test
            //int[,] matrix = new int[,] { {1,1,1,1},
            //                             {1,1,1,1},
            //                             {1,1,1,1},
            //                             {1,1,1,1}};
            SubSquareSum result = FindSubSquareWithMaxSum2(matrix);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
                sb.Append("Row vaue: ").Append(result.Row).Append(" , ");
                sb.Append("Col vaue: ").Append(result.Col).Append(" , ");
                sb.Append("Size: ").Append(result.Size).Append(" , ");
                sb.Append("Sum :  ").Append(result.Sum);


            }
            else
            {
                sb.Append("No squares found");
            }

            this.textBox6.Text = sb.ToString();

           

        }

        public SubSquareSum FindSubSquareWithMaxSum2(int[,] matrix)
        {
            //int[,] summatrix = ProcessMatrixToHoldSumValue(matrix);
            //Here the summatrix will be one size larger than matrix
            int[,] summatrix = ProcessMatrixToHoldSumValueMethod2(matrix);

            SubSquareSum max = new SubSquareSum(-1, -1, -1, Int32.MinValue);
            //To get all possible square go with the window approach n, n-1,,n-2 to 1
            //n = 4  4,3,2,1
            for (int i = matrix.GetLength(0); i >= 1; i--)
            {
                SubSquareSum resultbysize = FindAllPossibleSubSquareBySizeOptimized(matrix, i, summatrix);
                if (resultbysize.Sum > max.Sum)
                {
                    max = resultbysize;
                }
            }

            return (max.Sum == Int32.MinValue) ? null : max;
        }



        //This methods finds all the possible subsquares by size basis and compute the sum for each subsuare and return the max
        //use the computed matrix and get the sum
        public SubSquareSum FindAllPossibleSubSquareBySizeOptimized(int[,] matrix, int size, int[,] summatrix)
        {

            SubSquareSum max = new SubSquareSum(-1, -1, -1, Int32.MinValue);
     
            //For a given square of size n, there are (n - sz + 1) squares of length sz
            //that is for n = 5 there will be only 1 square..for n = 4 there will be two on first row..two on second row.. 
            //count is no of possible square for the given size on a particualr row or column..count is same for row and column bcoz  square row and col length is same
            int count = matrix.GetLength(0) - size + 1;
            //size =5 count = 1
            //size =4 count = 2

            //loop through rows
            for (int i = 0; i < count; i++)
            {

                //loop through colums
                for (int j = 0; j < count; j++)
                {

                    //we have starting point i, j
                    //get the end point from size
                    int row1 = i;
                    int col1 = j;

                    int row2 = (i + size - 1);
                    int col2 = (j + size - 1);

                    //we have starting and ending point.find sum from the optimized processed matrix
                    int sum = ComputeSumMethod2(summatrix, row1, row2, col1, col2);
                    if (sum > max.Sum)
                    {
                        max = new SubSquareSum(row1, col1, size, sum);

                    }
                }
            }

            return (max.Sum == Int32.MinValue) ? null : max;
        }





        public SubSquareSum FindSubSquareMaxWithDynamicProgramming(int[,] original)
        {

            int max = Int32.MinValue;
            //SubSquareSum maxsubsquare = new SubSquareSum(-1, -1, -1, Int32.MinValue);
            SubSquareSum maxsubsquare = null;
            int rowcount = original.GetLength(0);
            int colcount = original.GetLength(1);
            int[,] summatrix = ProcessMatrixToHoldSumValue(original);

            //for (int row1 = 0; row1 < rowcount; row1++)
            //{
            //    for (int row2 = row1; row2 < rowcount; row2++)
            //    {
            //        for (int col1 = 0; col1 < colcount; col1++)
            //        {
            //            for (int col2 = col1; col2 < colcount; col2++)
            //            {
            //                int sum = ComputeSum(summatrix, row1, row2, col1, col2);
            //                if(max < sum )
            //                {
            //                    maxsubsquare = new SubSquareSum(row1, col1, -1, sum);

            //                }
                           
            //                //max = Math.Max(max, ComputeSum(summatrix, row1, row2, col1, col2));
            //            }
            //        }
            //    }
            //}


            int sum = ComputeSum(summatrix, 1, 3, 0, 2);
            if (max < sum)
            {
                //maxsubsquare = new SubSquareSum(row1, col1, -1, sum);

            }

            return maxsubsquare;

        }


        //Logic:
        //1) Process the input matrix and store the values in another matix based on this formula
        // val(x,y) = val(x-1, y) + val (x, y-1) - val (x-1, y-1) + MatrixValue(x, y)
        //that is 
        //           y0.........*(x1,.........*(x2,y0)
        //          
        //           y1.........*(x1,y1)......*(x2,y1)
        //
        //           y2.........*(x1,y2)......*(x2,y2)

        //In the above square value/area of x2, y2 is
        //Value of (x2, y2) from (0,0) or area of x2, y2 = val(x1, y2) + val (x2, y1) - val (x1, y1) + MatrixValue(x2, y2)
        //update: helper Think you are building the matrix with the ref to (0,0). each cell has the area from cell to (0,0)
        public int[,] ProcessMatrixToHoldSumValue(int[,] orignal)
        {

            int[,] computedmatrix = new int[orignal.GetLength(0), orignal.GetLength(1)];

            for (int i = 0; i < orignal.GetLength(0); i++)
            {
                for (int j = 0; j < orignal.GetLength(1); j++)
                {
                    //first cell
                    if (i == 0 && j == 0)
                    {
                        computedmatrix[i, j] = orignal[i, j];
                    }
                    //first row
                    else if (i == 0)
                    {
                        computedmatrix[i, j] = computedmatrix[i, j - 1] + orignal[i, j];

                    }
                    //first column
                    else if (j == 0)
                    {
                        computedmatrix[i, j] = computedmatrix[i - 1, j] + orignal[i, j];
                    }
                    else
                    {
                        computedmatrix[i, j] = computedmatrix[i - 1, j] + computedmatrix[i, j - 1] - computedmatrix[i - 1, j - 1] + orignal[i, j];
                    }
                }
            }

            return computedmatrix;
        }

        //We defind the computermatrix to a size of original matrix + 1
        //eg: 00 01 02 03
        //    10 11 12 13
        //    20 21 22 23
        //    30 31 32 33

        //Imagine we need to calculate the area of the cell 22 from 00 we need values of the cell 21 and 12 and 11
        //area of (2,2) from (0,0) is  Area of (2,1) + Area of (1,2) - Area of (1,1) and value in the cell (2,2)
        public int[,] ProcessMatrixToHoldSumValueMethod2(int[,] orignal)
        {

            int[,] computedmatrix = new int[orignal.GetLength(0) + 1, orignal.GetLength(1)+ 1];

            for (int i = 0; i < computedmatrix.GetLength(0); i++)
            {
                for (int j = 0; j < computedmatrix.GetLength(1); j++)
                {
                    //if it first row or first col, put o
                    if (i == 0 || j == 0)
                    {
                        computedmatrix[i, j] = 0;
                    }
                    else
                    {
                        //original is 1 size smaller than computer
                        computedmatrix[i, j] = computedmatrix[i - 1, j] + computedmatrix[i, j - 1] - computedmatrix[i - 1, j - 1] + orignal[i -1, j -1];
                    }
                }
            }

            return computedmatrix;
        }


        public int ComputeSum(int[,] summatrix, int i1, int i2, int j1, int j2)
        {
            if (i1 == 0 && j1 == 0)
            {
                return summatrix[i2, j2];
            }
            //first row
            else if (i1 == 0)
            {
                return  summatrix[i2, j2] -summatrix[i2, j1 - 1] ;

            }
            //first column
            else if (j1 == 0)
            {
                return summatrix[i2, j2] - summatrix[i1 - 1, j2];
            }
            else
            {
                return summatrix[i2, j2] - summatrix[i2, j1 - 1] - summatrix[i1 - 1, j2] + summatrix[i1 - 1, j1 - 1];
            }
        }

        //Here the summatrix is greater than orignal by 1
        public int ComputeSumMethod2(int[,] summatrix, int row1, int row2, int col1, int col2)
        {
            //we are calculating the sum from start row, start col, end row, end col
            //Imagine
            //eg: 00 01 02 03
            //    10 11 12 13
            //    20 21 22 23
            //    30 31 32 33

            //area or sum of the matrix of size 2 from (1,1)
            //starting point (1,1) ending point (2,2)
            //we need (2,0), (0,0), (0,2) and (2,2)
            //Area/sum of (1,1) to (2,2) is  (2,2) - (2,0) + (0,0) - (0,2)
             
            //summatrix has size greater than 1
            row1 = row1 + 1;
            row2 = row2 + 1;
            col1 = col1 + 1;
            col2 = col2 + 1;

            int sum = summatrix[row2, col2] - summatrix[row2, col1 -1] + summatrix[row1 - 1, col1 - 1] - summatrix[row1 -1, col2];

            return sum;
   
        }


        private void button8_Click(object sender, EventArgs e)
        {


            //http://www.geeksforgeeks.org/dynamic-programming-set-27-max-sum-rectangle-in-a-2d-matrix/

            //and also in gayle book

            //construct a square
            int[,] matrix = new int[,] { {2,-1,-4,-20},
                                         {-3,4,2,1},
                                         {8,10,1,3},
                                         {-1,1,7,-6}};


            var result = SubArraySum.MaxSubMatrixWithIndex(matrix);
            //var result = SubArraySum.MaxSubMatrix(matrix);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
                sb.Append("Row: ").Append(result.RowStart).Append(" , ");
                sb.Append("Col: ").Append(result.ColStart).Append(" , ");
                sb.Append("Size: ").Append(result.RowEnd - result.RowStart + 1).Append(" , ");
                sb.Append("Sum :  ").Append(result.Sum);


            }
            else
            {
                sb.Append("No squares found");
            }

            this.textBox6.Text = sb.ToString();



        }



    }
}
