using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Array
{
   public  class ArrayHelper
  {

     //public static int[] RemoveDupicatesInSortedArray(int[] array)
     //{
     //  //since the array is sorted check for adjacent
     //  int[] output = new int[array.Length];
     //  int index = 0;

     //}

     public static int[] GetNonCommonItemsInSortedArray(int[] array)
     {
       //since the array is sorted check for adjacent
       int[] output = new int[array.Length];
       int index = 0;
       bool lastitemduplicate = false;
       for (int i = 1; i < array.Length; i++)
       {
          //check tht i and i-1
         if(array[i -1] == array[i])
         {
           //increment if i is not last item
            lastitemduplicate  = true;
         }
         else if(lastitemduplicate)
         {
           lastitemduplicate = false;
           //if last item
           if (i == array.Length - 1)
           {
             output[index] = array[i];
             index++;
           }
         }
         else
         {
            //when not equal and last item not the same
             output[index] = array[i-1];
             index++;

             //if last item
             if (i == array.Length - 1)
             {
               output[index] = array[i];
               index++;
             }
         }
 
       }

       System.Array.Resize<int>(ref output, index);

       return output;

     }


    //This method search the array for the key and return the index of the key..if not found returns -1
     public static int BinarySearchSortedArray(int[] array, int key)
     {
         //error handling
         if (array.Length == 0)
         {
             //throw error
         }

         int low = 0;
         int high = array.Length - 1;

         while (low <= high)
         {
             int middle = (low + high) / 2;

             if (key == array[middle])
             {
                 return middle; //index of middle element
             }
             else if (key < array[middle])
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
  }
}
