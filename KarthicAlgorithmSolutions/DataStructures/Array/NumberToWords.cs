using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Array
{
    public static class NumberToWords
    {

        //table for number..we can form any number with the following dictionary values
        //1- 9
        static string[] digits = {" One", " Two" , " Three" , " Four" , " Five" , " Six" , " Seven" , " Eight" , " Nine"  };
        //10 - 90
        static string[] tens = {" Ten" , " Twenty" , " Thirty" , " Fourty" , " Fifty" , " Sixty" , " Seventy" , " Eighty" , " Ninety" };

        //special number names 11 to 19
        static string[] teens = { " Eleven" , " Twelve" , " Thirteen" , " Fourteen" , " Fifteen" , " Sixteen" , " Seventeen" , " Eighteen" , " Nineteen"  };
  
        //Hundred can be handled in the code
        //we can maintain all the big number as the power of 3... 0, 1000, 1000000, 100000000
        static string[] big = {"", " Thousand ", " Million ", " Billion "}; //We can make this to grow for large numbers


        private static string NumberToStringIn100s(int number)
        {
            StringBuilder sb = new StringBuilder();
            //eg 345
            //convert 100's place
             if(number >= 100)
             {
                 //The reason we do -1 is bcoz the 345/100 = 3 but the digits array is 0 based 3-1 = 2 will be the word three
                 sb.Append(digits[number / 100 -1]).Append(" Hundred ");
                 number %= 100;
             }

            //convert 10's place
             if(number >= 11 && number <= 19)
             {
                  sb.Append(teens[number -11]).Append(" ");
             }
             else if (number == 10 || number >= 20)
             {
                 sb.Append(tens[number / 10 -1]).Append(" ");
                 number %=10;
             }

            //convert one's place
           if(number >=1 && number <=9)
            {
                sb.Append(digits[number -1]).Append(" ");
            }

            return sb.ToString();

        }
        

        public static string ConvertNumberToStringByIteration(int number)
        {
            StringBuilder sb = new StringBuilder();
            string output = "";
            //Logic 19,323,984
            //we can break the numbers as three digits from the last..10^3
            // ConvertFn (19) +  " Million " + ConvertFn (323) + " Thousand " + ConvertFn(984)

            if(number == 0)
            {
                return "Zero";
            }
            else if(number < 0)
            {
                //Important: Here we make the number positive and add the negative word
                 return "Negative" + ConvertNumberToStringByIteration(-1 * number);
            }

            //count keeps track of 10^3 .." ", "Thousand", "Million"
            int count = 0; 
            while (number > 0)
            {

                 if(number % 1000 != 0)
                 {
                      sb.Insert(0,NumberToStringIn100s(number % 1000) + big[count] + " ");
                      output =  NumberToStringIn100s(number % 1000) + big[count] + " " + output;
                 }

                 number /= 1000;  //number = number/1000;

                 count++;
            }

            return output;

            //return sb.ToString();

        }

        public static string ConvertNumberToStringByRecursion(long number, string output)
        {
            if (number == 0)
            {
                return output + " Zero ";
            }
            
            if (number < 0)
            {
                return " Negative " + ConvertNumberToStringByRecursion(-1 * number, output);
            }

            //Handle 1's place
            int length  = number.ToString().Length;
           

            if (length == 1)
            {
                //stop the recursion here and return the output when the length is one

                output = output + digits[number -1 ];

                return output;

            }
            //Handle 10's place
            else if(length == 2)
            {
                if (number >= 11 && number <= 19)
                {
                    output = output + teens[number - 11];

                    //No recursion called here
                    return  output;
                }
                else   //(number == 10 || number >= 20)
                {
                    output = output + tens[number / 10 - 1];

                    return ConvertNumberToStringByRecursion(number % 10, output);
                }

            }
            //Handle 100's place
            else if (length == 3)
            {
                output = output + digits[number / 100 - 1] + " Hundred ";

                return ConvertNumberToStringByRecursion(number % 100, output);
            }
            else if (length >= 4) //Handle 1000's and greater..length greater than 4
            {
                int highestfactorof1000 = (length - 1) / 3;

                output = output + ConvertNumberToStringByRecursion(number / Convert.ToInt64(Math.Pow(10, 3 * highestfactorof1000)), "") + big[highestfactorof1000];

                return ConvertNumberToStringByRecursion(number % Convert.ToInt64(Math.Pow(10, 3 * highestfactorof1000)), output);
            }

            else
            {
                return "Error";
            }
              

            //}


        }


        //update 5/7/2015 Build the string from last to first via recursion

        public static string ConvertNumberToStringByRecursionMethod2(long number, StringBuilder sb, int bigcountpointer)
        {
            if (number == 0)
            { 
                //This check is to differntiate actual value of zero input and the zero caused by dividing number/1000
                if (bigcountpointer == 0)
                {
                    return "Zero";
                }
                else
                {
                    return sb.ToString();
                }
            }

            if (number < 0)
            {
                return " Negative " + ConvertNumberToStringByRecursionMethod2(-1 * number, sb, bigcountpointer);
            }

            //Number is greater than 0
            int hundredsvalue = (int)number % 1000;
            sb.Insert(0, NumberToStringIn100s(hundredsvalue) + " " + big[bigcountpointer]);

            bigcountpointer++;

            int remainingnumber = (int) number / 1000;

            return ConvertNumberToStringByRecursionMethod2(remainingnumber, sb, bigcountpointer);

        }

    }
}
