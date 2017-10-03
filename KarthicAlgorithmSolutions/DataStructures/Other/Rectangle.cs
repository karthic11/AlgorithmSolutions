using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Common
{
    //Implementation question..read galyle lakmann page 488, 489
    public class LargestPossibelRectangle
    {


        public Rectangle PerformTheLogicOfThisProblem(Dictionary<string, bool> ht)
        {

            //Now form a largest possible rectangle from the given dictionary of words
            //such that letters from left to right should form a word and letters from top to bottom should form a word

            //The largest possible rectangle will be max of word length * max of word length
            //eg: 6 * 6 (square is also an rectangle)

            //Divide the given words and group it by length
            WordGroup[] wordgroups = CreateWordGroups(ht);

            //Remember the rectangle can be formed by any words.. not consecutive...row is a word from dictionar..so we need to check the colum for word
             Trie[] tries = new Trie[wordgroups.Length];

             Rectangle rect =  BuildMaxRectangle(wordgroups.Length, wordgroups, tries);


            return null;
        }



                //We need to get the max possible rectange 
        //this fn make sure to get max possible rectange
        public Rectangle BuildMaxRectangle(int maxWordlength, WordGroup[] wordgroups, Trie[] tries)
        {
            
          List<string> test = new List<string>();
          int maxarea = maxWordlength * maxWordlength; // 6 x 6 rectange 

            for (int z = maxarea; z >= 1; z--)  // 36, 35, 34....1
            {
                //for (int i = maxWordlength; i <= 1; i--)  //6,5,4,---1
                for (int i = 1; i <= maxWordlength; i++)  //1,2,3,4,5,6
                {
                    if (z % i == 0)  
                    {
                        int j = z / i;

                        if (j <= maxWordlength)
                        {

                            test.Add(i + " * " + j);

                            //test
                            if (i == 3 && j == 3)
                            {
                                string test1 = "";
                            }

                            //Create a rectange of length i and height j  here i*j = z
                            Rectangle rec = MakeRectange(i, j, wordgroups, tries);

                          

                            if (rec != null)
                            {
                                return rec;
                            }

                        }
                    }
                }
            }

            return null;
        }

        public Rectangle MakeRectange(int length, int height, WordGroup[] WordGrouplist, Trie[] tries)
        {
            //If there are no words in the group then return null
            if (WordGrouplist[length - 1] == null || WordGrouplist[height - 1] == null)
            {
                return null;
            }

           //we are building rectangle for row/length and height/column given
            //row is already a valid word from ht
            //but height/column is not..so create trie of given height if not exist
            if (tries[height - 1] == null)
            {
                Trie mytrie = new Trie();
                mytrie.Insert(WordGrouplist[height - 1].groupwords);
                //get all workd for the given height from list
                //and build trie from it
                tries[height - 1] = mytrie;
            }


            //return BuildPartialRectangle(length, height, new Rectangle(length), WordGrouplist, tries);
            //testing

           Rectangle rect =  BuildPartialRectangle(length, height, new Rectangle(length), WordGrouplist, tries);

           if (rect != null)
           {
               string test = "";
           }

           return rect;

        }



        public WordGroup[] CreateWordGroups(Dictionary<string, bool> ht)
        {
            //We need to group all the words in the dictionary

            //Find the max length of the group
            int maxlength = -1;
            foreach (KeyValuePair<string, bool> item in ht)
            {
                if (item.Key.Length > maxlength)
                {
                    maxlength = item.Key.Length;
                }

            }

            WordGroup[] wordgroups = new WordGroup[maxlength];

            //scan the words and put it in the corresponding groups
            foreach (KeyValuePair<string, bool> item in ht)
            {
                int length = item.Key.Length - 1;
                if (wordgroups[length] == null)
                {
                    wordgroups[length] = new WordGroup();
                }

                wordgroups[length].addWord(item.Key);
            }

            return wordgroups;
            
        }


        //This is the fuction that actually build rectangle with all the possible words for the given lenght and height
        public Rectangle BuildPartialRectangle(int length, int height, Rectangle partialrec, WordGroup[] WordGrouplist, Trie[] tries)
        {
            //check whether rectangle is complete
            if (partialrec.Height == height)
            {
                //check valid 
                if (partialrec.IsComplete(length, height, WordGrouplist[height - 1]))
                {
                    return partialrec;
                }
                else
                {
                    return null;
                }
            }

            //check whether we are build corrrect rect..compare columns with trie and see whether we are good to move
            if (partialrec.IsPartialOk(length, tries[height - 1]) == false)
            {
                return null;
            }

            //Get all the words of the given length and add each one by one
            //Go all possible combination to build this rectangle recusively
            List<string> words = WordGrouplist[length - 1].groupwords; //get all the words
            for (int i = 0; i < words.Count; i++)
            {
                //test
                if (partialrec.Height == 3 && length == 3 && height == 3)
                {
                    string test2 = "";
                }


                Rectangle newrec = partialrec.AppendString(words[i]);// add each word from the list

                //use the recursion to see all the possible combination
                Rectangle rec = BuildPartialRectangle(length, height, newrec, WordGrouplist, tries);
                if (rec != null)
                {
                    return rec;
                }
            }


            return null;
        }
    }





    public class Rectangle
    {
        public int Length { get; set; }
        public int Height { get; set; }
        //public int CurrentHeight { get; set; }
        public char[,] matrix { get; set; }

        public Rectangle()
        {
        }
        public Rectangle(int length, int height)
        {
            this.Length = length;
            this.Height = height;
            this.matrix = new char[height, length];

        }

        public Rectangle(int length)
        {
            this.Length = length;
            this.Height = 0;
            this.matrix = new char[this.Height, length];

        }

        public void IncrementHeight()
        {
            this.Height = this.Height + 1;

            char[,] copymatrix = new char[this.Height, this.Length];

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    copymatrix[i, j] = this.matrix[i, j];
                }
            }

            this.matrix = copymatrix;
        }

        public Rectangle(int length, int height, char[,] letters)
        {
            this.Height = height;
            this.Length = length;
            this.matrix = letters;
        }

        //Create a new rectangle by appended row of the current rectangle
        public Rectangle AppendString(string s)
        {
     
            Rectangle copyofcurrentrectangle = this;

            ////A word is added so increment the height
            //this.Height = this.Height + 1;
            copyofcurrentrectangle.IncrementHeight();

            for (int i=0; i < s.ToArray().Length; i++)
            {
                copyofcurrentrectangle.matrix[copyofcurrentrectangle.Height -1, i] = s[i]; 
            }

            
            //copyofcurrentrectangle.Height = copyofcurrentrectangle.Height + 1;

            return copyofcurrentrectangle;
        }

        public bool IsComplete(int l, int h, WordGroup list)
        {
            //check whether each column is a valide word
            //and height has required h
            if (this.Height == h)
            {
                // check whehter each column for a word in the ht
                for (int col = 0; col < l; col++)
                {
                    string columnword = GetColumnWordByColumnNo(col);
                    if (list.lookup.ContainsKey(columnword) == false)
                    {
                        return false;
                    }

                }


            }

            return true;
            
        }

        public bool IsPartialOk(int column, Trie trie)
        {
            if (this.Height == 0)
            {
                return true;
            }
            //for each of the column that is build ..check whether there may be a possible word
            for (int i = 0; i < Length; i++)
            {
                string columnword = GetColumnWordByColumnNo(i);

                //test
                if (columnword.ToLower() == "bb")
                {
                    string test = "";
                }
                if (trie.CheckWhetherWordCanBeFormed(columnword) == false)
                {
                    return false;
                }
            }

            return true;
        }


        public string GetColumnWordByColumnNo(int col)
        {
            //here height is no of rows and no of rows/height is dynamic
            //length is no of columns
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < this.Height; row++)
            {
                sb.Append(matrix[row, col]);
            }

            return sb.ToString();
        }

        public char getletter(int length, int height)
        {
            return this.matrix[length, height];
        }
       
    }

    public class WordGroup
    {
       

        public Dictionary<string, bool> lookup { get; set; }
        public List<String> groupwords { get; set; }

        public WordGroup()
        {
            lookup = new Dictionary<string, bool>();
            groupwords = new List<string>();
        }

        public bool ContainsWord(String word)
        {
            return lookup.ContainsKey(word);
        }

        public void addWord(String word)
        {
            groupwords.Add(word);
            lookup.Add(word, true);
        }
    }
}
