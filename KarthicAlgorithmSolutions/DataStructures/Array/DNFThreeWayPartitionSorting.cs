using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Array
{
    //http://www.geeksforgeeks.org/sort-an-array-of-0s-1s-and-2s/
    //Tri-partition aka Dutch National Flag Problem
    
      //http://www.capacode.com/?p=526


    public class DNFThreeWayPartitionSorting
    {
        public Dictionary<string, int> ht { get; set; }

        public DNFThreeWayPartitionSorting()
        {

           //set some seed data

            ht = new Dictionary<string, int>();
            ht.Add("karthic-2", 2);
            ht.Add("health-1", 1);
            ht.Add("wealth-3", 3);
            ht.Add("family-2", 2);
            ht.Add("dream-2", 2);
            ht.Add("happy-1", 1);
            ht.Add("wife-1", 1);
            ht.Add("job-3", 3);
            ht.Add("playing-3", 3);

            string[] productcodes = ht.Keys.ToArray();
           // GroupProductsByPriority(productcodes);

        }

        private int GetPriority(string productcode)
        {

            return ht[productcode];

        }

        //Sort the array is such a way the high priority comes in first, medium in middle and low priority at last
        //Hight Prioriy is refereed as 1. Medium as 2 and low as 3

        //Logic:
         // We have 3 pointers high, medium and low to partition the productcodes by priority
        //productscodes[0..high -1]  contains high priority productcodes
        //productscodes[high, medium-1]  contains medium priority productcodes
        //productscodes[medium, low] contains unknown priority productcodes that we have to rearrange;
        //productscodes[low+1, n-1] contains low priority productcodes

        //Complexity:
        //Time Complexity: O(n)  linear time (one array scanning with at most one swap per comparison)
        //Space: O(1)

        public void GroupProductsByPriority(string[] productscodes)
        {
            try
            {
                int high = 0, medium = 0, low = productscodes.Length - 1;

                while (medium <= low)
                {
                    int priorityvalue = GetPriority(productscodes[medium]);
                    switch (priorityvalue)
                    {
                        case 1: //High Priority
                            Swap(productscodes, high, medium);
                            high++;
                            medium++;
                            break;
                        case 2: //Medium Priority
                            medium++; 
                            break;
                        case 3: //Low Priority
                            Swap(productscodes, medium, low);
                            low--;
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                //Handle exception
                throw new Exception(ex.Message);
                
            }
         

        }



        /* Utility Function to swap two array values*/
        public static void Swap(string[] array, int index1, int index2)
        {

            //optional optimization:
            //check that the values being swapped are same or not.This step is not required but it is done to prevent swapping the same values in the array
            //we need the priority of both the indexes that need to be swapped and swap only if the priorities are different
            // I will implement this at the end
            string temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;

        }
    }
}
