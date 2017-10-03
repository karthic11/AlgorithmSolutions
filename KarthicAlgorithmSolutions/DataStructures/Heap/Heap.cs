using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{

  //http://www.sinbadsoft.com/blog/binary-heap-heap-sort-and-priority-queue/
  //http://en.wikipedia.org/wiki/Binary_heap
    //Heap property
//All nodes are either greater than or equal to or less than or equal to each of its children, according to a comparison predicate defined for the heap.


  public class Heap<T>
  {
    private readonly IList<T> _list;
    private readonly IComparer<T> _comparer;
    public  IComparer<T> _wordcomparer;

    public Heap(IList<T> list, int count, IComparer<T> comparer)
    {
      _comparer = comparer;
      _list = list;
      //Important: we use count to keep track of removed value..don't use list.count in the code
      Count = count;
      Heapify();
    }

    public int Count { get; private set; }

    public T PopRoot()
    {
      if (Count == 0) throw new InvalidOperationException("Empty heap.");
      //Get the value from the first element
      var root = _list[0];

      //swap the removed element (root) with the last element in the tree
      SwapCells(0, Count - 1);

      //note: we are not removing the item..we just decrement the count variable
      Count--;

      HeapDown(0);
      return root;
    }

    public T PeekRoot()
    {
      if (Count == 0) throw new InvalidOperationException("Empty heap.");
      return _list[0];
    }

    //Insert element into the heap
    //Logic
    //Add the element to the bottom level of the heap.
    //Compare the added element with its parent; if they are in the correct order, stop.
    //If not, swap the element with its parent and return to the previous step.
    public void Insert(T e)
    {
      //if the list has space

      //here the check is to see if there are any deleted items. if we have count lesser than list.count then overwrite last element
      //update 5/28/2015 
      // count is the actual no of elements that will be considered (without taking deleted/popped elements)
      //list.count is the count irrespective of elements popped or removed

      if (Count >= _list.Count)

        _list.Add(e);
      else
         //overwrite last element
         _list[Count] = e;

      Count++;

      HeapUp(Count - 1);
    }

    public int InsertAndGiveIndex(T e)
    {
      //if the list has space
      if (Count >= _list.Count)

        _list.Add(e);
      else
        _list[Count] = e;

      Count++;

      return CustomHeapUp(Count - 1);

     
    }

    private void Heapify()
    {
      for (int i = Parent(Count - 1); i >= 0; i--)
      {
        HeapDown(i);
      }
    }

    //Heapup or bubble up is done to make sure the 
    private void HeapUp(int i)
    {
      
      T elt = _list[i];
      while (true)
      {
        int parent = Parent(i);
        //if the parent value is lesser than o or
        //if the comparer value is greater than 0 then break...
        //eg in case of min-heap the parent should be smaller
        if (parent < 0 || _comparer.Compare(_list[parent], elt) > 0)
        {
          break;
        }
        SwapCells(i, parent);
        //this loop will continue untill the parent heap condition is satified
        //ie in min-heap the parent should be the smallest
        i = parent;
      }
    }


    private int CustomHeapUp(int i)
    {
      T elt = _list[i];
      while (true)
      {
        int parent = Parent(i);
        //if the parent value is lesser than o or
        //if the comparer value is greater than 0 then break...
        //eg in case of min-heap the parent should be smaller
        if (parent < 0 || _comparer.Compare(_list[parent], elt) > 0)
        {
          break;
        }
        SwapCells(i, parent);
        //this loop will continue untill the parent heap condition is satified
        //ie in min-heap the parent should be the smallest
        i = parent;
      }
      return i;
    }


    //Logic: This compares the new root with the children and make sure it does not violate the heap property

    private void HeapDown(int i)
    {
      while (true)
      {
        //get the leftchild
        int lchild = LeftChild(i);
        //if there is no left child..only one item exists
        if (lchild < 0) break;

        //get the index of right child
        int rchild = RightChild(i);
        

        //child should one of the two children depending on the min or max heap
        //in case of min heap the child will be the minimum of two childrens

        //if right is lesser than zero eg. only two elements are present take left as child
        //child will be the index of left or right child
        int child = rchild < 0 ? lchild  : (_comparer.Compare(_list[lchild], _list[rchild]) > 0 ? lchild : rchild);

        if (_comparer.Compare(_list[child], _list[i]) < 0)
        {
          //if the parent is less
          break;
        }
        SwapCells(i, child);
        i = child;
      }
    }


    public int CustomHeapDown(int i)
    {
      while (true)
      {
        //get the leftchild
        int lchild = LeftChild(i);
        //if there is no left child..only one item exists
        if (lchild < 0) break;

        //get the index of right child
        int rchild = RightChild(i);


        //child should one of the two children depending on the min or max heap
        //in case of min heap the child will be the minimum of two childrens

        //if right is lesser than zero eg. only two elements are present take left as child
        //child will be the index of left or right child
        int child = rchild < 0 ? lchild : (_comparer.Compare(_list[lchild], _list[rchild]) > 0 ? lchild : rchild);

        if (_comparer.Compare(_list[child], _list[i]) < 0)
        {
          //if the parent is less
          break;
        }
        SwapCells(i, child);
        i = child;
      }

      return i;
    }




    //get the index of the parent for the given index
    private int Parent(int i) 
    {
      return i <= 0 ? -1 : SafeIndex((i - 1) / 2);
    
    }

    //get the index of the right child of the given index
    private int RightChild(int i) 
    { 
      return SafeIndex(2 * i + 2); 
    }

    //gets the index of the left child of the given index
    private int LeftChild(int i) {
      return SafeIndex(2 * i + 1); }

    //Safeindex method get the index safe..ie if negative returns -1 if not actual value
    //This is very important step make sure we check i <  count.. bcoz when item is popped out we decreasae the value of count
    //so it means the popped element are not considered for future comparison
    //update: 5/28/2015
    //this is important when we pop out an element we don't actullay remove the element we only decrement the value of count
    //so when we get the index more than the current count we return -1..means popped element are not considered for future comparison

    private int SafeIndex(int i) 
    { 
      return i < Count ? i : -1; 
    }

    //To swap the value of the array for the given two array index
    private void SwapCells(int i, int j)
    {
      T temp = _list[i];
      _list[i] = _list[j];
      _list[j] = temp;
    }

    public T GetItemByIndex(int i)
    {
      return _list[SafeIndex(i)];
    }


    public T GetItemByValue(T item)
    {

      foreach (T current in _list)
      {
        if (_wordcomparer.Compare(current, item) == 0)
        {
          return current;

        }
      }

      return default(T);
    }
  }



  public class MinHeap<T>
  {
    private readonly Heap<T> heap;


    public MinHeap(IComparer<T> comparer)
    {
      heap = new Heap<T>(new List<T>(), 0, comparer);
     
    }

    public void SetWorkComparer(IComparer<T> wordcomparer)
    {
      heap._wordcomparer = wordcomparer;
    }
    public void Insert(T item)
    {
      heap.Insert(item);
    }


    public int CustomInsert(T item)
    {
      return heap.InsertAndGiveIndex(item);
    }

    public T PopRoot()
    {
      return heap.PopRoot();
    }

    public T Peek()
    {
      return heap.PeekRoot();
    }

    public int Size()
    {
      return heap.Count;
    }

    public T GetItemByIndex(int i)
    {
      return heap.GetItemByIndex(i);
      
    }

    //heapy down by index and return the new index
    public int CustomHeapDownByIndex(int i)
    {
      return heap.CustomHeapDown(i);
    }


    public T GetItemByValue(T item)
    {
      return heap.GetItemByValue(item);
        
    }

  }





  public class MaxHeap<T>
  {
    private readonly Heap<T> heap;

    public MaxHeap(IComparer<T> comparer)
    {
       heap = new Heap<T>(new List<T>(), 0, comparer);
    }

    public void Insert(T item)
    {
      heap.Insert(item);
    }

    public T PopRoot()
    {
      return heap.PopRoot();
    }

    public T Peek()
    {
      return heap.PeekRoot();
    }

    public int Size()
    {
      return heap.Count;
    }
  }



  // A Min Heap node
 public struct MinHeapNode
  {
    public TrieNode root; // indicates the leaf node of TRIE
    public int frequency; //  number of occurrences
    public string word;// the actual word stored

    public MinHeapNode(string word, int frequency, TrieNode node)
    {
      this.root = node;
      this.word = word;
      this.frequency = frequency;
    }
  };

 

  public class KarthicMinHeapNodeComparer : IComparer<MinHeapNode>
  {

    int IComparer<MinHeapNode>.Compare(MinHeapNode leftvalue, MinHeapNode rightvalue)
    {
      return rightvalue.frequency.CompareTo(leftvalue.frequency);
    }
  
}

  public class KarthicMinHeapWordComparer : IComparer<MinHeapNode>
  {

    int IComparer<MinHeapNode>.Compare(MinHeapNode leftvalue, MinHeapNode rightvalue)
    {
      return rightvalue.word.Equals(rightvalue.word) ? 0 : -1;
    }

  
  }

 
  public class KarthicMinHeapComparer2 : IComparer<int>
  {

    int IComparer<int>.Compare(int leftvalue, int rightvalue)
    {
      return rightvalue.CompareTo(leftvalue);
    }
  }


  public class KarthicMaxHeapComparer : IComparer<int>
  {
    int IComparer<int>.Compare(int leftvalue, int rightvalue)
    {
      return leftvalue.CompareTo(rightvalue);
    }
  }







  public class HeapSort<T>
  {
    public static void Sort(IList<T> list, IComparer<T> comparer)
    {
      var heap = new Heap<T>(list, list.Count, comparer);
      while (heap.Count > 0)
      {
        heap.PopRoot();
      }
    }
  }
}
