using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Puzzles.DataStructures.LinkedList;

namespace Puzzles
{
  public partial class LinkedListPage3 : Form
  {
    public LinkedListPage3()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
      KarthicLinkedList linkedlist1 = NodeHelper.GetLinkedListByString(this.textBox1.Text);
      KarthicLinkedList linkedlist2 = NodeHelper.GetLinkedListByString(this.textBox3.Text);

      linkedlist1.headnode =  KarthicLinkedListHelper.MergeSort(linkedlist1.headnode);
      linkedlist2.headnode = KarthicLinkedListHelper.MergeSort(linkedlist2.headnode);

      Node result = FindCommonElements(linkedlist1.headnode, linkedlist2.headnode);
      this.textBox2.Text = NodeHelper.GetStringByNode(result);

       //linkedlist1.headnode = KarthicLinkedListHelper.
      

    }

    public Node FindCommonElements(Node list1, Node list2)
    {

      Node resulthead = null;
      Node resulttail = null;

      while (list1 != null && list2 != null)
      {
        if (list1.Data == list2.Data)
        {
          if (resulthead == null)
          {
              //Don't play with the actulal object..make a copy of that
              //resulthead = list1;
              Node newnode = new Node(list2.Data);
              resulthead = newnode;
           
             resulthead.Next = null;
             resulttail = resulthead;
          }
          else
          {
            Node newnode = new Node(list2.Data);
            resulttail.Next = newnode;
            resulttail = resulttail.Next;
            resulttail.Next = null;
          }

          //after storing the data increment both
          list1 = list1.Next;
          list2 = list2.Next;
        }
        else if (list1.Data > list2.Data)
        {
          //since the two list are sorted..there won't be any possibility of list2.data in list1 so increment and check for next
          list2 = list2.Next;
        }
        else
        {
          list1 = list1.Next;
        }
      }

     
      return resulthead;

    }


 
    private void LinkedListPage3_Load(object sender, EventArgs e)
    {

    }

    private void button4_Click(object sender, EventArgs e)
    {
      KarthicLinkedList list1 = NodeHelper.GetLinkedListByString(this.textBox6.Text);
      KarthicLinkedList list2 = NodeHelper.GetLinkedListByString(this.textBox4.Text);
      Node result = FindIntersectionofTwoList(list1, list2);

      this.textBox5.Text = result.Data.ToString();

     

    }

    public Node FindIntersectionofTwoList(KarthicLinkedList list1, KarthicLinkedList list2)
    {

      //logic
      //since both the list are intersecting at a node. The length after the intersecting point will be the same
      //so calculate the height and then traverse the longer list to the difference
      //and the traverse both to find the common point

      int list1length = NodeHelper.GetLength(list1);
      int list2length = NodeHelper.GetLength(list2);

      KarthicLinkedList longer = null;
      KarthicLinkedList shorter = null;

      if (list1length > list2length)
      {
        longer = list1;
        shorter = list2;
      }
      else
      {
        longer = list2;
        shorter = list1;
      }

      int difference = Math.Abs(list1length - list2length);

      //Traverse through the longer list for the difference

      Node node1 = longer.headnode;
      Node node2 = shorter.headnode;

      while (difference != 0)
      {
        node1 = node1.Next;
        difference--;

      }

      //here both will be at a same height now traverse to find the intersection point

      while (node1 != null && node2 != null & node1.Data != node2.Data)
      {
        node1 = node1.Next;
        node2 = node2.Next;
      }

      if (node1 == null || node2 == null)
      {
        return null;
      }
      else
      {
        //both will be at the same node
        return node1;
      }


      
      

    }

    private void button7_Click(object sender, EventArgs e)
    {

    }

    private void button6_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void button5_Click(object sender, EventArgs e)
    {

    }
  }
}
