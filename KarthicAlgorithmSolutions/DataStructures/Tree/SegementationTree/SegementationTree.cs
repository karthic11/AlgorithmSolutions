using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.SegementationTree
{
    public class SegmentationTree
    {
        public int[] InputArray { get; set; }
        public int[] SegmentationArray { get; set; }
        public SegmentationTree()
        {

        }

        public SegmentationTree(int[] array)
        {
            this.InputArray = array;

            int n = array.Length;

            // Allocate memory for segment tree
            // Height of the segement tree is h = log n
            int h = (int)Math.Ceiling(Math.Log(n, 2));
            //Maximum size of segment tree is defined as  2 * (2 power log n) - 1
            int max_size = 2 * (int)Math.Pow(2, h) - 1;  // 2 * (2^h) -1

            this.SegmentationArray = new int[max_size];

            /* Update: 4/18/2015. I don't understand the below logic for size...so get the logic mentioned above
            //This tree will contain n leaf node(array element) and n-1 internal nodes with 2 childs
            //int totalnode = n + (n - 1);
            //int maxsize = n + 2(n - 1); //each internal node will have two children  ignoring the 
            int maxsize = 2 * n + 2;//last right child
            this.SegmentationArray = new int[maxsize + 1];//at the worst it needs maxzise indexes

            //size n + 2(n -1)  ..since 2(n-1) will the node that have childresn..last node right child 2*i + 2 

            */



            ConstructSegemtationTree(this.InputArray, 0, InputArray.Length - 1, this.SegmentationArray, 0);
        }

        //Construct Segemtation Tree
        public int ConstructSegemtationTree(int[] input, int ss, int se, int[] segtree, int segindex)
        {
            //when the ss and se is equal means only one element the set the index
            if (ss == se)

            {
                segtree[segindex] = input[se];
                return input[se];
            }

            int middle = (ss + se) / 2;
            //divide the given ss and se into two segments
            segtree[segindex] = ConstructSegemtationTree(input, ss, middle, segtree, 2 * segindex + 1) +
                                ConstructSegemtationTree(input, middle + 1, se, segtree, 2 * segindex + 2);


            return segtree[segindex];
        }

        public int QuerySumBetweenIndex(int lowindex, int hightindex)
        {
            int[] segarray = this.SegmentationArray;
            return GetSum(segarray, this.InputArray.Length, lowindex, hightindex);
        }


        public void UpdateValueAtindex(int index, int newvalue)
        {
              updateValue(this.InputArray, this.SegmentationArray, this.InputArray.Length, index, newvalue);
       
        }

        public int GetSum(int[] segarray, int n, int qs, int qe)
        {
            // Check for erroneous input values
            if (qs < 0 || qe > n - 1 || qs > qe)
            {
                //error
            }

            return getSumUtil(segarray, 0, n - 1, qs, qe, 0);
        }



        /*  A recursive function to get the sum of values in given range of the array.
    The following are parameters for this function.
 
    st    --> Pointer to segment tree
    index --> Index of current node in the segment tree. Initially 0 is
             passed as root is always at index 0
    ss & se  --> Starting and ending indexes of the segment represented by
                 current node, i.e., st[index]
    qs & qe  --> Starting and ending indexes of query range */

        //http://www.geeksforgeeks.org/segment-tree-set-1-sum-of-given-range/
        //        int getSum(node, l, r) 
        //{
        //   if range of node is within l and r
        //        return value in node
        //   else if range of node is completely outside l and r
        //        return 0
        //   else
        //    return getSum(node's left child, l, r) + 
        //           getSum(node's right child, l, r)
        //}
        public int getSumUtil(int[] st, int ss, int se, int qs, int qe, int index)
        {
            // If segment of this node is a part of given range, then return the 
            // sum of the segment or if( ss >= sq && se <= qe)
            // qs--------------qe
            //     ss-------se

            if (qs <= ss && qe >= se)
            {
                return st[index];
            }

            //               qs-----------qe              qs------qe
            //     ss-----se                      or                  ss----- se

            // If segment of this node is outside the given range
            if (se < qs || ss > qe)
            {
                return 0;
            }

            // If a part of this segment overlaps with the given range
            int mid = (ss + se) / 2;
            return getSumUtil(st, ss, mid, qs, qe, 2 * index + 1) +
                   getSumUtil(st, mid + 1, se, qs, qe, 2 * index + 2);
        }

        // The function to update a value in input array and segment tree.
        // It uses updateValueUtil() to update the value in segment tree
        public void updateValue(int[] arr, int[] st, int n, int i, int new_val)
        {
            // Check for erroneous input index
            if (i < 0 || i > n - 1)
            {
                // printf("Invalid Input");
                return;
            }

            // Get the difference between new value and old value
            int diff = new_val - arr[i];

            // Update the value in array
            arr[i] = new_val;

            // Update the values of nodes in segment tree
            updateValueUtil(st, 0, n - 1, i, diff, 0);
        }


        /* A recursive function to update the nodes which have the given index in
        their range. The following are parameters
        st, index, ss and se are same as getSumUtil()
        i    --> index of the element to be updated. This index is in input array.
        diff --> Value to be added to all nodes which have i in range */
        public void updateValueUtil(int[] st, int ss, int se, int i, int diff, int index)
        {
            // Base Case: If the input index lies outside the range of this segment
            if (i < ss || i > se)
                return;

            // If the input index is in range of this node, then update the value
            // of the node and its children
            st[index] = st[index] + diff;
            if (se != ss)
            {
                int mid = (ss + se) / 2;
                updateValueUtil(st, ss, mid, i, diff, 2 * index + 1);
                updateValueUtil(st, mid + 1, se, i, diff, 2 * index + 2);
            }
        }
    }



}
