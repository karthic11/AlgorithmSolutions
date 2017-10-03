using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles
{
    //class Permute
    //{
    //    private void swap(ref char a, ref char b)
    //    {
    //        if (a == b) return;
    //        a ^= b;
    //        b ^= a;
    //        a ^= b;
    //    }

    //    public void setper(char[] list)
    //    {
    //        int x = list.Length - 1;
    //        go(list, 0, x);
    //    }

    //    private string go(char[] list, int k, int m, StringBuilder sb)
    //    {
    //        int i;
    //        if (k == m)
    //        {
    //            return sb.ToString();
    //        }
    //        else
    //        {
    //            for (i = k; i <= m; i++)
    //            {
    //                swap(ref list[k], ref list[i]);
    //                sb.Append(go(list, k + 1, m, sb));
    //                swap(ref list[k], ref list[i]);
    //            }
    //        }

    //        return sb.ToString();
    //    }
    //}
}
