using Puzzles;
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
    public partial class CandidateQuesPage1 : Form
    {
        public CandidateQuesPage1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int K = Convert.ToInt32(this.textBox3.Text);
            int[,] matrix = { {10, 20, 30, 40},
                    {15, 25, 35, 45},
                    {24, 29, 37, 48},
                    {32, 33, 39, 50},
                  };

            this.textBox4.Text = FindthekthSmalleseElementInMatrix(matrix, K).ToString();
        }

        //logic:
        //The matrix is sorted in both rows and column
        //so the first element will be the smallest.
        //build min heap from the first row..
        //heap node should have data, rowno, colno
        //get the min element in heap..
        //get the col no and get the nextrow on the same and update into the heap (we can do insert if we pop on previou step ) but update and heapify is better for this
        //get the next smallest
        //http://www.geeksforgeeks.org/kth-smallest-element-in-a-row-wise-and-column-wise-sorted-2d-array-set-1/

        public int FindthekthSmalleseElementInMatrix(int[,] matrix, int k)
        {
            MinHeap<MatrixElement> myheap = new MinHeap<MatrixElement>(new MinMatrixElementComparer());

            //add first row to minheap//all col in first row
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                myheap.Insert(new MatrixElement(matrix[0, col], 0, col));
            }

            // to find kth smallest..iterate k times
            for (int i = 1; i < k; i++)
            {
                //get the min
                MatrixElement minelement = myheap.Peek();
                //Find the next element in the next row of the same colmumn
                //check the next row is not ourside bounds
                if (minelement.Row + 1 <= matrix.GetLength(1) - 1)
                {
                    MatrixElement nextelement = new MatrixElement(matrix[minelement.Row + 1, minelement.Col], minelement.Row + 1, minelement.Col);

                    //we can update the heap root element and then call minheapify of the root
                    //my heap fon't prov that so
                    myheap.PopRoot();
                    myheap.Insert(nextelement); //this will make sure to have the smallest


                }
                else
                {
                    myheap.PopRoot();
                }

            }

            return myheap.Peek().Data;
        }

        public class MatrixElement
        {
            public int Data { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }

            public MatrixElement(int data, int row, int col)
            {
                this.Data = data;
                this.Row = row;
                this.Col = col;
            }
        }

        public class MinMatrixElementComparer : IComparer<MatrixElement>
        {
            int IComparer<MatrixElement>.Compare(MatrixElement x, MatrixElement y)
            {
                return y.Data.CompareTo(x.Data);
            }
        }

        public class MaxMatrixElementComparer : IComparer<MatrixElement>
        {
            int IComparer<MatrixElement>.Compare(MatrixElement x, MatrixElement y)
            {
                return x.Data.CompareTo(y.Data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int K = Convert.ToInt32(this.textBox1.Text);
            int[,] matrix = { {10, 20, 30, 40},
                    {15, 25, 35, 45},
                    {24, 29, 37, 48},
                    {32, 33, 39, 50},
                  };

            this.textBox2.Text = FindthekthLargestElementInMatrix(matrix, K).ToString();

        }

        public int FindthekthLargestElementInMatrix(int[,] matrix, int k)
        {

            MaxHeap<MatrixElement> myheap = new MaxHeap<MatrixElement>(new MaxMatrixElementComparer());

            //add last row to max heap//all col in first row
            int lastrow = matrix.GetLength(1) - 1;
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                myheap.Insert(new MatrixElement(matrix[lastrow, col], lastrow, col));
            }

            // to find kth smallest..iterate k times
            for (int i = 1; i < k; i++)
            {
                //get the min
                MatrixElement maxelement = myheap.Peek();
                //Find the next element in the next row of the same colmumn
                //check the next row is not ourside bounds
                if (maxelement.Row - 1 >= 0)
                {
                    MatrixElement nextelement = new MatrixElement(matrix[maxelement.Row - 1, maxelement.Col], maxelement.Row - 1, maxelement.Col);

                    //we can update the heap root element and then call minheapify of the root
                    //my heap fon't prov that so
                    myheap.PopRoot();
                    myheap.Insert(nextelement); //this will make sure to have the smallest


                }
                else
                {
                    myheap.PopRoot();
                }

            }

            return myheap.Peek().Data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=_H50Ir-Tves
            //http://www.geeksforgeeks.org/median-of-two-sorted-arrays-of-different-sizes/


            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);

            this.textBox8.Text = FindingMedianofSortedArray(array1, array2).ToString();

        }



        public int FindingMedianofSortedArray(int[] a, int[] b)
        {

            // If all elements of array 1 are smaller then
            // median is average of last element of ar1 and
            // first element of ar2
            if (a[a.Length - 1] < b[0])
            {
                return (a[a.Length - 1] + b[0]) / 2;
            }

            // If all elements of array 1 are smaller then
            // median is average of first element of ar1 and
            // last element of ar2
            if (b[b.Length - 1] < a[0])
            {
                return (b[b.Length - 1] + a[0]) / 2;
            }

            //if the code come here find the median

            return FindingMedianofSortedArraysHelper(a, b, 0, a.Length, 0, b.Length);
        }
        //logic:
        //We know that we can find an element in array at constant time
        //we use that logic to find the median
        //lets say you are given array1 of length m and array b of length n..then median is
        // let i be the index of the element in array a
        //let j be the index of the element in array b
        //median  = ( m + n)/2 
        // i + j = (m + n) /2  then i and j be the index and sum of these values of the index will be the median
        // then j =  (m + n )/2 - i
        //since m and n are same size
        // j = n - i
        //here is the condition
        //A[i] should be between B[j] and B[j+1]
        //B[j] <= A[i] <= b[J+1] then A[i] and b[j] will be the median 
        public int FindingMedianofSortedArraysHelper(int[] a, int[] b, int aL, int aR, int bL, int bR)
        {

            //get the middle index of a
            int middle = (aL + aR) / 2;

            int i = middle; //let i be the median of array a
            int n = b.Length; //size
            int j = (n-1) - i; //let j be the median of array b actually it is (m+n)/2 - i

            //if i and j are really median index of the these two array
            //a[i] is between b[j] and b[j+1]
            if (b[j] <= a[i] && a[i] <= b[j + 1])
            {
                return (a[i] + b[j]) / 2;
            }
            else if (a[i] < b[j]) //then obviously less b[j+1]
            {
                //search in the follwin region
                //second half of a and first half of b
                return FindingMedianofSortedArraysHelper(a, b, i + 1, aR, bL, j - 1);
            }
            //when a[i] is greater than both b[j] and b[j+1]
            //else if (a[i] > b[j] && a[i] > b[j+1])
            else
            {
                //first half of a and secon half of b
                return FindingMedianofSortedArraysHelper(a, b, aL, i - 1, j + 1, bR);

            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);

            this.textBox8.Text = FindMedianofSortedArrayByComparingMedian(array1, array2).ToString();
        }

        //Assumption: Given arrays are equal size
        public int FindMedianofSortedArrayByComparingMedian(int[] a, int[] b)
        {

            // If all elements of array 1 are smaller then
            // median is average of last element of ar1 and
            // first element of ar2
            if (a[a.Length - 1] < b[0])
            {
                return (a[a.Length - 1] + b[0]) / 2;
            }

            // If all elements of array 1 are smaller then
            // median is average of first element of ar1 and
            // last element of ar2
            if (b[b.Length - 1] < a[0])
            {
                return (b[b.Length - 1] + a[0]) / 2;
            }


            return FindMedianByComparison(a, 0, a.Length - 1, b, 0, b.Length - 1);

        }

        //Assumption: Given arrays are equal size
        public int FindMedianByComparison(int[] a, int aL, int aR, int[] b, int bL, int bR)
        {
            //base case when after divind when both a and b have one element

            int n1 = aR - aL + 1;
            int n2 = bR - bL + 1;
            if (n1 == 1 && n2 == 1)  //al and aR will be equal 
            {

                return (a[aL] + b[bL]) / 2;
            }
            //both are size 2
            else if (n1 == 2 && n2 == 2)
            {
                int middle1 = Math.Max(a[aL], b[bL]);
                int middle2 = Math.Min(a[aR], b[bR]);
                return (middle1 + middle2) / 2;
            }


            //test
            if (n1 != n2)
            {
                int test = n1;
            }
            //Regular case
            int median1 = FindMedian(a, aL, aR);
            int median2 = FindMedian(b, bL, bR);

            if (median1 == median2)
            {
                return median2;
            }
            else if (median1 > median2)
            {
                //shirnk the search to aL to middle
                //bMiddle to bR
                //here since both array is of samle length
                //eg for even case: a = 1,2,3,4  b = {5,6,7,8}
                //{3,4} and then {5,6} note here for even array we increment + 1 to search on right half
                if (n1 % 2 == 0)
                {
                    return FindMedianByComparison(a, aL, (aL + aR) / 2, b, ((bL + bR) / 2) + 1, bR); //even second half excludint center eg 5,6,7,8 = { 7, 8}
                }
                else
                {
                    return FindMedianByComparison(a, aL, (aL + aR) / 2, b, (bL + bR) / 2, bR);
                }

            }
            else
            {
                //median 1 < median2
                //search in second half of a and first half of b
                //here the simple fucking logic screwed my day
                //since the length of two array is same..we maintain both half size


                if (n1 % 2 == 0)
                {
                    return FindMedianByComparison(a, ((aL + aR) / 2) + 1, aR, b, bL, (bL + bR) / 2);
                }
                else
                {
                    return FindMedianByComparison(a, ((aL + aR) / 2), aR, b, bL, (bL + bR) / 2);
                }

            }
        }


        public int FindMedian(int[] array, int left, int right)
        {
            int length = right - left + 1;

            //if (length == 1)
            //{
            //    return array[left]; //this is also handled below
            //}

            if (length % 2 == 0)  // {1,2,3,4}
            {
                //even average of the middle elements
                return (array[(left + right) / 2] + array[((left + right) / 2) + 1]) / 2;
            }
            else
            {
                //odd middle element
                return array[(left + right) / 2];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);

            this.textBox8.Text = FindMedianofSortedArrayUnEqualSizeWrapper(array1, array2).ToString();
        }

        //This function makes sure that smaller array is passed as first argument to findMedianUtil
        public double FindMedianofSortedArrayUnEqualSizeWrapper(int[] array1, int[] array2)
        {
            if (array1.Length < array2.Length)
            {
                return FindMedianByComparisonUnEqualSize(array1, 0, array1.Length - 1, array2, 0, array2.Length - 1);
            }
            else
            {
                return FindMedianByComparisonUnEqualSize(array2, 0, array2.Length - 1, array1, 0, array1.Length - 1);
            }


        }

        //Assumption: Array a is greater than b

        public double FindMedianByComparisonUnEqualSize(int[] a, int aL, int aR, int[] b, int bL, int bR)
        {
            //  Base cases:
            //The smaller array has only one element
            //Case 1: N = 1, M = 1.
            //Case 2: N = 1, M is odd
            //Case 3: N = 1, M is even
            //The smaller array has only two elements
            //Case 4: N = 2, M = 2
            //Case 5: N = 2, M is odd
            //Case 6: N = 2, M is even

            int aSize = aR - aL + 1;
            int bSize = bR - bL + 1;

            int bmiddle = bSize / 2;
            if (aSize == 1)
            {
                 
                //Case 1: N = 1, M = 1.
                if (bSize == 1)
                {
                    return (a[aL] + b[bL]) / 2;
                }
                //Case 2: N = 1, M is odd
                else if (bSize % 2 != 0)
                {
                    // A = {9}, B[] = {5, 8, 10, 20, 30} and
                    // A[] = {1}, B[] = {5, 8, 10, 20, 30}

                    return GetMedianOfTwoIntegers(b[bmiddle], (int)GetMedianOfThreeIntergers(a[aL], b[bmiddle - 1], b[bmiddle + 1]));

                }
                else
                {
                    // Case 3: If the larger array has even number of element, then median
                    // will be one of the following 3 elements
                    // ... The middle two elements of larger array
                    // ... The only element of smaller array
                    return GetMedianOfThreeIntergers(a[aL], b[bmiddle], b[bmiddle - 1]);
                }
                
            }
            else if (aSize == 2)
            {
                //Case 4: N = 2, M = 2
                if (bSize == 2)
                {
                    return GetMedianofFourIntegers(a[aL], a[aR], b[bL], b[bR]);
                }
                else if (bSize % 2 != 0)
                {
                    // Case 5: If the larger array has odd number of elements, then median
                    // will be one of the following 3 elements
                    // 1. Middle element of larger array
                    // 2. Max of first element of smaller array and element just
                    //    before the middle in bigger array
                    // 3. Min of second element of smaller array and element just
                    //    after the middle in bigger array
                    return GetMedianOfThreeIntergers(b[bmiddle], GetMax(a[aL], b[bmiddle - 1]), GetMin(a[aR], b[bmiddle + 1]));

                }
                else
                {
                    // Case 6: If the larger array has even number of elements, then
                    // median will be one of the following 4 elements
                    // 1) & 2) The middle two elements of larger array
                    // 3) Max of first element of smaller array and element
                    //    just before the first middle element in bigger array
                    // 4. Min of second element of smaller array and element
                    //    just after the second middle in bigger array
                    return GetMedianofFourIntegers(b[bmiddle], b[bmiddle - 1], GetMax(a[aL], b[bmiddle - 2]), GetMin(a[aR], b[bmiddle + 1]));
                }
            }


            //Regular case
            int median1 = FindMedian(a, aL, aR);
            int median2 = FindMedian(b, bL, bR);

            if (median1 == median2)
            {
                return median2;
            }
            else if (median1 > median2)
            {
                //shirnk the search to aL to middle
                //bMiddle to bR
                //here since both array is of samle length
                //eg for even case: a = 1,2,3,4  b = {5,6,7,8}
                //{3,4} and then {5,6} note here for even array we increment + 1 to search on right half
                if (bSize % 2 == 0) //since m2 is lesser that will be moving to sec half
                {
                    //when you increment middle make sure it is within bounds
                    int nexttomiddle = (bL + bR) / 2 == b.Length -1 ? (bL + bR) / 2 : ((bL + bR) / 2) + 1;
                    return FindMedianByComparison(a, aL, (aL + aR) / 2, b, nexttomiddle, bR); //even second half excludint center eg 5,6,7,8 = { 7, 8}
                }
                else
                {
                    return FindMedianByComparison(a, aL, (aL + aR) / 2, b, (bL + bR) / 2, bR);
                }

            }
            else
            {
                //median 1 < median2
                //search in second half of a and first half of b
                //here the simple fucking logic screwed my day
                //since the length of two array is same..we maintain both half size


                if (bSize % 2 == 0) //since m1 is lesser that will be moving to sec half
                {
                    //when you increment middle make sure it is within bounds
                    int nexttomiddle = (aL + aR) / 2 == a.Length - 1 ? (aL + aR) / 2 : ((aL + aR) / 2) + 1;
                    return FindMedianByComparison(a, nexttomiddle, aR, b, bL, (bL + bR) / 2);
                }
                else
                {

                    return FindMedianByComparison(a, ((aL + aR) / 2), aR, b, bL, (bL + bR) / 2);
                }

            }
        }


        private int GetMax(int a, int b)
        {
            return  (a > b) ? a : b;
        }

        private int GetMin(int a, int b)
        {
            return (a < b) ? a : b;
        }

        // A utility function to find maximum of two integers
        private double GetMedianOfTwoIntegers(int a, int b)
        {
            return (double)(a + b) / 2;
        }

        //// A utility function to find median of three integers
        private double GetMedianOfThreeIntergers(int a, int b, int c)
        {
            int min = GetMin(a, GetMin(b, c));
            int max = GetMax(a, GetMax(b, c));

            return a + b + c - min - max;
        }

        // A utility function to find median of four integers
        private double GetMedianofFourIntegers(int a, int b, int c, int d)
        {
            int min = GetMin(a, GetMin(b, GetMin(c,d)));
            int max = GetMax(a, GetMax(b, GetMax(c,d)));

            return (double)(a + b + c + d - min - max) / 2;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //http://articles.leetcode.com/2011/01/find-k-th-smallest-element-in-union-of.html
            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox10.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox9.Text);
            int k = Convert.ToInt32(this.textBox11.Text);
            //int smallest = kthSmallestMethod2(array1, array2, k);
            //update: 6/16/2015 see below method
            int smallest = FindKthSmallestUsingInvariant(array1, 0, array1.Length - 1, array2, 0, array2.Length - 1, k);
            this.textBox7.Text = smallest.ToString();

        }


        //funcking confusing
        private int kthSmallest(int[] A, int[] B, int k, int aL, int aR, int bL, int bR)
        {
            if (aL == aR) return B[bL + k];
            if (bL == bR) return A[aL + k];
            else
            {
                int midA = (aL + aR) / 2;
                int midB = (bL + bR) / 2;
                int lenA = midA - aL;
                int lenB = midB - bL;

                //                Maintaining the invariant
                //i + j = k – 1,
                if (A[midA] <= B[midB])
                {
                    if (k <= lenA + lenB)
                    {
                        return kthSmallest(A, B, k, aL, aR, bL, midB);
                    }
                    else
                    {
                        //here pass the first half of the array
                        return kthSmallest(A, B, k - lenA - 1, midA + 1, aR, bL, bR);
                    }
                }
                else
                {
                    if (k <= lenA + lenB)
                        return kthSmallest(A, B, k, aL, midA, bL, bR);
                    else
                        return kthSmallest(A, B, k - lenB - 1, aL, aR, midB + 1, bR);
                }
            }
        }

        public static int kthSmallestMethod2(int[] A, int[] B, int k)
        {
            if (A.Length == 0 || B.Length == 0 || k > A.Length + B.Length)
            {
                //  throw new IllegalArgumentException();
            }
            return kthSmallestEasy(A, 0, A.Length, B, 0, B.Length, k);
        }


        //http://articles.leetcode.com/2011/01/find-k-th-smallest-element-in-union-of.html

        protected static int kthSmallestEasy(int[] A, int aLow, int aLength, int[] B, int bLow, int bLength, int k)
        {
            //Error Handling
            //assert(aLow >= 0); assert(bLow >= 0);
            //assert(aLength >= 0); assert(bLength >= 0); assert(aLength + bLength >= k);

//            and also why do they calculate i as (int)((double)m / (m+n) * (k-1)).

//This gives an estimate of the new half-way point assuming an equidistribution of values between the known points.
            int i = (int)((double)((k - 1) * aLength / (aLength + bLength)));
            int j = k - 1 - i;

            int Ai_1 = aLow + i == 0 ? Int32.MinValue : A[aLow + i - 1];
            int Ai = aLow + i == A.Length ? Int32.MaxValue : A[aLow + i];

            int Bj_1 = bLow + j == 0 ? Int32.MinValue : B[bLow + j - 1];
            int Bj = bLow + j == B.Length ? Int32.MaxValue : B[bLow + j];

            if (Bj_1 < Ai && Ai < Bj)
                return Ai;
            else if (Ai_1 < Bj && Bj < Ai)
                return Bj;

            //assert(Ai < Bj - 1 || Bj < Ai_1);

            if (Ai < Bj_1) // exclude A[aLow .. i] and A[j..bHigh], k was replaced by k - i - 1
                return kthSmallestEasy(A, aLow + i + 1, aLength - i - 1, B, bLow, j, k - i - 1);
            else // exclude A[i, aHigh] and B[bLow .. j], k was replaced by k - j - 1
                return kthSmallestEasy(A, aLow, i, B, bLow + j + 1, bLength - j - 1, k - j - 1);
        }


        // Note: Read the logic here http://articles.leetcode.com/2011/01/find-k-th-smallest-element-in-union-of.html. It's easy
        // IF Ai is between Bj-1 and Bj then Ai is the i+j +1 smallest element
        
        //We try to approach this tricky problem by comparing middle elements of A and B, which we identify as Ai and Bj. 
        // If Ai is between Bj and Bj-1, we have just found the i+j+1 smallest element. 
        // Why? Therefore, if we choose i and j such that i+j = k-1, we are able to find the k-th smallest element. This is an important invariant that we must maintain for the correctness of this algorithm.
        private static int FindKthSmallestUsingInvariant(int[] a, int aLow, int aHigh, int[] b, int bLow, int bHigh, int k)
        {
            int aLength = aHigh - aLow + 1;
            int bLength = bHigh - bLow + 1;

            //Maintaining the invariant i + j = k – 1,
            //This gives an estimate of the new half-way point assuming an equidistribution of values between the known points.
            int invariantA = (int)((double)((k - 1) * aLength / (aLength + bLength)));
            int invariantB = k - 1 - invariantA;
            //important here i and j are not the correst index...there are the index based on the forumula 
            //if the aLength = 2 it eg aLow = 3, aHigh = 5 then length = 3...the forumal might give i as 1 or 2
            //so Ai is not A[i] here it is A[aLow + i]

            int i = aLow + invariantA;  //Here i is the index of array 1
            int j = bLow + invariantB;  //here j is the index of array 2

            int Ai = (i == aLength) ? Int32.MaxValue : a[i];
            int Bj = (j == bLength) ? Int32.MaxValue : b[j];
            //Ai_1 is array a minus 1
            int Ai_1 = (i == 0) ? Int32.MinValue : a[i - 1];
            int Bj_1 = (j == 0) ? Int32.MinValue : b[j - 1];

            //If Bj-1 < Ai < Bj, then Ai must be the k-th smallest,
           //or else if Ai-1 < Bj < Ai, then Bj must be the k-th smallest.

            if (Bj_1 < Ai && Ai < Bj)
            {
                return Ai;
            }
            else if (Ai_1 < Bj && Bj < Ai)
            {
                return Bj;
            }

            // if none of the cases above, then it is either:
            if (Ai < Bj)
            {
                // exclude Ai and below portion
                // exclude Bj and above portion
                return FindKthSmallestUsingInvariant(a, i + 1, aHigh, b, bLow, j - 1, (k - i - 1));
            }
            else /* Bj < Ai */
            {
                // exclude Ai and above portion
                // exclude Bj and below portion
                return FindKthSmallestUsingInvariant(a, aLow, i - 1, b, j + 1, bHigh, k - j - 1);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Implemenataion. Deep copy a graph
            KarthicGraph<int> graph = new KarthicGraph<int>();

            TreeHelper.SetUpUnDirectedGraph(graph);

            KarthicGraph<int> deepcopiedgraph = DeepCopyGraph(graph);
        }


        public KarthicGraph<int> DeepCopyGraph(KarthicGraph<int> graph)
        {

            Hashtable ht = new Hashtable();
            GraphNode<int> root = graph.Root;

            KarthicGraph<int> copygraph = new KarthicGraph<int>();

            Queue<GraphNode<int>> myqueue = new Queue<GraphNode<int>>();
            myqueue.Enqueue(root);
            //hashtable will have original node as key and copied node as data
            GraphNode<int> copynode = new GraphNode<int>(root.Data);
            ht.Add(root, copynode);
            copygraph.Root = copynode;


            while (myqueue.Count != 0)
            {
                GraphNode<int> parent = myqueue.Dequeue();

                foreach (GraphNode<int> child in parent.Neighbors)
                {
                    //make sure you check for exists..
                    //if exists means already added/visited etc..
                    //this will catch the undirected structre a -> b and b ->a
                    if (!ht.ContainsKey(child))
                    {
                        GraphNode<int> childcopy = new GraphNode<int>(child.Data);
                        
                        ht.Add(child, childcopy);
                        myqueue.Enqueue(child);
                    }

                    //get the copy of the parent and add the created childcopy to its neighbor
                    ((GraphNode<int>)ht[parent]).Neighbors.Add((GraphNode<int>)ht[child]);
                    //else
                    //{
                    //    //here it mean the child is alredy visited and added to ht by its parent
                    //    //unidirection ..we are adding only the copy
                    //    ((GraphNode<int>)ht[child]).Neighbors.Add((GraphNode<int>)ht[parent]);
                    //    //we don't add to queue to prevent loop
                    //}
                }
            }

            return copygraph;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            //http://www.geeksforgeeks.org/tree-isomorphism-problem/
            //see the below code
            //Two trees are called isomorphic if one of them can be obtained from other by a series of flips, i.e. by swapping left and right children of a number of nodes. Any number of nodes at any level can have their children swapped. Two empty trees are isomorphic.

        }

        /* Given a binary tree, print its nodes in reverse level order */
        bool isIsomorphic(KarthicBTNode<int> n1, KarthicBTNode<int> n2)
        {
            // Both roots are NULL, trees isomorphic by definition
            if (n1 == null && n2 == null)
                return true;

            // Exactly one of the n1 and n2 is NULL, trees not isomorphic
            if (n1 == null || n2 == null)
                return false;

            if (n1.Data != n1.Data)
                return false;

            // There are two possible cases for n1 and n2 to be isomorphic
            // Case 1: The subtrees rooted at these nodes have NOT been "Flipped".
            // Both of these subtrees have to be isomorphic, hence the &&
            // Case 2: The subtrees rooted at these nodes have been "Flipped"
            return
            (isIsomorphic(n1.Left, n2.Left) && isIsomorphic(n1.Right, n2.Right)) ||
            (isIsomorphic(n1.Left, n2.Right) && isIsomorphic(n1.Right, n2.Left));
        }

        private void button9_Click(object sender, EventArgs e)
        {

//            Example: 
//0 1 
//2 1 
//0 2 
//4 1 

//Answer: 
//1
            //Form a graph node with the above vertices or form edges
            GraphNode<int> node0 = new GraphNode<int>(0);
            GraphNode<int> node1 = new GraphNode<int>(1);
            GraphNode<int> node2 = new GraphNode<int>(2);
            GraphNode<int> node4 = new GraphNode<int>(4);

            List<Edge<int>> edges = new List<Edge<int>>();
            edges.Add(new Edge<int>(node0, node1));
            edges.Add(new Edge<int>(node2, node1));
            edges.Add(new Edge<int>(node0, node2));
            edges.Add(new Edge<int>(node4, node1));
       

            int noofTriangles = getNumberOfTriangles(edges);
           

        }


    //logicFirst approach - A naive approach using an adjacency map 

  //The adjacency map is a Map whose keys are vertices and whose values are sets of vertices which are all the neighbors of the key vertex.
  //logic is here: For every vertex, we'll check for every pair of its neighbors whether there is an edge between them and increment the triangle counter if so. 

//The total number of triangles will be the number of triangles we counted divided by 6 (we count each triangle 6 times). 
        //Complexity: The overall run-time complexity is O(n*d^2) where n is the number of vertices and d is the maximum degree of a vertex in the graph.
        
        //This is a good approach for graphs with small maximum vertex degree. But if the graph contains a vertex whose degree is O(n) 
        //then the overall complexity in this case would be O(n^3)

      
       public int getNumberOfTriangles(List<Edge<int>> edges){

           int triangle = 0;
           Dictionary<GraphNode<int>,List<GraphNode<int>>> ht = BuildAdjacencyMap(edges);

           //getting all values
           foreach(List<GraphNode<int>> valueslist in ht.Values)
           {
               //childrens of some parent
                foreach(GraphNode<int> node1 in valueslist)
                {
                    //checking whether there is edge btwn childrens
                    foreach(GraphNode<int> node2 in valueslist)
                    {
                         if((node1 != node2) && ht[node1].Contains(node2))
                         {
                             triangle++;
                         }
                    }
                }

           }

           return triangle/6;
       }

        public  Dictionary<GraphNode<int>,List<GraphNode<int>>> BuildAdjacencyMap(List<Edge<int>> edges)
        {
            Dictionary<GraphNode<int>,List<GraphNode<int>>> ht = new Dictionary<GraphNode<int>,List<GraphNode<int>>>();
            //key  Graphnode value List of its neighbours
            foreach(Edge<int> edge in edges)
            {
                  if(!ht.ContainsKey(edge.from))
                  {
                      ht.Add(edge.from, new List<GraphNode<int>>());
                  }
                  if(!ht.ContainsKey(edge.to))
                  {
                      ht.Add(edge.to, new List<GraphNode<int>>());
                  }

                  ht[edge.from].Add(edge.to);
                  ht[edge.to].Add(edge.from);
            }

            return ht;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Second approach - Using matrix multiplication 

            //Suppose A is the graph's adjacency matrix (A[i][j] = 1 if and only if there is an edge between i and j in the graph).
            //It can be shown that trace(A^3)/6 is the number of triangles in the graph
            //(using the fact that A^k[i][j] is the number of paths with k edges from i to j). This means that all we need to know the number of triangles is to calculate the matrix A^3 and its trace. 

            //This means that our algorithm complexity would depend on the complexity of the matrix multiplication algorithm: 
            //Naive: O(n^3) 
            //Strassen: O(n^{2.8074}) 
            //Coppersmith-Winograd: O(n^{2.3729}) 


            //create adjacency matrix
            //A[i,j] = 1 if there is an edge btw i and j
            //find the number of paths in the matrix
            //A^k[i,j] is the number of paths with k edges from i to j
            ////It can be shown that trace(A^3)/6 is the number of triangles in the graph
            //source: http://www.careercup.com/question?id=5988741646647296

            //            public static int getTriangles(int[][] adjMatrix) {
            //        //initialize from adjacency matrix the nodes that can be reached with one step:
            //        Connections con = new Connections(adjMatrix); 
            //        con.multiply(adjMatrix);        //nodes reached in exactly two steps
            //        con.multiply(adjMatrix);        //nodes reached in exactly three steps
            //        return con.getSumPolygons();    
            //    }

            //class Connections {
            //    public int steps; //keep track of number of steps 
            //    public int[][] m; //matrix that holds all connections after exactly steps

            //    public Connections(int[][] m) {
            //        this.m = Arrays.copyOf(m, m.length);  
            //        this.steps = 1;
            //    }

            //    public void multiply(int[][] f) {  //matrix multiplication
            //        int[][] n = new int[m.length][m.length];  
            //        for(int i = 0; i < m.length; i++) {
            //            for(int j = 0; j < m.length; j++) {
            //                for(int k = 0; k < m.length; k++ ) {
            //                    n[i][j] += f[i][k] * m[k][j];
            //                }
            //            }
            //        }
            //        steps++;   //increment number of steps used
            //        m = n;     //assign result of multiplication to class attribute
            //    }

            //    public int getSumPolygons() {
            //        int sum = 0; 
            //        for(int i = 0; i < m.length; i++) {
            //            //m[i][i] holds the number of paths that node i can be reached in s steps in both directions
            //            sum += m[i][i];
            //        }
            //        //divide by steps and 2 to avoid counting all nodes in path in both directions:
            //        return sum/steps/2; 
            //    }
            //}



            //            Example: 
            //0 1 
            //2 1 
            //0 2 
            //4 1 

            //Answer: 
            //1
            //Form a graph node with the above vertices or form edges
            GraphNode<int> node0 = new GraphNode<int>(0);
            GraphNode<int> node1 = new GraphNode<int>(1);
            GraphNode<int> node2 = new GraphNode<int>(2);
            GraphNode<int> node4 = new GraphNode<int>(4);

            List<Edge<int>> edges = new List<Edge<int>>();
            edges.Add(new Edge<int>(node0, node1));
            edges.Add(new Edge<int>(node2, node1));
            edges.Add(new Edge<int>(node0, node2));
            edges.Add(new Edge<int>(node4, node1));


            int noofTriangles = getNumberOfTrianglesUsingMatrixMultiplication(edges);

        }


        private int getNumberOfTrianglesUsingMatrixMultiplication(List<Edge<int>> edges)
        {
            int triangle = 0;
            Dictionary<GraphNode<int>, List<GraphNode<int>>> ht = BuildAdjacencyMap(edges);
            
            //Get all unique vertices
            List<GraphNode<int>> vertices = new List<GraphNode<int>>();
            foreach (KeyValuePair<GraphNode<int>, List<GraphNode<int>>> pair in ht)
            {
                vertices.Add(pair.Key);
            }

            int[,] adjacencymatrix = BuildAdjacencyMatrix(vertices, ht);

            Connections matrixconn = new Connections(adjacencymatrix);
            matrixconn.multiply(adjacencymatrix);
            matrixconn.multiply(adjacencymatrix);

            int value =  matrixconn.getSumPolygons();

            return value;

        }

        private int[,] BuildAdjacencyMatrix(List<GraphNode<int>> array, Dictionary<GraphNode<int>, List<GraphNode<int>>> ht)
        {
            int[,] matrix = new int[array.Count, array.Count];

            for(int row =0; row < array.Count; row++)
            {
                for(int col =0; col < array.Count; col++)
                {
                    if(row == col)
                    {
                        matrix[row,col] = 0;
                    }
                    else
                    {
                        //all vertices should be there in matrix
                        GraphNode<int> rownode = array[row];
                        GraphNode<int> colnode = array[col];

                        if (ht[rownode].Contains(colnode) && ht[colnode].Contains(rownode))
                        {
                            matrix[row, col] = 1;
                        }
                        else
                        {
                            matrix[row, col] = 0;
                        }
                    }
                }
            }

            return matrix;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);

            this.textBox8.Text = FindMedianOfSortedArray(array1, array2).ToString();
        }

        //Time Complexity: 0(n)
        //Assumption: Both array is of same size
        //This won't work if the array is unequal size
        public double FindMedianOfSortedArray(int[] array1, int[] array2)
        {
            int i = 0;  /* Current index of i/p array ar1[] */
            int j = 0; /* Current index of i/p array ar2[] */
            int m1 = -1, m2 = -1;
            int total = (array1.Length + array2.Length);
            int middle =  total/ 2;

            //For Odd number 1,2,3,4,5
            //Median is the middle of the element


            //for Even
            /* Since there are 2n elements, median will be average
             of elements at index n-1 and n in the array obtained after
             merging ar1 and ar2 */

            //find n and n-1 so iterate till n
            for (int count = 0; count <= middle; count++)
            {
                /*Below is to handle case where all elements of ar1[] are
                  smaller than smallest(or first) element of ar2[]*/
                if (i == middle)
                {
                    m1 = m2;
                    m2 = array2[0];
                    break;
                }

                /*Below is to handle case where all elements of ar2[] are
                  smaller than smallest(or first) element of ar1[]*/
                else if (j == middle)
                {
                    m1 = m2;
                    m2 = array1[0];
                    break;
                }

                if (array1[i] < array2[j])
                {
                    m1 = m2;  /* Store the prev median */
                    m2 = array1[i];
                    i++;
                }
                else
                {
                    m1 = m2;  /* Store the prev median */
                    m2 = array2[j];
                    j++;
                }
            }

            return (total % 2 == 0) ? (m1 + m2) / 2 : m2;
            //return (m1 + m2) / 2;

            //return (array1[i-1] + array2[j-1]) / 2;


        }

        private void button12_Click(object sender, EventArgs e)
        {
            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox10.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox9.Text);
            int k = Convert.ToInt32(this.textBox11.Text);
            int smallest = FindKthSmallestNaiveApproachWrapper(array1, array2, k);
            this.textBox7.Text = smallest.ToString();
        }


        public int FindKthSmallestNaiveApproachWrapper(int[] A, int[] B, int k)
        {
            if (A.Length == 0 || B.Length == 0 || k > A.Length + B.Length)
            {
                //  throw new IllegalArgumentException();
            }
            return FindKthSmallestNaiveApproach(A, B, k);
        }

        private int FindKthSmallestNaiveApproach(int[] array1, int[] array2, int k)
        {
            //we have to iterate k no of times irrespective of the array size
            int count = 0;
            int i =0;
            int j =0;
            bool IsArray1Last = false;

   
                while (count < k && i < array1.Length && j < array2.Length)
                {
                    if (array1[i] < array2[j])
                    {
                        i++;
                        IsArray1Last = true;
                    }
                    else
                    {
                        j++;
                        IsArray1Last = false;
                    }

                    count++;
                }

                while (count < k  && i < array1.Length)
                {
                    i++;
                    count++;
                    IsArray1Last = true;
                }

                while (count < k && j < array2.Length)
                {
                    j++;
                    count++;
                    IsArray1Last = false;
                }
     

            //We reached k iteration..find which array has the last pointer
            return (IsArray1Last == true) ? array1[i - 1] : array2[j - 1];
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] array1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox10.Text);
            int[] array2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox9.Text);
            int k = Convert.ToInt32(this.textBox11.Text);
            int smallest = FindKthSmallestUsingInvariant(array1, 0, array1.Length -1, array2, 0, array2.Length -1, k);
            this.textBox7.Text = smallest.ToString();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            //http://algorithmsandme.com/2014/12/find-kth-smallest-element-in-two-sorted-arrays/
        }



    //public int GetMin(int a, int b)
    //{
    //    return (a < b) ? a :b;
    //}
     
//      public int find_kth(int[] a, int[] b, int size_a, int size_b, int k){
//        /* to maintain uniformaty, we will assume that size_a is smaller than size_b
//                else we will swap array in call :) */
//        if(size_a > size_b)
//                return find_kth(b, a, size_b, size_a, k);
//        /* Now case when size of smaller array is 0 i.e there is no elemt in one array*/
//        if(size_a == 0 && size_b >0)
//                return b[k-1]; // due to zero based index
//        /* case where K ==1 that means we have hit limit */
//        if(k ==1)
//                return min(a[0], b[0]);
 
//        /* Now the divide and conquer part */
//        int i =  min(size_a, k/2) ; // K should be less than the size of array  
//        int j =  min(size_b, k/2) ; // K should be less than the size of array  
 
//        if(a[i-1] > b[j-1])
//                // Now we need to find only K-j th element
//                return find_kth(a, b+j, i, size_b -j, k-j);
//        else
//                return find_kth(a+i, b, size_a-i, j,  k-i);
 
//        return -1;
//}

        //Map<Object,Set<Object>> graph = buildAdjacencyMap(edges);
		
        //int triangles = 0;
        //for (Set<Object> neighbors : graph.values()){
        //    for (Object v2 : neighbors){
        //        for (Object v3 : neighbors){
        //            if ((!v2.equals(v3)) && (graph.get(v2).contains(v3))){
        //                triangles++;
        //            }
        //        }
        //    }
        //}
		
        //return (triangles/6);
	}

    }

    public class Edge<T>
    {
        public GraphNode<T> from;
        public GraphNode<T> to;

        public Edge(GraphNode<T> from, GraphNode<T> to)
        {
            this.from = from;
            this.to = to;

            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }
    }


    public class Connections
    {
        public int steps; //keep track of number of steps 
        public int[,] m; //matrix that holds all connections after exactly steps

        public Connections(int[,] array)
        {
            m = new int[array.GetLength(0), array.GetLength(1)];
            Array.Copy(array, this.m, array.Length);
            this.steps = 1;
        }

        public void multiply(int[,] f) {  //matrix multiplication
            int[,] n = new int[m.GetLength(0),m.GetLength(1)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    for (int k = 0; k < m.GetLength(1); k++)
                    {
                        n[i,j] += f[i,k] * m[k,j];
                    }
                }
            }
            steps++;   //increment number of steps used
            m = n;     //assign result of multiplication to class attribute
        }

        public int getSumPolygons()
        {
            int sum = 0;
            for (int i = 0; i < m.GetLength(1); i++)
            {
                //m[i][i] holds the number of paths that node i can be reached in s steps in both directions
                sum += m[i,i];
            }
            //divide by steps and 2 to avoid counting all nodes in path in both directions:
            return sum / steps / 2;
        }
    }

