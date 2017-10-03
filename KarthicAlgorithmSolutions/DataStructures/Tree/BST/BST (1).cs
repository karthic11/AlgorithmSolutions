using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    //Time Complexity of Traversal: O(n)
    public class KarthicBST<T>
    {

        public KarthicBTNode<T> Root { get; set; }

        public int Size { get; set; }

        public static int arrayindex = 0;

        public StringBuilder sb = null;

        public KarthicBST()
        {
            Root = null;
            sb = new StringBuilder();

        }


        public KarthicBST(KarthicBTNode<T> rootnode)
        {
            this.Root = rootnode;
        }

        //Pre-Order Traversal
        public string PreOrderTraversal(KarthicBTNode<int> currentnode)
        {

            //Base case
            if (currentnode == null)
            {
                return sb.ToString();
            }

            //print current node
            sb.Append(currentnode.Data).Append(',');

            //Traverse on left childresn

            PreOrderTraversal(currentnode.Left);

            //Traverse on right childrens

            PreOrderTraversal(currentnode.Right);

            return sb.ToString();

        }


        public void InOrderTraversal(KarthicBTNode<int> currentnode, ref StringBuilder sb)
        {
            //base case
            if (currentnode != null)
            {
                //Left childrens

                InOrderTraversal(currentnode.Left, ref sb);

                //Current node
                sb.Append(currentnode.Data).Append(',');

                //right childrens

                InOrderTraversal(currentnode.Right, ref sb);

            }

        }


        public void PostOrderTraversal(KarthicBTNode<int> currentnode, ref StringBuilder sb)
        {
            //base case
            if (currentnode != null)
            {
                //Left childrens
                PostOrderTraversal(currentnode.Left, ref sb);

                //right childrens

                PostOrderTraversal(currentnode.Right, ref sb);

                //Current node
                sb.Append(currentnode.Data).Append(',');

            }

        }


        //Search the node by iteration
        public KarthicBTNode<int> Find(int value, KarthicBTNode<int> root)
        {
            KarthicBTNode<int> runner = root;

            while (runner.Data != value)
            {

                if (value < runner.Data)
                {
                    runner = runner.Left;
                }
                else
                {
                    runner = runner.Right;
                }
                //check for null..if not null proceed with the iteration
                if (runner == null)
                {
                    return null;
                }

            }

            return runner;

        }


        //search the node by recurssion
        public KarthicBTNode<int> Find2(int value, KarthicBTNode<int> current)
        {
            //base case
            if (current == null)
            {
                return null;
            }
            else if (current.Data == value)
            {
                return current;
            }
            else if (value < current.Data)
            {
                return Find2(value, current.Left);
            }
            else
            {
                return Find2(value, current.Right);
            }

        }


        //public KarthicBTNode<int> Insert(int value)
        //{
        //    return Insert(value, this.Root);
        //}


        public KarthicBTNode<int> Insert(int value, KarthicBTNode<int> runner)
        {
            //base case
            if (runner == null)
            {
                return new KarthicBTNode<int>(value);
            }

            if (value == runner.Data)
            {
                throw new Exception("Data already exists");

            }
            else if (value < runner.Data)
            {
                //move toward left childrens
                KarthicBTNode<int> node = Insert(value, runner.Left);
                node.Parent = runner;
                runner.Left = node;

                //this is custom log for a problem
                runner.LeftChilds++;


            }
            else
            {
                //move toward right children
                KarthicBTNode<int> node = Insert(value, runner.Right);
                node.Parent = runner;
                runner.Right = node;

                //this is custom logic for a problem
                runner.RightChilds++;

            }

            Size++;

            return runner;
        }

        public KarthicBTNode<int> Delete(int value, KarthicBTNode<int> runner)
        {
            //base 
            if (runner == null)
            {
                return runner;
            }

            if (value < runner.Data)
            {
                //search in left
                KarthicBTNode<int> node = Delete(value, runner.Left);
                runner.Left = node;
                return runner;

            }
            else if (value > runner.Data)
            {
                //search in right
                KarthicBTNode<int> node = Delete(value, runner.Right);
                runner.Right = node;
                return runner;

            }
            else
            {
                //when the value is equal to runner.Data
                //this is the node to be deleted
                //check if left is null
                if (runner.Left == null)
                {
                    //copy the right to runner
                    KarthicBTNode<int> tempnode = runner.Right;
                    return tempnode;
                }
                else if (runner.Right == null)
                {
                    KarthicBTNode<int> tempnode = runner.Left;
                    return tempnode;
                }
                //when both left and right are not null

                //We can do either inorder successor or predecessor 
                //I will go with inorder successor
                KarthicBTNode<int> minnode = LeftMostNodeofSubtree(runner.Right);
                runner.Data = minnode.Data;
                runner.Right = Delete(minnode.Data, runner.Right);
                return runner;

            }
        }

        public int GetSize()
        {

            return InOrderTraversalGetSize(this.Root, 0);

        }

        public int GetHeight(KarthicBTNode<int> node)
        {
            //Height of a node (including root node) is Max of Height of childrens (left and right) + 1

            //Base case for recurrsion
            if (node == null)
            {
                return 0;
            }

            int LeftNodeHeight = GetHeight(node.Left);
            int RightNodeHeight = GetHeight(node.Right);

            int height = Math.Max(LeftNodeHeight, RightNodeHeight) + 1;

            return height;

        }


        public bool IsBinarySearchTree()
        {

            int[] array = new int[this.GetSize()]; //we need to get the size of the tree here
            InOrderTraversalResult(this.Root, array);

            //check the array is sorted or not

            return CheckArraySorted(array);

        }


        public bool CheckArraySorted(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1])
                {
                    return false;

                }
            }

            return true;

        }


        public void InOrderTraversalResult(KarthicBTNode<T> current, int[] array)
        {

            //base case
            if (current == null)
            {
                return;
            }

            //left
            InOrderTraversalResult(current.Left, array);

            //current
            array[arrayindex] = Convert.ToInt32(current.Data);
            arrayindex++;

            //right
            InOrderTraversalResult(current.Right, array);

        }

        public int InOrderTraversalGetSize(KarthicBTNode<T> current, int size)
        {

            //base case
            if (current == null)
            {
                return size;
            }

            //left
            size = InOrderTraversalGetSize(current.Left, size);

            //current
            size++;

            //right
            size = InOrderTraversalGetSize(current.Right, size);

            return size;

        }

        public bool IsBSTOptimalSolution(KarthicBTNode<int> root)
        {

            return IsBSTOptimalSolution(root, Int32.MinValue, Int32.MaxValue);


        }

        //we can use any traversal.. i am using  in-order
        public bool IsBSTOptimalSolution(KarthicBTNode<int> current, int min, int max)
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


            //For left children Min value is Parent's Min value and Max value is parent.Data (Excluding)
            //For right children Min value should be greater than parent.Data and Max value is parent's max 
            // Allow only distinct values

            return IsBSTOptimalSolution(current.Left, min, current.Data - 1)
                      && IsBSTOptimalSolution(current.Right, current.Data + 1, max);

            //if (IsBSTOptimalSolution(current.Left, min, current.Data) == false)
            //{
            //    return false;
            //}


            //if (IsBSTOptimalSolution(current.Right, current.Data, max) == false)
            //{
            //    return false;
            //}


            //return true;

        }


        public KarthicBTNode<int> NextInOrderSuccessor(KarthicBTNode<int> node)
        {

            //pseudocode
            // public Node Inordersuccessor(node)
            ///   if(node has right subtree)
            ///     return (left most node of right subtree)
            ///   else 
            ///     while (n is right of n.parent)
            ///            n = n.parent
            ///       return n.parent
            ///   

            if (node == null)
            {
                return node;
            }

            if (node.Right != null)
            {
                return LeftMostNodeofSubtree(node.Right);
            }
            else
            {
                KarthicBTNode<int> current = node;
                KarthicBTNode<int> parent = node.Parent;
                while (parent != null && parent.Left != current)
                //while (parent != null && parent.Right == current)
                {
                    //This code executes untill the parent is null or current becomes left children of parent
                    current = parent;
                    parent = current.Parent;

                }

                return parent;

            }



        }

        public KarthicBTNode<int> LeftMostNodeofSubtree(KarthicBTNode<int> node)
        {
            if (node == null)
            {
                return node;
            }


            while (node.Left != null)
            {
                //This code execute untill the node.left is null
                node = node.Left;
            }

            return node;
        }


        public KarthicBTNode<int> FindFirstCommonAncestor(KarthicBTNode<int> root, KarthicBTNode<int> node1, KarthicBTNode<int> node2)
        {
            //Check whether both node1 and node2 exsits in root
            if ((!FindDescendants(root, node1)) || (!FindDescendants(root, node2)))
            {
                throw new Exception("node 1 or 2 does not exists");
            }

            return FindFirstCommonAncestorHelper(root, node1, node2);


        }

        public KarthicBTNode<int> FindFirstCommonAncestorHelper(KarthicBTNode<int> root, KarthicBTNode<int> node1, KarthicBTNode<int> node2)
        {

            //Logic to find common ancestors
            //Check if the node1 and node2 is on the same side (left or right) of the root..
            //If they are on the different side then the root is the common ancestor...If not recurse till we get this condition or till we meet the end base

            if (root == null)
            {
                return null;
            }
            //we already checked whether node1 and node2 exists on the root but haven't checked it is equal to root
            if (root == node1 || root == node2)
            {
                return root;

            }

            bool IsNode1OnLeftSide = FindDescendants(root.Left, node1);
            bool IsNode2OnLeftSide = FindDescendants(root.Left, node2);

            //if they are on both sides..we are done ..just return the root
            if (IsNode1OnLeftSide != IsNode2OnLeftSide)
            {
                return root;
            }

            //if the code comes here means they are on the same side..get the side and continue the recurse

            KarthicBTNode<int> childnode = IsNode1OnLeftSide ? root.Left : root.Right;

            return FindFirstCommonAncestorHelper(childnode, node1, node2);

        }

        public bool FindDescendants(KarthicBTNode<int> runner, KarthicBTNode<int> node)
        {
            //base case
            if (runner == null || node == null)
            {
                return false;
            }

            //check for current 
            if (runner == node)
            {
                return true;
            }

            //This code is equivalent to the below lines
            //return FindDescendants(runner.Left, node) || FindDescendants(runner.Right, node);

            if (FindDescendants(runner.Left, node))
            {
                return true;
            }

            if (FindDescendants(runner.Right, node))
            {
                return true;
            }


            return false;


        }


        public KarthicBTNode<int> FindFirstCommonAncestorBetter(KarthicBTNode<int> root, KarthicBTNode<int> node1, KarthicBTNode<int> node2)
        {
            //This method takes root, node1 and node2
            //This will return null if both node1 and node2 are not in the root ..note here retun null only both are absent in the root..
            //This will return node1 if only node1 exists in the root's subtree
            //This will return node2 if only node2 exists in the root's subtree
            //This will return commonAncestor if node1 and node2 has commmonAncestor (bubble up)

            //Constrain..or Assumptions:
            //There is special case where this code won't work. if node1 is in the tree and and node2 is not..this will fail
            //so we need to make sure both node1 and node2 is present in the tree
            //This is based on the assumption the tree doesn't have duplicates and valid eg What if  node1 is present in both root.left and root.right

            if (root == null)
            {
                return null;
            }

            if (root == node1 || root == node2)
            {
                return root;
            }

            //this should be called as nodeleftresult
            KarthicBTNode<int> node1result = FindFirstCommonAncestorBetter(root.Left, node1, node2);
            //possible result = null, node1, node2, CommonAncestor (this might be root or new ancestor node)
            if (node1result != null && node1result != node1 && node1result != node2)
            {
                //This mean that common ancestor is already found..so return no need to execute the below code for this function
                return node1result;
            }

            KarthicBTNode<int> node2result = FindFirstCommonAncestorBetter(root.Right, node1, node2);
            //possible result = null, node1, node2, CommonAncestor (this might be root or new ancestor node)
            if (node2result != null && node2result != node1 && node2result != node2)
            {
                //This mean that common ancestor is already found..so return no need to execute the below code for this function
                return node2result;
            }

            //If the code come here then that means common ancestor is not yet found return the right value 
            //possible values node1result = null node2result = node1, node1result = null node2result = node2, node1result = node1 node2result = null
            if (node1result != null && node2result != null)
            {
                //node1result and node2result are found in different subtrees that is node1result = node1 and node2result = node2 or node2result = node1 and node1result = node2
                return root;

            }
            //else if (root == node1 || root == node2)
            //{
            //    return root;
            //}
            else if (node1result == null && node2result == null)
            {
                return null;
            }
            else
            {
                //If either node1result or node2result in non-null return that
                return (node1result != null) ? node1result : node2result;
            }
        }

        //Using Morris Traversal, we can traverse the tree without using stack and recursion. The idea of Morris Traversal is based on Threaded Binary Tree.
        //In this traversal, we first create links to Inorder successor and print the data using these links,
        //    and finally revert the changes to restore original tree.
        //Note: we change the structure of the tree and then reverse it back


        //Logic:
        // 1. Initialize current as root 
        //  2. While current is not NULL
        //  If current does not have left child
        //     a) Print current’s data
        //     b) Go to the right, i.e., current = current->right
        //  Else
        //     a) Get the predessor of the current node
             //b) Make current as right child of the rightmost node in current's left subtree (predessor)
        //     c)  Go to this left child, i.e., current = current->left
        //     b)But if the right child of predessor is current then print current and set current = current.right
        public void MorrisInOrderTraversal(KarthicBTNode<int> root, StringBuilder sb)
        {
            KarthicBTNode<int> current = null;
            KarthicBTNode<int> predecesser = null;
            current = root;

            if (current == null)
            {
                return;
            }

            while (current != null)
            {
                if (current.Left == null)
                {
                    sb.Append(current.Data).Append(",");
                    current = current.Right;
                }
                else
                {
                    /* Find the inorder predecessor of current */
                    predecesser = current.Left;

                    while (predecesser.Right != null && predecesser.Right != current)
                    {
                        predecesser = predecesser.Right;
                    }

                    /* Make current as right child of its inorder predecessor */
                    if (predecesser.Right == null)
                    {
                        predecesser.Right = current;
                        current = current.Left;
                    }
                    else
                    {
                        /* Revert the changes made in if part to restore the original 
                         tree i.e., fix the right child of predecssor */

                        predecesser.Right = null;
                        sb.Append(current.Data).Append(",");
                        current = current.Right;
                    }
                }
            }
        }


        /*  refer gayle new edition oag 258...not time now
         * 
         * Time 0(t) where t is size of the substree in this common ancestor worst case 0(n)
         
         */

        public KarthicBTNode<int>  FindFirstCommonAncestorUsingParent(KarthicBTNode<int> root, KarthicBTNode<int> node1, KarthicBTNode<int> node2)
        {

            if (root == null)
            {
                return null;
            }

            //make sure both node1 and node2 exsits in root
            if (FindDescendants(root, node1) == false || FindDescendants(root, node2) == false)
            {
                return null;
            }

            if(FindDescendants(node1, node2))
            {
                return node1;//node1 contains node2
            }
             
            if (FindDescendants(node2, node1))
            {
                return node2;
            }

            KarthicBTNode<int> sibling = FindSibling(node1);
            KarthicBTNode<int> parent = node1.Parent;
            //if node1 and node2 is missing in root mean this code will run indefinit but here we alredy check that root has node1 and node2
            while (FindDescendants(sibling, node2) == false)
            {
                sibling = FindSibling(parent);
                parent = parent.Parent;
            }
            //when code come here means we found descendant
            return parent;

        }

        //you have access to parent node..find the sibling of a node
        public KarthicBTNode<int> FindSibling(KarthicBTNode<int> node)
        {
            KarthicBTNode<int> parent = node.Parent;
            return parent.Left == node ? parent.Right : parent.Left;

        }

              /* Function to find LCA of n1 and n2. The function assumes that both
   n1 and n2 are present in BST */
         public KarthicBTNode<int> FindFirstCommonAncestorForOnlyBST(KarthicBTNode<int> root, KarthicBTNode<int> node1, KarthicBTNode<int> node2)
        {
             if(root == null)
             {
                 return null;
             }
              // If both n1 and n2 are smaller than root, then LCA lies in left
             if(node1.Data < root.Data && node2.Data < root.Data)
             {
                 return FindFirstCommonAncestorForOnlyBST(root.Left, node1, node2);
             }
                   // If both n1 and n2 are greater than root, then LCA lies in right
             else if(node1.Data > root.Data && node2.Data > root.Data)
             {
                   return FindFirstCommonAncestorForOnlyBST(root.Right, node1, node2);
             }
             else
             {
                 //if root lies between node1 and node2
                 //or if root == node1 or root == node2
                 return root;
             }
         }
  
    }
}
