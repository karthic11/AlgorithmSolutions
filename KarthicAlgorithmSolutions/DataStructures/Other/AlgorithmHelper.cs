using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    static class AlgorithmHelper
    {

      public static int[] ConvertCommaSeparetedStringToInt(string input)
      {
        ArrayList li = new ArrayList();

        if (input[input.Length - 1] == ',')
        {
            input = input.Substring(0, input.LastIndexOf(','));
        }

        foreach (var item in input.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
        {
          li.Add(Convert.ToInt32(item));

        }

        int[] array = (int[])li.ToArray(typeof(int));

        return array;
      }

      public static string ConvertIntArrayToCommaSeparatedString(int[] array)
      {
        StringBuilder sb = new StringBuilder();

        foreach (int i in array)
        {
          sb.Append(i).Append(",");
        }
        return sb.ToString();
      }

      public static string ConvertIntArrayToCommaSeparatedString(List<int> array)
      {
          StringBuilder sb = new StringBuilder();

          foreach (int i in array)
          {
              sb.Append(i).Append(",");
          }
          return sb.ToString();
      }

      public static string ConvertIntArrayToSpaceSeparatedString(int[] array)
      {
          StringBuilder sb = new StringBuilder();

          foreach (int i in array)
          {
              sb.Append(i).Append(" ");
          }
          return sb.ToString();
      }

      public static string ConvertStringListArrayToSpaceSeparatedString(List<string> array)
      {
          StringBuilder sb = new StringBuilder();

          foreach (string i in array)
          {
              sb.Append(i).Append(",");
          }
          return sb.ToString();
      }


      public static string[] ConvertCommaSeparetedStringToStringArray(string input)
      {
          ArrayList li = new ArrayList();

          foreach (var item in input.Trim().Split(','))
          {
              li.Add((item.ToString()));

          }

          string[] array = (string[])li.ToArray(typeof(string));

          return array;
      }


      public static char[] ConvertCommaSeparetedStringToCharArray(string input)
      {
          ArrayList li = new ArrayList();

          if (input[input.Length - 1] == ',')
          {
              input = input.Substring(0, input.LastIndexOf(','));
          }

          foreach (var item in input.Split(','))
          {
              li.Add(Convert.ToChar(item));

          }

          char[] array = (char[])li.ToArray(typeof(char));

          return array;
      }


      public static int[,] ConvertsPairsToTwoDArray(string input)
      {
          string[] items = input.Split(',').ToArray();

          int[,] pairs = new int[items.Length, items.Length];

          int counter = 0;
          foreach (var item in items)
          {
             // pairs[0,
              
          }

          return pairs;

      }


    }
}
