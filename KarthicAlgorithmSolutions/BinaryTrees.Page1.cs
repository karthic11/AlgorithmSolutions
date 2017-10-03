using System;
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
    public partial class BinaryTrees : Form
    {
        public BinaryTrees()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string input = this.textBox1.Text;

              KarthicLinkedList node = NodeHelper.GetLinkedListByString(input);
              KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();
               bt.Root = new KarthicBTNode<int>(node.headnode.Data);

               //set 1st level
               bt.Root.Left = new KarthicBTNode<int>(node.headnode.Next.Data);
               bt.Root.Right = new KarthicBTNode<int>(node.headnode.Next.Next.Data);
               
               //set 2nd level
               bt.Root.Left.Left = new KarthicBTNode<int>(node.headnode.Next.Next.Next.Data);
               bt.Root.Left.Right = new KarthicBTNode<int>(node.headnode.Next.Next.Next.Next.Data);
               bt.Root.Right.Left = new KarthicBTNode<int>(node.headnode.Next.Next.Next.Next.Next.Data);
               bt.Root.Right.Right = new KarthicBTNode<int>(node.headnode.Next.Next.Next.Next.Next.Next.Data);



               
               

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

        private void button2_Click(object sender, EventArgs e)
        {
            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

            //Pre-Order Traversal
            //Traverse the binary tree in the follwing order current node, left childrens  and right childrens

             string output = tree.PreOrderTraversal(tree.Root);

             this.textBox1.Text = output;

            //Expected output:  1, 2, 4, 5, 3, 6, 7

           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

            //In-Order Traversal
            //Traverse the binary tree in the follwing leftnode, current, right node
            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            this.textBox2.Text = sb.ToString();
            //Expected output: 4,2,5,1,6,3,7,

        }

        private void button4_Click(object sender, EventArgs e)
        {

            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

            //In-Order Traversal
            //Traverse the binary tree in the follwing leftnode, current, right node
            StringBuilder sb = new StringBuilder();
            tree.PostOrderTraversal(tree.Root, ref sb);

            this.textBox3.Text = sb.ToString();
            //Expected output: 4,5,2,6,7,3,1

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();
            //Test In-Order Traversal
            //Traverse the binary tree in the follwing leftnode, current, right node
            StringBuilder sb = new StringBuilder();
            
            tree.InOrderTraversal(tree.Root, ref sb);
            string test = sb.ToString();
            //Expected output: 1,3,4,6,7,8,10,13,14
            int value = Convert.ToInt16(this.textBox5.Text);
            //Insert a node value of 20
            KarthicBST<int> newtree = new KarthicBST<int>();
            newtree.Root = tree.Insert(value, tree.Root);
            StringBuilder sb2 = new StringBuilder();
            newtree.InOrderTraversal(tree.Root, ref sb2);
             this.textBox7.Text = sb2.ToString();
            //Expected output: 1,3,4,6,7,8,10,13,14,20


        }

        private void button10_Click(object sender, EventArgs e)
        {
            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            int input = Convert.ToInt32(this.textBox4.Text);

            //int input =
            KarthicBTNode<int> result = tree.Find(input, tree.Root);

            //KarthicBTNode<int> result = tree.Find2(input, tree.Root);

            this.textBox6.Text = (result != null) ? "true" : "false";
          
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

            Form f = new BinaryTees();
            f.Show();
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
            KarthicGraph<int> graph = new KarthicGraph<int>();
            TreeHelper.SetUpGraph(graph);
            StringBuilder output = new StringBuilder();
            graph.DepthFirstSearch(graph.Root, output);

            this.textBox8.Text = output.ToString();


        }

        private void button9_Click(object sender, EventArgs e)
        {
            KarthicGraph<int> graph = new KarthicGraph<int>();
            TreeHelper.SetUpGraph(graph);
            StringBuilder output = new StringBuilder();
            graph.BreadthFirstSearch(graph.Root, output);

            this.textBox9.Text = output.ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();
            //Test In-Order Traversal
            //Traverse the binary tree in the follwing leftnode, current, right node
            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);
            string test = sb.ToString();

            //Expected output: 1,3,4,6,7,8,10,13,14
            int value = Convert.ToInt16(this.textBox12.Text);
            //Insert a node value of 20
            KarthicBST<int> newtree = new KarthicBST<int>();
            newtree.Root = tree.Delete(value, tree.Root);
            StringBuilder sb2 = new StringBuilder();
            newtree.InOrderTraversal(tree.Root, ref sb2);
            this.textBox11.Text = sb2.ToString();
            //Expected output: 1,3,4,6,7,8,10,13,14,20
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


          KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

          //In-Order Traversal
          //Traverse the binary tree in the follwing leftnode, current, right node
          StringBuilder sb = new StringBuilder();
          tree.LevelTraversal(tree.Root, sb);

          this.textBox10.Text = sb.ToString();
      

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

            //In-Order Traversal
            //Traverse the binary tree in the follwing leftnode, current, right node
            StringBuilder sb = new StringBuilder();


            tree.InOrderTraversalUsingStack(tree.Root, ref sb);

            this.textBox2.Text = sb.ToString();
            //Expected output: 4,2,5,1,6,3,7,
        }


       
    }
}
