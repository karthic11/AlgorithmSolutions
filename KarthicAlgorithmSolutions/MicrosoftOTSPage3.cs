using Puzzles.DataStructures.Array;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class MicrosoftOTSPage3 : Form
    {
        public MicrosoftOTSPage3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DNFThreeWayPartitionSorting obj = new DNFThreeWayPartitionSorting();
            string[] array = obj.ht.Keys.ToArray();
            obj.GroupProductsByPriority(array);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<string> phonenumbers = new List<string>();
            phonenumbers.Add("477-832-5013");
            phonenumbers.Add("9858328496");
            phonenumbers.Add("541-713-5471");
            phonenumbers.Add("5341-713-5471");

            ReformatPhonenumber(phonenumbers.ToArray());
            string[] test = phonenumbers.ToArray();
            ReformatPhonenumber(test);
          
        }


        //Logic:
        //Assumption:
        //As per the given question, I have assumed that phone numbers in two formats  XXXYYYZZZZ or XXX-YYY-ZZZZ
        //This function converts this list of phonenumber and normalize it to YYY-XXX-ZZZZ

        //Logic:
        //We iterate through each phone number in this array
        //In each iteration we validate and normalize the phone number

        private void ReformatPhonenumber(string[] phonenumbers)
        {

            try
            {
                for (int i = 0; i < phonenumbers.Length; i++)
                {
                    if (IsPhoneNumberValid(phonenumbers[i]))
                    {
                        phonenumbers[i] = NormalizePhoneNumber(phonenumbers[i]);
                    }
                    else
                    {
                        throw new Exception("Phone number not valid " + phonenumbers[i]);
                    }
                }


            }
            catch (Exception ex)
            {
                //handle exception
                throw new Exception(ex.Message);
            }
         

        }

        //Validate phone numbers
        private bool IsPhoneNumberValid(string phonenumber)
        {
            //The phone number may be as XXXYYYZZZZ or XXX-YYY-ZZZZ
            if (phonenumber.Length == 10 || phonenumber.Length == 12)
            {
                Regex regex = new Regex(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$");
                Match match = regex.Match(phonenumber);
                return match.Success;

            }
            else
            {
                return false;
            }

        }


       
        //Logic:
        //We need to swap the indexs of prefix and area code of the given string
        //Since the strings are immutable(value cannot be changed), I'm creating a stringbuilder for the string
        //And then the swap logic is applied on the stringbuilder
        //This function returns a new normalized string in correct format YYY-XXX-ZZZZ
        private string NormalizePhoneNumber(string phonenumber)
        {
  
            //Strings are immutable, so I have copies the string into stringbuilder 
            StringBuilder sb = new StringBuilder(phonenumber);

            //The phonenumber may be as XXXYYYZZZZ or XXX-YYY-ZZZZ
            //If the phonenumber is in the format XXXYYYZZZZ, convert into XXX-YYY-ZZZZ.That is insert '-' to phonenumber
            if (sb.Length == 10) 
            {
                //This is done to convert XXXYYYZZZZ to XXX-YYY-ZZZZ
                //we can also do this but I don't want to create new instance of the string. We already have stringbuilder so, I inserted the characters in the index
                //double y = Double.Parse(sb.ToString());
                //string validformat = String.Format("{0:###-###-####}", y);
                //sb = new StringBuilder(validformat);
                sb.Insert(3, '-'); //length will be increased by 1
                sb.Insert(7, '-');

            }
            //In the format XXX-YYY-ZZZZ, area code starts at the index of 4
            int areacodeindex = 4;  

            for (int i = 0; i < 3; i++)
            {
                //we swap the prefix and area code
                swap(sb, i, areacodeindex + i);
            }

            return sb.ToString();
        }

     
        private void swap(char[] array, int index1, int index2)
        {
            char temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void swap(StringBuilder number, int index1, int index2)
        {
            char temp = number[index1];
            number[index1] = number[index2];
            number[index2] = temp;
        }

 
    }
}
