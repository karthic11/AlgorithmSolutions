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
    public partial class SortingAndSearch : Form
    {
        public SortingAndSearch()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string input = this.textBox1.Text;

            ArrayList li = new ArrayList();

            foreach (var item in input.Split(','))
            {
                li.Add(Convert.ToInt32(item));

            }

            int[] array = (int[])li.ToArray(typeof(int));

            SortHelper.MergeSort(array);
            StringBuilder sb = new StringBuilder();
            foreach(int i in array)
            {
                sb.Append(i).Append(',');
            }

            this.textBox6.Text = sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = this.textBox2.Text;

            ArrayList li = new ArrayList();

            foreach (var item in input.Split(','))
            {
                li.Add(Convert.ToInt32(item));

            }

            int[] array = (int[])li.ToArray(typeof(int));

            SortHelper.QuickSort(array,0, array.Length - 1);
            StringBuilder sb = new StringBuilder();
            foreach (int i in array)
            {
                sb.Append(i).Append(',');
            }

            this.textBox3.Text = sb.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
          string input = this.textBox5.Text;

          ArrayList li = new ArrayList();

          foreach (var item in input.Split(','))
          {
            li.Add(Convert.ToInt32(item));

          }

          int[] array = (int[])li.ToArray(typeof(int));

          List<int> sortedlist = SortHelper.PerformHeapSort(array.ToList<int>());

          StringBuilder sb = new StringBuilder();
          foreach (int i in sortedlist)
          {
            sb.Append(i).Append(',');
          }

          this.textBox4.Text = sb.ToString();

        }

        private void SortingAndSearch_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string input = this.textBox8.Text;

            ArrayList li = new ArrayList();

            foreach (var item in input.Split(','))
            {
                li.Add(Convert.ToInt32(item));

            }

            int[] array = (int[])li.ToArray(typeof(int));

            SortHelper.MergeSort(array, true);
            StringBuilder sb = new StringBuilder();
            foreach (int i in array)
            {
                sb.Append(i).Append(',');
            }

            this.textBox7.Text = sb.ToString();

        }
    }
}
