using Puzzles.SystemDesign;
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
    public partial class ImplementationProblems : Form
    {
        public ImplementationProblems()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int shift = Convert.ToInt32(this.textBox1.Text);
            string input = this.textBox3.Text;

            KarthicCeaserCipher obj = new KarthicCeaserCipher();

            this.textBox2.Text = obj.Encrypt(input, shift);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int shift = Convert.ToInt32(this.textBox1.Text);
            string input = this.textBox2.Text;

            KarthicCeaserCipher obj = new KarthicCeaserCipher();

            this.textBox2.Text = obj.Decrypt(input, shift);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string numberasstring = this.textBox6.Text;

            int num = KarthicAtoi.myAtoi(numberasstring.ToCharArray());

            this.textBox5.Text = num.ToString();

            //test

            string res = KarthicAtoi.ConvertIntToString(num);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //http://www.sinbadsoft.com/blog/a-lru-cache-implementation/
        }
    }
}
