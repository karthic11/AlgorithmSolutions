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
using System.Xml;
using Puzzles.DataStructures.Tree.BinaryTree;

namespace Puzzles
{
    public partial class GLModerateProblems_Page2 : Form
    {
        public GLModerateProblems_Page2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Given the defention
            //family  1
            //person 2
            //firstnmae 3  lastname 4 state 5


            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\kanmanik\Google Drive\NGU2013-Puzzles\KarthicAlgorithmSolutions\KarthicAlgorithmSolutions\Resources\XMLFiles\sample.xml");

            XmlElement element = doc.DocumentElement;

            Hashtable ht = new Hashtable();
            ht.Add("family", 1);
            ht.Add("person", 2);
            ht.Add("firstname", 3);
            ht.Add("lastname", 4);
            ht.Add("state", 5);

            this.textBox3.Text = EncodeXMLElementToString(element, ht);
            


        }

        private string EncodeXMLElementToString(XmlElement element, Hashtable ht)
        {

            StringBuilder sb = new StringBuilder();
            EncodeElement(element, sb, ht);

            return sb.ToString();

        }


        public void EncodeElement(XmlElement root, StringBuilder sb, Hashtable ht)
        {

            // <family lastname="kanmani" state="tamilnadu">
            //<person firstname="karthic"> you can do it</person>
            //</family>

            //encode the element start name tag
            encode(root.Name, ht, sb);
            
            //encode the attributes
            foreach (XmlAttribute attribute in root.Attributes)
            {
                encode(attribute.Name, ht, sb);
                sb.Append(attribute.Value).Append(" ");
            }
            //encode the end tag
            sb.Append("0").Append(" ");
            //encode the value..not the value might be elements as well

            //root.value is not working..if there is value it comes as root.InnerText in .NET and the same exisits for child nodes
            if (root.Value != null && root.Value != "")
            {
                
                sb.Append(root.Value).Append(" ");
            }
            else
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        EncodeElement((XmlElement)node, sb, ht);
                    }
                }
            }
            //encode the element end name tag
            sb.Append("0").Append(" ");
          
        }

        public void encode(string key, Hashtable ht, StringBuilder sb)
        {
            if (ht.ContainsKey(key))
            {
                sb.Append(ht[key]).Append(" ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int output = Random7();

            this.textBox1.Text = output.ToString();

        }


        private int Random7()
        {

            //given random 5 function..Implement random7()
            //This sln will not give result in 1/7 probabily bcoz
            //random5() will give range (0 to 4) in 1/5
            //so 1/5 + 1/5 will be the result probabilit
            //and also the value are not equally distributed..
            //Range of value we will get  
            // 0 from (0 to 4)     +   0 to 4    = 0 to 4
            // 1 from (0 to 4)     +   0 to 4    = 1 to 5
            // 2 from  (0 to 4)        0 to 4    = 2 to 6
            // 3 from  (0 to 4)  0 to 4    = 3 to 7
            // 4 from  (0 to 4)  0 to 4    = 4 to 8
            //Note here we can get 6 in multiple ways   2 + 4, 3 + 3, 4 + 2 but get 0 in only one way 0 + 0 thus it is not equally distributed
            int value = Random5() + Random5();

            return value % 7;

        }

        private int Random5()
        {
            Random rd = new Random();
            return rd.Next(0, 4);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int value = Random5() + Random5();

            int output = value % 7;

            this.textBox1.Text = output.ToString();


        }

        //This method will make nondeterministic number of calls to random5() but gives output in 1/7
        private int Random7Optimal()
        {

            //given random 5 function..Implement random7()
            //This sln will give result in 1/7 probabily bcoz
            //random5() will give range (0 to 4) in 1/5
            //so 1/5 + 1/5 will be the result probabilit
             //Range of value we will get  
            // 5 * 0 from (0 to 4)   0 to 4    = 0 to 4
            // 5 * 1 from (0 to 4)   0 to 4    = 5 to 9
            // 5 * 2 from  (0 to 4)  0 to 4    = 10 to 14
            // 5 * 3 from  (0 to 4)  0 to 4    = 15 to 19
            // 5 * 4 from  (0 to 4)  0 to 4    = 20 to 24  
            //Note here all the values has only one way of forming 6 can be formed 5 + 1 no other way
            //Since we use mod 7 ..we discard the values 21,22,23 and 24 bcoz this will give more weight for rand7() to give 0 to 3
            //after discarding the value 0 to 20 here the values 0 to 6 has equal 1/7 probability

            while (true)
            {

                int value = 5 * Random5() + Random5();

                if (value < 21)
                {
                    return value % 7;
                }

            }
     
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox2.Text);
            int sum = Convert.ToInt32(this.textBox4.Text);

            string output = FindPairs(sum, array);
            this.textBox5.Text = output;

        }


        private string FindPairs(int sum, int[] array)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable ht = new Hashtable();

            foreach (int item in array)
            {
                int difference = sum - item;
                if (ht.ContainsKey(difference))
                {
                    //if the hashtable contains the key difference mean..the ht has the pair
                    //difference + item  = sum
                    sb.Append(" ( " + difference + ", " + item + " )");
                }

                //here value doesn't matter we might maintain the fre of the key
                if (ht.ContainsKey(item))
                {
                    ht[item] = (int)ht[item] + 1;
                }
                else
                {
                    ht.Add(item, 1);
                }

            }

            return sb.ToString();

        }


        private string FindPairsBySort(int sum, int[] array)
        {
            StringBuilder sb = new StringBuilder();
            Array.Sort(array);

           //keep track of the index.. set the pointer to first and last index
            int first = 0;
            int last = array.Length - 1;

            while (first < last)
            {

                int value = array[first] + array[last];

                //if the value is equal to the sum, we found a pair..print and increment the pointers
                if (value == sum)
                {
                    sb.Append(" ( " + array[first] + ", " + array[last] + " )");
                    first++;
                    last--;
                }
                else
                {
                    //else the value might be lesser or greater
                    if (value < sum)
                    {
                        //remember the array is sorted..so no more complement of first will be found..so go for next
                        first++;
                    }
                    else //if the value is larger than sum..then complement of last will not be there. so go decrement the last
                    {
                        last--;
                    }
                }
            }

            return sb.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            int[] array = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox2.Text);
            int sum = Convert.ToInt32(this.textBox4.Text);

            string output = FindPairsBySort(sum, array);
            this.textBox5.Text = output;

        }

        private void button6_Click(object sender, EventArgs e)
        {


            //Construct a binary tree as the given example
            ////////////4/////////
            ///////2 ////////5///
            ////1////3/////////6
            //0////////////////////
            
            //Expected output as a doubly linked list  0,1,2,3,4,5,6

            BiNode root = new BiNode(4);
 
            root.Node1 = new BiNode(2);
            root.Node2 = new BiNode(5);

            root.Node1.Node1 = new BiNode(1);
            root.Node1.Node2 = new BiNode(3);
            root.Node2.Node1 = null;
            root.Node2.Node2 = new BiNode(6);

            root.Node1.Node1.Node1 = new BiNode(0);

            BinaryTreeToLinkedList obj = new BinaryTreeToLinkedList();


            string output = obj.ConvertUsingAdditionDataStructure(root);
            this.textBox6.Text = output;

        }

        private void button7_Click(object sender, EventArgs e)
        {

            //Construct a binary tree as the given example
            ////////////4/////////
            ///////2 ////////5///
            ////1////3/////////6
            //0////////////////////

            //Expected output as a doubly linked list  0,1,2,3,4,5,6

            //BiNode root = new BiNode(4);

            //root.Node1 = new BiNode(2);
            //root.Node2 = new BiNode(5);

            //root.Node1.Node1 = new BiNode(1);
            //root.Node1.Node2 = new BiNode(3);
            //root.Node2.Node1 = null;
            //root.Node2.Node2 = new BiNode(6);

            //root.Node1.Node1.Node1 = new BiNode(0);

            //test case as in the picture

            BiNode root = new BiNode(1);
            root.Node1 = new BiNode(2);
            root.Node2 = new BiNode(3);

            root.Node1.Node1 = new BiNode(4);
            root.Node1.Node2 = new BiNode(5);
            root.Node2.Node1 = new BiNode(6);
            root.Node2.Node2 = new BiNode(7);

            BinaryTreeToLinkedList obj = new BinaryTreeToLinkedList();


            string output = obj.ConvertWithoutUsingAdditionDataStructure(root);
            this.textBox6.Text = output;

        }

        private void button8_Click(object sender, EventArgs e)
        {

            //Construct a binary tree as the given example
            ////////////4/////////
            ///////2 ////////5///
            ////1////3/////////6
            //0////////////////////

            //Expected output as a doubly linked list  0,1,2,3,4,5,6

            BiNode root = new BiNode(4);

            root.Node1 = new BiNode(2);
            root.Node2 = new BiNode(5);

            root.Node1.Node1 = new BiNode(1);
            root.Node1.Node2 = new BiNode(3);
            root.Node2.Node1 = null;
            root.Node2.Node2 = new BiNode(6);

            root.Node1.Node1.Node1 = new BiNode(0);

            BinaryTreeToLinkedList obj = new BinaryTreeToLinkedList();


            string output = obj.ConvertOptimalUsingCircularLogic(root);
            this.textBox6.Text = output;


        }








    
    }
}
