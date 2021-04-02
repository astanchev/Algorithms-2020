namespace _02_Fast_and_Furious
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Node : IComparable<Node>
    {
        public Node(string name, decimal travelTime)
        {
            this.Name = name;
            this.Edges = new List<Edge>();
            this.TravelTime = travelTime;
        }

        public string Name { get; set; }

        public List<Edge> Edges { get; private set; }

        public decimal TravelTime { get; set; }

        public int CompareTo(Node other)
        {
            return this.TravelTime.CompareTo(other.TravelTime);
        }
    }
}
