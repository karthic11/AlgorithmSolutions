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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool IsUnique;
            IsUnique = IsStriKarthicnique(this.textBox1.Text);

            this.textBox2.Text = (IsUnique) ? "Unique" : "Not Unique";

        }


        public bool IsStriKarthicnique(string input)
        {
            string s = input.ToLower();
            char[] characters = s.ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                if (i != s.LastIndexOf(characters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool IsUnique;
            IsUnique = IsStriKarthicniqueByht(this.textBox1.Text);

            this.textBox2.Text = (IsUnique) ? "Unique" : "Not Unique";

        }

        public bool IsStriKarthicniqueByht(string input)
        {

            Hashtable ht = new Hashtable();

            foreach (char c in input.ToCharArray())
            {
                if (ht.Contains(c))
                {
                    return false;
                }
                else
                {
                    ht.Add(c, 1);
                }
            }

            return true;
          
        }

        private void button3_Click(object sender, EventArgs e)
        {

            bool IsUnique;
            IsUnique = IsStriKarthicniqueByAsciiValue(this.textBox1.Text);

            this.textBox2.Text = (IsUnique) ? "Unique" : "Not Unique";



        }

        //Time: O(n) and Space O(n) or O(1) not sure
        //Assumption: The input string contains only ascii string not unicode string
        //Ascii string contains only ascii characters like a-z,A_Z.1-9 and symbols. There are 127 ascii characters and most system use 256 bits for ascii
        //Unicode string contains language specific character it uses twe byts one for langulage info and other for char info..There are around 107,000 unicode chars
        public bool IsStriKarthicniqueByAsciiValue(string input)
        {

           // Boolean[] holder = new Boolean[256];
           //we set size to 256 bcoz there are 256 possible asii characters
            //Array[] holder = new Array[256];

          //In the ASCII character set, each binary value between 0 and 127 is given a specific character. 
          //Most computers extend the ASCII character set to use the full range of 256 characters available in a byte. The upper 128 characters handle special things like accented characters from common foreign languages.

            //foreach (char c in input.ToCharArray())
            //{
            //    if (holder[c])
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        holder[c] = true;
            //    }
            //}


            //check for the length
            if (input.Length > 256)
            {
                //bcoz there will be duplicates for sure
                //There are only 256 ascii characters
                return false;

            }
            Boolean[] holder = new Boolean[256];

            for (int i = 0; i < input.Length; i++)
            {
                //This int will get the Ascii value of the char
                int val = input[i];
                //char val = input.ToCharArray()[i];

                if (holder[val])
                {
                    return false;
                }
                else
                {
                    holder[val] = true;
                }

            }

            return true;
        }

        //Time: O(n) and Space O(1)
        public static bool isUniqueCharsbybitvector(String str)
        {
            int checker = 0;
            foreach (char c in str.ToCharArray())
            {
                int val = c - 'a';
                if ((checker & (1 << val)) > 0)
                {
                    return false;
                }
                else
                {
                    checker |= (1 << val);
                }
            }
            return true;
        }


        private void button4_Click(object sender, EventArgs e)
        {


            bool IsUnique;
            IsUnique = isUniqueCharsbybitvector(this.textBox1.Text);

            this.textBox2.Text = (IsUnique) ? "Unique" : "Not Unique";

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            string s = this.textBox4.Text;
            this.textBox3.Text = ReverseString(s);

        }

        public string ReverseString(string input)
        {

            if (input == string.Empty)
            {
                return string.Empty;
            }

            char[] chars = input.ToCharArray();
            //This work by 0(n) time
            Array.Reverse(chars);
            return new String(chars);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = this.textBox4.Text;
            this.textBox3.Text = ReverseStringBySb(s);
        }

        public string ReverseStringBySb(string input)
        {
            StringBuilder sb = new StringBuilder();
            if (input == string.Empty)
            {
                return string.Empty;
            }

            char[] chars = input.ToCharArray();

            for (int i = chars.Length - 1; i >= 0; i--)
            {
                sb.Append(chars[i]);
            }

            return sb.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = this.textBox4.Text;
            this.textBox3.Text = ReverseStringByXOR(s);

        }
        //http://www.dotnetfunda.com/codes/code1687-reverse-of-a-string-without-using-string-reverse-function-alternate-tips.aspx

        public string ReverseStringByXOR(string input)
        {
         
            //xor rule..To switch two value follow the the three operations
            //1) x1 = x1^x2 a
            //2)X2 = x2^x1 
            //3)x1 = x1^x2

            int lastindex = input.Length - 1;
            char[] chars = input.ToCharArray();
            for (int i = 0; i < lastindex; i++, lastindex--)
            {

                //we have to switch input[i] and input[j]
                chars[i] ^= chars[lastindex];
                chars[lastindex] ^= chars[i];
                chars[i] ^= chars[lastindex];

            }
            return new string(chars);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = this.textBox4.Text;
            this.textBox3.Text = ReverseStringBySwitch(s);

        }


        public string ReverseStringBySwitch(string input)
        {

            //To reverse a string we have to switch a string's first char to last and last to first
            //The swtich can be done using temp variable or by using xor or by using the input (Good performance
            //char[] chars = input.ToCharArray();

            //for (int i = 0, j = chars.Length - 1; i < j; i++, j--)
            //{
            //    char temp = chars[i];
            //    chars[i] = chars[j];
            //    chars[j] = temp;
                
            //}

            char[] charArray = new char[input.Length];
            int len = input.Length - 1;
            for (int i = 0; i <= len; i++)
            {
                charArray[i] = input[len - i];
            }
            return new string(charArray);
        
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string s = this.textBox4.Text;
            this.textBox3.Text = Reverse(s);

        }


        private static string Reverse(string str)
        {

            if (str.Length == 1)
            {
                return str;
            }
            else
            {
                return (str.Substring(str.Length - 1) + Reverse(str.Substring(0, (str.Length - 1))));
            }

            

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string s = this.textBox6.Text;
            //this.textBox5.Text = removeDuplicates(s);
        }


        private static string RemoveDuplicate(string s)
        {

            return "test";

        }

        //public static removeDuplicates(char[] str)
        //{
        //    if (str == null)
        //        return;
        //    int len = str.Length;
        //    if (len < 2)
        //        return;
        //    int tail = 1;
        //    for (int i = 1; i < len; ++i)
        //    {
        //        int j;
        //        for (j = 0; j < tail; ++j)
        //        {
        //            if (str[i] == str[j])
        //                break;
        //        }
        //        if (j == tail)
        //        {
        //            str[tail] = str[i];
        //            ++tail;
        //        }
        //    }
        //    str[tail] = (char)0;
        //}

        private void button10_Click(object sender, EventArgs e)
        {
            string s = this.textBox6.Text;
            this.textBox5.Text = RemoveDuplicateByIndex(s);


        }


        private static string RemoveDuplicateByIndex(string s)
        {

            string container = "";
            foreach (char c in s.ToCharArray())
            {
                if (container.IndexOf(c) == -1)
                {
                    container = container + c.ToString();

                }

            }
            return container;

        }

        private void button12_Click(object sender, EventArgs e)
        {
               string s = this.textBox6.Text;
               this.textBox5.Text = RemoveDuplicateCharsFast2(s);
        
        }

        private static string RemoveDuplicateCharsFast(string key)
        {
            // --- Removes duplicate chars using char arrays.
            int keyLength = key.Length;

            // Store encountered letters in this array.
            char[] table = new char[keyLength];
            int tableLength = 0;


            // Loop through all characters
            foreach (char value in key)
            {
                // Scan the table to see if the letter is in it.
                bool exists = false;
                //table.Contains()
                for (int i = 0; i < tableLength; i++)
                {
                    if (value == table[i])
                    {
                        exists = true;
                        break;
                    }
                }
                // If the letter is new, add to the table and the result.
                if (!exists)
                {
                    table[tableLength] = value;
                    tableLength++;

   
                }
            }
            // Return the string at this range.
            return new string(table, 0, tableLength);
        }


        private static string RemoveDuplicateCharsFast2(string key)
        {

            char[] chars = new char[key.Length];
            int nonduplength = 0;

            foreach (Char c in key.ToCharArray())
            {

                if (!chars.Contains(c))
                {

                    chars[nonduplength] = c;
                    nonduplength++;

                }

            }
          
            return new string(chars, 0, nonduplength);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

            string s = this.textBox6.Text;
            this.textBox5.Text = stringremove(s);

        }

        private static string stringremove(string key)
        {

            if (key == null || key == string.Empty)
            {
                return string.Empty;
            }
            else if (key.Length == 1)
            {
                return key;
            }
            else
            {

                for (int i = 0; i < key.Length; i++)
                {
                    //int j = key.LastIndexOf(key[i]);
                    //if (i != j)
                    //{
                    //    key = key.Remove(j);
                    //}

                    for (int j = (i+1); j < key.Length; j++)
                    {
                        if (key[i] == key[j])
                        {
                            key = key.Remove(j);
                        }
                    }
                }

                return key;
            }
        }

        private void button11_Click_2(object sender, EventArgs e)
        {

            this.textBox7.Text = IsAnagram(this.textBox8.Text, this.textBox9.Text) ? "True" : "False";

        }


        public string mysorting(string s)
        {
            char[] array = s.ToCharArray();
            Array.Sort(array);
            return new string(array);
        }

        private bool IsAnagram(string s, string t)
        {
            if (s == null)
            {
                return true;
            }
            else
            {
                return mysorting(s).Equals(mysorting(t));

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.textBox7.Text = IsAnagramByAscii(this.textBox8.Text, this.textBox9.Text) ? "True" : "False";
        }


        private bool IsAnagramByAscii(string s, string t)
        {
            if (s.Length != t.Length)
            {
                return false;
            }
            else
            {
                int[] checker = new int[256]; //Ascii max possible values

                foreach (char c in s)
                {
                    int value = c;

                    checker[c]++;

                }


                foreach (char c in t)
                {
                    if (checker[c] <= 0)
                    {
                        return false;

                    }
                    else
                    {
                        checker[c]--;

                    }
                }

                return true;
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.textBox14.Text = ReplaceStringwithSpace(this.textBox15.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string s = this.textBox15.Text;
            this.textBox14.Text = ReplaceFun(s.ToCharArray(), s.Length);
        }

        public string ReplaceStringwithSpace(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace(" ", "%20");
            return sb.ToString();
        }

        public string ReplaceFun(char[] str, int length)
        {
            //we can either use array list or use array of different length
            List<char> newstr = new List<char>();
            int totalspaces = 0;
            int index= 0;
            foreach (char c in str)
            {
                if (c == ' ')
                {
                    //totalspaces++;
                    newstr.Add('%');
                    newstr.Add('2');
                    newstr.Add('0');
                }
                else
                {
                    newstr.Add(c);
                }
            }
            ////replace space into %20
            //int newsize = length + (totalspaces * 2);
            //char[] newstr = new char[newsize];

            //foreach (char c in str)
            //{
            //    if (c == ' ')
            //    {
            //        newstr[index] = '%';
            //        newstr[index + 1] = '2';
            //        newstr[index + 2] = '0';
            //        index = index + 3;
            //    }
            //    else
            //    {
            //        newstr[index] = c;
            //        index++;
            //    }
            //}

            return new string(newstr.ToArray());
           
        }

        private void button14_Click(object sender, EventArgs e)
        {

            string s = this.textBox15.Text;
            this.textBox14.Text = ReplaceFun(s.ToCharArray(), s.Length);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Page2 frm = new Page2();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
      
         
      
    }
}
