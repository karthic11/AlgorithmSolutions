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
    public partial class ObjectOrientedDesign : Form
    {
        public ObjectOrientedDesign()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form f = new OODPage2();
            f.Show();
        }
    }
}
