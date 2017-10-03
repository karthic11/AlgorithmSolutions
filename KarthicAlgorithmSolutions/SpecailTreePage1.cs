using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Puzzles.DataStructures.Array;
using Puzzles.DataStructures.ArrayHelper;

namespace Puzzles
{
  public partial class SpecailTreePage1 : Form
  {
    public SpecailTreePage1()
    {
      InitializeComponent();
    }

    private void SpecailTreePage1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
      string input = this.textBox3.Text;
      string pattern = this.textBox2.Text;

      string[] words = input.Trim().Split(' ', ',', ';', '.');
      Trie mytrie = new Trie();

      foreach (string word in words)
      {
        mytrie.Insert(word);

      }
      this.textBox1.Text = mytrie.Search(pattern) ? "Exists" : "Not Exists";

      //this.textBox1.Text = mytrie.FindNoofOccurence(pattern).ToString();
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
      string input = this.textBox5.Text;
      string pattern = this.textBox4.Text;

     // string[] words = input.Trim().Split(' ', ',', ';', '.');
      SuffixTree tree = new SuffixTree(input);

      List<int> indexofpattern = new List<int>();
       indexofpattern = tree.Search(pattern);

       if (indexofpattern != null && indexofpattern.Count > 0)
      {

        //this.textBox6.Text = input.Substring(indexofpattern[0], pattern.Length);
        this.textBox6.Text = indexofpattern.Count.ToString();
      }
      else
      {
        this.textBox6.Text = "Pattern not found";
      }

     

    }

    private void button4_Click(object sender, EventArgs e)
    {
      string input1 = this.textBox8.Text;

      string input2 = this.textBox7.Text;

      string output = "";
      LongestCommonSubstring(input1, input2, out output);
      this.textBox9.Text = output;



    }


    public int LongestCommonSubstring(string str1, string str2, out string sequence)
    {
      sequence = string.Empty;
      if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
        return 0;

      int[,] num = new int[str1.Length, str2.Length];
      int maxlen = 0;
      int lastSubsBegin = 0;
      StringBuilder sequenceBuilder = new StringBuilder();

      for (int i = 0; i < str1.Length; i++)
      {
        for (int j = 0; j < str2.Length; j++)
        {
          if (str1[i] != str2[j])
            num[i, j] = 0;
          else
          {
            if ((i == 0) || (j == 0))
              num[i, j] = 1;
            else
              num[i, j] = 1 + num[i - 1, j - 1];

            if (num[i, j] > maxlen)
            {
              maxlen = num[i, j];
              int thisSubsBegin = i - num[i, j] + 1;
              if (lastSubsBegin == thisSubsBegin)
              {//if the current LCS is the same as the last time this block ran
                sequenceBuilder.Append(str1[i]);
              }
              else //this block resets the string builder if a different LCS is found
              {
                lastSubsBegin = thisSubsBegin;
                sequenceBuilder.Length = 0; //clear it
                sequenceBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
              }
            }
          }
        }
      }
      sequence = sequenceBuilder.ToString();
      return maxlen;
    }

    private void button3_Click(object sender, EventArgs e)
    {

      string input1 = this.textBox8.Text;

      string input2 = this.textBox7.Text;

//  - Concatenate S1 and S2.
//- Compute the suffix array.
//- Compute the LCP of all adjacent suffixes.
//the maximum LCP(i,i+1) is the required answer.
//LongestCommonSubstring(input1, input2, out output);
      //http://algs4.cs.princeton.edu/63suffix/LongestCommonSubstring.java.html
        //update: if u think with eg..u will get this
 
      KarthicSuffixArray array = new KarthicSuffixArray();
      string output = array.FindLongestCommonSubstring(input1, input2);
     
      this.textBox9.Text = output;

    }






    private void button6_Click(object sender, EventArgs e)
    {

      string input = this.textBox5.Text;
      string pattern = this.textBox4.Text;
      //// string[] words = input.Trim().Split(' ', ',', ';', '.');
      //SuffixArray sa1  = SuffixArray.Build(input);
      KarthicSuffixArray sa = new KarthicSuffixArray(input);
      List<string> matchedsuffix = sa.SearchPattern(pattern, sa.SourceString, sa.SuffixArray);



      //var str = input;
      //var sa1 = SuffixArray.Build(str);
      //string pat = pattern;
      //List<string> suffixes = new List<string>();
      //sa1 = sa1.Search(pat);
      // int count = 0;
      // foreach (var pos in sa1)
      // {
      //   count++;
      //   suffixes.Add(input.Substring(pos));


      // }

      // this.textBox6.Text = count.ToString();
      this.textBox6.Text = matchedsuffix.Count.ToString();
     

    }



    private void button5_Click(object sender, EventArgs e)
    {
      string input1 = this.textBox8.Text;

      string input2 = this.textBox7.Text;

      string longest = string.Empty;
      //watch this for the algorithm logic https://www.youtube.com/watch?v=RUckZMzqUcw

      List<string> result = LongesCommonSubstringDynamicMethod(input1.ToCharArray(), input2.ToCharArray());
      StringBuilder sb = new StringBuilder();
      if (result.Count > 0)
      {
          foreach (String s in result)
          {
              sb.Append(s).Append(",");
          }
      }
      //LongestCommonSubstring(input1, input2, out output);
      this.textBox9.Text = sb.ToString();



//      // A utility function to find maximum of two integers
//int max(int a, int b)
//{   return (a > b)? a : b; }
 
///* Returns length of longest common substring of X[0..m-1] and Y[0..n-1] */
//int LCSubStr(char *X, char *Y, int m, int n)
//{

//    int result = 0;  // To store length of the longest common substring
 
//    /* Following steps build LCSuff[m+1][n+1] in bottom up fashion. */
//    for (int i=0; i<=m; i++)
//    {
//        for (int j=0; j<=n; j++)
//        {
//            if (i == 0 || j == 0)
//                LCSuff[i][j] = 0;
 
//            else if (X[i-1] == Y[j-1])
//            {
//                LCSuff[i][j] = LCSuff[i-1][j-1] + 1;
//                result = max(result, LCSuff[i][j]);
//            }
//            else LCSuff[i][j] = 0;
//        }
//    }
//    return result;
//}
 
    }


    public List<String> LongesCommonSubstringDynamicMethod(char[] str1, char[] str2)
    {
      int m = str1.Length;
      int n = str2.Length;
      int max = -1;
      int[,] LCSuffix = new int[m + 1, n + 1];
      StringBuilder sb = new StringBuilder();
      List<String> list = new List<string>();
      //remember the first row and column is dummy and is going to help in calcuation

      //    // Create a table to store lengths of longest common suffixes of
      //    // substrings.   Notethat LCSuff[i][j] contains length of longest
      //    // common suffix of X[0..i-1] and Y[0..j-1]. The first row and
      //    // first column entries have no logical meaning, they are used only
      //    // for simplicity of program
      //    int LCSuff[m+1][n+1];

      for (int i = 0; i <= m; i++)
      {
        for (int j = 0; j <= n; j++)
        {
          if (i == 0 || j == 0)
          {
            LCSuffix[i, j] = 0;
          }
          else if (str1[i - 1] == str2[j - 1])
          {
            LCSuffix[i, j] = LCSuffix[i - 1, j - 1] + 1;
            //max = Math.Max(max, LCSuffix[i, j]);

            if (LCSuffix[i, j] > max)
            {
                max = LCSuffix[i, j];
                //clear the old longest
                list.Clear();
                //move to left end..take j..and get the susbtring index of j
                //int index = str2[j - 1];
                //sb.Append(new string(str2, 0, j));
                //here  j-1 will be the actual index on the str2
                //we need to move to left and take max character from that index to above (build upwords)
                string maxstring = new string(str2, (j - 1) - (max - 1), max);
                //sb.Append(maxstring);
                list.Add(maxstring);
            }
            else if (LCSuffix[i, j] == max)
            {
                  string maxstring = new string(str2, (j - 1) - (max - 1), max);
                  list.Add(maxstring);
            }

            ////when there is match and the max is caused by this 
            //if (max == LCSuffix[i, j])
            //{
            //  //clear the old longest
            //  sb.Clear();
            //  //move to left end..take j..and get the susbtring index of j
            //  //int index = str2[j - 1];
            //  //sb.Append(new string(str2, 0, j));
            //    //here  j-1 will be the actual index on the str2
            //    //we need to move to left and take max character from that index to above (build upwords)
            //  string maxstring = new string(str2, (j - 1) - (max - 1), max);
            //  sb.Append(maxstring);
              
            //}

          }
          else
          {
            LCSuffix[i, j] = 0;
          }
        }
      }

      //longest = sb.ToString();
      return list;
    }

    public int max(int a, int b)
    {
      return (a > b) ? a : b;
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
