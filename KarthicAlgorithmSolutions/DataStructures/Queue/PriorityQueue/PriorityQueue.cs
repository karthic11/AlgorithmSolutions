using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{


    //update: 6/25/2015
    public class KarthicPriorityQueue
    {
        Node head = null;
        Node tail = null;

        public KarthicPriorityQueue()
        {
        }

        public void Enqueue(int item)
        {
            //Generally item will be added in this fashion for queue   (head) 1...2...3...4 (tail)
            //For priority queue there is some priority for items..
            //The priority may be anything..in this case priority is the value that lesser should be ejected first

            Node newnode = new Node(item);

            if (head == null)
            {
                head = newnode;
                tail = newnode;
            }
            else
            {
                //make sure to maintain the list in ascending order
                //the new item should be always greater than the exsiting items in the list
                if (newnode.Data >= tail.Data)
                {
                    tail.Next = newnode;
                    tail = newnode;
                }
                else
                {
                    //if the new item is lesser then it has to go somewhere from head to tail
                    Node runner = head;
                    if (newnode.Data < runner.Data)
                    {
                        newnode.Next = runner;
                        head = newnode;
                    }
                    else
                    {
                        while (runner.Next != null && runner.Next.Data < newnode.Data)
                        {
                            runner = runner.Next;
                        }

                        //when code come here mean runner.Next.Data is greater than newnode
                        Node temp = runner.Next;
                        runner.Next = newnode;
                        newnode.Next = temp;
                    }

                }


            }
            
        }

        public int DeQueue()
        {
            int value = head.Data;

            head = head.Next;

            return value;
        }

        public int Peek()
        {
            if (head == null)
            {
               //throw exce
            }
            return head.Data;
        }

    }

  public interface IPriorityQueue<T> : IEnumerable<T>  where T : IComparable<T>
  {
    bool IsEmpty { get; }
    void Enqueue(T item);
    T Dequeue();
    T Peek();
  }

  //Icomparable<T> will help us to compare with any object(int,string or any)

  public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
  {
    private readonly LinkedList<T> _items;

    public PriorityQueue()
    {
      _items = new LinkedList<T>();
    }

    #region IPriorityQueue<T> Members


    //Add items to the queue
    public void Enqueue(T item)
    {
      if (IsEmpty)
      {
        _items.AddFirst(item);
        return;
      }

      //logic to order list by priority
      //This logic makes sure to have the lowest value in the top and that gets ejected first


      //take the head/first of the linkedlist
      LinkedListNode<T> existingItem = _items.First;

      //left.compareTo(right)  Result = 0 (both the values are same), 1 (left is greater), -1 (right is greater)
      //if the value is equal we still insert it to the last..
      while (existingItem != null && existingItem.Value.CompareTo(item) < 0)
      {
        //if the new item is greater than first item
        existingItem = existingItem.Next;
      }

      if (existingItem == null)
        //ready to add to the tail so that first item will be always lesser
        _items.AddLast(item);
      else
      {
        //if the new item is lesser than exisiting..add it to the top
        _items.AddBefore(existingItem, item);
      }
    }

    public T Dequeue()
    {
      T value = _items.First.Value;
      _items.RemoveFirst();

      return value;
    }

    public T Peek()
    {
      return _items.First.Value;
    }

    public bool IsEmpty
    {
      get { return _items.Count == 0; }
    }

    public IEnumerator<T> GetEnumerator()
    {
      return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion
  }

  //internal
  public class DummyStringComparer2 : IComparable<DummyStringComparer2>
  {
    public string StringOfX { get; set; }

    public DummyStringComparer2(string input)
    {
      this.StringOfX = input;
    }

    public int CompareTo(DummyStringComparer2 other)
    {
      // Return values:
      // <0: This instance smaller than obj.
      //  0: This instance occurs in the same position as obj.
      // >0: This instance larger than obj.

      return this.StringOfX.Length.CompareTo(other.StringOfX.Length);
    }



    public int Compare(DummyStringComparer2 x, DummyStringComparer2 y)
    {
      return x.StringOfX.Length.CompareTo(y.StringOfX.Length);
    }


  }


  public class KarthicComparer : IComparable<KarthicComparer>
  {
    public int Priority;
    public string Name;


    public int CompareTo(KarthicComparer other)
    {
      return this.Priority.CompareTo(other.Priority);
    }
  }


  public class KarthicStringComparer : IComparer<KarthicStringComparer>
  {
    public string StringOfX { get; set; }

    public KarthicStringComparer()
    {
    }

    public KarthicStringComparer(string input)
    {
      this.StringOfX = input;
    }

    int IComparer<KarthicStringComparer>.Compare(KarthicStringComparer x, KarthicStringComparer y)
    {
      return y.StringOfX.Length.CompareTo(x.StringOfX.Length);
    }
  }
}

