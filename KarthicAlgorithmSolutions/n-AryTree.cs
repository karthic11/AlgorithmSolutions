using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  //Note. N-ary tree is a tree with n-children n can be o to many
  //if n= 2 it is called binary tree
  public  class KarthicAryTree
  {
    public KarthicAryTree()
    {
    }
    public KarthicAryTreeNode root { get; set; }


    public void AddNode(KarthicAryTreeNode node)
    {

    }

    public string BreathFirstTraversal(KarthicAryTreeNode root)
    {
      if (root == null)
      {
        return string.Empty;
      }

      StringBuilder sb = new StringBuilder();

      Queue<KarthicAryTreeNode> myqueue = new Queue<KarthicAryTreeNode>();
      myqueue.Enqueue(root);
      sb.Append(root.Data).Append(",");

      while (myqueue.Count != 0)
      {

        KarthicAryTreeNode current = myqueue.Dequeue();

        foreach (KarthicAryTreeNode child in current.Children)
        {
             //visit child
          sb.Append(child.Data).Append(",");
          myqueue.Enqueue(child);
        }

      }

      return sb.ToString();
    }
  }

  public class KarthicAryTreeNode
  {
    public int Data { get; set; }
    public List<KarthicAryTreeNode> Children;

    public KarthicAryTreeNode(int data)
    {
      this.Data = data;
      Children = new List<KarthicAryTreeNode>();
    }
  
  }

}
