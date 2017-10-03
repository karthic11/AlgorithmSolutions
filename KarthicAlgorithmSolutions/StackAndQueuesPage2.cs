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
    public partial class StackAndQueuesPage2 : Form
    {
        public StackAndQueuesPage2()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string input = this.textBox1.Text;

            string[] list = input.Split(',');

            //create three tower
            Tower[] towers = new Tower[3];
            for (int i=0; i < 3; i++)
            {
                towers[i] = new Tower(list.Count(), i);
            }

            //Load disks in tower a
            for(int j = list.Count(); j >= 1; j--)
            {
                towers[0].Push(Convert.ToInt16(list[j -1]));

            }

            
            //move n disks from tower a to tower c
            towers[0].MoveDisks(list.Count(), towers[2], towers[1]);
           

            //check the tower c

            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < list.Length; j++)
            {
                sb.Append(towers[2].Pop()).Append(',');
            }

            this.textBox2.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            string input = this.textBox4.Text;

            input = "1,2,3,4,5";

            string[] list = input.Split(',');

            KarthicQueue<int> myqueue = new KarthicQueue<int>();

            foreach (var i in list)
            {
                myqueue.EnQueue(Convert.ToInt16(i));

            }

            //test case 1
            StringBuilder sb = new StringBuilder();

            //for (int j = 0; j < list.Length; j++)
            //{
          
            //    sb.Append(myqueue.DeQueue().ToString()).Append(',');
            //}
            //has 5 items

            //test case 2:

            //Dequeue 2 items


            for (int j = 0; j < 2; j++)
            {
                myqueue.DeQueue();
            }

            //Enqueue 3 items

            for (int i = 0; i < 3; i++)
            {
                myqueue.EnQueue(i + 1);
            }

            //dequeue 6 items

            for (int j = 0; j < 6; j++)
            {
                sb.Append(myqueue.DeQueue().ToString()).Append(',');
            }

            this.textBox3.Text = sb.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = this.textBox6.Text;

            string[] list = input.Split(',');

            KarthicStack mystack = new KarthicStack();

            foreach (var i in list)
            {
                mystack.Push(Convert.ToInt16(i));

            }

            KarthicStack result = SortStack(mystack);
            StringBuilder sb = new StringBuilder();
            while (!result.IsEmpty())
            {
                sb.Append(result.Pop().ToString()).Append(',');
            }

            this.textBox5.Text = sb.ToString();
            //Sort the stack

        }


        public KarthicStack SortStack(KarthicStack source)
        {

            KarthicStack result = new KarthicStack();

            Stack<int> s = new Stack<int>();

            while (!source.IsEmpty())
            {

                int value = source.Pop();
                //the value of the sorted stack should be always greater than the peek
                //sorted stack 5,4,3,2,1
                //Got be carefult with the while loop condition..The condition is to continue code
                //
                while (!result.IsEmpty() && value < result.Peek())
                {
                    //If the result already has larger value..push it to source and repear till it has lesser value
                    source.Push(result.Pop());

                }
                //the value that is push on the result should be always in asc order
                //the new value should be always larger than the peek
                result.Push(value);


            }

            return result;
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Test case:
            //Enqueue 6 animals

            AnimalQueue shelter = new AnimalQueue();

            shelter.EnQueue(new Dog());
            shelter.EnQueue(new Cat());
            shelter.EnQueue(new Dog());
            shelter.EnQueue(new Cat());
            shelter.EnQueue(new Dog());
            shelter.EnQueue(new Cat());


            //Dequeue any 5 animals
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                Animal a = shelter.DequeueAny();
                sb.Append(a.Name + a.Order).Append(',');


            }

            this.textBox7.Text = sb.ToString();
        }
    }
}
