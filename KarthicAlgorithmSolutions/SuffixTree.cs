﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{


  public class SuffixTreeNode
  {
    public int Index = -1;
    public Dictionary<char, SuffixTreeNode> Children = new Dictionary<char, SuffixTreeNode>();
    List<String> substrings = new List<string>();

  }


  public class SuffixTree
  {
  

    public SuffixTreeNode Root = new SuffixTreeNode();
    public String Text;

    public void InsertSuffix(string s, int from)
    {
      var cur = Root;
      for (int i = from; i < s.Length; ++i)
      {
        var c = s[i];
        if (!cur.Children.ContainsKey(c))
        {
          var n = new SuffixTreeNode() { Index = from };
          cur.Children.Add(c, n);

          // Very slow assertion. 
          Debug.Assert(Find(s.Substring(from)).Any());

          return;
        }
        cur = cur.Children[c];
      }
      Debug.Assert(false, "It should never be possible to arrive at this case");
      throw new Exception("Suffix tree corruption");
    }

    private static IEnumerable<SuffixTreeNode> VisitTree(SuffixTreeNode n)
    {
      foreach (var n1 in n.Children.Values)
        foreach (var n2 in VisitTree(n1))
          yield return n2;
      yield return n;
    }

    public IEnumerable<int> Find(string s)
    {
      var n = FindSuffixTreeNode(s);
      if (n == null) yield break;
      foreach (var n2 in VisitTree(n))
        yield return n2.Index;
    }

    private SuffixTreeNode FindSuffixTreeNode(string s)
    {
      var cur = Root;
      for (int i = 0; i < s.Length; ++i)
      {
        var c = s[i];
        if (!cur.Children.ContainsKey(c))
        {
          // We are at a leaf-node.
          // What we do here is check to see if the rest of the string is at this location. 
          for (var j = i; j < s.Length; ++j)
            if (Text[cur.Index + j] != s[j])
              return null;
          return cur;
        }
        cur = cur.Children[c];
      }
      return cur;
    }

 
  }

}
