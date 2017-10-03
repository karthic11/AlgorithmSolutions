using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;

namespace Puzzles
{
    //Graph Node - Each graph node contains data, list of adjacantent nodes/neighbors, list of cost
    //Neighbors  - Adjacents nodes
    //Cost  - mapping a weight from the GraphNode to a specific neighbor..weight may be distance, time, weight etc
    //Edge - Making a connections between nodes/vertices and the connection line is called edge
    //Graph - Graph has just the list of nodes/vertices
    //DirectedEdge - The edge formed between the nodes is unidirectional (one way)
    //UndirectedEdge - The edge formed between the nodes is not directional so by default it is considered as bi-directional

    public class GraphNode<T> 
    {
        //Data of the node
        public T Data { get; set; }
        //list of neighbors
        public List<GraphNode<T>> Neighbors { get; set; }
         //list of cost to associast cost with neighbors
        public List<int> Costs { get; set; }
        public bool Visited { get; set; }

        public State Status { get; set; }

        //To solve another problem..having the below prop
        public bool Marked { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }

        public GraphNode()
        {
        }

        public GraphNode(T data)
        {
            this.Data = data;
            this.Neighbors = new List<GraphNode<T>>();
            this.Costs = new List<int>();
        }

        public GraphNode(T data, List<GraphNode<T>> neighbors)
        {
            this.Data = data;
            this.Neighbors = neighbors;
            this.Costs = new List<int>();
        }

        public GraphNode(T data, List<GraphNode<T>> neighbors, List<int> costs)
        {
            this.Data = data;
            this.Neighbors = neighbors;
            this.Costs = costs;
        }

        

        
    }

    //NodeList is the collection of nodes like List<Node>
    public class GraphNodeList<T> : Collection<GraphNode<T>>
    {
        public GraphNodeList() : base() { }

        public GraphNodeList(int initialSize)
        {
            // Add the specified number of items
            for (int i = 0; i < initialSize; i++)
                base.Items.Add(default(GraphNode<T>));
        }

        public GraphNode<T> FindByValue(T value)
        {
            // search the list for the value
            foreach (GraphNode<T> node in Items)
                if (node.Data.Equals(value))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }
    }



    public class KarthicGraph<T>
    {
        //Graph contains list of Graph node
        public List<GraphNode<T>> Nodes { get; set; }
        public GraphNode<T> Root { get; set; }

        public KarthicGraph()
        {
            this.Nodes = new List<GraphNode<T>>();
            this.Root = null;

        }

        public KarthicGraph(List<GraphNode<T>> nodeslist)
        {
            this.Nodes = nodeslist;
            this.Root = null;
        }

        //Add nodes to the graph
        public void AddNode(GraphNode<T> node)
        {
            //add the root
            if (this.Root == null)
            {
                this.Root = node;
            }
            this.Nodes.Add(node);

        }

        public void AddNode(T value)
        {
            //add the root
            if (this.Root == null)
            {
                this.Root = new GraphNode<T>(value);
            }

            this.Nodes.Add(new GraphNode<T>(value));

        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to)
        {
            from.Neighbors.Add(to);

        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to)
        {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);

        }

       
        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }


        public GraphNode<T> FindNodeByValue(T value)
        {
            foreach (GraphNode<T> node in Nodes)
            {
                if (node.Data.Equals(value))
                {
                    return node;
                }
            }

            // if we reached here, we didn't find a matching node
            return null;
        }

        //First search the childrens and then go to siblings
        public void DepthFirstSearch(GraphNode<int> node, StringBuilder sb)
        {
             //base case
            if (node == null)
            {
                return;
            }

            sb.Append(node.Data).Append(',');
            node.Visited = true;

            //loop through the node's neighbors

            foreach (GraphNode<int> neighbor in node.Neighbors)
            {
                if (!neighbor.Visited)
                {
                    DepthFirstSearch(neighbor, sb);
                }
            }
        }

        //First go by siblings and then to childrens
        //This will give an output by each level
        //You got to use iteration with queue - no recurssion
        public void BreadthFirstSearch(GraphNode<int> root, StringBuilder sb)
        {

           
          Queue<GraphNode<int>> queue = new Queue<GraphNode<int>>();

            sb.Append(root.Data).Append(',');
            root.Visited = true;
            //add root to the queue
            queue.Enqueue(root);

            //after done with the root
            while (queue.Count != 0)
            {
                //eject from queue
                GraphNode<int> node = queue.Dequeue();

                //loop through its neighbor
                foreach (GraphNode<int> neighbor in node.Neighbors)
                {
                    if (!neighbor.Visited)
                    {
                        sb.Append(neighbor.Data).Append(',');
                        neighbor.Visited = true;
                        //we have visited only the neighbor not the neighbor adjacent nodes/neighbor so add it to the queue and it does based on the queue priority
                        queue.Enqueue(neighbor);
                    }

                }



            }


        }


        public bool IsRouteExists(GraphNode<int> startnode, GraphNode<int> endnode)
        {
            //Make a breath first traversal

            Queue<GraphNode<int>> queue = new Queue<GraphNode<int>>();
            //since we don't traverse from root we might miss node but the objective here is to find adjacents nodes of startnode and search for the connection to endnode
            queue.Enqueue(startnode);

            //This won't happen but just in case to check start and end are different nodes
            if (startnode.Data == endnode.Data)
            {
                throw new Exception("start and end are the same");

            }

            startnode.Visited = true;

            while (!(queue.Count == 0))
            {
                GraphNode<int> node = queue.Dequeue();

                foreach (GraphNode<int> neighbor in node.Neighbors)
                {
                    if (!neighbor.Visited)
                    {
                        if (neighbor.Data == endnode.Data)
                        {
                            return true;
                        }
                        else
                        {
                            neighbor.Visited = true;
                            queue.Enqueue(neighbor);
                        }
                    }
                }
                
            }

            return false;

        }


        public bool IsRouteExists2(GraphNode<int> startnode, GraphNode<int> endnode)
        {
            //Make a breath first traversal

            Queue<GraphNode<int>> queue = new Queue<GraphNode<int>>();

            foreach (GraphNode<T> node in this.Nodes)
            {
                node.Status = State.Unvisited;
            }
        
            //This won't happen but just in case to check start and end are different nodes
            if (startnode.Data == endnode.Data)
            {
                throw new Exception("start and end are the same");

            }

            startnode.Status = State.Visiting;
            //since we don't traverse from root we might miss node but the objective here is to find adjacents nodes of startnode and search for the connection to endnode
            queue.Enqueue(startnode);


            while (!(queue.Count == 0))
            {
                GraphNode<int> node = queue.Dequeue();

                foreach (GraphNode<int> neighbor in node.Neighbors)
                {
                    if (neighbor != null)
                    {
                        if (neighbor.Status == State.Unvisited)
                        {
                            if (neighbor.Data == endnode.Data)
                            {
                                return true;
                            }
                            else
                            {
                                neighbor.Status = State.Visiting;
                                queue.Enqueue(neighbor);
                            }
                        }
                    }

                    neighbor.Status = State.Visited;
                }

            }

            return false;

        }

    }

    public enum State
    {
        Unvisited, Visited, Visiting
    }

}
