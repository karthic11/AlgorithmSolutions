using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreeningTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintReverseFibonacciSeries(80, 50);
       	    
            //Test Cases
            PrintReverseFibonacciSeries(21, 13);
            PrintReverseFibonacciSeries(13, 21);
            PrintReverseFibonacciSeries(21, 21);
            PrintReverseFibonacciSeries(1, 1);
            PrintReverseFibonacciSeries(-21, -13);
            PrintReverseFibonacciSeries(0, -1);
            PrintReverseFibonacciSeries(0, 0);
            PrintReverseFibonacciSeries(7, 5);
   
         
        }

        private static void PrintReverseFibonacciSeries(int lastno, int lastbeforeno)
        {
            //check if the given two numbers are positive numbers
            if (lastno < 0 || lastbeforeno < 0)
            {
                throw new Exception("Invalid Input: Input cannot be negative");
            }
            else if (lastno < lastbeforeno)
            {
                throw new Exception("Invalid Input: Last number cannot be lesser than then last before number ");
            }
            else if ((lastno == lastbeforeno) && lastno != 1)
            {
                throw new Exception("Invalid Input: Last number and Last before number cannot be equal");
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(lastno.ToString()).Append(',');
            sb.Append(lastbeforeno.ToString()).Append(',');
            ReverseFibonacciSeries(lastno, lastbeforeno, sb);

            Console.WriteLine(sb.ToString());
         //   Console.ReadLine();
           
         
           

        }
        
        //Here x is lastno and y is lastbeforenumber
        private static void ReverseFibonacciSeries(int x, int y, StringBuilder sb)
        {
            if (y < 1)
            {
                return;
            }
            else
            {
                int z = x - y;
                sb.Append(z.ToString()).Append(',');
                x = y;
                y = z;
                ReverseFibonacciSeries(x, y, sb);
                
            }
        }



        //private static void PrintReverseFibonacciSeriesMethod2(int lastnum, int lastbeforenum)
        //{
        //    check if the given two numbers are positive numbers
        //    if (lastnum < 0 || lastbeforenum < 0)
        //    {
        //        throw new Exception("Invalid Input: Input cannot be negative");
        //    }
        //    else if (lastnum < lastbeforenum)
        //    {
        //        throw new Exception("Invalid Input: Last number cannot be lesser than Last before number ");
        //    }

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(lastnum.ToString()).Append(',');
        //    sb.Append(lastbeforenum.ToString()).Append(',');
        //    int nextno = 0;
        //    for (int i = lastnum; i >= 0; i = i - nextno)
        //    {
        //        nextno = lastnum - lastbeforenum;
        //        sb.Append(nextno.ToString()).Append(',');
        //        lastnum = lastbeforenum;
        //        lastbeforenum = nextno;

        //    }
        //    Console.WriteLine(sb.ToString());
        //    Console.ReadLine();

        //}
        

        

 
    }
}
