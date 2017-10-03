using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.jigsawPuzzle
{
   public class ReadMe
    {
    }

    //Core objects
    //Puzzle
    //Piece  
    //Edge   -each piece will have four edges
    //Orientation  -left, right, top and bottom that helps to find and rotate edges
    //Shape  - shape of edge flat, outer, inner


     public class Edge
    {
        public Type type { get; set; }
        public Piece parent { get; set; }  //edge belongs to a piece
        int index; //index of the edge (0,1,2,3) 4 edges in piece
        Edge attachedto; //attached to neighbor 
        bool fitwith(Edge edge) { return true; }
    }

    public class Piece
    {
        public Piece()
        {
        }
        public Edge[] edges { get; set; }
        public bool IsCorner() { return true; }
        public void AttachEdge(Edge ed1, Edge ed2)
        {
        }
        public void GetExposedEdge(Edge edge)
        {
        }
    }

    public class Puzzle
    {
       public Piece[,] board;
       public  Piece[] remainingpieces;
        private int size;
        public Puzzle(int size, Piece[] pieces)
        {
            this.size = size;
            this.remainingpieces = pieces;
            this.board = new Piece[size, size];

        }
       private void Group() { }
       private bool solve ()
       {
       
           //we will solve like a child will be solving this puzzle..
           //first set the boarder and corner pieces and then set the inside pieces

           //Group the given pieces into border, corner and inside
           List<Piece> corners = new List<Piece>();
           List<Piece> boarder = new List<Piece>();
            List<Piece> inside = new List<Piece>();
           
           //call the group method and group all the pieces into corner, boarder and inside

           /* walk through the board and set the peiec to matching piece */
           for(int row=0; row < this.board.GetLength(0); row++)
           {
              for(int col=0; col < this.board.GetLength(1); col++)
              {
                  //based on the row and col find the list that we need to search for
                  //eg if col =0 or col = size -1 then we need to search boarder pieces list 
                  List<Piece> tosearch = null; 
                  if(fitNextEdge(tosearch, row, col) == false)
                  {
                      return false;
                  }
              }
           }

           return true;
       
       }  //Solve algorithm is explain in gayle book pg: 299

       private bool fitNextEdge(List<Piece> pieceToSearch, int row, int col)
       {
           //see new gay pg 322
           return   true;
       }
    }
         

 //public enum Type { inner, outer, flat }
   
}
