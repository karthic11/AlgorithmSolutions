using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.BinaryTree
{


    /* Given Class This can be used both as binary tree and linkedlist */
    public class BiNode
    {
        public int Data { get; set; }
        public BiNode Node1 { get; set; } //Imagine this as left node for binary tree and previus node for LinkedList
        public BiNode Node2 { get; set; } //Imagine this as right node for binary tree and next node for linkedlist

        public BiNode(int data)
        {
            this.Data = data;
        }

        public BiNode(int data, BiNode left, BiNode right)
        {
            this.Data = data;
            this.Node1 = left;
            this.Node2 = right;
        }
    }

    public class BinaryTreeToLinkedList
    {

        /* Helper class and methods */
        public class NodePair
        {
            public BiNode Head { get; set; }
            public BiNode Tail { get; set; }

            public NodePair()
            {
            }
            public NodePair(BiNode head, BiNode tail)
            {
                this.Head = head;
                this.Tail = tail;
            }
        }

        /*Method to concat two nodes to form doubly linked list..eg: 0 and 1 will be formed as 0 -> 1 and 1 --> 0*/
        public void Concat(BiNode x, BiNode y)
        {
            //we wanna form doubly linked list from x and y
            //so the x.Next = y
            x.Node2 = y;
            //y.Previous = x
            y.Node1 = x;
        }

        public string ConvertLinkedlistToString(BiNode head)
        {
            StringBuilder sb = new StringBuilder();

            BiNode runner = head;
            while (runner != null)
            {
                sb.Append(runner.Data.ToString()).Append(",");
                runner = runner.Node2;
            }

            return sb.ToString();
            
        }



        public string ConvertUsingAdditionDataStructure(BiNode root)
        {

            NodePair result = ConvertMethod1(root);

            return ConvertLinkedlistToString(result.Head);

        }


        public string ConvertWithoutUsingAdditionDataStructure(BiNode root)
        {

            BiNode result = ConvertMethod2(root);

            return ConvertLinkedlistToString(result);

        }

        public string ConvertOptimalUsingCircularLogic(BiNode root)
        {

            BiNode head = ConvertToCircular(root);
            //the result will be linkedlist which is circular..break the circular connection
            //head.node1 will be tail (circular) and tail.node2 will be head
            head.Node1.Node2 = null;
            head.Node1 = null;
            return ConvertLinkedlistToString(head);
            
        }

        /* This is the recursive function that converts the binary tree to linked list and returns the Nodepair containaining the head and tail */
        public NodePair ConvertMethod1(BiNode root)
        {
            if (root == null)
            {
                return null;
            }
            //Part1 maintains the head and builds the list forward ..adding tail
            NodePair part1 = ConvertMethod1(root.Node1);
            //part2 maintains the tail and build the list backwards..adding head
            NodePair part2 = ConvertMethod1(root.Node2);

            //remember part1 will be nodepair with head and tail
            if (part1 != null)
            {
                //we want to merge and form linked list in this fashion    Head--->Tail + root
                Concat(part1.Tail, root);

            }
            if (part2 != null)
            {
                //part2 maintains the tail and build the list backwards..adding head
                //in this fashion     root + head ---> tail
                Concat(root, part2.Head);

            }

            NodePair result = new NodePair(part1 == null ? root : part1.Head,
                                           part2 == null ? root : part2.Tail);
            return result;

        } 


        /* This is the recursive function that converts the binary tree to linked list and returns the Nodepair containaining the head and tail */
        public BiNode ConvertMethod2(BiNode root)
        {
            if (root == null)
            {
                return null;
            }
            //Part1 maintains the head for left
            BiNode part1 = ConvertMethod2(root.Node1);
            //part2 maintains the head  for right and build the list backwards..adding head
            BiNode part2 = ConvertMethod2(root.Node2);

            //remember part1 will be nodepair with head and tail
            if (part1 != null)
            {
                //we want to merge and form linked list in this fashion    Head--->Tail + root
                Concat(GetTailFromHead(part1), root);

            }
            if (part2 != null)
            {
                //part2 maintains the tail and build the list backwards..adding head
                //in this fashion     root + head ---> tail
                Concat(root, part2);

            }

            //always return head...no concept of tail here
            return (part1 == null ? root : part1);

        }




        public BiNode GetTailFromHead(BiNode head)
        {
            if (head == null)
            {
                return null;
            }
            BiNode runner = head;

            while (runner.Node2 != null)
            {
                runner = runner.Node2;
            }

            return runner;

        }



        /* This is the recursive function that converts the binary tree to linked list and returns the BiNode which is circular */
        //makes the result circular
        //UPDATE:
        //Here is the logic
        // partition 1 + root + partition 3
        //(Head to tail) + root + (head to tail)
        //Make it circular  - connect tail of part3 to head of part1 and vice versa
        //If either of the part is null
        //If part1 is null - connect tail of part3 to root
        //if part3 is null - connect root to part1
        //dubug the code to understand
        //update: do revision often
        public BiNode ConvertToCircular(BiNode root)
        {
            if (root == null)
            {
                return null;
            }
            //Part1 maintains the head and builds the list forward ..here it is circular
            BiNode part1 = ConvertToCircular(root.Node1);
            //part3 maintains the head of the second partition..and the result node is circular
            BiNode part3 = ConvertToCircular(root.Node2);

            if (part1 == null && part3 == null)
            {
                //if both part1 and part3 are null make the root circular and return
                root.Node1 = root;
                root.Node2 = root;
                return root;
            }

            //tail3 maintain the tail of the sec partition..part3.node1 will be tail bczo it is circular
            BiNode tail3 = (part3 == null) ? root : part3.Node1;

            /* join the left to root to form this fashion         //(Head to tail) + root + (head to tail) */
            if (part1 == null)
            {
                //here the logic is hard 
                //part3.node1 will be the tail and we are adding tail + root to make it circular
                //no worries somewhere in the below root will be added before tail.and will make  it as   root + tail + root
                //For eg  think of this ////5////
                ////////////////////////////////6//
                ////////////////////////////////////7
                //for R(5)  part1 = null and part3 = 6 <-> 7 and circular
                //6,7, 5
                Concat(part3.Node1, root); 
            }
            else
            {
                //if part1 is not null
                //part1.node1 will be the tail of part1 bcoz of circular nature  
                //add the tail of first partition to the root 
                Concat(part1.Node1, root); //straight forward    
            }

            //note not null has logic and the null code is to make circular
            /* join the right to root to form this fashion           //(Head to tail) + root + (head to tail) */
            if (part3 == null)
            {
                Concat(root, part1);

            }
            else
            {
                //(Head to tail) + root + (head to tail)
                Concat(root, part3);
            }


            /* join right to left */
            ///we did the circular logic for null if both are not null..make it circular here
            if (part1 != null && part3 != null)
            {
                 Concat(tail3, part1);
            }

            //return the head of the circular linked list
            return part1 == null ? root : part1;

        }



    }
}
