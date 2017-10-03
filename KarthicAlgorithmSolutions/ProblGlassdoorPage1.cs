using Puzzles.DataStructures.Common;
using Puzzles.DataStructures.Other;
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
    public partial class ProblGlassdoorPage1 : Form
    {
        public ProblGlassdoorPage1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //     Input: Array of buildings
            //{ (1,11,5), (2,6,7), (3,13,9), (12,7,16), (14,3,25),
            //  (19,18,22), (23,13,29), (24,4,28) }

            List<Building> buildings = new List<Building>();
            Building building1 = new Building(1, 11, 5);
            Building building2 = new Building(2, 6, 7);
            Building building3 = new Building(3, 13, 9);
            Building building4 = new Building(12, 7, 16);
            Building building5 = new Building(14, 3, 25);
            Building building6 = new Building(19, 18, 22);
            Building building7 = new Building(23, 13, 29);
            Building building8 = new Building(24, 4, 28);

            buildings.Add(building1);
            buildings.Add(building2);
            buildings.Add(building3);
            buildings.Add(building4);
            buildings.Add(building5);
            buildings.Add(building6);
            buildings.Add(building7);
            buildings.Add(building8);

            int[] heightMapToxAxis = new int[30];

            foreach (Building building in buildings)
            {
                int xAxisStart = building.left;
                int xAxisEnd = building.right;
                for (int heightcell = xAxisStart; heightcell <= xAxisEnd; heightcell++)
                {
                    heightMapToxAxis[heightcell] = Math.Max(building.height, heightMapToxAxis[heightcell]);
                }

            }

            //update: This logic doesn't work in certain scenario..so wrong
            //In order to get the skyline 
            //check adjacent x-axis value check x-1 and x
            //if the height of the x increases then take (x and height of x)
            //if the height of the x decreases then the skyline dropped at x-1 to small height take (x-1 and height of x)
            List<Strip> strips = new List<Strip>();
            //we can ignore (0,0)
            for (int i = 1; i < heightMapToxAxis.Length; i++)
            {
                //ignore when the both heights are equal
                int heightofpreviuscell = heightMapToxAxis[i - 1];
                int heightofcurrentcell = heightMapToxAxis[i];

                if (heightofpreviuscell != heightofcurrentcell)
                {
                    //there is a difference in height
                    if (heightofcurrentcell > heightofpreviuscell)
                    {
                        //if the change is increase then
                        strips.Add(new Strip(i, heightofcurrentcell));
                    }
                    else
                    {
                        //if the change is lower then the skyline dropped at x-1 to small height take 
                        strips.Add(new Strip(i - 1, heightofcurrentcell));
                    }
                }
            }


            //
            int test = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Input: Array of buildings
            //{ (1,11,5), (2,6,7), (3,13,9), (12,7,16), (14,3,25),
            //  (19,18,22), (23,13,29), (24,4,28) }

            List<Building> buildings = new List<Building>();
            Building building1 = new Building(1, 11, 5);
            Building building2 = new Building(2, 6, 7);
            Building building3 = new Building(3, 13, 9);
            Building building4 = new Building(12, 7, 16);
            Building building5 = new Building(14, 3, 25);
            Building building6 = new Building(19, 18, 22);
            Building building7 = new Building(23, 13, 29);
            Building building8 = new Building(24, 4, 28);

            buildings.Add(building1);
            buildings.Add(building2);
            //buildings.Add(building3);
            //buildings.Add(building4);
            //buildings.Add(building5);
            //buildings.Add(building6);
            //buildings.Add(building7);
            //buildings.Add(building8);

            SkyLine result = FindSkyline(buildings.ToArray(), 0, buildings.Count - 1);

            StringBuilder sb = new StringBuilder();
            foreach (Strip s in result.strips)
            {
                if (s != null)
                {
                    sb.Append("(").Append(s.left).Append(",").Append(s.height).Append(")");
                }
            }

            string output = sb.ToString();

            //           Skyline for given buildings is
            //(1, 11),  (3, 13),  (9, 0),  (12, 7),  (16, 3),  (19, 18), 
            //(22, 3),  (23, 13),  (29, 0),

        }


        private SkyLine FindSkyline(Building[] building, int low, int high)
        {
            //here when we divide and seperate recursion there are two possibilities
            //low will be equal to high

            //if we handle low = high, low won't be greater to high and high won't go lesser than low
      
            if (low == high)
            {
                //You have one building, get skylines from the building
                //eg (1,11,5) will give (1,11) and (5,0)
                // update 06/28/2017
                // A skyline is a collection of rectangular strips. 
                // A rectangular strip is represented as a pair (left, ht) where left is x coordinate of left side of strip and ht is height of strip
                // here (5, 0) because 5 is the x corordinate of the left side of the strip (2nd strip) of height 0. ..
                SkyLine result = new SkyLine(2);
                result.append(new Strip(building[low].left, building[low].height));
                result.append(new Strip(building[low].right, 0));
                return result;
            }
            //default case low will be lesser than high

            int middle = (low + high) / 2;

            SkyLine skyline1 = FindSkyline(building, low, middle);
            SkyLine skyline2 = FindSkyline(building, middle + 1, high);

            //order is important here merge skyline1 to skyline2
            return MergeTwoSkylines(skyline1, skyline2);


        }

        //Note: order is important. This function append the skyline2 to skyline1
        //same as calling skyline1.merge(skyline2)
        public SkyLine MergeTwoSkylines(SkyLine skyline1, SkyLine skyline2)
        {
            // Create a resultant skyline with capacity as sum of two
            // skylines
            SkyLine result = new SkyLine(skyline1.GetSize() + skyline2.GetSize());

            // The idea is similar to merge of merge sort, start from first strips of two skylines, compare x coordinates.
            //Pick the strip with smaller x coordinate and add it to result. 
            //The height of added strip is considered as maximum of current heights from skyline1 and skyline2.
            //Example to show working of merge:

            //  Height of new Strip is always obtained by takin maximum of following
            //     (a) Current height from skyline1, say 'h1'.  
            //     (b) Current height from skyline2, say 'h2'
            //  h1 and h2 are initialized as 0. h1 is updated when a strip from
            //  SkyLine1 is added to result and h2 is updated when a strip from 
            //  SkyLine2 is added.

            int h1 = 0, h2 = 0;

            int leftpointer = 0; //left half pointer
            int rightpointer = 0; //right half pointer

            while (leftpointer < skyline1.GetSize() && rightpointer < skyline2.GetSize())
            {
                int mergeheight = 0;

                if (skyline1.strips[leftpointer].left < skyline2.strips[rightpointer].left)
                {
                    h1 = skyline1.strips[leftpointer].height;
                    mergeheight = Math.Max(h1, h2);
                    result.append(new Strip(skyline1.strips[leftpointer].left, mergeheight));
                    leftpointer++;
                }
                else
                {
                    //don't worry about equal condition. result.append already has logic to skip skyline if the previous skyline has same height and left
                    h2 = skyline2.strips[rightpointer].height;
                    mergeheight = Math.Max(h1, h2);
                    result.append(new Strip(skyline2.strips[rightpointer].left, mergeheight));
                    rightpointer++;

                }
            }

            // If there are strips left in this skyline or other
            // skyline
            while (leftpointer < skyline1.GetSize())
            {
                result.append(skyline1.strips[leftpointer]);
                leftpointer++;
            }
            while (rightpointer < skyline2.GetSize())
            {
                result.append(skyline2.strips[rightpointer]);
                rightpointer++;
            }

            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //     Input: Array of buildings
            //{ (1,11,5), (2,6,7), (3,13,9), (12,7,16), (14,3,25),
            //  (19,18,22), (23,13,29), (24,4,28) }

            List<Building> buildings = new List<Building>();
            Building building1 = new Building(1, 11, 5);
            Building building2 = new Building(2, 6, 7);
            Building building3 = new Building(3, 13, 9);
            Building building4 = new Building(12, 7, 16);
            Building building5 = new Building(14, 3, 25);
            Building building6 = new Building(19, 18, 22);
            Building building7 = new Building(23, 13, 29);
            Building building8 = new Building(24, 4, 28);

            buildings.Add(building1);
            buildings.Add(building2);
            buildings.Add(building3);
            buildings.Add(building4);
            buildings.Add(building5);
            buildings.Add(building6);
            buildings.Add(building7);
            buildings.Add(building8);

            SkyLine result = GetSkyline(buildings);

            StringBuilder sb = new StringBuilder();
            foreach (Strip s in result.strips)
            {
                if (s != null)
                {
                    sb.Append("(").Append(s.left).Append(",").Append(s.height).Append(")");
                }
            }

            string output = sb.ToString();

            //           Skyline for given buildings is
            //(1, 11),  (3, 13),  (9, 0),  (12, 7),  (16, 3),  (19, 18), 
            //(22, 3),  (23, 13),  (29, 0),

        }

        private SkyLine GetSkyline(List<Building> buildings)
        {
            SkyLine result = null;

            foreach (Building building in buildings)
            {
                SkyLine skyline = GetSkylinesFromSingleBuilding(building);

                if (result == null)
                {
                    result = skyline;
                }
                else
                {
                    result = MergeTwoSkylines(result, skyline);
                }

            }

            return result;
        }

        private SkyLine GetSkylinesFromSingleBuilding(Building building)
        {
            //there are two strips for single building
            SkyLine result = new SkyLine(2);
            result.append(new Strip(building.left, building.height));
            result.append(new Strip(building.right, 0));
            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Given two line segements and find whether these two lines interesect
            //Each line consist of two MathPoints start and end

            //so given four MathPoints where p1 and q1 form a line and p2 and q2 form another line
            MathPoint p1 = new MathPoint(10, 0);
            MathPoint q1 = new MathPoint(0, 10);
            MathPoint p2 = new MathPoint(0, 0);
            MathPoint q2 = new MathPoint(10, 0);

            //bool result = DoLinesIntersect(p1, q1, p2, q2);
            PointIntersection pi = new PointIntersection();

            bool result = pi.DoLinesIntersect(p1, q1, p2, q2);



        }




        private void button4_Click(object sender, EventArgs e)
        {
            //http://www.geeksforgeeks.org/given-a-set-of-line-segments-find-if-any-two-segments-intersect/
            //  PseudoCode:
            //The following pseudocode doesn’t use heap. It simply sort the array.

            //sweepLineIntersection(Points[0..2n-1]):
            //1. Sort Points[] from left to right (according to x coordinate) or use min heap to compare based on x-axis value

            //2. Create an empty Self-Balancing BST T. It will contain all active line 
            //   Segments ordered by y coordinate.

            //// Process all 2n points 
            //3. for i = 0 to 2n-1

            //    // If this point is left end of its line  
            //    if (Points[i].isLeft) 
            //       T.insert(Points[i].line())  // Insert into the tree

            //       // Check if this points intersects with its predecessor and successor
            //       if ( doIntersect(Points[i].line(), T.pred(Points[i].line()) )
            //         return true
            //       if ( doIntersect(Points[i].line(), T.succ(Points[i].line()) )
            //         return true

            //    else  // If it's a right end of its line
            //       // Check if its predecessor and successor intersect with each other
            //       if ( doIntersect(T.pred(Points[i].line(), T.succ(Points[i].line()))
            //         return true
            //       T.delete(Points[i].line())  // Delete from tree

            //4. return False
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KarthicBST<int> tree = TreeHelper.SetUpBinarySearchTree();
            StringBuilder sb = new StringBuilder();
            tree.MorrisInOrderTraversal(tree.Root, sb);
            string output = sb.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //        26
            //      /   \
            //    10     3
            //  /    \     \
            //4      6      3

            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();

            bt.Root = new KarthicBTNode<int>(26);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(10);
            bt.Root.Right = new KarthicBTNode<int>(3);

            //set level 2
            bt.Root.Left.Left = new KarthicBTNode<int>(4);
            bt.Root.Left.Right = new KarthicBTNode<int>(6);

            bt.Root.Right.Right = new KarthicBTNode<int>(3);

            bool result = IsSumTree(bt.Root);




        }

        private bool IsSumTree(KarthicBTNode<int> root)
        {
            //for null
            if (root == null)
            {
                return true;
            }
            //Important base case: return true for leaf node
            if (root.Left == null && root.Right == null)
            {
                return true;
            }

            int sumofleftsubtree = FindSum(root.Left);
            int sumofrightsubtree = FindSum(root.Right);

            if ((root.Data == sumofleftsubtree + sumofrightsubtree) && IsSumTree(root.Left) && IsSumTree(root.Right))
            {
                return true;
            }

            return false;
        }

        private int FindSum(KarthicBTNode<int> root)
        {

            if (root == null)
            {
                return 0;
            }
            int sum = root.Data;
            if (root.Left != null)
            {
                sum += root.Left.Data;
            }

            if (root.Right != null)
            {
                sum += root.Right.Data;
            }

            return sum;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //        26
            //      /   \
            //    10     3
            //  /    \     \
            //4      6      3

            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();

            bt.Root = new KarthicBTNode<int>(26);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(10);
            bt.Root.Right = new KarthicBTNode<int>(3);

            //set level 2
            bt.Root.Left.Left = new KarthicBTNode<int>(4);
            bt.Root.Left.Right = new KarthicBTNode<int>(6);

            bt.Root.Right.Right = new KarthicBTNode<int>(3);
            //http://www.geeksforgeeks.org/check-if-a-given-binary-tree-is-sumtree/

            bool result = IsSumTreeOptimized(bt.Root);

        }

        //Logic: we check whether the childer is a sum tree and then go back to parents
        //We go to parent only when children is true..and in parent we don't have to calculate sum again..it will be twice as the current.data...

        // The Method 1 uses sum() to get the sum of nodes in left and right subtrees. The method 2 uses following rules to get the sum directly.
        //1) If the node is a leaf node then sum of subtree rooted with this node is equal to value of this node.
        //2) If the node is not a leaf node then sum of subtree rooted with this node is twice the value of this node (Assuming that the tree rooted with this node is SumTree).
        private bool IsSumTreeOptimized(KarthicBTNode<int> root)
        {
            if (root == null)
            {
                return true;
            }

            if (root.Left == null && root.Right == null)
            {
                return true;
            }

            if (IsSumTreeOptimized(root.Left) && IsSumTreeOptimized(root.Right))
            {
                int leftsubtreesum = 0;
                int rightsubstreesum = 0;
                //if (root.Left == null)
                //{
                //    leftsubtreesum = 0;
                //}
                if (root.Left != null)
                {
                    if (IsLeafNode(root.Left))
                    {
                        leftsubtreesum = root.Left.Data;
                    }
                    else
                    {
                        //we alredy know the children is valid sum tree
                        //so the value of child will be twice the current
                        leftsubtreesum = 2 * (root.Left.Data);
                    }
                }
                if (root.Right != null)
                {
                    if (IsLeafNode(root.Right))
                    {
                        rightsubstreesum = root.Right.Data;
                    }
                    else
                    {
                        rightsubstreesum = 2 * (root.Right.Data);
                    }
                }

                return (root.Data == (leftsubtreesum + rightsubstreesum));
            }

            return false;
        }

        private bool IsLeafNode(KarthicBTNode<int> current)
        {
            if (current == null)
                return true;

            if (current.Left == null && current.Right == null)
                return true;

            return false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //http://www.geeksforgeeks.org/convert-an-arbitrary-binary-tree-to-a-tree-that-holds-children-sum-property/
            //Given Tree
            //            50
            //         /     \     
            //       /         \
            //     7             2
            //   / \             /\
            // /     \          /   \
            //3        5      1      30

            //Each node is sum of the left child + right child
            //note: Only Incrementing the node is allowed..no decrement
            //Convert to 

            //          50
            //        /    \     
            //      /        \
            //    19           31
            //   / \           /  \
            // /     \       /      \
            //14      5     1       30


            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();

            bt.Root = new KarthicBTNode<int>(50);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(7);
            bt.Root.Right = new KarthicBTNode<int>(2);

            //set level 2
            bt.Root.Left.Left = new KarthicBTNode<int>(3);
            bt.Root.Left.Right = new KarthicBTNode<int>(5);

            bt.Root.Right.Left = new KarthicBTNode<int>(1);
            bt.Root.Right.Right = new KarthicBTNode<int>(30);


        }

        //logic:
        //Do post order travelsal
        //Traverse given tree in post order to convert it, i.e., first change left and right children to hold the children sum property then change the parent node.

        //Let difference between node’s data and children sum be diff.

        //diff = node’s children sum - node’s data  
        //If diff is 0 then nothing needs to be done.

        //If diff > 0 ( node’s data is smaller than node’s children sum) increment the node’s data by diff.

        //If diff < 0 (node’s data is greater than the node's children sum) then increment one child’s data.
        //We can choose to increment either left or right child if they both are not NULL. Let us always first increment the left child.
        //Incrementing a child changes the subtree’s children sum property so we need to change left subtree also. 
        //So we recursively increment the left child. If left child is empty then we recursively call increment() for right child.
        private void ConvertTreeToHoldChildrenSum(KarthicBTNode<int> root)
        {
            int left_data = 0;
            int right_data = 0;
            int diff = 0;
            //don't do anything for null and leaf node
            if (root == null || (root.Left == null && root.Right == null))
            {
                return;
            }
            else
            {

                ConvertTreeToHoldChildrenSum(root.Left);
                ConvertTreeToHoldChildrenSum(root.Right);

                /* If left child is not present then 0 is used
       as data of left child */
                if (root.Left != null)
                {
                    left_data = root.Left.Data;
                }

                if (root.Right != null)
                {
                    right_data = root.Right.Data;
                }

                diff = (left_data + right_data) - root.Data;

                if (diff > 0)
                {
                    //root.data is lesser than  (left_data + right_data) then we can increment root.Data
                    root.Data = root.Data + diff;
                }
                /* THIS IS TRICKY --> If node's data is greater than children sum,
                    then increment subtree by diff */
                else if (diff < 0)
                {
                    IncrementSubTreeByDifference(root, -diff); // -diff is used to make diff positive
                }
            }



        }

        /* This function is used to increment subtree by diff */
        private void IncrementSubTreeByDifference(KarthicBTNode<int> root, int diff)
        {
            /* IF left child is not NULL then increment it */
            if (root.Left != null)
            {
                root.Left.Data = root.Left.Data + diff;
                //  // Recursively call to fix the descendants of node->left
                IncrementSubTreeByDifference(root.Left, diff);
            }
            else if(root.Right != null)
            {
                // Else increment right child
                root.Right.Data = root.Right.Data + diff;
                // Recursively call to fix the descendants of node->right
                IncrementSubTreeByDifference(root.Right, diff);

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {

            //List of Nodes
            //http://www.careercup.com/question?id=5648527329853440
            //id,parent,weight 
            //10,30,1   
         
            //30,0,10 
            //20,30,2 
            //50,40,3 
            //40,30,4 

            //we parse the given input using ht and get the structure of the tree like this
            //                     0
            //            30   
            //         10  20 40
            //                  50

            NodeWithWeight node0 = new NodeWithWeight(0,0);
            NodeWithWeight node30 = new NodeWithWeight(30, 10);
            NodeWithWeight node10 = new NodeWithWeight(10,1);
            NodeWithWeight node20 = new NodeWithWeight(20, 2);
            NodeWithWeight node40 = new NodeWithWeight(40, 4);
            NodeWithWeight node50 = new NodeWithWeight(50, 3);

            node30.Parent = node0;
            node10.Parent = node30;
            node20.Parent = node30;
            node40.Parent = node30;
            node50.Parent = node40;
          
         

            List<NodeWithWeight> nodes = new List<NodeWithWeight>();
            nodes.Add(node0);
            nodes.Add(node30);
            nodes.Add(node10);
            nodes.Add(node20);
            nodes.Add(node40);
            nodes.Add(node50);

            PrintSubTreeWeight(nodes);


        }

       //Given is the list of node and it has parent, value and weight
       //To calculate total weight of the subtree we need to know the parent and child relation that is Parent and List<children>
       //we can either use ht to build the structure or modify the node to have this property
       //Find the root of the tree
       //And then use DFS to find the tree's weight..go to children first and then sibling

        private void PrintSubTreeWeight(List<NodeWithWeight> nodes)
        {

            //Build ht with key as parent and value as list of children
            Dictionary<NodeWithWeight, List<NodeWithWeight>> htparentchild = new Dictionary<NodeWithWeight, List<NodeWithWeight>>();
            //Build another ht with key as childnode and value as parent ..this is used to find the root
            //we can use childparent map to find the root but here since we already loop through the nodes, we can find the root without the use of this ht
            //Dictionary<NodeWithWeight, NodeWithWeight> htchildparentmap = new Dictionary<NodeWithWeight, NodeWithWeight>();
            NodeWithWeight root = null;
            foreach (NodeWithWeight node in nodes)
            {
                //we have to build parent and list of chilren
                if (node.Parent != null)
                {
                    if (!htparentchild.ContainsKey(node.Parent))
                    {
                        htparentchild.Add(node.Parent, new List<NodeWithWeight>());
                    }

                    htparentchild[node.Parent].Add(node);
                }
                else
                {
                    root = node;
                }

            }

            //void function that set the weight prop of the tree  //test get the root weight
            int result =  CalculateTreeWeight(root, htparentchild);

           
      
            
        }

        private int CalculateTreeWeight(NodeWithWeight root,  Dictionary<NodeWithWeight, List<NodeWithWeight>> htparentchild)
        {
            if (root == null)
            {
                return 0;
            }

            int Weight = root.Weight;

            //get the weight of the subtrees
            if (htparentchild.ContainsKey(root))
            {
                List<NodeWithWeight> childrens = htparentchild[root];
                foreach (NodeWithWeight child in childrens)
                {
                    Weight += CalculateTreeWeight(child, htparentchild);
                }
            }

            //set the prop of the tree 
            root.TreeWeight = Weight;

            return Weight;
            
           

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //List of Nodes
            //http://www.careercup.com/question?id=5648527329853440
            //id,parent,weight 
            //10,30,1   

            //30,0,10 
            //20,30,2 
            //50,40,3 
            //40,30,4 

            //we parse the given input using ht and get the structure of the tree like this
            //                     0
            //            30   
            //         10  20 40
            //                  50

            NodeWithWeight node0 = new NodeWithWeight(0, 0);
            NodeWithWeight node30 = new NodeWithWeight(30, 10);
            NodeWithWeight node10 = new NodeWithWeight(10, 1);
            NodeWithWeight node20 = new NodeWithWeight(20, 2);
            NodeWithWeight node40 = new NodeWithWeight(40, 4);
            NodeWithWeight node50 = new NodeWithWeight(50, 3);

            node30.Parent = node0;
            node10.Parent = node30;
            node20.Parent = node30;
            node40.Parent = node30;
            node50.Parent = node40;



            List<NodeWithWeight> nodes = new List<NodeWithWeight>();
            nodes.Add(node0);
            nodes.Add(node30);
            nodes.Add(node10);
            nodes.Add(node20);
            nodes.Add(node40);
            nodes.Add(node50);

            PrintSubTreeWeight2(nodes);

        }


        //Given is the list of node and it has parent, value and weight
        //To calculate total weight of the subtree we need to know the parent and child relation that is Parent and List<children>
        //we can either use ht to build the structure or modify the node to have this property
        //Find the root of the tree
        //And then use DFS to find the tree's weight..go to children first and then sibling
        private void PrintSubTreeWeight2(List<NodeWithWeight> nodes)
        {
            NodeWithWeight root = null;
           //Change the structure of the tree to have list<childre>
            foreach (NodeWithWeight node in nodes)
            {
               
                if (node.Parent != null)
                {
                    node.Parent.Childs.Add(node);
                }
                else
                {
                    root = node;
                }

            }

            int totalweight = CalculateTreeWeight(root);


        }

        private int CalculateTreeWeight(NodeWithWeight root)
        {
            if (root == null)
            {
                return 0;
            }

            int Weight = root.Weight;

            //get the weight of the subtrees
            if (root.Childs.Count > 0)
            {
                foreach (NodeWithWeight child in root.Childs)
                {
                    Weight += CalculateTreeWeight(child);
                }
            }

            //set the prop of the tree 
            root.TreeWeight = Weight;

            return Weight;



        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Given two line segements and find whether these two lines interesect
            //Gayle 6th edition page 464


            //so given four MathPoints where p1 and q1 form a line and p2 and q2 form another line
            MathPoint p1 = new MathPoint(10, 0);
            MathPoint q1 = new MathPoint(0, 10);
            MathPoint p2 = new MathPoint(0, 0);
            MathPoint q2 = new MathPoint(10, 0);

            PointIntersection pi = new PointIntersection();

            MathPoint result = pi.GetIntersectionPointUsingSlopes(p1, q1, p2, q2);
            if (result != null)
            {
                //we found intersction point
            }
            

       
        }


    }

    //public class MathPoint
    //{
    //    public int x { get; set; }
    //    public int y { get; set; }

    //    public MathPoint(int x, int y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }
    //}

    public class NodeWithWeight
    {
        public int Value { get; set; }
        public NodeWithWeight Parent { get; set; }
        public int Weight { get; set; }
        public int TreeWeight { get; set; }

        //custom method2
        public List<NodeWithWeight> Childs { get; set; }

        public NodeWithWeight(int value, int weight)
        {
            this.Value = value;
            this.Weight = weight;

            //custom
            Childs = new List<NodeWithWeight>();
        }
    }

}
