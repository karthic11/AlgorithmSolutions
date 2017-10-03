using Puzzles.DataStructures.Custom;
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
    public partial class GLModerateProblems_Page3 : Form
    {
        public GLModerateProblems_Page3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //UPDATE: 6/1/2015 Logic here is not optimal..Move. This problem is similar to the one in GLHard Problems page 2 problem 6

//            string input = this.textBox1.Text;
//            //update: Same as Given a list of words, Write a program to find the longest word made of other words of words, can  you optimize the solution?
//            //The cod in another place (hard prb) has memoziation concept in ht..if you do this operation for another words
//// Recursive implementation:
////The idea is simple, we consider each prefix and search it in dictionary.
//// If the prefix is present in dictionary, we recur for rest of the string (or suffix).
//// If the recursive call for suffix returns true, we return true, otherwise we try next prefix. 
////If we have tried all prefixes and none of them resulted in a solution, we return false.

//            WordSegmentation obj = new WordSegmentation();
       
//            StringBuilder sb = new StringBuilder();
//            Puzzles.DataStructures.Custom.WordSegmentation.WordResult result = obj.CheckWordsFormationByString(input, sb);

//            this.textBox2.Text = result.CanFormWords.ToString();
//            this.textBox4.Text = result.ValidWords;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string sentence = this.textBox7.Text;
            //test
            string test = "ilikegoogleman";
            Hashtable ht = new Hashtable();
            ht.Add("i", 1);
            ht.Add("like", 1);
            ht.Add("sam", 1);
            ht.Add("sung", 1);
            ht.Add("mobile", 1);
            ht.Add("ice", 1);
            ht.Add("cream", 1);
            ht.Add("icecream", 1);
            ht.Add("man", 1);
            //ht.Add("go", 1);
            ht.Add("mango", 1);
            ht.Add("ilikesamsungs", 1);
            WordSegmentation obj = new WordSegmentation(sentence);

            //int result = obj.parseSimple(0, 1);

            int result = obj.FindMinimumOfUnrecognizedChars(test, 0, test.Length - 1, ht);

            string output = "";
            this.textBox5.Text = output;
            
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sentence = this.textBox7.Text;
            //test
            string test = "ilikegoogleman";
            Hashtable ht = new Hashtable();
            ht.Add("i", 1);
            ht.Add("like", 1);
            ht.Add("sam", 1);
            ht.Add("sung", 1);
            ht.Add("mobile", 1);
            ht.Add("ice", 1);
            ht.Add("cream", 1);
            ht.Add("icecream", 1);
            ht.Add("man", 1);
            //ht.Add("go", 1);
            ht.Add("mango", 1);
            ht.Add("ilikesamsungs", 1);
            //WordSegmentation obj = new WordSegmentation(sentence);

            ////int result = obj.parseSimple(0, 1);

            //int result = obj.FindMinimumOfUnrecognizedChars(test, 0, test.Length - 1, ht);

            //string output = "";
            this.textBox5.Text = BestStringSplit(ht, sentence);
        }



        private string BestStringSplit(Hashtable ht, string sentence)
        {
            //We can use ht as memo or array of ParseResult object
            Dictionary<int, ParseResult1> memo = new Dictionary<int, ParseResult1>();
            ParseResult1 r = Split(ht, sentence, 0, memo);
            return r == null ? null : r.Parsed;
        }

        private ParseResult1 Split(Hashtable ht, string sentence, int index, Dictionary<int, ParseResult1> memo)
        {
            if (index >= sentence.Length)
            {
                return new ParseResult1(0, string.Empty);
            }

            if (memo.ContainsKey(index))
            {
                return memo[index];
            }
            int bestInvalid = Int32.MaxValue;
            string bestparsing = null;
            string partial = "";
            int startindex = index;
            while (startindex < sentence.Length)
            {
                char c = sentence[startindex];
                partial += c;
                int invalid = ht.ContainsKey(partial) ? 0 : partial.Length;
                if (invalid < bestInvalid)
                {

                    ParseResult1 result = Split(ht, sentence, startindex + 1, memo);

                    if (invalid + result.NoOfInvalidCharacters < bestInvalid)
                    {
                        bestInvalid = invalid + result.NoOfInvalidCharacters;

                        bestparsing = partial + " " + result.Parsed;

                        if (bestInvalid == 0)
                        {
                            break;
                        }
                    }
                }

                //This is important step..increment the index to see every possible prefix starting from this index can make best split
                startindex++;
            }

            ParseResult1 indexbestparse = new ParseResult1(bestInvalid, bestparsing);
            memo.Add(index, indexbestparse);
            return indexbestparse;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sentence = this.textBox1.Text;

            //test
            string test = "ilikegoogleman";
            Hashtable ht = new Hashtable();
            ht.Add("i", 1);
            ht.Add("like", 1);
            ht.Add("sam", 1);
            ht.Add("sung", 1);
            ht.Add("mobile", 1);
            ht.Add("ice", 1);
            ht.Add("cream", 1);
            ht.Add("icecream", 1);
            ht.Add("man", 1);
            ht.Add("go", 1);
            ht.Add("mango", 1);
            ht.Add("ilikesamsungs", 1);

            WorkBreak wb = new WorkBreak(ht);

            //int result = obj.parseSimple(0, 1);

            bool result = wb.CanStringBeSegmentedToWordsByRecursion(sentence);

            this.textBox2.Text = result == true ? "Yes" : "No";

            this.textBox4.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sentence = this.textBox1.Text;

            //test
            string test = "ilikegoogleman";
            Hashtable ht = new Hashtable();
            ht.Add("i", 1);
            ht.Add("like", 1);
            ht.Add("sam", 1);
            ht.Add("sung", 1);
            ht.Add("mobile", 1);
            ht.Add("ice", 1);
            ht.Add("cream", 1);
            ht.Add("icecream", 1);
            ht.Add("man", 1);
            ht.Add("go", 1);
            ht.Add("mango", 1);
            ht.Add("ilikesamsungs", 1);

            WorkBreak wb = new WorkBreak(ht);

            //int result = obj.parseSimple(0, 1);

            bool result = wb.CanStringBeSegmentedToWordsByDP(sentence);

            this.textBox2.Text = result == true ? "Yes" : "No";

            this.textBox4.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sentence = this.textBox1.Text;

            //test
            string test = "ilikegoogleman";
            Hashtable ht = new Hashtable();
            ht.Add("i", 1);
            ht.Add("like", 1);
            ht.Add("sam", 1);
            ht.Add("sung", 1);
            ht.Add("mobile", 1);
            ht.Add("ice", 1);
            ht.Add("cream", 1);
            ht.Add("icecream", 1);
            ht.Add("man", 1);
            ht.Add("go", 1);
            ht.Add("mango", 1);
            ht.Add("ilikesamsungs", 1);

            WorkBreak wb = new WorkBreak(ht);

            //int result = obj.parseSimple(0, 1);

            string result = wb.CanStringBeSegmentedToWordsByDPWithBacktracking(sentence);

            this.textBox2.Text = string.IsNullOrEmpty(result) == false ? "Yes" : "No";

            this.textBox4.Text = result;
        }


    }

    public class ParseResult1
    {
        public int NoOfInvalidCharacters { get; set; }
        public String Parsed { get; set; }

        public ParseResult1(int invalid, string parsed)
        {
            this.NoOfInvalidCharacters = invalid;
            this.Parsed = parsed;
        }
    }
}
