namespace _04_Parking_Zones
{
    using System;
    using System.Linq;

    public class ParkingZones
    {
        public static void Main(string[] args)
        {
            var parkingZonesCount = int.Parse(Console.ReadLine());
            var parkingZones = new ParkingZone[parkingZonesCount];

            for (var i = 0; i < parkingZonesCount; i++)
            {
                parkingZones[i] = new ParkingZone(Console.ReadLine());
            }

            var parkingSpots = Console.ReadLine()
                .Split(';')
                .Select(x => ParkingSpot.Parse(x, parkingZones))
                .ToArray();

            var targetPoint = ParkingSpot.Parse(Console.ReadLine(), parkingZones);

            var timeToTravelBlock = int.Parse(Console.ReadLine());

            foreach (var parkingSpot in parkingSpots)
            {
                parkingSpot.Distance = parkingSpot.GetDistance(targetPoint) * 2;
                parkingSpot.UpdateCost(timeToTravelBlock);
            }

            var bestParkingSpot = parkingSpots
                .OrderBy(x => x.Cost)
                .ThenBy(x => x.Distance)
                .First();

            Console.WriteLine(bestParkingSpot);
        }
    }
}
