namespace _04_Parking_Zones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ParkingZone
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Price { get; set; }

        public ParkingZone(string zoneString)
        {
            var tokens = zoneString.Split(':').ToArray();

            this.Name = tokens[0];

            var data = tokens[1].Split(", ").ToArray();

            this.X = int.Parse(data[0]);
            this.Y = int.Parse(data[1]);
            this.Width = int.Parse(data[2]);
            this.Height = int.Parse(data[3]);
            this.Price = decimal.Parse(data[4]);
        }

        public bool IsInZone(int x, int y)
        {
            return x >= this.X
                && x <= this.X + this.Width
                && y >= this.Y
                && y <= this.Y + this.Height;
        }
    }
}
