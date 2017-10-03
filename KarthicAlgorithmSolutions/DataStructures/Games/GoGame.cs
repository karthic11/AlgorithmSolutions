using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Games
{
    public class GoGame
    {

        public int[,] Board { get; set; }
        public bool[,] blackstones { get; set; }
        public bool[,] whitestones { get; set; }

        public GoGame(int xSize, int ySize)
        {
            Board = new int[xSize, ySize];

            //black is 2
            //white is 1
            //empty is 0
            Board = new int[,] {          {0,0,0,0},
                                          {0,0,2,0},
                                          {0,2,0,2},
                                          {0,0,2,0},
                                          {0,0,0,0}};

            //construct a tw array
            // 00 01 02 03
            // 10 11 12 13
            // 20 21 22 23
            // 30 31 32 33
            // 40 41 42 43

            //we are trying to place white in the middle of 2 that is in the position 22
            blackstones = new bool[Board.GetLength(0), Board.GetLength(1)];
            //set black stones
            blackstones[1, 2] = true;
            blackstones[2, 3] = true;
            blackstones[2, 1] = true;
            blackstones[3, 2] = true;
            //set white stones
            whitestones = new bool[Board.GetLength(0), Board.GetLength(1)];
            //set
            //blackstones[1, 2] = true;
            //blackstones[2, 3] = true;
            //blackstones[2, 1] = true;
            //blackstones[3, 2] = true;
            ////set white stones
            //point is 2,2

            bool result = IsLegalMove(false, 2, 2, blackstones, whitestones);

            //second scnearaio
            blackstones = new bool[Board.GetLength(0), Board.GetLength(1)];
            //set black stones
            blackstones[1, 1] = true;
            blackstones[1, 2] = true;
            blackstones[2, 0] = true;
            blackstones[2, 3] = true;
            blackstones[3, 0] = true;
            blackstones[3, 2] = true;
            blackstones[4, 1] = true;
            //set white stones
            whitestones = new bool[Board.GetLength(0), Board.GetLength(1)];
            whitestones[2, 1] = true;
            whitestones[3, 1] = true;

            bool result2 = IsLegalMove(false, 2, 2, blackstones, whitestones);

            //third scnearaio
            blackstones = new bool[Board.GetLength(0), Board.GetLength(1)];
            //set black stones
            blackstones[1, 1] = true;
            blackstones[1, 2] = true;
            blackstones[2, 0] = true;
            blackstones[2, 3] = true;
           // blackstones[3, 0] = true;
            blackstones[3, 2] = true;
            blackstones[4, 1] = true;
            //set white stones
            whitestones = new bool[Board.GetLength(0), Board.GetLength(1)];
            whitestones[2, 1] = true;
            whitestones[3, 1] = true;
            whitestones[3, 0] = true;
            whitestones[4, 0] = true;

            bool result3 = IsLegalMove(false, 2, 2, blackstones, whitestones);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isBlackStone"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="blackStones"></param>
        /// <param name="whitestones"></param>
        /// <returns></returns>
        //Make sure the given co-ordinate is empty
        public bool CheckNeighbours(bool isBlackStone, int x, int y, bool[,] blackStones, bool[,] whitestones, bool[,] visited)
        {
            //check for outside bonds
            if (x < 0 || x >= this.Board.GetLength(0) || y < 0 || y >= this.Board.GetLength(1))
            {
                //if it goes outside bonds and we couldn't find the enemy then it means true
                return true;
              
            }

            //check if this position is already visited
            if (visited[x, y] == true)
            {
                return false;
            }

            //check if this position is empty
            if (blackstones[x, y] == false && whitestones[x, y] == false)
            {
                //if empty then we can place the stone here
                return true;
            }

            //Check which stone we are looking for
            if (isBlackStone)
            {
                //we are dealing with black stone so check if there is whitestone on this co-ordinate then return false
                if (whitestones[x, y] == true)
                {
                    return false;
                }

            }
            else
            {
                //we are dealing with white stone so check if there is black stone on this position
                if (blackstones[x, y] == true)
                {
                    return false;
                }
            }



            //when the code come here means this position has the right stone on this position
            //so, recurssive check all its neighbours
            //mark the co-ordinate as visited
            visited[x, y] = true;

            //check check of adjacent stones has libery

            //upwards
            int xOfNeighbour = x - 1;
            int yOfNeightbour = y;
            if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
            {
                return true;
            }

            //downwards
            xOfNeighbour = x + 1;
            yOfNeightbour = y;
            if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
            {
                return true;
            }

            //right 
            xOfNeighbour = x;
            yOfNeightbour = y + 1;
            if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
            {
                return true;
            }

            //left 
            xOfNeighbour = x;
            yOfNeightbour = y - 1;
            if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
            {
                return true;
            }

            return false;

        }

        //Logic:
        //This function check whether the move with either black stone or white stone is IsLegal or not
        //1) First we check whether the given co-ordinate is empty, If it is not empty then we cannot place the stone
        //2) We need to keep track of the visited co-ordinate so that we don't explore the position that we already visited 
        //   so to this we can create a new datastructre either bool[,] or hashtable or we can even modify the original matrix. 
        // Assuming that we don't have to modify the original matrix I'm creating new  bool[,]  visited object
        //3) Check all the neigbours to see whether the move can form liberty
        public bool IsLegalMove(bool isBlackStone, int x, int y, bool[,] blackStones, bool[,] whitestones)
        {
            //check if the given co-ordinate already has any stone
            if (blackstones[x, y] == true || whitestones[x, y] == true)
            {
                //move cannot be made since we already have white/black stone on this co-ordinate
                return false;
              
            }
            //check whether a legal move is possible
            //We need the bounds of the board. Assuming that we have access to the board (matrix) we get the length of the board dimentions
            //or we can get this from blackstone or whitestones bool matrix
            bool[,] visited = new bool[this.Board.GetLength(0), this.Board.GetLength(1)];
            //mark this co-ordinates as visited
            visited[x, y] = true;

            //check for enemies in the neighbour

            //upwards
            int xOfNeighbour = x - 1;
            int yOfNeightbour = y;
            if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
            {
                return true;
            }
            //downwards
             xOfNeighbour = x + 1;
             yOfNeightbour = y;
             if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
             {
                 return true;
             }

             //right 
             xOfNeighbour = x;
             yOfNeightbour = y + 1;
             if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
             {
                 return true;
             }

             //left 
             xOfNeighbour = x;
             yOfNeightbour = y - 1;
             if (CheckNeighbours(isBlackStone, xOfNeighbour, yOfNeightbour, blackstones, whitestones, visited) == true)
             {
                 return true;
             }

             return false;


        }

      
    }
}
