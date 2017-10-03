
using System.Threading.Tasks;
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

namespace Puzzles.DataStructures.Array
{

    //http://stackoverflow.com/questions/7043778/longest-palindrome-in-a-string-using-suffix-tree

    //A suffix array is a sorted array of all suffixes of a given string. 
    //http://www.geeksforgeeks.org/suffix-array-set-1-introduction/

  public class KarthicSuffixArray
  {

    public int[] SuffixArray = null;
    public int[] LCPArray = null;
    public string SourceString { get; set; }
    public string LongestPanlindrome = string.Empty;
  
    public KarthicSuffixArray(string s)
    {
      this.SourceString = s;
      SuffixArray = new int[s.Length];
      LCPArray = new int[s.Length];
      SuffixArray = BuildSuffixArray(s);
      LCPArray = BuildLCPArray(SuffixArray, this.SourceString);

    }

    public KarthicSuffixArray()
    {
    }
    public KarthicSuffixArray(string sourcestring, bool GetPalindrome)
    {
      this.SourceString = sourcestring;

      char[] chars = sourcestring.ToCharArray();
      System.Array.Reverse(chars);
      string reverse = new string(chars, 0, chars.Length);
      string palindromecheck = SourceString + "#" + reverse;



      SuffixArray = new int[SourceString.Length];
      LCPArray = new int[SourceString.Length];
      SuffixArray = BuildSuffixArray(palindromecheck);
      LCPArray = BuildLCPArray(SuffixArray, palindromecheck);

      LongestPanlindrome = FindLongestPalindrome(LCPArray, SuffixArray, SourceString, palindromecheck);
     

    }



    private int[] BuildSuffixArray(string s)
    {
      ////suffix has all the possible suffix of the given string
      //string[] suffixes = new string[s.Length];
      ////banana
      //for(int i=0; i < s.Length; i++)
      //{
      //    suffixes[i] = s.Substring(i);
      //}

      ////here banana
      ////suffix[0] = banana
      ////suffix[1] =  anana
      ////suffix[2] =   nana
      ////suffix[3] =    ana
      ////suffix[4] =     na
      ////suffix[5] =      a

      ////remember the suffix array has all the possible suffix for the given string
      ////and also the index value point to the index of the substring in the given string

      //Hashtable ht = new Hashtable();

      //for(int i=0; i < suffixes.Length; i++)
      //{
      //  ht.Add(suffixes[i], i);
         
      //}
      ////suffix hold all the possible suffix of the given string
      ////sort the suffixes array
      // System.Array.Sort(suffixes);

      // int[] suffixarray = new int[s.Length];
      // for (int j = 0; j < suffixes.Length; j++)
      // {
      //   string sorteditem = suffixes[j];
      //   suffixarray[j] = (int)ht[sorteditem];


      // }

      int[] suffixarray = Enumerable.Range(0, s.Length).ToArray();
      System.Array.Sort(suffixarray, (x, y) => string.Compare(s, x, s, y, s.Length));



       return suffixarray;
    }


    private int[] BuildLCPArray(int[] suffixarray,string source)
    {
      int[] lcp = new int[suffixarray.Length];
     //lcp[i] : longest common prefix between sa[i] and sa[i+1]
      //so last index will have 0 or the first will have 0
      for (int i = 1; i < suffixarray.Length ; i++)
      {
        string result = FindLongestCommonPrefix(source.Substring(suffixarray[i -1]), source.Substring(suffixarray[i]));
        lcp[i] = result.Length;

          //while building LCP (longest common prefix we can also find the longest common substring)


      }

      //this.LCPArray = lcp;
      return lcp;
    }

    //This function returns the longest prefix in the secondstring
    //First it will look for whole prefix..it not decrement the prefix
    //eg look for   bana, ban, ba, b 
    public string FindLongestCommonPrefix(string input1, string input2)
    {
      StringBuilder sb = new StringBuilder();
      //find the smallers length
      int length = Math.Min(input1.Length, input2.Length);

      for (int i = 0; i < length; i++)
        {
          if (input1[i] == input2[i])
          {
            sb.Append(input2[i]);
          }
          else
          {
            break;
          }
          
        }

      return sb.ToString();
    }


    public String findLongPrefix(String[] str)
    {
      StringBuilder strBuilder = new StringBuilder();

      char[] firstStr = str[0].ToCharArray();
      for (int i = 0; i < firstStr.Length; i++)
      {
        bool found = true;
        foreach (String item in str)
        {
          if (item[i] != firstStr[i])
          {
            found = false;
            break;
          }
        }

        if (found)
        {
          strBuilder.Append(firstStr[i]);
        }
        else
          break;

      }

      return strBuilder.ToString();
    }



    //def find(sa, text, q):
    //size = len(sa)
    //qsize = len(q)
    //hi = size -1
    //lo = 0
    //while hi >= lo:
    //    mid = (hi + lo) / 2
    //    begin = sa[mid]
    //    end = min(size, begin + qsize)
    //    test = text[begin: end]
    //    if test > q:
    //        hi = mid -1
    //    elif test < q:
    //        lo = mid + 1
    //    else:
    //        return begin
    //return None

    public int Search2(string pattern, string source, int[] suffixarray)
    {
      int sourcesize = suffixarray.Length;
      int patternsize = pattern.Length;
      int low = 0;
      int high = sourcesize - 1;
      int middle;
      int start, end;
      string test = string.Empty;

      while (low < high)
      {
        middle = (low + high) / 2;
        start = suffixarray[middle];
        end = Math.Min(sourcesize, start);
        test = source.Substring(start, end);
        if (pattern.CompareTo(test)  > 0)
        {
          low = middle + 1;
        }
        else if (pattern.CompareTo(test) < 0)
        {
        
          high = middle - 1;
        }
        else
        {
          return start;
        }
      }

      return -1;
    }


    //def search(P):
    //    l = 0; r = n
    //    while l < r:
    //        mid = (l+r) / 2
    //        if P > suffixAt(A[mid]):
    //            l = mid + 1
    //        else:
    //            r = mid
    //    s = l; r = n
    //    while l < r:
    //        mid = (l+r) / 2
    //        if P < suffixAt(A[mid]):
    //            r = mid
    //        else:
    //            l = mid + 1
    //    return (s, r)



//      Let the given string be "banana".

//0 banana                          5 a
//1 anana     Sort the Suffixes     3 ana
//2 nana      ---------------->     1 anana  
//3 ana        alphabetically       0 banana  
//4 na                              4 na   
//5 a                               2 nana

//So the suffix array for "banana" is {5, 3, 1, 0, 4, 2}

    public List<string> Search(string pattern, string source, int[] suffixarray)
    {
      int low = 0;
      int high = source.Length - 1;
      int middle;

      while (low < high)
      {
        middle = (low + high) / 2;

             //if P > suffixAt(A[mid]):
             //   l = mid + 1
        string substring = source.Substring(suffixarray[middle]);
        int test1 = CompareTo(pattern, source, suffixarray[middle]);
        if (CompareTo(pattern, source, suffixarray[middle]) > 0)
        {
          low = middle + 1;
        }
        else
        {
          high = middle;
        }
      }

      int startposition = low;
      high = source.Length - 1;

      while (low < high)
      {
        middle = (low + high) / 2;
        var tes = source.Substring(suffixarray[middle]);
        int test2 = CompareTo(pattern, source, suffixarray[middle]);
        if (CompareTo(pattern, source, suffixarray[middle]) < 0)
        {
          high = middle;
         
        }
        else
        {
          low = middle + 1;
         
        }
      }
      int endposition = high - 1;


      //start position will be the first occurance
      //end position will be the last occurance 
       //count no of items between first and last
      //rememeber we can also get all the suffixes too
      int count = endposition - startposition + 1;
      List<string> suffixes = new List<string>();
      for (int i = startposition; i <= endposition; i++)
      {
        suffixes.Add(source.Substring(SuffixArray[i]));

      }

      return suffixes;
    }


    public List<string> SearchPattern(string pattern, string source, int[] suffixarray)
    {
        List<string> suffixes = new List<string>();
        //we will find the index of the suffixarray that has the pattern
        int index = BinarySearchSortedArray(suffixarray, 0, suffixarray.Length - 1, pattern, source);
       //here the logic is different from regular binary search
       //we might have the same patter above and below the index like if you are looking for a in array { a, ana , anana }
        if (index != -1)
        {

            int startposition = index;
            int endposition = index;
            int current = index -1;
            //get start position
            while (current >= 0)
            {
                int suffixarrraycurrentvalue = suffixarray[current];
                int result  = String.Compare(pattern, 0, source, suffixarrraycurrentvalue, pattern.Length);
                if (result == 0)
                {
                    //This mean the current index has also the pattern
                    startposition = current;
                    current = current - 1;
                }
                else
                {
                    break;//important break after the first search..only continue if you find the pattern
                }
            }

            //get end position
            current = index + 1;
            while (current < suffixarray.Length)
            {
                int suffixarrraycurrentvalue = suffixarray[current];
                int result = String.Compare(pattern, 0, source, suffixarrraycurrentvalue, pattern.Length);
                if (result == 0)
                {
                    //This mean the current index has also the pattern
                    endposition = current;
                    current = current + 1;
                }
                else
                {
                    break;//important break after the first search..only continue if you find the pattern
                }
            }

            //start position will be the first occurance
            //end position will be the last occurance 
            //count no of items between first and last
            //rememeber we can also get all the suffixes too
            int count = endposition - startposition + 1;

            for (int i = startposition; i <= endposition; i++)
            {
                suffixes.Add(source.Substring(SuffixArray[i]));

            }
        }

        return suffixes;
       
    }
    //This method search the array for the key and return the index of the key..if not found returns -1
    public  int BinarySearchSortedArray(int[] suffixarray, int low, int high, string pattern, string originalstring)
    {
        //error handling
        if (suffixarray.Length == 0)
        {
            //throw error
        }

        while (low <= high)
        {
            int middle = (low + high) / 2;
            int sourcestartindex = suffixarray[middle];
            int compareresult = String.Compare(pattern, 0, originalstring, sourcestartindex, pattern.Length);

            if (compareresult == 0)
            {
                return middle; //index of middle element
            }
            else if (compareresult < 0)
            {
                //search the first half of the array
                //exclude the middle element
                high = middle - 1;
            }
            else
            {
                low = middle + 1;
            }
        }

        //code come means key not found
        return -1;
    }

   


    private int CompareTo(string pattern, string source, int sourcestartindex)
    {
      // Comparison takes into account maximum length(w) 
      // characters. For example, strings "ab" and "abc" 
      // are thus considered equal.
      return String.Compare(pattern, 0, source, sourcestartindex, pattern.Length);
    }
    //private string find(KarthicSuffixArray sa, string source, string pattern)
    //{
    //}\

    //    //http://stackoverflow.com/questions/7043778/longest-palindrome-in-a-string-using-suffix-tree

    private string FindLongestPalindrome(int[] LCP, int[] suffixArray, string sourcestring, string concat)
    {
      //   Let the length of the Longest Palindrome ,longestlength:=0 (Initially)
      //Let Position:=0.
      int longestlength = 0;
      int Position = 0;
      int Len = concat.Length;
      int actuallen = sourcestring.Length;
      for (int i = 1; i < Len; ++i)
      {
        //Note that Len=Length of Original String +"#"+ Reverse String
        if ((LCP[i] > longestlength))
        {
          //Note Actual Len=Length of original Input string .
          if ((suffixArray[i - 1] < actuallen && suffixArray[i] > actuallen) || (suffixArray[i] < actuallen && suffixArray[i - 1] > actuallen))
          {
            //print :Calculating Longest Prefixes b/w suffixArray[i-1] AND  suffixArray[i]


            longestlength = LCP[i];
            //print The Longest Prefix b/w them  is ..
            //print The Length is :longestlength:=LCP[i];
            Position = suffixArray[i];
          }
        }
      }

      string longestpalindrome = sourcestring.Substring(Position, Position + longestlength - 1);
      return longestpalindrome;
      //So the length of Longest Palindrome :=longestlength;
      //and the longest palindrome is:=Str[position,position+longestlength-1];
    }

    //- Concatenate S1 and S2.
    //- Compute the suffix array.
    //- Compute the LCP of all adjacent suffixes.
    //the maximum LCP(i,i+1) is the required answer.
    //LongestCommonSubstring(input1, input2, out output);
    //while building LCP (longest common prefix we can also find the longest common substring)
    //LCP[i]=Length of Longest Common Prefix between Suffix i and suffix (i+1). (for i>0)
    public string FindLongestCommonSubstring(string text1, string text2)
    {
        //make sure "#" is not present in both the text1 and text2
        string merge = text1 + "#" + text2;
        int[] suffixarray = BuildSuffixArray(merge);
 
        string longestsubstring = string.Empty;
        int[] lcp = new int[suffixarray.Length];
        //lcp[i] : longest common prefix between sa[i] and sa[i+1]
        //so last index will have 0 or the first will have 0
        for (int i = 1; i < suffixarray.Length; i++)
        {

            string suffix1 = merge.Substring(suffixarray[i - 1]);
            string suffix2 = merge.Substring(suffixarray[i]);

            string result = FindLongestCommonPrefix(merge.Substring(suffixarray[i - 1]), merge.Substring(suffixarray[i]));
            lcp[i] = result.Length;


            //we need to make sure the result is compared btw two different string s1 and s2
            if (result.Length > longestsubstring.Length) 
            {
                //one should have # and other shouldn't
                //if((suffix1.Contains('#') && (suffix2.Contains('#') == false)) ||
                //    (suffix2.Contains('#') && (suffix1.Contains('#') == false)))
                if ((suffix1.Contains('#') ^ suffix2.Contains('#')) == true)
                {
                    longestsubstring = result;
                }
               
            }
           
        }

        return longestsubstring;
    }
   
  }
}
