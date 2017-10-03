using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    //First oN First out
     public class KarthicQueue<T>
    {
         //This queue is implemented using two stacks as per the solution to one problem
         //newest is the regular stack..the new item will be on the top
         Stack<T> stacknewest;
         //reversed the stacknewest to have the newest at the bottom and oldest at the top
         Stack<T> stackoldest;
        
         public KarthicQueue()
         {
             stacknewest = new Stack<T>();
             stackoldest = new Stack<T>();
         }

    

         public T DeQueue()
         {
             //remove the older

             ReverseStack();
             //we removed from the oldest
            return stackoldest.Pop();
             //remove the correspoding on the new
       
         }

         public void EnQueue(T value)
         {

             //when we add new item simply add to the regular stack
             stacknewest.Push(value);

         }

         public T Peek()
         {
             //remember the peek should be the first item added..oldest
             ReverseStack();
             return stackoldest.Peek();
         }

         public void ReverseStack()
         {
             //This is done only when the old is empty
             if (stackoldest.Count == 0)
             {
                 while (stacknewest.Count > 0)
                 {
                     T value = stacknewest.Pop();//update: here we clear the item from reglar and then adding to the oldstack
                     stackoldest.Push(value);
                 }
             }
         }
    }


     //First in First out
     public class KarthicQueueByLinkedList<T>
     {

       LinkedList<T> queuelist;
      // LinkedListNode<T> last ;

       public KarthicQueueByLinkedList()
       {
         queuelist = new LinkedList<T>();
       }

       public void EnQueue(T item)
       {
         //New item is added

         if (queuelist.Count == 0)
         {
           queuelist.AddFirst(new LinkedListNode<T>(item));
            //last = queuelist.First;
         }
         else
         {
            queuelist.AddLast(item);
             
         }

          //make sure to rearrange the item based on the priortiy
          //
       }

       public T DeQueue()
       {
         if (queuelist.Count == 0)
         {
           throw new Exception("Queue is empty");
         }

         LinkedListNode<T> head  = queuelist.First;
         queuelist.RemoveFirst();
         return head.Value;
          
       }

       public T Peek()
       {
         return queuelist.First.Value;
       }
     }

}
