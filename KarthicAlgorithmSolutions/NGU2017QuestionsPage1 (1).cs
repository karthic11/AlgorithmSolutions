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

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }

    class Solution
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int checkWinner(List<List<string>> codeList,
                               List<string> shoppingCart)
        {
            // WRITE YOUR CODE HERE
            // Logic
            // I will build a suffix array with the list of fruits from the shopping cart
            // And then we can find the pattern of the any fruits combination in the given suffix array
            // if all the codelist pattern exists in the suffix array then we return as checkWinner

            // Build the suffix array for the shopping list
            FruitsSuffixArray shoppingSourceList = new FruitsSuffixArray(shoppingCart);

            foreach (List<string> code in codeList)
            { 
                // check whether each code exists in the shopping list
                StringBuilder codePattern = new StringBuilder();
                foreach (string fruit in code)
                {
                    codePattern.Append(fruit);
                }

                bool patternExsists = shoppingSourceList.SearchPattern(codePattern.ToString(), shoppingSourceList.SourceString, shoppingSourceList.SuffixArray);

                // if the fruit code pattern does not exists
                if (!patternExsists)
                {
                    return 0;
                }
            }

            // we looped through every code list and all the pattern in the code exists in the shopping cart so return true as winner
            return 1;
        }
        // METHOD SIGNATURE ENDS

        public class FruitsSuffixArray
        {

            public int[] SuffixArray = null;
            public int[] LCPArray = null;
            public string SourceString { get; set; }
            public string LongestPanlindrome = string.Empty;

            public FruitsSuffixArray(List<string> fruits)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string fruit in fruits)
                {
                    sb.Append(fruit);
                }
                this.SourceString = sb.ToString();

                SuffixArray = new int[this.SourceString.Length];
                LCPArray = new int[this.SourceString.Length];
                SuffixArray = BuildSuffixArray(this.SourceString);
                LCPArray = BuildLCPArray(SuffixArray, this.SourceString);
            }

            private int[] BuildSuffixArray(string s)
            {
                int[] suffixarray = Enumerable.Range(0, s.Length).ToArray();
                System.Array.Sort(suffixarray, (x, y) => string.Compare(s, x, s, y, s.Length));
                return suffixarray;
            }


            private int[] BuildLCPArray(int[] suffixarray, string source)
            {
                int[] lcp = new int[suffixarray.Length];
                //lcp[i] : longest common prefix between sa[i] and sa[i+1]
                //so last index will have 0 or the first will have 0
                for (int i = 1; i < suffixarray.Length; i++)
                {
                    string result = FindLongestCommonPrefix(source.Substring(suffixarray[i - 1]), source.Substring(suffixarray[i]));
                    lcp[i] = result.Length;
                }

                //this.LCPArray = lcp;
                return lcp;
            }

            //This function returns the longest prefix in the secondstring
            //First it will look for whole prefix..it not decrement the prefix
            public string FindLongestCommonPrefix(string input1, string input2)
            {
                StringBuilder sb = new StringBuilder();
                //find the smallers length
                int length = Math.Min(input1.Length, input2.Length);

                for (int i = 0; i < length; i++)
                {
                    if (input1[i] == input2[i])
                    {
                        sb.Append(input2[i]);
                    }
                    else
                    {
                        break;
                    }

                }

                return sb.ToString();
            }

            public bool SearchPattern(string pattern, string source, int[] suffixarray)
            {
                List<string> suffixes = new List<string>();
                //we will find the index of the suffixarray that has the pattern
                int index = BinarySearchSortedArray(suffixarray, 0, suffixarray.Length - 1, pattern, source);
                //here the logic is different from regular binary search
                //we might have the same patter above and below the index like if you are looking for a in array { a, ana , anana }
                if (index != -1)
                {

                    int startposition = index;
                    int endposition = index;
                    int current = index - 1;
                    //get start position
                    while (current >= 0)
                    {
                        int suffixarrraycurrentvalue = suffixarray[current];
                        int result = String.Compare(pattern, 0, source, suffixarrraycurrentvalue, pattern.Length);
                        if (result == 0)
                        {
                            //This mean the current index has also the pattern
                            startposition = current;
                            current = current - 1;
                        }
                        else
                        {
                            break;//important break after the first search..only continue if you find the pattern
                        }
                    }

                    //get end position
                    current = index + 1;
                    while (current < suffixarray.Length)
                    {
                        int suffixarrraycurrentvalue = suffixarray[current];
                        int result = String.Compare(pattern, 0, source, suffixarrraycurrentvalue, pattern.Length);
                        if (result == 0)
                        {
                            //This mean the current index has also the pattern
                            endposition = current;
                            current = current + 1;
                        }
                        else
                        {
                            break;//important break after the first search..only continue if you find the pattern
                        }
                    }

                    //start position will be the first occurance
                    //end position will be the last occurance 
                    //count no of items between first and last
                    //rememeber we can also get all the suffixes too
                    int count = endposition - startposition + 1;

                    for (int i = startposition; i <= endposition; i++)
                    {
                        suffixes.Add(source.Substring(SuffixArray[i]));

                    }
                }

                return suffixes.Count > 0 ? true : false;

            }

            //This method search the array for the key and return the index of the key..if not found returns -1
            public int BinarySearchSortedArray(int[] suffixarray, int low, int high, string pattern, string originalstring)
            {
                //error handling
                if (suffixarray.Length == 0)
                {
                    //throw error
                }

                while (low <= high)
                {
                    int middle = (low + high) / 2;
                    int sourcestartindex = suffixarray[middle];
                    int compareresult = String.Compare(pattern, 0, originalstring, sourcestartindex, pattern.Length);

                    if (compareresult == 0)
                    {
                        return middle; //index of middle element
                    }
                    else if (compareresult < 0)
                    {
                        //search the first half of the array
                        //exclude the middle element
                        high = middle - 1;
                    }
                    else
                    {
                        low = middle + 1;
                    }
                }

                //code come means key not found
                return -1;
            }


            private int CompareTo(string pattern, string source, int sourcestartindex)
            {
                // Comparison takes into account maximum length(w) 
                // characters. For example, strings "ab" and "abc" 
                // are thus considered equal.
                return String.Compare(pattern, 0, source, sourcestartindex, pattern.Length);
            }
        }
    }


    //public class Solution
    //{
    //    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
    //    // RETURN AN EMPTY LIST IF NO SIMILAR MOVIE TO THE GIVEN MOVIE IS FOUND
    //    public static List<Movie> getMovieRecommendations(Movie movie, int N)
    //    {
    //       // Logic
    //       // We can use either graph dfs or bfs and visit all the related movies
    //       // while visiting each related movie, store the movie to min-heap ordered by rating of the movie
    //       // when the min heap reached the buffer of requested movie, pop the peek which has min rating and add the new high rated movie
    //       // I'm using BFS
    //        Queue<Movie> myqueue = new Queue<Movie>();
    //        HashSet<Movie> visitedMovies = new HashSet<Movie>();
    //        MinHeap<Movie> recommendedMovieList = new MinHeap<Movie>(0, new MovieRatingComparer());

    //        //add root to the queue
    //        myqueue.Enqueue(movie);
    //        visitedMovies.Add(movie);

    //        //after done with the root
    //        while (myqueue.Count != 0)
    //        {
    //            //eject from queue
    //            Movie parentMovie = myqueue.Dequeue();

    //            //loop through the related movies
    //            foreach (Movie relatedMovie in parentMovie.getSimilarMovies())
    //            {
    //                if (!visitedMovies.Contains(relatedMovie))
    //                {
    //                    // Add the movie to the rating list
    //                    if (recommendedMovieList.Size() == N && relatedMovie.Rating > recommendedMovieList.Peek().Rating)
    //                    { 
    //                        // pop the least rated movie
    //                        recommendedMovieList.PopRoot(); 
    //                    }

    //                    recommendedMovieList.Insert(relatedMovie);

    //                    //we have visited only the neighbor not the neighbor adjacent nodes/neighbor so add it to the queue and it does based on the queue priority
    //                    myqueue.Enqueue(relatedMovie);
    //                    visitedMovies.Add(relatedMovie);
    //                }
    //            }
    //        }

    //        // after visited all the movie, the minheap will have the recommened list of movie
    //        return recommendedMovieList.GetItems();
    //    }
    //    // METHOD SIGNATURE ENDS

    //    public class MovieRatingComparer : IComparer<Movie>
    //    {
    //        int IComparer<Movie>.Compare(Movie x, Movie y)
    //        {
    //            return x.getRating().CompareTo(y.getRating());
    //        }
    //    }

    //    public class Heap<T>
    //    {
    //        private readonly IList<T> _list;
    //        private readonly IComparer<T> _comparer;
    //        public IComparer<T> _wordcomparer;

    //        public Heap(IList<T> list, int count, IComparer<T> comparer)
    //        {
    //            _comparer = comparer;
    //            _list = list;
    //            //Important: we use count to keep track of removed value..don't use list.count in the code
    //            Count = count;
    //            Heapify();
    //        }

    //        public int Count { get; private set; }

    //        public T PopRoot()
    //        {
    //            if (Count == 0) throw new InvalidOperationException("Empty heap.");
    //            //Get the value from the first element
    //            var root = _list[0];

    //            //swap the removed element (root) with the last element in the tree
    //            SwapCells(0, Count - 1);

    //            //note: we are not removing the item..we just decrement the count variable
    //            Count--;

    //            HeapDown(0);
    //            return root;
    //        }

    //        public T PeekRoot()
    //        {
    //            if (Count == 0) throw new InvalidOperationException("Empty heap.");
    //            return _list[0];
    //        }

    //        //Insert element into the heap
    //        //Logic
    //        //Add the element to the bottom level of the heap.
    //        //Compare the added element with its parent; if they are in the correct order, stop.
    //        //If not, swap the element with its parent and return to the previous step.
    //        public void Insert(T e)
    //        {
    //            if (Count >= _list.Count)

    //                _list.Add(e);
    //            else
    //                //overwrite last element
    //                _list[Count] = e;

    //            Count++;

    //            HeapUp(Count - 1);
    //        }

    //        private void Heapify()
    //        {
    //            for (int i = Parent(Count - 1); i >= 0; i--)
    //            {
    //                HeapDown(i);
    //            }
    //        }

    //        //Heapup or bubble up is done to make sure the 
    //        private void HeapUp(int i)
    //        {

    //            T elt = _list[i];
    //            while (true)
    //            {
    //                int parent = Parent(i);
    //                //if the parent value is lesser than o or
    //                //if the comparer value is greater than 0 then break...
    //                //eg in case of min-heap the parent should be smaller
    //                if (parent < 0 || _comparer.Compare(_list[parent], elt) > 0)
    //                {
    //                    break;
    //                }
    //                SwapCells(i, parent);
    //                //this loop will continue untill the parent heap condition is satified
    //                //ie in min-heap the parent should be the smallest
    //                i = parent;
    //            }
    //        }

    //        //Logic: This compares the new root with the children and make sure it does not violate the heap property

    //        private void HeapDown(int i)
    //        {
    //            while (true)
    //            {
    //                //get the leftchild
    //                int lchild = LeftChild(i);
    //                //if there is no left child..only one item exists
    //                if (lchild < 0) break;

    //                //get the index of right child
    //                int rchild = RightChild(i);


    //                //child should one of the two children depending on the min or max heap
    //                //in case of min heap the child will be the minimum of two childrens

    //                //if right is lesser than zero eg. only two elements are present take left as child
    //                //child will be the index of left or right child
    //                int child = rchild < 0 ? lchild : (_comparer.Compare(_list[lchild], _list[rchild]) > 0 ? lchild : rchild);

    //                if (_comparer.Compare(_list[child], _list[i]) < 0)
    //                {
    //                    //if the parent is less
    //                    break;
    //                }
    //                SwapCells(i, child);
    //                i = child;
    //            }
    //        }

    //        //get the index of the parent for the given index
    //        private int Parent(int i)
    //        {
    //            return i <= 0 ? -1 : SafeIndex((i - 1) / 2);

    //        }

    //        //get the index of the right child of the given index
    //        private int RightChild(int i)
    //        {
    //            return SafeIndex(2 * i + 2);
    //        }

    //        //gets the index of the left child of the given index
    //        private int LeftChild(int i)
    //        {
    //            return SafeIndex(2 * i + 1);
    //        }

    //        private int SafeIndex(int i)
    //        {
    //            return i < Count ? i : -1;
    //        }

    //        //To swap the value of the array for the given two array index
    //        private void SwapCells(int i, int j)
    //        {
    //            T temp = _list[i];
    //            _list[i] = _list[j];
    //            _list[j] = temp;
    //        }

    //        public T GetItemByIndex(int i)
    //        {
    //            return _list[SafeIndex(i)];
    //        }


    //        public T GetItemByValue(T item)
    //        {

    //            foreach (T current in _list)
    //            {
    //                if (_wordcomparer.Compare(current, item) == 0)
    //                {
    //                    return current;

    //                }
    //            }

    //            return default(T);
    //        }

    //        public IList<T> GetList()
    //        {
    //            return this._list;
    //        }
    //    }

    //    public class MinHeap<T>
    //    {
    //        private readonly Heap<T> heap;


    //        public MinHeap(int size, IComparer<T> comparer)
    //        {
    //            heap = new Heap<T>(new List<T>(), size, comparer);

    //        }

    //        public void Insert(T item)
    //        {
    //            heap.Insert(item);
    //        }

    //        public T PopRoot()
    //        {
    //            return heap.PopRoot();
    //        }

    //        public T Peek()
    //        {
    //            return heap.PeekRoot();
    //        }

    //        public int Size()
    //        {
    //            return heap.Count;
    //        }

    //        public List<T> GetItems()
    //        {
    //            return (List<T>) heap.GetList();
    //        }
    //        public T GetItemByIndex(int i)
    //        {
    //            return heap.GetItemByIndex(i);

    //        }

    //        public T GetItemByValue(T item)
    //        {
    //            return heap.GetItemByValue(item);

    //        }

    //    }

    //    public class Movie
    //    {
    //        public int ID { get; set; }
    //        public float Rating { get; set; }

    //        public List<Movie> getSimilarMovies()
    //        {
    //            return new List<Movie>();
    //        }

    //        public float getRating()
    //        {
    //            return this.Rating;
    //        }
    //    }
    //}



}
