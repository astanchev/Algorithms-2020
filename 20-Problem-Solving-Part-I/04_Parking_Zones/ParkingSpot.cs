namespace _04_Parking_Zones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ParkingSpot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }
        public decimal Cost { get; set; }
        public ParkingZone ParkingZone { get; set; }

        public ParkingSpot(int x, int y, ParkingZone[] parkingZones)
        {
            this.X = x;
            this.Y = y;
            this.Distance = int.MaxValue;
            this.Cost = decimal.MaxValue;

            foreach (var parkingZone in parkingZones)
            {
                if (!parkingZone.IsInZone(X, Y))
                {
                    continue;
                }

                this.ParkingZone = parkingZone;
                break;
            }
        }

        public static ParkingSpot Parse(string spotString, ParkingZone[] parkingZones)
        {
            var tokens = spotString
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            return new ParkingSpot(tokens[0], tokens[1], parkingZones);
        }

        public int GetDistance(ParkingSpot other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y) - 1;
        }

        public void UpdateCost(int timeToTravelBlock)
        {
            var time = this.Distance * timeToTravelBlock;

            var timeInMinutes = time / 60 + (time % 60 > 0 ? 1 : 0);

            this.Cost = timeInMinutes * this.ParkingZone.Price;
        }

        public override string ToString()
        {
            return $"Zone Type: {this.ParkingZone.Name}; X: {this.X}; Y: {this.Y}; Price: {this.Cost:F2}";
        }
    }
}
