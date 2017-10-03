using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.ArrayHelper
{
  // Suffix array represents simple text indexing mechanism.
  //http://dzmitryhuba.blogspot.com/search/label/Algorithms
    //http://dzmitryhuba.blogspot.com/2010/04/suffix-array.html
  public class SuffixArray : IEnumerable<int>
  {
    private const int c_lower = 0;
    private const int c_upper = -1;

    private readonly string m_text;
    private readonly int[] m_pos;
    private readonly int m_lower;
    private readonly int m_upper;

    public SuffixArray(string text, int[] pos, int lower, int upper)
    {
      m_text = text;
      m_pos = pos;
      // Inclusive lower and upper boundaries define search range.
      m_lower = lower;
      m_upper = upper;
    }

    public static SuffixArray Build(string text)
    {
      //Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(text));

      var length = text.Length;
      // Sort starting positions of suffixes in lexicographical 
      // order.
      var pos = Enumerable.Range(0, length).ToArray();
      System.Array.Sort(pos, (x, y) => String.Compare(text, x, text, y, length));
      // By default all suffixes are in search range.
      return new SuffixArray(text, pos, 0, text.Length - 1);
    }

    public SuffixArray Search(string str)
    {
      //Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(str));

      // Search range is empty so nothing to narrow.
      if (m_lower > m_upper)
        return this;
      // Otherwise search for boundaries that enclose all 
      // suffixes that start with supplied string.
      var lower = Search(str, c_lower);
      var upper = Search(str, c_upper);
      // Once precomputed sorted suffixes positions don't change
      // but the boundaries do so that next refinement 
      // can be done within smaller range and thus faster.
      // For example, you may narrow search range to suffixes 
      // that start with "ab" and then search within this smaller
      // search range suffixes that start with "abc".
      return new SuffixArray(m_text, m_pos, lower + 1, upper);
    }

    public IEnumerator<int> GetEnumerator()
    {
      // Enumerates starting positions of suffixes that fall 
      // into search range.
      for (var i = m_lower; i <= m_upper; i++)
        yield return m_pos[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    private int Compare(string w, int i)
    {
      // Comparison takes into account maximum length(w) 
      // characters. For example, strings "ab" and "abc" 
      // are thus considered equal.
        int test = String.Compare(w, 0, m_text, m_pos[i], w.Length);
      return String.Compare(w, 0, m_text, m_pos[i], w.Length);
    }

    private int Search(string w, int bound)
    {
      // Depending on bound value binary search results 
      // in either lower or upper boundary.
        int x = m_lower - 1;
        int y = m_upper + 1;
      
        if (Compare(w, m_lower) < 0)
           return x;
        if (Compare(w, m_upper) > 0)
           return y;
      while (y - x > 1)
      {
        var m = (x + y) / 2;
        // If bound equals to 0 left boundary andvances to median 
        // only // if subject is strictly greater than median and 
        // thus search results in lower bound (position that 
        // preceeds first suffix equal to or greater than 
        // subject w). Otherwise search results in upper bound 
        // (position that preceeds fisrt suffix that is greater 
        // than subject).
        if (Compare(w, m) > bound)
          x = m;
        else
          y = m;
      }
      return x;
    }
  }
}
