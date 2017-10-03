using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Common
{
    public class SkylineProblem
    {
    }

    // A structure for building
    public class Building
    {
        public int left;  // x coordinate of left side
        public int height;    // height
        public int right; // x coordinate of right side

        public Building(int left, int height, int right)
        {
            this.left = left;
            this.right = right;
            this.height = height;
        }
    }


    // A strip in skyline
    //A rectangular strip is represented as a pair (left, ht) where left is x coordinate of left side of strip and ht is height of strips
    public class Strip
    {
        public int left;  // x coordinate of left side
        public int height; // height
        public Strip(int length, int height)
        {
            this.left = length;
            this.height = height;
        }

    }

    //A skyline is a collection of rectangular strips. 
    public class SkyLine
    {
        public Strip[] strips = null; // Array of strips
        int capacity; // Capacity of strip array
        int n; // Actual number of strips in array (growing size)

        public SkyLine(int size)
        {
            this.strips = new Strip[size];
            this.capacity = size;
            this.n = 0;
            
        }

        public int GetSize()
        {
            return n; 
        }

        // Function to add a strip 'st' to array
        public void append(Strip strip)
        {
            // Check for redundant strip,
            //a strip is redundant if it has same height or left as previous
            if (n > 0 && strips[n - 1].height == strip.height)
            {
                return;
            }
            //if the strip has same left as previous, then update the strip height to get max height
            if (n > 0 && strips[n - 1].left == strip.left)
            {
                strips[n - 1].height = Math.Max(strips[n - 1].height, strip.height);
                return;
            }

            strips[n] = strip;
            n++;
        }
    }
}
