using Puzzles.DataStructures.Array;
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
    public partial class GLModerateProblems_Page1 : Form
    {
        public GLModerateProblems_Page1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Assumption; The slots contain 4 characters
            int slotlength = 4;
            string actualslots = this.textBox1.Text;
            string userguess = this.textBox2.Text;
            string output = CalculateResults(actualslots.ToUpper().ToCharArray(), userguess.ToUpper().ToCharArray(), slotlength);
            this.textBox3.Text = output;

        }

        //note: we shouldn't consider the slots that are hit..
        //Time Complexity: O(2n)
        private string CalculateResults(char[] slots, char[] userguess, int SlotLength)
        {

            if (slots.Length != userguess.Length || slots.Length != SlotLength)
            {
                return "Slots Length is invalid";
            }

            int hits = 0;
            int psudeohits = 0;

            int[] frequency = new int[SlotLength]; //
            //Hits is defined as when the right slot and right color matches between actual computer slots and user guess

            //compute the number of hits
            for (int i = 0; i < slots.Length; i++)
            {
                   //if the actual slot color and user guess slot color and order are equal then it's a hit
                if (slots[i] == userguess[i])
                {
                    hits++;
                }
                else
                {
                    int value = GetValueByColor(slots[i]);
                    frequency[value]++;
                }

            }

            //compute the number of pseudo hits

            for (int i = 0; i < userguess.Length; i++)
            {
                 //check the guess mismatched values is eligible for pesudohit
                //pseudo hit is a hit when the user guessed a color and it doesn't match the actual slot but the color exsists in one of the other slots..
                //note: we shouldn't consider the slots that are hit..
                //here the frequency has value of more than 0 for the colors in the actaul slots that are not hit. Those are possible pseduo hot
                int colorvalue = GetValueByColor(userguess[i]);
                if (colorvalue >= 0 && frequency[colorvalue] > 0 && userguess[i] != slots[i])
                {
                    psudeohits++;
                    frequency[colorvalue]--;
                }
            }


            return "No of Hits : " + hits + " No of pseudohits : " + psudeohits;

        }



        private int GetValueByColor(char c)
        {
            int result = -1;
            switch (c)
            {
                case 'B':
                    result = 0;
                    break;
                case 'G':
                    result = 1;
                    break;

                case 'R':
                    result = 2;
                    break;

                case 'Y':
                    result = 3;
                    break;
              
            }

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Input: 1,2,4,7,10,11,7,12,6,7,16,18,19
            //Given an sequence of integers. Find the unsorted sequence of the given list
            //1)Find the longest increasing susbsequence at the beginning and the longest increasing subsequence at the end..
            //   Thus divide the input into three left: 1,2,4,7,10,11  - Middle 7,12   Right 6,7,16,18,19
            //2) In order to solve the problem we got to maintain left  < middle < right and the answer is the start and end of the middle
            //   but here the middle is unsorted so the right condition is =
            //  end of left < min(middle) and max(middle) < start of right
            //Important: we need to  find the min and max index by consideraing the end_left and start_right else it will not be right
            //3) To maintain the condition shrink the left and right and extend the middle untill the condition is satisfied
            //   eg:  left: 1,2,4  Middle  7, 10, 11, 7, 12,6, 7  Right: 16, 18,19
            //Answer is 7,10,11,7,12,6, 7 where m = 3 and n = 9

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
            SortResult result = FindUnSortedSequence(input);
            this.textBox7.Text = result.M.ToString();
            this.textBox8.Text = result.N.ToString();

            StringBuilder sb = new StringBuilder();
            for(int i = result.M; i <= result.N; i++)
            {
                sb.Append(input[i].ToString()).Append(",");
            }
            this.textBox4.Text = sb.ToString();

            //Test Cases

            //Input: 1,2,4,7,10,11,7,17,3,7,16,18,19
            

        }

        private class SortResult
        {
            public int M { get; set; }
            public int N { get; set; }
        }
        private SortResult FindUnSortedSequence(int[] array)
        {
            SortResult result = new SortResult();

            int end_left = FindEndofLeftSubsequence(array);

            int start_right = FindStartofRightSubsequence(array);

            //Find the min and max element of the middle unsored sequence
            int min_index = end_left - 1;

            if (min_index >= array.Length)
            {
                //array is already sorted
                return null;
            }

            int max_index = start_right + 1;
            // * Important
            //we need to  find the min and max index by consideraing the end_left and start_right else it will not be right
            //Update: 5/7/2015
            //The reason we take the end of left and start of right is end of left has the max of left sequence and start of right has the min of right sequence
            // eg: 2, 30 (5, 8, 32, 1) 15 20, 50
            //We take that in consideration of the min and max. AFter shrinking the left and right, left sequ is guarnteed to be lesser than min and right seq is guranteed to be greater than right

            for (int i = end_left; i <= start_right; i++)
            {
                if (array[i] < array[min_index])
                {
                    min_index = i;
                }

                if (array[i] > array[max_index])
                {
                    max_index = i;
                }

            }

            result.M = shrinkLeft(array, min_index, end_left);
            result.N = shrinkRight(array, max_index, start_right);


            return result;

        }


        //This function returns the index which is the end of the left subsequence where the increasing order fails
        private int FindEndofLeftSubsequence(int[] array)
        {

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1])
                {
                    return i - 1;
                }
            }

            //when code comes here it mean the entire array is already sorted
            return array.Length - 1;
        }

        //This function return the start of the right subsequence where the increasing order fails
        private int FindStartofRightSubsequence(int[] array)
        {
            for (int i = array.Length - 2; i >= 0; i--)
            {

                if (array[i] > array[i + 1])
                {
                    return i + 1;
                }
            }

            return 0;
            
        }

        //This function return the index of array after shrinking the left half..
        //Shrink from the end untill it finds a value lesser than min_index value
        private int shrinkLeft(int[] array, int min_index, int start)
        {
            int value = array[min_index];

            for(int i= start; i >= 0; i--)
            {
                if (array[i] <= value)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        //This function return the index of array after shrinking the right half..
        //Shrink from the start of the array untill it finds a value greater than max_index value
        private int shrinkRight(int[] array, int max_index, int end)
        {
            int value = array[max_index];

            for (int i = end; i < array.Length; i++)
            {
                if (array[i] >= array[max_index])
                {
                    return i - 1;
                }
            }

            return array.Length - 1;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(this.textBox5.Text);
            string output = NumberToWords.ConvertNumberToStringByIteration(number);
            this.textBox9.Text = output; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(this.textBox5.Text);
            //string output =  NumberToWords.ConvertNumberToStringByRecursion(number,"");
            string output = NumberToWords.ConvertNumberToStringByRecursionMethod2(number, new StringBuilder(), 0);
            this.textBox9.Text = output; 

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);
            SequenceSum result = FindMaxSumAndSequence2(input);
            this.textBox10.Text = result.MaxSum.ToString();
            this.textBox12.Text = result.Sequence;



        }


        //Logic: Maintain the maxsum value and sum value
        //remember to reset sum to zero when the sum value is lesser than 0 bcoz when the negative value comes as sum..if we add to another negative worse sum or to positive still lesser than positive value
        //Consider the scenario when the array contains all negative numbers..possible options 0, larger of negative or min_int..discuss with interviewer

  
        private SequenceSum FindMaxSumAndSequence(int[] array)
        {

            //we can intialize to 0 but to handle negative number, i set it to min int
            int maxsum = Int32.MinValue;
            int sum = 0;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                sum = sum + array[i];
               sb.Append(array[i]).Append(",");
 
               if (maxsum < sum)
                {
                    maxsum = sum;
     
                }

                   //dont use else
                 if (sum < 0)
                {
                    sum = 0;  //reset sum to 0 when the sum value is negativ
                    sb.Clear();
                }

            }

            SequenceSum result = new SequenceSum();
            result.MaxSum = maxsum;
            result.Sequence = sb.ToString();
            return result;
        }


        //Logic: Maintain the maxsum value and sum value
        //remember to reset sum to zero when the sum value is lesser than 0 bcoz when the negative value comes as sum..if we add to another negative worse sum or to positive still lesser than positive value
        //Consider the scenario when the array contains all negative numbers..possible options 0, larger of negative or min_int..discuss with interviewer
        private SequenceSum FindMaxSumAndSequenceWithAllNegativeHandle(int[] array)
        {

            //we can intialize to 0 but to handle negative number, i set it to min int
            int maxsum = Int32.MinValue;
            int sum = 0;
            bool skippedsequence = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                sum = sum + array[i];
                //only if the sum is greater than 0 append...ie build the sequence
                if (sum > 0)
                {
                    sb.Append(array[i]).Append(",");
                }
             
                if (maxsum < sum)
                {
                    //Handle for negative number
                    //if the maxsum is already negative and the new sum is greater than maxsum..clear the sb
                    if (maxsum < 0 || (skippedsequence == true))
                    {
                        sb.Clear();
                        sb.Append(array[i]).Append(",");
                        skippedsequence = false;
                    }
                    maxsum = sum;

                }
                if (sum < 0)
                {
                    sum = 0;  //reset sum to 0 when the sum value is negative
                    
                    //if sum is negative and maxsum is positive then we are skipped the number in sequence
                    //so we have to clear when we find the highest
                    if (maxsum > 0)
                    {
                        skippedsequence = true;
                    }

                }

            }

            SequenceSum result = new SequenceSum();
            result.MaxSum = maxsum;
            result.Sequence = sb.ToString();
            return result;
        }


        //update This is the right sln which takes care of the negative and sequence building..
        private SequenceSum FindMaxSumAndSequence2(int[] array)
        {

            //we can intialize to 0 but to handle negative number, i set it to min int
            int maxsum = Int32.MinValue;
            int sum = 0;
            StringBuilder sequence = new StringBuilder();
            string maxsequence = string.Empty;

            for (int i = 0; i < array.Length; i++)
            {
                sum = sum + array[i];
                sequence.Append(array[i]).Append(",");
             
                if (maxsum < sum)
                {
                    maxsum = sum;
                    maxsequence = sequence.ToString();
                    
                }
                else if (sum < 0)
                {
                    sum = 0;  //reset sum to 0 when the sum value is negative
                    sequence.Clear();
                }

            }

            SequenceSum result = new SequenceSum();
            result.MaxSum = maxsum;
            result.Sequence = maxsequence.ToString();
            return result;
        }

        public class SequenceSum
        {
            public int MaxSum { get; set; }
            public string Sequence { get; set; }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);
            SequenceSum result = FindMaxSumAndSequenceWithAllNegativeHandle(input);
            this.textBox10.Text = result.MaxSum.ToString();
            this.textBox12.Text = result.Sequence;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);
            //SequenceSum result = FindMaxSumAndSequenceWithAllNegativeHandle(input);

            //bad test case
            //input = new int[]{-2,-1,-5,-8,-3};

            Puzzles.DataStructures.Array.SubArraySum.SubArrayResult result = SubArraySum.maxsubArrayWithIndex(input);
            this.textBox10.Text = result.Sum.ToString();

            StringBuilder sb = new StringBuilder();
            for (int i = result.StartIndex; i <= result.EndIndex; i++)
            {
                sb.Append(input[i]).Append(',');
            }
            this.textBox12.Text = sb.ToString(); 


            
        }

 




    }
}
