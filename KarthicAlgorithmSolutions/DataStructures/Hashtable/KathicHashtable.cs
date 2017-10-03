using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Puzzles.DataStructures.KarthicHashtable
{
     public class KarthicHashtable
    {
         //Array of linked list where each linkedlist has cell type object
         LinkedList<Cell<object, object>>[] buckets = null;
         public int Capacity { get; set; }

         
        
          public KarthicHashtable(int size)
         {
             this.Capacity = size;
             buckets = new LinkedList<Cell<object, object>>[size];
         }

         //This is a bad hash function..in real world this would be 
          public int GetHash(object key)
          {
              int arrayindex = key.ToString().Length % Capacity;
              return arrayindex;
          }

          public void Add(object key, object value)
          {
              int arrayindex = GetHash(key);

              LinkedList<Cell<object, object>> bucket = null;

              //if there is no collision..it should be null
              if (buckets[arrayindex] == null)
              {
                  //new item
                  bucket = new LinkedList<Cell<object, object>>();
                  //associate bucket to the index of the arry
                  buckets[arrayindex] = bucket;
              }
              else
              {
                  //hadle collision
                  //if there in an item already the bucket..means these two keys has same hashcode
                  //add the item to the list
                  bucket = buckets[arrayindex];

                  //Make sure the key doesn't exisit..if exists...remove it
                  foreach (var item in bucket)
                  {
                       if(item.IsEquivalent(key))
                       {
                           //duplicate value exists
                           bucket.Remove(item);
                           break;
                       }
                  }

              }

              //add cell to the bucket
              Cell<object, object> bucketcell = new Cell<object, object>(key, value);
              bucket.AddFirst(bucketcell);


          }

          public void Remove(object key)
          {

              //find the index of the array (hash)
              //get the linkedlist in the index
              //count the no of cell in linked list
              
              //if it has only one cell...clear that linkedlist or the cell
              //if it has more than cell..iterate and delete the cell that has to be removed
          }



    }

     public class Cell<K, V> : IComparer
     {
         public K Key { get; set; }
         public V Value { get; set; }

         public Cell(K key, V value)
         {
             this.Key = key;
             this.Value = value;
         }


         public bool IsEquivalent(K key)
         {
             return this.Key.Equals(key);

         }
           int IComparer.Compare(object x, object y)
         {
         	throw new NotImplementedException();
          }





     }

}
