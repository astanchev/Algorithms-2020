namespace _03_Prims_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EdgeComparer : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            return x.Weight - y.Weight;
        }
    }
}
