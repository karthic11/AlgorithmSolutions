using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{

    public class Tower
    {
        Stack<int> disks;
        public int Index { get; set; }
        public int capacity { get; set; }

        public Tower(int diskcapacity, int index)
        {
            disks = new Stack<int>(diskcapacity);
            capacity = diskcapacity;
        }

        public void Push(int value)
        {
           
            if (disks.Count != capacity)
            {
                //In case of tower new value should be always lesser than the peek of the stack
                if ((disks.Count == 0) || (value < disks.Peek()))
                {
                    disks.Push(value);
                }
                else
                {
                    throw new Exception("cannot add the greater value");
                }
               
            }
            else
            {
                throw new Exception("Out of space");
            }
        }

        public int TowerSize()
        {
            return disks.Count;
        }

        public int Pop()
        {
            if (disks.Count > 0)
            {
                return disks.Pop();
            }
            else
            {
                throw new Exception("Empty disk");
            }
        }


        public void MoveTopToAnotherTower(Tower destination)
        {
            int value = this.Pop();

            if (destination.TowerSize() != capacity)
            {
                destination.Push(value);
            }
            else
            {
                throw new Exception("Out of space");
            }
        }


        public void MoveDisks(int n, Tower destination, Tower buffer)
        {
            //Recurssion is a void function so put a break for this condition
            if (n > 0)
            {
                //To move n disk from origin to destination

                //move (n-1) disks from origin to buffer (so that n is kept ready for move)

                this.MoveDisks((n - 1), buffer, destination);
                //move the nth disk from the origin to destination

                this.MoveTopToAnotherTower(destination);

                //move the (n-1) from the buffer to destination
                buffer.MoveDisks((n - 1), destination, this);
            }
        }
    }
}