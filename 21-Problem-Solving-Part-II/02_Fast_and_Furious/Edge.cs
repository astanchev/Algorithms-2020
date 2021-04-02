namespace _02_Fast_and_Furious
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Edge
    {
        public Edge(string parent, string child, decimal travelTime)
        {
            this.Parent = parent;
            this.Child = child;
            this.TravelTime = travelTime;
        }

        public decimal TravelTime { get; set; }

        public string Parent { get; set; }

        public string Child { get; set; }
    }
}
