using Puzzles.DataStructures.Tree.IntervalTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class TechCompanyPage4 : Form
    {
        public TechCompanyPage4()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            int input = Convert.ToInt32(this.textBox15.Text);


            //test cases
            //34200 = 34.2 k
            //434200 = 434.2 k
            //34212 = 34.2 k
        

            string output = ConvertBytesValueIntoCustomFormat(input);
            this.textBox12.Text = output;

        }


        //Given a byte integer value lesser than or equal to 1 gb
        //Assumption: 1000 bytes = 1 kb
        //The ouput should have max of 3 digits (excluding decimal point)
        //should be accurate. Round to the nearest value
        //should have single letter for unit

        //logic:
        //create an unit array (for 10 power 2)  char[] units = { 'B', 'K', 'M', 'G' };
        //Take the given value and iteratively divide by 1000 to get the max unit
        //While dividing keep track of max unit, build the string from back to front (either string or stack) and keep track of the lastadded item length
        //Build the string and keep the decimal point
        //Convert it to decimal
        //Round the decimal and make sure to maintain the max Decimal.Round(unitvalue, (maxlength - lastvaluelength));
        //Remove trailing zero eg 324.00 should be 324K USING g29 format
        //Added the unit highest level

        public string ConvertBytesValueIntoCustomFormat(int value)
        {
            //Bytes, kilobytes, mega bytes, giga bytes
            char[] units = { 'B', 'K', 'M', 'G' };
            int maxlength = 3;
            //make sure there is no leading zero

            if (value < 0 || value > Convert.ToInt32(Math.Pow(10, 9)))
            {
               //error input greater than 1 gb or lesser than 0
            }
            StringBuilder sb = new StringBuilder();
            Stack<string> mystack = new Stack<string>();
            int level = 0;
            int lastvaluelength = 0;
            while (value > 0)
            {
                int remainder = value % 1000;
                //string validdigits = RemoveTrailingAndLeadingZero(remainder);

                ////we can use string and build backward but i'm using stack
                //sb.Insert(0, validdigits); //build the backwards
                mystack.Push(remainder.ToString());

                value = value / 1000;
                level++;
                lastvaluelength = remainder.ToString().Length;
            }

           //we are done with the input
           //lastvalue is have the last added string that will help to keep the decimal
           //level -1 will have the biggest unit
           //Make sure to round the value

           //since i used stack whatever is after the last we need to keep decimal point
            StringBuilder output = new StringBuilder();
            int counter = 0; //to keep decimal after the first item
            int lengthtracker = maxlength;
            while (mystack.Count != 0)
            {
                string s = mystack.Pop();
                counter++;
                output.Append(s);
                if (counter == 1)
                {
                    lastvaluelength = s.Length; 
                    output.Append(".");
                }
            }

            //We will get the decimal
            decimal unitvalue = Convert.ToDecimal(output.ToString());
 
            //Round the values to the max of 3
            decimal unitrounded = Decimal.Round(unitvalue, (maxlength - lastvaluelength));

            //append with the level
            //g29 format removes trailing zero after decimal
            //http://stackoverflow.com/questions/4786713/how-do-i-format-a-c-sharp-decimal-to-remove-extra-following-0s
            //http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx#GFormatString
            string unitroundedandtrailed = unitrounded.ToString("G29");
            //string unitroundedandtrailed = RemoveTrailingAfterDecimal(unitrounded.ToString(), (maxlength - lastvaluelength));
            output.Clear();
            output.Append(unitroundedandtrailed);

            //when u convert the decimal to strin..we will get trailing zeros eg 4 to string will be 4.00

             
            //remove the trailing zero from the string
            output.Append(units[level - 1]);

            return output.ToString();

        }

        public string RemoveTrailingAndLeadingZero(int value)
        {
            int result = value;
            while (result > 0 && result % 10 == 0)
            {
                result = result / 10;
            }

            return result.ToString();
        }

        //Input is a decimal like 4.00 or 4.30 or 100.00 or 130.10
        public string RemoveTrailingAfterDecimal(string value,int maxallowedafterdecimal)
        {
            int indexofdecimalpoint = value.IndexOf('.');
            int counter = indexofdecimalpoint + 1;
            //00
            while (counter  < value.Length)
            {
                //Next element
                if (value[counter] == '0')
                {
                    //If o found immediately after decimal then we can ignore the decimal
                    if (counter == indexofdecimalpoint + 1)
                    {
                        //we ignore the decimal point
                        return value.Substring(0, indexofdecimalpoint);
                    }
                    else
                    {
                        return value.Substring(0, counter);
                    }
                }

                counter++;
            }

            //code comes means no zero found

            return value;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            int m = Convert.ToInt32(this.textBox2.Text);
            string output = "";
            bool ispossible = IsSetContainsSubsetofValue(input, m);
            this.textBox4.Text = ispossible == true ? "Yes" : "No";
            this.textBox1.Text = output;


        }

        // Returns true if there is a subset of set[] with sun equal to given sum
        public bool isSubsetSum(int[] set, int n, int sum)
        {
            // Base Cases
            if (sum == 0)
            {
                return true;
            }
            if (n == 0 && sum != 0)
                return false;

            // If last element is greater than sum, then ignore it
            if (set[n - 1] > sum)
                return isSubsetSum(set, n - 1, sum);

            /* else, check if sum can be obtained by any of the following
               (a) including the last element
               (b) excluding the last element   */
            return isSubsetSum(set, n - 1, sum) || isSubsetSum(set, n - 1, sum - set[n - 1]);

        }


        public bool IsSubsetSumUpdated(int[] set, int index, int sum)
        {
            if (index == set.Length)
            {
                return (sum == 0);
            }

            // we can't take an item which is greater than sum
            if (set[index] > sum)
            {
                // if the current item is greater than sum then we can use it
                return IsSubsetSumUpdated(set, index + 1, sum);
            }

            /* else, check if sum can be obtained by any of the following
      (a) including the last element
      (b) excluding the last element   */
            return  IsSubsetSumUpdated(set, index + 1, sum - set[index]) || IsSubsetSumUpdated(set, index + 1, sum);
        }


       // http://codeaccepted.wordpress.com/2014/02/27/dynamic-programming-subset-sum-problem/

        // Returns true if there is a subset of set[] with sun equal to given sum
        //This is such a simple dyanmic programming stratergy but took my fucking time to get that...Need more dp practice hmm
        //eg: find the sum of 4 in 1,2,3,5
        public  bool IsSubsetSumUsingDynamicProgramming(int[] set, int n, int sum, List<string> subsets)
        {
            //1 means true..0 means false
            bool[,] ispossible = new bool[n + 1, sum + 1];
            //int[i,j] means is it possible to form sum j using 0 to i of the given set
            //let i be the range from 0 to given number
            //let j be the given subset of 0 to j
            //ispossoible[i,j] = 1 means it is possible to make the i from 0 to j
            //we need to find ispossible[lastindex][4]

            ///0 can be made by any subsets like empty subset
            for (int k = 0; k < set.Length; k++)
            {
                ispossible[0, k] = true;
            }
            //by default 0
             for (int k = 0; k < sum; k++)
            {
                ispossible[k, 0] = false;
            }
            //since 0 is alredy set for both the first row and first col, we can start from 1
             for (int i = 1; i <= sum; i++)
             {
                 for (int j = 1; j < set.Length; j++)
                 {

                     ispossible[i, j] = ispossible[i, j - 1];
                     //set is an array and the matrix has one extra colum
                     if (i >= set[j - 1])
                     {
                         ispossible[i, j] = ispossible[i, j - 1] || ispossible[i - set[j - 1], j - 1];

                     }


                      //  In general, ispossible[i][j] = 1 iff one of the following two conditions is true:

                     //1.	ispossible[i-1][j] is 1. If ispossible[i-1][j] is 1 it means that it is possible to obtain a sum of j by selecting some numbers from {nums[1],nums[2],nums[3],…,nums[i-1]}, so obviously the same is also possible with the set {nums[1],nums[2],nums[3],….,nums[i-1],nums[i]}.

                     //2.	ispossible[i-1][j-nums[i]] is 1. If ispossible[i-1][j-nums[i]] is 1 it means that it is possible to obtain a sum of (j-nums[i]) by selecting numbers from {nums[0],nums[1], nums[2],…,nums[i-1]}. Now if we select nums[i] too, then we can obtain a sum of (j-nums[i])+nums[i] = j. Therefore ispossible[i][j] is 1.
               
                   //testing to make subets
                     //if (ispossible[i - set[j - 1], j - 1])
                     //{
                     //    int lastindex = j;

                     //}
                 
                 }
             }

             return ispossible[sum, n];

           
        }

        //updae: 4/9/2015 Just tring to rewrite the code for fun
        //we need to do some special backtracking logic to get the items that formed the sum

        public bool IsSetContainsSubsetofValue(int[] set, int value)
        {
            //We use dp to see whether subset of the given set has the value
            //j will be the sum value from (o to value)
            //i will be the number of items in the set (o to set.size -1)

            bool[,] Ispossible = new bool[set.Length + 1, value + 1];
            //bool[i,j] means we can form the sum value of j using 0 to i items
            //bool[set.length-1, value] is the whether the value can be made using 0 to set.length -1 items

            //Set case of i and j =0
            //The sum of 0 can be made with any subet eg empty subset. without taking that item bcoz empty is also a subset of the given set
            //we can iterate through the number of items but remember i value is greater than set size. because we added one extra item to this 2d array set.Length + 1
            //for (int k = 0; k <= set.Length; k++)
            //{
            //    Ispossible[k, 0] = true;
            //}

            //Nested Iteration
            //First iterate the sum value which is j
            for (int j = 0; j < Ispossible.GetLength(1); j++)
            {
                //next iterate the set/items
                for (int i = 0; i < Ispossible.GetLength(0); i++)
                {
                    //we know the fact the sum of o can be formed by any set (empty set ) so when j value is o we can set the value to true for any value of i
                    if (j == 0)
                    {
                        Ispossible[i, j] = true;
                        //break;
                    }

                    //we know the fact that sum value of greater than o cannot be made with empty sets
                    //so when i value is 0 (which means no item is taken) and when j value is not 0 (any sum value like 1,2,3) we set to false
                    else if (i == 0 && j != 0)
                    {
                        Ispossible[i, j] = false;
                       // break;
                    }

                    //now we come to the actual logic

                    //we have two value that need to be considered to form a sum value
                    // ispossible[i,j] is equal to true for either of the two conditions
                    //1) check Excluding the ith item: if a sum of j can be formed from 0 to i-1 then ofcouse we can from the sum of j using o to i item 
                    //becasue the 0 to i-1 is also a subset of 0 to i
                    //2) check Including the ith item: Subtract the value of the item and see whether the difference value can be made using o to i-1 
                    //Note: second condition is checked only when the value of the item is equal to or greater than the sum

             
                    //now check the ith item is greter than the sum 
                    //Note: i here is greater than set size so to catch the actual value of set using i -1
                    else if (j >= set[i-1])
                    {
                        //If the value can be made either excluding the item or including it
                        Ispossible[i, j] = Ispossible[i - 1, j] || Ispossible[i - 1, j - set[i - 1]];

                    }
                    else
                    {
                        //Excluding the ith item is stored the in the array
                        Ispossible[i, j] = Ispossible[i - 1, j];
                    }


                }
            }

            return Ispossible[set.Length, value];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            int m = Convert.ToInt32(this.textBox2.Text);
            string output = "";
            bool ispossible = IsSubsetSumUpdated(input,0, m);
            this.textBox4.Text = ispossible == true ? "Yes" : "No";
            this.textBox1.Text = output;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);
            int m = Convert.ToInt32(this.textBox2.Text);
            string output = "";
            bool ispossible = isSubsetSum(input, input.Length, m);
            this.textBox4.Text = ispossible == true ? "Yes" : "No";
            this.textBox1.Text = output;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //http://www.careercup.com/question?id=5772881111810048

            //http://stackoverflow.com/questions/5534063/zero-sum-subarray

            /* ex: 2 3 -4 9 -1 -7 -5 6 5 
         sum to enter in hash table : 0 2 5 1 10 9 2 -3 3 8 
         index to enter in hash table: -1 0 1 2 3 4 5 6 7 8 
             * Logic:
             * 1) Create an ht with sum as key and list of index as value
             * 2)  we add the base value in the ht as sum 0 and index -1 because if we encounter 0 in the iteration then we found the subarray of value 0
             * 3) We iterate the given array from 0 to array.length -1
             * 4) In the iteration we make the sum of the previous elements with the current element and use the sum as the key to ht
             * 5) If the ht does not contain the sum. add sum as the key and value as the index (index list)
             * 6) If the ht contains the sum, then we found the subarray having the value of zero 
             * grab the index of the sum in the ht with ht[sum]. The index sum and the current item sum is equla to 0 so that mean the items from index + 1 to current index sum will be zero
             * print the values from index + 1 to current index 
             * The reason we have List<int> as value is we might have multiple sum value so each key might have multiple indexes like below
             * int[] array = {0, 1, -1, 0}  we need to print every subarray the can cause the sum to 0  eg (0) (1,-1) (0,1,-1) etc
              

          */

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);

            List<string> result = FindSubArraySumOfzero(input);
            this.textBox5.Text = AlgorithmHelper.ConvertStringListArrayToSpaceSeparatedString(result);



        }


        private List<String> FindSubArraySumOfzero(int[] input)
        {
            List<string> output = new List<string>();
            //Dictionary will have key has sum and value is the index of the array
            Dictionary<int, List<int>> ht = new Dictionary<int, List<int>>();

            //add the base as sum = o  and index -1 bcoz if you get sum of 0 in the iteration we need to print that
            List<int> value = new List<int>();
            value.Add(-1);
            ht.Add(0, value);

            //Iterate through the array and store the sum and index in ht
            //subarraysum[i] = input[i-1] + input[i]
            int currentvalue = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //add the value to the previous sum
                currentvalue += input[i];

                if (ht.ContainsKey(currentvalue))
                {
                    //if the ht contain the value then found the subarray of sum 0 from (index of the ht value + 1) to current i
                    List<int> indexes = ht[currentvalue];
                    foreach (int index in indexes)
                    {
                        //from index will be index + 1 
                        //to index will be current value of i
                        output.Add(PrintSubArrayByIndex(input, index + 1, i));
                    }

                    //we printed the subarray that has value of zero

                    indexes.Add(i);
                    ht[currentvalue] = indexes; //updated the value and inclue the new item

                }
                else
                {
                    List<int> indexvalues = new List<int>();
                    indexvalues.Add(i);
                    ht.Add(currentvalue, indexvalues);
                }

            }

            return output;

        }

        private string PrintSubArrayByIndex(int[] array, int fromindex, int toindex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = fromindex; i <= toindex; i++)
            {
                sb.Append(array[i]).Append(",");
            }
            sb.Length = sb.Length - 1; //to remove the extra comma
            sb.Append(")");

            return sb.ToString();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Given am method to get all appointment by username

            //Simulate the given condition
            //user1 appoint
            Dictionary<string, List<Appointment>> userapt = new Dictionary<string, List<Appointment>>();
            List<Appointment> apt1 = new List<Appointment>();
            apt1.Add(new Appointment(ParseStringDate("06/05/2014 7:00 AM"), ParseStringDate("06/05/2014 8:00 AM")));
            apt1.Add(new Appointment(ParseStringDate("06/05/2014 9:00 AM"), ParseStringDate("06/05/2014 11:00 AM")));
            apt1.Add(new Appointment(ParseStringDate("06/05/2014 12:00 PM"), ParseStringDate("06/05/2014 3:00 PM")));

            //user2 appointments
            List<Appointment> apt2 = new List<Appointment>();
            //apt2.Add(new Appointment(ParseStringDate("06/05/2014 7:30 AM"), ParseStringDate("06/05/2014 7:40 AM")));
            apt2.Add(new Appointment(ParseStringDate("06/05/2014 10:00 AM"), ParseStringDate("06/05/2014 11:30 AM")));
            apt2.Add(new Appointment(ParseStringDate("06/05/2014 12:30 PM"), ParseStringDate("06/05/2014 2:30 PM")));

            //user2 appointments
            List<Appointment> apt3 = new List<Appointment>();
            apt3.Add(new Appointment(ParseStringDate("06/05/2014 7:10 AM"), ParseStringDate("06/05/2014 7:35 AM")));
            apt3.Add(new Appointment(ParseStringDate("06/05/2014 08:00 AM"), ParseStringDate("06/05/2014 10:30 AM")));
            apt3.Add(new Appointment(ParseStringDate("06/05/2014 01:00 PM"), ParseStringDate("06/05/2014 2:00 PM")));


            userapt.Add("user1", apt1);
            userapt.Add("user2", apt2);
            userapt.Add("user3", apt3);

            /* Logic:
             * 1) Get the appointment of each user and add it to the object UserAppointment with prop username, start time and end time
             * 2) All the appointment of the users will be in one collection object. Sort that object by using Appointment Start Time  (use custom comparator)
             * 3) Once the list is sorted compare the adjacent appointment
             *   Note: Adjacent appointment may also be with the same user
             * 4) Check whether two adjancent appointments (s1,e1) (s2,e2) has conflict or overlapping time slot by checking condition S2 <= E1
             * 5) If the conflict exists then grab the overlapping appointment between those two adjacent appointments
             *  while doing this traversal use ht to store the username encountered
             * 6) User the grabbed overlappingappointment btwn two users and compare it with the next appointment..continue this untill u get no conflicts
             * 7) When two appointments have no conflict means we crossed that time slot so check whether the overlapping appointment encountered all the users
             * 8) If the overlappint appointment encountered all the users (ht.count == given users count) then we found the common time btw all user. print it in output
             * 9) Reset the overlappint and i varaible to check for next adjacent and continue this till the end of the loop
             * 
             */
            List<Appointment> conflicts = GetConflictingTimeSlots(userapt);
             
        }


        public List<Appointment> GetConflictingTimeSlots(Dictionary<string, List<Appointment>> userlist)
        {
            List<Appointment> conflictslots = new List<Appointment>();

            List<UserAppointment> userapts = new List<UserAppointment>();

            foreach (KeyValuePair<string, List<Appointment>> entry in userlist)
            {
                foreach(Appointment apt in entry.Value)
                {
                    userapts.Add(new UserAppointment(apt.StartTime, apt.EndTime, entry.Key));
                }
            }

            //we will have global list by username
            userapts.Sort(new UserAppointmentComparator());

            //we have the sorted list by apt start date

            UserAppointment overlapping = null;
            Hashtable ht = new Hashtable();

            for (int i = 0; i < userapts.Count; i++)
            {

                UserAppointment current = null;
                UserAppointment next = null;

                if (overlapping == null)
                {
                    current = userapts[i];
                    next = userapts[i + 1];
                }
                else
                {
                    current = overlapping;
                    next = userapts[i];
                }

                //compare the two adjacent appointments

                //check for conflict (s1,e1) (s2, e2)  if conflict s2 < e1
                if (next.StartTime < current.EndTime)
                {

                    if (!ht.ContainsKey(userapts[i].Username))
                    {
                        ht.Add(userapts[i].Username, true);
                    }

                    if (overlapping == null)
                    {
                        if (!ht.ContainsKey(userapts[i + 1].Username))
                        {
                            ht.Add(userapts[i + 1].Username, true);
                        }

                        i++;
                    }

                    //overlapping.StartTime = Max(userapts[i - 1].StartTime, userapts[i].StartTime);
                    //overlapping.EndTime = Min(userapts[i - 1].StartTime, userapts[i].StartTime);
                    overlapping = new UserAppointment(Max(current.StartTime, next.StartTime), Min(current.EndTime, next.EndTime), string.Empty);

                }
                else
                {
                    //no conflict  end of this range so check the ht count
     
                    if (overlapping != null)
                    {
                        if (ht.Count == userlist.Count)
                        {
                            conflictslots.Add(new Appointment(overlapping.StartTime, overlapping.EndTime));
                        }
                        //reset
                        overlapping = null;
                        i--;
                    }

                    //Either way clear the hashtable
                    ht.Clear();
                    

                }

            }

            //after end 
            if (ht.Count == userlist.Count)
            {
                if (overlapping != null)
                {
                    conflictslots.Add(new Appointment(overlapping.StartTime, overlapping.EndTime));
                }

            }

            return conflictslots;


        }

        public DateTime ParseStringDate(string date)
        {
            return DateTime.Parse(date);
        }

        public static DateTime Max(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }

        public static DateTime Min(DateTime a, DateTime b)
        {
            return a < b ? a : b;
        }

        //This function takes all the usernames and return the list of timeslots that are not-overlapping which means these time slots are avaiable to schedule meeting with all user
        public List<Appointment> GetAvailableTimeSlots(List<string> usernames)
        {
            List<Appointment> allusersappointment = new List<Appointment>();
            List<Appointment> NonOverlappingApt = new List<Appointment>();
            //Find all the available timeslots for the user and add it to the allusers appointment list.
            //By doing this we are storing all the appointment in a single list  assuming the memory of this list is not an issue
            foreach(string username in usernames)
            {
                allusersappointment.AddRange(GetAvailableAppointment(username));
            }
            //We are sorting all the appointment based the Start time of the appoinment. This merge sort will be done in o(n log n)
            //AppointComparator is a custom comparer that i created which sorts the appointment based on the start time
            allusersappointment.Sort(new AppointmentComparator());
            //we compare two adjacent appointments
            for (int i = 1; i < allusersappointment.Count; i++)
            {
              // Let say we have two appointment Current (S1,E1) and Next (S2,E2) 
              //In order to check whether current is non-overlapping appoinment, we need to check condition S1 < S2 && E1 < S2   
                Appointment current = allusersappointment[i - 1];
                Appointment nextappt = allusersappointment[i];
                //Check whether each appointment is overlapping or not
                if (current.StartTime < nextappt.StartTime && current.EndTime < nextappt.StartTime)
                {
                    //Add the non-overlapping appointment to the result list
                    NonOverlappingApt.Add(current);
                }

            }
       
            return NonOverlappingApt;
           
          
         
        }


        //In the above GetAvailableTimeSlotsMethod1, I compared two adjacent appointmenst in the sorted list. I can imporve this by using Binary Search of the array.
        //Assuming we have many appointments whose possibility of conflict is more then I can use use Binary Search and find whether conflict happend for the give one

        public List<Appointment> GetAvailableTimeSlotsMethod2(List<string> usernames)
        {
            List<Appointment> allusersappointment = new List<Appointment>();
            List<Appointment> NonOverlappingApt = new List<Appointment>();
            //Find all the available timeslots for the user and add it to the allusers appointment list.
            //By doing this we are storing all the appointment in a single list  assuming the memory of this list is not an issue
            foreach (string username in usernames)
            {
                allusersappointment.AddRange(GetAvailableAppointment(username));
            }
            //We are sorting all the appointment based the Start time of the appoinment. This merge sort will be done in o(n log n)
            //AppointComparator is a custom comparer that i created which sorts the appointment based on the start time
            allusersappointment.Sort(new AppointmentComparator());
            //we compare two adjacent appointments
            for (int i = 0; i < allusersappointment.Count; i++)
            {
               
                Appointment current = allusersappointment[i];
                int nextoverlappingindex = FindLastOverlappingAppointment(allusersappointment, i, current);
                //if no overlapping found
                if (nextoverlappingindex == -1)
                {
                    //Add the non-overlapping appointment to the result list
                    NonOverlappingApt.Add(current);
                }
                else
                {
                    //Last overlapping index found so don't add i to lastindex apt as available
                    i = nextoverlappingindex; // the for loop will increment i and go for the next o
                }

            }

            return NonOverlappingApt;
           
          

        }
        
        //Custom Compare condition based on the Appointment Start time
        public class AppointmentComparator : IComparer<Appointment>
        {

            int IComparer<Appointment>.Compare(Appointment x, Appointment y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }

        //Custom Compare condition based on the Appointment Start time
        public class UserAppointmentComparator : IComparer<UserAppointment>
        {

            int IComparer<UserAppointment>.Compare(UserAppointment x, UserAppointment y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }


      
        //This is a given function. It takes the username and gives all the available timeslots for the user
        public List<Appointment> GetAvailableAppointment(string username)
        {

            List<Appointment> timeslots = new List<Appointment>();

            Appointment ts1 = new Appointment(DateTime.Now, DateTime.Now.AddHours(2));
            Appointment ts2 = new Appointment(DateTime.Now.AddHours(5), DateTime.Now.AddHours(5));
            Appointment ts3 = new Appointment(DateTime.Now.AddMonths(1), DateTime.Now.AddHours(2).AddMonths(1));

            timeslots.Add(ts1);
            timeslots.Add(ts2);
            timeslots.Add(ts3);

            return timeslots;

            

            
        }

        public class Appointment
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool HasConflict; //This is for custom problem..not all apt will have this

            public Appointment(DateTime starttime, DateTime endtime)
            {
                this.StartTime = starttime;
                this.EndTime = endtime;
            }
        }

        public class UserAppointment
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }

            public string Username { get; set; }

            public UserAppointment(DateTime starttime, DateTime endtime, string username)
            {
                this.StartTime = starttime;
                this.EndTime = endtime;
                this.Username = username;
            }
        }

        //This function will return the last overlapping index for the currentappointment
        //if the last overlapping index is same as the current apppoint index, then return -1 else return the acutual index of the sorted appointment
        public int FindLastOverlappingAppointment(List<Appointment> list, int startindex, Appointment CurrentApt)
        {
            int low = startindex;
            int high = list.Count - 1;
            int middle;
            while (low < high)
            {
                middle = (low + high) / 2;

                //Here we are doing overlapping condition  
                //Let say we have two appointment Current (S1,E1) and Next (S2,E2) 
                //overlapping means S1 <= E2 and E1 >= S2
                if(CurrentApt.StartTime <= list[middle].EndTime &&  CurrentApt.EndTime >= list[middle].StartTime)
                {
                    low = middle + 1;
                }
                else
                {
                    high = middle;
                }
            }

            //if the last overlapping index is same as the current apppoint index, then return -1 else return the acutual index of the sorted appointment
            return (high == startindex) ? -1 : high;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //http://crackprogramming.blogspot.com/2012/11/find-overlapping-range-or-find.html
            //Given:{{1, 3}, {2, 4}, {10, 12}, {5, 6} {5.30,7}}
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 01:00 PM"), ParseStringDate("06/05/2014 03:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 02:00 PM"), ParseStringDate("06/05/2014 04:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 10:00 PM"), ParseStringDate("06/05/2014 12:00 AM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 05:00 PM"), ParseStringDate("06/05/2014 06:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 05:30 PM"), ParseStringDate("06/05/2014 07:00 PM")));

            List<Appointment> conflicts = FindOverlappingAppoinments(appointments);

            //Follow up Merge the conflicts
            List<Appointment> mergelist = new List<Appointment>();
            //since the result is already sorted
            for (int i = 1; i < conflicts.Count; i++)
            {
                 //compare adjacen conflicts
                if (conflicts[i].StartTime <= conflicts[i-1].EndTime)
                {
                    //merge the conflcit.
                    //update..5/10 actaully we should use max..not the below important only start is sorted end is not
                    //mergelist.Add(new Appointment(conflicts[i-1].StartTime, conflicts[i].EndTime));
                    mergelist.Add(new Appointment(conflicts[i - 1].StartTime, Max(conflicts[i].EndTime, conflicts[i - 1].EndTime)));
                    i++;
                }
            }

            int count2 = mergelist.Count;

        }

        //Logic:
        //1)Sort the list by apt.StartDate
        //2)Iterate the sorted list while doing it for each appointment (0..n)
        //find the appointment whose startdate is lesser than i end date (by binary search) and return the index of the last found
        //if we find anything then mark all the apt from i to the index as overlapping and continue
        //if we don't find anything or the index is the same as i then no conflicting apt found mark conflicting as false and continue
        public List<Appointment> FindOverlappingAppoinments(List<Appointment> apts)
        {
            List<Appointment> conflictapt = new List<Appointment>();
            //sort the list by apt.startdate
            apts.Sort(new AppointmentComparator());

            //iterate over the sorted list
            for (int i = 0; i < apts.Count; i++)
            {
                int lastindex = BinarySearchofArray(i, apts);
                //no conflict found
                if (lastindex == -1)
                {
                    //mark the ith else has no conflict
                    apts[i].HasConflict = false;
                }
                else
                {
                      ///mark for i to last index as conflict
                    int lastoverlapped = lastindex;
                    int startindex = i;
                    while (startindex <= lastindex)
                    {
                        //go from first to last to maintain the sorted order
                        apts[startindex].HasConflict = true;
                        conflictapt.Add(apts[startindex]);
                        startindex++;

                    }

                    //set the interation to continue after last index
                    i = lastoverlapped;
                }


            }

            //finally we marked all the conflicting prop of apts
            return conflictapt;


        }

        //Find the last index of the list whose appointment is conflicting with the given index appointment
        //Time : o n(log n)
        public int BinarySearchofArray(int currentindex, List<Appointment> list)
        {
            Appointment currentapt = list[currentindex];

            int low = currentindex + 1;
            int high = list.Count - 1;
   

            while (high >= low)
            {
                int middle = (low + high) / 2;
                Appointment middleapt = list[middle];

                //check for overlapping condition in sorted list 
                //(s1, e1) (s2, e2)
                //if (s2 <= e1) means overlapping

                if (middleapt.StartTime <= currentapt.EndTime)
                {
                    //then middle is the conflicting apt 
                    //find anything we have greater than this
                    low = middle + 1;

                }
                else
                {
                    high = middle - 1;
                }

            }

            //trick not sure still
            //if nothing found then high will lesser than low
            return (high == currentindex)? -1 : low -1;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //http://www.geeksforgeeks.org/interval-tree/
            //http://www.cs.toronto.edu/~krueger/cscB63h/lectures/tut06.txt
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

            //Given:{{1, 3}, {2, 4}, {10, 12}, {5, 6} {5.30,7}}
            List<Puzzles.TechCompanyPage4.Appointment> appointments = new List<Puzzles.TechCompanyPage4.Appointment>();
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 01:00 PM"), ParseStringDate("06/05/2014 03:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 02:00 PM"), ParseStringDate("06/05/2014 04:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 10:00 AM"), ParseStringDate("06/05/2014 12:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 05:00 PM"), ParseStringDate("06/05/2014 06:00 PM")));
            appointments.Add(new Appointment(ParseStringDate("06/05/2014 05:30 PM"), ParseStringDate("06/05/2014 07:00 PM")));


 

  

            //convert everything to interval with low and hight

            List<Interval<DateTime>> intervals = new List<Interval<DateTime>>();

            foreach (var apt in appointments)
            {
                Interval<DateTime> interval = new Interval<DateTime>();
                interval.Low = apt.StartTime;
                interval.High = apt.EndTime;
                intervals.Add(interval);
            }

            //Imagine i need to book a appointment {11am - 12 am}..check whether there will be a conflict and then pick the one
            KarthicIntervalTree<DateTime> tree = new KarthicIntervalTree<DateTime>();
            foreach (var i in intervals)
            {
               tree.Root = tree.InsertInterval(tree.Root, i);
            }

            //search the interval
            Interval<DateTime> needtobook = new Interval<DateTime>();
            needtobook.Low = ParseStringDate("06/05/2014 11:00 AM");
            needtobook.High = ParseStringDate("06/05/2014 12:00 PM");


            Interval<DateTime> needtobook2 = new Interval<DateTime>();
            needtobook2.Low = ParseStringDate("06/05/2014 4:00 AM");
            needtobook2.High = ParseStringDate("06/05/2014 5:30 AM");


            IntervalTreeNode<DateTime> conflict = tree.SearchInterval(tree.Root, needtobook);



            string result = string.Empty;
            if (conflict == null)
            {
                result = "No conflict";
            }
            else
            {
                result = "Conflicts with the apt" + conflict.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //print all subarray and find who values is o
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);

            List<String> result = new List<string>();

            List<SubsetValue> allsubsets = GetAllSubsetsCustom(input, 0, result);

            StringBuilder sb = new StringBuilder();
            foreach (string s in result)
            {
                sb.Append("(").Append(s).Append(")").Append(",");
            }
            this.textBox11.Text = sb.ToString();

        }

        //Time Complexity: 2^n where n is the number of char in the given string
        //That is, for the first element, there are 2 choices. For the second, there are two, etc. 
        //So, doing 2 * 2 * … * 2 n times gives us 2^n subsets
        private List<SubsetValue> GetAllSubsetsCustom(int[] set, int index, List<String> zerolist)
        {
            List<SubsetValue> allsubsets = new List<SubsetValue>();
            if (index == set.Length)
            {
                //allsubsets.Add(string.Empty);
                //return allsubsets;
                allsubsets.Add(new SubsetValue(0, string.Empty));
                return allsubsets;
            }

            //This subset will be excluding current item   eg: abc    a againt {{},{c}, {b}, {bc}
            allsubsets = GetAllSubsetsCustom(set, index + 1, zerolist);

            //string currentvalue = set[index].ToString();
            SubsetValue currentvalue = new SubsetValue();
            currentvalue.list = set[index].ToString();
            currentvalue.Value = set[index];


            //Now we are creating a subset including the current item
            List<SubsetValue> newcopy = new List<SubsetValue>();
            foreach (var subset in allsubsets)
            {
                //There will be string.empty in the list and that will help to add the current
                //Add current..Each single character is a subset of the given string so add it to the list
                //concatenate the current value to all the subsets of the (current -1)
                string concat = currentvalue.list + " , "+ subset.list;
                int value = currentvalue.Value + subset.Value;

                if (value == 0)
                {
                    zerolist.Add(concat);
                }
                newcopy.Add(new SubsetValue(value, concat));
            }

            //Here we are add both including and excluding subsets
            //Important: we got the subset for currentindex..add it with the substers returned for current -1
            allsubsets.AddRange(newcopy);

            return allsubsets;
        }

        public class SubsetValue
        {

            public int Value { get; set; }
            public String list { get; set; }

            public SubsetValue(int value, string list)
            {
                this.Value = value;
                this.list = list;
            }

            public SubsetValue()
            {
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //print all subarray and find who values is o
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox16.Text);

            //List<String> result = new List<string>();

            //List<SubsetValue> allsubsets = GetAllSubsetsCustom(input, 0, result);

            //StringBuilder sb = new StringBuilder();
            //foreach (string s in result)
            //{
            //    sb.Append("(").Append(s).Append(")").Append(",");
            //}

            //note this would work..just testing to proove this won't work for negative numbers
            bool ispossible = IsSetContainsSubsetofValue(input, 0);
            this.textBox11.Text = ispossible == true ? "Yes" : "No";
 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Interval tree can be used to solve this..see below
           // http://www.geeksforgeeks.org/given-n-appointments-find-conflicting-appointments/
        }










    }
}
