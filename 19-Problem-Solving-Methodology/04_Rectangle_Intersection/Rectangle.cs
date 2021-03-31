namespace _04_Rectangle_Intersection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Rectangle : IComparable<Rectangle>
    {
        public Rectangle(int minX, int maxX, int minY, int maxY)
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }

        public int MinX { get; set; }

        public int MaxX { get; set; }

        public int MinY { get; set; }

        public int MaxY { get; set; }

        public int CompareTo(Rectangle other)
        {
            return this.MinX.CompareTo(other.MinX);
        }

        public decimal CalculatedArea()
        {
            return Math.Abs((this.MaxX - this.MinX) * (this.MaxY - this.MinY));
        }
    }
}
