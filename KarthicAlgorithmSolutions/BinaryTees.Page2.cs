using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Puzzles
{
    public partial class BinaryTees : Form
    {
        public BinaryTees()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            //set tree with the following nodes

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

            bt.Root.Left.Left.Left = new KarthicBTNode<int>(8);

            //making the tree unbalaced
            bt.Root.Left.Left.Left.Left = new KarthicBTNode<int>(9);

            //

            //In this problem, they have given the defenition of balanced tree..
            //For any node in the tree, the difference btw the height of left and height of right should not be greater than 1..
            //If it greater than 1 it is not balanced tree else it is balanced tree

            bool result = TreeHelper.IsBalanced(bt.Root);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //set tree with the following nodes

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

            bt.Root.Left.Left.Left = new KarthicBTNode<int>(8);

            //making the tree unbalaced
            bt.Root.Left.Left.Left.Left = new KarthicBTNode<int>(9);

            //If tree is unbalaced returns -1 else if balanced returns the actual height
            int result = TreeHelper.GetHeightAndBalaced(bt.Root);

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            KarthicGraph<int> graph = new KarthicGraph<int>();
            TreeHelper.SetUpUnDirectedGraph(graph);
            GraphNode<int> startnode = graph.FindNodeByValue(Convert.ToInt16(this.textBox4.Text));
            GraphNode<int> endnode = graph.FindNodeByValue(Convert.ToInt16(this.textBox1.Text));

            bool result = graph.IsRouteExists(startnode, endnode);

            this.textBox2.Text = (result == true) ? "Route Exists" : "No route exists";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Given an sorted array..we need to create a binary tree with minimal height..
            //we can't add node to binary tree by iterating through array bcoz the height will grow on left or right side

            int[] array = { 11, 22, 33, 44, 55, 66, 77 };

            int length = array.Length;

            KarthicBST<int> tree = new KarthicBST<int>();
            tree.Root = CreateBinarySearchTreeWithMinimalHeight(array);


            int height = tree.GetHeight(tree.Root);

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

             


        }

        //This method returns tree's root node
        public KarthicBTNode<int> CreateBinarySearchTreeWithMinimalHeight(int[] array)
        {

           
            //This will set the root and the root will have all the left and right childrens
            return AddToTreeMinimalHeight(array, 0, array.Length - 1);

        }

        
        //This will first add root node and then add all its left and right childrent by recurssion..
        //At the end of recurssion it will return the root node
        //Important: Base case and calculation of the middle pointer are important
        public KarthicBTNode<int> AddToTreeMinimalHeight(int[] array, int startpointer, int endpointer)
        {
            //In  order to get a tree with minimal height for the given ascending order array
            //1) Take the array and set the middle element of the array as the node
            //2) Take the left of the middle node that is (o to middle-1) as left childrens for this node
            //3) Take the right of the middle node that is (middle+1 to length-1) as the right childresn for this node
            //Recurrse this logic for every node and recurrsion has to stop on the last node 
           

            //Base case.. stop the recurssion if end value is lesser than start value
            if (endpointer < startpointer)
            {
                //Base return
                return null;
            }
            //we can't calculate the middle based on the array length here..bcoz that is constant..only the start and end changes
            //Round the value if the total is odd 7/2 = 4
            int middlepointer = (startpointer + endpointer) / 2;

            //Create node witht the middle element
            KarthicBTNode<int> node = new KarthicBTNode<int>(array[middlepointer]);
            //Input : 11, 22, 33, 44, 55, 66, 77
            //The last of the left children will have 
            //11(node).left = AddToTreeMinimalHeight(array, 0, (0 -1 ));
            //11(node).Right = AddToTreeMinimalHeight(array, 0+1, (0 -1 ));
            //add left children this node
            node.Left = AddToTreeMinimalHeight(array,startpointer, middlepointer - 1);
            //add right children to this node
            node.Right = AddToTreeMinimalHeight(array, middlepointer + 1, endpointer);


            return node;
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //We really don't have to do level by level traversal..all we have to do is traverse by knowing the level during each recurrsion/iteration

            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();
            //List<KarthicLinkedList> colllist = new List<KarthicLinkedList>();
            List<LinkedList<KarthicBTNode<int>>> colllist = new List<LinkedList<KarthicBTNode<int>>>();
            CreateLinkedListForNodes(tree.Root, colllist, 0);
            StringBuilder sb = new StringBuilder();

            foreach (LinkedList<KarthicBTNode<int>> list in colllist)
            {
                if (sb.ToString() != string.Empty)
                {
                    sb.AppendLine();
                }
                foreach (KarthicBTNode<int> value in list)
                {
                    sb.Append(value.Data).Append(',');

                }
            }

            string output = sb.ToString();
       



        }


        private void CreateLinkedListForNodes(KarthicBTNode<int> current, List<LinkedList<KarthicBTNode<int>>> colllist, int level)
        {
            //Base case for recursion
            if (current == null)
            {
                return;
            }
            //we shouldn't create new..If we need new linkedlist add that reference or add to the existing reference
            LinkedList<KarthicBTNode<int>> nodelist = null;
            //if count is equal to level..level are in o based index where size is not.. if count = 0 and level = o we need to add one linkedlist to store the values
            if (colllist.Count == level)
            {
                nodelist = new LinkedList<KarthicBTNode<int>>();
                colllist.Add(nodelist);
            }
            else
            {
                //If the count = 3 and level = 1..we need to get the reference of level 1
                nodelist = colllist[level];
            }

            //Pre-order traversal

            //current
            nodelist.AddLast(current);

            //left children
            CreateLinkedListForNodes(current.Left, colllist, level + 1);

            //right children
            CreateLinkedListForNodes(current.Right, colllist, level + 1);

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //In case of breath first search , we have queue implementaion.. we can make some changes to it
            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();
            //List<KarthicLinkedList> colllist = new List<KarthicLinkedList>();
            List<LinkedList<KarthicBTNode<int>>> colllist = new List<LinkedList<KarthicBTNode<int>>>();
            colllist = CreateLinkedList2(tree.Root);
            StringBuilder sb = new StringBuilder();

            foreach (LinkedList<KarthicBTNode<int>> list in colllist)
            {
                if (sb.ToString() != string.Empty)
                {
                    sb.AppendLine();
                }
                foreach (KarthicBTNode<int> value in list)
                {
                    sb.Append(value.Data).Append(',');

                }
            }

            string output = sb.ToString();
       
        }

        private List<LinkedList<KarthicBTNode<int>>> CreateLinkedList(KarthicBTNode<int> root)
        {
            //This DFS traversal is level by level
            List<LinkedList<KarthicBTNode<int>>> colllist = new List<LinkedList<KarthicBTNode<int>>>();
            LinkedList<KarthicBTNode<int>> currentlist = new LinkedList<KarthicBTNode<int>>();

            if (root != null)
            {
                //add root 
                currentlist.AddLast(root);
            }

            //condition.. The current list is changed on the below statement so iteration continues till the below condition is true
            while (currentlist.Count > 0)
            {

                colllist.Add(currentlist);
                LinkedList<KarthicBTNode<int>> parent = currentlist;
                //Add new list for the next level
                currentlist = new LinkedList<KarthicBTNode<int>>();

                foreach (KarthicBTNode<int> node in parent)
                {

                    if (node.Left != null)
                    {
                        currentlist.AddLast(node.Left);
                    }

                    if (node.Right != null)
                    {
                        currentlist.AddLast(node.Right);
                    }

                }

            }


            return colllist;

        }

        private List<LinkedList<KarthicBTNode<int>>> CreateLinkedList2(KarthicBTNode<int> root)
        {
            //This bFS traversal is level by level
            List<LinkedList<KarthicBTNode<int>>> colllist = new List<LinkedList<KarthicBTNode<int>>>();
            LinkedList<KarthicBTNode<int>> currentlist = new LinkedList<KarthicBTNode<int>>();
            Queue<LinkedList<KarthicBTNode<int>>> queue = new Queue<LinkedList<KarthicBTNode<int>>>();


            if (root != null)
            {
                //add root 
                currentlist.AddLast(root);
                queue.Enqueue(currentlist);

            }

            //condition.. The current list is changed on the below statement so iteration continues till the below condition is true
            while (queue.Count != 0)
            {
                LinkedList<KarthicBTNode<int>> parent = queue.Dequeue();
                colllist.Add(parent);
                currentlist = new LinkedList<KarthicBTNode<int>>();

              //this for each is for the linkedlist to give every node..we can use own linkedlist insteat and use while iteration
               foreach (KarthicBTNode<int> node in parent)
                {

                    if (node.Left != null)
                    {
                        currentlist.AddLast(node.Left);
                    }

                    if (node.Right != null)
                    {
                        currentlist.AddLast(node.Right);
                    }


                }

               if (currentlist.Count > 0)
               {
                   queue.Enqueue(currentlist);
               }

            }


            return colllist;

        }

        private void button6_Click(object sender, EventArgs e)
        {

           // KarthicBST<int> tree = TreeHelper.SetUpNonBinarySearchTree();

           ////bool result =  tree.IsBinarySearchTree(tree.Root);

           ////string output = (result) ? "Binary Tree" : "Not a Binary Tree";

        }

        private void button7_Click(object sender, EventArgs e)
        {

            //To check the given tree is bst, If we use in-order sort we will get all the data in ascending order..
            //put the result in an array and check whether the array is sorted..
            //limitation: This won't work for duplicate records example below
            //************25****
            //***********20*****
            //*********20**********

            //*************25
            //***********20
            //*************20  

            //The resulting array for both case will be 25 20 20..but the second case is invalid

            //so array sort will work only for non duplicate values


            KarthicBST<int> tree = TreeHelper.SetUpNonBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();

            //int[] array = new int[9];

            //InOrderTraversalResult(tree.Root, array, 0);




            //If it is BST, the resulting array should in sorted order

            string output =  tree.IsBinarySearchTree() ? "Tree is BST" : "Tree is not BST";

           

            

        }

        private void button8_Click(object sender, EventArgs e)
        {

            //This is an optimized way to check a tree is bst or not
            //logic during iteration make sure every node satisfies the bst definition and also check this case
            //**********20*********
            //********10***30******
            //**********25***********

            //Though the 25 is on the right of the node 10..it is still larger than 20 (root)
            //so during every iteration we need to maintain the min and max of every node

            KarthicBST<int> tree = TreeHelper.SetUpNonBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();

            //int[] array = new int[9];

            //InOrderTraversalResult(tree.Root, array, 0);




            //If it is BST, the resulting array should in sorted order

            string output = tree.IsBSTOptimalSolution(tree.Root) ? "Tree is BST" : "Tree is not BST";

           

        }

        private void button9_Click(object sender, EventArgs e)
        {

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();


            //Given the node of a bst, find the next node of the given node via in-order traversal

            //In-order traversal  left, current  and right
            
            //pseudocode
            // public Node Inordersuccessor(node)
            ///   if(node has right subtree)
            ///     return (left most node of right subtree)
            ///   else 
            ///     while (n is right of n.parent)
            ///            n = n.parent
            ///       return n.parent
            ///       

            KarthicBTNode<int> next = tree.NextInOrderSuccessor(tree.Find(10, tree.Root));

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form f = new BinaryTreesPage3();
            f.Show();
        }

       

       
    }
}
