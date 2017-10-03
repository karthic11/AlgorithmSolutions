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
    public partial class LinkedListPage2 : Form
    {
        public LinkedListPage2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            KarthicLinkedList linkedlist = NodeHelper.GetLinkedListByString(this.textBox1.Text);
            //Make it circular
            Node node = new Node();

            //this is just a hack to make list cirucular...there might be good way to do this
            //it won't work for no loop 
            foreach (Node n in linkedlist)
            {
                if (n.Data == linkedlist.lastnode.Data)
                {
                    node = n;
                    break;
                }
            }

            linkedlist.lastnode.Next = node;
            
            Node startloop = GetLoopStartNode(linkedlist.headnode);
            this.textBox2.Text = startloop.Data.ToString();


        }


        //alogorithm to find the node that begins the loop
        public Node GetLoopStartNode(Node head)
        {

            Node fastrunner = head;
            Node slowrunner = head;

            //Fast runner moves 2 nodes per iteration
            //slow runner moves 1 node per iteration

            //If the given linked list has a loop then fast runner will meet  slow runner at colliding point (Loopsize - k) where k is no of steps moved
            //If there is no loop fast runner node will not collide and fast runner will reach null at the end of the node

            while (fastrunner != null && fastrunner.Next != null)
            {

                slowrunner = slowrunner.Next;
                fastrunner = fastrunner.Next.Next;

                //check for colliding point
                if (slowrunner == fastrunner)
                {
                    break;
                    //This will be the colliding point (loopsize - k) and they will meet after loopsize - k turns
                    //The slow will be k steps..fast runner will be another k steps (2k) into the loop
                    //Slow runner is k step behind fast runner
                    //fast runner is (loopsize -k ) steps behind slow runner (behind bcoz circle)
                    //At colliding point slow runner is k steps from the from of loop.
                   
                }
            }

            //if there is no collision then fast runner or fast runner next will be null
            if (fastrunner == null || fastrunner.Next == null)
            {
                return null;
                //no loop exists
            }

            //Loop detected
            //find the start of the loop
            //at this point put keep one pointer on that location and move another pointer to header
            slowrunner = head;

            while (slowrunner != fastrunner)
            {
                slowrunner = slowrunner.Next;
                fastrunner = fastrunner.Next;
            }

            //at this point both
            return slowrunner;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            KarthicLinkedList linkedlist = NodeHelper.GetLinkedListByString(this.textBox4.Text);
            this.textBox3.Text = IsPalindrome(linkedlist.headnode) == true ? "Palindrome" : "Not a Palindrome";


        }


        //Given linkedlist length is not known..if length is given reverse the linked list and compare just the first half with the reversed list first half

        public bool IsPalindrome(Node head)
        {
           
            if (head == null)
            {
                return true;
            }

            Node slow = head;
            Node fast = head;
            //fast runs twice as fast as slow runner
            //Note: we need to be carefult with the odd lengh and even length of palindrom
            //eg 123454321  1234554321
            //If the list is odd, we need to skip the middle else consider the middle

            Stack<int> stack = new Stack<int>(); //Last in first out
            while (fast != null && fast.Next != null)
            {
                stack.Push(slow.Data);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            //At the end of the above while loop 
            //if the given list is odd fast will be in last node and slow would in middle node (which need to be skipped from stack insertion)
            //if the given list is even fast will be null and slow would in right half (good to go)

            //since we need to skip the middle for odd...for odd fast.next will be null and fast won't be null:)
            if (fast != null)
            {
                slow = slow.Next;
            }

            //Now the slow is in the middle..move the slow to the end and compare it with the stach which give the last as first

            while (slow != null)
            {
                int value = stack.Pop();
                if (slow.Data != value)
                {
                    return false;
                }
                slow = slow.Next;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            KarthicLinkedList linkedlist = NodeHelper.GetLinkedListByString(this.textBox6.Text);
            int value = Convert.ToInt16(this.textBox7.Text);
            this.textBox5.Text = NodeHelper.GetStringByNode(partitionlinkedlist(linkedlist.headnode, value));



        }

        private Node partitionlinkedlist(Node head, int value)
        {


            Node firstpartitionstart = null;
            Node firstpartitionend = null;
            Node secpartitionstart = null;
            Node secpartitionend = null;

            //we have to partition the given linked list by the value
            //iterate through the linked list
            Node runner = head;
            Node next = null;
            while (runner != null)
            {
                //we might change the next value of the node so put it tem variable to use for iteration
                next = runner.Next;
                //we clear the next node of the runner bcoz we are taking this node and inserting into a different linkedlist
                runner.Next = null;

                //if the data is lesser than value put it in first partition
                if (runner.Data < value)
                {
                    if (firstpartitionstart == null)
                    {
                        firstpartitionstart = runner;
                        firstpartitionend = firstpartitionstart;
                    }
                    else
                    {
                        firstpartitionend.Next = runner;
                        firstpartitionend = runner;
                    }

                }
                //if the data is greater than value put it in sec partition
                else
                {
                    if (secpartitionstart == null)
                    {
                        secpartitionstart = runner;
                        secpartitionend = secpartitionstart;
                    }
                    else
                    {
                        secpartitionend.Next = runner;
                        secpartitionend = runner;
                    }

                }


                runner = next;

            }

            //at the end of while loop we will have two linked list one with less than value and other greater than value

            //merge two linkedlist

            if (firstpartitionstart == null)
            {
                return secpartitionstart;
            }

            firstpartitionend.Next = secpartitionstart;

            return firstpartitionstart;

          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KarthicLinkedList linkedlist = NodeHelper.GetLinkedListByString(this.textBox6.Text);
            int value = Convert.ToInt16(this.textBox7.Text);
            Node result = partitionlinkedlistSolution2(linkedlist.headnode, value);
            this.textBox5.Text = NodeHelper.GetStringByNode(result);

        }

        private Node partitionlinkedlistSolution2(Node node, int value)
        {

            Node firstpart = null;
            Node secpart = null;
            //Node next = null;
            while (node != null)
            {
                Node next = node.Next;

                if (node.Data < value)
                {
                    //we are overwriting the next and that's the reason we stored it in temp
                    node.Next = firstpart;
                    firstpart = node;
                }
                else
                {
                    node.Next = secpart;
                    secpart = node;
                }


                node = next;


            }


           //at the end of while  50 15        sec 800 900 400

            //at the end of while loop we will have two linked list one with less than value and other greater than value

            //merge two linkedlist

            if (firstpart == null)
            {
                return secpart;
            }

         //here we don't have the ref to the end of first part
          //firstpart will be the head so iterate to the end

            Node merge = firstpart;

            while (firstpart.Next != null)
            {
                firstpart = firstpart.Next;
            }

            firstpart.Next = secpart;

            return merge;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            KarthicLinkedList linkedlist = NodeHelper.GetLinkedListByString(this.textBox10.Text);

            this.textBox9.Text = NodeHelper.GetStringByNode(MergeSort(linkedlist.headnode));


        }

        public Node MergeSort(Node head)
        {

            if (head == null || head.Next == null)
            {
                return head;
            }
            Node middle = getMiddle(head);      //get the middle of the list
            Node sHalf = middle.Next;  //split the list into two halfs
            middle.Next = null;
            Node firstparthead = MergeSort(head);
            Node secparthead = MergeSort(sHalf);

            //return MergeAndSort(MergeSort(head), MergeSort(sHalf));  //recurse on that
            return MergeAndSort(firstparthead, secparthead);  //recurse on that


        }

        //Merge two nodes by comparing to get sorted node and return the sorted node
        public Node merge(Node a, Node b)
        {
          //dummy is like a helper node..It is an empty head..and it helps to form a sorted item to its tail
            Node dummyHead = new Node();
            Node curr; 


            //curr will be a runner..it's pointer will be running between the node.it's start with the head node
            curr = dummyHead;

            while (a != null && b != null)
            {
                if (a.Data <= b.Data)
                {
                    curr.Next = a;
                     a = a.Next; 
                }
                else
                { 
                    curr.Next = b; 
                    b = b.Next;
                }
                curr = curr.Next;
            }

          //here eg : Node a (3->5->6) and Node b (1-4)
          //then here the current will be 1->3-->4   and Node a (5-->6) b (null)
          //
            curr.Next = (a == null) ? b : a;

            return dummyHead.Next;
        }

        //Merge two nodes by comparing to get sorted node and return the sorted node
        public Node MergeAndSort(Node a, Node b)
        {


            Node resulthead = null;
            Node resulttail = null;

            while (a != null && b != null)
            {
                if (a.Data <= b.Data)
                {
                    Node a_Next = a.Next;
                    //clear a.Next
                    a.Next = null;
                    if (resulthead == null)
                    {
                        resulthead = a;
                        resulttail = a;
                    }
                    else
                    {
                        resulttail.Next = a;
                        resulttail = a;
                    }
                    a = a_Next;
                }
                else
                {
                    Node b_Next = b.Next;

                    if (resulthead == null)
                    {
                        resulthead = b;
                        resulttail = b;
                    }
                    else
                    {
                        resulttail.Next = b;
                        resulttail = b;
                    }

                    b = b_Next;
                }
              
            }

            //here eg : Node a (3->5->6) and Node b (1-4)
            //then here the current will be 1->3-->4   and Node a (5-->6) b (null)
            //
            resulttail.Next = (a == null) ? b : a;

            return resulthead;
        }


        public Node getMiddle(Node head)
        {
            if (head == null) 
            {
                return head; 
            }
            Node slow, fast; 
            slow = fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next; 
                fast = fast.Next.Next;
            }
            return slow;
        }

        private void button6_Click(object sender, EventArgs e)
        {


          //http://rajpal.x10.mx/copy-a-linked-list-with-next-and-random-pointer/


          RandomNode node1 = new RandomNode(1);
          RandomNode node2 = new RandomNode(2);
          RandomNode node3 = new RandomNode(3);
          RandomNode node4 = new RandomNode(4);

          node1.Next = node2;
          node2.Next = node3;
          node3.Next = node4;
          
          //set random pointers
          node1.Random = node3;
          node2.Random = node4;
          node3.Random = node4;
          node4.Random = node1;


          //Now we created linkedlist with next and random pointer..

          RandomNode duplicate = DuplicateLinkedListWithRandomNode(node1);

          this.textBox8.Text = NodeHelper.GetStringByRandomNode(duplicate);

        }

        // http://www.geeksforgeeks.org/clone-linked-list-next-random-pointer-o1-space/

        public RandomNode DuplicateLinkedListWithRandomNode(RandomNode originalhead)
        {

          RandomNode duplicatehead = null;
          RandomNode originalrunner, duplicaterunner, tempnext, tempnew;

          //Iterate through the original and create a duplicate node and place it between original and next
          //eg: Input A, B, C
          // A a B b C c

          originalrunner = originalhead;

          while (originalrunner != null)
          {
            //store the next ..be safe for overwriting
            tempnext = originalrunner.Next;

            //create a new node everytime
            tempnew = new RandomNode(originalrunner.Data);
            tempnew.Next = originalrunner.Next;
            tempnew.Random = null; //don't set the next

            //set the new next to the original
            originalrunner.Next = tempnew;

            //we already incremented this
            originalrunner = tempnext;

          }

          //At the end of this will loop we will have A, a, B, b, C, c

          //Now copy the arbitrary link in this fashion original->next->random = original->random->next;
          //This works because original->next is nothing but copy of original and Original->random->next is nothing but copy of random.

          originalrunner = originalhead;

          while (originalrunner != null)
          {
            //here original.next will be the copy of the orignal node.. orignal.random.next will be the copy of random
            originalrunner.Next.Random = originalrunner.Random.Next;

            //move two step to get only the orignla
            originalrunner = originalrunner.Next.Next;
          }

          //At the end of loop both orignal and copy will have same random pointer

          //restore back the orignal next property

          originalrunner = originalhead;
          //set duplicate header
          duplicatehead = originalrunner.Next;
          duplicaterunner = duplicatehead;

          while (originalrunner != null && duplicaterunner != null)
          {

            //set the correct orignal
            originalrunner.Next = originalrunner.Next.Next;
            //this is for iteration
            originalrunner = originalrunner.Next;

            if (duplicaterunner.Next != null)
            {
              duplicaterunner.Next = duplicaterunner.Next.Next;
              duplicaterunner = duplicaterunner.Next;
            }

          }

          //at the end of duplicate is copied and orignal is restored

          return duplicatehead;
        }


        private RandomNode deepCopy(RandomNode original)
        {
          // We use the following map to associate newly created instances 
          // of RandomNode with the instances of RandomNode in the original list

          Hashtable map = new Hashtable();
          // We scan the original list and for each RandomNode x we create a new 
          // RandomNode y whose data is a copy of x's data, then we store the 
          // couple (x,y) in map using x as a key. Note that during this 
          // scan we set y.next and y.random to null: we'll fix them in 
          // the next scan
          RandomNode x = original;
          while (x != null)
          {
            RandomNode y = new RandomNode(x.Data);
            y.Next = null;
            y.Random = null;

            map.Add(x, y);

            x = x.Next;
          }
          // Now for each RandomNode x in the original list we have a copy y 
          // stored in our map. We scan again the original list and 
          // we set the pointers buildings the new list
          x = original;
          while (x != null)
          {
            // we get the node y corresponding to x from the map
            RandomNode y = (RandomNode)map[x];
            // let x' = x.next; y' = map.get(x') is the new node 
            // corresponding to x'; so we can set y.next = y'
            y.Next = (RandomNode)map[x.Next];
            // let x'' = x.random; y'' = map.get(x'') is the new 
            // node corresponding to x''; so we can set y.random = y''
            y.Random = (RandomNode)map[x.Random];
         
            x = x.Next;
          }
          // finally we return the head of the new list, that is the RandomNode y
          // in the map corresponding to the RandomNode original
          return (RandomNode)map[original];
        }

        private void button7_Click(object sender, EventArgs e)
        {


            //http://www.jusfortechies.com/java/core-java/deepcopy_and_shallowcopy.php

          RandomNode node1 = new RandomNode(1);
          RandomNode node2 = new RandomNode(2);
          RandomNode node3 = new RandomNode(3);
          RandomNode node4 = new RandomNode(4);

          node1.Next = node2;
          node2.Next = node3;
          node3.Next = node4;

          //set random pointers
          node1.Random = node3;
          node2.Random = node4;
          node3.Random = node4;
          node4.Random = node1;


          //Now we created linkedlist with next and random pointer..

          RandomNode duplicate = deepCopy(node1);

          this.textBox8.Text = NodeHelper.GetStringByRandomNode(duplicate);

        }


    }
}
