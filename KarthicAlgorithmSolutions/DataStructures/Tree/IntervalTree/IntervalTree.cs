using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.IntervalTree
{
    //Consider a situation where we have a set of intervals and we need following operations to be implemented efficiently. 
    //1) Add an interval
    //2) Remove an interval
    //3) Given an interval x, find if x overlaps with any of the existing intervals.
    //Then Interval Tree is the right sln. All the above operation is done in o(logn) time
    //Does my lifeguarding shift overlap with anyone else's shift?
    //If so, which one?
    //Given: I am a lifeguard from 9:00 to 12:00.
    //The other shifts are: 6:00 to 8:00, 10:00 to 13:00, 12:00 to 15:00 
    //and 14:00 to 17:00


    /*Logic
     * Interval tree Node contains Interval (low and High) and max value (max value is the max value that its subtree has)
     * We can build a regualr BST tree with IntervalTreeNode as datatype
     * Insertion and deleteion is same as bst we use the Interval.Low value to compare with the root.Interval.Low and decide whether to go in left or right subtree
     * Search has a special logic with the help pf Max property. 
     * We can do the regualr BST search with the Interval low property because we will miss a interval..take this eg
     *           All the time here are in am                           Check whether (9 am to 12 am) has conflict
     *              (8 am to 8.30 am)
     *     (6 am to 1 pm)        (10 am to 11 am)
     *     
     * Here in this we search my slot (9 am to 12 am) based on the start/low prop 9 am. I have to go the right of root because 9 am > 8am
     * But here the node is actually interval the left node of root has 6 am till 1 pm ..Though the start time is lesser than root, the end time range would be the conflict for the given
     * So we need to consider the max property of the node to compare the conflict
     * Insert, delete and search happens in o(log n) time
     * */
    public class KarthicIntervalTree<T> where T : IComparable<T>
    {
        public  IntervalTreeNode<T> Root { get; set; }
        public KarthicIntervalTree()
        {
            this.Root = null;
        }

        public IntervalTreeNode<T> InsertInterval(IntervalTreeNode<T> root, Interval<T> interval)
        {
            if (root == null)
            {
                root = new IntervalTreeNode<T>(interval, interval.High);
                return root;
            }

            //do binary search logic using interval.low..
            //Imagin interval.low is binary tree value and do the insert
            //interval.low < root.interval.low
            if (interval.Low.CompareTo(root.Interval.Low) <= 0)
            {
                //add the interval in the left subtree
                root.Left = InsertInterval(root.Left, interval);

            }
            else
            {
                //if the value is greater than root

                root.Right = InsertInterval(root.Right, interval);
            }

            //Important: we need to overide the max value of the ancestor
            // Update the max value of this ancestor if needed
            //if (root->max < i.high)
            //    root->max = i.high;
            if (root.Max.CompareTo(interval.High) < 0)
            {
                root.Max = interval.High;

            }
            return root;
        }

        public IntervalTreeNode<T> SearchInterval(IntervalTreeNode<T> root, Interval<T> interval)
        {
            // Base Case, tree is empty
            if (root == null)
            {
                return null;
            }

            //check whether overlap happens with the root
            if (IsOverlap(root.Interval, interval))
            {
                return root;
            }

            // If left child of root is present and max of left child is
            // greater than or equal to given interval, then i may
            // overlap with an interval is left subtree
            //note: we are comparing root.left max prop with linterval low prop
            //comparing high of root subtree to low
            if (root.Left != null && root.Left.Max.CompareTo(interval.Low) >= 0)
            {
                //then the interval should be in left subtree
                return SearchInterval(root.Left, interval);
            }
            else
            {
                //the interval should be on the right subtree
                return SearchInterval(root.Right, interval);
            }
        }


        //Given for two intervals to overlap the following condition should be tree
        //The given true may be in any order first>second or second > first in sorted order
        //two interval will overlap if the following two conditions are true
        //1) second.low <= first.high  and
        //2) first.low <= second.high   (the second condition won't need for sorted list) bcoz for sorted list s2 > s1 so obviously e2 > s1 but here we need to check whether e2 > s1 or s1 <= e2
        //ef (9.15,9.30) (9,10)
        public bool IsOverlap(Interval<T> first, Interval<T> second)
        {
            //if (second.Low <= first.High && first.Low <= second.High)
            if((second.Low.CompareTo(first.High) <= 0)  && (first.Low.CompareTo(second.High) <= 0))
            {
                return true;

            }
            return false;
           
        }
    }


    public class IntervalTreeNode<T>
    {

        public Interval<T> Interval { get; set; }
        public T Max { get; set; }
        public IntervalTreeNode<T> Left { get; set; }
        public IntervalTreeNode<T> Right { get; set; }

        public IntervalTreeNode(Interval<T> interval, T max)
        {
            this.Interval = interval;
            this.Max = max;
            this.Left = null;
            this.Right = null;
        }



    }

    public class Interval<T>
    {
        public T Low { get; set; }
        public T High { get; set; }

        public Interval()
        {
        }

        public Interval(T low, T high)
        {
            this.Low = low;
            this.High = high;
        }
    }
}
