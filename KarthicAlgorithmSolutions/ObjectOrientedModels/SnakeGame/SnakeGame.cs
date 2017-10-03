using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.SnakeGame
{

    //Logic:
    //https://wiki.engr.illinois.edu/display/ece190sp12/Machine+Problem+5
    //http://www.ncsa.illinois.edu/People/kindr/teaching/ece190_sp11/MPs/ece190_sp11_mp5.pdf
    //See the datastructes below
    //Creating methods is uptp the imagination..
    //I can't write all the methods now. do it on the fly
    public class SnakeGame
    {

        Board board;
        Snake Player1 = new Snake();
        TimeSpan Step;
        int growthperfood;
        double Probabilityoffood;
        Food food;
        public SnakeGame()
        {
        }


        public void Initalize()
        {
           board  = new Board(5, 5);

            //Imagine the below board
            int[,] input = new int[,]{{1,0,1,1,0},
                                   {0,1,1,1,0},
                                   {1,1,1,1,1},
                                   {1,0,1,1,1},
                                   {1,1,1,1,1}};

            food = new Food(1);
            //board value 2 means border --which will be out
            //board value 1 means food
            //board value 0 mean free to move

            //Initially board will set up with few food and snake with few length

            //1)Once food is there..snake will find an way to catch the food
            //2) Find the closet way from the start point to end point and travel in that way
            //3) For each move we need to update the snake pointer with the matrix cell values
            //3) When food is catched the snake will grow, add nodes to tail and updateits pointer
            //4) When snake touches border or tail game over

        }


        public void StartGame()
        {

            //Initialize snake with few node and 
            Snake snake1 = new Snake();
            //count the time
            //psuedocode
            //while(snake1.head != tail and snake1.head.pointer != boarder)
            //   Find the closet distance of food
            //    MoveOneStep() -- update the point
            //
            //
        }


        public void StopGame()
        {
        }
        
       

       

    }

    //Snake will be a linkedlist with node as segment
    public class Snake
    {
        public Snake()
        {
        }
        //Actual since the snake needs to grow that is add few nodes to tail..we can use tail as head and head as tail
        SnakeBodySegement Head;
        SnakeBodySegement Tail;
        int foodconsumed = 0;
        int length = 1;
        Point Startpoint;
        Point FoodPoint;
        //when snake moves without any food, the no of segments will be maintained but its pointer will have different data..
        //Snake can move north, east, west
        //Move a point in any closest direction and updates the pointer and return the new head point
        public Point Move(Point startpoint, Point nextpointerinpath, int[,] board)
        {
            //move to the next pointer
            //updated the pointers

            return nextpointerinpath;

        }


        public void UpdatePointerForEachMove(Point CurrentPosition)//we also need direction moved
        {
        }


        public void AddSegementsToTailAfterFood(Point currentpointer, int settingnotoadd)
        {
        }

      



        
    }

    //Linked list node with extra fields
    public class SnakeBodySegement
    {
        //position of the segment in 2d array
        Point point;
        SnakeBodySegement Next;
    }


    //board is a 2 d array of cell
    public class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        int[,] cells;
        public Board(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            cells = new int[rows,cols];
        }

       
        
    }
    public class Food
    {
        //position of the food in 2d array
        Point point;
        public double Probability { get; set; }
        public Food(double probability)
        {
            this.Probability = probability;
        }

        public void randomly_add_food()
        {

            this.point = new Point();//from randowm alogrithm
        }

    }

    public class Point
    {
        public int col { get; set; }
        public int row { get; set; }

        //Food will appear in the random position..
        //Random algorithm will give food position
        public Point GenerateRandomPositionWithinBound()
        {
            //random point
            return new Point();
        }
    }

}
