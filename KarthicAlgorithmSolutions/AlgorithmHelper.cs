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

        foreach (var item in input.Split(','))
        {
          li.Add(Convert.ToInt32(item));

        }

        int[] array = (int[])li.ToArray(typeof(int));

        return array;
      }

    }
}
