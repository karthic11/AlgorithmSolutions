using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    public class AnagramComparator : IComparer<string>
    {

        public string SortChars(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        int IComparer<string>.Compare(string x, string y)
        {
            return SortChars(x).CompareTo(SortChars(y));
        }
    }
}
