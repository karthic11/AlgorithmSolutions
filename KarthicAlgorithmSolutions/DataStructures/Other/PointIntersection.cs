using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.DataStructures.Other
{
    public class PointIntersection
    {

        //Easy and Simple Method using Slope and y-Intercept
        public MathPoint GetIntersectionPointUsingSlopes(MathPoint p1, MathPoint q1, MathPoint p2, MathPoint q2)
        {
            //Rearrange the points based on x co-ordinate values to make the calculate easier 
            //start point should be lesser than endpoint that is p1 < q1
            //Point 1 is lesser than point 2 .. p1.x < p2.x
            //p1 and q1 makes a line
            if (p1.x > q1.x)
            {
                Swap(p1, q1);
            }

            if (p2.x > q2.x)
            {
                Swap(p2, q2);
            }

            //check both lines
            if (p1.x > p2.x)
            {
                Swap(p1, q1);
                Swap(p2, q2);
            }

            //Now all the point are sorted x-axis value
            Line line1 = new Line(p1, q1);
            Line line2 = new Line(p2, q2);

            //Two lines will intersect on two condition

            //conditon 1: (both line are parallen and overlapping
            //   if the lines are parallel (slope are equal), they intersect only if they have the same y-interecept and start 2 in on line 1
            //Condtion 2 : If two line intersect, then they will have same point of intersection and the point of intesection will be with both lines coordinates

            if(line1.Slope == line2.Slope)
            {
                if(line1.Intercept == line2.Intercept && IsBetween(p1, p2, q1))
                {
                        return p2; //point of intersection will be p2 
 
                }

                return null;
            }


            /* Get co-ordinatinates of intersection based on the forumla
             * If the 2 lines intersect, then the at the point of intersection both lines will share the same x-coordinate and y-coordinate
             * so y = mx + b  for lines will be equal to y = mx + b of line 2
             * x = (b2 -b1)/ (m1- m2)
             */
            double x = (line2.Intercept - line1.Intercept)/ (line1.Slope - line2.Slope);
            double y = x * line1.Slope + line1.Intercept; //y = mx + b where (x,y) are any co-ordinates in the line
            MathPoint IntersectionPoint = new MathPoint(x, y);

            //check whether the intesection point x and y is within line segement range
            if (IsBetween(line1.StartPoint, IntersectionPoint, line1.EndPoint) && IsBetween(line2.StartPoint, IntersectionPoint, line2.EndPoint))
            {
                return IntersectionPoint;
            }
         
            return null;

        }

        private void Swap(MathPoint p1, MathPoint p2)
        {
            double temp_X = p1.x;
            double temp_Y = p1.y;

            p1.x = p2.x;
            p1.y = p2.y;

            p2.x = temp_X;
            p2.y = temp_Y;


        }

        private bool IsBetween(MathPoint start, MathPoint middle, MathPoint end)
        {
            return IsBetweenValue(start.x, middle.x, end.x) && IsBetweenValue(start.y, middle.y, end.y);
        }

        private bool IsBetweenValue(double start, double middle, double end)
        {
            if(start < end)
            {
                if(middle >= start && middle <= end)
                {
                    return true;
                }
            }
            else
            {
                 if(middle >= end && middle <= start)
                 {
                     return true;
                 }
            }

            return false;
        }

        public bool DoLinesIntersect(MathPoint p1, MathPoint q1, MathPoint p2, MathPoint q2)
        {

            // Find the four orientations needed for general and  special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);


            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private int orientation(MathPoint p, MathPoint q, MathPoint r)
        {
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
            int val = Convert.ToInt32((q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y));

            if (val == 0) return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // Given three colinear MathPoints p, q, r, the function checks if
        // MathPoint q lies on line segment 'pr'
        private bool onSegment(MathPoint p, MathPoint q, MathPoint r)
        {
            if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Max(p.x, r.x) &&
                q.y <= Math.Max(p.y, r.y) && q.y >= Math.Max(p.y, r.y))
            {
                return true;
            }

            return false;
        }

        //Given: List of points, find the lines that passes the most number of points
        //Logic:
        /*  We represent line as slope and intercept (y or x) then two lines will be equal or same line if their slope and intercept is same
         *  Note: parallel lines will have same slope but won't have same interecept
         *  Build ht with key as slope and value as List<Line>. Iterate every pair of points and populate the ht
         *  The slope that has max number of lines will mostly be the best line..but iterate over the list to check if their intercept is same and count is max
         * 
         *  The problem is ht key can't be double..double won't have accurate key so use the concept of epsilon
         *  Round down the slope to next epsilon value and use that flooredslope as the key
         *  When we look for equivalent look for flooredslope, flooredslope + epsilon, flooredslope - epsilon in ht
         *  and also check all the lines are equal. 
         *  We can't assume that all the lines in a flooredslope key will be equal bcoz parallel line will also have same slope..check for equivalen in line method
         */
        public Line FindBestLine(MathPoint[] points)
        {
            //ht key as flooredslope and values is list<line>
            Dictionary<double, List<Line>> ht = PopulatePointsToht(points);

            return GetBestLine(ht);

        }

        private Dictionary<double, List<Line>> PopulatePointsToht(MathPoint[] points)
        {
            Dictionary<double, List<Line>>  ht = new Dictionary<double,List<Line>>();

            //iterate every pair of points
            for(int i=0; i < points.Length; i++)
            {
                for(int j=i+1; j < points.Length; j++)
                {
                    Line line = new Line(points[i], points[j]);
                    double key = line.GetFlooredSlope(line.Slope);
                    List<Line> lines = null;
                    if(ht.ContainsKey(key))
                    {
                        lines = ht[key];
                        lines.Add(line);
                    }
                    else
                    {
                        lines = new List<Line>();
                        lines.Add(line);
                        ht.Add(key, lines);
                    }
                   
                }
            }

            return ht;
        }


        private Line GetBestLine(Dictionary<double, List<Line>> ht)
        {
        
            Line bestline = null;
            int maxcount = 0;
            //iterate over the ht
            foreach(KeyValuePair<double, List<Line>> pair in ht)
            {
              
                 //*  We can't assume that all the lines in a flooredslope key will be equal bcoz parallel line will also have same slope..check for equivalen in line method
              
                List<Line> lines = pair.Value;
                foreach(Line line in lines)
                {
                    // When we look for equivalent look for flooredslope, flooredslope + epsilon, flooredslope - epsilon in ht
                    int totalcount = GetTotalCountofSlopeUsingEpsilon(ht, line);

                    if(totalcount > maxcount)
                    {
                        maxcount = totalcount;
                        bestline = line;
                    }
                }

            }

            return bestline;
        }

        private int GetTotalCountofSlopeUsingEpsilon(Dictionary<double, List<Line>> ht, Line line)
        {
            int count = 0;

            double flooredslope = line.GetFlooredSlope(line.Slope);
            count += CountEqualLines(ht[flooredslope], line);
            if (ht.ContainsKey(flooredslope + Line.epsilon))
            {
                count += CountEqualLines(ht[flooredslope + Line.epsilon], line);
            }
            if(ht.ContainsKey(flooredslope - Line.epsilon))
            {
            count += CountEqualLines(ht[flooredslope - Line.epsilon], line);
            }

            return count;
            

        }

        private int CountEqualLines(List<Line> lines, Line compareline)
        {
            int count =0;
            foreach(Line line in lines)
            {
                //check whether the compare line and the given lines are equal
                if(compareline.IsEquilvalent(line))
                {
                    count++; 
                }
            }

            return count;
        }

    }

    public class MathPoint
    {
        public double x { get; set; }
        public double y { get; set; }

        public MathPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Line
    {
        public MathPoint StartPoint { get; set; }
        public MathPoint EndPoint { get; set; }
        public double Slope { get; set; }
        public double Intercept { get; set; } //usually it is y-intercept..it will be x-intercept for infinte slope

        //custom prob
        public static double epsilon = .0001;
        private bool infiniteslope = false;

        public Line(MathPoint startpoint, MathPoint endpoint)
        {
            this.StartPoint = startpoint;
            this.EndPoint = endpoint;

            //Infinite slope is the slope of the vertical line where there is no change in x-axis..so check for that
            if (Math.Abs(startpoint.x - endpoint.x) > epsilon)
            {
                this.Slope = (endpoint.y - startpoint.y) / (endpoint.x - startpoint.x);
                this.Intercept = endpoint.y - (Slope * endpoint.x); // y = mx + b where m is slope, b is y-intercept and (x,y) is any point in the line
            }
            else
            {
                infiniteslope = true;
                Intercept = startpoint.x; //x-intercept
            }

        }

        public double GetFlooredSlope(double slope)
        {
            int r = (int)(slope / epsilon);
            return (double)r * epsilon;
        }


        public bool IsEquilvalent(Line line2)
        {
            if (IsEqualValue(this.Slope, line2.Slope) && IsEqualValue(this.Intercept, line2.Intercept) && this.infiniteslope == line2.infiniteslope)
            {
                return true;
            }

            return false;
        }

        //Always compre the values taking epsilon into account
        public bool IsEqualValue(double value1, double value2)
        {
            if (Math.Abs(value1 - value2) < epsilon)
            {
                return true;
            }

            return false;
        }



    }

    
       

}
