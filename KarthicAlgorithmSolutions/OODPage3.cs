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
    public partial class OODPage3 : Form
    {
        public OODPage3()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
          

            //Given primary key of the db record on the shortening service
            int key = 89893;
            key = Int32.MaxValue;

            string url = ShortenUrlService.Encode(key);

            int resultkey = ShortenUrlService.DecodeURL(url);
        }
    }
}
