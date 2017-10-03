using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.AVLTree
{

    /*The AVL trees are more balanced compared to Red Black Trees, but they may cause more rotations during insertion and deletion.
    //So if your application involves many frequent insertions and deletions, then Red Black trees should be preferred. 
    //And if the insertions and deletions are less frequent and search is more frequent operation, then AVL tree should be preferred over Red Black Tree.

    AVL tree is a self-balancing Binary Search Tree (BST) where the difference between heights of left and right subtrees cannot be more than one for all nodes.

    Why AVL Trees?
     Most of the BST operations (e.g., search, max, min, insert, delete.. etc) take O(h) time where
     * h is the height of the BST. 
     * The cost of these operations may become O(n) for a skewed Binary tree. 
     * If we make sure that height of the tree remains O(Logn) after every insertion and deletion, then we can guarantee an upper bound of O(Logn) for all these operations.
     * The height of an AVL tree is always O(Logn) where n is the number of nodes in the tree 
     * 
     * Note: Height propery have to maintained during each rotation
     * See the picture for all four case
     * 1) Left Right case:
     *             We need to rotate the root.left to left (we will get the result node and update that to root.left = newnode) and then
     *             rotate root to right
     * 2) Left Left Case:
     *             rotate root to right
     * 3) Right Left Case:
     *             We need to rotate the root.Right to Right (we will get the result node and update that to root.Right = newnode) and then
     *             rotate root to left
     * 4) Right Right Case:
     *              rotate root to left
     *       


    //https://www.youtube.com/watch?v=YKt1kquKScY  */

    public class AVLTree
    {
        public AVLTreeNode Root { get; set; }

        public AVLTreeNode Insert(AVLTreeNode root, int data)
        {
            /* 1.  Perform the normal BST insert */
            if (root == null)
            {
                //When we create a node, we default the height to 1
                AVLTreeNode node = new AVLTreeNode(data);
                return node;
            }

            if (data <= root.Data)
            {
                //goes into left subtree
                root.Left = Insert(root.Left, data);
            }
            else
            {
                root.Right = Insert(root.Right, data);
            }

            /* 2. Update height of this ancestor node */
            // Max of Height of (Left, Right) + 1

            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;

            /* 3. Get the balance factor of this ancestor node to check whether
             this node became unbalanced */
            //since the balancing factor is calculated by (Height of Left - Height of right)
            //If a root is unbalaced which means the height of left - right is greater than 1 or lesser than -1
            //balacefactor is > 1, then left subtree of root has more height than right and that caused the unbalacing tree
            //balacefactore is < -1, then right subtree of root has more height and that caused the unbalancing
            int balacefactor = GetBalanceFactor(root);


            
            if (balacefactor < -1 || balacefactor > 1)
            {
                //just for test test
                int i = balacefactor;


            }
            //check if the root is unbalaced for all the four case and do the rotation


            //Logic:
            //root.left != null and balacefactor > 1 tells that root.left has more height
            //to differentiate btn left left case and left right case we have an another condition
            //Check whether the data (from the recursion params) that we added goes to the left or right of Root.Left
            //If data < root.Left.Data then the data (new node) has went to left of root.Left and that caused the unbalacing  -- Left Left Case
            //If data > root.Right.Data then the data has went to right of root.Left and that caused unbalancing --Left Right case
            //similarly right right case and right left case

            //left left case
            if (root.Left != null && balacefactor > 1 && data < root.Left.Data)
            {
                //then rotate the root to right
                return RotateRight(root);
            }

            //right right case
            if (root.Right != null && balacefactor < -1 && data > root.Right.Data)
            {
                 //then rotate the root to left
                return RotateLeft(root);
            }
            //left right case
            if (root.Left != null && balacefactor > 1 && data > root.Left.Data)
            {
                //then we need to do 2 rotations
                root.Left = RotateLeft(root.Left);
                //and then rotate the root to right
                return RotateRight(root);
            }
            //right left case
            if (root.Right != null && balacefactor < -1 & data < root.Right.Data)
            {
                root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }

            //if the root is balaced we return the actula unchanged root

            return root;


        }

        //this is a custome insert function
        //http://stackoverflow.com/questions/15197058/number-of-distinct-smaller-elements-on-left-for-each-position-in-a-array
        //http://www.geeksforgeeks.org/count-smaller-elements-on-right-side/

        /*Logic
         * We need to find the number of smallest element on the right of a particular array element
         * So we add the array elements from right to left to a self balancing tree (eg AVL tree)
         * so that for each element we can see how many element were smaller from right side
         
         * Here the counter parameter is returned on each insertion and it has the count of no of elements smaller than the data added
         * We create the insert logic of the AVL tree ( ie with the height propery and rotation management)
         * We add a new property called size to maintain the size of each node. Size of leaf node is 1
         * When we add a data to a root
         *   a) if the data is lesser than the root it goes to the right and the counter can still remind the same
         *   b) if the data is greater than the root then it means that data is greater then root + size of root.Left so we have to handle this logic on the recurssion
         * The rotation really doesn't matter but we have to recalculate the height and size property during rotation
         * Counter is the variable that tells how many elements are lesser than the data without going to each elements with the help of size property
         * 
         * Time Complexity: O(nLogn)
            Auxiliary Space: O(n)
        
         */
        public AVLTreeNode Insert(AVLTreeNode root, int data, ref int counter)
        {
            /* 1.  Perform the normal BST insert */
            if (root == null)
            {
                AVLTreeNode node = new AVLTreeNode(data);
                return node;
            }

            //Custom prob logic to skip inserting when the value already exists
            if (data == root.Data)
            {
                return root;
            }

            if (data < root.Data)
            {
                //goes into left subtree
                root.Left = Insert(root.Left, data, ref counter);
            }
            else
            {
                if (data == 12)
                {
                    int test = root.Data;
                }
                root.Right = Insert(root.Right, data, ref counter);
                //whenever we go to the right of a node to insert which means that current data is greater than the root
                //or the root is smaller than the current..the root might have left subtree so the counter should be update
                counter = counter + GetSize(root.Left) + 1; //1 is for the root itselft which is smaller and 
                //source: http://stackoverflow.com/questions/15197058/number-of-distinct-smaller-elements-on-left-for-each-position-in-a-array
                
            }

            /* 2. Update height of this ancestor node */
            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;
            //updat the size of the root
            root.Size = GetSize(root.Left) + GetSize(root.Right) + 1;

            /* 3. Get the balance factor of this ancestor node to check whether
             this node became unbalanced */
            int balacefactor = GetBalanceFactor(root);



            if (balacefactor < -1 || balacefactor > 1)
            {
                //just for test test
                int i = balacefactor;


            }
            //check if the root is unbalaced for all the four case and do the rotation

            //left left case
            if (root.Left != null && balacefactor > 1 && data < root.Left.Data)
            {
                //then rotate the root to right
                return RotateRight(root);
            }

            //right right case
            if (root.Right != null && balacefactor < -1 && data > root.Right.Data)
            {
                //then rotate the root to left
                return RotateLeft(root);
            }
            //left right case
            if (root.Left != null && balacefactor > 1 && data > root.Left.Data)
            {
                //then we need to do 2 rotations
                root.Left = RotateLeft(root.Left);
                //and then rotate the root to right
                return RotateRight(root);
            }
            //right left case
            if (root.Right != null && balacefactor < -1 & data < root.Right.Data)
            {
                root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }

            //if the root is balaced we return the actula unchanged root

            return root;


        }

        public AVLTreeNode Delete(AVLTreeNode root, int data)
        {
            /* 1.  Perform the normal BST delete */
            if (root == null)
            {

                return null;
            }

            if (data < root.Data)
            {
                //goes into left subtree
                root.Left = Delete(root.Left, data);
            }
            else if (data > root.Data)
            {
                root.Right = Delete(root.Right, data);
            }
            else
            {
                // node with only one child or no child
                if ((root.Left == null) || (root.Right == null))
                {
                    //when equal we need to delete this node
                    AVLTreeNode temp = root.Left != null ? root.Left : root.Right;
                    //if it has no childres
                    if (temp == null)
                    {
                        root = null;
                    }
                    //if it has one children
                    else
                    {
                        root = temp;
                    }
                }
                else
                {
                    //if it has two children 
                    // node with two children: Get the inorder successor (smallest
                    // in the right subtree)
                    AVLTreeNode temp = minValueNode(root.Right);

                    // Copy the inorder successor's data to this node
                    root.Data = temp.Data; //here we overwrite the node to be deleted with temp.data

                    // Delete the inorder successor
                    root.Right = Delete(root.Right, temp.Data);

               


                }
            }


            // If the tree had only one node then return
            if (root == null)
                return root;

            /* 2. Update height of this ancestor node */
            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;

            /* 3. Get the balance factor of this ancestor node to check whether
             this node became unbalanced */
            int balacefactor = GetBalanceFactor(root);



            if (balacefactor < -1 || balacefactor > 1)
            {
                //just for test test
                int i = balacefactor;


            }
            //check if the root is unbalaced for all the four case and do the rotation
            //here the logic is hard to follow with GetBalanceFactor
            //left left case
            if (root.Left != null && balacefactor > 1 && GetBalanceFactor(root.Left) >= 0)
            {
                //then rotate the root to right
                return RotateRight(root);
            }

            //right right case
            if (root.Right != null && balacefactor < -1 && GetBalanceFactor(root.Right) <= 0)
            {
                //then rotate the root to left
                return RotateLeft(root);
            }
            //left right case
            if (root.Left != null && balacefactor > 1 && GetBalanceFactor(root.Left) < 0)
            {
                //then we need to do 2 rotations
                root.Left = RotateLeft(root.Left);
                //and then rotate the root to right
                return RotateRight(root);
            }
            //right left case
            if (root.Right != null && balacefactor < -1 & GetBalanceFactor(root.Right) > 0)
            {
                root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }

            //if the root is balaced we return the actula unchanged root

            return root;


        }

        // A utility function to get height of the tree
        public int GetHeight(AVLTreeNode node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }


        public int GetSize(AVLTreeNode node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Size;
        }
        // Get Balance factor of node N
        public int GetBalanceFactor(AVLTreeNode node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {  //get difference of left and right subtree height
                return (GetHeight(node.Left) - GetHeight(node.Right));
            }
        }

        /* Given a non-empty binary search tree, return the node with minimum
   key value found in that tree. Note that the entire tree does not
   need to be searched. */
        public AVLTreeNode minValueNode(AVLTreeNode node)
        {
            AVLTreeNode current = node;

            /* loop down to find the leftmost leaf */
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        // A utility function to get maximum of two integers
        public int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // A utility function to right rotate subtree rooted with y
        // See the diagram given above.
        //      T1, T2, T3 and T4 are subtrees.
        //       z                                      y 
        //      / \                                   /   \
        //     y   T4      Right Rotate (z)          x      z
        //    / \          - - - - - - - - ->      /  \    /  \ 
        //   x   T3                               T1  T2  T3  T4
        //  / \
        //T1   T2


        public AVLTreeNode RotateRight(AVLTreeNode node)
        {

            AVLTreeNode node1 = node.Left;
            AVLTreeNode node2 = node1.Right;
            //rotation
            node1.Right = node;
            node.Left = node2;
            //make sure to adjust the height of the nodes for which we changed the children z and y
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            node1.Height = Math.Max(GetHeight(node1.Left), GetHeight(node1.Right)) + 1;

            //As per custom problem we need to update the size property as well
            node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;
            node1.Size = GetSize(node1.Left) + GetSize(node1.Right) + 1;

            //return the new parent
            return node1;
        }


        //   z                                y
        // /  \                            /   \ 
        //T1   y     Left Rotate(z)       z      x
        //    /  \   - - - - - - - ->    / \    / \
        //   T2   x                     T1  T2 T3  T4
        //       / \
        //     T3  T4

        public AVLTreeNode RotateLeft(AVLTreeNode z)
        {
            AVLTreeNode n1 = z.Right;
            AVLTreeNode n2 = n1.Left;

            //rotate
            n1.Left = z;
            z.Right = n2;
            //make sure to adjust height of z and y
            n1.Height = Math.Max(GetHeight(n1.Left), GetHeight(n1.Right)) + 1;
            z.Height = Math.Max(GetHeight(z.Left), GetHeight(z.Right)) + 1;


            //As per custom problem we need to update the size property as well
            n1.Size = GetSize(n1.Left) + GetSize(n1.Right) + 1;
            z.Size = GetSize(z.Left) + GetSize(z.Right) + 1;

            return n1;

        }
    }

    public class AVLTreeNode
    {
        public int Data { get; set; }
        public AVLTreeNode Left { get; set; }
        public AVLTreeNode Right { get; set; }
        public int Height { get; set; }
        public int Size { get; set; } //custom problem

        public AVLTreeNode(int data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
            this.Height = 1; //for new node which will be added to leaf node h =1
            this.Size = 1;
        }

   
    }
}
