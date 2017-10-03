using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public class KarthicBTNode<T>
    {

        public T Data { get; set; }
        public KarthicBTNode<T> Left, Right, Parent;

        //custom prop these two pro is to solve a problem
        public int LeftChilds  { get; set; }
        public int RightChilds { get; set; }


        public KarthicBTNode()
        {
        }

        public KarthicBTNode(T value)
        {
            this.Data = value;
            Left = null;
            Right = null;
            Parent = null;
             
        }

        public KarthicBTNode(T value, KarthicBTNode<T> left, KarthicBTNode<T> right)
        {
            this.Data = value;
            this.Left = left;
            this.Right = right;
        }

    }


    public class KarthicBinaryTree<T>
    {

        public KarthicBTNode<T> Root { get; set; }

        public StringBuilder sb = null;

        public KarthicBinaryTree()
        {
            Root = null;
            sb = new StringBuilder();
        }


        public KarthicBinaryTree(KarthicBTNode<T> rootnode)
        {
            this.Root = rootnode;
        }


        public string PreOrderTraversal(KarthicBTNode<int> currentnode, bool InsertZerofornull)
        {

            //Base case
            if (currentnode == null)
            {
                if (InsertZerofornull)
                {
                    sb.Append(0).Append(',');
                }
                return sb.ToString();
            }

            //print current node
            sb.Append(currentnode.Data).Append(',');

            //Traverse on left childresn

            PreOrderTraversal(currentnode.Left, true);

            //Traverse on right childrens

            PreOrderTraversal(currentnode.Right, true);

            return sb.ToString();

        }


        public void InOrderTraversal(KarthicBTNode<int> currentnode, ref StringBuilder sb, bool InsertZerofornull)
        {
            //base case
            if (currentnode == null)
            {
                if (InsertZerofornull)
                {
                    sb.Append(0).Append(',');
                }
                return;
            }
         
                //Left childrens

                InOrderTraversal(currentnode.Left, ref sb, true);

                //Current node
                sb.Append(currentnode.Data).Append(',');

                //right childrens

                InOrderTraversal(currentnode.Right, ref sb, true);

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

        // http://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion/
        //1) Create an empty stack S.
       //2) Initialize current node as root
       //3) Push the current node to S and set current = current->left until current is NULL
       //4) If current is NULL and stack is not empty then 
          //     a) Pop the top item from stack.
          //     b) Print the popped item, set current = popped_item->right 
          //     c) Go to step 3.
       //5) If current is NULL and stack is empty then we are done. 
        public void InOrderTraversalUsingStack(KarthicBTNode<int> currentnode, ref StringBuilder sb)
        {
            Stack<KarthicBTNode<int>> mystack = new Stack<KarthicBTNode<int>>();
            KarthicBTNode<int> node = currentnode;

            while (node != null || mystack.Count != 0)
            {
                while (node != null)
                {
                    mystack.Push(node);
                    node = node.Left;
                }

                KarthicBTNode<int> parent = mystack.Pop();
                sb.Append(parent.Data);
                node = parent.Right;
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


        public void LevelTraversal(KarthicBTNode<int> currentnode, StringBuilder sb)
        {
          Queue<KarthicBTNode<int>> myqueue = new Queue<KarthicBTNode<int>>();
          //root
          sb.Append(currentnode.Data).Append(",");
          myqueue.Enqueue(currentnode);
          while (myqueue.Count != 0)
          {
            KarthicBTNode<int> node = myqueue.Dequeue();

            if (node.Left != null)
            {
              sb.Append(node.Left.Data).Append(",");
              myqueue.Enqueue(node.Left);
            }

            if (node.Right != null)
            {
              sb.Append(node.Right.Data).Append(",");
              myqueue.Enqueue(node.Right);

            }
         
          }

        }

        public void LevelZigZacTraversal(KarthicBTNode<int> currentnode, StringBuilder sb)
        {

          //Logic: use two stack to make zigzac
          //stack1 - will contain nodes from one direction (left to right)
          //stack2 - will contain nodes from the opposite direction (right to left)


          Stack<KarthicBTNode<int>> stack1 = new Stack<KarthicBTNode<int>>();
          Stack<KarthicBTNode<int>> stack2 = new Stack<KarthicBTNode<int>>();
          KarthicBTNode<int> current1 = new KarthicBTNode<int>();
          KarthicBTNode<int> current2 = new KarthicBTNode<int>();
          stack1.Push(currentnode);
        

          while (stack1.Count != 0 || stack2.Count != 0)
          {

            while (stack1.Count != 0)
            {
                current1 = stack1.Pop();
                sb.Append(current1.Data).Append(",");

              if (current1.Left != null)
              {
                 stack2.Push(current1.Left);
              }
              if (current1.Right != null)
              {
                  stack2.Push(current1.Right);
              }
            }

            while (stack2.Count != 0)
            {
              current2 = stack2.Pop();
              sb.Append(current2.Data).Append(",");

                //important visit right children and then left so that we maintain the reverse order

              if (current2.Right != null)
              {
                stack1.Push(current2.Right);
              }
              if (current2.Left != null)
              {
                stack1.Push(current2.Left);
              }
            }

          }




        }


      //Search the node by iteration


        public KarthicBTNode<int> Search(int value, KarthicBTNode<int> root) 
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


        //Add node to the binary search tree ..see insert node in bst tree

        public void AddNode(KarthicBTNode<T> node)
        {



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

            bool IsNode1OnLeftSide = FindDescendants(root, node1);
            bool IsNode2OnLeftSide = FindDescendants(root, node2);

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


        public int GetDepth(KarthicBTNode<int> node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Math.Max(GetDepth(node.Left), GetDepth(node.Right));
        }
            
    }



}
