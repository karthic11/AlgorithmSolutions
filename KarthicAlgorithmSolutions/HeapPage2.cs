using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
  public partial class HeapPage2 : Form
  {
    public HeapPage2()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {

      string input = this.textBox1.Text;

      string[] words = input.Trim().Split(' ', ',', ';', '.');


      Trie mytrie = new Trie();

      int heapcapacity = Convert.ToInt32(this.textBox3.Text);

      CustomMinHeap minheap = new CustomMinHeap(new KarthicMinHeapNodeComparer());

      foreach (string word in words)
      {

        InsertWorkIntoHeapAndTier(word, minheap, mytrie, heapcapacity);
      }
      StringBuilder sb = new StringBuilder();

      for(int i=0; i < heapcapacity; i++)
      {
        var item = minheap.PopRoot();
        sb.Append(item.word).Append(',');
      }

      this.textBox2.Text = sb.ToString();
    }


    private void InsertWorkIntoHeapAndTier(string word, CustomMinHeap minheap, Trie mytrie, int heapcapacity)
    {

      //Insert word into trie and increase the frequency and return the tier pointer
      TrieNode current = mytrie.CustomInsert(word);

      int heapindex = -1;

      if (current != null)
      {
        //already existing word in heap
        if (current._heapArrayIndex != -1)
        {
             
            //get the item from heap and update it frequency and re-heafity the index
          var item = minheap.GetItemByWord(word);
          item.frequency = current._frequency;

          //heapdown by index

          heapindex = minheap.CustomHeapDownByIndex(item.root._heapArrayIndex);

        }
        //new word
        {
             //insert into heap and get heapindex

            //if there is space in heap
            if(minheap.Size() < heapcapacity)
            {

              heapindex = minheap.CustomInsert(new MinHeapNode(word, current._frequency, current));
  
            }
            else{

              //if there is no space.
              //and if the new word frequency is lesser than the peek of heap then ignore it..we maintain the occurence frequency in tier..heap to just kee track of the no of latest records
               if(current._frequency > minheap.Peek().frequency)
               {
                 //remove the peek of minheap and update the corresponding on tier
                 var removed = minheap.PopRoot();
                 
                 
                 //update the tier to set heapindex to -1
                 mytrie.SearchAndUpdateIndex(removed.word);
             
                 //insert new word
                 heapindex = minheap.CustomInsert(new MinHeapNode(word, current._frequency, current));

               }
            }

            //After inserting into the minheap get the heapindex and update the corresponding tree node -minheapindex
            current._heapArrayIndex = heapindex;

        }

      


      }

    }

    private void button2_Click(object sender, EventArgs e)
    {

      string input = this.textBox1.Text;

      string[] words = input.Trim().Split(' ', ',', ';', '.');


      Trie mytrie = new Trie();

      foreach (string word in words)
      {
        mytrie.Insert(word);

      }

    //  this.textBox5.Text = mytrie.Search(this.textBox4.Text) ? "Exists" : "Not Exists";

      this.textBox5.Text = mytrie.FindNoofOccurence(this.textBox4.Text).ToString();

    }

    private void button3_Click(object sender, EventArgs e)
    {

      string input = this.textBox1.Text;

      string[] words = input.Trim().Split(' ', ',', ';', '.');


      Hashtable ht = new Hashtable();

      foreach (string word in words)
      {
        if (ht.ContainsKey(word))
        {
          ht[word] =  (int)ht[word] + 1;
        }
        else
        {
          ht.Add(word, 1);
        }
      }

      //sort hashtable 

      int topwords = Convert.ToInt32(this.textBox3.Text);
   
      //we need to sort the ht by the value in descending order
      //and the grab the key value for top words
      // this.textBox5.Text = mytrie.Search(this.textBox4.Text) ? "Exists" : "Not Exists";

      //this.textBox5.Text = mytrie.FindNoofOccurence(this.textBox4.Text).ToString();

       CustomMinHeap minheap = new CustomMinHeap(new KarthicMinHeapNodeComparer());

       int capacity = topwords;


       foreach (DictionaryEntry entry in ht)
       {
           if (minheap.Size() < capacity)
           {
               minheap.Insert(new MinHeapNode((string)entry.Key, (int)entry.Value, null));
               
           }
           else
           {
               //and if the new word frequency is lesser than the peek of heap then ignore it..we maintain the occurence frequency in tier..heap to just kee track of the no of latest records
               if ((int)entry.Value > minheap.Peek().frequency)
               {
                   minheap.PopRoot();
                   minheap.Insert(new MinHeapNode((string)entry.Key, (int)entry.Value, null));
               }
           
           }

       }


       StringBuilder sb = new StringBuilder();

       for (int i = 0; i < capacity; i++)
       {
           var item = minheap.PopRoot();
           sb.Append(item.word).Append(',');
       }

       this.textBox2.Text = sb.ToString();  


    }
  }
}
