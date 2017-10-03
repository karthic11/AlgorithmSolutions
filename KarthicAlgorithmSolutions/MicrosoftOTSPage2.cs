using Puzzles.DataStructures.Games;
using Puzzles.DataStructures.Graph;
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
using System.Xml;

namespace Puzzles
{
    public partial class MicrosoftOTSPage2 : Form
    {
        public MicrosoftOTSPage2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameOfLife obj = new GameOfLife(5, 5);
            bool[,] grid = obj.Cells;
            bool result = obj.NextGeneration(grid.GetLength(0), grid.GetLength(1), ref grid);
            obj.Cells = grid;
            bool[,] grid2 = obj.Cells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "karthic";

            //debug(s.ToCharArray());
            //test(s.ToCharArray());

            char[] test1 = new char[] { 't', 'e', ' ', ' ' };
            char[] test2 = new char[] { 't', 'e', 's', 't' };
            char[] test3 = new char[] {};
            char[] test4 = new char[] { ' ', ' ', ' ' };
            char[] test5 = new char[] { '@', '1', '2', ' ' };


           int count = CountEmptyCharacters(test1);
            count = CountEmptyCharacters(test2); 
           count = CountEmptyCharacters(test3);
           count = CountEmptyCharacters(test4);
           count = CountEmptyCharacters(test5);

        }

        public static void test(char[] p)
        {

            //Bugs:
            //The provided function didn't handle the array bounds So, it will throwArray Index Out Of Bounds Exception
            //The for loop termination condition is not correct

            //Assumption:
            //If we fix the above bugs, the function still does nothing with this iteration So, I'm assuming this function iterates 
            //all the characters in the given array and check whether this array has any empty character or counts no of empty characters in the given array

            //Fix:
            //We need to initilaize i as P.Length -1 so that 'i' will be the last index of the array P and P[i] will be the character in the last index
            //For-loop end conditon will be i >= 0
            //Inside the loop we check for the empty character condition So, this function will be like

            //Test Cases:
            //Char[] test1 = new Char[]{'
            //char[] test1 = new char[] { 't', 'e', ' ', ' ' };
            //char[] test2 = new char[] { 't', 'e', 's', 't' };
            //char[] test3 = new char[] { };
            //char[] test4 = new char[] { ' ', ' ', ' ' };
            //char[] test5 = new char[] { '@', '1', '2', ' ' };


            //int count = CountEmptyCharacters(test1);
            //count = CountEmptyCharacters(test2);
            //count = CountEmptyCharacters(test3);
            //count = CountEmptyCharacters(test4);
            //count = CountEmptyCharacters(test5);

            //Concerns:
            //This function has a for loop with a condition to check last character index to empty
            int i = 0;
            for (i = p.Length -1 ; 1 >= 0 && p[i] == ' '; i--)
            {
                char test = p[i];
            }
        }


           public static void debug(char[] p)
          {
            int i = 0;
            for (i = p.Length - 1; i >= 0 && p[i] == ' '; i--) ;
            }
        //Count no of empty character on the char array
        public static int CountEmptyCharacters(char[] p)
        {
            int count = 0;

            for (int i = p.Length - 1; i >=0 ; i--)
            {
                if (p[i] == ' ')
                {
                    count++;
                }
            }

            return count;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            //MysteryFunction2(5, 6, 2, 3);
       

            //int a= 5;
            //int b= 7;
            //int x=2;
            //int y=1;

            //MysteryFunctionCorrected(a, b, x, y);
            //int test = -1;

            //string folderpath = @"F:\Selected Photos\Bride house 20.2.2015";
            //List<string> images =   GetImagesPath(folderpath);


             

        }


        public List<String> GetImagesPath(String folderName)
        {

            DirectoryInfo Folder;
            FileInfo[] Images;

            Folder = new DirectoryInfo(folderName);
            Images = Folder.GetFiles("*.jpg", SearchOption.AllDirectories);

            List<String> imagesList = new List<String>();

            for (int i = 0; i < Images.Length; i++)
            {
                imagesList.Add(String.Format(@"{0}/{1}", folderName, Images[i].Name));
                // Console.WriteLine(String.Format(@"{0}/{1}", folderName, Images[i].Name));
            }


            return imagesList;
        }

        public static void MysteryFunction2(int a, int b, int x, int y)
        {
            while (a != x && b != y)
            {
                if (a > x)
                {
                    a++;
                }
                else
                {
                    a--;
                }

                if (b > y)
                {
                    b++;
                }
                else
                {
                    b--;
                }
            }
        }


        //Problem:
        //This function will have endless loop because the condition defined in the while loop will always be true 
        //or in other words variable 'a' won't be equal to x or the variable 'b' won't be equal to 'y'. So, the given condition won't be met

        //Fix:
        //1) Change the logic on the if condition to decrement the value of a if the value of a is greater than x else increment the value of a if 'a' is lesser than or equal to x
        //2) Similarly decrement the value of b if the value of b is greater than y else increment the value of b if 'b' is lesser than or equal to y

        //This function change the values of the parm a and b and iterates number of times till the values of a is equal to x or the value of 'b' is equal to 'y'

        public static void MysteryFunctionCorrected(int a, int b, int x, int y)
        {
            while (a != x && b != y)
            {
                if (a > x)
                {
                    a--;
                }
                else
                {
                    a++;
                }

                if (b > y)
                {
                    b--;
                }
                else
                {
                    b++;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Person person1 = new Person("karthic", null);
            Person person2 = new Person("karthic2", null);
            Person person3 = new Person("karthic3", null);
            Person person4 = new Person("karthic4", null);
            Person person5 = new Person("karthic5", null);
            Person person6 = new Person("karthic6", null);


            List<Person> friends = new List<Person>();
            friends.Add(person2);
            friends.Add(person3);
            person1.Acquaintances = friends.ToArray();


            List<Person> friends2 = new List<Person>();
            friends2.Add(person1);
            friends2.Add(person5);
            person2.Acquaintances = friends2.ToArray();

            List<Person> friends3 = new List<Person>();
            friends3.Add(person4);
            friends3.Add(person5);
            person3.Acquaintances = friends3.ToArray();


           bool result =   person1.IsConnectedInNetworkByBFS("karthic4");
           bool result2 = person1.IsConnectedInNetworkByBFS("karthic6");
        }

        private void button5_Click(object sender, EventArgs e)
        {
          

            string filepath = @"C:\Users\kanmanik\Google Drive\NGU2013-Puzzles\KarthicAlgorithmSolutions\KarthicAlgorithmSolutions\Resources\XMLFiles\test.xml";
            bool result = CheckXMLValidation(filepath);
        }


        private bool CheckXMLValidation(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            XmlTextReader reader = new XmlTextReader(filepath);
            
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                       
                    case XmlNodeType.Element: // The node is an element.

                        string element = reader.Name;

                        break;
                    case XmlNodeType.Text: //Display the text in each element.

                        string test = reader.Value;
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        string elemente = reader.Name;
                        break;
                }
            }


            return true;
        }
    }
}
