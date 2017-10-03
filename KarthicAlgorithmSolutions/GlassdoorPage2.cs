using Puzzles.DataStructures.Other;
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
    public partial class GlassdoorPage2 : Form
    {
        public GlassdoorPage2()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox15.Text);
            this.textBox12.Text = MinimumNumberofJumps(input, 0, input.Length - 1).ToString();


        }

        //http://www.geeksforgeeks.org/minimum-number-of-jumps-to-reach-end-of-a-given-array/

        private int MinimumNumberofJumps(int[] array, int low, int high)
        {
            //base case 
            if (low == high)
            {
                //destination or the last is reached..
                return 0;
            }

            //if any of the array value is 0.. mean we can't move any step further so retun max
            if (array[low] == 0)
            {
                return Int32.MaxValue;
            }
            //if number of steps need to move is outside the bound of the array
            if (low + array[low] > high)
            {
                return Int32.MaxValue;
            }

            int minjumps = Int32.MaxValue;
            ///we can start from low + 1 to number of steps mentioned in the low value
            for (int i = low + 1; i <= high && i <= low + array[i]; i++)
            {
                int jumps = 1; //we made a jump from low to any of the next value
                //to avoid overflow error
                int childjumps = MinimumNumberofJumps(array, i, high);
                if (childjumps == Int32.MaxValue)
                {
                    jumps = Int32.MaxValue;
                }
                else
                {
                    jumps = jumps + childjumps;
                }

                if (jumps < minjumps)
                {
                    minjumps = jumps;
                }
            }


            return minjumps;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=cETfFsSTGJI

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox15.Text);

            JumpItem result = MinimumNumberofJumpsDP(input);
            this.textBox12.Text = result.MinimumJump.ToString();
            this.textBox1.Text = result.Path;


        }

        private JumpItem MinimumNumberofJumpsDP(int[] input)
        {
            if (input.Length == 0 || input[0] == 0)
            {
                return null;
            }

            int[] jumps = new int[input.Length];
            int[] jumpvalues = new int[input.Length];

            // Find the minimum number of jumps to reach arr[i] from 0 to i -1
            // from arr[0], and assign this value to jumps[i]
            jumps[0] = 0;
            jumpvalues[0] = -1;

            for (int i = 1; i < input.Length; i++)
            {
                jumps[i] = Int32.MaxValue; //Initialize with max and then we update with actual value
                //Here we need to see whether we can reach i from 0 to i -1...
                //Note i and j should not be equal because there is no reason to start and end at the same place. (No need to jump if the start and end are same)
                for (int j = 0; j < i; j++)
                {
                    //can we reach index i if we jump from j position
                    if (j + input[j] >= i && jumps[j] != Int32.MaxValue)
                    {
                        int steps = jumps[j] + 1;
                        if (steps < jumps[i])
                        {
                            //jumps[i] = min(jumps[i], jumps[j] + 1);
                            jumps[i] = steps;
                            jumpvalues[i] = j; //index where it came from
                   
                        }
                    }
                }
            }


            //Backtrack with jump values

            StringBuilder sb = new StringBuilder();
            int k = input.Length -1;
            sb.Insert(0, "," + input[k]);

            while (k > 0)
            {
                int lastjumpfromIndex = jumpvalues[k];
                sb.Insert(0, "," + input[lastjumpfromIndex]);
                k = lastjumpfromIndex;
            }

            JumpItem result = new JumpItem();
            result.MinimumJump = jumps[input.Length - 1]; //get the lasat item
            result.Path = sb.ToString();
            return result;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \   \
            //   D   E   F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->F-->NULL

            //Build the tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');
            root.Right.Right = new NodeWithNext<char>('F');

            PopulateNextPointerUsingBFS(root);

            //test
            NodeWithNext<char> test = root.Right.Next;//null
            NodeWithNext<char> test2 = root.Left.Right.Next; //f

        }

        private void PopulateNeighboursUsingBFS(NodeWithNext<char> root)
        {
            if (root == null)
            {
                return;
            }

            Queue<NodeWithNext<char>> myqueue = new Queue<NodeWithNext<char>>();
            //set the root next pointer
            root.Next = root.Left;
            myqueue.Enqueue(root);

            while (myqueue.Count != 0)
            {
                NodeWithNext<char> parent = myqueue.Dequeue();
                //set the parent next pointer
                //check if there is no next element in the queue or the next element doesn't belong to this level
                if (myqueue.Count == 0)
                {
                    parent.Next = null;
                }
                else
                {
                    parent.Next = myqueue.Peek();
                }

                if (parent.Left != null)
                {
                    myqueue.Enqueue(parent.Left);
                }

                if (parent.Right != null)
                {
                    myqueue.Enqueue(parent.Right);
                }

            }

        }

        private void PopulateNextPointerUsingBFS(NodeWithNext<char> root)
        {
            if (root == null)
            {
                return;
            }

            int level = 0;
            Queue<QueueCustomNode<char>> myqueue = new Queue<QueueCustomNode<char>>();
            QueueCustomNode<char> level0 = new QueueCustomNode<char>(root, level);
            //set the root next pointer
            root.Next = root.Left;
            myqueue.Enqueue(level0);

            while (myqueue.Count != 0)
            {
                QueueCustomNode<char> parent = myqueue.Dequeue();
                //set the parent next pointer
                //check if there is no next element in the queue or the next element doesn't belong to this level
                level = parent.Level + 1;
                if (myqueue.Count == 0 || myqueue.Peek().Level != parent.Level)
                {
                    parent.Node.Next = null;
                }
                else
                {
                    parent.Node.Next = myqueue.Peek().Node;
                }

                if (parent.Node.Left != null)
                {
                    myqueue.Enqueue(new QueueCustomNode<char>(parent.Node.Left, level));
                }

                if (parent.Node.Right != null)
                {
                    myqueue.Enqueue(new QueueCustomNode<char>(parent.Node.Right, level));
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \    \
            //   D   E   G  F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->G  F-->NULL

            //Build the tree

            //Imporant this logic won't work unless the tree is complete binary tree so make a complete binary tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');

            root.Right.Left = new NodeWithNext<char>('G');
            root.Right.Right = new NodeWithNext<char>('F');

            //set root's next
            root.Next = null;
            PopulateNextPointerUsingDFS(root);

            //test
            NodeWithNext<char> test = root.Right.Next;//null
            NodeWithNext<char> test2 = root.Left.Right.Next; //G
        }


        //Assumption:
        //The tree is complete binary tree
        //Root next's is already set (set root next outside this function
        private void PopulateNextPointerUsingDFS(NodeWithNext<char> root)
        {
            if (root == null)
            {
                return;
            }

            //The root next will be alredy fixed
            //set the next of the left and right child
            if (root.Left != null)
            {
                root.Left.Next = root.Right;
            }

            if (root.Right != null)
            {
                //see whether the parent has next pointer
                //if root.next == null then it is the rightmost node so set to null
                root.Right.Next = (root.Next == null) ? null : root.Next.Left;
            }

            PopulateNextPointerUsingDFS(root.Left);
            PopulateNextPointerUsingDFS(root.Right);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \   \
            //   D   E   F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->F-->NULL

            //Build the tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');
            root.Right.Right = new NodeWithNext<char>('F');

            PopulateNeighboursUsingBFS(root);

            //test
            NodeWithNext<char> test = root.Right.Next;//d
            NodeWithNext<char> test2 = root.Left.Right.Next; //f
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \   \
            //   D   E   F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->F-->NULL

            //Build the tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');
            root.Right.Right = new NodeWithNext<char>('F');

            root.Next = null;

            PopulateNextPointerUsingDFSNonCompleteTrees(root);

            //test
            NodeWithNext<char> test = root.Right.Next;//null
            NodeWithNext<char> test2 = root.Left.Right.Next; //f
        }

        //http://www.geeksforgeeks.org/connect-nodes-at-same-level-with-o1-extra-space/
        //Logic:  Traverse in (next, left, right) fashion
        //use helper function to get next for right child and for left child when right is null
        //Assumption: root should have next set before calling this function

        private void PopulateNextPointerUsingDFSNonCompleteTrees(NodeWithNext<char> root)
        {
            if (root == null)
            {
                return;
            }

            //Logic:  Traverse in (next, left, right) fashion
            if (root.Next != null)
            {
                PopulateNextPointerUsingDFSNonCompleteTrees(root.Next);
            }

            if (root.Left != null)
            {
                //set the next pointer of root.Left and root.Right and call the recurssive fu for left

                if (root.Right != null)
                {
                    root.Left.Next = root.Right;
                    root.Right.Next = GetNextNode(root);
                }
                else
                {
                    root.Left.Next = GetNextNode(root);
                }
                
                // Recursively call for next level nodes.  Note that we call only
                //for left child. The call for left child will call for right child */
                PopulateNextPointerUsingDFSNonCompleteTrees(root.Left);
            }
            else if (root.Right != null)
            {
                //set the next pointer for right child and then call recurssion
                root.Right.Next = GetNextNode(root);

                PopulateNextPointerUsingDFSNonCompleteTrees(root.Right);
            }
            else
            {
                PopulateNextPointerUsingDFSNonCompleteTrees(GetNextNode(root));
            }
            
        }

        /* This function returns the leftmost child of nodes at the same level as p.
   This function is used to getNExt right of p's right child
   If right child of p is NULL then this can also be used for the left child */
        private NodeWithNext<char> GetNextNode(NodeWithNext<char> root)
        {
            NodeWithNext<char> runner = root.Next;
            while (runner != null)
            {
                if (runner.Left != null)
                {
                    return runner.Left;
                }
                else if (runner.Right != null)
                {
                    return runner.Right;
                }
                else
                {
                    runner = runner.Next;
                }
            }
            return runner;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \   \
            //   D   E   F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->F-->NULL

            //Build the tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');
            root.Right.Right = new NodeWithNext<char>('F');

            //last node should have null as next
            NodeWithNext<char> lastvisited = null;
            PopulateNextPointToInOrderSuccessor(root, ref lastvisited);

            //test
            NodeWithNext<char> test = root.Right.Next;//F
            NodeWithNext<char> test2 = root.Left.Right.Next; //A
            NodeWithNext<char> test3 = root.Left.Right.Next.Next; //C
        }

        //Logic Travers in this fashio (right, current, left)
        //We need to pass value from children to parent (bubble up ) so we have to use return varaible
        //But here return variable will not work because for null parent, we still need to preserve lastvisited node..so use static variable or use reference to get the lastvisited node
        private void PopulateNextPointToInOrderSuccessor(NodeWithNext<char> root, ref NodeWithNext<char> lastvisited)
        {
            if (root != null)
            {
        
                PopulateNextPointToInOrderSuccessor(root.Right, ref lastvisited);

                //current
                root.Next = lastvisited;
                lastvisited = root;

                PopulateNextPointToInOrderSuccessor(root.Left, ref lastvisited);

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MathPoint p1 = new MathPoint(1, 1);
            MathPoint p2 = new MathPoint(4, 4);
            MathPoint p3 = new MathPoint(5, 5);
            MathPoint p4 = new MathPoint(2,5);
            MathPoint p5 = new MathPoint(6, 3);

            List<MathPoint> points = new List<MathPoint>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);

            MathPoint[] pointsarray = points.ToArray();
            PointIntersection obj = new PointIntersection();
            Line bestline = obj.FindBestLine(pointsarray);


        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Given a historgram
            //refer gayle 6th edition page 596

            // int[] histogram = { 0, 0, 4, 0, 0, 6, 0, 0, 3, 0, 8, 0, 2, 0, 5, 2, 0, 3, 0, 0 };
            int[] histogram = { 3, 0, 0, 2, 0, 4 };
            //historgramn width is assumed to be 1

            int result = ComputeHistogramVolume(histogram);

            //result = 46
            bool test = (result == 46);
        }

        /* Logic:
         * Given is the bar graph with bar values means height of the bar and imagine water is poured on top of that
         * Find the heighest bar value..The highest doesn't have any impact..it depend upon the next highest on the right and left
         * Find the heightest in the given array
         * Split the array into left and right based on the height
         * Find the next heighest on each recurrsion and Min of heighest and nextheight will the one used to calculate volume..see the pic
         * Computer volume for left and right side and return the su,
         */

        // http://www.geeksforgeeks.org/trapping-rain-water/

        private int ComputeHistogramVolume(int[] histogram)
        {
            int left = 0;
            int right = histogram.Length - 1;
            int highestindex = FindIndexOfMax(histogram, left, right);
            int leftvolume = FindSubGraphVolume(histogram, left, highestindex, true);
            int rightvolume = FindSubGraphVolume(histogram, highestindex, right, false);
            return leftvolume + rightvolume;

        }



        private int FindSubGraphVolume(int[] histogram, int left, int right, bool isleftofhighest)
        {
            /* we can get the highest from the params and get next heightest in the sub graph */
            if (left >= right)
            {
                return 0;
            }
            int sum = 0;
            if (isleftofhighest)
            {
                int highestindex = right;
                int  nexthighestindex = FindIndexOfMax(histogram, left, right - 1);
                sum += FindborderVolume(histogram, nexthighestindex, highestindex);
                sum += FindSubGraphVolume(histogram, left, nexthighestindex, isleftofhighest);
            }
            else
            {
                int highestindex = left;
                int nexthighestindex = FindIndexOfMax(histogram, highestindex + 1, right);
                sum += FindborderVolume(histogram, highestindex, nexthighestindex);
                sum += FindSubGraphVolume(histogram, nexthighestindex, right, isleftofhighest);
            }

            return sum;
        }

        private int FindIndexOfMax(int[] array, int start, int end)
        {
            int maxindex = -1;
            int maxvalue = Int32.MinValue;
            for(int index=start; index <= end; index++)
            {
                if (array[index] > maxvalue)
                {
                    maxvalue = array[index];
                    maxindex = index;

                }

            }

            return maxindex;
        }

        private int FindborderVolume(int[] histogram, int start, int end)
        {
            //we take the min of start and end for volume calculation
            int minborder = Math.Min(histogram[start], histogram[end]);
            int count =0;
            for (int i = start + 1; i < end; i++)
            {
                //think of the volume of the container
                count = count + (minborder - histogram[i]);
            }

            return count;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Given a historgram
            //refer gayle 6th edition page 596

            int[] histogram = { 0, 0, 4, 0, 0, 6, 0, 0, 3, 0, 8, 0, 2, 0, 5, 2, 0, 3, 0, 0 };
            //historgramn width is assumed to be 1

            int result = ComputeHistorgramVolumeInOptimezedWay(histogram);

            bool test = (result == 46);
        }

        /* Logic:
         * Given Height:                        0, 0, 4, 0, 0, 6, 0, 0, 3, 0, 8, 0, 2, 0, 5, 2, 0, 3, 0, 0 
         * Compute Left Max                     0  0  4  4  4  6  6  6  6  6  8  8  8  8  8  8  8  8  8  8
         * (max value from the oth index)
         * Compute Right Max                    8 ............................8  5  5  5  5  3  3  3  0  0
         * Min of Left and right max            0  0   4 4  4  6  6  6  6  6  8  5  5  5  5  3  3  3  0  0
         * 
         * Delta 
         * (diff btw min and actual height)     0  0 0  4 4 0 6 6..................................0  0  0
         *
         * Formula: Volume of water at bar = difference between (height of the bar  and min (tallest bar on left, tallest bar on right))
         */
        private int ComputeHistorgramVolumeInOptimezedWay(int[] histogram)
        {
            int volume = 0;
            int[] leftmax = new int[histogram.Length];
            //scan the array and populate leftmax

            int max =  histogram[0];

            for (int i = 0; i < histogram.Length; i++)
            {
                if(histogram[i] > max)
                {
                    max = histogram[i];
                }

                leftmax[i] = max;
            }


            int rightmax = histogram[histogram.Length -1];
           //scan the array another time and populate right max..calculate min, delta and volume
            for (int i = histogram.Length -1; i >= 0; i--)
            {
                if (histogram[i] > rightmax)
                {
                    rightmax = histogram[i]; 
                }

                //we have right max calculate minimum of left and right max
                int minofLR = Math.Min(rightmax, leftmax[i]);
               
                if(minofLR > histogram[i])
                {
                    volume = volume + (minofLR - histogram[i]);
                }

            }

            return volume;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int[] bigarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] smallarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            this.textBox4.Text = "";
        }

        /* Logic:
         * Iterate over the big array 
         * and for each index of big array scan the big array again and search for all the elements of the small array 
         * get the max index on the big array where all the small array elements are first found.
         * Populate the range with the bigarray start and end index..
         * Continue this for every index and returmn the Range that has shortes size
         
         * Time Complexity: 0(SB^2) bcoz for each of the big array of size b we do 0(sB) so sb^2
         */
        private Range FindShoresSuperSequenceBrutal(int[] bigarrray, int[] smallarray)
        {
            int beststart = -1;
            int bestend = -1;

          // No time Refer the g page 584
            return null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[] bigarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] smallarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            Range result = GetShortesSubsequence(bigarray, smallarray);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {
       
                for (int i = result.StartIndex; i <= result.EndIndex; i++)
                {
                    sb.Append(bigarray[i]).Append(",");
                }
            }
            this.textBox4.Text = sb.ToString();
        }

        /*Logic: pg: 586 in gay new book
         * Scan the big array in reverse and find the last occurance of each element
         * /Given small array 1,5,9
         *   value                       7 5 9 0 2 1 3 5 7 9  1  1  5 8  8  9  7
         *   Index                       0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16
         *   sa value 1
         *   (populate from last)        5 5
         *   last occurance
         *    sa value 5                 1 1
         *    sa value 9                 2 2
         *    
         *    closure                    5 5
         *    (max of all values)
         *    difference btw clousre     5 4
         *    and index
         *   
         *     
         * 
         */
        private Range GetShortesSubsequence(int[] bigarray, int[] smallarray)
        {
            int[] closures = GetClosures(bigarray, smallarray);
            return FindShortestRange(closures);

        }

        private Range FindShortestRange(int[] closures)
        {
            //initalize to 0
            Range shortest = new Range(0, closures[0]);

            for (int i = 1; i < closures.Length; i++)
            {
                if (closures[i] == -1)
                {
                    //some element might be missing so skip this
                    break; //look at the diagram anything after this will be -1
                }
                else
                {
                    Range range = new Range(i, closures[i]);
                    if (range.GetLength() < shortest.GetLength())
                    {
                        shortest = range;
                    }
                }
            }

            return shortest;

        }

        private int[] GetClosures(int[] bigarray, int[] smallarray)
        {
            int[] closures = new int[bigarray.Length];
            for (int i = 0; i < smallarray.Length; i++)
            {
                populateclosure(bigarray, closures, smallarray[i]);
            }

            return closures; //closures here will have max of the last occurance of the small array elements
        }

        private void populateclosure(int[] bigarray, int[] closures, int smallarrayvalue)
        {
            int next = -1;
            for (int i = bigarray.Length - 1; i >= 0; i--)
            {
                if (bigarray[i] == smallarrayvalue)
                {
                    next = i;
                }
                //if the next is -1 update closure..
                //if the next is positive and greater then the current value..then it might be max update it
                //if the closure already have -1 means some element in small array is missing after this index so don't upate..let it be -1
                if ((next == -1 || next > closures[i]) && closures[i] != -1)
                {
                    closures[i] = next;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] bigarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] smallarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            Range result = GetShortestSubSequenceUsingMinHeap(bigarray, smallarray);
            StringBuilder sb = new StringBuilder();
            if (result != null)
            {

                for (int i = result.StartIndex; i <= result.EndIndex; i++)
                {
                    sb.Append(bigarray[i]).Append(",");
                }
            }
            this.textBox4.Text = sb.ToString();
        }

        /* Logic
         * For each small array element get all the occurence of the index in big array
         *  Given Small array (1,5,9)
         *  1 will be in index { 5,10, 11)
         *  5 will be in index { 1,7,12}
         *  9 will be in index {2,3,9,15}
         *  
         * First sub sequence start will be  min of head and  end will be max of head
         * Put all the head of the list in min heap..calculate the range
         * pop the min value..Get the list from where the min element is popped
         * remove the popped element from the list as well
         * add the head of this list to min heap again and find the range
         * Continue till one of the list in empty
         * 
         * Time: 0( b log s)
         * 
         * 
         */

        private Range GetShortestSubSequenceUsingMinHeap(int[] bigarray, int[] smallarray)
        {
            List<Queue<int>> myqueue = PopulatelistofQueue(bigarray, smallarray);

            return FindShortestRange(myqueue);


        }
        
        //we can use either queue or linkedlist
        private List<Queue<int>> PopulatelistofQueue(int[] bigarray, int[] smallarray)
        {

            List<Queue<int>> mylist = new List<Queue<int>>();
            for (int i = 0; i < smallarray.Length; i++)
            {
                Queue<int> myqueue = new Queue<int>();
                int smallarrayvalue = smallarray[i];
                for (int j = 0; j < bigarray.Length; j++)
                {
                    if (bigarray[j] == smallarrayvalue)
                    {
                        myqueue.Enqueue(j); //get the index of big array
                    }
                }

                if (myqueue.Count == 0)
                {
                    return null; //if any element in not found in big array
                }

                mylist.Add(myqueue);
            }

            return mylist;
           
        }

        private Range FindShortestRange(List<Queue<int>> queuelist)
        {
            MinHeap<HeapNodeCustom> heap = new MinHeap<HeapNodeCustom>(new KarthicMinHeapComparer4());

            int bestRangemin = Int32.MaxValue;
            int bestRangemax = Int32.MinValue;

            //insert head of  list into heap
            for (int i = 0; i < queuelist.Count; i++)
            {
                Queue<int> queue = queuelist[i];
                heap.Insert(new HeapNodeCustom(queue.Peek(), i));
                if (queue.Peek() > bestRangemax)
                {
                    bestRangemax = queue.Peek();
                }
            }

            bestRangemin = heap.Peek().Value;
            Range shortestrange = new Range(bestRangemin, bestRangemax);  //initial best to first



            //we have max value...get the min value from the heap
            while (true)
            {
                HeapNodeCustom minrecord = heap.PopRoot(); //pop the min value
                //get the list of poped element
                Queue<int> queue =  queuelist[minrecord.IndexofList];
                //remove the first element
                queue.Dequeue();

                if (queue.Count == 0)
                {
                    break;
                }

                //insert next element in this queue to the heap
                heap.Insert(new HeapNodeCustom(queue.Peek(), minrecord.IndexofList));
                //we have running value of max
                if (queue.Peek() > bestRangemax)
                {
                    bestRangemax = queue.Peek();
                }
                bestRangemin = heap.Peek().Value;
                Range range = new Range(bestRangemin, bestRangemax);
                if (range.GetLength() < shortestrange.GetLength())
                {
                    shortestrange = range;
                }

            }

            return shortestrange;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //            Input Tree
            //       A
            //      / \
            //     B   C
            //    / \   \
            //   D   E   F


            //Output Tree
            //       A--->NULL
            //      / \
            //     B-->C-->NULL
            //    / \   \
            //   D-->E-->F-->NULL

            //Build the tree
            NodeWithNext<char> root = new NodeWithNext<char>('A');
            root.Left = new NodeWithNext<char>('B');
            root.Right = new NodeWithNext<char>('C');

            root.Left.Left = new NodeWithNext<char>('D');
            root.Left.Right = new NodeWithNext<char>('E');
            root.Right.Right = new NodeWithNext<char>('F');

            //set for root
            root.Next = (root.Left != null) ? root.Left : root.Right;
            PopulateNeightboursUsingDFS(root);

            //test
            NodeWithNext<char> test = root.Right.Next;//d
            NodeWithNext<char> test2 = root.Left.Right.Next; //f
        }

        private void PopulateNeightboursUsingDFS(NodeWithNext<char> root)
        {
            if (root == null)
            {
                return;
            }

            //Logic: Root will already have next set. Now  Traverse (left, right) fashion
     
            if (root.Left != null)
            {
                //set the next pointer of root.Left and root.Right and call the recurssive fu for left

                if (root.Right != null)
                {
                    root.Left.Next = root.Right;
                    root.Right.Next = GetNextNode(root);
                }
                else
                {
                    root.Left.Next = GetNextNode(root);
                }

                // Recursively call for next level nodes.  Note that we call only
                //for left child. The call for left child will call for right child */
                PopulateNextPointerUsingDFSNonCompleteTrees(root.Left);
            }
            else if (root.Right != null)
            {
                //set the next pointer for right child and then call recurssion
                root.Right.Next = GetNextNode(root);

                PopulateNextPointerUsingDFSNonCompleteTrees(root.Right);
            }
            else
            {
                PopulateNextPointerUsingDFSNonCompleteTrees(GetNextNode(root));
            }
            

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }


       
  
    }

    public class HeapNodeCustom
    {
        public int Value { get; set; }
        public int IndexofList { get; set; }

        public HeapNodeCustom(int value, int indexoflist)
        {
            this.Value = value;
            this.IndexofList = indexoflist;
        }
    }



    public class KarthicMinHeapComparer4 : IComparer<HeapNodeCustom>
    {

        public int Compare(HeapNodeCustom x, HeapNodeCustom y)
        {
            return y.Value.CompareTo(x.Value);
        }
    }

    public class Range
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public Range(int startindex, int endindex)
        {
            this.StartIndex = startindex;
            this.EndIndex = endindex;
        }

        public int GetLength()
        {
            return EndIndex - StartIndex + 1;
        }
    }
    public class JumpItem
    {
        public int MinimumJump { get; set; }
        public string Path { get; set; }
    }

    public class NodeWithNext<T>
    {
        public T Data { get; set; }
        public NodeWithNext<T> Left { get; set; }
        public NodeWithNext<T> Right { get; set; }
        public NodeWithNext<T> Next { get; set; }


        public NodeWithNext(T value)
        {
            this.Data = value;
            this.Left = null;
            this.Right = null;
            this.Next = null;

        }


    }

    public class QueueCustomNode<T>
    {
        public NodeWithNext<T> Node { get; set; }
        public int Level { get; set; }

        public QueueCustomNode(NodeWithNext<T> node, int level)
        {
            this.Node = node;
            this.Level = level;
        }
    }
}
