using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{

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
            }
            else
            {
                //move toward right children
                KarthicBTNode<int> node = Insert(value, runner.Right);
                node.Parent = runner;
                runner.Right = node;
            }

            Size++;

            return runner;
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
            size =  InOrderTraversalGetSize(current.Left, size);

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

        public bool IsBSTOptimalSolution(KarthicBTNode<int> current, int min, int max)
        {


            //we can use any traversal.. i am using  in-order
            //base case
            if (current == null)
            {
                return true;
            }

            //current data should be always greater than or equal min

            if (current.Data <= min || current.Data > max)
            {
                return false;
            }

            //For left children Min value is Parent's Min value (excluding) and Max value is parent.Data (including..can be equal)
            if (IsBSTOptimalSolution(current.Left, min, current.Data) == false)
            {
                return false;
            }

            //For right children Min value should be greater than parent.Data and Max value is parent's max (
            if (IsBSTOptimalSolution(current.Right, current.Data, max) == false)
            {
                return false;
            }


            return true;

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
            //This will return null if both node1 and node2 are not in the root
            //This will return node1 if only node1 exists in the root's subtree
            //This will return node2 if only node2 exists in the root's subtree
            //This will return commonAncestor if node1 and node2 has commmonAncestor (bubble up)

            //Constrain
            //We got to make sure node1 and node2 is present in the tree al least for the first master method

            if (root == null)
            {
                return null;
            }

            if (root == node1 || root == node2)
            {
                return root;
            }

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
    }
}
