using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  //Note. N-ary tree is a tree with n-children n can be o to many
  //if n= 2 it is called binary tree

//Time Complexity of Depth and Breath First Search
//Time: O(|V| + |E|)
 //Space: O(|V| + |E|)

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


    public string DepthFirstSearch(KarthicAryTreeNode root)
    {

        StringBuilder sb = new StringBuilder();

        if (root == null)
        {
            return string.Empty;
        }

        //root is not null..visit the current 
        sb.Append(root.Data);

        //check for leaf node  //End of child marker
        if (root.Children.Count == 0)
        {
            sb.Append('(');
            return sb.ToString();
        }

        //It is not leaf node..then go depth first search
        foreach (KarthicAryTreeNode child in root.Children)
        {
            sb.Append(DepthFirstSearch(child));
        }

        //End of child marker
        sb.Append('(');

        return sb.ToString();

    }


    public int GetHeight(KarthicAryTreeNode root)
    {
        //Height of root node is 0
        //Height of 1st level is 1

        if (root == null)
        {
            return 0;
        }

        //if there is a leaf node then return 0
        if (root.Children.Count == 0)
        {
            return 0;
        }

        int maxheight = 0;
        foreach (KarthicAryTreeNode child in root.Children)
        {
            maxheight = Math.Max(maxheight, GetHeight(child));
        }

        return maxheight + 1;
        
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


  public class AryTree<T>
  {
      public AryTree()
      {
      }
      public AryTreeNode<T> root { get; set; }


      public void AddNode(AryTree<T> node)
      {

      }

      public string BreathFirstTraversal(AryTreeNode<T> root)
      {
          if (root == null)
          {
              return string.Empty;
          }

          StringBuilder sb = new StringBuilder();

          Queue<AryTreeNode<T>> myqueue = new Queue<AryTreeNode<T>>();
          myqueue.Enqueue(root);
          sb.Append(root.Data).Append(",");

          while (myqueue.Count != 0)
          {

              AryTreeNode<T> current = myqueue.Dequeue();

              foreach (AryTreeNode<T> child in current.Children)
              {
                  //visit child
                  sb.Append(child.Data).Append(",");
                  myqueue.Enqueue(child);
              }

          }

          return sb.ToString();
      }

      //Depth First Search with end of child marker
      //custom problem for serilaization of n-ary tree
      public string DepthFirstSearch(AryTreeNode<T> root)
      {

          StringBuilder sb = new StringBuilder();

          if (root == null)
          {
              return string.Empty;
          }

          //root is not null..visit the current 
          sb.Append(root.Data);

          ////check for leaf node  //End of child marker
          //if (root.Children.Count == 0)
          //{
          //    sb.Append('(');
          //    return sb.ToString();
          //}

          //It is not leaf node..then go depth first search
          foreach (AryTreeNode<T> child in root.Children)
          {
              sb.Append(DepthFirstSearch(child));
          }

          //End of child marker
          sb.Append('(');

          return sb.ToString();

      }

      public int GetHeight(AryTreeNode<T> root)
      {
          //Height of root node is 0
          //Height of 1st level is 1

          if (root == null)
          {
              return 0;
          }

          //if there is a leaf node then return 0
          if (root.Children.Count == 0)
          {
              return 0;
          }

          int maxheight = 0;
          foreach (AryTreeNode<T> child in root.Children)
          {
              maxheight = Math.Max(maxheight, GetHeight(child));
          }

          return maxheight + 1;

      }
  }

  public class AryTreeNode<T>
  {
      public T Data { get; set; }
      public List<AryTreeNode<T>> Children;

      public AryTreeNode(T data)
      {
          this.Data = data;
          Children = new List<AryTreeNode<T>>();
      }

  }


}
