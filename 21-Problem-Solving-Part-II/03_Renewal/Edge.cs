namespace _03_Renewal
{
    using System;

    public class Edge : IComparable<Edge>
    {
        public int a;
        public int b;
        public int cost;

        public Edge(int a, int b, int cost)
        {
            this.a = a;
            this.b = b;
            this.cost = cost;
        }

        public int CompareTo(Edge e)
        {
            return cost - e.cost;
        }
    }
}