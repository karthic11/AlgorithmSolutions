using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
  public partial class MicrosoftIQ : Form
  {
    public MicrosoftIQ()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {


      //Build a two dimentional matrix similar to the picture
      //Build a maze board of size 6x6..
      char[,] maze = new char[,]{{'S', '#', '#', '#', '#', '#'},
                                 {'.', '.', '.', '.', '.', '#'},
                                 {'#', '.', '#', '#', '.', '#'},
                                 {'#', '.', '#', '#', '.', '#'},
                                 {'.', '.', '.', '#', '.', 'G'},
                                 {'#', '#', '.', '.', '.', '#'}};

      //Assumption:
      //This matrix is represented by x,y co-ordinates and x = 0 at the bottom and y = 0 at the top

      //Locate the start position..
      //In this case the start is at (0,0)


      if (FindPath(0, 0, maze.GetLength(0), 'G', maze))
      {
        //if path found 
        //print the maze
        for (int i = 0; i < maze.GetLength(0); i++)
        {
          for (int j = 0; j < maze.GetLength(1); j++)
          {
            char s = maze[i, j];
          }
        }
      }
      //both case set the start to s
      maze[5, 0] = 'S';


    }

    //Find the path in the maze from the given starting point to ending point
    //Base case  (when this recursion should stop..eg when end found, when point goes outside bounds of maze, if obstacle found, if already visited
    //Mark position visited
    //FindPath(North of the point)
    //FindPath(East of the point)
    //FindPath(South of the point)
    //FindPath(West of the point)
    ////Unmark position
    //return
    public bool FindPath(int startx, int starty, int size, char endgoal, char[,] maze)
    {

      //Base case
      //check for outside bounds
      if (startx < 0 || startx >= size || starty < 0 || starty >= size)
      {
        return false;
      }

      //check for obstacle
      if (maze[startx, starty] == '#')
      {
        return false;
      }
      //check if already visited..to prevent loop
      if (maze[startx, starty] == '+')
      {
        return false;
      }

      //check for goal
      if (maze[startx, starty] == endgoal)
      {
        return true;
      }

      //when the code comes means the path is free and goal not found so traverse in all direction

      //Mark the current point as visited
      maze[startx, starty] = '+';

      //search in north of the point
      if (FindPath(startx - 1, starty, size, endgoal, maze) == true)
      {
        return true;
      }
      //search in the east of the point
      if (FindPath(startx, starty + 1, size, endgoal, maze) == true)
      {
        return true;
      }
      //search in the south of the point
      if (FindPath(startx + 1, starty, size, endgoal, maze) == true)
      {
        return true;
      }
      //search in the west of the point
      if (FindPath(startx, starty - 1, size, endgoal, maze) == true)
      {
        return true;
      }

      //goal not found ..unmark the point
       //This is called backtracking bcoz we come back to this where we mared as '+' and update it to '.' bcoz the position doesn't lead to the destination
      maze[startx, starty] = '.';

      return false;


    }

    private void button1_Click(object sender, EventArgs e)
    {

        //http://stackoverflow.com/questions/10099221/breadth-first-search-on-an-8x8-grid-in-java

      char[,] maze = new char[,]{{'S', '#', '#', '#', '#', '#'},
                                 {'.', '.', '.', '.', '.', '#'},
                                 {'#', '.', '#', '#', '#', '#'},
                                 {'#', '.', '#', '#', '#', '#'},
                                 {'.', '.', '.', '#', '.', 'G'},
                                 {'#', '#', '.', '.', '.', '#'}};


        //Updated 
       int[,] backtrackmap = new int[maze.GetLength(0), maze.GetLength(1)];

       CustomCell goalposition = BreadthFirstSearchIn2DArray(0, 0, maze.GetLength(0), 'G', maze, backtrackmap);

        //backtrack will have counter in the position..

        //track from the goal position to the starting and mark the path visited
        char[,] result = BacktrackVisitedPath(goalposition.XAxis, goalposition.YAxis, maze.GetLength(0), 'S', maze, backtrackmap, goalposition.Count);


      //Here is the logic
      //1) Build a graph with only the possible routes by ignoring the obstacle
      //2) Breath-First-Search of the graph and find the shorest route..while finding the route..mark the shortest node path
      //3) Loop through the shortest path (marked on the graph) and update the 2d martix for the found path with the help of co-ordinates on the graph node


      //build a graph
      //KarthicGraph<Char> mazegraph = new KarthicGraph<char>();
      //GraphNode<char> root = new GraphNode<char>('&');
      //mazegraph.AddNode(root);

      //mazegraph.Root = new GraphNode<char>(maze[0, 0]);
      //BuildGraph(0, 0, maze.GetLength(0), 'G', maze, root, mazegraph);

      //The Graph is build with all the possible '.'
      //find the shortest route from the graph

      //GraphNode<char> startpoint = mazegraph.FindNodeByValue('S');
      //GraphNode<char> endpoint = mazegraph.FindNodeByValue('G');

      //StringBuilder sb = new StringBuilder();

      //The shortest distance will be found here and the correspoding co-ordinates will have marked set
      //string route = BreadthFirstSearchFindRoute(startpoint, endpoint, sb);

      //loop throught the graph and get the marked path and set the correspinding two d matrix path with a special character






    }


    public char[,] BacktrackVisitedPath(int endx, int endy, int size, char startgoal, char[,] maze, int[,] backtrackmap, int count)
    {

        Queue<CustomCell> myqueue = new Queue<CustomCell>();
        //start of the maze
        CustomCell cell = new CustomCell(endx, endy, count);
    
        myqueue.Enqueue(cell);
        int counter = 0;
        CustomCell parent = null;
     
        while (myqueue.Count != 0)
        {
            parent = myqueue.Dequeue();
            counter = parent.Count;
      
            //Now we need to find the children for this parent and add to the queue
            counter = counter - 1;
            //The robot can move in any direction..
            //Find out all the direction it can move

           //Move north  (y-1,x) and other direction
           Point[] directions = new Point[4];
           Point north = new Point(parent.XAxis - 1, parent.YAxis);
           Point south = new Point(parent.XAxis + 1, parent.YAxis);
           Point east = new Point(parent.XAxis, parent.YAxis + 1);
           Point west = new Point(parent.XAxis, parent.YAxis - 1);
           directions[0] = north;
           directions[1] = south;
           directions[2] = east;
           directions[3] = west;

           for (int i = 0; i < directions.Length; i++)
           {
               int x = directions[i].xAxis;
               int y = directions[i].yAxis;
               if (CanMoveForBacktrack(x, y, size, backtrackmap, counter))
               {
                   if (maze[y, x] == startgoal)
                   {
                       return maze;
                   }
                   //possible move
                   myqueue.Enqueue(new CustomCell(x, y, counter));

                   //mark visited
                   maze[y, x] = '@';
               }

           }
        }


        //The code will come here after finding the goal
        //use the backtrack map to find the path
        //if no goal is found
        return maze;

    }
      //Algorithm to find no of steps moved to the destination
      //Imagine the matrix as a tree with 4 children and do breadth first search
    public CustomCell BreadthFirstSearchIn2DArray(int startx, int starty, int size, char endgoal, char[,] maze, int[,] backtrackmap)
    {
    
        Queue<CustomCell> myqueue = new Queue<CustomCell>();
        //start of the maze
        CustomCell cell = new CustomCell(0,0,0);
        cell.XAxis = 0;
        cell.YAxis = 0;
        cell.Count = 0;
        myqueue.Enqueue(cell);
        int counter = 0;
        CustomCell parent = null;
        maze[0, 0] = '+'; //mark root as visited
        while (myqueue.Count != 0)
        {
            parent = myqueue.Dequeue();
            counter = parent.Count;
            //Now we need to find the children for this parent and add to the queue
            counter = counter + 1;
            //The robot can move in any direction..
            //Find out all the direction it can move


            //Move north  (y-1,x) and other direction
            Point[] directions = new Point[4];
            Point north = new Point(parent.XAxis - 1, parent.YAxis);
            Point south = new Point(parent.XAxis + 1, parent.YAxis);
            Point east = new Point(parent.XAxis, parent.YAxis + 1);
            Point west = new Point(parent.XAxis, parent.YAxis - 1);
            directions[0] = north;
            directions[1] = south;
            directions[2] = east;
            directions[3] = west;

            for (int i = 0; i < directions.Length; i++)
            {
                int x = directions[i].xAxis;
                int y = directions[i].yAxis;

                if (CanMove(x, y, size, maze))
                {
                    if (maze[y, x] == endgoal)
                    {
                        //This is to return the no of path took..but we need to find the path
                        //return counter;
                        //break;  //break the while loop here 
                        return new CustomCell(x, y, counter); //return the position of the goal with the counter
                    }
                    //possible move
                    myqueue.Enqueue(new CustomCell(x, y, counter));

                    //mark visited
                    maze[y, x] = '+';

                    //store the counter on the backtrack map
                    backtrackmap[y, x] = counter;
                }
            }
        }
        //The code will come here if the goal is not found
        //use the backtrack map to find the path
        //if no goal is found
        return null;

    }

    public bool CanMove(int x, int y, int size, char[,] maze)
    {
        //Base case
        //check for outside bounds
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            return false;
        }

        //check for obstacle
        if (maze[y, x] == '#')
        {
            return false;
        }
        //check if already visited..to prevent loop
        if (maze[y, x] == '+')
        {
            return false;
        }

        return true;

    }

    public bool CanMoveForBacktrack(int x, int y, int size, int[,] backtrack, int countervalue)
    {
        //Base case
        //check for outside bounds
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            return false;
        }

        //check for obstacle
        if (backtrack[y, x] == countervalue)
        {
            return true;
        }
       
        return false;

    }
    public bool BuildGraph(int startx, int starty, int size, char endgoal, char[,] maze, GraphNode<char> parent, KarthicGraph<char> graph)
    {

      //Base case
      //check for outside bounds
      if (startx < 0 || startx >= size || starty < 0 || starty >= size)
      {
        return false;
      }

      //check for obstacle
      if (maze[startx, starty] == '#')
      {
        return false;
      }
      //check if already visited..to prevent loop
      if (maze[startx, starty] == '+')
      {
        return false;
      }


      //when the code comes means the path is free and goal not found so traverse in all direction
      GraphNode<char> child = new GraphNode<char>(maze[startx, starty]);
      child.XAxis = startx;
      child.YAxis = starty;
      // child.Marked = true;
      graph.AddNode(child);
      graph.AddDirectedEdge(parent, child);
      //parent.Neighbors.Add(child);


      //check for goal
      if (maze[startx, starty] == endgoal)
      {
        return true;
      }

      //Mark the current point as visited
      maze[startx, starty] = '+';


      //search in north of the point
      if (BuildGraph(startx - 1, starty, size, endgoal, maze, child, graph))
      {
        return true;
      }

      //search in the east of the point
      if (BuildGraph(startx, starty + 1, size, endgoal, maze, child, graph))
      {
        return true;
      }

      //search in the south of the point
      if (BuildGraph(startx + 1, starty, size, endgoal, maze, child, graph))
      {
        return true;
      }

      //search in the west of the point
      if (BuildGraph(startx, starty - 1, size, endgoal, maze, child, graph))
      {
        return true;
      }

      //goal not found ..unmark the point
      maze[startx, starty] = '.';
      // child.Marked = false;

      return false;


    }

    //First go by siblings and then to childrens
    //This will give an output by each level
    //You got to use iteration with queue - no recurssion
    public string BreadthFirstSearchFindRoute(GraphNode<char> root, GraphNode<char> endpoint, StringBuilder sb)
    {


      Queue<GraphNode<char>> queue = new Queue<GraphNode<char>>();

      sb.Append("(" + root.XAxis + " , " + root.YAxis + ")").Append("->");
      root.Visited = true;
      root.Marked = true;
      //add root to the queue
      queue.Enqueue(root);

      //after done with the root
      while (queue.Count != 0)
      {
        //eject from queue
        GraphNode<char> node = queue.Dequeue();

        //loop through its neighbor
        foreach (GraphNode<char> neighbor in node.Neighbors)
        {
          if (!neighbor.Visited)
          {
            sb.Append("(" + neighbor.XAxis + " , " + neighbor.YAxis + ")").Append("->");

            if (neighbor.Data == endpoint.Data)
            {
              return sb.ToString();
            }
            else
            {
              //sb.Append(neighbor.Data).Append(',');
              neighbor.Visited = true;
              neighbor.Marked = true;
              queue.Enqueue(neighbor);
            }
            //we have visited only the neighbor not the neighbor adjacent nodes/neighbor so add it to the queue and it does based on the queue priority

          }

        }



      }

      return string.Empty;


    }

    private void button4_Click(object sender, EventArgs e)
    {
      string input = this.textBox1.Text;
      this.textBox2.Text = ConvertRelativePathToAbsolute(input);


    }


    public string ConvertRelativePathToAbsolute(string input)
    {
      string[] paths = input.Split('/');
      Stack<string> mystack = new Stack<string>();

      foreach (string path in paths)
      {
        if (path != "." && path != string.Empty)
        {
          if (path == "..")
          {
            mystack.Pop();
          }
          else
          {
            mystack.Push(path);
          }


        }

      
      }

      //At the end stack will have only the aboulte path folder..build the path from stack

      StringBuilder sb = new StringBuilder();
      while (mystack.Count != 0)
      {
        sb.Insert(0, "/" + mystack.Pop());
      }

      return sb.ToString();


    }

    private void button3_Click(object sender, EventArgs e)
    {

      string input = this.textBox1.Text;
      this.textBox2.Text = ConvertRelativePathToAbsolute2(input);

    }

    public string ConvertRelativePathToAbsolute2(string input)
    {
      string[] paths = input.Split('/');

      char[] output = new char[input.Length];
      int index = 0;
      

      foreach (string path in paths)
      {
        if (path != "." && path != string.Empty)
        {
          if (path == "..")
          {
            index = index - 2;
          }
          else
          {

            output[index] = '/';
            index++;
            output[index] = Convert.ToChar(path);
            index++;

          }
        }
      }


      return new string(output, 0, index);

   
   }

    private void button6_Click(object sender, EventArgs e)
    {

      string input = this.textBox1.Text;
      this.textBox2.Text = ConvertRelativePathToAbsolute3(input.ToCharArray());

    }


    public string ConvertRelativePathToAbsolute3(char[] input)
    {

      int outputindex = 0;
      int parentflagcount = 0;

      for (int i = 0; i < input.Length; i++)
      {

        if (input[i] == '.')
        {

            //yes .. move to parent
            parentflagcount++;
          //check if this is ..
          if (input[i + 1] == '.')
          {
           
            outputindex = outputindex - 2;
            i = i + 2;
             
          }
          else
          {
            //skip the next / and the for loop will increament another step to move to ..
             i++;
          }

        }
        else
        {

          if (parentflagcount > 0)
          {
            input[outputindex] = input[i];
          }

           outputindex++;
        }

      }

      return new string(input, 0, outputindex);


    }

    private void button7_Click(object sender, EventArgs e)
    {
      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);
      int size = Convert.ToInt16(this.textBox5.Text);

      KarthicCircularQueue<int> circularqueue = new KarthicCircularQueue<int>(size);

      foreach (int i in input)
      {
        circularqueue.Enqueue(i);
      }

      StringBuilder sb = new StringBuilder();

      while (!circularqueue.IsEmpty)
      {
        sb.Append(circularqueue.Dequeue().ToString()).Append(",");
      }

      this.textBox3.Text = sb.ToString();

    }

    private void button8_Click(object sender, EventArgs e)
    {

      int[] input = AlgorithmHelper.ConvertCommaSeparetedStringToInt(this.textBox4.Text);
      int size = Convert.ToInt16(this.textBox5.Text);



      CustomCircularQueue<int> circularqueue = new CustomCircularQueue<int>(size);


      foreach (int i in input)
      {
        circularqueue.Enqueue(new CircularQueueItem<int>(i));
      }

      StringBuilder sb = new StringBuilder();

      //while (!circularqueue.IsEmpty)
      //{
      //  sb.Append(circularqueue.Dequeue().Data.ToString()).Append(",");
      //}


      //Test Cases:

      //The queue has 6,2,3,4,5

      //retrive the next 2..don't dequeue ...just set retrive
      circularqueue.Get(); //inside this method we set the retrive prop to true  //read the 2
      //read 3 so when we dequeue 2 won't be removed only 3
      var item1 = circularqueue.Dequeue();

      circularqueue.Get(); //read 4
      var item2 = circularqueue.Dequeue(); // remove 5
      //dequeue two items...2 shouldn't be dequeued only 3 and 4
  
 
      

      //Now the buffer is full and 2 and 3 are read so 
      //when we add new item it should overwrite 4 instead of 2
      //circularqueue.Enqueue(7);
      //circularqueue.Enqueue(8);

      this.textBox3.Text = sb.ToString();


    }

    private void button5_Click(object sender, EventArgs e)
    {

    }

    public class CustomCell
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public int Count { get; set; }

        public CustomCell(int x, int y, int count)
        {
            this.XAxis = x;
            this.YAxis = y;
            this.Count = count;
        }
    }

    private void button9_Click(object sender, EventArgs e)
    {
        //Don't come here ..it is wrong somewhere
        //Input Maze 5 * 6 
        char[,] maze = new char[,]{{'#','.','.','.','.', '#'},  //00 01 02 03 04 05
                                   {'S','.','#','#','.', '#'},  //10 11 12 13 14 15
                                   {'#','.','#','.','.', 'G'},
                                   {'.','.','.','.','#', '#'},
                                   {'#','.','.','.','#', '#'}};

         int[,] backtrack = new int[maze.GetLength(0), maze.GetLength(1)];

       //you are given the start location here  10 ...
        //find the endlocation in a shortest distance and mark the path traveled in the maze without altering another path
        int startrow = 1;
        int startcol = 0; //(10)
        char endgoal = 'G';
        MazeCell endlocation = MoveRobotInBFSManner(startrow, startcol, maze.GetLength(0), maze.GetLength(1), maze, backtrack, endgoal);

      
        //backtrack done to get the shortest distance and mark the matrix visited
        char[,] result = BacktrackVisitedPath(endlocation.Row, endlocation.Col, maze.GetLength(0), 'S', maze, backtrack, endlocation.StepNumber);


        
    

    }

    //this method return the end location and also return/populates the backtrack array with the counter/step value which will be used later to get the shortest distance
    private MazeCell MoveRobotInBFSManner(int row, int col, int rowlength, int collength, char[,] maze, int[,] backtrack, char endgoal)
    {
        //We create the mazecell
        int counter = 0;
        MazeCell startnode = new MazeCell(row, col, counter);
        Queue<MazeCell> myqueue = new Queue<MazeCell>();
        MazeCell parent = null;
        int rowno = 0;
        int colno = 0;

        //Look if the given maze is not allowed to edit..then we can use a dictionary of Mazecell and query the mazecell collection to see whether these row and col is visited
        //This can be used if the given maze is not allowed to edit...and we can use visited as the bool in the dictionary
        Dictionary<string, bool> MazeCellVisitTracker = new Dictionary<string, bool>();
        //Mark the start as visited
        MazeCellVisitTracker.Add(startnode.Row + "-" + startnode.Col, true);
       
        
        myqueue.Enqueue(startnode);
        while (myqueue.Count != 0)
        {
            parent = myqueue.Dequeue();
            counter = parent.StepNumber;

            //The robot can move in four direction North, south, east and west
            //north
            rowno = parent.Row - 1;
            colno = parent.Col;

            MazeCell possiblemove = RobotMove(rowno, colno, rowlength, collength, counter, maze, endgoal, MazeCellVisitTracker);
            if (possiblemove != null)
            {
                //this mean robot has a possible solution or found the goal
                if (possiblemove.LastCell)
                {
                    return possiblemove;
                }
                else
                {
                    //this will be a possible sln move so add it to queue and backtrack
                    myqueue.Enqueue(possiblemove); //this already contains the counter incremented
                    backtrack[possiblemove.Row, possiblemove.Col] = possiblemove.StepNumber;
                }
            }

            //south
            rowno = parent.Row + 1;
            colno = parent.Col;

             possiblemove = RobotMove(rowno, colno, rowlength, collength, counter, maze, endgoal, MazeCellVisitTracker);
            if (possiblemove != null)
            {
                //this mean robot has a possible solution or found the goal
                if (possiblemove.LastCell)
                {
                    return possiblemove;
                }
                else
                {
                    //this will be a possible sln move so add it to queue and backtrack
                    myqueue.Enqueue(possiblemove); //this already contains the counter incremented
                    backtrack[possiblemove.Row, possiblemove.Col] = possiblemove.StepNumber;
                }
            }

            //east
            rowno = parent.Row;
            colno = parent.Col + 1;

            possiblemove = RobotMove(rowno, colno, rowlength, collength, counter, maze, endgoal, MazeCellVisitTracker);
            if (possiblemove != null)
            {
                //this mean robot has a possible solution or found the goal
                if (possiblemove.LastCell)
                {
                    return possiblemove;
                }
                else
                {
                    //this will be a possible sln move so add it to queue and backtrack
                    myqueue.Enqueue(possiblemove); //this already contains the counter incremented
                    backtrack[possiblemove.Row, possiblemove.Col] = possiblemove.StepNumber;
                }
            }

            //west
            rowno = parent.Row;
            colno = parent.Col - 1;

            possiblemove = RobotMove(rowno, colno, rowlength, collength, counter, maze, endgoal, MazeCellVisitTracker);
            if (possiblemove != null)
            {
                //this mean robot has a possible solution or found the goal
                if (possiblemove.LastCell)
                {
                    return possiblemove;
                }
                else
                {
                    //this will be a possible sln move so add it to queue and backtrack
                    myqueue.Enqueue(possiblemove); //this already contains the counter incremented
                    backtrack[possiblemove.Row, possiblemove.Col] = possiblemove.StepNumber;
                }
            }


        }
        
        return null;
    }


    private MazeCell RobotMove(int row, int col, int rowlength, int collength, int stepno, char[,] maze, char endgoal, Dictionary<string, bool> MazeCellVisitTracker)
    {
        //If the move is outside bonds
        if (row < 0 || row >= rowlength || col < 0 || col >= collength)
        {
            return null;
        }

        //if the moved position is alredy visited
        //Look if the given maze is not allowed to edit..then we can use a dictionary of Mazecell and query the mazecell collection to see whether these row and col is visited
        //if (maze[row, col] == '+')
        string visitedkey = row + "-" + col;
        if(MazeCellVisitTracker.ContainsKey(visitedkey) && (MazeCellVisitTracker[visitedkey] == true))
        {
            return null;
        }

        if (maze[row, col] == endgoal)
        {
            MazeCell endcell = new MazeCell(row, col, stepno);
            endcell.LastCell = true;
            return endcell;
        }

        //Move is valid there might be possible solution to end goal so mark this as visited 
        //and increment the counter and add it to queue
        
        //mark visited
        MazeCellVisitTracker.Add(row + "-" + col, true);

        stepno = stepno + 1;

        MazeCell possiblemove = new MazeCell(row, col, stepno);


        return possiblemove;

    }

  }

    public class MazeCell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int StepNumber { get; set; }
        public bool LastCell { get; set; }
  

        public MazeCell(int row, int col, int stepno)
        {
            this.Row = row;
            this.Col = col;
            this.StepNumber = stepno;
      
        }

    }
}