using Puzzles.DataStructures.KarthicHashtable;
using Puzzles.SystemDesign;
using System;
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
    public partial class TechCompany : Form
    {
        public TechCompany()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] weights = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
            int[] values = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);
            int capacity = Convert.ToInt32(this.textBox2.Text);
            //using memoizing 
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            int result = FindKnackSnapMaxValue(capacity, weights, values, 0, dictionary);
            this.textBox3.Text = result.ToString();

        }


        public int FindKnackSnapMaxValue2(int capacity, int[] weights, int[] values, int n)
        {
            //base case 
            if (n == 0 || capacity == 0)
            {
                return 0;
            }

            if (weights[n - 1] > capacity)
            {
                return FindKnackSnapMaxValue2(capacity, weights, values, n - 1);
            }

            int excludeitem = FindKnackSnapMaxValue2(capacity, weights, values, n - 1);
            int includeitem = values[n - 1] + FindKnackSnapMaxValue2(capacity - weights[n - 1], weights, values, n - 1);

            return Math.Max(excludeitem, includeitem);

        }

        public int FindKnackSnapMaxValue(int capacity, int[] weights, int[] values, int n, Dictionary<string, int> dict)
        {
            //base case 
            if (n == values.Length || capacity == 0)
            {
                return 0;
            }

            //The capacity and n is the varies. If subproblem comes with the same valuem then we can make of memoizing
            if(dict.ContainsKey(n+"-"+capacity))
            {
                return dict[n + "-" + capacity];
            }

            if (weights[n] > capacity)
            {
                //if the current n weight is greater than the capacity, skip n and go for next
                return FindKnackSnapMaxValue(capacity, weights, values, n + 1, dict);
            }

            //Get the max value excluding the current for the weight of capacity
            int excludeitem = FindKnackSnapMaxValue(capacity, weights, values, n + 1, dict);

            //Get the max value including the current for the weight of capacity
            //since we are including the current value, subtract the weight of it in the capacity and get the max value of (n+1)
            int includeitem = values[n] + FindKnackSnapMaxValue(capacity - weights[n], weights, values, n + 1, dict);

            //return the max of the including or excluding
            int result = Math.Max(excludeitem, includeitem);

            dict.Add(n + "-" + capacity, result);


            return result;

        }



      

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.textBox10.Text;

            List<string> inputset = new List<string>();
            //inputset.Add(input);

            foreach (var c in input.ToCharArray())
            {
                inputset.Add((c.ToString()));
            }


            //Method 1..during revision look at method 2
            List<List<string>> output = GetAllSubsets(inputset, 0);

            StringBuilder sb = new StringBuilder();

            foreach (List<string> coll in output)
            {

                StringBuilder sb1 = new StringBuilder();
                foreach (string s in coll)
                {
                    sb1.Append(s);

                }
                sb.Append("(").Append(sb1.ToString()).Append(")").Append(",");
                
            }

            //Method 2

            StringBuilder sb2 = new StringBuilder();
            List<string> output2 = GetAllSubsets(input.ToCharArray(), 0);

            foreach (string s in output2)
            {
                sb2.Append("(").Append(s).Append(")").Append(",");

            }

            //Method 3

            StringBuilder sb3 = new StringBuilder();
            List<string> output3 = GetAllSubsetsUsingBinaryCounter(input.ToCharArray(), 0);

            foreach (string s in output3)
            {
                sb3.Append(s).Append(",");

            }

            this.textBox7.Text = sb3.ToString();
        }

        private void PrintAllSubsets(int[] input, int index, StringBuilder sb)
        {

        }

        private List<List<string>> GetAllSubsets(List<string> set, int index)
        {
            List<List<string>> allsubsets = null;
            //if the set is empty
            if (set.Count == index)
            {
                allsubsets = new List<List<string>>();
                allsubsets.Add(new List<string>());
            }
            else
            {
                allsubsets = GetAllSubsets(set, index + 1);
                string item = set[index];
                List<List<string>> moresubsets = new List<List<string>>();
                foreach (List<string> subset in allsubsets)
                {
                    List<string> newsubset = new List<string>();
                    newsubset.AddRange(subset);
                    newsubset.Add(item);
                    moresubsets.Add(newsubset);
                }
                allsubsets.AddRange(moresubsets);
            }

            return allsubsets;
        }

        //Time Complexity: 2^n where n is the number of char in the given string
        //That is, for the first element, there are 2 choices. For the second, there are two, etc. 
        //So, doing 2 * 2 * … * 2 n times gives us 2^n subsets
        private List<string> GetAllSubsets(char[] set, int index)
        {
            List<string> allsubsets = new List<string>();
            if (index == set.Length)
            {
                allsubsets.Add(string.Empty);
                return allsubsets;
            }

            //This subset will be excluding current item   eg: abc    a againt {{},{c}, {b}, {bc}
            allsubsets = GetAllSubsets(set, index + 1);
      
            string currentvalue =set[index].ToString();
          
            //Now we are creating a subset including the current item
            List<string> newcopy = new List<string>();
            foreach (string subset in allsubsets)
            {
                //There will be string.empty in the list and that will help to add the current
                //Add current..Each single character is a subset of the given string so add it to the list
                 //concatenate the current value to all the subsets of the (current -1)
                 newcopy.Add(currentvalue + subset);
            }
        
            //Here we are add both including and excluding subsets
            //Important: we got the subset for currentindex..add it with the substers returned for current -1
            allsubsets.AddRange(newcopy);

            return allsubsets;
        }



        //Time Complexity: 2^n where n is the number of char in the given string
        //That is, for the first element, there are 2 choices. For the second, there are two, etc. 
        //So, doing 2 * 2 * … * 2 n times gives us 2^n subsets
        //http://martinm2w.wordpress.com/2012/06/04/8-3-recursion-all-subsets-of-a-set/
        //Logic:
        //When we’re generating a set, we have two choices for each element: (1) the element is in the set (the “yes” state) or
        //    (2) the element is not in the set (the “no” state).
        //This means that each subset is a sequence of yesses / nos—e.g., “yes, yes, no, no, yes, no” LIKE BITS!!!
        //eg abc (yes yes yes)
        //   ab  (yes yes no)
        //    bc (no yes yes)

        private List<string> GetAllSubsetsUsingBinaryCounter(char[] set, int index)
        {
            List<string> allsubsets = new List<string>();

            int totalsubsetsize = (int) Math.Pow(2, set.Length);
            //int counter = 0;
            //we loop through 0 to 8
            for (int counter = 0; counter < totalsubsetsize; counter++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < set.Length; j++)
                {
                    /* Check if jth bit in the counter is set to 0
            If set then pront jth element from set */
                    if ((counter & (1 << j)) == 0)
                    {
                        //if the jth bit in the counter is o then add to the set
                        sb.Append(set[j]);
                    }
                }
                string subset = "( " + sb.ToString() + " )";
                allsubsets.Add(subset);
             
            }
         
            return allsubsets;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = this.textBox10.Text;

            List<string> inputset = new List<string>();

            StringBuilder sb3 = new StringBuilder();

            List<string> output3 = GetAllSubsetsUsingBinaryCounter(input.ToCharArray(), 0);

            foreach (string s in output3)
            {
                sb3.Append(s).Append(",");

            }



            this.textBox7.Text = sb3.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {

            string input = this.textBox10.Text;

            List<string> inputset = new List<string>();

            StringBuilder sb3 = new StringBuilder();
            List<string> output3 = GetAllSubsetsUsingBinaryCounter(input.ToCharArray(), 0);

            foreach (string s in output3)
            {
                sb3.Append(s).Append(",");

            }



            this.textBox7.Text = sb3.ToString();

        }
        public int FindSubsetsMaxValue(int[] array, int index, StringBuilder sb)
        {
            if (index == array.Length)
            {
                return 0;
            }

            int excludingcurrent = FindSubsetsMaxValue(array, index + 1, sb);
            int current = array[index];

            int includingcurrent = current + excludingcurrent;

            sb.Append(current).Append(",");

            return Math.Max(excludingcurrent, includingcurrent);
           
           

        }

        public Items FindSubsetsMaxValueWithItems(int[] array, int index)
        {
            if (index == array.Length)
            {
                Items empty = new Items();
                return empty;
            }

            Items excludingcurrentItems = FindSubsetsMaxValueWithItems(array, index + 1);

            //Items current = new Items();
            //current.Value =  array[index];
            //current.Items = array[index].ToString();

            Items includingcurrentitems = new Items();
            includingcurrentitems.Value = array[index] + excludingcurrentItems.Value;
            includingcurrentitems.ItemNames = array[index].ToString() + " , " + excludingcurrentItems.ItemNames;


            if (excludingcurrentItems.Value > includingcurrentitems.Value)
            {
                return excludingcurrentItems;
            }
            else
            {
                return includingcurrentitems;
            }



        }


        private void button13_Click(object sender, EventArgs e)
        {

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            StringBuilder sb = new StringBuilder();
            int maxvalue = FindSubsetsMaxValue(input, 0, sb);
            //this.textBox8.Text = maxvalue.ToString(); we need to do extra logic to do value and its easy..no time now

            //update: 4/21/2015  I did the extra logic to get the values
            Items result = FindSubsetsMaxValueWithItems(input, 0);

            this.textBox9.Text = result.Value.ToString();
            this.textBox8.Text = result.ItemNames;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] weights = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
            int[] values = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);
            int capacity = Convert.ToInt32(this.textBox2.Text);

            var result = FindKnackSnapMaxValueWithItems(capacity, weights, values, 0);
            this.textBox3.Text = result.Max.ToString();
            this.textBox4.Text = result.Items;
        }




        public StolenItems FindKnackSnapMaxValueWithItems(int capacity, int[] weights, int[] values, int n)
        {
            //base case 
            if (n == values.Length || capacity == 0)
            {
                StolenItems item = new StolenItems();
                item.Max = 0;
                item.Items = string.Empty;
                return item;
            }

            if (weights[n] > capacity)
            {
                //if the current n weight is greater than the capacity, skip n and go for next
                return FindKnackSnapMaxValueWithItems(capacity, weights, values, n + 1);
            }

            //Get the max value excluding the current for the weight of capacity
            StolenItems excludeitem = FindKnackSnapMaxValueWithItems(capacity, weights, values, n + 1);

            //Get the max value including the current for the weight of capacity
            //since we are includeitem = new StolenItems();
            StolenItems includeitem = new StolenItems();
            includeitem = FindKnackSnapMaxValueWithItems(capacity - weights[n], weights, values, n + 1);
            includeitem.Max = values[n] + includeitem.Max;



            StolenItems currentitem = new StolenItems();

            //return the max of the including or excluding

            if (excludeitem.Max > includeitem.Max)
            {
                currentitem.Items = excludeitem.Items;
                currentitem.Max = excludeitem.Max;

            }
            else
            {
                currentitem.Items = includeitem.Items + " , " + values[n];
                currentitem.Max = includeitem.Max;
            }
            return currentitem;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] weights = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
            int[] values = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);
            int capacity = Convert.ToInt32(this.textBox2.Text);

            //int[] weights = new int[] { 3,2,1};
            //int[] values = new int[] { 5, 3, 4 };
            //int capacity = 5;
            int rowno = 3;
            int colno = 5;
            int[,] test = new int[rowno, colno];

            

            int result = FindKnackSnapMaxValueUsingDP(weights, values, capacity);
            this.textBox3.Text = result.ToString();
        }

        //Logic of this Dynamic Program
        //We iterate for each unit of the weight againt each item 
        //As usual put the first row and column as zero (if there is no weight we can't put any item or if there is no item, there is no value)
        //If the item cannot be put into the bag of the weight(w), then put zero
        //if the item can fit into bag then take two values 
        //value1  - one the cell about it cell[row -1, col]
        //and value2 - value of the item + (if there are any weight left, then find the left unit for this item and get the value from the row above row  m[row -1, [weightleft]
        //set max of value1 and value2
        //continue till last 
        //last bottom right will be the answer and that is the maxvalue for using all weight and for all items
        //https://www.youtube.com/watch?v=EH6h7WA7sDw
        public int FindKnackSnapMaxValueUsingDP(int[] weight, int[] values, int capacity)
        {

            //Declare an array with one index greater than given
            //Let's m the x-axia as weight and y axis as value
            int noofitems = weight.Length;
            int[,] maxarray = new int[ noofitems + 1 ,capacity + 1];

            // Build table K[][] in bottom up manner
            //noofitem = value.Length...not index
            //iterate for ech item
            for (int i = 0; i <= noofitems; i++)
            {
                //each unit of weight
                for (int j = 0; j <= capacity; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        maxarray[i, j] = 0;
                    }  
                    //check the item can fit into the bag of capacity j
                    //int[] weight is index based and we are iterating via length based
                    else if (weight[i - 1] <= j)
                    {
                        //it can fit into the bag
                        //excluding this item
                        int value1 = maxarray[i - 1, j];
                        //including this item
                        int value2 = values[i-1] + maxarray[i - 1, (j - weight[i - 1])];
                        maxarray[i, j] = Math.Max(value1, value2);
                    }
                    else
                    {
                        //cannot fit
                        maxarray[i, j] = 0;
                    }

                }

            }


            return maxarray[ noofitems,capacity];



        }

        private void button7_Click(object sender, EventArgs e)
        {

            int[] weights = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
            int[] values = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);
            int capacity = Convert.ToInt32(this.textBox2.Text);

            //int[] weights = new int[] { 3, 2, 1 };
            //int[] values = new int[] { 5, 3, 4 };
            //int capacity = 5;


            var result = FindKnackSnapMaxValueWithItemsUsingDP(weights, values, capacity);
            this.textBox3.Text = result.Max.ToString();
            this.textBox4.Text = result.Items;
     

        }


        //Logic of this Dynamic Program
        //We iterate for each unit of the weight againt each item 
        //As usual put the first row and column as zero (if there is no weight we can't put any item or if there is no item, there is no value)
        //If the item cannot be put into the bag of the weight(w), then put zero
        //if the item can fit into bag then take two values 
        //value1  - one the cell about it cell[row -1, col]
        //and value2 - value of the item + (if there are any weight left, then find the left unit for this item and get the value from the row above row  m[row -1, [weightleft]
        //set max of value1 and value2
        //continue till last 
        //last bottom right will be the answer and that is the maxvalue for using all weight and for all items

        //create an another array to keep track of all the matches- this will be used to backtrack the items
        //with the backtrack array you can check whether each item is added. It a item is added subtract it's weight and check the next item for the remaining weight

        //https://www.youtube.com/watch?v=EH6h7WA7sDw
        public StolenItems FindKnackSnapMaxValueWithItemsUsingDP(int[] weight, int[] values, int capacity)
        {

            //Declare an array with one index greater than given
            //Let's m the x-axia as weight and y axis as value
            int noofitems = weight.Length;
            int[,] maxarray = new int[noofitems + 1, capacity + 1];
            int[,] backtrack = new int[noofitems + 1, capacity + 1];

            // Build table K[][] in bottom up manner
            //noofitem = value.Length...not index
            //iterate for ech item
            for (int i = 0; i <= noofitems; i++)
            {
                //each unit of weight
                for (int j = 0; j <= capacity; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        maxarray[i, j] = 0;
                    }
                    //check the item can fit into the bag of capacity j
                    //int[] weight is index based and we are iterating via length based
                    else if (weight[i - 1] <= j)
                    {
                        //it can fit into the bag
                        int value1 = maxarray[i - 1, j];
                        int value2 = values[i - 1] + maxarray[i - 1, (j - weight[i - 1])];
                        maxarray[i, j] = Math.Max(value1, value2);

                        //logic for backtrack array..
                        //if the item is placed in the bag then mark 1 else 0 ( by default it is zero)
                        if (maxarray[i, j] == value2) //which means this is placed in the bag
                        {
                            backtrack[i, j] = 1;
                        }
                    }
                    else
                    {
                        //cannot fit
                        maxarray[i, j] = 0;
                    }

                }

            }

            StolenItems items = new StolenItems();
            items.Max = maxarray[noofitems, capacity];
            
            //We need to find the items
           //keep track of remaining capacity in the bag
            int w = capacity;
            bool[] itemsused = new bool[noofitems];

            //check whether each item can be added from last item
            //i = 3,2,1
            for (int i = noofitems; i > 0; i--)
            {
                if (backtrack[i, w] == 1)
                {
                    //we are keeping this item in the bag
                    itemsused[i - 1] = true;
                    //substract actual weight of the item
                   // w = w - 1;
                    w = w - weight[i - 1];
                }
                else
                {
                    itemsused[i - 1] = false;
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < itemsused.Length; k++)
            {
                if (itemsused[k])
                {
                    sb.Append(values[k]).Append(",");
                }

            }


            //test this logic use this logic in real world

            StringBuilder sb2 = new StringBuilder();
            //values will be 0,1,2
            int runningcapacity = capacity;
            for (int j = 0; j < values.Length; j++)
            {
                //backtrack array has one extra item so the corresponding value will be j + 1

                if (backtrack[j + 1, runningcapacity] == 1)
                {
                    sb2.Append(values[j]).Append(",");
                    runningcapacity = runningcapacity - weight[j];
                }

            }
          
            //check sb is equal to sb2
            bool test = sb2.Equals(sb);


            items.Items = sb.ToString();


            return items;

        }

        private void TechCompany_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

            //Logic:
            //There are 2^32 possible intergers = 4 Billion integers
            //Given memory 1 GB = 1023 MB = 10 pow 3 mb = 10 pow 6 kb = 10 pow 9 bytes = 8 * 10 pow 9 = 8 billion bits
            //We are not going to store int32 value as you know each int needs 32 bits which willbe 32 * 4 billion bits
            //We can create 4 billion bits and represent each bit for each int  
            //Creating 4 billion bits will only take 512mb memory

            //Initialize all 4 billion bits to 0 (by default)
            //Read the file and for each number n..find the nth bit set it to 1..duplicates shouldn't be a problem
            // after done
            //read the bits and return the first bit that has 0

            //update:
            //Unfortunately we don't have datatype to store bits but we have byte which is of 8 bits
            //so consider 1 bit  as index 1 in byte 1  000000001
            //byte[0] will have  0 to 7
            //byte[1] will have  8 to 15
            //byte[2] will have  16 to 23
            //....untill byte[4billion/8] will have x---4billion
            //for simplity imagine the file has number 0 to 10

            //we should have 4 billion positive interger..C# has only 2 billion integers
            //+1 is bcoz index are 0 based .. number 8 will have 2 byte[0] and byte[1]
            long numberofints = (long)Int32.MaxValue + 1;
            //we are using bytes array not bit..so update the nth bits on the bytes array
            //4 billion number need 4 billion bits = 4 billion/8 bytes
            //we can get number bytes array length can be set like this
            //byte[] bitfield = new byte[(int)(numberofints / 8)];


            //But here we are doing this logic test with small set of numbers

            int size = Convert.ToInt32(this.textBox16.Text);
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);

            //this.textBox8.Text = maxvalue.ToString(); we need to do extra logic to do value and its easy..no time now
            this.textBox13.Text = FindFirstMissingNumberUsingBytesLogic(size, input).ToString();
          

        }

        public int FindFirstMissingNumberUsingBytesLogic(int size, int[] input)
        {
         
            //we are using bytes array not bit..so update the nth bits on the bytes array
            //4 billion number need 4 billion bits = 4 billion/8 bytes
            byte[] bitfield = new byte[(int)Math.Ceiling((double)size / 8)];
            int returnvalue = -1;
            //parse the file and read input...for simplity imagine the file has number 0 to 10
            //Iterate from number 1 to 20
            for (int i = 0; i < input.Length; i++)
            {
                /*
                here we need to set the ith bit to 1...
                Example no: 10    we know byte[0] will have  0 to 7 and byte[1] will have  8 to 15
                we need to byte[1] and updates it's 2 index byte from right
                0000 0000  we need to change this to 
                0000 0100  
                
                To do this 
                 1<< (2) = shifts 2 index in left will be 0000 0100  
                we are making or operating with the existing value
                0000 0100 or
                existingvalue

                update
                 we need [i/8] to find the which array of bytes has the value we need
                 we need (i % 8) to find the bit of the byte array that need to be set
                */

                
                int number = input[i];
                int bitposition = number - 1;//bit position is used to store the number 1 is oth position so that we don't waste oth position else it will be stored in 1st position

                int value = bitfield[bitposition / 8] | 1 << (bitposition % 8);
                //i/8 to get the right byte
                bitfield[bitposition / 8] = (byte)value;
            }

            //we are done reading the file..return the first empty or zero value


           //here iterate from 0 to 4billion/8
            for (int i = 0; i < bitfield.Length; i++)
            {
                string bitdigits = Convert.ToString(bitfield[i], 2);

                for (int j = 0; j < 8; j++)
                {

                  
                    //retrive the individual bits of each byte
                    //when 0 if found retur the correspoding bit
                    if ((bitfield[i] & (1 << j))== 0)
                    {

                        //get the actual int value
                        returnvalue = i * 8 + j;
                        return returnvalue + 1;
                       

                    }

                }
            }

            //because we store the number 1 in oth index
            return -1;
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Assumption: The numbers are unique
            //As we know there are 4 billion integers...
            //As like the previous step we can create 4 billion bits that is 4 billion / 8 bytes bcoz 4 billion bits is like 512 mb

            /* You are given 10 mb of memory and we have a find the missing interger from the file that contain 1 billion integers
             * The values are distinct and the intergers are not consecutive
             * 
             * 
             * Previous Problem:
             *     In previous problem we had 1 GB memory = 1000 mb = 10 pow 6 kb = 10 pow 9 bytes = 8 * 10 pow 9 bits = 8 billion bits
             *     Since we had 8 billion bits we are able to store 4 billion integers with each bit corresponding to each integer
             * 
             * But here
             * 
             *
             *     We have now 10 Mb memory = 10 * 10 pow 3 kb = 10 * 10 pow 6 bytes = 10 pow 7 bytes = 8 * 10 pow 7 bits..
             *     The point is we don't have billion bits memory to store billion integers
             *     
             * Here is the approach:
             * 
             * Divid the dataset into subproblems. Divide the data into blocks.
             * We iterative the file 2 times
             * STEP: 1
             *          We scan the file first time and find which block has the missing element
             * STEP: 2
             *        We scan the file second time and find the missing number in that particular block

             * Eg: we can have 5 blocks and each block can store 20 elements. Total 100 elements
             *   block[0] = 0 to 19
             *   block[1] = 20 to 39
             *   ...
             *   block[4] = 80 to 99
             * 
             * We need two things Size of the block and range of the block
             *     Size of the block * Range of the block = Total non-negative integers (2 pow 31)
             *
             * Though we have only 1 billion integers as input, the missing can be any of the non-negative integers
             *       Size of the block * Range of the block = 2 pow 31
             *       
             *  The Size of the block should fit the given memory capacity
             *  Range of the block will be the size of the byte array in the second step. so that should fit in the given memory as well
             *  
             * Size of the block:
             *            We are storing the int value in the block. eg block[0] = 15 mean it has 15 elements 
             *            An integer is a 32 bit value that is 4 bytes. Each block has 4 bytes
             *            Given 10 mb can have 2 pow 23 bytes.
             *            block size will be  (2 pow 23 )/ 4 that is 2 pow 21     
             *            Block size should cannot exceed 2 pow 21 other it will be out of the given 10 mb memeory
             *            
             * Size of the block =  (Total integers)       2 pow 31            2 pow 21  >= 2 pow 31
             *                      ----------------   =  ---------                        ----------
             *                          Range of block     Range of block                   Range of block
             *                          
             * 
             * Range of Block  >= 2 pow 31
             *                   --------       = Range of block >= 2 pow 10
             *                     2 pw 21
             *
             * On Step 2, the size of block will become size of the byte array
             * We are going to represent each bit as 1 integer
             * 10 mb of memory has 2 pow 23 bytes = 8 * 2 pow 23 = 2 pow 26 bits
             * We can use the at the max of 2 pow 26 bits
             * 
             * Range size can be between 2 pow 10 and  2 pow 26
             * 
             * Choose any value as the range size continue the problem
             * 
             * 
             *     
             * */

            //total element 50
            //simulation of input
            int[] input = new int[50]; //0 to 49
            for (int i = 0; i < 50; i++)
            {
                input[i] = i;
            }
            //set the missing element
            //24
            input[24] = 0;

            // now the input will have 1 to 50 with 24 missing 


            int blocksize = 5;
            int rangesize = 10;

            int result = FirstMissingIntegerSpaceEffiecient(input, blocksize, rangesize);




        }

        private int FirstMissingIntegerSpaceEffiecient(int[] input, int blocksize, int rangesize)
        {
            int[] blocks = new int[blocksize];


            //read the file and increment the corresponding block
            foreach (int num in input)
            {
                //num 0 to 9 will be in block[o] 
                //num 10 to 19 will be in block[1]
                blocks[num / rangesize] = blocks[num / rangesize] + 1;

            }


            //Find which block has the missing number
            int missingblock = -1;
            for (int j = 0; j < blocks.Length; j++)
            {
                if (blocks[j] < rangesize)
                {
                    missingblock = j;
                }

            }

            //You will find the missing once that is found do the logic in previous problem to retrive the missing number
            //When u scan the file for second time, consider only the number in the missing blcok..skip all other elements

            byte[] bytearray = new byte[(rangesize / 8) + 1];
            //scan the file again
            for (int n = 0; n < input.Length; n++)
            {
                //consider only the no is the missing block
                int blockno = input[n] / rangesize;
                if (blockno == missingblock)
                {
                    //here we consider only the missing block
                    int virtualnumber = input[n] % rangesize;
                    int bitlocation = virtualnumber % 8;

                    //we set the corresponding bit
                    int value = bytearray[virtualnumber / 8] | (1 << bitlocation);

                    bytearray[virtualnumber / 8] = (byte)value;
                }

            }


            //here iterate from 0 to 4billion/8
            for (int i = 0; i < bytearray.Length; i++)
            {
                string bitdigits = Convert.ToString(bytearray[i], 2);

                for (int j = 0; j < 8; j++)
                {

 //retrive the individual bits of each byte
                    //when 0 if found retur the correspoding bit
                    if ((bytearray[i] & (1 << j)) == 0)
                    {

                        //get the actual int value
                        return i * 8 + j + (missingblock * rangesize);

                    }

                }
            }

            return -1;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string version1 = this.textBox15.Text;
            string version2 = this.textBox14.Text;
            int result = CompareVersion(version1, version2);
            string output = result == 0 ? "Equal" : result == 1 ? "Version 1 is greater" : "Version 2 is greater";

            this.textBox12.Text = output;
        }

        //Assumption:
        //My assumption was wrong initially
        //1.9 Vs 1.10   - 1.10 is greater version
        //9.9 vs 10.0   - 10.0 is greater vestion
        //1.2.3 vs 1.2.3.4  - 1.2.3.4 is the greater version

        //split the string with demlimitor
        //find the first unequal part in the array and convert to int and compare
       
        public int CompareVersion(string version1, string version2)
        {
           //The reason v1 and v2 is string[] is the version can also have alphabets
            //Test cases: v1 1.2.3 and v2 1.2.3.4
            //2)   1.9  and 1.10
            //3)   1.923 and 1.919
            string[] v1 = version1.Split('.');
            string[] v2 = version2.Split('.');

            //compare the split array
            //we can't decide by the length of the array bcoa 2.0 is bigger than 1.9.4.3

            int i = 0; 
            //increase the counter untill they are equal
            while (i < v1.Length && i < v2.Length && v1[i] == v2[i])
            {
                i++;
            }

            //when code comes here we will be in the first unequal value or when one reaches end
            // compare first non-equal ordinal number
            if (i < v1.Length && i < v2.Length)
            {
                //Update 7/13/2015 ..Note: we are comparing two int objects 
                //

                int diff = Convert.ToInt32(v1[i]).CompareTo(Convert.ToInt32(v2[i]));
                return diff; ///-1 means v2 is greater, 1 mean v1 is greater

            }
            // the strings are equal or one string is a substring of the other
            // e.g. "1.2.3" = "1.2.3" or "1.2.3" < "1.2.3.4"
            else
            {
               //This means we have equal version value till the end of one version
                int diff = Math.Sign(v1.Length - v2.Length);
                return diff;
            }


            
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
          
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            int size = Convert.ToInt32(this.textBox17.Text);
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox19.Text);

            //this.textBox8.Text = maxvalue.ToString(); we need to do extra logic to do value and its easy..no time now

            List<int> result = FindDuplicatesUsingBitLogic(input, size);
            StringBuilder sb = new StringBuilder();

            foreach(int value in result)
            {
                sb.Append(value).Append(",");
            }
            this.textBox18.Text = sb.ToString();
        }

        private List<int> FindDuplicatesUsingBitLogic(int[] input, int size)
        {
            //we are using bytes array not bit..so update the nth bits on the bytes array
            //4 billion number need 4 billion bits = 4 billion/8 bytes
            byte[] bitfield = new byte[(int)Math.Ceiling((double)size / 8)];
            List<int> duplicates = new List<int>();
            //parse the file and read input...for simplity imagine the file has number 0 to 10
            //Iterate from number 1 to 20
            for (int i = 0; i < input.Length; i++)
            {
                /*
                here we need to set the ith bit to 1...
                Example no: 10    we know byte[0] will have  0 to 7 and byte[1] will have  8 to 15
                we need to byte[1] and updates it's 2 index byte from right
                0000 0000  we need to change this to 
                0000 0100  
                
                To do this 
                 1<< (2) = shifts 2 index in left will be 0000 0100  
                we are making or operating with the existing value
                0000 0100 or
                existingvalue

                update
                 we need [i/8] to find the which array of bytes has the value we need
                 we need (i % 8) to find the bit of the byte array that need to be set
                */


                int number = input[i];
                int bitposition = number - 1;//bit position is used to store the number 1 is oth position so that we don't waste oth position else it will be stored in 1st position
                int value = bitfield[bitposition / 8] | 1 << (bitposition % 8);

                //If the particular bit is already set to 1 then or will not change the value of the original
                if (value == bitfield[bitposition / 8])
                {
                    //It is already set so duplicate
                    duplicates.Add(number);

                }
                else
                {
                    //set the pit position to 1
                  
                    //i/8 to get the right byte
                    bitfield[bitposition / 8] = (byte)value;
                }
            }
            return duplicates;
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
       
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox20.Text);

            //this.textBox8.Text = maxvalue.ToString(); we need to do extra logic to do value and its easy..no time now

            this.textBox22.Text = FindSingleMissingNumberInConsecutiveNumbersint(input).ToString();
        }


        // 1. Get the sum of numbers 
        //       total = n*(n+1)/2
        //2  Subtract all the numbers from sum and
        //   you will get the missing number.
        private int FindSingleMissingNumberInConsecutiveNumbersint(int[] input)
        {

            //total = n * (n+1) /2
            //If we don't know whether consecutive numbers start from 0 or 1 get the last item value
            int n = input[input.Length - 1];
            int total = (n * (n + 1)) / 2;

            foreach (int value in input)
            {
                total = total - value;
            }

            return total;
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

      




    }

        public class StolenItems
        {
              //public int Cost { get; set; }
              //public int Weight { get; set; }

              public int Max { get; set; }
              public string Items { get; set; }
        }

        public class Items
        {
            public int Value { get; set; }
            public string ItemNames { get; set; }
        }
}
