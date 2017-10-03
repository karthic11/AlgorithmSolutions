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

namespace Puzzles
{
  public partial class SpecialTreePage2 : Form
  {
    public SpecialTreePage2()
    {
      InitializeComponent();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      string input2 = this.textBox8.Text;
      string input1 = this.textBox7.Text;
      int[,] lcs = new int[input1.Length + 1, input2.Length + 1];
      int[,] test = new int[6,4];
      int count = LongestCommonSubsequence(input1.ToCharArray(), input2.ToCharArray(), out lcs);
      string output = BackTrackLCS(lcs, input1.ToCharArray(), input2.ToCharArray(), input1.Length, input2.Length);
      this.textBox9.Text = output;

    }


    //https://www.youtube.com/watch?v=P-mMvhfJhu8
    //A subsequence is a sequence that appears in the same relative order, but not necessarily contiguous. 
    //For example, “abc”, “abg”, “bdf”, “aeg”, ‘”acefg”, .. etc are subsequences of “abcdefg”. So a string of length n has 2^n different possible subsequences.
    
      public int LongestCommonSubsequence(char[] str1, char[] str2, out int[,] LCS)
    {
      int m = str1.Length;
      int n = str2.Length;
      LCS = new int[m + 1, n + 1];
      for (int i = 0; i <= m; i++)
      {
        for (int j = 0; j <= n; j++)
        {
          if (i == 0 || j == 0)
          {
            LCS[i, j] = 0;
          }
          else if (str1[i - 1] == str2[j - 1])
          {
            //take diagonal and add 1 
            LCS[i, j] = LCS[i - 1, j - 1] + 1;
          }
          else
          {
            LCS[i, j] = Math.Max(LCS[i - 1, j], LCS[i, j - 1]);
          }

        }
      }

      /* L[m][n] contains length of LCS for X[0..n-1] and Y[0..m-1] */
      //the last will have the length of longest common subsequence
      return LCS[m, n];

    }

    //function backtrack(C[0..m,0..n], X[1..m], Y[1..n], i, j)
    //if i = 0 or j = 0
    //    return ""
    //else if  X[i] = Y[j]
    //    return backtrack(C, X, Y, i-1, j-1) + X[i]
    //else
    //    if C[i,j-1] > C[i-1,j]
    //        return backtrack(C, X, Y, i, j-1)
    //    else
    //        return backtrack(C, X, Y, i-1, j)

    public string BackTrackLCS(int[,] LCS, char[] str1, char[] str2, int i, int j)
    {
      if (i == 0 || j == 0)
      {
        return string.Empty;
      }
          //there will be a bridge for match so check for bridge
      else if (str1[i -1] ==  str2[j -1])
      {
        //if match cross through bridge
        return BackTrackLCS(LCS, str1, str2, i - 1, j - 1) + str1[i -1];
      }
      else
      {
        //we can move either left or top but we got to maintain consistency
        if (LCS[i, j - 1] > LCS[i - 1, j])
        {
          return BackTrackLCS(LCS, str1, str2, i, j - 1);
        }
        else
        {
          return BackTrackLCS(LCS, str1, str2, i - 1, j);
        }

      }

    }

    private void button1_Click(object sender, EventArgs e)
    {

      string input = this.textBox3.Text;

      //string forward = input;
      //char[] chars = input.ToCharArray();
      //Array.Reverse(chars);
      //string reverse = new string(chars, 0, chars.Length);
      //string palindromecheck = forward + "#" + reverse;
      //KarthicSuffixArray sa = new KarthicSuffixArray(input, true);

      //create two suffix tree..
      //eg: Banana
      //forward suffix tree with chars and end with $              suffix1 =b,a,n,a,n,a, $
      //revese the chars and make another tree and end with #      suffix1= a,n,a,n,a ,b, #

      //For every suffix i in Sf, find the lowest common ancestor with the suffix n - i + 1 in Sr.

      //find the longest common substring of this string and its reverse.

      //string output = sa.LongestPanlindrome;


        //try 2

      KarthicSuffixArray sa = new KarthicSuffixArray();

      string reverse = Reverse(input);

        //fucking success..logic is
        //reverse the given word
        //concat giveword + '#' + reveser
        //and then find the longest common substring
      this.textBox1.Text = sa.FindLongestCommonSubstring(input, reverse); ;
  


        //Building the suffix tree takes \Theta(N) time (if the size of the alphabet is constant).
        //If the tree is traversed from the bottom up with a bit vector telling which strings are seen below each node, the k-common substring problem can be solved in \Theta(NK) time. If the suffix tree is prepared for constant time lowest common ancestor retrieval, it can be solved in \Theta(N) time.[1]
    }

    private string Reverse(string s)
    {
        char[] chararray = s.ToCharArray();
        Array.Reverse(chararray);
        return new string(chararray);
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
        string[,] lunchmenu = {{"pi", "italian"},
                               {"curry", "indain"}};
        string[,] a = { { "karthic", "indian" } };

        //var result = Solution.matchLunches(lunchmenu, a);

    }
  }


  //public class Solution
  //{


  //    private const int MAX = 256;

  //    private static bool Compare(char[] arr1, char[] arr2)
  //    {
  //        for (int i = 0; i < MAX; ++i)
  //            if (arr1[i] != arr2[i])
  //                return false;

  //        return true;
  //    }

  //    public static List<int> getAnagramIndices(string haystack, string needle)
  //    {
  //        List<int> anagramIndexList = new List<int>();
  //        char[] patternArrayCount = new char[MAX];
  //        char[] TextArrayCount = new char[MAX];

  //        for (int i = 0; i < needle.Length; ++i)
  //        {
  //            (patternArrayCount[needle[i]])++;
  //            (TextArrayCount[haystack[i]])++;
  //        }

  //        for (int i = needle.Length; i < haystack.Length; ++i)
  //        {
  //            if (Compare(patternArrayCount, TextArrayCount))
  //                anagramIndexList.Add((i - needle.Length));

  //            (TextArrayCount[haystack[i]])++;
  //            TextArrayCount[haystack[i - needle.Length]]--;
  //        }

  //        if (Compare(patternArrayCount, TextArrayCount))
  //            anagramIndexList.Add(haystack.Length - needle.Length);

  //        return anagramIndexList;
  //    }

  //    public static List<int> Search(string str, string pat)
  //    {
  //        List<int> retVal = new List<int>();
  //        char[] countP = new char[MAX];
  //        char[] countT = new char[MAX];

  //        for (int i = 0; i < pat.Length; ++i)
  //        {
  //            (countP[pat[i]])++;
  //            (countT[str[i]])++;
  //        }

  //        for (int i = pat.Length; i < str.Length; ++i)
  //        {
  //            if (Compare(countP, countT))
  //                retVal.Add((i - pat.Length));

  //            (countT[str[i]])++;
  //            countT[str[i - pat.Length]]--;
  //        }

  //        if (Compare(countP, countT))
  //            retVal.Add(str.Length - pat.Length);

  //        return retVal;
  //    }

  //    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
  //    // RETURN AN EMPTY MATRIX IF PREFERRED LUNCH IS NOT FOUND
  //    public static string[,] matchLunches(string[,] lunchMenuPairs,
  //                                            string[,] teamCuisinePreference)
  //    {
  //        List<PersonFood> personFoods = new List<PersonFood>();

  //        Dictionary<string, List<string>> cuisineFoodMap = new Dictionary<string, List<string>>();

  //        // we can build a ht with key as cuisine and key as List of food option
  //        for (int i = 0; i < lunchMenuPairs.GetLength(0); i++)
  //        {
  //            string food = lunchMenuPairs[i, 0];
  //            string cuisine = lunchMenuPairs[i, 1];

  //            if (cuisineFoodMap.ContainsKey(cuisine))
  //            {
  //                cuisineFoodMap[cuisine].Add(food);
  //            }
  //            else
  //            {
  //                List<string> foods = new List<string>();
  //                foods.Add(food);
  //                cuisineFoodMap.Add(cuisine, foods);
  //            }
  //        }

  //        for (int i = 0; i < teamCuisinePreference.GetLength(0); i++)
  //        {
  //             string person = teamCuisinePreference[i, 0];
  //             string cuisine = teamCuisinePreference[i, 1];
  //              List<string> foods = new List<string>();

  //             if(cuisine == "*")
  //             {
  //               foreach(KeyValuePair<string, List<string>> entry in cuisineFoodMap)
  //               {
  //                   foods.AddRange(entry.Value);
  //               }
  //             }
  //             else
  //             {
  //                 if(cuisineFoodMap.ContainsKey(cuisine))
  //                 {
  //                     foods = cuisineFoodMap[cuisine];
  //                 }
  //             }
               
  //             PersonFood personFood = new PersonFood(person, foods);
  //             personFoods.Add(personFood);
  //        }

  //          string[,] result = new string[5, 5];
  //        // convert the list of person food to 2 d array
         
  //        return result;
  //    }

  //    public class PersonFood
  //    {
  //        string Name;
  //        List<string> Foods;

  //        public PersonFood(string name, List<string> foods)
  //        {
  //            this.Name = name;
  //            this.Foods = foods;
  //        }
  //    }



  //}
}
