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


        public static void QuickSort(int[] array, int left, int right)
        {
            int partition = Partition(array, left, right);

            //sort left half
            if (left < partition - 1)
            {
                QuickSort(array, left, partition - 1);
            }

            if (right > partition)
            {
                QuickSort(array, partition, right);
            }


        }

        public static int Partition(int[] array, int left, int right)
        {
            //pick a random index as a pivot
            //i am taking the middle element
            int pivot = array[(left + right) / 2];
            //int pivot = array[left];

            while (left <= right)
            {
                //all the elements to the lesser than the pivot should be on the left
                while (array[left] < pivot)
                {
                    left++;

                }

                while (array[right] > pivot)
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
         
                array[left] = array[left] ^ array[right];
                array[right] = array[right] ^ array[left];
                array[left] = array[left] ^ array[right];
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
          var heap = new Heap<int>(input, input.Count, new KarthicMinHeapComparer());
          while (heap.Count > 0)
          {

            output.Add(heap.PopRoot());
          }

          return output;
           
        }
        

        // //Heap Sort
        //public class HeapSort<T>
        //{
        //  public static List<T> Sort(IList<T> list, IComparer<T> comparer)
        //  {

        //    List<T> output = new List<T>();
        //    var heap = new Heap<T>(list, list.Count, comparer);
        //    while (heap.Count > 0)
        //    {

        //      output.Add(heap.PopRoot());
        //    }

        //    return output;
        //  }
        //}

    }



}
