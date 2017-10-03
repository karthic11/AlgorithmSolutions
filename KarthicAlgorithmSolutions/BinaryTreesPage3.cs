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
    public partial class BinaryTreesPage3 : Form
    {
        public BinaryTreesPage3()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            //KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();


            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();

            KarthicBTNode<int> node = tree.FindFirstCommonAncestor(tree.Root, tree.Find(1, tree.Root), tree.Find(6, tree.Root));

            KarthicBTNode<int> node1 = tree.FindFirstCommonAncestor(tree.Root, tree.Find(4, tree.Root), tree.Find(7, tree.Root));

            KarthicBTNode<int> node2 = tree.FindFirstCommonAncestor(tree.Root, tree.Find(4, tree.Root), tree.Find(14, tree.Root));

        }

        private void button1_Click(object sender, EventArgs e)
        {

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();


            KarthicBTNode<int> node1 = tree.Find(4, tree.Root);
            KarthicBTNode<int> node2 = tree.Find(7, tree.Root);
            KarthicBTNode<int> root = tree.Root;

            KarthicBTNode<int> result1 = tree.FindFirstCommonAncestorBetter(root, null, null);

            KarthicBTNode<int> result2 = tree.FindFirstCommonAncestorBetter(tree.Root, tree.Find(4, tree.Root), tree.Find(103, tree.Root));

        }

        private void button3_Click(object sender, EventArgs e)
        {

            KarthicBinaryTree<int> t1 = TreeHelper.SetUpBinaryTree();


            KarthicBinaryTree<int> t2 = TreeHelper.SetupBinarySubTree();
            t2.Root.Left.Right = new KarthicBTNode<int>(9);

            //Array solution with special character
            //Make In-order and pre-order travesal by inserting zero for null values and return string
            //Check whether s2 is a substring of s1
            //Key things: We have to do insert zero for null
            //we have to do both in-order and pre-order

           
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            StringBuilder s3 = new StringBuilder();
            StringBuilder s4 = new StringBuilder();

            t1.InOrderTraversal(t1.Root, ref s1, true);
            t2.InOrderTraversal(t2.Root, ref s2, true);
            t1.PreOrderTraversal(t1.Root, true); //get in

            t2.PreOrderTraversal(t2.Root, true);
            string output = string.Empty;
            //t2 is substring for t1 via in order traversal 
            if (t1.ToString().Contains(t2.ToString()) && (t1.sb.ToString().Contains(t2.sb.ToString())))
            {


                output = "T2 is a subtree of T1";

            }
            else
            {
                output = "T2 is not a subtree of T1";
            }


            

        }

        private void button4_Click(object sender, EventArgs e)
        {

            KarthicBinaryTree<int> t1 = TreeHelper.SetUpBinaryTree();


            KarthicBinaryTree<int> t2 = TreeHelper.SetupBinarySubTree();
            t2.Root.Left.Right = new KarthicBTNode<int>(9);

            //Traverse through t1 for every match to the root check match tree
            string output = string.Empty;
            //t2 is substring for t1 via in order traversal 
            if (ContainsTree(t1.Root, t2.Root))
            {


                output = "T2 is a subtree of T1";

            }
            else
            {
                output = "T2 is not a subtree of T1";
            }


        }


        public bool ContainsTree(KarthicBTNode<int> t1, KarthicBTNode<int> t2)
        {
            //null will be always there is tree so if t2 is null..then t2 is subtree of t1
            if (t2 == null)
            {
                return true;
            }

            return CheckSubTree(t1, t2);

        }

        public bool CheckSubTree(KarthicBTNode<int> t1, KarthicBTNode<int> t2)
        {
           

            if (t1 == null)
            {
                return false;
            }

            //have to traverse t1

            if (t1.Data == t2.Data)
            {
                if(CheckMatchTree(t1, t2))
                {
                    return true;
                }
            }

            return (CheckSubTree(t1.Left, t2) || CheckSubTree(t1.Right, t2));

        }

        //This method is to check both tree has same nodes
        public bool CheckMatchTree(KarthicBTNode<int> n1, KarthicBTNode<int> n2)
        {
            //base case
            if (n1 == null && n2 == null)
            {
                return true;
            }
            else if (n1 == null || n2 == null)
            {
                return false;
            }

            if (n1.Data != n2.Data)
            {
                return false;
            }

            return (CheckMatchTree(n1.Left, n2.Left) && CheckMatchTree(n1.Right, n2.Right));

       
           
        }

      

        private void button6_Click(object sender, EventArgs e)
        {

            /* Constructed binary tree is
              10
            /   \
          8      2
        /  \    /
      3     5  2
    */


            KarthicBinaryTree<int> tree = new KarthicBinaryTree<int>();
            tree.Root = new KarthicBTNode<int>(10);
            tree.Root.Left = new KarthicBTNode<int>(8);
            tree.Root.Left.Left = new KarthicBTNode<int>(3);
            tree.Root.Left.Right = new KarthicBTNode<int>(5);
            tree.Root.Right = new KarthicBTNode<int>(2);
            tree.Root.Right.Left = new KarthicBTNode<int>(2);

            //            For example, in the above tree root to leaf paths exist with following sums.

            //21 –> 10 – 8 – 3
            //23 –> 10 – 8 – 5
            //14 –> 10 – 2 – 2

            //So the returned value should be true only for numbers 21, 23 and 14. For any other number, returned value should be false
            int sum = 11;
            string output;
            if (HasPath(tree.Root, sum))
            {
                output = "There is a root-to-leaf path with sum " + sum;
            }
            else
            {
                output = "There is no root-to-leaf path with sum " + sum;
            }

        }


        /*
 Given a tree and a sum, return true if there is a path from the root
 down to a leaf, such that adding up all the values along the path
 equals the given sum.
 
 Strategy: subtract the node value from the sum when recurring down,
 and check to see if the sum is 0 when you run out of tree (leaf node)
*/
        public bool HasPath(KarthicBTNode<int> node, int sum)
        {

            if (node == null)
            {
                //if (sum == 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

                return (sum == 0);
            }

            int subsum = sum - node.Data;

            /* If we reach a leaf node and sum becomes 0 then return true*/
            if (subsum == 0 && node.Left == null && node.Right == null)
            {
                return true;
            }

            /* otherwise check both subtrees */

            return (HasPath(node.Left, subsum) || HasPath(node.Right, subsum));
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //            .e.g

            //_________________5___________________
            //__________6____________7_____________
            //_____1_________2

            //Possible path..updated 4/7/14...It doesn't have to be from root..can be any path
            //5, 6,1
            //5,6,2
            //5, 7




            KarthicBinaryTree<int> tree = new KarthicBinaryTree<int>();
            //level 0
            tree.Root = new KarthicBTNode<int>(7);

            //level 1
            tree.Root.Left = new KarthicBTNode<int>(5);
            tree.Root.Right = new KarthicBTNode<int>(12);

            //level 2
            tree.Root.Left.Left = new KarthicBTNode<int>(3);
            tree.Root.Left.Right = new KarthicBTNode<int>(6);
            tree.Root.Right.Left = new KarthicBTNode<int>(9);
            tree.Root.Right.Right = new KarthicBTNode<int>(15);

            //level 3
            tree.Root.Left.Left.Left = new KarthicBTNode<int>(1);
            tree.Root.Left.Left.Right = new KarthicBTNode<int>(4);
            tree.Root.Right.Left.Left = new KarthicBTNode<int>(8);
            tree.Root.Right.Left.Right = new KarthicBTNode<int>(10);
            tree.Root.Right.Right.Left = new KarthicBTNode<int>(13);
            tree.Root.Right.Right.Right = new KarthicBTNode<int>(17);

            int sum = Convert.ToInt16(this.textBox1.Text);

            List<string> output = new List<string>();

            int depth = tree.GetDepth(tree.Root);
            int[] array = new int[depth];

            //output = FindSum(tree.Root, sum, new ArrayList(), 0, output);

            output = FindSumUsingArray(tree.Root, sum, array, 0, output);


            foreach (string s in output)
            {
                this.listView1.Items.Add(s);
            }
     
        }


        public List<string> FindSumUsingArray(KarthicBTNode<int> node, int sum, int[] buffer, int level, List<string> output)
        {
            if (node == null)
            {
                return output;
            }

            //buffer.Add(node.Data);
            buffer[level] = node.Data;

            int temp = sum;
            //check for paths
            for (int i = level; i > -1; i--)
            {
                temp = temp - (int)buffer[i];
                //if path found
                if (temp == 0)
                {
                    output = PrintOutputArray(buffer, level, i, output);
                }
            }


            output = FindSumUsingArray(node.Left, sum, buffer, level + 1, output);

            output = FindSumUsingArray(node.Right, sum, buffer, level + 1, output);


            return output;
        }


        public void FindSum(KarthicBTNode<int> node, int sum, ArrayList buffer, int level)
        {
            if (node == null)
            {
                return;
            }


            if (node.Data == 12)
            {
                string s = "Test";
            }
            buffer.Add(node.Data);


            int temp = sum;
            //check for paths
            for (int i = level; i > -1; i--)
            {
                temp = temp - (int)buffer[i];
                //if path found
                if (temp == 0)
                {
                     PrintOutput(buffer, level, i, new List<string>());
                }
            }

            ArrayList leftcopy =  (ArrayList) buffer.Clone();
            ArrayList rightcopy = (ArrayList) buffer.Clone();

            FindSum(node.Left, sum, leftcopy, level + 1);

            FindSum(node.Right, sum, rightcopy, level + 1);

        }



        public List<string> FindSum(KarthicBTNode<int> node, int sum, ArrayList buffer, int level, List<string> output)
        {
            if (node == null)
            {
                return output;
            }

            buffer.Add(node.Data);
            int temp = sum;
            //check for paths
            for (int i = level; i > -1; i--)
            {
                temp = temp - (int)buffer[i];
                //if path found
                if (temp == 0)
                {
                    output = PrintOutput(buffer, level, i, output);
                }
            }

            ArrayList leftcopy = (ArrayList)buffer.Clone();
            ArrayList rightcopy = (ArrayList)buffer.Clone();

            output = FindSum(node.Left, sum, leftcopy, level + 1, output);

            output = FindSum(node.Right, sum, rightcopy, level + 1, output);

        
            return output;
        }

        public List<String> PrintOutput(ArrayList buffer, int startindex, int endindex, List<string> output)
        {

            StringBuilder sb = new StringBuilder();

            for (int j = startindex; j >= endindex; j--)
            {
                sb.Append(buffer[j]).Append(',');
            }

            string s = sb.ToString();

            output.Add(sb.ToString());

            return output;
        }

        public List<String> PrintOutputArray(int[] buffer, int startindex, int endindex, List<string> output)
        {

            StringBuilder sb = new StringBuilder();

            for (int j = startindex; j >= endindex; j--)
            {
                sb.Append(buffer[j]).Append(',');
            }

            string s = sb.ToString();

            output.Add(sb.ToString());

            return output;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Things to ask interviewer
            //1) Is it BST OR BINARY tree?. If it is bst we can use the value and compare

            //bst code


            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();


            KarthicBTNode<int> node1 = tree.Find(4, tree.Root);
            KarthicBTNode<int> node2 = tree.Find(7, tree.Root);
            KarthicBTNode<int> root = tree.Root;

            KarthicBTNode<int> result2 = tree.FindFirstCommonAncestorForOnlyBST(tree.Root, tree.Find(4, tree.Root), tree.Find(103, tree.Root));








            //2) Is the parent node is given... 

            //If the parent node is given and addition datastructure is allowed

            //Take one node and mark all the path visited as true from root..either modify tree node struc or use ht
            //Take another node and keep track of last visited..when we reach first non-visited path then last visited will be ancestor

            //if the parent node is goven and addition datasturc not allowed

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

            string check = sb.ToString();


            KarthicBTNode<int> node1 = tree.Find(4, tree.Root);
            KarthicBTNode<int> node2 = tree.Find(7, tree.Root);
            KarthicBTNode<int> root = tree.Root;

            KarthicBTNode<int> result1 = tree.FindFirstCommonAncestorUsingParent(root, null, null);

            KarthicBTNode<int> result2 = tree.FindFirstCommonAncestorUsingParent(tree.Root, tree.Find(4, tree.Root), tree.Find(103, tree.Root));
        }

        //private v FindFirstCommonAncestorUsingParent
    
    }
}
