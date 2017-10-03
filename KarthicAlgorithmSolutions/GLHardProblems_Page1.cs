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
    public partial class GLHardProblems_Page1 : Form
    {
        public GLHardProblems_Page1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);

            string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(FisherYatesShuffleAlgorithm(input));
            this.textBox1.Text = output;


        }


        private int[] ShuffleArray(int[] cards)
        {
            //logic..
            //create temp[] array which is copy of the given array
            //int[] temp = new int[cards.Length];
            //Array.Copy(cards, temp, cards.Length);

            List<int> templist = new List<int>();
            templist = cards.ToList();

            Random random = new Random();
            //now we have a copy of the given array
            for (int i = 0; i < cards.Length; i++)
            {
                //random.next(min, max) min is including but max is not so we have to add + 1 here instead of length -1 we have given length
                int randomindex = random.Next(0, templist.Count);
                int randomvalue = templist[randomindex];

                cards[i] = randomvalue;

                //remove the item from temp on the list
                templist.RemoveAt(randomindex);
                

            }

            return cards;
           
        }


        private int[] FisherYatesShuffleAlgorithm(int[] cards)
        {

           //http://www.geeksforgeeks.org/shuffle-a-given-array/
            //Logic...Given Random() to given random number from min to max in 0(1)...
            //Start from the last element to first
            //Select a random index from the array 0 to last index inclusive..
            //swap the values of random index and last index
            //and repeat the process till we hit the first element.

            Random random = new Random();
            //we don't have to do this for first element it will already shuffled
            for (int i = cards.Length - 1; i > 0; i--)
            {
                //next should have o to last index inclusive
                int randomindex = random.Next(0, i + 1);

                //swap element
                int temp = cards[i];
                cards[i] = cards[randomindex];
                cards[randomindex] = temp;

            }

            return cards;
           
        }


             private void button1_Click(object sender, EventArgs e)
        {

            int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);

            string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(ShuffleArray(input));
            this.textBox1.Text = output;


        }

             private void button3_Click(object sender, EventArgs e)
             {
                 int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);
                 int m = Convert.ToInt32(this.textBox5.Text);
                 string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(GeneratorRandomArrayMethod1(input, m));
                 this.textBox2.Text = output;
             }


             private int[] GeneratorRandomArrayMethod1(int[] array, int m)
             {
                 //since we don't need to change the given input array..i am cloning it
                 int[] temp = new int[array.Length];
                 Array.Copy(array, temp, array.Length);
                 Random random = new Random();
                 int[] subset = new int[m];

                 for (int i = 0; i < m; i++)
                 {

                     //inclusive..we pass i bcoz the
                     int randomindex = random.Next(i, temp.Length );

                     subset[i] = temp[randomindex];
                     //we used the randomindex value for subset so remove that to avoid duplicates
                     //instead swapping we overwrite the temp[randomindex] with temp[i]
                     //the value that was overwritten is lost and that is the value added to the subset
                     temp[randomindex] = temp[i];


                 }

                 return subset;
             }

             private int[] GeneratorRandomArrayMethod2(int[] array, int m)
             {
                 Random random = new Random();
                 // 1,2,3,4,5,6, 7, 8
                 //subset 1, 2,3,4,5,6
                 int[] subset = new int[m];
                 //copy first m from original to result
                 for (int i = 0; i < m; i++)
                 {
                     subset[i] = array[i];

                 }
              
                 //now we are iteration from 6 to 8 and inserting into some random index (0 to 5)..no way for duplicates
                 for (int j = m; j < array.Length; j++)
                 {
                     //important o to  j not o to m
                     int randomindex = random.Next(0, j);

                     if (randomindex < m)
                     {
                         subset[randomindex] = array[j];
                     }
                 }

                 return subset;
             }

             private void button4_Click(object sender, EventArgs e)
             {

                 int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);
                 int m = Convert.ToInt32(this.textBox5.Text);
                 string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(GeneratorRandomArrayMethod2(input, m));
                 this.textBox2.Text = output;

             }

             private void button5_Click(object sender, EventArgs e)
             {
                 int input = Convert.ToInt32(this.textBox8.Text);
                 string output = CountNumberof2InRange(input).ToString();
                 this.textBox7.Text = output;


             }

             private int CountNumberof2InRange(int number)
             {
                 //simple..loop through  2 to number
                 //find the no of possible 2 in each number
                 int count = 0;
                 //instead starting from 0 we can start from 2 :)
                 for (int i = 2; i <= number; i++)
                 {
                     count += Numberof2InNumber(i);

                 }

                 return count;
             }

            //This method calculates the number of 2 in the given number
            //eg 22  = 2, 32 = 1,  322 = 2 , 20 = 1
             private int Numberof2InNumber(int number)
             {
                 int count = 0;

                 while (number > 0)
                 {
                     if (number % 10 == 2)
                     {
                         count++;
                     }

                     number = number / 10;
                 }

                 return count;
             }

             private void button6_Click(object sender, EventArgs e)
             {

                 int input = Convert.ToInt32(this.textBox8.Text);
                 string output = CountNumberof2InOptimalWay(input).ToString();
                 this.textBox7.Text = output;

             }


             private int CountNumberof2InOptimalWay(int number)
             {

                 int sum = 0;
                 int length = number.ToString().Length;
                 for (int i = 0; i < length; i++)
                 {
                     sum += Count2sInRangeATDigit(number, i);
                 }

                 return sum;

             }



             //Logic
             //Here we calculate the number of 2 as digit basis not in range..that is number of possible 2 in each digit of the string from  string.length -1 to 0
             //eg  x = 61523   ..x[d] = 1 where d = 3   ..x[0] = 3, x[1] = 2 
             // Consider the digit 1 at 3rd index of the string 61523 
             //This digit has three possible ways :
             //It is fun to make this calculation but see the Gayle book pg 466 for the calculation..but here is the formula for each case
             //1) Digit lesser than 2         Round Down to the greater power of 10^(d+1) of the given number that is 60000 and then divide by 10 = 6000
             //2) Digit greater than 2        Round Up to the greatest power of 10^(d + 1) of the given number that is 7000 and then divide by 10 = 7000
             //3) Digit equal to 2         Eg: 62523 Round Down to the greater power of 10^(d+1) of the given number that is 60000 and then add (number / 10d) (to the right of that index) + 1 = 6000 + 523 + 1 = 6524

             private int Count2sInRangeATDigit(int number, int d)
             {
                 int highestpowerof10 = (int)Math.Pow(10, d);

                 int Nexthighestpower = highestpowerof10 * 10;

                 int right = number % highestpowerof10;  // 61523 % 1000 = 523

                 int rounddown = number - (number % Nexthighestpower);
                 int roundup = rounddown + Nexthighestpower;

                 //d = 3 where digit = 1 in 61523
                 int digit = (number / highestpowerof10) % 10;  // 61523 / 1000 = 61 and then 61 % 10 = 1

                 if (digit < 2)
                 {
                     return rounddown / 10;
                 }
                 else if (digit > 2)
                 {
                     return roundup / 10;
                 }
                 else
                 {
                     //equal to 2
                     return (rounddown / 10) + right + 1;
                 }


             }

             private void button7_Click(object sender, EventArgs e)
             {
                 string input = this.textBox9.Text;

                 //read the txt file and convert into string[] words
                 string[] words = input.Trim().Split(' ');
                 string word1 = this.textBox6.Text;
                 string word2 = this.textBox10.Text;
                 string output = ShortestDistanceBetweenTwoWords(word1.ToLower(), word2.ToLower(), words).ToString();
                 this.textBox11.Text = output;


             }

             private int ShortestDistanceBetweenTwoWords(string word1, string word2, string[] words)
             {
                 int min = Int32.MaxValue;
                 //keep two pointers to keep track of the last position of word 1 and word2 
                 int lastword1position = -1;
                 int lastword2position = -1;

                 //traverse through all the words and find the 
                 for (int i = 0; i < words.Length; i++)
                 {
                     //check for word1
                     if (words[i].ToLower() == word1.ToLower())
                     {
                         lastword1position = i;
                         //if the order of word does not count eg: you can   can you 
                         //below three lines can be commented
                         if (lastword2position > -1)
                         {
                             int distance = lastword1position - lastword2position ;
                             if (distance < min)
                             {
                                 min = distance;
                             }
                         }
                     }
                     else if (words[i].ToLower() == word2.ToLower())
                     {
                         lastword2position = i;

                         if (lastword1position > -1)
                         {
                             int distance = lastword2position - lastword1position;
                             if (distance < min)
                             {
                                 min = distance;
                             }
                         }
                     }
                 }


                 return min;
             }

             private void button8_Click(object sender, EventArgs e)
             {


                 string input = this.textBox9.Text;
                 //read the txt file and convert into string[] words
                 string word1 = this.textBox6.Text;
                 string word2 = this.textBox10.Text;
                 //testing
                 //input = "is was is was in old check this is on test this in lady is in";
                 //word1 = "is";
                 //word2 = "in";
                 string[] words = input.Trim().Split(' ');
                 Hashtable ht = BuildHashTableusingwords(words);

                 string output = ShortDistanceUsingHashtable(word1.ToLower(), word2.ToLower(), ht).ToString();
                 this.textBox11.Text = output;


             }

            
             private int ShortDistanceUsingHashtable(string word1, string word2, Hashtable ht)
             {
                 //The hashtable has the values of the word1 and word2
                 //check for both word1 and word2 are in ht
                 string word1values = string.Empty;
                 string word2values = string.Empty;

                 if (ht.ContainsKey(word1))
                 {
                     word1values = (string)ht[word1.ToLower()];
                 }
                 else
                 {
                     //return error
                 }

                 if (ht.ContainsKey(word2))
                 {
                     word2values = (string)ht[word2.ToLower()];
                 }
                 else
                 {
                     //return error
                 }

                 //we will have two list
                 //remove the comma on the last character if present
                 if(word1values.IndexOf(',') != -1)
                 {
                     word1values = word1values.Substring(0, word1values.LastIndexOf(','));
                 }
                 if(word2values.IndexOf(',') != -1)
                 {
                     word2values = word2values.Substring(0, word2values.LastIndexOf(','));
                 }
                 int[] word1list = AlgorithmHelper.ConvertCommaSeparetedStringToInt(word1values);
                 int[] word2list = AlgorithmHelper.ConvertCommaSeparetedStringToInt(word2values);

                 //now we have two list now there are many ways to find the minimum difference between a value between list a and list b...
                 //assumption order doesn't count
                 //below are the ways
                 //1) Both the list are already sorted so compare the value from list1 to list2 find the minimun differece 
                 //2) Merge these list into one sored list but tag each number from the list it below to 

                 //I will go with option 2 bocz we applying array sort and merge on the cusotm class interesting

                 WordTag[] wordtags1 = ConverListToCustomClas(word1list, "a");
                 WordTag[] wordtags2 = ConverListToCustomClas(word2list, "b");
                 
                 //Merge the two array
                 WordTag[] wordtags = new WordTag[wordtags1.Length + wordtags2.Length];

                 Array.Copy(wordtags1, wordtags, wordtags1.Length);
                 Array.Copy(wordtags2, 0, wordtags, wordtags1.Length , wordtags2.Length);

                 Array.Sort(wordtags, new WordTagComparer());

                 //here we will have sorted list now finding the 

                 return MinimumDistanceBetweenTwoTags(wordtags);

             }




             private Hashtable BuildHashTableusingwords(string[] words)
             {
                 //build hashtable with key and value as the following
                 //key       value
                 //word     index of occurence
                 //luck     1, 5, 6
                 //is       2, 9, 11

                 Hashtable ht = new Hashtable();
                 for (int i = 0; i < words.Length; i++)
                 {
                     string key = words[i].ToLower();
                     StringBuilder sb = new StringBuilder();
                      
                     if (ht.ContainsKey(key))
                     {
                         string value = (string)ht[key];
                         //get the existing value from ht
                         sb.Append((string)ht[key]);
                         sb.Append(i).Append(",");
                         ht[key] = sb.ToString();
                     }
                     else
                     {
                         //new word not present in hashtable
                         sb.Append(i).Append(",");
                         ht.Add(key, sb.ToString());
                     }

                 }

                 return ht;

             }


             public WordTag[] ConverListToCustomClas(int[] list, string tagname)
             {
                 WordTag[] values = new WordTag[list.Length];
                 for(int i=0; i < list.Length; i++)
                 {
                     values[i] = new WordTag(list[i], tagname.ToLower());
                    
                 }

                 return values;
                
             }

             public class WordTag
             {
                 public int Value { get; set; }
                 public string TagName { get; set; }

                 public WordTag(int value, string tag)
                 {
                     this.Value = value;
                     this.TagName = tag;
                 }
             }


             public class WordTagComparer : IComparer<WordTag>
             {

                 int IComparer<WordTag>.Compare(WordTag x, WordTag y)
                 {
                     return x.Value.CompareTo(y.Value);
                 }
             }


             private int MinimumDistanceBetweenTwoTags(WordTag[] array)
             {
                 //there are two tags
                 //0(a0, 2(a), 4(b), 8(a), 12(b), 14(a), 15(b)
                 //find the distance 
                 int min = Int32.MaxValue;

                 for (int i = 1; i < array.Length; i++)
                 {
                     //if both are different tag
                     if (array[i - 1].TagName != array[i].TagName)
                     {
                         int differnece = array[i].Value - array[i - 1].Value;
                         if (differnece < min)
                         {
                             min = differnece;
                         }

                     }

                 }

                 return min;
             }

             private void button10_Click(object sender, EventArgs e)
             {


                 int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox13.Text);
                 int i = Convert.ToInt32(this.textBox12.Text);
                 //this sln is for ith..for length 
                 //i = i - 1;
                 string output = SortHelper.Findthesmallest(input, i).ToString();
                 this.textBox14.Text = output;

             }

             private void button1_Click_1(object sender, EventArgs e)
             {
                 int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);

                 string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(ShuffleArray(input));
                 this.textBox1.Text = output;
             }

             private void button9_Click(object sender, EventArgs e)
             {

             }

             private void button11_Click(object sender, EventArgs e)
             {

             }

             private void button12_Click(object sender, EventArgs e)
             {
                 int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox13.Text);
                 int i = Convert.ToInt32(this.textBox15.Text);
                 //this sln is for ith..for length 
                 //i = i - 1;
                 string output = SortHelper.FindtheLargest(input, i).ToString();
                 this.textBox14.Text = output;
             }

             private void GLHardProblems_Page1_Load(object sender, EventArgs e)
             {

             }

             private void button13_Click(object sender, EventArgs e)
             {
                 // 5,2,17,13,14,17,19,14,18,2,4,8,2,5,0,6,13,4,12,18,10 

                 // Logic
                 //1. Select a pivot element based on Median Of Medians(see below).
                 //2. Rearrange numbers such that, elements to the left of pivot and smaller and elements to the right are greater than the pivot.
                 //3. Let pivotIndex be the index of the pivot.
                 //4. If pivotIndex == arr.length/2,  we have found the median, return pivotIndex.
                 //5. else if pivotIndex < arr.length/2,  recursively search for Median in the left subArray, arr[start……pivotIndex-1]
                 //6. else if pivotIndex > arr.length/2,  recursively search for Median in the right subArray, arr[pivotIndex+1…end]
             }

    }
}
