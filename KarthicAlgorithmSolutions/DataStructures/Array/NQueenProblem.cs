using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Array
{
    // https://www.youtube.com/watch?v=xouin83ebxE
    // http://www.geeksforgeeks.org/backtracking-set-3-n-queen-problem/
    public class NQueenProblem
    {
        public int Size { get; set; }

        public NQueenProblem(int size)
        {
            this.Size = size;
        }

        public Positions[] FindPositionsForQueen()
        {
            // max position the board can have for queen 
            // For boardsize 4. max is 4
            Positions[] positions = new Positions[this.Size];

            if (IsPossibleQueenPosition(this.Size, 0, positions))
            {
                return positions;
            }
            else
            {
                return null;
            }
        }

        private bool IsPossibleQueenPosition(int size, int row, Positions[] positions)
        {
           // base condition 
           // if all the rows are done which means that we place queen in all the rows
            if (row == size)
            {
                // the positions will have the places where all the queens are placed
                return true;
            }

            // For each of col
            for (int col = 0; col < size; col++)
            {
                bool isSafePosition = true;

                // check for all the previous queen placement
                for (int previousRow = 0; previousRow < row; previousRow++)
                {
                    // The queen cannot be placed in the same column of the previous queen
                    // The queen cannot be placed diagonally with the previous queen
                    if (positions[previousRow].Col == col ||
                       (positions[previousRow].Row + positions[previousRow].Col == row + col) ||  // check diagonal top See the video for the trick
                       (positions[previousRow].Row - positions[previousRow].Col == row - col)) // check diagonal bottom
                    {
                        isSafePosition = false;
                        break;
                    }
                }

                if (isSafePosition)
                {
                    positions[row] = new Positions(row, col);

                    bool isPossiblePosition = IsPossibleQueenPosition(size, row + 1, positions);
                    if (isPossiblePosition)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class Positions
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public Positions(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}
