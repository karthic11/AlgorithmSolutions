using Puzzles.DataStructures.Common;
using Puzzles.DataStructures.Tree.AVLTree;
using Puzzles.DataStructures.Tree.SegementationTree;
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
    public partial class TechCompanyPage5 : Form
    {
        public TechCompanyPage5()
        {
            InitializeComponent();
        }

        private void TechCompanyPage5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<int> index = new List<int>();
            
       
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /* Constructing tree given in the above figure */
            AVLTree tree = new AVLTree();
            var root = tree.Root;

            root = tree.Insert(root, 9);
            root = tree.Insert(root, 5);
            root = tree.Insert(root, 10);
            root = tree.Insert(root, 0);
            root = tree.Insert(root, 6);
            root = tree.Insert(root, 11);
            root = tree.Insert(root, -1);
            root = tree.Insert(root, 1);
            root = tree.Insert(root, 2);

            /* The constructed AVL Tree would be
                    9
                   /  \
                  1    10
                /  \     \
               0    5     11
              /    /  \
             -1   2    6 */

            //delete 10

            root = tree.Delete(root, 10);


            /* The AVL Tree after deletion of 10
                    1
                   /  \
                  0    9
                /     /  \
               -1    5     11
                   /  \
                  2    6
            */
 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);
            int windowsize = Convert.ToInt32(this.textBox1.Text);
            string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(MaxOfSlidingWindow(input, windowsize));
            this.textBox11.Text = output;
        }

        public int[] MaxOfSlidingWindow(int[] input, int w)
        {
            int[] output = new int[input.Length];
            int outputindex = 0;

            for (int i = 0; i <= input.Length - w; i++)
            {
                int max = Int32.MinValue;
                //for each of i ..iterate for another w 0,1,2
                for (int j = 0; j < w; j++)
                {
                    if (input[i + j] > max)
                    {
                        max = input[i + j];
                    }

                }

                //at the end of the window
                output[outputindex] = max;
                outputindex++;

            }

             Array.Resize<int>(ref output, outputindex);

             return output;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);
            int windowsize = Convert.ToInt32(this.textBox1.Text);
            string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(MaxofSlidingWindowUsingHeap(input, windowsize));
            this.textBox11.Text = output;

        }


        //Logic:
        //Create maxheap to store max value of the window
        //note: maxheap size doesn't matter
        //Insert first 3 items (window size)
        //loop the other item
        //the heap item has value and index
        //for each item added we insert into the output
        // Generally we should delete the items that are not in the window  eg 3,2,4,5, 6 consider (3,2,4) and then (2,4,5) and then (5,6 etc)
        //but here since we know the heap max we delete the item only if the max heap index is outside the window
        //If the last max heap is outside the current window we remove the peek and then check untill the maxheap index is within the current window
        //Note: here the heap size doesn't matter we may have heap of size 10 for windows of 3 eg input  0,1,2,3,4,5,6,7,8,9,10

        public int[] MaxofSlidingWindowUsingHeap(int[] input, int w)
        {
            int[] output = new int[input.Length];
            int outputindex = 0;
            MaxHeap<Pair> maxheap = new MaxHeap<Pair>(new KarthicMaxHeapPairComparer());
            //0,1,2
            for (int i = 0; i < w; i++)
            {
                maxheap.Insert(new Pair(input[i], i));
            }
            //we already insert 0 to w-1
            for (int j = w; j < input.Length ; j++)
            {
                //previous window peek
                Pair max = maxheap.Peek();
                output[outputindex] = max.Value;
                outputindex++;
                //j - w make sure that only the elements that are lesser than this window should be popped up if it has the peek value
                //update: check whether the max.Index is inside the current window
                //If it is outside the current window then pop and get the next max
                //while (max.Index + w <= j ) this condition is also same
                while (max.Index <= j - w)
                {
                    maxheap.PopRoot();
                    max = maxheap.Peek();

                }

                maxheap.Insert(new Pair(input[j], j));

            }


            Pair max1 = maxheap.Peek();
            output[outputindex] = max1.Value;
            outputindex++;


             Array.Resize<int>(ref output, outputindex);

             return output;

        }

        public class Pair
        {
            public int Value { get; set; }
            public int Index { get; set; }
            public Pair(int value, int index)
            {
                this.Value = value;
                this.Index = index;
            }
        }

        public class KarthicMaxHeapPairComparer : IComparer<Pair>
        {

            int IComparer<Pair>.Compare(Pair x, Pair y)
            {
                return x.Value.CompareTo(y.Value);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);
            int windowsize = Convert.ToInt32(this.textBox1.Text);
            string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(MaxofSlidingWindowUsingDoubleEndedQueue(input, windowsize));
            this.textBox11.Text = output;

        }

        //http://www.geeksforgeeks.org/maximum-of-all-subarrays-of-size-k/

        //since we don't have implementation for double ended queue , i'm using linked list   
        // Create a Double Ended Queue, Qi that will store indexes of array elements
        // The queue will store indexes of useful elements in every window and it will
        // maintain decreasing order of values from front to rear in Qi, i.e., 
        // arr[Qi.front[]] to arr[Qi.rear()] are sorted in decreasing order
        
        //Important Note:
        //We are using double ended queue here so list has first and last
        //We have to maintain the order eg descending order on each iteration   first [ list eg (3,2,1) ] last

        //to maintain the order we have this followng rules
        //1) when a new item come in, we need to delete all the items lesser than the new item in the list and add the new item to the last (delete useless item)
        //2) when a new items comes in, we need to check if the peek/first here is within the current window..if not delete  (delete out of window item)
        //we will maintain the first (hight to low) last order
        //we store only the index in the list

  

        public int[] MaxofSlidingWindowUsingDoubleEndedQueue(int[] input, int w)
        {
            int outputsize = input.Length - (w - 1);
            int[] output = new int[outputsize];
            int outputindex = 0;
            LinkedList<int> list = new LinkedList<int>();

            //0,1,2
            for (int i = 0; i < w; i++)
            {
                //add the element to the back of the list
                //if the element to be added is larger than the last of the back then pop

                //If the incoming element in larger than last, trim the tail using this
                while (list.Count != 0 && input[i] >= input[list.Last.Value])
                //while (list.Count != 0 && input[i] <= input[list.Last.Value]) //for window to get minimum 
                {
                   //pop the last
                    list.RemoveLast();
                }

                list.AddLast(i); //list maintain the index not the value
                //here we add the element to last bcoz it is smaller than all the elenments in the list
            }

            //iterate for the rest 
            for (int j = w; j < input.Length; j++)
            {
                //the front will have the largest element of the previous window..print that
                output[outputindex] = input[list.First.Value];
                outputindex++;

                //make sure to remove elements on the first of the list (which hold the largest) if it is not in the current window
                // Remove the elements which are out of this window
                while (list.Count != 0 && list.First.Value <= j - w)
                {
                    list.RemoveFirst();
                }

                // Remove all elements smaller than the currently
                // being added element (remove useless elements)
           
                while (list.Count != 0 && input[j] >= input[list.Last.Value])
                {
                    list.RemoveLast();
                }

                // Add current element at the rear of Qi
                list.AddLast(j);

            }

            output[outputindex] = input[list.First.Value];
            outputindex++;


            return output;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);

            this.textBox3.Text = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(ConstructSmallerElements(input));

        }

        public int[] ConstructSmallerElements(int[] input)
        {
            int[] output = new int[input.Length];
           
            for (int i = 0; i < input.Length; i++)
            {
           
                for (int j = i +1; j < input.Length; j++)
                {
                    if (input[j] < input[i])
                    {
                        output[i]++;
                    }
                    
                }
            }

            return output;
        }

        private void button5_Click(object sender, EventArgs e)
        {

     
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);

            int[] output = new int[input.Length];
            AVLTree tree = new AVLTree();

            for (int i = input.Length -1; i >= 0; i--)
            {
                int counter = 0;
                tree.Root = tree.Insert(tree.Root, input[i], ref counter);
                output[i] = counter;

            }


            this.textBox3.Text = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(output);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int querystart = Convert.ToInt32(this.textBox7.Text);
            int queryend = Convert.ToInt32(this.textBox6.Text);

            SegmentationTree tree = new SegmentationTree(input);

            int sum = tree.QuerySumBetweenIndex(querystart, queryend);
            this.textBox2.Text = sum.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int index = Convert.ToInt32(this.textBox8.Text);
            int value = Convert.ToInt32(this.textBox9.Text);
            SegmentationTree tree = new SegmentationTree(input);
            tree.UpdateValueAtindex(index, value);
            this.textBox5.Text = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(tree.InputArray);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //implementation question..see this class//
            // http://codegolf.stackexchange.com/questions/2554/find-possible-word-rectangles
            //2x2 
            //PA
            //AM

            ////2x3
            //GOB
            //ORE

            ////3x3
            //BAG
            //AGO
            //RED

            ////3x4
            //MACE
            //AGES
            //WEES


            //Given a dictionary with millions of words
            
            //Imagine you are given dictionar with the following word
            Dictionary<string, bool> ht = new Dictionary<string, bool>();
            ht.Add("BAG", true);
            ht.Add("AGO", true);
            ht.Add("RED", true);
            ht.Add("BAR", true);
            ht.Add("AGE", true);
            ht.Add("GOD", true);
            ht.Add("hard", true);
            ht.Add("work", true);
            ht.Add("never", true);
            ht.Add("fails", true);
            ht.Add("123456", true);


      


            LargestPossibelRectangle obj = new LargestPossibelRectangle();
            obj.PerformTheLogicOfThisProblem(ht);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox12.Text);

            //http://stackoverflow.com/questions/15197058/number-of-distinct-smaller-elements-on-left-for-each-position-in-a-array
            //eg For the array 1 1 2 4 5 3 6 the the answer would be 0 0 1 2 3 2 5
            //The same as above problem but insert from left to right
            int[] output = new int[input.Length];
            AVLTree tree = new AVLTree();

            for (int i = 0; i < input.Length; i++ )
            {
                int counter = 0;
                tree.Root = tree.Insert(tree.Root, input[i], ref counter);
                output[i] = counter;

            }


            this.textBox10.Text = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(output);
        }


    }
}
