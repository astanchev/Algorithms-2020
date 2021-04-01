namespace _03_Lumber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Log
    {
        private readonly int x1;
        private readonly int y1;
        private readonly int x2;
        private readonly int y2;

        public Log(string coordinatesStr, int index)
        {
            var coordinates = coordinatesStr
                .Split()
                .Select(int.Parse)
                .ToArray();

            this.x1 = Math.Min(coordinates[0], coordinates[2]);
            this.y1 = Math.Max(coordinates[1], coordinates[3]);
            this.x2 = Math.Max(coordinates[0], coordinates[2]);
            this.y2 = Math.Min(coordinates[1], coordinates[3]);

            this.Index = index;
        }

        public int Index { get; }

        public bool Overlap(Log other)
        {
            return this.x1 <= other.x2 && other.x1 <= this.x2 &&
                   this.y1 >= other.y2 && other.y1 >= this.y2;
        }

        public override string ToString()
        {
            return $"{Index}: {this.x1} {this.y1}, {this.x2} {this.y2}";
        }
    }
}
