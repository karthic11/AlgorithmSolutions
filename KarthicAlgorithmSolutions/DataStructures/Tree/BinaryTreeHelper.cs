using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public static class TreeHelper
    {

        public static int GetHeight(KarthicBTNode<int> node)
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

        public static bool IsBalanced(KarthicBTNode<int> node)
        {
            //For any node in the tree, the difference btw the height of left and height of right should not be greater than 1..
            //If it greater than 1 it is not balanced tree else it is balanced tree

            //Base case 
            if(node == null)
            {
                return true;
            }

            int leftheight = GetHeight(node.Left);
            int rightheight = GetHeight(node.Right);

            int difference = Math.Abs(leftheight - rightheight);

            if(difference > 1)
            {
                return false;
            }
            else
            {
                return IsBalanced(node.Left) && IsBalanced(node.Right);

            }

        }

        //If tree is unbalaced returns -1 else if balanced returns the actual height
        //
        public static int GetHeightAndBalaced(KarthicBTNode<int> node)
        {
            //Height of a node (including root node) is Max of Height of childrens (left and right) + 1

            //Base case for recurrsion
            if (node == null)
            {
                return 0;
            }

            int LeftNodeHeight = GetHeightAndBalaced(node.Left);
            //break if unbalanced
            if (LeftNodeHeight == -1)
            {
                return -1;
            }

            int RightNodeHeight = GetHeightAndBalaced(node.Right);
            //break if unbalanced
            if (RightNodeHeight == -1)
            {
                return -1;
            }

             //check for balanced

            if (Math.Abs(LeftNodeHeight - RightNodeHeight) > 1)
            {
                return -1;
              
            }
            else
            {
                return Math.Max(LeftNodeHeight, RightNodeHeight) + 1;
            }

        }


        public static KarthicBinaryTree<int> SetUpBinaryTree()
        {
            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();
            bt.Root = new KarthicBTNode<int>(1);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(2);
            bt.Root.Right = new KarthicBTNode<int>(3);

            //set level 2

            bt.Root.Left.Left = new KarthicBTNode<int>(4);
            bt.Root.Left.Right = new KarthicBTNode<int>(5);
            bt.Root.Right.Left = new KarthicBTNode<int>(6);
            bt.Root.Right.Right = new KarthicBTNode<int>(7);

            //set level 3

            //bt.Root.Left.Left.Left = new KarthicBTNode<int>(8);

            ////making the tree unbalaced
            //bt.Root.Left.Left.Left.Left = new KarthicBTNode<int>(9);

            return bt;
        }

        public static KarthicBinaryTree<int> SetupBinarySubTree()
        {
            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();
            bt.Root = new KarthicBTNode<int>(3);

            bt.Root.Left = new KarthicBTNode<int>(6);
            bt.Root.Right = new KarthicBTNode<int>(7);

            return bt;

        }

        public static KarthicBST<int> SetUpBinarySearchTree()
        {
            KarthicBST<int> btree = new KarthicBST<int>();
            btree.Root = new KarthicBTNode<int>(8);
            btree.Insert(3, btree.Root);
            btree.Insert(10, btree.Root);
            btree.Insert(1, btree.Root);
            btree.Insert(6, btree.Root);
            btree.Insert(4, btree.Root);
            btree.Insert(7, btree.Root);
            btree.Insert(14, btree.Root);
            btree.Insert(13, btree.Root);
          
            //bt.Root = new KarthicBTNode<int>(8);

            ////set level 1
            //bt.Root.Left = new KarthicBTNode<int>(3);
            //bt.Root.Right = new KarthicBTNode<int>(10);

            ////set level 2
            //bt.Root.Left.Left = new KarthicBTNode<int>(1);

            //bt.Root.Left.Right = new KarthicBTNode<int>(6);
            //bt.Root.Left.Right.Left = new KarthicBTNode<int>(4);
            //bt.Root.Left.Right.Right = new KarthicBTNode<int>(7);

            //bt.Root.Right.Right = new KarthicBTNode<int>(14);
            //bt.Root.Right.Right.Left = new KarthicBTNode<int>(13);
            ////making the tree unbalaced
            //bt.Root.Left.Left.Left.Left = new KarthicBTNode<int>(9);

            return btree;
        }

        public static KarthicBST<int> SetUpNonBinarySearchTree()
        {
            KarthicBST<int> bt = new KarthicBST<int>();
            bt.Root = new KarthicBTNode<int>(8);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(3);
            bt.Root.Right = new KarthicBTNode<int>(10);

            //set level 2
            bt.Root.Left.Left = new KarthicBTNode<int>(1);

            bt.Root.Left.Right = new KarthicBTNode<int>(6);
            bt.Root.Left.Right.Left = new KarthicBTNode<int>(4);
            bt.Root.Left.Right.Right = new KarthicBTNode<int>(7);

            bt.Root.Right.Right = new KarthicBTNode<int>(14);
            bt.Root.Right.Right.Left = new KarthicBTNode<int>(13);

            //making non binary tree
            bt.Root.Right.Right.Left.Right = new KarthicBTNode<int>(11);

            ////making the tree unbalaced
            //bt.Root.Left.Left.Left.Left = new KarthicBTNode<int>(9);
            //return bt;

            //custom problem
            KarthicBST<int> bt2 = new KarthicBST<int>();
            bt2.Root = new KarthicBTNode<int>(8);

            //set level 1
            bt2.Root.Left = new KarthicBTNode<int>(3);
            bt2.Root.Right = new KarthicBTNode<int>(10);

            //set level 2
            bt2.Root.Left.Left = new KarthicBTNode<int>(2);

            bt2.Root.Left.Right = new KarthicBTNode<int>(6);

            return bt2;
        }


        public static void SetUpGraph(KarthicGraph<int> graph)
        {


            GraphNode<int> node1 = new GraphNode<int>(0);
            GraphNode<int> node2 = new GraphNode<int>(1);
            GraphNode<int> node3 = new GraphNode<int>(2);
            GraphNode<int> node4 = new GraphNode<int>(3);
            GraphNode<int> node5 = new GraphNode<int>(4);
            GraphNode<int> node6 = new GraphNode<int>(5);
            GraphNode<int> node7 = new GraphNode<int>(6);

            //Add vertices
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);
            graph.AddNode(node6);
            graph.AddNode(node7);

            //Add edges

            graph.AddDirectedEdge(node1, node2);
            graph.AddDirectedEdge(node1, node3);
            graph.AddDirectedEdge(node1, node4);

            graph.AddDirectedEdge(node2, node5);
            graph.AddDirectedEdge(node5, node7);

            graph.AddDirectedEdge(node4, node6);
            //graph.AddDirectedEdge(new GraphNode<int>(4), new GraphNode<int>(6));
           
        }

        public static void SetUpUnDirectedGraph(KarthicGraph<int> graph)
        {


            GraphNode<int> node1 = new GraphNode<int>(0);
            GraphNode<int> node2 = new GraphNode<int>(1);
            GraphNode<int> node3 = new GraphNode<int>(2);
            GraphNode<int> node4 = new GraphNode<int>(3);
            GraphNode<int> node5 = new GraphNode<int>(4);
            GraphNode<int> node6 = new GraphNode<int>(5);
            GraphNode<int> node7 = new GraphNode<int>(6);

            //Add vertices
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);
            graph.AddNode(node6);
            graph.AddNode(node7);

            //Add edges
            graph.AddUndirectedEdge(node1, node2);
            graph.AddUndirectedEdge(node1, node3);
            graph.AddUndirectedEdge(node1, node4);

            graph.AddUndirectedEdge(node2, node5);
            graph.AddUndirectedEdge(node5, node7);

            graph.AddUndirectedEdge(node4, node6);
            //graph.AddDirectedEdge(new GraphNode<int>(4), new GraphNode<int>(6));

        }


        public static KarthicAryTree SetUpAryTreeWithThreeChildren()
        {

          KarthicAryTree tree = new KarthicAryTree();

          KarthicAryTreeNode node1 = new KarthicAryTreeNode(1);
          KarthicAryTreeNode node2 = new KarthicAryTreeNode(2);
          KarthicAryTreeNode node3 = new KarthicAryTreeNode(3);
          KarthicAryTreeNode node4 = new KarthicAryTreeNode(4);
          KarthicAryTreeNode node5 = new KarthicAryTreeNode(5);
          KarthicAryTreeNode node6 = new KarthicAryTreeNode(6);
          KarthicAryTreeNode node7 = new KarthicAryTreeNode(7);
          KarthicAryTreeNode node8 = new KarthicAryTreeNode(8);
          KarthicAryTreeNode node9 = new KarthicAryTreeNode(9);

          tree.root = node1;
          node1.Children.Add(node2);
          node1.Children.Add(node3);
          node1.Children.Add(node4);

          //level 2
          node2.Children.Add(node5);
          node2.Children.Add(node6);

          node4.Children.Add(node7);
          node4.Children.Add(node8);
          node4.Children.Add(node9);

          return tree;

     
        }

        public static AryTree<char> SetAryTreewithMaxFourchildrens()
        {



            AryTree<char> tree = new AryTree<char>();

            AryTreeNode<char> node1 = new AryTreeNode<char>('A');
            AryTreeNode<char> node2 = new AryTreeNode<char>('B');
            AryTreeNode<char> node3 = new AryTreeNode<char>('C');
            AryTreeNode<char> node4 = new AryTreeNode<char>('D');
            AryTreeNode<char> node5 = new AryTreeNode<char>('E');
            AryTreeNode<char> node6 = new AryTreeNode<char>('F');
            AryTreeNode<char> node7 = new AryTreeNode<char>('G');
            AryTreeNode<char> node8 = new AryTreeNode<char>('H');
            AryTreeNode<char> node9 = new AryTreeNode<char>('I');

            AryTreeNode<char> node10 = new AryTreeNode<char>('J');
            AryTreeNode<char> node11 = new AryTreeNode<char>('K');


            //Lets buid a n-ary tree
            ///////////////////A
            ///////B///////////C//////////////////D
            ///E//////F//////////////////////G///H////I///J
            /////////K/////////////////////////////////////
            tree.root = node1;
            node1.Children.Add(node2);
            node1.Children.Add(node3);
            node1.Children.Add(node4);

            node2.Children.Add(node5);
            node2.Children.Add(node6);

            node4.Children.Add(node7);
            node4.Children.Add(node8);
            node4.Children.Add(node9);
            node4.Children.Add(node10);

            node6.Children.Add(node11);

            return tree;
      

        }


        

    }
}
