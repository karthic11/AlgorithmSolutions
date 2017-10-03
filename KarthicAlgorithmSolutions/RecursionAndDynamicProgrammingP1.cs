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
  public partial class RecursionAndDynamicProgrammingP1 : Form
  {
    public RecursionAndDynamicProgrammingP1()
    {
      InitializeComponent();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      /*
      *  Example   n= 3.
      *  Output :  ((())) , (()()) , (())() , ()(()), ()()()
      *
      *  The problem can be solved by recursi  on. Select left or a right paren.
      *  If count(LeftParen) > 0 one can insert left paren.
      *  If count(RightParen) > count(LeftParen) i.e. no. of Left paren in an expression is greater than no. of right paren in the expression one can insert right parent.
      *
      *  1. Check for base case i.e. when no left or right paren are left all used up.
      *  2. If base case then print the string of parenthesis.
      *  3. If count of left parentheses > 0 then
      *        Insert Left parentheses in string str and decrement count of left parentheses by 1 and make recursive call.
      *  4. If count of right parentheses > count of left parentheses
      *        Insert right parentheses in string str and decrement count of right parentheses by 1 and make recursive call.
      *
*/ 

      int input = Convert.ToInt32(this.textBox1.Text);

      List<String> strcombination = new List<string>();
      char[] str = new Char[input * 2];
      AddParentheses(strcombination, input, input, str, 0);
      StringBuilder sb = new StringBuilder();
      foreach (String s in strcombination)
      {
        sb.Append(s).Append(",");

      }

      this.textBox2.Text = sb.ToString();

    }


    //Logic:
    //This is a recursive fn and we gotto be careful of using leftavailable, rightavailable and indexofchar
    //we are increaing the value and passing to the recurssive function but the actual value of the variable (in the parent method) is not changed
    //so one recursion returns the next recursion uses the parent variables
    //even though char[] has the old values the indexofchar is maintained so we overwrite the character in the array using that


    public void AddParentheses(List<String> strcomination, int leftavailable, int rightavailable, char[] str, int indexofchar )
    {
        //Base condition
       //Invalid condition: If the leftavailable is lesser than 0 or the rightavailable is lesser than left available
      if (leftavailable < 0 || rightavailable < leftavailable) //  ())) for given n=3 rightavailable = 0 , left = 2
      {
        return;
      }

       //if all the parentheses are used
      if (leftavailable == 0 && rightavailable == 0)
      {
        strcomination.Add(new string(str));
      }

      //If there are remaining left parentheses avaiable then use it
      if (leftavailable > 0)
      {
        str[indexofchar] = '(';
        AddParentheses(strcomination, leftavailable - 1, rightavailable, str, indexofchar + 1);
      }

      if (rightavailable > leftavailable)
      {
        str[indexofchar] = ')';
        AddParentheses(strcomination, leftavailable, rightavailable - 1, str, indexofchar + 1);
      }

    }
  }
}
