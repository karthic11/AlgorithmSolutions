using System;
using System.Collections;
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
    public partial class ArrayAndStringsP4 : Form
    {
        public ArrayAndStringsP4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = this.textBox1.Text.Split(',');

            this.textBox2.Text = String.Join(",", SortToMakeAnagramsCloser(input));



        }

        public string[] SortToMakeAnagramsCloser(string[] input)
        {

            Array.Sort(input, new AnagramComparator());

            return input;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] input = this.textBox1.Text.Split(',');

            this.textBox2.Text = String.Join(",", SortToMakeAnagramsCloserUsingHashTable(input));


        }


        public string[] SortToMakeAnagramsCloserUsingHashTable(string[] input)
        {
            Hashtable ht = new Hashtable();
            List<string> list = null;
            foreach (var s in input)
            {
                string key = SortHelper.SortChars(s);
               
                if (ht.ContainsKey(key))
                {
                    list = (List<string>) ht[key];
                    list.Add(s);
                }
                else
                {
                    list = new List<string>();
                    list.Add(s);
                    ht.Add(key, list);
                }
                 
            }

            string[] output = new string[input.Length];
            int i = 0;
        
            foreach (List<string> list1 in ht.Values)
            {
                foreach (string s in list1)
                {
                    output[i] = s;
                    i++;

                }

            }

            return output;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] input = this.textBox4.Text.Split(',');
            int[] array = new int[input.Length];
            int index = 0;
            foreach (string s in input)
            {
                array[index] = Convert.ToInt16(s);
                index++;
            }
            int x = Convert.ToInt32(this.textBox3.Text);
            int output = SearchSortedandReversedArray(array, 0, array.Length - 1, x);

            this.textBox5.Text = output.ToString();


        }

        //In case of sorted and reversed array either left or right might be normallly ordered (small to large) find the normally ordered side
        //With that find where to begin the search
        //if both the
        public int SearchSortedandReversedArray(int[] array, int left, int right, int x)
        {

            int middle = (left + right)/2;
            if (array[middle] == x)
            {
                return middle;
            }

            if (right < left)
            {
                return -1;
            }

           
            if (array[left] < array[middle])
            {
                //left side is normally ordered

                if (x >= array[left] && x <= array[middle])
                {
                    //x lies between left and middle...so search in left
                    //middle is already searched
                    return SearchSortedandReversedArray(array, left, middle - 1, x);
                }
                else
                {
                    //search right
                    return SearchSortedandReversedArray(array, middle + 1, right, x);
                }

            }
            else if (array[middle] < array[right])
            {
                //right side is normally ordered
                if (x >= array[middle] && x <= array[right])
                {
                    //x lies between middle and right...so search in right
                    //middle is already searched
                    return SearchSortedandReversedArray(array, middle + 1, right, x);
                }
                else
                {
                    //search left
                    return SearchSortedandReversedArray(array, left, middle - 1, x);
                }

                
            }
            else if (array[left] == array[middle])
            {
                //left and middle are same, we cannot determine the normally ordered
                //so check the right and middle

                if (array[middle] != array[right])
                {
                    //search right
                   return  SearchSortedandReversedArray(array, middle + 1, right, x);

                }
                else
                {
                    //search on both sides
                    int result = SearchSortedandReversedArray(array, left, middle - 1, x);
                    if (result == -1)
                    {
                        //search on right
                        return SearchSortedandReversedArray(array, middle + 1, right, x);
                    }
                    else
                    {

                        return result;
                    }
                }

            }

            return -1;


        }

        private void button2_Click(object sender, EventArgs e)
        {
          string[] input = this.textBox8.Text.Split(',');
          int[] array = new int[input.Length];
          int index = 0;
          foreach (string s in input)
          {
            array[index] = Convert.ToInt16(s);
            index++;
          }
          int[] test = new int[0];

          this.textBox6.Text = FindFirstBreakValueInMonotonicallyIncreasingSequence(array, array.Length).ToString();


        }

      // Microsoft interview question to debug this logic
      //First check for the array empty. If the given array is empty (size o) throw error
      //First check the i < size -1 before making the check for increasing sequence else index out of bound will occur
      //The code will go to the return statement if either one of the condition is false
      //If the condition i >- Size -1 is true return the last element else return the element that broke the sequence

        public int FindFirstBreakValueInMonotonicallyIncreasingSequence(int[] p, int size)
        {

          if (size == 0)
          {
            //The array is empty
            throw new Exception("Input array sequence is empty");
          }

          int i = 0;
          while (i < size - 1 && p[i + 1] >= p[i])
        
            ++i;
       
          return (i >= size -1 ) ? p[size -1 ] : p[i + 1];
            
        }

        public int FindFirstBreakIndexInMonotonicallyIncreasingSequence(int[] p, int size)
        {
          if (size == 0)
          {
            //The array is empty
            throw new Exception("Input array sequence is empty");
          }

          int i = 0;
          while (i < size - 1 && p[i + 1] >= p[i])
          {
            ++i;
          }

          return (i >= size - 1) ? size - 1 : i + 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {


          int input = Convert.ToInt16(this.textBox9.Text);

          this.textBox7.Text = findthelogic(input).ToString();
          

        }


        public long findthelogic(long a)
        {

          int c = 0;
          if (a != 0)
          {
            do
            {
              ++c;
            } while ((a = a & (a - 1)) == 0);
          }

          return c;
        }

       

    }
}
