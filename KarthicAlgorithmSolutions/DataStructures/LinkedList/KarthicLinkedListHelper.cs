using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.LinkedList
{
 public static class KarthicLinkedListHelper
  {

   public static Node MergeSort(Node head)
    {

      if (head == null || head.Next == null)
      {
        return head;
      }
      Node middle = getMiddle(head);      //get the middle of the list
      Node sHalf = middle.Next;  //split the list into two halfs
      middle.Next = null;

      return merge(MergeSort(head), MergeSort(sHalf));  //recurse on that


    }


   //This function take two list and return the sorted merged list
   public static Node merge(Node a, Node b)
    {
      //dummy is like a helper node..It is an empty head..and it helps to form a sorted item to its tail
      Node dummyHead = new Node();
      Node curr;

      //curr will be a runner..it's pointer will be running between the node.it's start with the head node
      curr = dummyHead;

      while (a != null && b != null)
      {
        if (a.Data <= b.Data)
        {
          curr.Next = a;
          a = a.Next;
        }
        else
        {
          curr.Next = b;
          b = b.Next;
        }
        curr = curr.Next;
      }

      //here eg : Node a (3->5->6) and Node b (1-4)
      //then here the current will be 1->3-->4   and Node a (5-->6) b (null)
      //
      curr.Next = (a == null) ? b : a;

      return dummyHead.Next;
    }



   public static Node getMiddle(Node head)
    {
      if (head == null)
      {
        return head;
      }
      Node slow, fast;
      slow = fast = head;
      while (fast.Next != null && fast.Next.Next != null)
      {
        slow = slow.Next;
        fast = fast.Next.Next;
      }
      return slow;
    }
  }
}
