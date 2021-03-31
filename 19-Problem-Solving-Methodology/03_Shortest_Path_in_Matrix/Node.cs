namespace _03_Shortest_Path_in_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Node  : IComparable<Node>
    {
        public Node(int id, int value, int distanceFromStart = int.MaxValue)
        {
            this.Id = id;
            this.Value = value;
            this.DistanceFromStart = distanceFromStart;
        }

        public int Id { get; set; }

        public int Value { get; set; }

        public int DistanceFromStart { get; set; }

        public Node Previous { get; set; }

        public int CompareTo(Node other)
        {
            int compare = this.DistanceFromStart.CompareTo(other.DistanceFromStart);

            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }

            return compare;
        }

        public override string ToString()
        {
            return string.Format($"value:{this.Value}; dis:{this.DistanceFromStart}", this.Value, this.DistanceFromStart);        }
    }
}
