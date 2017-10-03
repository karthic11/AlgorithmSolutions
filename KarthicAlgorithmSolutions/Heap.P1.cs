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
  public partial class Heap : Form
  {
    public Heap()
    {
      InitializeComponent();
    }

    private void button10_Click(object sender, EventArgs e)
    {
        MaxHeap<int> maxheap = new MaxHeap<int>(new KarthicMaxHeapComparer());
        //array 2,4,1,6,3
        //max heap 6,4,3,2,1

        maxheap.Insert(2);
        maxheap.Insert(4);
        maxheap.Insert(1);
        maxheap.Insert(6);
        maxheap.Insert(3);

        maxheap.PopRoot();
        maxheap.PopRoot();

        maxheap.Insert(5);

        StringBuilder sb = new StringBuilder();
        while (maxheap.Size() != 0)
        {
            sb.Append(maxheap.PopRoot()).Append(",");

        }

 

        string result = sb.ToString();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);


      //Assumption:
      //1) Store the values lesser than median in max heap and keep the root with the max value
      //2) Store the values greater than the median in min heap
      //3) Maxheap >= Minheap

 //* Use a max heap and a min heap to maintain the stream, where
 //* maxheap.size() == minheap.size() or
 //* maxheap.size() - 1 == minheap.size()
 //* always holds.

// Similar to balancing BST in Method 2 above, we can use a max heap on left side to represent elements that are less than effective median, and a min heap on right side to represent elements that are greater than effective median.

//After processing an incoming element, the number of elements in heaps differ utmost by 1 element. When both heaps contain same number of elements, we pick average of heaps root data as effective median. When the heaps are not balanced, we select effective median from the root of heap containing more elements.

      MinHeap<int> minheap = new MinHeap<int>(new KarthicMinHeapComparer2());
      MaxHeap<int> maxheap = new MaxHeap<int>(new KarthicMaxHeapComparer());



      foreach (int i in input)
      {
        InsertRandomElement(i, maxheap, minheap);

      }


      //Median Calcualtion
      //If there are odd total numbers, maxheap will be greater
      //if there are even total number then minheap == maxheap

      this.textBox2.Text = CalculateMedian(maxheap, minheap).ToString();

    }







    public void InsertRandomElement(int randomvalue, MaxHeap<int> maxheap, MinHeap<int> minheap)
    {
      if (maxheap.Size() == minheap.Size())
      {
        if (minheap.Size() > 0 && randomvalue > minheap.Peek())
        {
          //if the coming random value is greater than the minheap peek
          maxheap.Insert(minheap.PopRoot());
          minheap.Insert(randomvalue);
        }
        else
        {
           maxheap.Insert(randomvalue);
        }
      }
      else
      {
        //if the Size() is not equal means the max heap will be bigger
        if (randomvalue < maxheap.Peek())
        {
          minheap.Insert(maxheap.PopRoot());
          maxheap.Insert(randomvalue);

        }
        else
        {
          minheap.Insert(randomvalue);
        }
        
      }

    }


    public double  CalculateMedian(MaxHeap<int> maxheap, MinHeap<int> minheap)
    {

      //check for empty..if maxheap is empty then minheap will also be empty
      if (maxheap.Size() == 0)
      {
        return 0;
      }
      if (maxheap.Size() == minheap.Size())
      {
        //mean there are even number of item..median = (sum of middle two items)/2
        return Convert.ToDouble((maxheap.Peek() + minheap.Peek()) / 2);
      }
      else
      {
        return maxheap.Peek();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void label15_Click(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }

    private void label8_Click(object sender, EventArgs e)
    {

    }
   
  }
}
