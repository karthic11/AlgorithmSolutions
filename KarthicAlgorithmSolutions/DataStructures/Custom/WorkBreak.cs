using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Custom
{
    public class WorkBreak
    {
        public Hashtable map { get; set; }

        public WorkBreak(Hashtable map)
        {
            this.map = map;
        }

        // http://www.geeksforgeeks.org/dynamic-programming-set-32-word-break-problem/
        // // returns true if string can be segmented into space separated
        // words, otherwise returns false
        // Time: O(2 ^ n)
        public bool CanStringBeSegmentedToWordsByRecursion(string input)
        {
            //base case
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            // non empty string
            for (int i = 1; i <= input.Length; i++)
            {
                string prefix = input.Substring(0, i);
                string remaining = input.Substring(i);

                if (this.map.Contains(prefix) &&
                    CanStringBeSegmentedToWordsByRecursion(remaining))
                {
                    return true;
                }
            }

            // If we have tried all prefixes and none of them worked
            return false;
        }

        // https://www.youtube.com/watch?v=WepWFGxiwRs
        // https://github.com/mission-peace/interview/blob/master/src/com/interview/dynamic/BreakMultipleWordsWithNoSpaceIntoSpace.java
        // returns true if string can be segmented into space separated
        // words, otherwise returns false
        // Time: O(n*n)
        public bool CanStringBeSegmentedToWordsByDP(string input)
        {
            bool[,] wordbreak = new bool[input.Length, input.Length];

            // Logic
            // 1)  Wordbreak[i,j] == true when string formed between i to j index is word (as whole or split) or
            // 2)  Find a index k between i and j such that wordbreak[i,k-1] == true  (string formed between i and k-1 is word)
            //                                          and wordbreak[k, j] == true    (string formed between k and j is word)

            // Iterate along the length
            for (int length = 1; length <= input.Length; length++)
            {
                for (int i = 0; i < input.Length - length + 1; i++)
                {
                    // For string of length 6 eg" "IAMACE"
                    // For Length = 1, i will be {0 to 5} and so on
                    // For Length = 2, i will be {0 to 4} and so on

                    int j = i + length - 1;

                    // find whether Wordbreak[i,j] == true based on 2 conditions
                    string wholeword = input.Substring(i, length);
                    if (this.map.Contains(wholeword))
                    {
                        wordbreak[i, j] = true;
                        continue;
                    }

                    // Find k between i and j
                    for (int k = i + 1; k <= j; k++)
                    {
                        if (wordbreak[i, k - 1] == true &&
                           wordbreak[k, j] == true)
                        {
                            wordbreak[i, j] = true;
                            break;
                        }
                    }
                }
            }

            // last column in first row is the answer
            return wordbreak[0, input.Length - 1];
        }

        // https://www.youtube.com/watch?v=WepWFGxiwRs
        // https://github.com/mission-peace/interview/blob/master/src/com/interview/dynamic/BreakMultipleWordsWithNoSpaceIntoSpace.java
        // returns true if string can be segmented into space separated
        // words, otherwise returns false
        // Time: O(n*n)
        public string CanStringBeSegmentedToWordsByDPWithBacktracking(string input)
        {
            bool[,] wordbreak = new bool[input.Length, input.Length];

            // use bt
            int[,] backtracking = new int[input.Length, input.Length];
            for (int i = 0; i < backtracking.GetLength(0); i++)
            {
                for (int j = 0; j < backtracking.GetLength(1); j++)
                {
                    backtracking[i, j] = -1;
                }
            }

            // Logic
            // 1)  Wordbreak[i,j] == true when string formed between i to j index is word (as whole or split) or
            // 2)  Find a index k between i and j such that wordbreak[i,k-1] == true  (string formed between i and k-1 is word)
            //                                          and wordbreak[k, j] == true    (string formed between k and j is word)

            // Iterate along the length
            for (int length = 1; length <= input.Length; length++)
            {
                for (int i = 0; i < input.Length - length + 1; i++)
                {
                    // For string of length 6 eg" "IAMACE"
                    // For Length = 1, i will be {0 to 5} and so on
                    // For Length = 2, i will be {0 to 4} and so on

                    int j = i + length - 1;

                    // find whether Wordbreak[i,j] == true based on 2 conditions
                    string wholeword = input.Substring(i, length);
                    if (this.map.Contains(wholeword))
                    {
                        wordbreak[i, j] = true;
                        backtracking[i, j] = i;
                        continue;
                    }

                    // Find k between i and j
                    for (int k = i + 1; k <= j; k++)
                    {
                        if (wordbreak[i, k - 1] == true &&
                           wordbreak[k, j] == true)
                        {
                            wordbreak[i, j] = true;
                            backtracking[i, j] = k;
                            break;
                        }
                    }
                }
            }

            // last column in first row is the answer
            if (wordbreak[0, input.Length - 1] == false)
            {
                // no work seg found
                return string.Empty;
            }

            // Word segmentation found
            StringBuilder sb = new StringBuilder();
            int low = 0;
            int high = input.Length - 1;

            while (low < high)
            {
                int splitindex = backtracking[low, high];

                if (splitindex == low)
                {
                    // The complete work from low to high is the work
                    sb.Append(input.Substring(low, high - low + 1)).Append(",");
                    break;
                }
                else
                {
                    sb.Append(input.Substring(low, splitindex - low)).Append(",");
                }

                low = splitindex;
            }

            return sb.ToString();
        }
    }
}
