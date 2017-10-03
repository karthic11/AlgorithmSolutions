using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Puzzles.DataStructures.Array;

namespace Puzzles
{
  public partial class ArrayAndStringP5 : Form
  {
    public ArrayAndStringP5()
    {
      InitializeComponent();
    }

    //This program is actully to strip comment in C/c++ file since i don't know those laKarthicages..i am stripping lines from C# in C#
    //For c program see here http://c-programmingguide.blogspot.com/2012/10/c-program-to-remove-comments-from-c.html

    private void button2_Click(object sender, EventArgs e)
    {
      string input = this.textBox8.Text;
      this.textBox6.Text = StripComments(input);
    }

    public string StripComments(String input)
    {

      var blockComments = @"/\*(.*?)\*/";
      var lineComments = @"//(.*?)\r?\n";
      var strings = @"""((\\[^\n]|[^""\n])*)""";
      var verbatimStrings = @"@(""[^""]*"")+";

    
      string noComments = Regex.Replace(input,
    blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
    me =>
    {
      if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
        return me.Value.StartsWith("//") ? Environment.NewLine : "";
      // Keep the literal strings
      return me.Value;
    },
    RegexOptions.Singleline);

      return noComments;

    }

    private void button1_Click(object sender, EventArgs e)
    {
      // Build a 3x3 matrix
      //Assumption 0 means empty, 1 means red, 2 means blue
      int[,] board = new int[,]{{1,0,1},
                             {2,1,0},
                             {2,2,1}};
      int result = HasWontheGame(board);
      string output = string.Empty;
      switch (result)
	   { 
        case 0:
          output = "No winner found";
          break;
        case 1:
          output = "Red " + result +" is the winner";
          break;
        case  2:
           output = "Blue " + result + "is the winner";
           break;
	
      }
      
      this.textBox1.Text = output;
    }

    //if return is 0 - no winner, 1 mean red winner , 2 means blue winner
    public int HasWontheGame(int[,] board)
    {
      int size = board.GetLength(0);
      int row = 0;
      int col = 0;
      //check for horizondal rows
      for (row = 0; row < size; row++)
      {
        if (board[row, 0] != 0)
        {
          //column traversal
          for (col = 1; col < size; col++)
          {
            if (board[row, col - 1] != board[row, col])
            {
              break;
            }
          }
          //if the iteration succeded till the end means horizondal match found
          if (col == size)
          {
              //get the value of the winner
            return board[row, 0];
            
          }
        }
      }

      //check for the vertical winner columns

      for (col = 0; col < size; col++)
      {
        if (board[0, col] != 0)
        {
          for (row = 1; row < size; row++)
          {
            if (board[row - 1, col] != board[row, col])
            {
              break;
            }
          }

          //vertical match found
          if (row == size)
          {
            return board[0, col];
          }
        }
      }

      //check for diagonal

      if (board[0, 0] != 0)
      {
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
          return board[0, 0];
        }
      }
     
      //check for reverse diagonal
      if (board[2, 0] != 0)
      {
          //Hard code the value and test
        if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
        {
          return board[2, 0];
        }
      }

      return 0;
    }


    public int HasWonTheGameofNXNMatrix(int[,] board)
    {
      int size = board.GetLength(0);
      int col = 0;
      int row = 0;
      //check the rows
      for(row =0; row < size; row++)
      {
        if (board[row, 0] != 0)
        {
          for (col = 1; col < size; col++)
          {
            if (board[row, col - 1] != board[row, col])
            {
              break;
            }
          }

          if (col == size)
          {
            return board[row, 0];
          }
        }
      }

      //check for vertical columns

      for (col = 0; col < size; col++)
      {
        if (board[0, col] != 0)
        {
          for (row = 1; row < size; row++)
          {
            if (board[row - 1, col] != board[row, col])
            {
              break;
            }

            if (row == size)
            {
              return board[0, col];
            }
          }
        }
      }

      //check for diagonal..we can use the same variable but to avoid confusion..i didn't
      if (board[0, 0] != 0)
      {
        int i = 1;
        for (i = 1; i < size; i++)
        {
          if (board[i -1, i -1] != board[i , i ])
          {
            break;
          }
        }

        if (i == size)
        {
          return board[0, 0];
        }
      }

      //check for the diagonal bottom left to top right
      if (board[size - 1, 0] != 0)
      {
        int j = 1;
        for (j = 1; j < size; j++)
        {
          if (board[((size - 1) - (j-1)), j - 1] != board[(size - 1) - j, j])
          {
            break;
          }
        }

        if (j == size)
        {
          return board[size - 1, 0];
        }
      }


      return 0;
      
    }

    public enum TicTacToePiece
    {
         Empty = 0,
         Red = 1,
         Blue = 2
    }

    private void button3_Click(object sender, EventArgs e)
    {

      // Build a 3x3 matrix
      //Assumption 0 means empty, 1 means red, 2 means blue
      int[,] board = new int[,]{{1,0,1,2,2},
                                {2,1,0,2,1},
                                {2,2,2,1,0},
                                {1,2,2,2,1},
                                {2,2,2,1,0}};



      int result = HasWonTheGameofNXNMatrix(board);
      string output = string.Empty;
      switch (result)
	   { 
        case 0:
          output = "No winner found";
          break;
        case 1:
          output = "Red " + result +" is the winner";
          break;
        case  2:
           output = "Blue " + result + "is the winner";
           break;
	
      }
      
      this.textBox1.Text = output;

    }

    private void button4_Click(object sender, EventArgs e)
    {
        //The other solution are better if the winner function is called once
        //This solution is to give another idea may or may not be best  (optimal suggestion)..depends on the use
      // Logic on 3x3 board there are 9 boxes and each box may have one of the 3 value (0,1,2)
      //so there are total of 3^9 around 20,000 board layouts..
      //Imagine if the board is preprocessed with all the possible layout and the winner for each layout in hashtable
      //ht with key as the board layout (0 to 3 power 9)and value as the winner (0,1,2)
      //if this already exsists then we can have a function like below

       //update 6/21/2015
       //we should have a ht with key as boardlayout number and values as winner (0,1,2)

      //we need to preprocess all the boardlayout. Each board layout can be converted into a number using the below formula

      //For given 3*3 matrix there are 3 pow 9 boxes..The sum will be
      // int sum  = 3 pow 0 * v0 + 3 pow 1 * v1 + 3 pow 2 * v2 + ......+  3 pow 9 * v8
      //where v will be 0 for empty, 1 for red, 2 for blue

        // Build a 3x3 matrix
        //Assumption 0 means empty, 1 means red, 2 means blue
        int[,] board = new int[,]{{1,0,1,2,2},
                                {2,1,0,2,1},
                                {2,2,2,1,0},
                                {1,2,2,2,1},
                                {2,2,2,1,0}};

        int boardlayoutvalue = ConvertBoardLayoutToValue(board);

      //The parm is boardlayoutvalue
      //how to convert a board to a int value
      //Each board is represented as 3 power 0 & ok..refer the Gayle book on pg: 432

      //ANOTHER way to check winner in contact time
      //Have sepearate array for each row,col, digonal and anti-diagonal for eg Array row1, row2, row3, row4, row5 eg
      //During each move update the value of the corresponding array with the value of the player 1 for red, -1 for blue
      //If there is winner the sum of atleast 1 array will be size -1 

    }

    //assumption is is n*n matrix ..all tic tac toe are n*n
    private int ConvertBoardLayoutToValue(int[,] boardlayout)
    {
        int sum = 0;
        int factor = 1;
        for (int row = 0; row < boardlayout.GetLength(0); row++)
        {
            for (int col = 0; col < boardlayout.GetLength(1); col++)
            {

                 // int sum  = 3 pow 0 * v0 + 3 pow 1 * v1 + 3 pow 2 * v2 + ......+  3 pow 9 * v8
                int v = boardlayout[row,col]; //get tic tac toe coin value
                sum = sum + v * factor;
                factor = factor * boardlayout.GetLength(0); //for 3 * 3 factor = factor * 3

            }
        }

        return sum;
    }

    public int GetWinnerByBoardLayout(int boardlayoutvalue, Hashtable boarddefinitons)
    {
      // return ht[boardlayoutvalue]
      return -1;
    }

    private void button8_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);

     int[] output =  SortHelper.MergeSort(input, true);

      StringBuilder sb = new StringBuilder();
      foreach (int i in output)
      {
        sb.Append(i).Append(',');
      }

      this.textBox3.Text = sb.ToString();

    }

    private void button6_Click(object sender, EventArgs e)
    {
      int[] input1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox7.Text);
      int[] input2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);

      int[] input = new int[input1.Length + input2.Length];
      Array.Copy(input1, input, input1.Length);
      Array.Copy(input2, 0, input, input1.Length, input2.Length);
      Array.Sort(input);

      int[] noncommon = ArrayHelper.GetNonCommonItemsInSortedArray(input);

      string output = AlgorithmHelper.ConvertIntArrayToCommaSeparatedString(noncommon);
      this.textBox9.Text = output;
    }

    private void button9_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox12.Text);

      //calculate sum and avg

      int sum = 0;
      foreach (int i in input)
      {
        sum += i;

      }

      double avg = sum / input.Length;

      //sort the given array
      Array.Sort(input);
      //get the first index
      int index = FindTheFirstLargestOfGiven(input, avg);

      StringBuilder sb = new StringBuilder();

      for (int i = index; i < input.Length; i++)
      {
        sb.Append(input[i]).Append(",");
      }


      this.textBox10.Text = sb.ToString();
      this.textBox11.Text = avg.ToString();

    }

    //this function returns the index of first largest or equal element for the given number
    public int FindTheFirstLargestOfGiven(int[] array, double avg)
    {

     
      int low = 0;
      int high = array.Length - 1;

      while (low < high)
      {
        int middle = (low + high) / 2;

        //if (array[middle] == avg)
        //{
        //  return middle;
        //}
        if (array[middle] < avg)
        {
          low = middle + 1;
        }
        else
        {
          high = middle;
        }
      }

      return high;

    }

    private void ArrayAndStringP5_Load(object sender, EventArgs e)
    {

    }

    private void button5_Click(object sender, EventArgs e)
    {

    }


   
  }
}
