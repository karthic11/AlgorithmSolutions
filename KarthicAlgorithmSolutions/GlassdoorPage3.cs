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
    public partial class GlassdoorPage3 : Form
    {
        public GlassdoorPage3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] input = new int[,]{{1, 5,  9,  13},
                                      {2, 6, 10, 14 },
                                      {3,7, 11, 15 },
                                      {4, 8,  12 ,16},

                                      {32, 34,  33 ,46},
                                   
            };

            string output = SprialMatrixPrint(input);

            Node n = new Node();
            int code = n.GetHashCode();
           


        }


        private string SprialMatrixPrint(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int rowstart = 0;
            int rowend = matrix.GetLength(0) - 1;
            int colstart = 0;
            int colend = matrix.GetLength(1) - 1;

            while (rowstart < rowend && colstart < colend)
            {
                /* Print the first row from the remaining rows */
                for (int i = colstart; i <= colend; i++)
                {
                    sb.Append(matrix[rowstart, i]).Append(",");
                }
                //done with this row
                rowstart++;

                /* Print the last column from the remaining columns */
                for (int j = rowstart; j <= rowend; j++)
                {
                    sb.Append(matrix[j, colend]).Append(",");
                }
                //done with last col
                colend--;
                /* Print the last row from the remaining rows */
                //if (rowstart < rowend)
                //{
                    for (int k = colend; k >= colstart; k--)
                    {
                        sb.Append(matrix[rowend, k]).Append(",");
                    }
                    //done with the last row
                    rowend--;
                //}
                /* Print the first column from the remaining columns */
                //if (colstart < colend)
                //{
                    for (int i = rowend; i >= rowstart; i--)
                    {
                        sb.Append(matrix[i, colstart]).Append(",");
                    }
                    colstart++;
                //}
            }

            return sb.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string text = this.textBox15.Text;
            string pattern = this.textBox12.Text;

            List<int> indexlist = AnagramSubstringSearch(text, pattern);

            StringBuilder sb = new StringBuilder();

            foreach(int i in indexlist)
            {
                sb.Append(text.Substring(i, pattern.Length)).Append("at Index" + i).Append(",");
            }

            this.textBox1.Text = sb.ToString();
        }

        private List<int> AnagramSubstringSearch(string text, string pattern)
        {
            int maxASCIIValue = 256;
            List<int> anagramIndexList = new List<int>();
            char[] patternArrayCount = new char[maxASCIIValue];
            char[] TextArrayCount = new char[maxASCIIValue];

            for (int i = 0; i < pattern.Length; ++i)
            {
                (patternArrayCount[pattern[i]])++;
                (TextArrayCount[text[i]])++;
            }

            for (int i = pattern.Length; i < text.Length; ++i)
            {
                if (Compare(patternArrayCount, TextArrayCount))
                    anagramIndexList.Add((i - pattern.Length));

                (TextArrayCount[text[i]])++;
                TextArrayCount[text[i - pattern.Length]]--;
            }

            if (Compare(patternArrayCount, TextArrayCount))
                anagramIndexList.Add(text.Length - pattern.Length);

            return anagramIndexList;

        }

        private static bool Compare(char[] arr1, char[] arr2)
        {
            int maxASCIIValue = 256;
            for (int i = 0; i < maxASCIIValue; ++i)
                if (arr1[i] != arr2[i])
                    return false;

            return true;
        }
    }
}
