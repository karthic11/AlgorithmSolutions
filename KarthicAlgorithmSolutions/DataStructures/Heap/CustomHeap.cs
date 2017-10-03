using System;
using System.Collections.Generic;
using System.Linq;


namespace Puzzles
{
   public class CustomHeap
  {
    private readonly IList<MinHeapNode> _list;
    private readonly IComparer<MinHeapNode> _comparer;
    public IComparer<MinHeapNode> _wordcomparer;

    public CustomHeap(IList<MinHeapNode> list, int count, IComparer<MinHeapNode> comparer)
    {
      _comparer = comparer;
      _list = list;
      //Important: we use count to keep track of removed value..don't use list.count in the code
      Count = count;
      Heapify();
    }

    public int Count { get; private set; }

    public MinHeapNode PopRoot()
    {
      if (Count == 0) throw new InvalidOperationException("Empty heap.");
      //Get the value from the first element
      var root = _list[0];

      //swap the removed element (root) with the last element in the tree
      SwapCells(0, Count - 1);

      Count--;

      HeapDown(0);
      return root;
    }

    public MinHeapNode PeekRoot()
    {
      if (Count == 0) throw new InvalidOperationException("Empty heap.");
      return _list[0];
    }

    //Insert element into the heap
    //Logic
    //Add the element to the bottom level of the heap.
    //Compare the added element with its parent; if they are in the correct order, stop.
    //If not, swap the element with its parent and return to the previous step.
    public void Insert(MinHeapNode e)
    {
      //if the list has space
      if (Count >= _list.Count)

        _list.Add(e);
      else
        _list[Count] = e;

      Count++;

      HeapUp(Count - 1);
    }

    public int InsertAndGiveIndex(MinHeapNode e)
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

      MinHeapNode elt = _list[i];
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
      MinHeapNode elt = _list[i];
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


    //Logic: MinHeapNodehis compares the new root with the children and make sure it does not violate the heap property

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
        int child = rchild < 0 ? lchild : (_comparer.Compare(_list[lchild], _list[rchild]) > 0 ? lchild : rchild);

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
    private int LeftChild(int i)
    {
      return SafeIndex(2 * i + 1);
    }

    //Safeindex method get the index safe..ie if negative returns -1 if not actual value
    //MinHeapNodehis is very important step make sure we check i <  count.. bcoz when item is popped out we decreasae the value of count
    //so it means the popped element are not considered for future comparison

    private int SafeIndex(int i)
    {
      return i < Count ? i : -1;
    }

    //MinHeapNodeo swap the value of the array for the given two array index
    private void SwapCells(int i, int j)
    {
      MinHeapNode temp = _list[i];

      _list[i] = _list[j];
      //Also update the index change
      if (_list[i].root != null)
      {
          _list[i].root._heapArrayIndex = j;
      }

      _list[j] = temp;
      if (_list[j].root != null)
      {
          _list[j].root._heapArrayIndex = temp.root._heapArrayIndex;
      }
    }

    public MinHeapNode GetItemByIndex(int i)
    {
      return _list[SafeIndex(i)];
    }


    public MinHeapNode GetItemByValue(MinHeapNode item)
    {

      foreach (MinHeapNode current in _list)
      {
        if (_wordcomparer.Compare(current, item) == 0)
        {
          return current;

        }
      }

      return default(MinHeapNode);
    }

    public MinHeapNode GetItemByWord(string word)
    {

      foreach (MinHeapNode current in _list)
      {
        if (current.word.Equals(word))
        {
          return current;

        }
      }

      return default(MinHeapNode);
    }
  }


   public class CustomMinHeap
   {
     private readonly CustomHeap heap;


     public CustomMinHeap(IComparer<MinHeapNode> comparer)
     {
     
         heap = new CustomHeap(new List<MinHeapNode>(), 0, comparer);

     }


     public void Insert(MinHeapNode item)
     {
       heap.Insert(item);
     }


     public int CustomInsert(MinHeapNode item)
     {
       return heap.InsertAndGiveIndex(item);
     }

     public MinHeapNode PopRoot()
     {
       return heap.PopRoot();
     }

     public MinHeapNode Peek()
     {
       return heap.PeekRoot();
     }

     public int Size()
     {
       return heap.Count;
     }

     public MinHeapNode GetItemByIndex(int i)
     {
       return heap.GetItemByIndex(i);

     }

     //heapy down by index and return the new index
     public int CustomHeapDownByIndex(int i)
     {
       return heap.CustomHeapDown(i);
     }


     public MinHeapNode GetItemByValue(MinHeapNode item)
     {
       return heap.GetItemByValue(item);

     }

     public MinHeapNode GetItemByWord(string word)
     {
       return heap.GetItemByWord(word);
     }

   }


}
