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
  public partial class StackAndQueue : Form
  {
    public StackAndQueue()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      string input = this.textBox6.Text;

      if (IsValidParenthesis(input))
      {
        this.textBox5.Text = "Is Valid";
      }
      else
      {
        this.textBox5.Text = "Is not valid";
      }


    }

    //Logic: Iterate through the characters..
    //1) whenever you encouter open bracket, push it to the stack
    //2) whenever you encounter close bracket, check the peek of stack has the matching closed bracket,If not matched return false if true continue
    //3) Make sure to check for empty stack at the end for example ((())  and size of the string at the beginning

    //Allowed parenthesis are '(','[','{'
    public bool IsValidParenthesis(string input)
    {

      //check for empty input

       //check length..if not even..return false
      if (input.Length % 2 != 0)
      {
        return false;
      }

      Stack<char> mystack = new Stack<char>();

      foreach (char c in input.ToCharArray())
      {
          //check for open bracket
        if (c == '(' || c == '{' || c == '[')
        {
          mystack.Push(c);
        }
        else
        {
          //if length of stac is zero and the string starts with { ..example " }()"
          if (mystack.Count == 0)
          {
            return false;
          }
          else
          {
              //we can check with the peek of stack but not required..lets pop if not matches has to return false anyhow
            char lastopenbracket = mystack.Pop();
            //we need to check whether we have corresponding close bracket
            if (CheckCorrespondingCloseBracket(lastopenbracket, c) == false)
            {
              return false;
            }

          }
        }
      }

      //if stack is not empty after iterating all the characters return false
      //has to be empty
      return (mystack.Count == 0);
     
    
       

    }


    public bool CheckCorrespondingCloseBracket(char openbracket, char closebracket)
    {
      if ((openbracket == '(' && closebracket == ')')
        || (openbracket == '[' && closebracket == ']')
        || (openbracket == '{' && closebracket == '}'))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox2.Text);

      //test queue
         KarthicQueueByLinkedList<int> myqueue = new KarthicQueueByLinkedList<int>();
     
        foreach (int i in input)
        {
           myqueue.EnQueue(i);
           
        }

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
          sb.Append(myqueue.DeQueue().ToString()).Append(",");
        }

        this.textBox1.Text = sb.ToString();

    }

    private void button3_Click(object sender, EventArgs e)
    {

      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);

      // arrange
      var queue = new KarthicPriorityQueue();


      foreach (int i in input)
      {
        queue.Enqueue(i);

      }

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < input.Length; i++)
      {
        sb.Append(queue.DeQueue().ToString()).Append(",");
      }

      this.textBox3.Text = sb.ToString();

    }

    private void button4_Click(object sender, EventArgs e)
    {

      string[] input = this.textBox8.Text.Split(',');

      var queue = new PriorityQueue<DummyStringComparer2>();

      foreach (string s in input)
      {
        queue.Enqueue(new DummyStringComparer2(s));
      }

     StringBuilder sb = new StringBuilder();

      for (int i = 0; i < input.Length; i++)
      {
         sb.Append(queue.Dequeue().StringOfX).Append(",");
      }

      this.textBox7.Text = sb.ToString();

    }

    private void button5_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);

      // arrange

     // Heap<int> minheap = new Heap<int>(new List<int>(), 0, new KarthicMinHeapComparer());

      MinHeap<int> minheap = new MinHeap<int>(new KarthicMinHeapComparer2());


      foreach (int i in input)
      {
         minheap.Insert(i);

      }

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < input.Length; i++)
      {
        sb.Append(minheap.PopRoot().ToString()).Append(",");
      }

      this.textBox3.Text = sb.ToString();
    }

    private void button6_Click(object sender, EventArgs e)
    {

      string[] input = this.textBox8.Text.Split(',');

      MinHeap<KarthicStringComparer> minheap = new MinHeap<KarthicStringComparer>(new KarthicStringComparer());


      foreach (string s in input)
      {
        minheap.Insert(new KarthicStringComparer(s));
      }

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < input.Length; i++)
      {
        sb.Append(minheap.PopRoot().StringOfX).Append(",");
      }

      this.textBox7.Text = sb.ToString();

    }


   
  }
}
