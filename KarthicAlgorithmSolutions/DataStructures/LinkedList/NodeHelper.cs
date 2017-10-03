using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{


    public class NodeAdditionResult
    {
        public Node resultnode = null;
        public int carry = 0;
    }


    public static class NodeHelper
    {

        public static int GetLength(KarthicLinkedList list)
        {
            if(list == null)
            {
                return 0;
            }

            Node runner = list.headnode;

            int length = 0;

            while (runner != null)
            {
                length++;
                runner = runner.Next;
            }

            return length;
             

        }

        public static Node AddPadding(Node head, int paddingsize)
        {
            Node temp = head;

            for (int i = 0; i < paddingsize; i++)
            {
                Node newnode = new Node(0);
                newnode.Next = temp;
                temp.Previous = newnode;
                temp = newnode;

            }

            return temp;
        }

        public static Node AddNodeToFirst(Node node, int value)
        {

            Node newnode = new Node(value);
            if (node != null)
            {
                newnode.Next = node;
                node.Previous = newnode;
               
            }
            return newnode;
        }

        public static KarthicLinkedList GetLinkedListByString(string input)
        {
            KarthicLinkedList numberlist = new KarthicLinkedList();
            foreach (string s in input.Split(','))
            {
                numberlist.AddNode(new Node(Convert.ToInt32(s)));
            }

            return numberlist;
        }


        public static string GetStringByNode(Node head)
        {
            StringBuilder sb = new StringBuilder();
            Node runner = new Node();
            runner = head;
            while (runner != null)
            {
                sb.Append(runner.Data.ToString()).Append(",");
                runner = runner.Next;

            }

            return sb.ToString();
        }

        public static string GetStringByRandomNode(RandomNode head)
        {
          StringBuilder sb = new StringBuilder();
          RandomNode runner = new RandomNode();
          runner = head;
          while (runner != null)
          {
            sb.Append(runner.Data.ToString()).Append(",");
            runner = runner.Next;

          }

          return sb.ToString();
        }
    }

}
