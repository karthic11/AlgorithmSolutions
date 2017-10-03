using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Common
{
    public class Triangle
    {
        public int FindTriangleTypeBySides(int a, int b, int c)
        {
            //check all the sides are greater than 0 and sum of any two sides should be greater than third side
            if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
            {
                return (int)TriangleType.Error;
            }
            else if (a == b && b == c)
            {
                //all the sides are equal
                return (int)TriangleType.Equilateral;
            }
            else if (a == b || b == c || a == c)
            {
                //any two side are equal
                return (int)TriangleType.Isosceles;
            }
            else
            {
                //The sides are unequal
                return (int)TriangleType.Scalene;
            }
        }


        public enum TriangleType
        {
            Scalene = 1,
            Isosceles = 2,
            Equilateral = 3,
            Error = 4
        }

    }
}
