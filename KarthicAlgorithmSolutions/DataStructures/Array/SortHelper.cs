using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public class SortHelper
    {


        public static int[] MergeSort(int[] input)
        {
            int[] helper = new int[input.Length];

            MergeSort(input, helper, 0, input.Length - 1);


            return input;

        }

        //Divide the array into max possible units and apply merge
        private static void MergeSort(int[] array, int[] helper, int low, int high)
        {
            if (low < high)
            {
                int middle = (low + high)/2;
                MergeSort(array, helper, low, middle);
                MergeSort(array, helper, middle + 1, high);
                Merge(array, helper, low, high, middle);

            }


        }


        private static void Merge(int[] array, int[] helper, int low, int high, int middle)
        {
            //copy both the halves of the array into helper
            for (int i = low; i <= high; i++)
            {
                helper[i] = array[i];
            }

            int helperLeft = low;
            int helperRight = middle + 1;
            int current = low;
             
            //iterate through the helper array...
            //compare the left and right halves of the helper array. put the smaller one into the helper array
            while (helperLeft <= middle && helperRight <= high)
            {

                if (helper[helperLeft] <= helper[helperRight])
                {
                    array[current] = helper[helperLeft];
                    helperLeft++;
                }
                else if (helper[helperRight] <= helper[helperLeft])
                {
                    array[current] = helper[helperRight];
                    helperRight++;

                }

                current++;

            }

            //remaining
            //remaining is done only for left..it is not required for right bcoz it is already there...(don't know how still..got to understand)
            int remaining = middle - helperLeft;

            //copy the rest of the left side of the array into the target array
            for (int i = 0; i <= remaining; i++)
            {
              array[current] = helper[helperLeft];
              helperLeft++;
              current++;
                //array[current + i] = helper[helperLeft + i];
            }
       

        }

        //https://www.youtube.com/watch?v=Z5nSXTnD1I4
        public static void QuickSort(int[] array, int left, int right)
        {

            if (left < right)
            {
                int partition = PartitionUsingForLoop(array, left, right);
                QuickSort(array, left, partition - 1);// sort the left part
                QuickSort(array, partition +1, right);// sort the right part

                ////sort left half
                //if (left < partition - 1)
                //{
                //    QuickSort(array, left, partition - 1);
                //}

                //if (right > partition)
                //{
                //    QuickSort(array, partition, right);
                //}
            }

        }



        public static int PartitionUsingForLoop(int[] array, int left, int right)
        {

            //Best way is to choose either random or middle as pivot..to avoid worst cast time complexity
            int pivotindex = (left + right)/2;
            int pivotvalue = array[pivotindex];

            // put the chosen pivot at A[hi]
            Swap(array, pivotindex, right);
            int i = left;
            //iteration is from left to right -1 excluding last element bcoz last is the pivot

            for (int j = left; j <= right - 1; j++)
            {
                if (array[j] < pivotvalue)
                {
                    Swap(array, i, j);
                    i++;
                }
            }

            //finally the array is sorted like following
            //0 to i-1 will be lesser than pivot
            //i to n will be greater or equal to pivot           
            // Move pivot to its final place
            Swap(array, i, right);
            return i;


        }
        public static int Partition(int[] array, int left, int right)
        {
            //pick a random index as a pivot
            //i am taking the middle element

            Random r = new Random();

            int pivot = array[r.Next(left, right + 1)];
            //int pivot = array[left];

            while (left <= right)
            {
                //all the elements to the lesser than the pivot should be on the left
                while ((left <= right) && array[left] <= pivot)
                {
                    left++;

                }

                while ((left <= right) && array[right] > pivot)
                {
                    right--;
                }

                //swap elements and move left and right indices
                if (left <= right)
                {
                    Swap(array, left, right);
                    left++;
                    right--;

                }

            }
            
            return left;

        }


        public static void Swap(int[] array, int left, int right)
        {
            //to swap two elements..either use temp or xor
            //a ^= b;
            //b ^= a;
            //a ^= b;
            //XOR will not work if left is equal to right..will be resulting in 0
            //Actually we dont need to swap if the left and right is same or leftvalue and right values is same
            if (left != right && array[left] != array[right])
            {
                array[left] = array[left] ^ array[right];
                array[right] = array[right] ^ array[left];
                array[left] = array[left] ^ array[right];
            }
        }

        /* A typical recursive implementation of Quicksort for array*/

       /* This function takes last element as pivot, places the pivot element at its
        correct position in sorted array, and places all smaller (smaller than 
        pivot) to left of pivot and all greater elements to right of pivot */

        public static int Partition2(int[] arr, int l, int h)
        {
            int x = arr[h];
            int i = (l - 1);

            for (int j = l; j <= h - 1; j++)
            {
                if (arr[j] <= x)
                {
                    i++;
                    Swap(arr, arr[i], arr[j]);
                }
            }
            Swap(arr, arr[i + 1], arr[h]);
            return (i + 1);
        }

            /* A[] --> Array to be sorted, l  --> Starting index, h  --> Ending index */

        public static void QuickSort2(int[] A, int l, int h)
        {
            if (l < h)
            {
                int p = Partition2(A, l, h); /* Partitioning index */
                QuickSort2(A, l, p - 1);
                QuickSort2(A, p + 1, h);
            }
        }


        public static string SortChars(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }


        public static List<int> PerformHeapSort(List<int> input)
        {

          List<int> output = new List<int>();
          var heap = new Heap<int>(input, input.Count, new KarthicMinHeapComparer2());
          while (heap.Count > 0)
          {

            output.Add(heap.PopRoot());
          }

          return output;
           
        }



        // see this https://en.wikipedia.org/wiki/Merge_sort
        public static int[] MergeSort(int[] input, bool RemoveDuplicates)
        {
          int[] helper = new int[input.Length];
          List<int> array = new List<int>(input);


          MergeSortAndRemoveDuplicates(array, helper, 0, input.Length - 1, RemoveDuplicates);

          return array.ToArray();

        }

        //Divide the array into max possible units and apply merge
        //kk--This has a bug and need to be fixed
        private static int MergeSortAndRemoveDuplicates(List<int> array, int[] helper, int low, int high, bool removeduplicatesflag)
        {
          int newlength = high; //initialize with actual high

          if (low < high)
          {
              int middle = (low + high) / 2;
              int newmiddle = MergeSortAndRemoveDuplicates(array, helper, low, middle, removeduplicatesflag);
              int difference = middle - newmiddle;
              int newhigh = MergeSortAndRemoveDuplicates(array, helper, (middle + 1 - difference), high - difference, removeduplicatesflag);
              int difference2 = (high - difference) - newhigh;
              newlength = MergeAndRemoveDuplicates(array, helper, low, high - difference - difference2, (low + high - difference2) / 2, removeduplicatesflag);
      


          }

          return newlength;
        }

      //This method performs merge and remove duplicates (and deletes the duplicates and changes the array size) and returns the new size (high)
        private static int MergeAndRemoveDuplicates(List<int> array, int[] helper, int low, int high, int middle, bool removeduplicates)
        {
          //copy both the halves of the array into helper
          for (int i = low; i <= high; i++)
          {
            helper[i] = array[i];
      
          }

          int helperLeft = low;
          int helperRight = middle + 1;
          int current = low;

          //iterate through the helper array...
          //compare the left and right halves of the helper array. put the smaller one into the helper array
          while (helperLeft <= middle && helperRight <= high)
          {
            //since we added -1 for duplicates handle that logic
            //if (helper[helperLeft] == -1)
            //{
            //  helperLeft++;
            //  //don't increment the current
            //}
            //else if (helper[helperRight] == -1)
            //{
            //  helperRight++;
            //  //don't increment the current
            //}
            if (helper[helperLeft] < helper[helperRight])
            {
             
              array[current] = helper[helperLeft];
              helperLeft++;
                current++;
            }
            else if (helper[helperRight] < helper[helperLeft])
            {
              array[current] = helper[helperRight];
              helperRight++;
                current++;
            }
            else ///special else case to handle when two items are equal
            {
               //when HR and HL are equal..take either one and increment both pointer so that we skip the duplicate one
              array[current] = helper[helperRight];
              helperLeft++;
              helperRight++;
                current++;

            }

          }

          //remaining
          //remaining is done only for left..it is not required for right bcoz it is already there...(don't know how still..got to understand)
          int remaining = middle - helperLeft;

          //copy the rest of the left side of the array into the target array
          for (int i = 0; i <= remaining; i++)
          {
            array[current] = helper[helperLeft];
            helperLeft++;
            current++;
            //array[current + i] = helper[helperLeft + i];
          }
          //Since we have remove duplicates which should make an empty on the right corner for duplicates..i m handling the logic for right

          while (helperRight <= high)
          {
            array[current] = helper[helperRight];
            helperRight++;
            current++;
          }

          //since we skipped the dup..current might not be equal to high
          //while (current -1 != high)
          //{
          //  //array[current] = -1;
          //  //current++;
        
          //}

          int duplicates = high - (current - 1);
          for (int i = 0; i < duplicates; i++)
          {
            array.RemoveAt(high);
            high--;

          }
          //if there are duplicates the high will be reduced
          return high;

        }



        //find the smallest ith element 
        public static int Findthesmallest(int[] array, int rank)
        {
            return SelectionRankAlgorithmSmallest(array, 0, array.Length - 1, rank);
        }

        public static int FindtheLargest(int[] array, int rank)
        {
            return SelectionRankAlgorithmLargest(array, 0, array.Length - 1, rank);
        }


        //selection rank algorithm is used to return the ith smallest or largest element in the array...
        public static int SelectionRankAlgorithmSmallest(int[] array, int left, int right, int rank)
        {
            //partition using random value and get the index of the left
            //random element as pivot
            //important pivot should be the value of random ele not index
            //this has to be random number not middle...
            //Random random = new Random();

            //int pivot = array[GetRandomValue(left,right)];

            int leftpartitionend = PartitionUsingForLoop(array, left, right);

            int leftsize = (leftpartitionend + 1) - left;

            //simalry we can calculate right size for ith largest element and then do minofarray on the right
            int rightsize = (right - leftpartitionend) + 1;

            //if there are exactly rank elements on the left
            //update..since rank is size ..no need + 1
            //if (leftsize == rank + 1)
            if (leftsize == rank)
            {
                return MaxoftheArray(array, left, leftpartitionend);

            }
            else if (rank < leftsize)
            {
                return SelectionRankAlgorithmSmallest(array, left, leftpartitionend, rank);
            }
            else
            {
                return SelectionRankAlgorithmSmallest(array, leftpartitionend + 1, right, rank - leftsize);
            }

        }


        public static int SelectionRankAlgorithmLargest(int[] array, int left, int right, int rank)
        {

            int partitionend = PartitionUsingForLoop(array, left, right);

            int rightstarting = partitionend + 1;

            int rightsize = (right - rightstarting) + 1;

            if (rightsize == rank)
            {
                return MinofArray(array, rightstarting, right);
            }
            else if (rank > rightsize)
            {
                return SelectionRankAlgorithmLargest(array, left, partitionend, rank - rightsize);
            }
            else
            {
                return SelectionRankAlgorithmLargest(array, rightstarting, right, rank);
            }
        }

        public static int MaxoftheArray(int[] array, int startindex, int endindex)
        {

            int max = Int32.MinValue;

            for (int i = startindex; i <= endindex; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }

            }

            return max;
      

        }

        public static int MinofArray(int[] array, int startindex, int endindex)
        {
            int min = Int32.MaxValue;
            for (int i = startindex; i <= endindex; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }

            }

            return min;
        }

        //This method partition elements based on the random pivor and returns the index of the left where the partition ends.
        public static int partitionelements(int[] array, int left, int right, int pivot)
        {

            while (true)
            {
                while (left <= right && array[left] <= pivot)
                {
                    left++;
                }

                while (left <= right && array[right] > pivot)
                {
                    right--;
                }

                if (left > right)
                {
                    return left - 1;
                }

                Swap(array, left, right);
            }


        }


        public static int GetRandomValue(int lower, int higher)
        {
            Random random = new Random();

            return random.Next(lower, higher);
        }
        

    }

    



}
