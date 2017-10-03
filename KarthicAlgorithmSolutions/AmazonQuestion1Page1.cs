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
    public partial class AmazonQuestion1Page1 : Form
    {
        public AmazonQuestion1Page1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = this.textBox4.Text;
            //Solution Logic:
            //1) We read the input line by line and convert each space seperated line into array of intergers by ignoring the invalid input (as given)
            //2) We sort the array using merge sort algorithm which takes o(nlogn) time complexity and o(n) space complexity to sort the array
            //3) Once the array is sorted, the duplicate integers will be adjacent to each other so, we do linear comparison in place to remove the duplicates which takes
            // O(n) time to remove duplicates.
            //4) Once the duplicates are removed we print the output
            //Complexity:
            //Time Complexity: O(nlogn) + O(n) = O(nlogn)
            //Space Complexity: O(n)

            //Solution 2: We can also tweak the merge sort to handle the delete duplicate value logic to remove duplicates while sorting 
            //that will take same time complexity O(nlogn).To maintain the code clean i have used the method 1 described above

            //Assumption:
            //1) We take multiple lines in input by using console.Readline() in loop.
            //2) As per the rquirement, the program should ignore invalid input so, error is ignored instead of throwing an exception
           
            string[] lines = input.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = SanitizeNumbers(lines);

            StringBuilder sb = new StringBuilder();
            foreach (string line in list)
            {
                sb.Append(line).Append(Environment.NewLine);
            }

            this.textBox2.Text = sb.ToString();
        }


        //This method takes string array of lines and returns the sorted and distinct numbers list
        public List<string> SanitizeNumbers(string[] lines)
        {
            List<string> outputlist = new List<string>();
            foreach (string line in lines)
            {

                int[] numbers = GetNumberArrayByParsingEachLine(line);
                //sorting is done in 0(n log n) complexity 
                MergeSort(numbers);
                //removing duplicates is done in linear time by comparing adjacent values. Time complexity 0(n)
                int[] DistinctSortedNumber = RemoveDuplicatesInSortedArray(numbers);
                string sortednumberline = ConvertIntArrayToSpaceSeparatedString(DistinctSortedNumber);
                outputlist.Add(sortednumberline);
            }

            return outputlist;

        }

        //This method parses each lines and return array of numbers. 
        private int[] GetNumberArrayByParsingEachLine(string line)
        {
            //Remove Empty entries will remove all the empty spaces between numbers
            string[] linecontent = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int[] numbers = new int[linecontent.Length];
            int counter = 0;
            //process numbers in each line
            foreach (string number in linecontent)
            {
                int parsedInt = 0;
                if (int.TryParse(number, out parsedInt))
                {
                    //if the input is valid number
                    numbers[counter] = parsedInt;
                    counter++;
                }
                // else
                // {
                //If the input is invalid(if it is not an number), we can catch and throw the exception here
                //Since the question says to ignore the invalid entry without stopping the processing, I'm commenting the else statement
                //throw new Exception("Invalid Input");
                // }

            }

            Array.Resize<int>(ref numbers, counter);
            return numbers;


        }

        //Merge Sort: Sorts the array in O(n log n) time complexity and O(n) space in worst case
        private int[] MergeSort(int[] input)
        {
            int[] helper = new int[input.Length];
            MergeSort(input, helper, 0, input.Length - 1);
            return input;

        }

        //Divide the array into max possible units and apply merge
        private void MergeSort(int[] array, int[] helper, int low, int high)
        {
            if (low < high)
            {
                int middle = (low + high) / 2;
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
            //remaining is done only for left..it is not required for right bcoz it is already there
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


        //Remove duplicates is done in o(n) complexity  in place
        //I didn't create a new array, we keep track of the output index and use the original array to overwrite the duplicate values
        private int[] RemoveDuplicatesInSortedArray(int[] array)
        {
            int outputindex = 1;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != array[i - 1])
                {
                    array[outputindex] = array[i];
                    outputindex++;
                }
            }

            Array.Resize<int>(ref array, outputindex);
            return array;

        }


        private string ConvertIntArrayToSpaceSeparatedString(int[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in array)
            {
                sb.Append(i).Append(" ");
            }
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
