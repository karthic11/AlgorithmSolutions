using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Tree.BTree
{
//    Properties of B-Tree
//1) All leaves are at same level.
//2) A B-Tree is defined by the term minimum degree ‘t’. The value of t depends upon disk block size.
//3) Every node except root must contain at least t-1 keys. Root may contain minimum 1 key.
//4) All nodes (including root) may contain at most 2t – 1 keys.
//5) Number of children of a node is equal to the number of keys in it plus 1.
//6) All keys of a node are sorted in increasing order. The child between two keys k1 and k2 contains all keys in range from k1 and k2.
//7) B-Tree grows and shrinks from root which is unlike Binary Search Tree. Binary Search Trees grow downward and also shrink from downward.
//8) Like other balanced Binary Search Trees, time complexity to search, insert and delete is O(Logn).
    //source:http://www.geeksforgeeks.org/b-tree-set-1-introduction-2/

    //insertion http://www.geeksforgeeks.org/b-tree-set-1-insert-2/

    public class BTree
    {

        BTreeNode root;
        public int t { get; set; } //minimum degree


        // Constructor (Initializes tree as empty)
        public BTree(int _t)
        {
            root = null; t = _t;
        }
 
    // function to traverse the tree
    public void traverse(StringBuilder sb)
    {  if (root != null) 
           root.traverse(sb); 
    }
 
    // function to search a key in this tree
    BTreeNode search(int k)
    {  return (root == null)? null : root.search(k); }
    }
}


public class BTreeNode
{
    int[] keys; // An array of keys
    int t;      // Minimum degree (defines the range for number of keys)
    BTreeNode[] Childrens;// An array of child pointers
    int n;     // Current number of keys
    bool leaf; // Is true when node is leaf. Otherwise false
    // Constructor
    public BTreeNode(int _t, bool _leaf)
    {
        // Copy the given minimum degree and leaf property
        t = _t;
        leaf = _leaf;
        // Allocate memory for maximum number of possible keys
        // and child pointers
        keys = new int[2 * t - 1];  //formula
        Childrens = new BTreeNode[2 * t];  //+ 1 of no of keys

        // Initialize the number of keys as 0
        n = 0;
    }

    // A function to traverse all nodes in a subtree rooted with this node
    public void traverse(StringBuilder sb)
    {
        // There are n keys and n+1 children, travers through n keys
        // and first n children
        int i;
        for (i = 0; i < n; i++)  // no. of current used keys
        {
            // If this is not leaf, then before printing key[i],
            // traverse the subtree rooted with child C[i].
            if (leaf == false)
            {
                this.Childrens[i].traverse(sb);
            }

            sb.Append(keys[i]).Append(",");

        }

        // Print the subtree rooted with last child //forloop will have the i incremented
        if (leaf == false)
        {
            this.Childrens[i].traverse(sb);
        }
    }

    // A function to search a key in subtree rooted with this node.   
    // returns NULL if k is not present.
    public BTreeNode search(int k)
    {
        // Find the first key greater than or equal to k
        int i = 0;
        while (i < n && k > keys[i])
            i++;

        // If the found key is equal to k, return this node
        if (keys[i] == k)
            return this;

        // If key is not found here and this is a leaf node
        if (leaf == true)
            return null;

        // Go to the appropriate child
        return this.Childrens[i].search(k);
    }

}

