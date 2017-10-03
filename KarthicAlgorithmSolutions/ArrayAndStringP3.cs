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
    public partial class ArrayAndStringP3 : Form
    {
        public ArrayAndStringP3()
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            string input = this.textBox1.Text;

            this.textBox2.Text = GetLongestPalindrome(input);

        }

        public string GetLongestPalindrome(string input)
        {
            char[] strarray = input.ToCharArray();
            List<string> palindrome = new List<string>();

            string substring = string.Empty;

            //Get all possible substring for the given string and check for palindrome

            for (int i = 0; i < strarray.Length; i++)
            {
                for (int j = i + 1; j < strarray.Length; j++)
                {
                    //All possible prefix starting from i
                    substring = input.Substring(i, (j +1) - i);
                    if (IsPalindromeMethod2(substring))
                    {
                        palindrome.Add(substring);

                    }
                }

             }

            int maxcount = 0;
            string longest = string.Empty;
            foreach (var item in palindrome)
            {

                if (item.Length > maxcount)
                {
                    maxcount = item.Length;
                    longest = item;
                }
            }

            return longest;

        }

        public bool IsPalindrome(string substring)
        {

            char[] input = substring.ToCharArray();
            Array.Reverse(input);
            string rev = new string(input);

            return (substring.Equals(rev, StringComparison.OrdinalIgnoreCase));


        }


        public  bool IsPalindromeMethod2(string value)
        {
            int min = 0;
            int max = value.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = value[min];
                char b = value[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input1 = this.textBox4.Text;
            string input2 = this.textBox5.Text;

            ArrayList al1 = new ArrayList();
            ArrayList al2 = new ArrayList();
            
            foreach (var item in input1.Split(','))
            {

                al1.Add(Convert.ToInt32(item));

            }

            foreach (var item in input2.Split(','))
            {

                al2.Add(Convert.ToInt32(item));

            }

            int[] result = MergeArray( (int[])al1.ToArray(typeof(int)), (int[]) al2.ToArray(typeof(int)));

            StringBuilder sb = new StringBuilder();

            foreach (var i in result)
            {
                sb.Append(i).Append(',');
            }

            this.textBox3.Text = sb.ToString();
        }

        public int[] MergeArray(int[] array1, int[] array2)
        {

            int[] result = new int[array1.Length + array2.Length];

            int i = 0, j = 0, k = 0;

            while (i < array1.Length && j < array2.Length)
            {
                if (array1[i] < array2[j])
                {
                    result[k] = array1[i];
                    i++;
                    k++;

                }
                else
                {
                    result[k] = array2[j];
                    j++;
                    k++;
                }
            }

            while(i < array1.Length)
            {
                result[k] = array1[i];
                i++;
                k++;
            }

            while (j < array2.Length)
            {
                result[k] = array2[j];
                j++;
                k++;
            }


            return result;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string input1 = this.textBox4.Text;
            string input2 = this.textBox5.Text;

            ArrayList al1 = new ArrayList();
            ArrayList al2 = new ArrayList();

            foreach (var item in input1.Split(','))
            {

                al1.Add(Convert.ToInt32(item));

            }

            foreach (var item in input2.Split(','))
            {

                al2.Add(Convert.ToInt32(item));

            }

            int[] result = MergeSortedArrayWithoutUsingThirdArray((int[])al2.ToArray(typeof(int)), (int[])al1.ToArray(typeof(int)));

            StringBuilder sb = new StringBuilder();

            foreach (var i in result)
            {
                sb.Append(i).Append(',');
            }

            this.textBox3.Text = sb.ToString();

        }

        public int[] MergeSortedArrayWithoutUsingThirdArray(int[] a, int[] b)
        {
            // pointer to end of the first sorted array
       
            int aindex = a.Length - 1;
            // pointer to end of the second sorted array (pointer at 100 in below array bArr)
      
            int bindex = b.Length - a.Length - 1;
            // pointer to end of the second sorted array (pointer at last 0)
            int cindex = b.Length - 1;
            /**
     * whichever is higher in two arrays, place that
     * element in last position of the bigger array
     */
            while (aindex >= 0 && bindex >=0 && cindex >=0)
            {
                //whichever is greater put it at the last
                if (a[aindex] > b[bindex])
                {
                    b[cindex] = a[aindex];
                    cindex--;
                    aindex--;

                }
                else
                {
                    b[cindex] = b[bindex];
                    cindex--;
                    bindex--;

                }
            }

            while (aindex >= 0)
            {
                b[cindex] = a[aindex];
                cindex--;
                aindex--;
            }

            // not need alread sorted
            while (bindex >= 0)
            {
                b[cindex] = b[bindex];
                cindex--;
                bindex--;
            }

            return b;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string input = this.textBox8.Text;

            this.textBox7.Text = Permute(input.ToCharArray(), 0, input.Length - 1, new StringBuilder());
           


        }

        private string Permute(char[] list, int k, int m, StringBuilder sb)
        {
            int i;
            if (k == m)
            {
                sb.Append(list).Append(',');

            }
            else
            {
                for (i = k; i <= m; i++)
                {
                    swap(ref list[k], ref list[i]);
                    Permute(list, k + 1, m, sb);
                    swap(ref list[k], ref list[i]);
                }
            }

            return sb.ToString();

        }


        private void swap(ref char a, ref char b)
        {
            if (a == b) return;
            a ^= b;
            b ^= a;
            a ^= b;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string input = this.textBox9.Text;
            ArrayList li = new ArrayList();

            foreach (var item in input.Split(','))
            {
                li.Add(Convert.ToInt32(item));

            }

            int[] inputchar = (int[]) li.ToArray(typeof (int));

            this.textBox6.Text = PrintDuplicates(inputchar);


        }

        public string PrintDuplicates(int[] input)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable ht = new Hashtable();

            foreach (int i in input)
            {
                if (ht.ContainsKey(i))
                {
                    ht[i] = (int)ht[i] + 1;
                }
                else
                {
                    ht.Add(i, 1);
                }

            }

            foreach (DictionaryEntry entry in ht)
            {
                if ((int) entry.Value > 1)
                {
                    sb.Append(entry.Key).Append(',');
                }

            }

            return sb.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string input = this.textBox9.Text;
            ArrayList li = new ArrayList();

            foreach (var item in input.Split(','))
            {
                li.Add(Convert.ToInt32(item));

            }

            int[] inputchar = (int[])li.ToArray(typeof(int));

            this.textBox6.Text = PrintDuplicateBySort(inputchar);
        }


        public string PrintDuplicateBySort(int[] input)
        {
            StringBuilder sb = new StringBuilder();
            //By default .net used merge sort alogorithm to sort. Run time 0(nlogn)
            Array.Sort(input);

            for (int i = 1; i < input.Length - 1; i++)
            {
                if (input[i] == input[i - 1])
                {
                    sb.Append(input[i]).Append(',');

                    //skip the multiple duplicate entry
                    while (i < input.Length - 1 && input[i] == input[i - 1])
                    {
                        i++;
                    }

                    //while (true)
                    //{
                    //    if (input[i] == input[i + 1] && i < input.Length - 2)
                    //    {
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}

                }
            }

            return sb.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox11.Text = GetFibonocciNumber(Convert.ToInt32(this.textBox12.Text)).ToString();
        }

        public int GetFibonocciNumber(int number)
        {
            //Fo = 0 ; f1 = 1 and  the forumula is Fn = f (n-1) + f (n-2)

            if (number <= 0)
            {
                return 0;
            }
            else if (number == 1)
            {
                return 1;
            }
            else
            {
                return (GetFibonocciNumber(number - 1) + GetFibonocciNumber(number - 2));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox11.Text = FibonacciByIteration(Convert.ToInt32(this.textBox12.Text)).ToString();

        }


        public int FibonacciByIteration(int n)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + a;
            }
            return a;
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
          //http://stackoverflow.com/questions/7043778/longest-palindrome-in-a-string-using-suffix-tree

          //create two suffix tree..
          //eg: Banana
          //forward suffix tree with chars and end with $              suffix1 =b,a,n,a,n,a, $
          //revese the chars and make another tree and end with #      suffix1= a,n,a,n,a ,b, #

          //For every suffix i in Sf, find the lowest common ancestor with the suffix n - i + 1 in Sr.

          //find the longest common substring of this string and its reverse.

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
