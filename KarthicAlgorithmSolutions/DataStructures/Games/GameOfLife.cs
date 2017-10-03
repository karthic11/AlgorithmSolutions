using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Games
{
    public class GameOfLife
    {
        //Cells can be alive or dead...alive mean true , dead means false
        public bool[,] Cells { get; set; }
        public GameOfLife(int xSize, int ySize)
        {
            Cells = new bool[xSize, ySize];

            //set up seed values

            Cells = new bool[,] {{ true,false,false,false,false},
                                 {false,true,false,true,false},
                                 {false,false,true,false,false},
                                 {false,true,false,true,false},
                                 {false,false,false,false,false}};


            //Rules
            // Any live cell with fewer than two live neighbours dies, as if by loneliness.

            //Any live cell with more than three live neighbours dies, as if by overcrowding.

            //Any live cell with two or three live neighbours lives, unchanged, to the next generation.

            //Any dead cell with exactly three live neighbours comes to life



        }


        //grid has to be updated to next generation
        //return true if there is alteast one live cell in the next generation or false
        //Time Complexity: O(Rows * Columns) here O(width * height)
        public bool NextGeneration(int width, int height, ref bool[,] grid)
        {
            //we create a 2-d temp with the size of the given grid
            //Actually we can either copy the content from the grid to this temp and update the given original grid in the nested iteration
            //But copying the grid takes additional 0(n^2) time here so to avoid that I have just created the temp and updated the temp in the iteration 
            //and finally updated the given grid with the values from temp so, the original grid will have the next generation cell values
            bool[,] temp = new bool[grid.GetLength(0), grid.GetLength(1)];
            int countlivecells = 0;
            //Travel to each cell and set the next generation of that cell
            for (int row = 0; row < width; row++)
                for (int col = 0; col < height; col++)
                {
                    int n = NumberOfAliveNeighbours(row, col, width, height, grid);

                    if (n > 3 || n < 2)
                    {
                        // Any live cell with fewer than two live neighbours dies, as if by loneliness.
                        //Any live cell with more than three live neighbours dies, as if by overcrowding.
                        temp[row,col] = false;
                    }
                    else if (n == 3)
                    {
                        temp[row,col] = true;
                    }
                    else
                    {
                        temp[row, col] = grid[row,col];
                    }

                    if (temp[row, col] == true)
                    {
                        countlivecells++;
                    }
                   
                }

            grid = temp;

            return countlivecells > 0;

        }

        //This funciton will itereate through all its 8 neighbours (Horizondal, vertialcal and diagonal) and ignorea the actualy co-ordinates (x,y)
        public int NumberOfAliveNeighbours(int row, int col, int rowLength, int colLength, bool[,] grid)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                   //check for grid bounds
                    if (i >= 0 && i < rowLength && j >= 0 && j < colLength)
                    {

                        //ignore the current row col
                        if ((i != row || j != col) && grid[i, j] == true)
                        {
                            count++;
                        }
                    }

                }
            }
            return count;
        }
    }
}
