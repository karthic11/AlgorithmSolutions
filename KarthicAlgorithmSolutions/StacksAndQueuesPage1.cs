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
    public partial class StacksAndQueuesPage1 : Form
    {
        public StacksAndQueuesPage1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string input = this.textBox1.Text;

            string[] list = input.Split(',');

            KarthicStack mystack = new KarthicStack();

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));
                
            }

            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < list.Length; j++)
            {
                sb.Append(mystack.Pop()).Append(',');
            }

            this.textBox2.Text = sb.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.textBox1.Text;

            string[] list = input.Split(',');

            KarthicArrayStack mystack = new KarthicArrayStack(100);

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));

            }

            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < list.Length; j++)
            {
                sb.Append(mystack.Pop()).Append(',');
            }

            this.textBox2.Text = sb.ToString();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //This is a design question..Don't expect me to write code here...
            //Given a single array say with the length of 300
            //Concept is to have three stacks of n/3 size where n is the size of array
            //Push and Pop method takes stach number as the additional paramerter
            //user array to keep track of the pointers in all the three stacks

            //The code for this implementation is not on this solution..need to add from Gayle book
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //this is also design implemenation question

            string input = this.textBox4.Text;

            string[] list = input.Split(',');

            KarthicStackWithMin mystack = new KarthicStackWithMin();

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));

            }



            this.textBox3.Text = mystack.GetMin().ToString();


        }

        private void StacksAndQueues_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Solution2 with extra stacj

            string input = this.textBox4.Text;

            string[] list = input.Split(',');

            KarthicStack mystack = new KarthicStack();

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));

            }

            this.textBox3.Text = mystack.GetMin().ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string input = this.textBox6.Text;

            string[] list = input.Split(',');

            SetOfStacks mystack = new SetOfStacks();

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));

            }

            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < list.Length; j++)
            {
                sb.Append(mystack.Pop()).Append(',');
            }

            this.textBox5.Text = sb.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //This is another design question
            //solution is made on the stack class

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form f = new StackAndQueuesPage2();
            f.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }
    }
}
