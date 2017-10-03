using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    
    public class Node
    {
        public Node()
        {
        }

        public Node(int value)
        {
            this.Data = value;
        }
        public int Data { get; set; }
        public Node  Previous { get; set; }
        public Node Next { get; set; }

    }


    public class RandomNode
    {
      public RandomNode()
      {
      }

      public RandomNode(int value)
      {
        this.Data = value;
        this.Next = null;
        this.Random = null;
      }
      public int Data { get; set; }
      public RandomNode Random { get; set; }
      public RandomNode Next { get; set; }

    }


    public class KarthicLinkedList : IEnumerable<Node>
    {
        public KarthicLinkedList()
        {
        }
        public Node headnode { get; set; }
        public Node lastnode { get; set; }

        public Node runner { get; set; }

        public void AddNode(Node node)
        {
     
            if (headnode != null)
            {
                runner = headnode;
                while (runner.Next != null)
                {

                    runner = runner.Next;
                }
                //at the end of this while loop runner will be the last node
                runner.Next = node;
                lastnode = node;
            }
            else
            {
                headnode = node;
            }


        }


        //keythings:   yield and return type Ienumerator
        public IEnumerator<Node> GetEnumerator()
        {
           
            //IEnumerator concept...This function saves the state of the function
            //keywork yield return

           //test
            //Node node1 = new Node(1);
            //Node node2 = new Node(2);
            //Node node3 = new Node(3);
            //Node node4 = new Node(4);

            //yield return node1;
            //yield return node2;
            //yield return node3;
            //yield return node4;

            //yield first to last
            runner = headnode;

            while (runner != null)
            {
                yield return runner;

                runner = runner.Next;
            }
             

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
