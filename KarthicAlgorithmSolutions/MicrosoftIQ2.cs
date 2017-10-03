using Puzzles.DataStructures.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
  public partial class MicrosoftIQ2 : Form
  {
    public MicrosoftIQ2()
    {
      InitializeComponent();
    }

    private void button4_Click(object sender, EventArgs e)
    {

      //Input array:
      //1 0 1 1 0
      //0 1 1 1 0
      //1 1 1 1 1
      //1 0 1 1 1
      //1 1 1 1 1


      // Result array:
      //1 1 1 0 1
      //1 0 1 1 0
      //1 1 1 1 1
      //1 1 1 1 1
      //1 1 1 0 0


      //int[,] input = new int[,]{{1,0,1,1,0},
      //                                {0,1,1,1,0},
      //                                {1,1,1,1,1},
      //                                {1,0,1,1,1},
      //                                {1,1,1,1,1}
      //      };


      int[,] input = new int[,] {{1,2,3,4},
                                    {5,6,7,8},
                                    {9,10,11,12},
                                    {13,14,15,16}};
                                   


      List<TwoDimensitionArrayStore> store = ConvertArray(input);
      store = RotateDataStructureBy90Degree(store, input.GetLength(0));

      //Now the store has the rotated value and we know that store has only 0 value..
      //Loop through the matrix and if the store has the point (x and y) then set the value to 0 else 1
      for (int i = 0; i < input.GetLength(0); i++)
      {
        for (int j = 0; j < input.GetLength(1); j++)
        {
          //remember here i becomes as y -co-oridnate and j b
          if (IsAvaiableInStore(j, i, store))
          {
             //input[i, j] = 0;
             input[i, j] = GetStoreValue(j, i, store);
          }
          else
          {
             input[i, j] = 1;
          }
        }
      }
      
      //Grab the result
      StringBuilder sb = new StringBuilder();

       for (int i = 0; i < input.GetLength(0); i++)
      {

         sb.Append("{");

        for (int j = 0; j < input.GetLength(1); j++)
        {
              sb.Append(input[i,j].ToString()).Append(",");
        }

        sb.Append("}+");
      }

      this.textBox3.Text = sb.ToString();


    }



    public List<TwoDimensitionArrayStore> ConvertArray(int[,] input)
    {

       List<TwoDimensitionArrayStore> storearray = new List<TwoDimensitionArrayStore>();

        for(int i=0; i < input.GetLength(0); i++)
      {

          for(int j=0; j < input.GetLength(1); j++)
        {

             //if(input[i , j ] == 0)
             //{
                //x means row number and y means column number
                 storearray.Add(new TwoDimensitionArrayStore(j , i ,input[i , j]));
             //}
        }
      }

      return storearray;
    }

    public List<TwoDimensitionArrayStore> RotateDataStructureBy90Degree(List<TwoDimensitionArrayStore> store, int matrixsize)
    {
      int temp;

      //swap the elements to rotate to 90
      foreach (TwoDimensitionArrayStore item in store)
      {
        //put y on temp
        //when u rotate the two dimension matrix by 90 degree..this is want happens
        //00 01 02 03 
        //10 11 12 13
        //20 21 22 23
        //30 31 32 33

        //after rotating to 90
        //30 20 10 00
        //31 21 11 01
        //32 22 12 02
        //33 23 13 03

        //Note in a two matrix the value are in the (y,x) format bcoz here x denotes columns and y denotes rows
        //when rotated 90 degree the x of original becomes y of rotated
        //so the new will point will be y1 = x and x1 = (matrixlength -1) -y
        temp = item.point.yAxis;
        item.point.yAxis = item.point.xAxis;
        item.point.xAxis = (matrixsize - 1) - temp;

        //item.point.yAxis = (matrixsize - 1) - item.point.xAxis;
        //item.point.xAxis = temp;




          
      }

      return store;

    }


    public bool IsAvaiableInStore(int x, int y, List<TwoDimensitionArrayStore> store)
    {

      foreach (var item in store)
      {

        if (item.point.xAxis == x && item.point.yAxis == y)
        {
             return true;
        }
      }


      return false;
    }


    public int GetStoreValue(int x, int y, List<TwoDimensitionArrayStore> store)
    {
        foreach (var item in store)
        {
            if (item.point.xAxis == x && item.point.yAxis == y)
            {
                return item.Data;
            }
        }

        return -1;
    }

    private void button3_Click(object sender, EventArgs e)
    {

      string filepath = this.textBox5.Text;
      KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();

      //we need to store the binary tree in file system/database or send across network
      //we can store either in xml, txt file or byte[] etc
      //i am going to store in txt file
      
      //To convert bt into string there are two methods
       //Method 1: Store both pre-order and In-order array traversal and construct tree from using both array
       //Method 2: We need to handle null value and do pre-order traversal (current, left, right) Note: In-order traversal doesn't work for this method becasue we can't figure what caused null pointer

      StringBuilder sb = new StringBuilder();

      PreOrderTraversalWithNullHandle(tree.Root, sb);

      //Method1: simple way to write string to text file
     // System.IO.File.WriteAllText(filepath, sb.ToString());

      //Method2: To convert the string to byte[]

      byte[] buffer = GetBytes(sb.ToString());
      StoreByteSIntofile(filepath, buffer);


      ///Deserailization is done here..
      //Take the byte[] array and convert into binary tree or take the file and convert into bt

      byte[] filebuffer = ConvertFileIntoByte(filepath);

      //we know that this file contains only binary tree char..convert this to string
      string output = GetString(filebuffer);

      string[] nodes = output.Split(',');

      //build tree with the string

      //KarthicBinaryTree<int> tree2 = new KarthicBinaryTree<int>();
      //tree2.Root = BuildTree(nodes, 0);

      NodeIndex test = DeserializeAndBuildTree(nodes, 0);


      StringBuilder testoutput = new StringBuilder();

      PreOrderTraversalWithNullHandle(test.treenode, testoutput);

      this.textBox6.Text = testoutput.ToString();

 
    }

    public void PreOrderTraversalWithNullHandle(KarthicBTNode<int> node, StringBuilder sb)
    {

      if (node == null)
      {
        sb.Append("#").Append(",");
        return;
      }

      //current
      sb.Append(node.Data).Append(",");

      //left
      PreOrderTraversalWithNullHandle(node.Left, sb);

      //right
      PreOrderTraversalWithNullHandle(node.Right, sb);

    }

    public static byte[] GetBytes(string str)
    {

      //important when we need to give the size of bytes
      //We need to know how many bytes each char is (every value type has some size of bytes..i guess the char takes 2 bytes)
      //multiply with the length
      byte[] bytes = new byte[str.Length * sizeof(char)];
      System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
      return bytes;
    }


    public static byte[] GetBytes(int[] array)
    {

      //important when we need to give the size of bytes
      //We need to know how many bytes each char is (every value type has some size of bytes..i guess the char takes 2 bytes)
      //multiply with the length
      byte[] bytes = new byte[array.Length * sizeof(int)];
      System.Buffer.BlockCopy(array, 0, bytes, 0, bytes.Length);
      return bytes;
    }

    public static string GetString(byte[] bytes)
    {
      char[] chars = new char[bytes.Length / sizeof(char)];
      System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
      return new string(chars);


    }

    public static int[] GetArrayFromByte(byte[] bytes)
    {
      int[] array = new int[bytes.Length / sizeof(int)];
      System.Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
      return array;
    }


    public static char[] GetCharArrayFromByte(byte[] bytes)
    {
        char[] array = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
        return array;
    }


    public void StoreByteSIntofile(string filepath, byte[] buffer)
    {
        using (FileStream file = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.Write))
        {
            file.Write(buffer, 0, buffer.Length);
            file.Close();
        }
     

    }


    public byte[] ConvertFileIntoByte(string filepath)
    {

      byte[] buffer = null;

        using(FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read))
        {
          buffer = new byte[fs.Length];
          //fs.Read(buffer, 0, fs.Length);
          fs.Read(buffer, 0, buffer.Length);
          fs.Close();
              
        }
        return buffer;
    }


    public NodeIndex DeserializeAndBuildTree(string[] input, int index)
    {

        //base case
      if (index >= input.Length)
      {
        return null;

      }

      if (input[index] == "#")
      {
        //this index is read..move to next
          index++;

        NodeIndex nullnode = new NodeIndex();
        nullnode.treenode = null;
        nullnode.StartIndex = index;
        return nullnode;
      }
      //the string was built using pre-order so do pre-order build

      NodeIndex node = new NodeIndex();
      node.treenode = new KarthicBTNode<int>(Convert.ToInt32(input[index]));

      index++;
      //Important here we can actually change the value of index and make index++ 
      NodeIndex leftnode = DeserializeAndBuildTree(input, index);
      node.treenode.Left = leftnode.treenode;

      //Important start reading from the index left by the left
      NodeIndex rightnode = DeserializeAndBuildTree(input, leftnode.StartIndex);
      node.treenode.Right = rightnode.treenode;
     
      //before retuning the function carry the index value to the next recurrsive function
      node.StartIndex = rightnode.StartIndex;

      return node;
       
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

    private void button1_Click(object sender, EventArgs e)
    {

      //Lets buid a n-ary tree
      ///////////////////1
      ///////2///////////3//////////////////4
      ///5//////6//////////////////////7///8////9
      ///

//      For a k-ary tree with height h, the upper bound for the maximum number of leaves is k^h.
//The height h of a k-ary tree does not include the root node, with a tree containing only a root node having a height of 0.
//The total number of nodes in a perfect k-ary tree is (k^{h+1} - 1)/(k-1), while the height h is
      KarthicAryTree tree = TreeHelper.SetUpAryTreeWithThreeChildren();

      string result = tree.BreathFirstTraversal(tree.root);

      //serialize the n-ary tree..convert to array and to bytes
      int[] array = ConvertAryTreeIntoString(tree);

      string filepath = this.textBox2.Text;
      byte[] buffer = GetBytes(array);
      //write the output in file
      StoreByteSIntofile(filepath, buffer);

      //deserialize the tree from byte[] or from file

      byte[] bufferfromfile = ConvertFileIntoByte(filepath);
      int[] arrayfrombyte = GetArrayFromByte(bufferfromfile);

      KarthicAryTree tree2 = BuildAryTreeFromArray(arrayfrombyte, 3);

      string result2 = tree2.BreathFirstTraversal(tree2.root);

      this.textBox1.Text = result2;

      bool output = result.Equals(result2);
       

    }

    public int[] ConvertAryTreeIntoString(KarthicAryTree tree)
    {
       //Logic
      //we need to do level by level traversal and store the value in array using below formula
      // cth children = k * i + 1 + c 
      //where k is the maximum number of children the node can have
      //i is the index of the parent
      //c is the index of the children relative to parent (1st children index o, 2nd children index 1)
      // parent index = i - 1 / k
      // where i is the index of the child and k is the k-ary number




      //how to know the size of the array..
       //i guess the interview will give the no of nodes in n-ary tree OR
       //the interview won't give you anything

        //If the interviewer giving no info about the no of nodes or height, we can calculate no of nodes by height

        //No of nodes of any tree (binary or n-ary)
        //No. of node = k^(h+1) -1 / (k -1) where k is 2 for binary and h is height of the tree

        //calculate the no of nodes using the forumula
        // No. of node = k^(h+1) -1 / (k -1)
        //This is the same formula for binary tree as well..for binary tree k = 2 so the formula becomes
        //2^(h+1) -1

        //Get the height of the n-ary tree
        int height = tree.GetHeight(tree.root);
       

        //for eg 3-ary tree (0 to 3 children)
        // (k^{h+1} - 1) / (k - 1)

        int k = 3; //For 3-ary tree 
        int totalnodes = (int)Math.Ceiling(Math.Pow(k, (height + 1)) - 1) / (k - 1);


        int[] array = new int[totalnodes]; //each node can have max of 3 children..so take the max possible size

       int index = 0;

       array[index] = tree.root.Data;

       Queue<AryNodeWithIndex> myqueue = new Queue<AryNodeWithIndex>();
       //the root's index is 0
       myqueue.Enqueue(new AryNodeWithIndex(tree.root, 0));

       while (myqueue.Count != 0)
       {
         AryNodeWithIndex current = myqueue.Dequeue();
         int parentindex = current.index;
         //for every parent we start from 0
         int childnumber = 0;
         foreach(KarthicAryTreeNode child in current.node.Children)
         {
           int childindex = k * parentindex + 1 + childnumber;
           array[childindex] = child.Data;
           childnumber += 1;
           //visit child
           myqueue.Enqueue(new AryNodeWithIndex(child, childindex));
         }
       }
       return array;
        
    }


    public KarthicAryTree BuildAryTreeFromArray(int[] array, int k)
    {

      //int[] array has value..remember if the value is zero mean there is no node...there might be lot of empty value..
      //bcoz all the node in n-ary may or may not have n children

      //build the root

      List<KarthicAryTreeNode> nodelist = new List<KarthicAryTreeNode>(); //we got to maintain the node list
      Dictionary<int, KarthicAryTreeNode> ht = new Dictionary<int, KarthicAryTreeNode>();
    //ht key is index and value is tree node


      KarthicAryTree tree = new KarthicAryTree();
      tree.root = new KarthicAryTreeNode(array[0]);
      //nodelist.Add(tree.root);
      ht.Add(0, tree.root);

      //we already build the root so start from the 1
      for (int index = 1; index < array.Length; index++)
      {
        //only if the value is greater than 0 we build the tree
          if (array[index] > 0)
          {
              //get the parent index
              int parentindex = (index - 1) / k;

              //since we build level by level the parent node would already be present when the code comes here
              //get the parent node
              KarthicAryTreeNode parent = ht[parentindex];
              //build child
              KarthicAryTreeNode child = new KarthicAryTreeNode(array[index]);
              //nodelist.Add(child);
              ht.Add(index, child);
              parent.Children.Add(child);
          }
          //else
          //{
          //    //Update 5/5/2015
          //    //If the code comes here mean an array[index] == 0 found
          //    //that means no children exists for the given index
          //    //to maintain the index of nodelist so that parent is maintained in right order
          //    nodelist.Add(null);
          //}

      }

      return tree;
    }

    public class AryNodeWithIndex
    {
      public KarthicAryTreeNode node { get; set; }
      public int index { get; set; }

      public AryNodeWithIndex(KarthicAryTreeNode node, int index)
      {
        this.node = node;
        this.index = index;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      KarthicBinaryTree<int> tree = TreeHelper.SetUpBinaryTree();
      StringBuilder sb = new StringBuilder();
      tree.LevelZigZacTraversal(tree.Root, sb);

      this.textBox1.Text = sb.ToString();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox7.Text);

      //Logic 
      //sort the given array
      //if the input size is odd, return the middle element
      // if the input size is even, calculate the mean of middle and middle + 1 element
      double median;

      Array.Sort(input);

      if (input.Length % 2 == 0)
      {
        //even
        int middle = input.Length / 2;
        median = (input[middle - 1] + input[middle]) / 2;
        

      }
      else
      {
        int middle = input.Length / 2;

        median = input[middle];
      }


      this.textBox8.Text = median.ToString();

    }

    private void button6_Click(object sender, EventArgs e)
    {

    }

    private void button7_Click(object sender, EventArgs e)
    {
        int[] sides = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox10.Text);

        this.textBox9.Text = FindTriangleTypeBySides(sides[0], sides[1], sides[2]).ToString();

    }

    public int FindTriangleTypeBySides(int a, int b, int c)
    {
        //check all the sides are greater than 0 and sum of any two sides should be greater than third side
        if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
        {
            return (int)TriangleType.Error;
        }
        else if (a == b && b == c)
        {
            //all the sides are equal
            return (int)TriangleType.Equilateral;
        }
        else if (a == b || b == c || a == c)
        {
            //any two side are equal
            return (int)TriangleType.Isosceles;
        }
        else
        {  
            //The sides are unequal
            return (int) TriangleType.Scalene;
        }
    }


    public enum TriangleType
    {
        Scalene = 1,
        Isosceles = 2,
        Equilateral = 3,
        Error = 4
    }

    private void button8_Click(object sender, EventArgs e)
    {
        int[,] input = new int[,] {  {1,2,3,4},
                                     {5,6,7,8},
                                     {9,10,11,12},
                                     {13,14,15,16}};

         RotateMatrixInPlace(ref input);


        //Grab the result
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < input.GetLength(0); i++)
        {

            sb.Append("{");

            for (int j = 0; j < input.GetLength(1); j++)
            {
                sb.Append(input[i, j].ToString()).Append(",");
            }

            sb.Append("}+ ");
        }

        this.textBox3.Text = sb.ToString();
        
    }

    //Layer by Layer approach
    //Rotating a matrix in place can be done only by Square matrix that is N*N..
     //Imagine this
    //Input array:
    //1 5  9  13
    //2 6  10 14 
    //3 7  11 15 
    //4 8  12 16

    //After rotating 90 degree the resulting array will be

    // Result array:
    // 4  3  2  1
    // 8  7  6  5
    //12 11  10 9
    //16 15  14 13

    // 00 01 02 03
    // 10 11 12 13
    // 20 21 22 23
    // 30 31 32 33

    private void RotateMatrixInPlace(ref int[,] matrix)
    {

        int nooflayers = matrix.GetLength(0) / 2;

        //iterate layer by layer
        for (int layer = 0; layer < nooflayers; layer++)
        {
            //The width for the first and second ring differs by length
            //For 4 * 4 layer 1 the width will be 0 to 3
            //          layer 2 the width will be 1 to 2

            int widthstart = layer;
            int widthend = (matrix.GetLength(0) - 1) - widthstart;
     
            //You got the layer..Now iterate for the cells in the layer
            //Important: Iterate only till the last before cell bcoz last cell will already be changed by the first cell rotation...work on eg on 4*4 matrix
            for (int cell = widthstart; cell < widthend; cell++)
            {
                //Offset for 1st layer will be {0,1,2,3}
                //Offset for 2nd layer will be {0,1}
                //You can use (Matrix.GetLength(0) -1 - cell) instead of offsett..but offset will be neat
               
                int offset = cell - widthstart;

                //Put the top on the temp
                //Top is 00 01 02 03 for 1st ring
                //Top is 11 12       for 2nd ring
                int temp = matrix[widthstart, cell];   //Important we are using cell instead of offset

                //Put the left corner value into top...start from  bottom left to top left
                //Left is 30 20 10 00 for 1st ring
                //Left is 21 11       for 2nd ring
                matrix[widthstart, cell] = matrix[widthend - offset,widthstart];

                //Put the bottom on the left
                //start the bottom from right to left
                // Bottom is 33 32 31 30 for 1st ring
                // Bottom is 22 21       for 2nd ring
                matrix[widthend - offset, widthstart] = matrix[widthend, widthend - offset];

                //Put the right on bottom
                //Right is 03 13 23 33 for 1st ring
                //right is 12 22 for 2nd ring
                matrix[widthend, widthend - offset] = matrix[cell, widthend];

                //Put top on right
                matrix[cell, widthend] = temp;

            }


        }

        

    }


    //[TestMethod]
    //public void TestValidTriangles()
    //{
    //    //Assuming FindTriangleTypeBySides() method is in Triangle class
    //    Triangle triangle = new Triangle();
    //    var result = triangle.FindTriangleTypeBySides(10, 10, 10); //Equilateral
    //    Assert.AreEqual("Equilateral", result);

    //}


    //      10,10,10 Equilateral
    //10,5,4 scalene
    //10,10,5 Isosceles
    //0,10,5 invalid
    //0,0,0 invalid
    //-7,10,10 invalid
    //10,2,3 invalid(sum should be greater)


    public int[,] RotateMatrix(int[,] matrix)
    {
        int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];
        //Input array:
        //1 0 1 1 0
        //0 1 1 1 0
        //1 1 1 1 1
        //1 0 1 1 1
        //1 1 1 1 1


        // Result array:
        //1 1 1 0 1
        //1 0 1 1 0
        //1 1 1 1 1
        //1 1 1 1 1
        //1 1 1 0 0

        //so 00  will be  01
        //   01  will be  13
        //   02  will be  23
        // .................
        //   10  will be 02

        //Given Row1 and col1 a

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {

                //Row2 and col2 will be Row2 = Col1 and Col2 = (A.length - 1) - Row1
                result[col, (matrix.GetLength(0) - 1) - row] = matrix[row, col];
            }

        }

        return result;

    }

    private void button9_Click(object sender, EventArgs e)
    {
        //Input array:
        //1 0 1 1 0
        //0 1 1 1 0
        //1 1 1 1 1
        //1 0 1 1 1
        //1 1 1 1 1


        // Result array:
        //1 1 1 0 1
        //1 0 1 1 0
        //1 1 1 1 1
        //1 1 1 1 1
        //1 1 1 0 0



        int[,] input = new int[,] {  {1,2,3,4},
                                     {5,6,7,8},
                                    {9,10,11,12},
                                    {13,14,15,16}};


        int[,] result  = RotateMatrix(input);


        //Grab the result
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < result.GetLength(0); i++)
        {

            sb.Append("{");

            for (int j = 0; j < result.GetLength(1); j++)
            {
                sb.Append(result[i, j].ToString()).Append(",");
            }

            sb.Append("}+ ");
        }

        this.textBox3.Text = sb.ToString();

    }

    private void button10_Click(object sender, EventArgs e)
    {
        //Lets buid a n-ary tree
        ///////////////////A
        ///////B///////////C//////////////////D
        ///E//////F//////////////////////G///H////I///J
        ///


        AryTree<char> tree = TreeHelper.SetAryTreewithMaxFourchildrens();

        //Serialization is done by using dfs end of children pointer...
        string result = tree.DepthFirstSearch(tree.root);



        string filepath = this.textBox2.Text;
        byte[] buffer = GetBytes(result);
        //write the output in file
        StoreByteSIntofile(filepath, buffer);

        //deserialize the tree from byte[] or from file

        byte[] bufferfromfile = ConvertFileIntoByte(filepath);
        char[] arrayfrombyte = GetCharArrayFromByte(bufferfromfile);

        AryTree<char> tree2 = DeserializeTreeFromArray(arrayfrombyte, result.Length);

        string result2 = tree2.DepthFirstSearch(tree2.root);

        this.textBox1.Text = result2;

        bool output = result.Equals(result2);
       
    }


    private AryTree<char> DeserializeTreeFromArray(char[] array, int count)
    {
        if (array.Length == 0)
        {
            return null;
        }

        AryTree<char> tree = new AryTree<char>();
       
        int index = 0;
        //Add first element to root
        Stack<AryTreeNode<char>> mystack = new Stack<AryTreeNode<char>>();
        AryTreeNode<char> root = new AryTreeNode<char>(array[index]);
        tree.root = root;

        mystack.Push(root);
        index++;

        AryTreeNode<char> parent = null;

        while (mystack.Count != 0 && index < count)
        {
            if (array[index] == '(')
            {
                mystack.Pop();
   
            }
            else
            {
                parent = mystack.Peek();
                AryTreeNode<char> child = new AryTreeNode<char>(array[index]);
                parent.Children.Add(child);
                mystack.Push(child);
       
            }
            index++;
        }

        return tree;
    }

  }
}
