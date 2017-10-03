using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
  public class TwoDimensitionArrayStore
  {
    public Point point { get; set; }
    public int Data { get; set; }

    public TwoDimensitionArrayStore(int x, int y, int Data)
    {

      point = new Point();
      this.point.xAxis = x;
      this.point.yAxis = y;
      this.Data = Data;
    }
  }

  public class  Point
  {
      public int xAxis { get; set; }
      public int yAxis { get; set; }
      public Point(int x, int y)
      {
          this.xAxis = x;
          this.yAxis = y;

      }
      public Point()
      {
      }
  }
}
