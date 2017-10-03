using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.BST
{
    public class BinarySearchTree
    {
        public Node Root { get; set; }
        public BinarySearchTree()
        {
            this.Root = null;
        }
        //We will have methods for Insertion, Deletion, Search and Iteration (Pre-Order, Post-Order and In-Order)

        public Node Insert(int value, Node runner)
        {

            return null;
        }

   
        //Logic:
        //There are two methods that I can think to find either the binary tree is Binary Search Tree
        //Method 1: Travese the tree via any traversal (i am using  In-order) and using Min and Max to track each level
        //Method 2:
        //To check the given tree is bst, If we use in-order sort we will get all the data in ascending order..
        //put the result in an array and check whether the array is sorted..
        //limitation: This won't work for duplicate records example below


        //Time Complexity: O(n)
        //Auxiliary Space : O(1) if Function Call Stack size is not considered, otherwise O(n)

        //Logic:
        //I'm using method 1
        //This function checks whether the given root Node of the binary tree is Binary Search Tree or not
        //returns 'true' if it is BST 
        //return 'false' if it is not BST 



        public bool IsBST(Node root)
        {
            return IsBST(root, Int32.MinValue, Int32.MaxValue);
        }

        //We can give another function name but I like to give this method same name as previous (bcoz this function does the same purpose) but we have different parameter (overloading)
        public bool IsBST(Node current, int min, int max)
        {

            //base case
            if (current == null)
            {
                return true;
            }

            if (current.Data < min || current.Data > max)
            {
                return false;
            }

            //For left children Min value is Parent's Min value (excluding) and Max value is parent.Data - 1 (we are doing -1 is to make the value distinct)
            //For right children Min value should be greater than parent.Data and Max value is parent's max 
            // Allow only distinct values
            return IsBST(current.Left, min, current.Data - 1) && IsBST(current.Right, current.Data + 1, max);


        }



    }

    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
        }
        public Node(int key)
        {
            this.Data = key;
            Left = null;
            Right = null;
        }
    }

}
