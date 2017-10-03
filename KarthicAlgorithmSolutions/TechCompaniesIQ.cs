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
    public partial class TechCompaniesiq : Form
    {
        public TechCompaniesiq()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);

            KarthicBST<int> tree = new KarthicBST<int>();
            tree.Root = BuildTreeWithPreOrderArray(array, 0, 0, array.Length - 1).treenode;

            string output = tree.PreOrderTraversal(tree.Root);

            bool result = String.Equals(this.textBox1.Text, output.Substring(0, output.LastIndexOf(',')), StringComparison.OrdinalIgnoreCase);

        }

        //Input: 10,5,1,7,40,50
        //Logic: 
        //we take the first element as the root since it is preorder traveral (current, left,right)
        //We scan the array to see the first largest item greater than root.(which will tell tat is right child)
        //Once we find that we divide the array into two (0 to i -1) as left subtree of root and (i to array end) as right subtree of root
        //continue this recrusion till the end of array
        private NodeIndex BuildTreeWithPreOrderArray(int[] array, int index, int low, int high)
        {
             //base case 
            if (index >= array.Length || high < low)
            {
                NodeIndex nodenull = new NodeIndex();
                nodenull.treenode = null;
                nodenull.StartIndex = index;
                return nodenull;
            }

            //first node is always root
            NodeIndex nodewithindex = new NodeIndex();
            nodewithindex.treenode = new KarthicBTNode<int>(array[index]);
            //we increment the index so that tree can build from the next index
            index++;

            //search for the first greater element than root
            int i;
            for (i = low; i <= high; i++)
            {
                if (array[i] > nodewithindex.treenode.Data)
                {
                    //we found the element greater than the root..take this array value and divide the array for left and right tree
                    break;
                }
            }

            // Use the index of element found to divide preorder array in
            // two parts. Left subtree and right subtree
            //Here we have
            //                          10
            //////(5,1,7) as left sub tree   (40,5) as right sub tree

            NodeIndex leftsubtree = BuildTreeWithPreOrderArray(array, index, index, i - 1);
            nodewithindex.treenode.Left = leftsubtree.treenode;


            NodeIndex rightsubtree = BuildTreeWithPreOrderArray(array, leftsubtree.StartIndex, i , high);
            nodewithindex.treenode.Right = rightsubtree.treenode; 


            //get the startindex from the right subtree
            nodewithindex.StartIndex = rightsubtree.StartIndex;
            return nodewithindex;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);

            KarthicBST<int> tree = new KarthicBST<int>();
            tree.Root = ConstructTreeWithMinAndMax(array, 0, Int32.MinValue, Int32.MaxValue, array[0]).treenode;

            string output = tree.PreOrderTraversal(tree.Root);

            bool result = String.Equals(this.textBox1.Text, output.Substring(0, output.LastIndexOf(',')), StringComparison.OrdinalIgnoreCase);

        }


        //Logic 
        //We take the first element and construct as root element
        //The values lesser between min and root will be constructed in the left tree
        //and the values greater than root and max will be contructed in the right tree
        //key things is index value of the array shouldn't bubble up 
        //whereas the value (array value) should bubble up bcoz the root value should be preserver so that after traversing left subtree, the right sub tree needs the root values as min
        public NodeIndex ConstructTreeWithMinAndMax(int[] array, int index, int min, int max, int value)
        {
            //base case 
            if (index >= array.Length)
            {
                NodeIndex nodenull = new NodeIndex();
                nodenull.treenode = null;
                nodenull.StartIndex = index;
                return nodenull;
            }

            //first node is always root
            NodeIndex nodewithindex = new NodeIndex();
            nodewithindex.treenode = null;
          
            //if the value is greater than min and lesser than max 
            //The second condition is value <= max is little tricky
            if (value > min && value <= max)
            {
                nodewithindex.treenode = new KarthicBTNode<int>(value);
                //we added the node..increment the index to get the next array element
                index++;
                //since we incremented the index check for this condition
                if (index < array.Length)
                {

                    //build left substree for values btw min and current data
                    NodeIndex ltree = ConstructTreeWithMinAndMax(array, index, min, value, array[index]);
                    nodewithindex.treenode.Left = ltree.treenode;

                    //build right substree for value btw current data and max
                    NodeIndex rtree = ConstructTreeWithMinAndMax(array, ltree.StartIndex, value, max, array[ltree.StartIndex]);
                    nodewithindex.treenode.Right = rtree.treenode;
                    nodewithindex.StartIndex = rtree.StartIndex;

                }

            }
            else
            {
                //when the condition fails..will fail in the leaf node..have to maintain the index
                NodeIndex nodenull = new NodeIndex();
                nodenull.treenode = null;
                nodenull.StartIndex = index;
                return nodenull;
            }

            return nodewithindex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox1.Text);

            KarthicBST<int> tree = new KarthicBST<int>();
            tree.Root = ConstructTreeusingStacks(array);

            string output = tree.PreOrderTraversal(tree.Root);

            bool result = String.Equals(this.textBox1.Text, output.Substring(0, output.LastIndexOf(',')), StringComparison.OrdinalIgnoreCase);

            //test

            KarthicBST<int> tree1 = TreeHelper.SetUpBinarySearchTree();

            string test1 = tree1.PreOrderTraversal(tree1.Root);

            int[] input2 = AlgorithmHelper.ConvertCommaSeparetedStringToInt(test1);


            KarthicBST<int> tree3 = new KarthicBST<int>();
            tree3.Root = ConstructTreeusingStacks(input2);

            string test2 = tree3.PreOrderTraversal(tree3.Root);

            bool result2 = String.Equals(test1, test2, StringComparison.OrdinalIgnoreCase);




        }

        //Logic
        //Create a stack. Create root node from the first element and add it the stack
        //loop through the array for other elements (1 to array.length -1)
        //create node for each element and see where it can be added
        //if the value is lesser than the root/node, put it on left of parent and push the item to stack and the parent will be stack.peek()..and continue for next
        //if the value is greater than the root/node, then find the largest element in the stack by popping...Get the last popped item as parent //Important do not take current peek as parent that last popped as parent
        //and add the node to the right and add the current node to the stack
        //continue this till the end
        KarthicBTNode<int> ConstructTreeusingStacks(int[] array)
        {

            if (array.Length == 0)
            {
                return null;
            }
            KarthicBTNode<int> root = new KarthicBTNode<int>();
            Stack<KarthicBTNode<int>> mystack = new Stack<KarthicBTNode<int>>();
            //take first element and put it on stack
            root.Data = array[0];
            mystack.Push(root);
            KarthicBTNode<int> parent = root;
           
            for (int i = 1; i < array.Length; i++)
            {
                KarthicBTNode<int>  currentnode = new KarthicBTNode<int>(array[i]);
                parent = mystack.Peek();

                if (currentnode.Data <= parent.Data)
                {
                    parent.Left = currentnode;
                }
                else
                {
                    //logic here is the last popped element will be the parent

                    //KarthicBTNode<int> runner = mystack.Peek();

                    while (mystack.Count != 0 && mystack.Peek().Data < currentnode.Data)
                    {
                        //parent will always be last popped
                        parent = mystack.Peek();
                        mystack.Pop();
                       
                    }
                    //at the end of while the parent will be the root.
                    parent.Right = currentnode;
                }

                mystack.Push(currentnode);
            }


            return root;

        }



       

        private void button4_Click(object sender, EventArgs e)
        {
            char[] inorderarray = AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox2.Text);
            char[] preorderarray = AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox3.Text);

            CustomNode<char> tree = new CustomNode<char>();
            KarthicBST<char> root = new KarthicBST<char>();
            tree = BuildBSTreeFromInOrderAndPreOrderArray(preorderarray, 0, inorderarray, 0, inorderarray.Length - 1);

            //The result tree
            //        //A
            //       /   \
            //     /       \
            //    B         C
            //   / \        /
            // /     \    /
            //D       E  F
         


        }


        //souce: http://www.geeksforgeeks.org/construct-tree-from-given-inorder-and-preorder-traversal/
        //Logic
        //Inorder sequence: D B E A F C
        //Preorder sequence: A B D E C F
        //Given inorder and preorder sequence
        //pick the first elemnet in preorder and build root element
        //increment the value of preorderindex and maintain in the recurrsion..no bubble needed
        //find the root element in the inorder array and get it's index
        //the element to the left of index are leftsubtree and the element right of the index forms rightsubtree.
        //recurse and build the tree in this logic

        //The result tree
        //        //A
        //       /   \
        //     /       \
        //    B         C
        //   / \        /
        // /     \    /
        //D       E  F


        public CustomNode<char> BuildBSTreeFromInOrderAndPreOrderArray(char[] preArray, int preIndex, char[] inArray, int inLow, int inHigh)
        {

            //base case 
            if (inHigh < inLow)
            {
                CustomNode<char> nodenull = new CustomNode<char>();
                nodenull.treenode = null;
                nodenull.StartIndex = preIndex;
                return nodenull;
            }

            //build the current node
            KarthicBTNode<char> current = new KarthicBTNode<char>(Convert.ToChar(preArray[preIndex]));
            //increment 
            preIndex++;

           CustomNode<char> result = new CustomNode<char>();
            result.treenode = current;
            result.StartIndex = preIndex;

            //after building the node..check for low and high
            //for case low = 2 and high = 3..it mean this is leaf node..left and right will be null
            //we stop the recursion here
            if (inLow == inHigh)
            {
                return result;
            }

            int indexofcurrent = SearchArray(inArray, current.Data, inLow, inHigh);

            //divide the tree
            CustomNode<char> lsubtree = BuildBSTreeFromInOrderAndPreOrderArray(preArray, preIndex, inArray, inLow, indexofcurrent - 1);
            result.treenode.Left = lsubtree.treenode;

            CustomNode<char> rsubtree = BuildBSTreeFromInOrderAndPreOrderArray(preArray, lsubtree.StartIndex, inArray, indexofcurrent + 1, inHigh);
            result.treenode.Right = rsubtree.treenode;

            result.StartIndex = rsubtree.StartIndex;


            return result;


            

        }

        public int SearchArray(char[] array, char value, int startindex, int endindex)
        {
            for (int j = startindex; j < array.Length; j++)
            {
                if (value == array[j])
                {
                    return j;
                }
            }

            return -1;
        }

        private void TechCompaniesIQ_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //source: http://www.geeksforgeeks.org/construct-a-special-tree-from-given-preorder-traversal/
            int[] preorderarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox5.Text);
            char[] leafarray = AlgorithmHelper.ConvertCommaSeparetedStringToCharArray(this.textBox4.Text);

            CustomNode<int> tree1 = new CustomNode<int>();
            KarthicBinaryTree<int> tree = new KarthicBinaryTree<int>();
            tree.Root = BuildSpecialTreeFromPreorderArray(preorderarray, leafarray, 0).treenode;

            string output = tree.PreOrderTraversal(tree.Root);


            bool result = String.Equals(this.textBox5.Text, output.Substring(0, output.LastIndexOf(',')), StringComparison.OrdinalIgnoreCase);

        }


        public  CustomNode<int> BuildSpecialTreeFromPreorderArray(int[] preArray, char[] LeafArray, int index)
        {
            if(index == preArray.Length)
            {
                CustomNode<int> nodenull = new CustomNode<int>();
                nodenull.treenode = null;
                nodenull.StartIndex = index;
                return nodenull;

            }

            KarthicBTNode<int> node = new KarthicBTNode<int>(preArray[index]);
            int actualnodeindex = index;
            index++;

            CustomNode<int> custom = new CustomNode<int>();
            custom.treenode = node;
            custom.StartIndex = index;
            //No leaf node
            if (LeafArray[actualnodeindex] == 'N')
            {
                CustomNode<int> ltree = BuildSpecialTreeFromPreorderArray(preArray, LeafArray, index);
                custom.treenode.Left = ltree.treenode;

                CustomNode<int> rtree = BuildSpecialTreeFromPreorderArray(preArray, LeafArray, ltree.StartIndex);
                custom.treenode.Right = rtree.treenode;

                custom.StartIndex = rtree.StartIndex;
            }


            return custom;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] levelarray = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox7.Text);
          
           
            KarthicBinaryTree<int> tree = new KarthicBinaryTree<int>();
            tree.Root = BuildSpecialTreeFromLevelTraversalArray(levelarray);
            StringBuilder output = new StringBuilder();
            tree.LevelTraversal(tree.Root, output);

           
            bool result = String.Equals(this.textBox7.Text, output.ToString().Substring(0, output.ToString().LastIndexOf(',')), StringComparison.OrdinalIgnoreCase);
        }

        //logic array value 1 mean 2 childs, 0 mean leaf node
        public KarthicBTNode<int> BuildSpecialTreeFromLevelTraversalArray(int[] array)
        {
            int index = 0;
            if (array.Length == 0)
            {
                //error
            }
            //build first element
            KarthicBTNode<int> root = new KarthicBTNode<int>(array[index]);
            index++;

            if (index == array.Length)
            {
                return root;
            }
            Queue<KarthicBTNode<int>> myqueue = new Queue<KarthicBTNode<int>>();
            myqueue.Enqueue(root);
            KarthicBTNode<int> parent = null;

            while (myqueue.Count != 0)
            {
                parent = myqueue.Dequeue();

                //value 1 means parent has both left and right children
                if (parent.Data == 1)
                {
                      //add left child
                    KarthicBTNode<int> lchild = new KarthicBTNode<int>(array[index]);
                    index++;
                    parent.Left = lchild;
                    //add left to queue
                    myqueue.Enqueue(lchild);

                    //add right child
                    KarthicBTNode<int> rchild = new KarthicBTNode<int>(array[index]);
                    index++;
                    parent.Right = rchild;

                    //add right to queue
                    myqueue.Enqueue(rchild);

                }
            }

            return root;
           
        }
    }

    public class NodeIndex
    {
        public KarthicBTNode<int> treenode { get; set; }
        public int StartIndex { get; set; }


        public NodeIndex()
        {
        }
        public NodeIndex(KarthicBTNode<int> node, int nextindextoread)
        {
            this.treenode = node;
            this.StartIndex = nextindextoread;
        }
    }

    public class CustomNode<T>
    {
        public KarthicBTNode<T> treenode { get; set; }
        public int StartIndex { get; set; }


        public CustomNode()
        {
        }
        public CustomNode(KarthicBTNode<T> node, int nextindextoread)
        {
            this.treenode = node;
            this.StartIndex = nextindextoread;
        }
    }
}
