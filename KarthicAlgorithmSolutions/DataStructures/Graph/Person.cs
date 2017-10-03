using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Graph
{
    public class Person
    {
        public string Name { get; set; }
        public Person[] Acquaintances { get; set; }
        public bool Visited { get; set; }

        public Person(string name, Person[] acquaintances)
        {
            if(String.IsNullOrEmpty(name))
            {
                throw new Exception("Name cannot be null or whitespace: " + name);

            }
            this.Name = name;
            this.Acquaintances = acquaintances;
        }


        //This implemenation of Depth First Search does not have the visited logic. That is we will end up in visiting the same node again and will end up in infinite loop
        //DFS delays checking whether a vertex has been discovered until the vertex is popped from the stack rather than making this check before pushing the vertex.


        public bool IsConnectedInNetworkByDFS(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("Name cannot be null or whitespace: " + name);
            }

            Stack<Person> mystack = new Stack<Person>();
            foreach (Person aquaintance in this.Acquaintances)
            {
                mystack.Push(aquaintance);

            }

            do
            {
                var person = mystack.Pop();
                if (person.Name.Equals(name))
                {
                    return true;
                }

                foreach (Person aquaintance in person.Acquaintances)
                {
                    mystack.Push(aquaintance);
                }

            } while (mystack.Count >= 0);

            return false;
        }



        //Breadth First Search will be better to get the shortest paths in an undirected graph. It is optimal to get shortest path in the graph than DFS
        //DFS may require less memory than BFS but it might not yield the shortest path 

        public bool IsConnectedInNetworkByBFS(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("Name cannot be null or whitespace: " + name);
            }
            //check whether we are searing this object name
            //making the check case insensitive
            if (this.Name.ToLower().Equals(name.ToLower()))
            {
                //We can throw an exception here too. It depends on the requirement whether a person can search himself
                return true;
            }
            Queue<Person> myqueue = new Queue<Person>();
            //mark this as visited
            this.Visited = true;
            myqueue.Enqueue(this); //this here is the Peron object

            while (myqueue.Count != 0)
            {
                //eject from queue
                var person = myqueue.Dequeue();
      
                if (person.Acquaintances != null)
                {
                    //loop through  the person acqantainces
                    foreach (Person acquaintance in person.Acquaintances)
                    {
                        //check only if this is not visited before
                        if (!acquaintance.Visited)
                        {
                            if (acquaintance.Name.ToLower().Equals(name.ToLower()))
                            {
                                return true;
                            }

                            acquaintance.Visited = true;
                            //we have visited only the acquaintance not the acquaintance friends (acquaintance of this acquaintance) 
                            //so add it to the queue 
                            myqueue.Enqueue(acquaintance);
                        }

                    }
                }
            }

            return false;
        }
    }
}
