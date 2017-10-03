using System;
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
  public partial class ArrayAndString : Form
  {
    public ArrayAndString()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {

 //Take 2 pointers, one from Start, other From End 
//and Keep Swapping 0 and non-zero
    }

    private void button9_Click(object sender, EventArgs e)
    {
      //suse custom sort to put zero in the first place
    }

    private void button11_Click(object sender, EventArgs e)
    {
        int size = Convert.ToInt32(this.textBox16.Text);
        int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);
        this.textBox13.Text = FindtheMissingNumberUsingFormula(input, size).ToString();

    }

    //      1. Get the sum of numbers 
    //       total = n*(n+1)/2
    //2  Subtract all the numbers from sum and
    //   you will get the missing number.


    private int FindtheMissingNumberUsingFormula(int[] input, int range)
    {
        int n = range; //range of 20 {1,2.....20)
        int total = (n * (n + 1)) / 2;

        foreach(int num in input)
        {
            total = total - num;
        }

        return total;

     
    }


    private void button2_Click(object sender, EventArgs e)
    {

        int size = Convert.ToInt32(this.textBox16.Text);
        int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox11.Text);
        this.textBox13.Text = FindtheMissingNumberUsingXOR(input, size).ToString();

    }


    // 1) XOR all the array elements, let the result of XOR be X1.
    //2) XOR all numbers from 1 to n, let XOR be X2.
    //3) XOR of X1 and X2 gives the missing number.
    private int FindtheMissingNumberUsingXOR(int[] input, int range)
    {
        int x1 = input[0];
        //XOR all the array elements
        for(int j = 1; j < input.Length; j++)
        {
            x1 = x1 ^ input[j];
        }
        //2) XOR all numbers from 1 to n
        int x2 = 1;
        for (int i = 2; i <= range; i++)
        {
            x2 = x2 ^ i;
        }

        return x1 ^ x2;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        int[] input1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox2.Text);
        int[] input2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);

        this.textBox1.Text = FindMissingNumberUsingHashtable(input1, input2).ToString();
    }
    //since there may be duplicates in the given number ht is used with key as number and values as occurance of the number
    private int FindMissingNumberUsingHashtable(int[] input1, int[] input2)
    {
        int[] shortestarray = null;
        int[] longestarray = null;
        if(input1.Length < input2.Length)
        {
            shortestarray = input1;
            longestarray = input2;
        }
        else
        {
            shortestarray = input2;
            longestarray = input1;
        }
        Dictionary<int, int> ht = new Dictionary<int, int>();
        foreach (int num in shortestarray)
        {
            if (ht.ContainsKey(num))
            {
                ht[num] = ht[num] + 1;
            }
            else
            {
                ht.Add(num, 1);
            }
             
        }

        foreach (int num in longestarray)
        {
            if (ht.ContainsKey(num))
            {
                ht[num] = ht[num] - 1;

                if (ht[num] < 0)
                {
                    return num;
                }

            }
            else
            {
                return num;
            }
        }


        return -1;


    }

    private void button3_Click(object sender, EventArgs e)
    {
        int[] input1 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox2.Text);
        int[] input2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox3.Text);

        this.textBox1.Text = FindMissingNumberUsingXOR(input1, input2).ToString();
    }

    //Time o(n) and Space o(1)..no extra datastructures used
      //n is elements in both array
    private int FindMissingNumberUsingXOR(int[] input1, int[] input2)
    {

        //XORing a 4-bit number with 1011 would flip the first, third, and fourth bits of the number. 
        //XORing the result again with 1011 would flip those bits back to their original value. So, if we XOR a number two times with some number nothing will change.
        //We can also XOR with multiple numbers and the order would not matter. 
        //For example, say we XOR the number n1 with n2, then XOR the result with n3, then XOR their result with n2, and then with n3.
        //The final result would be the original number n1. Because every XOR operation flips some bits and when we XOR with the same number again, we flip those bits back.
        //So the order of XOR operations is not important. If we XOR a number with some number an odd number of times, there will be no effect. - See more at:
        //http://www.ardendertat.com/2011/09/27/programming-interview-questions-4-find-missing-element/#sthash.9EVRdvmC.dpuf
        //eg: if we do this 3 xor 11 and then with the result xor 11, we will get 3 back.
        //Order and multiple times doesn't matter

        //here we xor all the element of both array the result will be the missing elements
        int xorresult = input1[0];

        for (int i = 1; i < input1.Length; i++)
        {
            xorresult = xorresult ^ input1[i];
        }

        for (int i = 0; i < input2.Length; i++)
        {
            xorresult = xorresult ^ input2[i];
        }

        return xorresult;

    }

    private void button10_Click(object sender, EventArgs e)
    {
       // http://www.geeksforgeeks.org/find-a-repeating-and-a-missing-number/
        int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox6.Text);
        int n = Convert.ToInt32(this.textBox4.Text);

        int x = 0;
        int y = 0;
        getTwoElements(input, n,out x, out y);
        this.textBox5.Text = x.ToString();
        this.textBox7.Text = y.ToString();




    }

    private void button8_Click(object sender, EventArgs e)
    {
        //http://www.geeksforgeeks.org/find-a-repeating-and-a-missing-number/

        /* Build a ht using key as num and occureance as value
         * First scan the given array and insert into ht
         * Then iterate the range 1 to givenrange. 
         * In this iteration, If the ht doesn't contain a number it is missing
         * If the ht contains values greater than 2 then it is repeating
         * 
         * 
         */
    }

      // the least significant bit (LSB) is the bit position in a binary integer giving the units value, that is, determining whether the number is even or odd. The LSB is sometimes referred to as the right-most bit, 



      /* The output of this function is stored at *x and *y */
public void getTwoElements(int[] arr, int n, out int x, out int y)
{
  int xor1;   /* Will hold xor of all elements and numbers from 1 to n */
  int set_bit_no;  /* Will have only single set bit of xor1 */
  int i;
  x = 0;
  y = 0;
 
  xor1 = arr[0];
 
  /* Get the xor of all array elements elements */
  for (i = 1; i < n; i++)
  {
      xor1 = xor1 ^ arr[i];
  }
 
  /* XOR the previous result with numbers from 1 to n*/
  for (i = 1; i <= n; i++)
  {
      xor1 = xor1 ^ i;
  }
 
  /* Get the rightmost set bit in set_bit_no */
  set_bit_no = xor1 & ~(xor1-1);
 
  /* Now divide elements in two sets by comparing rightmost set
   bit of xor1 with bit at same position in each element. Also, get XORs
   of two sets. The two XORs are the output elements.
   The following two for loops serve the purpose */
  for(i = 0; i < n; i++)
  {
      if ((arr[i] & set_bit_no) == 1)
      {
          x = x ^ arr[i]; /* arr[i] belongs to first set */
      }
      else
      {
          y = y ^ arr[i]; /* arr[i] belongs to second set*/
      }
  }
  for(i = 1; i <= n; i++)
  {
      if ((i & set_bit_no) == 1)
      {
          x = x ^ i; /* i belongs to first set */
      }
      else
      {
          y = y ^ i; /* i belongs to second set*/
      }
  }
 
  /* Now *x and *y hold the desired output elements */
}
 

  }
}
