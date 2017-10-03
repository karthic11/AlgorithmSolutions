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
    public partial class CandidateQuestionsPage2 : Form
    {
        public CandidateQuestionsPage2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //  - Get digits of N in positional order 
            //- Find first digit M that is not in ascending order (searching from right to left) 
            //- If all digits are in ascending order from right to left, then return N 
            //- Find the smallest digit P that is to the right of M and is also larger than M 
            //- Swap positions of M and P 
            //- Sort digits after original position of M in ascending order from left to right 
            //- Build and return from digits

            int number = Convert.ToInt32(this.textBox6.Text);

            int output = GetNextHigherNumWitSameDigits(number);

            this.textBox1.Text = output.ToString();
        }

        public static int[] IntToIntArr(int Num)
        {
            int r = 0;
            List<int> li = new List<int>();
            while (Num > 0)
            {
                r = Num % 10;
                li.Add(r);
                Num = Num / 10;
            }
            li.Reverse();
            return li.ToArray();
        }

        public static int IntArrToInt(int[] NumArr)
        {
            int Num = 0;
            for (int i = 0; i< NumArr.Length; i++)
            {
                Num = Num * 10 + NumArr[i];
            }
            return Num;
        }

        public static int GetNextHigherNumWitSameDigits(int Num)
        {
            if (Num < 10) return Num;

            int[] NumArr = IntToIntArr(Num);
            int prev = NumArr[NumArr.Length - 1];

            int i = NumArr.Length - 2;

            //Find the first number which is smaller than the prev 
            //number travelling from rear end
            for (; i >= 0; i--)
            {
                if (prev > NumArr[i]) 
                {
                    break;
                }
                prev = NumArr[i];
            }

            if (i == -1) return Num;

            int smallIndex = i+1;

            //Find the smallest number greater than 'NumArr[i]' towards 
            //right of 'NumArr[i]'.
            for (int j = i+2; j < NumArr.Length; j++)
            {
                if (NumArr[j] > NumArr[i] && NumArr[j] < NumArr[smallIndex])
                    smallIndex = j;
            }
         
            //swap the two index
            int temp = NumArr[smallIndex];
            NumArr[smallIndex] = NumArr[i];
            NumArr[i] = temp;
            Array.Sort(NumArr, i + 1, NumArr.Length - i - 1);
            return IntArrToInt(NumArr);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.textBox3.Text;

            List<String> output = new List<string>();
            ConvertStarToBinary(input, output);

        }

        public void ConvertStarToBinary(string input, List<String> ouput)
        {
            int lastindex = input.LastIndexOf('*');
            if (lastindex == -1)
            {
                ouput.Add(input);
                return;
            }

            //we have two * with 0 and * with 1
            //form a string with * to 0
            char[] chararray = input.ToCharArray();
            chararray[lastindex] = '0';
            ConvertStarToBinary(new string(chararray), ouput);

            chararray[lastindex] = '1';
            ConvertStarToBinary(new string(chararray), ouput);



        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //set tree with the following nodes

            KarthicBinaryTree<int> bt = new KarthicBinaryTree<int>();
            bt.Root = new KarthicBTNode<int>(20);

            //set level 1
            bt.Root.Left = new KarthicBTNode<int>(8);
            bt.Root.Right = new KarthicBTNode<int>(22);

            //set level 2

            bt.Root.Left.Left = new KarthicBTNode<int>(4);
            bt.Root.Left.Right = new KarthicBTNode<int>(12);


            bt.Root.Right.Right = new KarthicBTNode<int>(25);

            bt.Root.Left.Right.Left = new KarthicBTNode<int>(10);
            bt.Root.Left.Right.Right = new KarthicBTNode<int>(14);


            this.textBox4.Text = PrintBorderOfBinaryTree(bt.Root);


        }

        // http://www.geeksforgeeks.org/boundary-traversal-of-binary-tree/
        private string PrintBorderOfBinaryTree(KarthicBTNode<int> Node)
        {
            StringBuilder sb = new StringBuilder();
            if (Node != null)
            {
                sb.Append(Node.Data).Append(",");

                PrintLeftBorder(Node.Left, sb);

                PrintBinaryTreeLeafNodes(Node.Left, sb);

                PrintBinaryTreeLeafNodes(Node.Right, sb);

                PrintRightBorder(Node.Right, sb);

            }

            return sb.ToString();

        }

        private void PrintBinaryTreeLeafNodes(KarthicBTNode<int> Node, StringBuilder sb)
        {
            if (Node == null)
            {
                return;
            }

            if (Node.Left == null && Node.Right == null)
            {
                sb.Append(Node.Data).Append(",");
                return;
            }

            PrintBinaryTreeLeafNodes(Node.Left, sb);
            PrintBinaryTreeLeafNodes(Node.Right, sb);

        }

        //print all left border except the leaf node to avoid duplicates
        private void PrintLeftBorder(KarthicBTNode<int> node, StringBuilder sb)
        {
            if (node.Left != null && node.Right != null)
            {

                sb.Append(node.Data).Append(",");
                PrintLeftBorder(node.Left, sb);

            }

        }


        // A function to print all right boundry nodes, except a leaf node
        // Print the nodes in BOTTOM UP manner
        private void PrintRightBorder(KarthicBTNode<int> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left == null && node.Right == null)
            {
                return;
            }

            if (node.Right != null)
            {
                PrintRightBorder(node.Right, sb);
                sb.Append(node.Data).Append(",");
            }
            //if (node != null)
            //{
            //    if (node.Right != null)
            //    {

            //        sb.Append(node.Data).Append(",");
            //        PrintRightBorder(node.Right, sb);

            //    }
            //}
        }

    }
}
