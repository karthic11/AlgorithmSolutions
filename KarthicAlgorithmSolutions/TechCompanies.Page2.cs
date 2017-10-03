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
    public partial class TechCompanies : Form
    {
        public TechCompanies()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(this.textBox2.Text);

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            this.textBox3.Text = findKthSmallesIterative(tree.Root, k).Data.ToString();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

        }


        //Logic:
        //Get to the leftmost of the tree untill it reaches null and push the left node to the stack while doing
        //After reaching null pop the top on the stack which will be the first smallest check for the k value when popped up
        //If k not found which mean we need to find the next smalles element
        //Since the left child is null the next smallest will be leftmost of the rightsubtree so check if it has rightsubtree
        //If rightsubtree exists get the leftmost of that untill it reaches null
        //If rightsubtree is null we pop the next element which would be the next smallest

        public KarthicBTNode<int> findKthSmallesIterative(KarthicBTNode<int> node, int K)
        {
            Stack<KarthicBTNode<int>> stack = new Stack<KarthicBTNode<int>>();
            int i = 1;
            while (stack.Count != 0 || node != null)
            {

                if (node == null)
                {
                    node = stack.Pop();
                    if (i == K)
                    {
                        return node;
                    }
                    ++i;
                    node = node.Right;
                }
                if (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }


            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(this.textBox2.Text);

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            this.textBox3.Text = FindkthSmallestNodeByChildNo(tree.Root, k).Data.ToString();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

        }

        //Logic
        //We check if the noofchilds of the current node with k value
        //if its equal return the current node
        //greater search on the right child (k - leftchilds)
        //lesser it has to the left side so search on the node.left for k

        //update 5/9 below code is for kth largest
        public KarthicBTNode<int> FindkthSmallestNodeByChildNo(KarthicBTNode<int> root, int k)
        {

            if (root == null)
            {
                return null;
            }

            int leftchilds = root.LeftChilds;
            int rootandchilds = leftchilds + 1; //this value is inclusive of the root node and its left childs
            //int rightchilds = root.RightChilds;

            if (k == rootandchilds)
            {
                return root;

            }
            else if (k < rootandchilds)
            {
                return findKthSmallesIterative(root.Left, k);
            }
            else
            {
                return findKthSmallesIterative(root.Right, k - rootandchilds);
            }

        
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int k = Convert.ToInt16(this.textBox2.Text);

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            Stack<KarthicBTNode<int>> mystack = new Stack<KarthicBTNode<int>>();

            this.textBox3.Text = FindkthsmallestnodebyInordertraversal(tree.Root, k, mystack).node.Data.ToString();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);

        }

        //Logic: we can find the kth smallest by many ways
        //1) we can build sorted array from preorder traversal and find the kth smalles on the sorted array
        //2) we can put items on stack and when stack count reaches k return the element
        //3) use static count inside the method and when k reaches counter return the element
        
        //public KarthicBTNode<int> FindkthsmallestnodebyInordertraversal(KarthicBTNode<int> root, int k, Stack<KarthicBTNode<int>> mystack)
        //{
        //    //base case
        //    if (root != null)
        //    {
        //        //left
        //        FindkthsmallestnodebyInordertraversal(root.Left, k, mystack);
        //        //current
        //        mystack.Push(root);

        //        if (k == mystack.Count)
        //        {
        //            return root;
        //        }

        //        //right
        //        FindkthsmallestnodebyInordertraversal(root.Right, k, mystack);
              
        //    }
           
           
        //}

        public BTCustomNode<int> FindkthsmallestnodebyInordertraversal(KarthicBTNode<int> root, int k, Stack<KarthicBTNode<int>> mystack)
        {

            //base case
            if (root == null)
            {
                return null;

            }


            //left

            BTCustomNode<int> ltree = FindkthsmallestnodebyInordertraversal(root.Left, k, mystack);
            if (ltree != null && ltree.IsSmallest)
            {
                return ltree;
            }


            //current

            BTCustomNode<int> current = new BTCustomNode<int>();
            current.node = root;
            mystack.Push(root);
            if (k == mystack.Count)
            {
                current.IsSmallest = true;
                return current;
            }
            else
            {
                current.IsSmallest = false;
            }


            //right
            BTCustomNode<int> rtree = FindkthsmallestnodebyInordertraversal(root.Right, k, mystack);
            if (rtree != null && rtree.IsSmallest)
            {
                return rtree;
            }




            return current;


        }



        public class BTCustomNode<T>
        {
            public KarthicBTNode<T> node { get; set; }
            public bool IsSmallest { get; set; }
            public bool IsLargest { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = this.textBox4.Text;
            string output = EvaluateExpression(input).ToString();
            this.textBox1.Text = output;
        }

        //Logic is so simple source: http://stackoverflow.com/questions/28256/equation-expression-parser-with-precedence
        //http://www.geeksforgeeks.org/expression-evaluation/
        //You want to have two stacks, one for numbers, and another for operators. You push numbers onto the stack all the time. 
        //You compare each new operator with the one at the top of the stack, if the one on top of the stack has higher priority, 
        //you pop it off the operator stack, pop the operands off the number stack, apply the operator and push the result onto the number stack. Now you repeat the comparison with the top of stack operator.
        //shunting yard algorithm
        // convert In fix Notation to Postfix Notation: 

        //Assumption:
        //1) All the expression is valid eg 8 + ((2 +2   Eg: The open parenthesis is never close..check for that
        //2) Error handling for 2 + 4** + 2
        public int EvaluateExpression(string expression)
        {
            char[] chars = expression.ToCharArray();
            Stack<int> stackvalue = new Stack<int>();
            Stack<char> stackoperators = new Stack<char>();

            //

            //100 + ( 2 * 6 ) + 4
            for(int i = 0; i < chars.Length; i++)
            {

                //Ignore white spaces in the string
                if (chars[i] == ' ')
                {
                    continue;
                    //skip the rest of the code
                }

                //test
                if (i == 7)
                {
                    int value = stackoperators.Count;
                }

                //if the c is a number, then push to the stack
                if (chars[i]  >= '0' && chars[i] <= '9')
                {
                    //the number might be more than one digit so find the whole number and push it into the stack
                    StringBuilder sb = new StringBuilder();

                    while (i < chars.Length && chars[i] >= '0' && chars[i] <= '9')
                    {
                        sb.Append(chars[i]);
                        i++; //increment the string to find the next char
                    }

                    stackvalue.Push(Convert.ToInt32(sb.ToString()));
                    //At the end we would increment to the character after the digit but it will be handle in the below code
                    //3/28/2015 We have a bug here if we increment the value then we bypass the below code bcoz we are in if statement so
                    i--;
                }
                else if (chars[i] == '(')
                {
                     //if we find open parenthesis put it on stack
                    stackoperators.Push(chars[i]);
                }
                else if (chars[i] == ')')
                {
                    //if a closed parenthesis is encountered then we need to solve the expression within the parenthesis
                    while (stackoperators.Peek() != '(')
                    {
                        //Pop the first operator and pop the last two values and solve
                        int value = ApplyOperatorOnValue(stackoperators.Pop(), stackvalue.Pop(), stackvalue.Pop());
                        stackvalue.Push(value);
                    }
                   //remove the '(' operator
                    stackoperators.Pop();
                }
                else if (chars[i] == '+' || chars[i] == '-' || chars[i] == '*' || chars[i] == '/')
                {
                    //if the char is a operator then comes the actual logic
                    //if the operator precedence is greater than the stackoperator.peek(), then add it to the stack
                    //but if the operator precedence is smaller or equal to the stackoperator.peek(),
                    //then do the calculation untill you find another operator in the stack which islesser than the current or the stack is empty
                     //Here we are checking if the char[i] has less precedence over parm 2 peek() of operator stack
                    while (stackoperators.Count != 0 && checkpeekhashigherorequalprecedence(chars[i], stackoperators.Peek()))
                    {
                        //Pop the first operator and pop the last two values and solve
                        int value = ApplyOperatorOnValue(stackoperators.Pop(), stackvalue.Pop(), stackvalue.Pop());
                        stackvalue.Push(value);
                    }
                    //push the actual value to the stack
                    stackoperators.Push(chars[i]);

                }


              
            }//done with for loop..finished the char array

            // Entire expression has been parsed at this point, apply remaining
            // ops to remaining values
            while (stackoperators.Count != 0)
            {
                //Pop the first operator and pop the last two values and solve
                int value = ApplyOperatorOnValue(stackoperators.Pop(), stackvalue.Pop(), stackvalue.Pop());
                stackvalue.Push(value);
            }

            // Top of 'values' contains result, return it
            return stackvalue.Pop();

        }

        //Note: + and  - has same precedence and its is lesser than  * and / 
        //* and / has same precendence
        // Returns true if 'op2' has higher or same precedence as 'op1',
        // otherwise returns false.
        //Update: 3/28/2015 Since this method is confusing I'm gonna retire this
        public bool checkpeekhashigherorequalprecedence(char newchar, char peek)
        {
            if (peek == '(' || peek == ')')
                return false;
            if ((newchar == '*' || newchar == '/') && (peek == '+' || peek == '-'))
                return false;
            else
                return true;
        }


        /*
         For this method imaginethe following scenario. You have a list of talented people say 5. You shouldn't exceed this list
         * you have a big problem to solve and the venod is send a new guy
         * If the new guy has higher precedence (high talent) include him to your group make the group stronger
         * If the new guy has low precedence(low talent) then use the high talenented guy in your group to solve the problem
         * Bottom line if the new character has high precedence then add it to stack else pop the last element
         */
        public bool CheckIfNewCharacterHasLessOrEqualPrecedence(char newchar, char peek)
        {
            if (peek == '(' || peek == ')')
                return false;
            if ((newchar == '*' || newchar == '/') && (peek == '+' || peek == '-'))
                return false;
            else
                return true;
           
        }



        //Important: First pop is in variable a, second pop is in b. the second pop will be greater than first pop
        //so b is greater than a 
        //
        public int ApplyOperatorOnValue(char oper, int a, int b)
        {
            switch (oper)
            {
                case '+':
                    return b + a;
                case '-':
                    return b - a;
                case '*':
                    return b * a;
                case '/':
                    if (a == 0)
                    {
                        // UnsupportedOperationException("Cannot divide by zero");
                    }
                    return b / a;
            }
            return 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {


         //   int[,] pairs = AlgorithmHelper.ConvertsPairsToTwoDArray(this.textBox5.Text);

          //  int[,] pairs = (int[,])this.textBox5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            int[,] pairs = { { 2, 4 },
                           { 1, 2 },
                           { 3, 6 },
                           { 1, 3 }, 
                           { 2, 5 } };

            //int[,] board = new int[,]{{1,0,1,2,2},
            //                    {2,1,0,2,1},
            //                    {2,2,2,1,0},
            //                    {1,2,2,2,1},
            //                    {2,2,2,1,0}};

            KarthicBTNode<int> root = reconstructTree(pairs);



        }



        //Language: C#
        //Assumption: The tree has only one root and each node has 0 to 2 children
        //Approach:  
        //We need map with key as parent and List<child> as value to build the tree
        //and we need another map with key as child and parent as value to find the root node of the tree
        //We loop through the given pairs once and populate both the map 
        
        //Finding the root of the tree:
        //Root is a node in the tree that does not have any parent. we can use this logic and find the root node using childparentmap
        //Let's assume the first element in the array as root and traverse to the parent of that element 
        //untill we find a value that does not have a parent and the value will be root node.

        //Building a tree from root in recursion function
        //First create the root node
        //Count the number of childrens of the node using parentchildmap
        //if the childcount > 0, we take the first child and build that left node using the same logic recursion
        //if the childcount == 2, we take the second child and build the right node using the same logic using recursion


        //Run-Time Complexity: 
        //ConstructTree Recursion function time complexity o(n) where n is number of nodes in the tree
        //Build dictionary from the given 2d array takes 0(n)
        //Search the value from the key used in the algorithm is 0(1)

        //Data Structure Used
        //Dictionar<int, List<child>>
        //Dictionary<int, int>

        public KarthicBTNode<int> reconstructTree(int[,] pairs)
        {
      
            //we can use hashtable or dictionary, I'm using Dictionary bcoz Dictionary is strongly type (no boxing and unboxing for value types)
            //Create map with parent child relation with Key - Parent and Value - List<Children>
            Dictionary<int, List<int>> parentchildmap = new Dictionary<int, List<int>>();
            //Create map with child and  parent relation. This map is used to find the root node
            Dictionary<int, int> childparentmap = new Dictionary<int, int>();
         

            //populate dictionary with pairs
            //length of one dimentional 
            for (int i = 0; i < pairs.GetLength(0); i++)
            {
                List<int> childs = null;

                //we know that first column is parent and second column is child
                if (parentchildmap.ContainsKey(pairs[i, 0]))
                {
                    childs = parentchildmap[pairs[i, 0]];
                }
                else
                {
                    childs = new List<int>();
                    parentchildmap.Add(pairs[i, 0], childs);
                }

                childs.Add(pairs[i, 1]);
                if (childparentmap.ContainsKey(pairs[i, 1]))
                {
                    throw new Exception("Child "+ pairs[i, 1] +" contains more than one parent");
                }
                else
                {
                    childparentmap.Add(pairs[i, 1], pairs[i, 0]);
                }

            }
           
             //Find the root node for the tree
            //In a valid binary tree the root node will have no parent 
            //If we take any node in the tree and traverse to its parent and continue doing this untill we find a node where there is no parent and that will be the root node
            int root = pairs[0, 0]; //take any parent
            while (childparentmap.ContainsKey(root))
            {
                root = childparentmap[root];
            }


            return BuildTreeUsingMap(parentchildmap, root);
           
        }


        public KarthicBTNode<int> BuildTreeUsingMap(Dictionary<int, List<int>> map, int rootvalue)
        {

            KarthicBTNode<int> node = new KarthicBTNode<int>(rootvalue);

            int childs = 0;

            if (map.ContainsKey(rootvalue))
            {
                childs = map[rootvalue].Count;
            }

            if (childs == 0)
            {
                return node;
            }

            node.Left = BuildTreeUsingMap(map, map[rootvalue][0]);

            if (childs == 2)
            {
                node.Right = BuildTreeUsingMap(map, map[rootvalue][1]);
            }


            return node;


        }

        private void button7_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(this.textBox2.Text);

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            this.textBox3.Text = FindkthLargestNodeByChildNo(tree.Root, k).Data.ToString();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);
        }

        //Assumtion: Each node has no of right childs value
        //Time complexity: log n
        private KarthicBTNode<int> FindkthLargestNodeByChildNo(KarthicBTNode<int> node, int k)
        {
            if (node == null)
            {
                return null;
            }
            int rightchild = node.RightChilds;
            int rootAndrightchild = rightchild + 1;

            if (k == rootAndrightchild)
            {
                return node;
            }
            else if (k < rootAndrightchild)
            {
                return FindkthLargestNodeByChildNo(node.Right, k);
            }
            else
            {
                //k is lesser than rootandrightchild
                return FindkthLargestNodeByChildNo(node.Left, k - rootAndrightchild);

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt16(this.textBox2.Text);

            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();

            StringBuilder sb1 = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb1);

            Stack<KarthicBTNode<int>> mystack = new Stack<KarthicBTNode<int>>();

            this.textBox3.Text = FindKthLargestByTraversal(tree.Root, k, mystack).node.Data.ToString();

            StringBuilder sb = new StringBuilder();
            tree.InOrderTraversal(tree.Root, ref sb);
        }

        private BTCustomNode<int> FindKthLargestByTraversal(KarthicBTNode<int> node, int k, Stack<KarthicBTNode<int>> mystack)
        {
            if (node == null)
            {
                BTCustomNode<int> basetree = new BTCustomNode<int>();
                basetree.node = null;
                return basetree;
            }
            //traverse right child
            BTCustomNode<int> righttree = new BTCustomNode<int>();
            righttree = FindKthLargestByTraversal(node.Right, k, mystack);
            if (righttree.node != null && righttree.IsLargest)
            {
                return righttree;
            }

           //visit current
            BTCustomNode<int> current = new BTCustomNode<int>();
            mystack.Push(node);
            current.node = node;
            if (mystack.Count == k)
            {
                current.IsLargest = true;
                return current;
            }
            else
            {
                current.IsLargest = false;
            }


            BTCustomNode<int> lefttree = new BTCustomNode<int>();
            lefttree =  FindKthLargestByTraversal(node.Left, k, mystack);
            if (lefttree.node != null && lefttree.IsLargest)
            {
                return lefttree;
            }

            return current;

        }
     

    }
}
