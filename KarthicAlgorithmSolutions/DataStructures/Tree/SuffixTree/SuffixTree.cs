using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  //Suffix Tree is a compressed Trie Tree
  //In case of Trie, the root will build the tree from the first to children
  //In case of suffix tree we get all the possible suffix of the given string and then build the tree for each suffix
  public class SuffixTree
  {

    public SuffixTreeNode root = new SuffixTreeNode(' ');

    public SuffixTree(String s)
    {
        //build suffix tree using the string s
      for (int i = 0; i < s.Length; i++)
      {
        string suffix = s.Substring(i);
        
        this.root.InsertString(suffix, i);
      }

    }

    public List<int> Search(string pattern)
    {
      return root.Search(pattern);
    }

 

  }


  public class SuffixTreeNode
  {
    public char Value { get; set; }
    //stores the list of index that has the char value 
    List<int> IndexList = new List<int>();
   
    public Dictionary<char, SuffixTreeNode> Children = new Dictionary<char, SuffixTreeNode>();

    public SuffixTreeNode(char value)
    {
        this.Value = value;
    }
    //Insert the given string in this suffixtree node and 
    //index is the index of the first char in the original string..
    public void InsertString(string s, int index)
    {
      IndexList.Add(index);
      if (s != null && s.Length > 0)
      {
        //hardcoded get the first char of string
        char value = s[0];
        SuffixTreeNode child = null;
        if (Children.ContainsKey(value))
        {
          child = Children[value];
        }
        else
        {
          //new child
          child = new SuffixTreeNode(value);
          this.Children.Add(value, child);
        }

        //hardcoded to 1 b
        string remaining = s.Substring(1);
        //note we are passing the index of the parent node to the child
        child.InsertString(remaining, index);
      }
    }

    //This method searches the given string pattern in this node and returns the list of index if found
    //Remember this tree was built with a source string
    //Now we search for pattern in the source string and if found we will get the index of the pattern found in the source string
    //if there are multiple occurence of the pattern in the string, we will get all the indexes of the source string that contains the pattern

    public List<int> Search(String s)
    {
      if (s == null || s.Length == 0)
      {
        return IndexList;
      }
      else
      {
        char first = s[0];

        if (this.Children.ContainsKey(first))
        {
             string remaining = s.Substring(1);
             return this.Children[first].Search(remaining);
        }
       
      }

      return null;
    }


    
  }


  
}
