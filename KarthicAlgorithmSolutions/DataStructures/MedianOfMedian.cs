using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures
{
   public class MedianOfMedian
    {
        public int[] input { get; set; }

        public MedianOfMedian(int[] input)
        {
            this.input = input;
        }

        // http://www.geeksforgeeks.org/kth-smallestlargest-element-unsorted-array-set-3-worst-case-linear-time/
        // https://interviewalgorithm.wordpress.com/sortingordering/median-of-medians-algorithm/

        // 5,2,17,13,14,17,19,14,18,2,4,8,2,5,0,6,13,4,12,18,10 

        // Logic

       //Median Of Medians algorith
       //1. Divide the list into sublists of length five. (Note that the last sublist may have length less than five.)
       //2. Sort each sublist and determine its median directly.
       //3. Use the median of medians algorithm to recursively determine the median of the set of all medians from the previous step.
       //4. Use the median of the medians from step 3 as the pivot.

         
        //1. Select a pivot element based on Median Of Medians algorithm (as above)
        //2. Rearrange numbers such that, elements to the left of pivot and smaller and elements to the right are greater than the pivot.
        //3. Let pivotIndex be the index of the pivot.
        //4. If pivotIndex == arr.length/2,  we have found the median, return pivotIndex.
        //5. else if pivotIndex < arr.length/2,  recursively search for Median in the left subArray, arr[start……pivotIndex-1]
        //6. else if pivotIndex > arr.length/2,  recursively search for Median in the right subArray, arr[pivotIndex+1…end]

        public int FindMiddleElement(int[] array, int low, int high)
        {
            int medianOfMedian = FindMedianOfMedian(input, low, high);

            // use the median of median as pivot and arrange the element as in QuickSort
            int partitionIndex = PartitionUsingPivot(input, low, high, medianOfMedian);

            // we are looking for the middle element of the input array
            int middleElementIndex = (low + high) /2;
            if (partitionIndex == middleElementIndex)
            {
                // we found the middle element
                return input[partitionIndex];
            }
            else if (partitionIndex > middleElementIndex)
            {
                return FindMiddleElement(input, low, partitionIndex - 1);
            }
            else
            {
                return FindMiddleElement(input, partitionIndex + 1, high);
            }
        }

        private int FindMedianOfMedian(int[] input, int low, int high)
        {
            if (low == high)
            {
                // when we have only one element in the array
                return input[0];
            }
            // Divide the list into sublist of length 5
            // Number is just the bucker size and when we sort with this bucket size we should do in 0(n)
            int bucketSize = 5;
            int size = high -low + 1;
            int medianSize = (size % bucketSize == 0) ? size/bucketSize : (size/bucketSize) +1;
            int[] medians = new int[medianSize];

            for (int i = 0; i < medians.Length; i++)
            {
                int startIndex = (i * bucketSize);
                int endIndex = (i + 1) * bucketSize - 1;

                if (endIndex > input.Length - 1)
                {
                    endIndex = input.Length - 1;
                }
                medians[i] = FindMedian(input, startIndex, endIndex);
            }

            int medianOfMedian = FindMedianOfMedian(medians, 0, medians.Length - 1);

            return medianOfMedian;
        }



        private int FindMedian(int[] array, int startIndex, int endIndex)
        {
            int size = endIndex - startIndex + 1;

            int[] subset = new int[size];
            int j = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                subset[j] = array[i];
                j++;
            }

            subset = SortHelper.MergeSort(subset);

            if (size % 2 == 0)
            {
                return subset[size / 2] + subset[(size / 2) - 1];
            }
            else
            {
                return subset[size / 2];
            }
        }

        private int PartitionUsingPivot(int[] array, int low, int high, int pivotValue)
        {
            // get the index of pivotvalue
            int pivotIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == pivotValue)
                {
                    pivotIndex = i;
                    break;
                }
            }

            // put the pivot value to the last of the array
            SortHelper.Swap(array, array.Length - 1, pivotIndex);

            int k = low;

            // iterate from low to last before (bcoz last had pivot value)
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivotValue)
                {
                    SortHelper.Swap(array, j, k);
                    k++;
                }
            }

            SortHelper.Swap(array, k, array.Length - 1);

            return k;
            // Here elements on the left of k is smaller or equal to k
            //      elements on the right of k is greater or equal to k
  
        }

    }
}
