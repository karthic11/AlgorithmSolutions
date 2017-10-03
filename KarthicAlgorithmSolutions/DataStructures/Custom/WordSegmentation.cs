using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Custom
{
    public class WordSegmentation
    {


        public String Sentenece { get; set; }

        public WordSegmentation()
        {
   
        }

        public WordSegmentation(string sentence)
        {
 
            this.Sentenece = sentence;

        }




        //public bool CheckWordsFormationByString(string sentence, Dictionary<string, bool> ht)
        //{
            ////base case
            //if (sentence.Length == 0)
            //{
            //    return true;
            //}

            ////find all the possible prefixes for the given string..eg ilike
            ////i , il, ili, ilk, ilike  using string.substring
            //for (int i = 1; i <= sentence.Length; i++)
            //{
            //    //sec pram is length
            //    string prefix = sentence.Substring(0, i);
            //    if (IsWordExistsInDictionary(prefix) &&
            //        CheckWordsFormationByString(sentence.Substring(i, sentence.Length - i)))
            //    {
            //        return true;
            //    }
            //}

            ////we checked all the possible prefixes no words can be formed
            //return false;

        //}


        public bool CheckWordsCanBeFormedByWordsInDictionary(string word, Dictionary<string, bool> ht)
        {
            return true;
        }


        public WordResult CheckWordsFormationByString(string sentence,  StringBuilder sb)
        {
            //base case
            if (sentence.Length == 0)
            {
                return new WordResult(true, sb.ToString());
                
            }

            //find all the possible prefixes for the given string..eg ilike
            //i , il, ili, ilk, ilike  using string.substring
            for (int i = 1; i <= sentence.Length; i++)
            {
                //sec pram is length
                string prefix = sentence.Substring(0, i);
                if (IsWordExistsInDictionary(prefix))
                {
                    sb.Append(prefix).Append(" ");

                    WordResult result = CheckWordsFormationByString(sentence.Substring(i, sentence.Length - i), sb);
                    if (result.CanFormWords)
                    {
                        return result;
                    }
                    else
                    {
                        sb.Clear();
                    }

                }
                
            }

            //we checked all the possible prefixes no words can be formed
            return new WordResult(false, string.Empty);

        }




        private bool IsWordExistsInDictionary(string word)
        {
            return true;
        }




        public class WordResult
        {
            public bool CanFormWords { get; set; }
            public String ValidWords { get; set; }

            public WordResult(bool canformwords, string validwords)
            {
                this.CanFormWords = canformwords;
                this.ValidWords = validwords;
            }
        }


        public  int parseSimple(int wordStart, int wordEnd)
        {
            if (wordEnd >= this.Sentenece.Length)
            {
                return wordEnd - wordStart;
            }

            String word = this.Sentenece.Substring(wordStart, wordEnd + 1);

            /* break current word */
            int bestExact = parseSimple(wordEnd + 1, wordEnd + 1);
            if (!IsWordExistsInDictionary(word))
            {
                bestExact += word.Length;
            }

            /* extend current word */
            int bestExtend = parseSimple(wordStart, wordEnd + 1);

            /* find best */
            return Math.Min(bestExact, bestExtend);
        }


        public int FindMinimumOfUnrecognizedChars(string input, int startindex, int endindex, Hashtable ht)
        {

            //List<int> wordindex = new List<int>(); //contains index of the sentence that contain words

            if (endindex <= startindex || endindex > input.Length -1)
            {
                return Int32.MaxValue;
            }
            //each word has two choice..

            ////make it whole word
            //if (endindex + 1 == input.Length)
            //{
               
            //}
            string fullword = input.Substring(startindex, endindex + 1);
            int fullwordcount = 0;
            if (!ht.ContainsKey(fullword))
            {
                fullwordcount = fullword.Length;

            }
           

           //split the word with space after first char

            string left = input.Substring(startindex, 1); //string[startindex]
            string remaining = input.Substring(startindex + 1, endindex );

            int minremaining = FindMinimumOfUnrecognizedChars(input, startindex + 1, endindex, ht);

            int splitcount = 0;
            if (!ht.ContainsKey(left))
            {
                splitcount = splitcount + left.Length;
            }
            splitcount = splitcount + minremaining;


            // or put space after first char

            return Math.Min(fullwordcount, splitcount);

        }


        public int FindNumberofUnrecognizedCharacters(int wordstart, int wordend)
        {
            if (wordend < wordstart)
            {
                return wordend - wordstart;
            }

             /* break current character */
             int spliwordcounter = 0;
             string prefix = this.Sentenece.Substring(wordstart, 1);
             string remainingsuffix = this.Sentenece.Substring(wordstart +1, (wordend + 1) - (wordstart -1 ));
             if(!IsWordExistsInDictionary(prefix))
             {
                //if the word does not exists in dictionary
                spliwordcounter += prefix.Length;
             }

              spliwordcounter += FindNumberofUnrecognizedCharacters(wordstart + 1, wordend + 1);

            /* don't break current character */

            int nospacecounter = 0;

            string prefix2 = this.Sentenece.Substring(wordstart, wordstart + 2);
            string remainingsuffix2 = this.Sentenece.Substring(wordstart +2, wordend - 2);

            if(!IsWordExistsInDictionary(prefix2))
            {
                nospacecounter += prefix2.Length;
            }
              
          
            nospacecounter += FindNumberofUnrecognizedCharacters(wordstart + 2, wordend + 1);


            return Math.Min(spliwordcounter, nospacecounter);

        }
        
    }

    public class WordUnrecognized
    {
        public int MinCount { get; set; }
        public List<string> Words { get; set; }
    }


    
}
