using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    //Last In First Out
    public class KarthicStack
    {
        Node last;


        Stack<int> minstack = new Stack<int>();

        public void Push(int item)
        {
            Node newitem = new Node(item);
            newitem.Next = last;
            last = newitem;

            //this is for min
            //int minvalue = Math.Min(item, GetMin());
            if ((minstack.Count == 0) ||(item < GetMin()))
            {
                minstack.Push(item);
            }
        }

        public int Pop()
        {
            

            Node temp = last;
            last = last.Next;

            if (temp.Data == minstack.Peek())
            {
                minstack.Pop();
            }
            return temp.Data;
        }


        public int GetMin()
        {
            if (minstack.Count > 0)
            {
                return minstack.Peek();
            }
            else
            {
                throw new Exception("stack is empty");
            }
        }

        public bool IsEmpty()
        {
            return (last == null);
        }

        public int Peek()
        {
            if (last != null)
            {
                return last.Data;
            }
            else
            {
                throw new Exception("Stack is empty");
            }
        }

        
    }




    public class KarthicArrayStack
    {

     
        int[] stackarray;
       
        int arraylastpointer = 0;

        public KarthicArrayStack(int size)
        {
            stackarray = new int[size];
        }

        public void Push(int item)
        {
            if (arraylastpointer + 1 >= stackarray.Length)
            {
                throw new Exception("array out of space");

            }
            else
            {
                stackarray[arraylastpointer] = item;
                arraylastpointer++;
            }
        }

        public int Pop()
        {
            if (arraylastpointer >= 1)
            {
                int temp = stackarray[arraylastpointer -1];
                arraylastpointer--;
                return temp;
            }
            else
            {
                throw new Exception("array is empty");
            }
        }


        public int Peek()
        {
            return stackarray[arraylastpointer];
        }

    }

      //Last In First Out
    public class KarthicStackWithMin
    {
        NodeWithMin last;

        int minvalue;

        public void Push(int item)
        {
            int minvalue = GetMin();

            if (item < minvalue)
            {
                minvalue = item;
            }
            NodeWithMin newitem = new NodeWithMin(item, minvalue);
            newitem.Next = last;
            last = newitem;
        }

        public int Pop()
        {
            NodeWithMin temp = last;
            last = last.Next;
            return temp.Data;
        }


        public int GetMin()
        {
            if (last != null)
            {
                return last.Min;

            }
            else
            {
                return Int16.MaxValue;
            }

        }
        
    }

    public class NodeWithMin
    {

       
        public NodeWithMin()
        {
        }

         public NodeWithMin(int value, int min)
        {
            this.Data = value;
            this.Min = min;
        }
        public int Data { get; set; }
        public int Min { get; set; }
        //public Node  Previous { get; set; }
        public NodeWithMin Next { get; set; }

    }


    public class SetOfStacks
    {

        readonly int capacity = 3;
        List<Stack<int>> stackslist = new List<Stack<int>>();
        public void Push(int value)
        {
            Stack<int> laststack = GetLastStack();
            if (laststack != null && laststack.Count != capacity)
            {
                laststack.Push(value);
            }
            else
            {
                Stack<int> newstack = new Stack<int>(capacity);
                newstack.Push(value);
                stackslist.Add(newstack);
            }

        }       

        public int Pop()
        {
            Stack<int> laststack = GetLastStack();
            int value = laststack.Pop();

            if (laststack.Count == 0)
            {
                //we should remove the poped oot stack so that last will be the next stack
                stackslist.Remove(laststack);
            }

            return value;

        }

        private Stack<int> GetLastStack()
        {
            if (stackslist.Count > 0)
            {
                return stackslist[stackslist.Count - 1];
            }
            else
            {
                return null;
            }
        }

        //where index is the index of the stack
        //If there are 3 there stacks PopAt(0) means popup the element in first stack
        //Solution 1:  Just pop up the element at the specified stack without shifting the elements from previous stack to the popup stack...
        //In this method, we don't maintain the capacity of the poped stack..
        //Solution 2: we maintain the capacity of stack. If the first stack element is poped out, we shif the last element from the previous stack to the poped out..
        //This solution is complex we should argue with the interviewer
        private int PopAt(int index)
        {

            return leftshift(index, true);
             
        }

        public int GetBottom(int index)
        {
            //This logic has to be in the stack class ..either the node or array
            //we need to maintain the bottom node and count of nodes (doubly linked list)
            //int lastvalue = bottom.Data
            //bottom = bottom.previous;
            //bottom.next = null;
            //size--;
            //retun lastvalue
            return 1;
        }

        private int leftshift(int index, bool removetop)
        {

            Stack<int> stackinstance = stackslist[index];
            int removeditem;
            if (stackinstance.Count > 0)
            {
                if (removetop)
                {
                    removeditem = stackinstance.Pop();
                }
                else
                {
                    //get the bottom item of the stack 
                    removeditem = GetBottom(index);
                }
            }
            else
            {
                //error
                throw new Exception("mentioned index is empty");
            }

            //we removed the item from the mentioned stack but still we haven't shifter from left

            if (stackslist.Count > index + 1)
            {
                int lastitemtobeshifted = leftshift(index + 1, false);
                stackinstance.Push(lastitemtobeshifted);
            }


            return removeditem;

        }

    }



   
}
