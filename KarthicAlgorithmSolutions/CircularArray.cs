using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  public class CircularQueue
  {
    // Variable Declaration  
    private int _noofElements = 0;
    private int _queueStart = 0;
    private int[] _queueArray;
    private object _lock;
    public int Length
    {
      get
      {
        return _noofElements;
      }
    }
    //Function to get Next Index in circular array  
    public int NextIndex()
    {
      int extendedSize = _queueStart + _noofElements;
      // Restart new index from start of array if end of array is reached  
      return extendedSize >= _queueArray.Length ? extendedSize - _queueArray.Length : extendedSize;
    }
    //Function to Initialize Queue Array and Reset Variables  
    public void Initialize(int Size)
    {
      // Initialize Circular Queue Array        
      lock (_lock)
      {
        _queueArray = new int[Size];
        _queueStart = 0;
        _noofElements = 0;
      }
    }
    public void Enqueue(int NewMember)
    {
      if (_queueArray.Length == _noofElements)
        throw new Exception("Queue is Full");
      // Set member at new circular index  
      lock (_lock)
      {
        _queueArray[NextIndex()] = NewMember;
        _noofElements++; // Increment element counter  
      }
    }
    public int Dequeue()
    {
      if (_noofElements == 0)
        throw new Exception("Queue is Empty");
      int temp = _queueArray[_queueStart]; // Store position value in temp variable  
      lock (_lock)
      {
        _queueArray[_queueStart] = 0; // Reset position value  
        _queueStart = (_queueStart == _queueArray.Length - 1) ? 0 : _queueStart + 1; // Get new start position  
        _noofElements--; // Decrement element counter  
      }
      return temp;
    }
  }

}
