using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Array
{
 

    //Logic: Maintain the maxsum value and sum value
    //remember to reset sum to zero when the sum value is lesser than 0 bcoz when the negative value comes as sum..if we add to another negative worse sum or to positive still lesser than positive value
    public class SubArraySum
    {
        //Kadane’s algorithm for 1D array
        public static int maxsubArray(int[] array)
        {
            int maxsum = Int32.MinValue;
            int runningsum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                runningsum = runningsum + array[i];

                if (runningsum > maxsum)
                {
                    maxsum = runningsum;
                }

                //reset sum to 0 when the sum value is negative
                if (runningsum < 0)
                {
                    runningsum = 0;
                }
            }

            return maxsum;
        }

        //Kadane’s algorithm for 1D array
        public static SubArrayResult maxsubArrayWithIndex(int[] array)
        {
            int maxsum = Int32.MinValue;
            int runningsum = 0;
            List<int> runningindexlist = new List<int>();
            List<int> maxindexlist = null;
            SubArrayResult result = null;

            for (int i = 0; i < array.Length; i++)
            {
                runningsum = runningsum + array[i];
                runningindexlist.Add(i);

                if (runningsum > maxsum)
                {
                    maxsum = runningsum;
                    maxindexlist = new List<int>(runningindexlist);
                }

                //reset sum to 0 when the sum value is negative
                if (runningsum < 0)
                {
                    runningsum = 0;
                    runningindexlist.Clear();
                }
            }

            if (maxindexlist.Count > 0)
            {
                result = new SubArrayResult();
                result.Sum = maxsum;
                result.StartIndex = maxindexlist[0];
                result.EndIndex = maxindexlist[maxindexlist.Count - 1];
            }

            return result;
        }


        //just playing for fun
        public static SubArrayResult maxsubArrayWithIndexMethod2(int[] array)
        {
            SubArrayResult result = new SubArrayResult();


            int maxsum = Int32.MinValue;
            int runningsum = 0;
            StringBuilder sbIndexrunning = new StringBuilder();
            StringBuilder maxIndexstring = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                runningsum = runningsum + array[i];
                sbIndexrunning.Append(i).Append(',');

                if (runningsum > maxsum)
                {
                    maxsum = runningsum;
                    maxIndexstring = sbIndexrunning;
                }

                if (runningsum < 0)
                {
                    runningsum = 0;
                    sbIndexrunning.Clear();
                }
            }

            if (maxIndexstring.Length > 0)
            {
            result.Sum = maxsum;
            string[] maxvalues = maxIndexstring.ToString().TrimEnd(',').Split(',');
            result.StartIndex = Convert.ToInt32(maxvalues[0]);
            result.EndIndex = Convert.ToInt32(maxvalues[maxvalues.Length - 1]);
            }

            return result;
        }

        public class SubArrayResult
        {
            public int Sum { get; set; }
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }

        }

        public class MatrixSubArrayResult
        {
            public int Sum { get; set; }
            public int RowStart { get; set; }
            public int RowEnd { get; set; }
            public int ColStart { get; set; }
            public int ColEnd { get; set; }

        }

        //we gonna use kadane's 1 d to 2d matrix
        //http://ihaventyetdecided.blogspot.com/2010/10/kadanes-2d-algorithm.html
        //http://www.geeksforgeeks.org/dynamic-programming-set-27-max-sum-rectangle-in-a-2d-matrix/


        public static int MaxSubMatrix(int[,] matrix)
        {
            int rowcount = matrix.GetLength(0);
            int colcount = matrix.GetLength(1);
            int[] partialsum = new int[colcount];  //size of colum
            int maxsum = Int32.MinValue;
           // 1 2 -1
           //-3 -1 -4
           // 1 -5 2
           // get all possible rows..
            //Now in this 3 x 3 matrix, the row can be grouped as [1],[1,2],[1,2,3],[2],[2,3],[3].

            for (int rowstart = 0; rowstart < rowcount; rowstart++)
            {

                ClearArray(partialsum);
                  
                for (int rowend = rowstart; rowend < rowcount; rowend++)
                {

                    //here we will get all possible row grouping
                    //For the given row range ( 1 to 1 or 1 -3) we sum the col value 
                    //p[0] will be the sum of first col
                    //p[1] will be the sume of secon col
                    //p[columcount -1] 
                    //Important note: here we don't clear the array in this iteration
                    //becoze row of 1 result will be added to row of 2 and then three..
                    //we clear it only after the  master loop value changes
                    for (int i = 0; i < colcount; i++)
                    {
                        //This is an important piece..what we do is
                        //for [1 t0 1] row p[0] = 1 p[1] = 2 p[2] = -1
                        partialsum[i] = partialsum[i] + matrix[rowend, i];

                    }

                    //here we can play with co-ordinates
                     int sum = maxsubArray(partialsum);
                     if (sum > maxsum)
                     {
                         maxsum = sum;
                     }

                }
            }

            return maxsum;
           

        }


        public static MatrixSubArrayResult MaxSubMatrixWithIndex(int[,] matrix)
        {
            int rowcount = matrix.GetLength(0);
            int colcount = matrix.GetLength(1);
            int[] partialsum = new int[colcount];  //size of colum
            int maxsum = Int32.MinValue;
            MatrixSubArrayResult result = new MatrixSubArrayResult();

            //test
            List<String> test = new List<string>();

            // 1 2 -1
            //-3 -1 -4
            // 1 -5 2
            // get all possible rows..
            //Now in this 3 x 3 matrix, the row can be grouped as [1],[1,2],[1,2,3],[2],[2,3],[3].

            for (int rowstart = 0; rowstart < rowcount; rowstart++)
            {

                ClearArray(partialsum);

                for (int rowend = rowstart; rowend < rowcount; rowend++)
                {

                    //here we will get all possible row grouping
                    //For the given row range ( 1 to 1 or 1 -3) we sum the col value 
                    //p[0] will be the sum of first col
                    //p[1] will be the sume of secon col
                    //p[columcount -1] 
                    //Important note: here we don't clear the array in this iteration
                    //becoze row of 1 result will be added to row of 2 and then three..
                    //we clear it only after the  master loop value changes
                    for (int i = 0; i < colcount; i++)
                    {
                        //This is an important piece..what we do is
                        //for [1 t0 1] row p[0] = 1 p[1] = 2 p[2] = -1
                        partialsum[i] = partialsum[i] + matrix[rowend, i];

                    }

                    //StringBuilder sb = new StringBuilder();
                    ////testing
                    //for (int j = 0; j < partialsum.Length; j++)
                    //{
                    //    sb.Append(partialsum[j]).Append(',');
                    //}
                    //test.Add(sb.ToString());
                    //here we can play with co-ordinates
                    


                    SubArrayResult infores = maxsubArrayWithIndex(partialsum);
                    if (infores != null && infores.Sum > maxsum)
                    {
                        //maxsum = sum;
                        maxsum = infores.Sum;

                        result.Sum = infores.Sum;
                        result.RowStart = rowstart;
                        result.RowEnd = rowend;
                        result.ColStart = infores.StartIndex;
                        result.ColEnd = infores.EndIndex;
                    }

                }
            }

            return result;


        }
        public static void ClearArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }

        }

    }
}
