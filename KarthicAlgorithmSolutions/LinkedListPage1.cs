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
    public partial class LinkedListPage1 : Form
    {
        public LinkedListPage1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string input = this.textBox1.Text;

            LinkedList<int> numberlist = new LinkedList<int>();

            foreach(string s in input.Split(','))
            {
                numberlist.AddLast(Convert.ToInt32(s));
            }

            this.textBox2.Text = AvoidDuplicates(numberlist);

            //learn linked list

            

        


          

        }


        public string AvoidDuplicates(LinkedList<int> list)
        {
            Hashtable ht = new Hashtable();
            LinkedListNode<int> node = list.First;
            LinkedListNode<int> previousnode;
            //Apporach 1



            //foreach (var item in numberlist)
            //{
            //    if (ht.ContainsKey(item))
            //    {
            //        ht[item] = (int)ht[item] + 1;
            //    }
            //    else
            //    {
            //        ht.Add(item, 1);
            //    }
            //    //sb.Append(item.ToString()).Append(",");
            //}

            //foreach (DictionaryEntry entry in ht)
            //{
            //    if (entry.Value is int)
            //    {
            //        if ((int)entry.Value > 1)
            //        {

            //            numberlist.Remove((int)entry.Key);
            //        }
            //    }
            //}


            //if we loop through an item in foreach of any object..we can't remove that from the collection
            while (node != null)
            {

                if (ht.ContainsKey(node.Value))
                {
                    //have to remove the node
                    previousnode = node.Previous;
                    list.Remove(node);
                    node = previousnode;
                }
                else
                {
                    ht.Add(node.Value, 1);
                }


                node = node.Next;

            }


            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString()).Append(",");
            }


            return sb.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {

            string input = this.textBox1.Text;

            LinkedList<int> numberlist = new LinkedList<int>();

            foreach (string s in input.Split(','))
            {
                numberlist.AddLast(Convert.ToInt32(s));
            }

            this.textBox2.Text = AvoidDuplicatesWithoutExtraBuffer(numberlist);


        }


        private string AvoidDuplicatesWithoutExtraBuffer(LinkedList<int> list)
        {
            //Take the first item in the current node

            LinkedListNode<int> runner = null;
            LinkedListNode<int> temp = null;

            LinkedListNode<int> current = list.First;

            while (current != null)
            {
                //code to remove duplicates
                //get the next node
                runner = current.Next;
                //nested loop to check the current value with all other node
                while (runner != null)
                {
                    if ((int)runner.Value == (int)current.Value)
                    {
                        temp = runner.Previous;
                        //delete the runner node
                        list.Remove(runner);
                        runner = temp;
                  
                    }


                    runner = runner.Next;
                }

                //loop through each node 
                current = current.Next;

            }


            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString()).Append(",");
            }


            return sb.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            string input = this.textBox4.Text;

            LinkedList<int> numberlist = new LinkedList<int>();

            foreach (string s in input.Split(','))
            {
                numberlist.AddLast(Convert.ToInt32(s));
            }

            this.textBox3.Text = nthnodeFromtheLastElement(numberlist, Convert.ToInt32(this.textBox5.Text)).Value.ToString();


        }

        private LinkedListNode<int> nthnodeFromtheLastElement(LinkedList<int> list, int n)
        {
            //Assumption: The single linked list does not contain any duplicate nodes

            //Iterate from last to the node

            //LinkedListNode<int> current = list.Last;
            //int n = -1;
            //while (current != null)
            //{
            //    n++;

            //    if (current.Value == nodevalue)
            //    {
            //        return n;

            //    }

            //    current = current.Previous;
            //}

            //return -1;

            int counter = 1;  //n=1 for the last element
            LinkedListNode<int> node = list.Last;

            while (node != null)
            {
                if (counter == n)
                {
                    return node;
                }

                counter++;
                node = node.Previous;
            }


            return null;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = this.textBox4.Text;

            LinkedList<int> numberlist = new LinkedList<int>();

            foreach (string s in input.Split(','))
            {
                numberlist.AddLast(Convert.ToInt32(s));
            }

            //nth element: parameter pass the head and the nth number to get nth node element

            this.textBox3.Text = nthToLast(numberlist.First, Convert.ToInt32(this.textBox5.Text)).Value.ToString();



        }

        // Function to return the nth node from the end of a linked list.
        // Takes the head pointer to the list and n as input
        // Returns the nth node from the end if one exists else returns NULL.
        public LinkedListNode<int> nthToLast(LinkedListNode<int> head, int n)
        {
            if (head == null || n < 1)
            {
                return null;
            }

            // make pointers p1 and p2 point to the start of the list.
            LinkedListNode<int> p1 = head;
            LinkedListNode<int> p2 = head;
            // The key to this algorithm is to set p1 and p2 apart by n-1 nodes initially
            // so we want p2 to point to the (n-1)th node from the start of the list
            // then we move p2 till it reaches the last node of the list. 
            // Once p2 reaches end of the list p1 will be pointing to the nth node 
            // from the end of the list.

            // loop to move p2.
            for (int j = 0; j < n - 1; ++j)
            { // skip n-1 steps ahead
                // while moving p2 check if it becomes NULL, that is if it reaches the end
                // of the list. That would mean the list has less than n nodes, so its not 
                // possible to find nth from last, so return NULL.
                if (p2 == null)
                {
                    return null; // not found since list size < n
                }
                p2 = p2.Next; 
            }
            // at this point p2 is (n-1) nodes ahead of p1. Now keep moving both forward
            // till p2 reaches the last node in the list.
            while (p2.Next != null)
            {
                p1 = p1.Next;
                p2 = p2.Next;
            }
            return p1;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string input = this.textBox8.Text;

            LinkedList<int> numberlist = new LinkedList<int>();

            foreach (string s in input.Split(','))
            {
                numberlist.AddLast(Convert.ToInt32(s));
            }

            this.textBox7.Text = Deletenode(numberlist, Convert.ToInt32(this.textBox6.Text));

        }

        private string Deletenode(LinkedList<int> list, int nodevalue)
        {

            LinkedListNode<int> current = list.First;
            LinkedListNode<int> temp;
            while (current != null)
            {

                if (current.Value == nodevalue)
                {
                    temp = current.Previous;
                    list.Remove(current);
                    current = temp;
                }

                current = current.Next;
            }


            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString()).Append(",");
            }


            return sb.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            //we don't have access to the list or the head
            //give the node and we need to delete the node

            //Implemented my own node :)


            string input = this.textBox8.Text;

            //LinkedList<int> numberlist = new LinkedList<int>();
            KarthicLinkedList numberlist = new KarthicLinkedList();

            foreach (string s in input.Split(','))
            {
                Node node = new Node();
                node.Data = Convert.ToInt32(s);
                numberlist.AddNode(node);
            }


            StringBuilder sb = new StringBuilder();
            foreach (var item in numberlist)
            {
                sb.Append(item.Data.ToString()).Append(",");
            }


  
              this.textBox7.Text = sb.ToString();

          //  this.textBox7.Text = Deletenode(numberlist, Convert.ToInt32(this.textBox6.Text));

        }

        private void button7_Click(object sender, EventArgs e)
        {

            string input = this.textBox8.Text;

            //LinkedList<int> numberlist = new LinkedList<int>();
            KarthicLinkedList numberlist = new KarthicLinkedList();

            foreach (string s in input.Split(','))
            {
                numberlist.AddNode(new Node(Convert.ToInt32(s)));
            }

            this.textBox7.Text = Iteratefromhead(DeleteNodeAcessonlynode(numberlist.headnode, Convert.ToInt32(this.textBox6.Text)));

        }

        //Given the head and node with value..delete the node with value and return the head
        //Assumption: The node values are unique
        private Node DeleteNodeAcessonlynode(Node head, int value)
        {
            //No api use...you have access only to the node
            //There is no remove method on the linkedlist..shouln't access first
            //The give value might be head
            if (head.Data == value)
            {
                //this will be null..if head has to be removed the return should be null
                return head.Next;
            }
            //Assign head to runner
            Node runner = head;
            //loop from top to bottom
            while (runner.Next != null)
            {
                //check the runner.next node
                if (runner.Next.Data == value)
                {
                    //we have to remove runner.Next
                    runner.Next = runner.Next.Next;
                    return head;
                }

                runner = runner.Next;
            }

            return head;
        }


        private Node IncrementExists(Node node, int value)
        {

            //Two condition to stop recursion
            if (node == null)
            {
                return null;
            }
            if (node.Data == value)
            {
               return IncrementExists(node.Next, value);

            }
            else
            {
                return node;
            }
        }

        private string Iteratefromhead(Node head)
        {
            StringBuilder sb = new StringBuilder();
            Node runner = new Node();
            runner = head;
            while (runner != null)
            {
                sb.Append(runner.Data.ToString()).Append(",");
                runner = runner.Next;

            }

            return sb.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            string input = this.textBox8.Text;

            //LinkedList<int> numberlist = new LinkedList<int>();
            KarthicLinkedList numberlist = new KarthicLinkedList();

            foreach (string s in input.Split(','))
            {
                numberlist.AddNode(new Node(Convert.ToInt32(s)));
            }

            this.textBox7.Text = Iteratefromhead(deleteNode(numberlist.headnode, Convert.ToInt32(this.textBox6.Text)));

        }

        //This code is from the Gayle book

        Node deleteNode(Node head, int d)
        {
            Node n = head;
            if (n.Data == d)
            {
                return head.Next; /* moved head */
            }
            while (n.Next != null)
            {
                if (n.Next.Data == d)
                {
                    n.Next = n.Next.Next;
                    return head; /* head didn’t change */
                }
                n = n.Next;
            }
            return head;
        }


        private Node DeleteReduntactNode(Node head, int d)
        {
            Node newhead = null;
            Node runner = head;

            //make sure newhead doesn't have value d

            while (runner.Data == d)
            {
                runner = runner.Next;
            }

            //runner will be the first non d value
            newhead = runner;

            while (runner.Next != null)
            {

                if (runner.Next.Data == d)
                {
                    runner.Next = runner.Next.Next;

                }
                else
                {
                    runner = runner.Next;
                }

            }

            return newhead;
        }





        private void button9_Click(object sender, EventArgs e)
        {
            string input1 = this.textBox10.Text;
            string input2 = this.textBox11.Text;
            KarthicLinkedList list1 = new KarthicLinkedList();
            foreach (string s in input1.Split(','))
            {
                list1.AddNode(new Node(Convert.ToInt32(s)));
            }
            KarthicLinkedList list2 = new KarthicLinkedList();
            foreach (string s in input2.Split(','))
            {
                list2.AddNode(new Node(Convert.ToInt32(s)));
            }

            Node result = AddTwoLinkedList(list1.headnode, list2.headnode, 0);

            this.textBox9.Text = Iteratefromhead(result);

        }

        private Node AddTwoLinkedList(Node node1, Node node2, int carry)
        {
            //condition to stop the recursion
            //Note: If list is greater than list still this need to continue
            if (node1 == null && node2 == null && carry == 0)
            {
                return null;
            }

            if (node1 == null && node2 == null && carry != 0)
            {
                //carry can have only two values either 0 or 1
                Node last = new Node(carry);
                return last;
            }

            int total = carry;

            if (node1 != null)
            {
                total = total + node1.Data;
            }
            if (node2 != null)
            {
                total = total + node2.Data;
            }

            Node node = new Node();
            node.Data = total % 10;


            node.Next = AddTwoLinkedList((node1 != null) ? node1.Next : null, 
                                         (node2 != null) ? node2.Next : null, 
                                         total >= 10 ? 1 : 0);

            ////total will be (7+ 5) = 12..//place the 2 is the result node and carry on the 1
            //if (node1.Next != null || node2.Next != null)
            //{

            //    node.Next = AddTwoLinkedList(node1.Next, node2.Next, total >= 10 ? 1 : 0);
            //}
            //else
            //{

            //    //if both are null and if the total is greater than zero then we need to add the carry to the result
            //    if (total >= 10)
            //    {
            //        //if the last is 12...2 is already in the return add 1 (carry)
            //        Node last = new Node(1);
            //        node.Next = last;
            //        last.Previous = node;
            //        //return node;

            //    }
            //}

            return node;
        }


        private string ConvertLinkedListToString(KarthicLinkedList numberlist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in numberlist)
            {
                sb.Append(item.Data.ToString()).Append(",");
            }

            return sb.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string input1 = this.textBox10.Text;
            string input2 = this.textBox11.Text;
            KarthicLinkedList list1 = new KarthicLinkedList();
            foreach (string s in input1.Split(','))
            {
                list1.AddNode(new Node(Convert.ToInt32(s)));
            }
            KarthicLinkedList list2 = new KarthicLinkedList();
            foreach (string s in input2.Split(','))
            {
                list2.AddNode(new Node(Convert.ToInt32(s)));
            }

            Node result = AddTwoLinkedListFromBackwards(list1, list2);

            this.textBox9.Text = Iteratefromhead(result);

        }


        public Node AddTwoLinkedListFromBackwards(KarthicLinkedList list1, KarthicLinkedList list2)
        {

            int length1 = NodeHelper.GetLength(list1);
            int length2 = NodeHelper.GetLength(list2);
            Node result = new Node();
            Node node1 = new Node();
            Node node2 = new Node();

            if (length1 > length2)
            {
                //Add padding to the shortest list
                node2 = NodeHelper.AddPadding(list2.headnode, (length1 - length2));
                node1 = list1.headnode;
            }
            else
            {
                node1 = NodeHelper.AddPadding(list1.headnode, (length2 - length1));
                node2 = list2.headnode;
            }

            //here the two nodes will be of same size 
            NodeAdditionResult resultnode = AddTwoLinkedListNodesFromTail(node1, node2);
            //After adding two list, if there are any carry over add it on the top
            if (resultnode.carry > 0)
            {
              
                Node node = NodeHelper.AddNodeToFirst(resultnode.resultnode, resultnode.carry);
                return node;
            }

           return resultnode.resultnode;
        }


      //This is recurrssive function
        //Note: Since we alredy did the padding, here both node1 and node2 will be of same length for sure
        public NodeAdditionResult AddTwoLinkedListNodesFromTail(Node node1, Node node2)
        {

           //stop the recurrsion at this condition
            if (node1 == null && node2 == null)
            {
                NodeAdditionResult emptynode = new NodeAdditionResult();
                return emptynode;
            }
            //we have to do the addition from right to left so call the recurrsion first
            NodeAdditionResult runnerresult = new NodeAdditionResult();
            //Both the node is of same size..but during recursion stop the recursion when the next is null
            //so both node1 and node2 will be null at the same time
            runnerresult = AddTwoLinkedListNodesFromTail(node1 != null ? node1.Next : null, node2 != null ? node2.Next : null);

            int total = runnerresult.carry;

            if (node1 != null)
            {
                total = total + node1.Data;
            }
            if (node2 != null)
            {
                total = total + node2.Data;
            }

           //if the total is 12 add 2 to the node and add 1 to the carry
            //here resultnode will have the result of the right of the current node
            //append the resultnode with the result of the current node...it will be  current result --> right result
            runnerresult.resultnode = NodeHelper.AddNodeToFirst(runnerresult.resultnode, total % 10);
            runnerresult.carry = total >= 10 ? 1 : 0;

            return runnerresult;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string input = this.textBox8.Text;

            //LinkedList<int> numberlist = new LinkedList<int>();
            KarthicLinkedList numberlist = new KarthicLinkedList();

            foreach (string s in input.Split(','))
            {
                numberlist.AddNode(new Node(Convert.ToInt32(s)));
            }

            this.textBox7.Text = Iteratefromhead(DeleteNodeAcessonlynode(numberlist.headnode, Convert.ToInt32(this.textBox6.Text)));

        }


        public static Boolean deletenodewithouthead(Node n)
        {
            if (n == null || n.Next == null)
            {
                //cannot delete the node
                return false;
            }

            //use a variable fro next node
            Node next = new Node();
            next = n.Next;
            n.Data = next.Data;
            n.Next = next.Next;
   
            return true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            LinkedListPage2 frm = new LinkedListPage2();
            frm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string input = this.textBox1.Text;

           KarthicLinkedList list = NodeHelper.GetLinkedListByString(input);

           this.textBox2.Text = NodeHelper.GetStringByNode(AvoidDuplicateswithhead(list.headnode));

        }

        private Node AvoidDuplicateswithhead(Node head)
        {
            Hashtable ht = new Hashtable();
            Node lastnode = null;
            Node node = head;
            while (node != null)
            {

                if (ht.ContainsKey(node.Data))
                {
                    lastnode.Next = node.Next;
                    //we don't update the last node

                }
                else
                {
                    ht.Add(node.Data, true);
                    lastnode = node;
                }


                node = node.Next;
            }

            return head;
        }

        private void button13_Click(object sender, EventArgs e)
        {
          string input = this.textBox1.Text;

            KarthicLinkedList list = NodeHelper.GetLinkedListByString(input);

            this.textBox2.Text = NodeHelper.GetStringByNode(RemoveDuplicatesWithoutExtraBuffer(list.headnode));
        }



        private Node RemoveDuplicatesWithoutExtraBuffer(Node head)
        {

            if (head == null)
            {

                return null;
            }
            Node current = head;
            Node runner = null;

            while (current != null)
            {

                runner = current;

                while (runner.Next != null)
                {
                    if (runner.Next.Data == current.Data)
                    {
                        runner.Next = runner.Next.Next;
                        //Important after this step it again goes through the while loop and check the runner.next.next is not current
                        //We don't increament next here 
                    }
                    else
                    {
                        runner = runner.Next;
                    }
                }
                
                current = current.Next;
            }

            return head;
        }

        private Node AvoidDuplicatesWithoutUsingExtraBuffer(Node head)
        {

            if (head == null)
            {
                return null;
            }

            Node current = head.Next;
            Node previous = null;
            Node runner = null;
            //current makes a normal iteration from second node to last
            while (current != null)
            {
                runner = head;
                //runnercompare the current node to its prior nodes. If the prior node has its value then it delets the current and updates

                //This while loop breaks when the current and runner are same which means 100 200 300 400 500
                //If current is 300..Runner will check its prior nodes 100, 200
                //current is 400..Runner will check its prior node 100,200,300
                while (runner != current)
                {
                    //If any of the prior node contain the same value
                    if (runner.Data == current.Data)
                    {
                        //then remove the current 
                        //To remove the current we maintain the previous
                        previous.Next = current.Next;
                        current = current.Next;
                        //here we updated the current to next and
                        //we got to make sure to check this new value..so to by pass this from master while loop increment
                        break;
                    }


                    runner = runner.Next;
                }

                //This condition is to by pass the delete node...current is update to current.next if we have a delete 
                if (runner == current)
                {
                    previous = current;
                    current = current.Next;
                }
            }

            return head;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            string input = this.textBox8.Text;

            //LinkedList<int> numberlist = new LinkedList<int>();
            KarthicLinkedList numberlist = new KarthicLinkedList();

            foreach (string s in input.Split(','))
            {
                numberlist.AddNode(new Node(Convert.ToInt32(s)));
            }

            this.textBox7.Text = Iteratefromhead(DeleteReduntactNode(numberlist.headnode, Convert.ToInt32(this.textBox6.Text)));

        }

        private void button16_Click(object sender, EventArgs e)
        {

          //just implementation
            deletegivennode(new Node(5));


        }

        public bool deletegivennode(Node node)
        {

            if (node == null || node.Next == null)
            {

                return false;
                //Important: cannot delete the null node or the last node

            }

            Node nextnode = node.Next;
            node.Data = nextnode.Data;
            node.Next = nextnode.Next;

            return true;

        }


    }

}
      
