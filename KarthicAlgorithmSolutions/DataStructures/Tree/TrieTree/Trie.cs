using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{

 // Insert and search Time Complexity: O(key_length) where lenght of the key
  // however the memory requirements of trie is  space: O(ALPHABET_SIZE * key_length * N) where n is no. of items in the tree

  public class TrieNode
  {

    char _letter;
    bool _last;
    
    Dictionary<char, TrieNode> _children;

    //This prop is used to solve one of the problem
    public int _heapArrayIndex { get; set; }

    public int _frequency { get; set; }

    public char Letter
    {
      get { return this._letter; }
      set { this._letter = value; }
    }

    public bool Last
    {
      get { return this._last; }
      set { this._last = value; }
    }

    public Dictionary<char, TrieNode> Children
    {
      get { return this._children; }
      set { this._children = value; }
    }


    protected TrieNode() { }

    public TrieNode(char c)
    {
      _children = new Dictionary<char, TrieNode>();
      _last = false;
      _letter = c;
      _frequency = 0;
      _heapArrayIndex = -1;
    }


    public TrieNode ChildTrieNode(char c)
    {
      if (Children != null)
      {
        if (Children.ContainsKey(c))
        {
          return Children[c];
        }
      }
      return null;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;

      return Equals(obj);
    }

    public bool Equals(TrieNode obj)
    {
      if (obj != null
          && obj.Letter == this.Letter)
      {
        return true;
      }

      return false;
    }

    public override int GetHashCode()
    {
      int hash = 13;
      hash = (hash * 7) + this.Letter.GetHashCode();
      return hash;
    }

  }


  public class Trie
  {
    private TrieNode _root;

    public Trie()
    {
      _root = new TrieNode(' ');
    }

    public void Insert(List<string> list)
    {

        foreach (string s in list)
        {
            Insert(s);
        }

    }

    public void Insert(string s)
    {
      char[] word = s.ToLower().ToCharArray();

      TrieNode current = _root;

      if (word.Length == 0)
      {
        current.Last = true;
      }

      for (int i = 0; i < s.Length; i++)
      {
        TrieNode child = current.ChildTrieNode(word[i]);
        if (child != null)
        {
          current = child;
        }
        else
        {
          current.Children.Add(word[i], new TrieNode(word[i]));
          current = current.ChildTrieNode(word[i]);
        }

        if (i == word.Length - 1)
        {
          current.Last = true;
          current._frequency = current._frequency + 1;
        }
      }
    }

      //Given might be list of character that can form a word eg tes   
      //the trie containt the word test
    public bool CheckWhetherWordCanBeFormed(string charlist)
    {
        char[] word = charlist.ToLower().ToCharArray();
        TrieNode current = _root;
        ///I guess while is incoreect..we need to replace with if
        
        int i = 0;
        while (current != null && i < word.Length)
        {
                if (current.ChildTrieNode(word[i]) == null)
                {
                    return false;
                }
                else
                {
                    current = current.ChildTrieNode(word[i]);
                }

                i++;

        }

        return true;
        
    }
    //custom insert to get the pointer
    public TrieNode CustomInsert(string s)
    {
      char[] word = s.ToLower().ToCharArray();

      TrieNode current = _root;

      if (word.Length == 0)
      {
        current.Last = true;
      }

      for (int i = 0; i < s.Length; i++)
      {
        TrieNode child = current.ChildTrieNode(word[i]);
        if (child != null)
        {
          current = child;
        }
        else
        {
          current.Children.Add(word[i], new TrieNode(word[i]));
          current = current.ChildTrieNode(word[i]);
        }

        if (i == word.Length - 1)
        {
          current.Last = true;
          current._frequency = current._frequency + 1;
          //after last character of the string insertion into tier return current
          return current;

        }
      }

      return null;
    }

    public bool Search(string s)
    {
      char[] word = s.ToLower().ToCharArray();
      TrieNode current = _root;
        ///I guess while is incoreect..we need to replace with if
      while (current != null)
      {
        for (int i = 0; i < word.Length; i++)
        {
          if (current.ChildTrieNode(word[i]) == null)
            return false;
          else
            current = current.ChildTrieNode(word[i]);
        }

        if (current.Last == true)
          return true;
        else
          return false;
      }
      return false;
    }

    public bool SearchAndUpdateIndex(string s)
    {
      char[] word = s.ToLower().ToCharArray();
      TrieNode current = _root;
      while (current != null)
      {
        for (int i = 0; i < word.Length; i++)
        {
          if (current.ChildTrieNode(word[i]) == null)
            return false;
          else
            current = current.ChildTrieNode(word[i]);
        }

        if (current.Last == true)
        {
          //set the index to -1
          current._heapArrayIndex = -1;
          return true;
        }
        else
        {
          return false;
        }
      }
      return false;
    }


    public int FindNoofOccurence(string s)
    {
      char[] word = s.ToLower().ToCharArray();
      TrieNode current = _root;
      while (current != null)
      {
        for (int i = 0; i < word.Length; i++)
        {
          if (current.ChildTrieNode(word[i]) == null)
            return -1;
          else
            current = current.ChildTrieNode(word[i]);
        }

        if (current.Last == true)

          return current._frequency;
        else
          return -1;
      }
      return -1;
    }
  }

}
