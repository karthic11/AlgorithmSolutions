using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  public class KarthicCircularQueue<T>
  {

    // head  of the queue is pointing at the latest value
    public int _head { get; set; }
    //tail will always point to the oldest value in the buffer. 
    public int _tail { get; set; }

    public int _length { get; set; }
    public int _bufferSize { get; set; }
    public T[] _buffer { get; set; }

    object _lock = new object();

    public KarthicCircularQueue(int size)
    {
      this._buffer = new T[size];
      this._bufferSize = size;
      _head = _bufferSize - 1; //keep the pointer on last index
    }

    public bool IsEmpty
    {
      get { return _length == 0; }
    }

    public bool IsFull
    {
      get { return _length == _bufferSize; }
    }

    //This takes the index value of the parameter and give the next index by applying the circular logic
    //Eg: if it reaches the size it gives the next as 0 and then again it continues
    public virtual int NextPosition(int position)
    {
        //Increment the current position to the next and get the next position slot on the circular buffer
       return (position + 1) % _bufferSize;
    }

    //this method is created to help the custom class
    public void MoveTailToNextOldest()
    {
      _tail = NextPosition(_tail);
    }

    //Add items to the queue

    public virtual  void Enqueue(T toAdd)
    {
      //To make thread safe
      lock (_lock)
      {
        //When the class is intialized the head is set to length -1 bcoz the first item will be place in 0 index
        //head will always point to the index of the new element
        _head = NextPosition(_head);
        _buffer[_head] = toAdd;
          //Important only if the length is full we move the tail
        if (IsFull)
          //code will come here when the buffer is full..now the first element will be overwritten..when 0 is ovewritten..1 becomes old item
          //tail should always point to the old data..when the buffer is full we set the tail value accordingly
          _tail = NextPosition(_tail);
          //when 0 is overwritten 1 will become old item
        else
          //lenght is incremented at the last so that when last element is added he length will be full
          _length++;
      }
    }


    public virtual T Dequeue()
    {
      lock (_lock)
      {
        if (IsEmpty) throw new InvalidOperationException("Queue exhausted");
        //dequeue element
        T dequeued = _buffer[_tail];
        //set the tail to the next oldest
        _tail = NextPosition(_tail);
        //decrease the length
        _length--;
        return dequeued;
      }
    }

    //Logic to remove only if the value is not retrived
    //Get the tail of the buffer
    
    public T GetTail()
    {
      lock (_lock)
      {
        T lastitem = _buffer[_tail];

        return lastitem;
      }
    }
  }


  public class CircularQueueItem<T>
  {
    public T Data { get; set; }
    public bool Retrived { get; set; }

    public CircularQueueItem(T Data)
    {
      this.Data = Data;
      Retrived = false;
    }
  }

  //Custom Class Requirement: The class should behave like circular queue. while removing the item, it has to check whether the value is retrived before. if the value is not retrived..
  //then it is safe to remove..if not find the next olderst that in not retived..if all are retrived then give the error message
  public class CustomCircularQueue<T> : KarthicCircularQueue<CircularQueueItem<T>>
  {
    public CustomCircularQueue(int size) : base (size)
    {

    }

    public CircularQueueItem<T> Get()
    {
       CircularQueueItem<T> tail = base.GetTail();
       tail.Retrived = true;
       return tail;

    }


    public bool CheckItemReadByIndex(int position)
    {

      CircularQueueItem<T> item = _buffer[position];

      return item.Retrived;
    }


    public override void Enqueue(CircularQueueItem<T> toAdd)
    {
      //get the next index and check whehter it can be enqueued


    }


    public override CircularQueueItem<T> Dequeue()
    {
      //get the tail index as per the circula queue
      CircularQueueItem<T> tail = this.GetTail();

      //check whether the tail is retrived
      if (tail.Retrived == false)
      {
        return base.Dequeue();
      }
      else
      {
         //if the tail value is retrived then move the tail pointer to the next older
        //If all the elements are retrived this will become a loop..so avoid by attempting only to the size of the array
      int attempts = this._length; //no of item in the buffer

      while (attempts > 0 && tail.Retrived == true)
      {
         //move to next oldest and check for the retrivel
        this.MoveTailToNextOldest();
         tail = this.GetTail();
         attempts--;
          
      }

      if(attempts == 0)
      {
        //It tried all the elements in the buffer but every element is retrived before
         throw new Exception("cannot add elements to the buffer");
      }


          return base.Dequeue();
     
      }

      
 	        
    }
     
   
  }
}
