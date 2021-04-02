namespace _02_Fast_and_Furious
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FastAndFurious
    {
        private static Dictionary<string, Node> graph;
        private static SortedDictionary<string, List<KeyValuePair<DateTime, string>>> cars;

        public static void Main(string[] args)
        {
            graph = new Dictionary<string, Node>();
            cars = new SortedDictionary<string, List<KeyValuePair<DateTime, string>>>();

            ReadRoads();

            ReadCars();

            foreach (var car in cars)
            {
                if (IsSpeeding(car, graph))
                {
                    Console.WriteLine(car.Key);
                }
            }

        }

        private static bool IsSpeeding(KeyValuePair<string, List<KeyValuePair<DateTime, string>>> car, Dictionary<string, Node> graph)
        {
            List<KeyValuePair<DateTime, string>> sightings = car.Value.OrderByDescending(x => x.Key).ToList();

            for (int i = 0; i < sightings.Count; i++)
            {
                for (int j = i + 1; j < sightings.Count; j++)
                {
                    TimeSpan travelTime = sightings[i].Key.Subtract(sightings[j].Key);

                    TimeSpan allowedTime = Dijkstra(sightings[j].Value, sightings[i].Value, graph);

                    TimeSpan difference = travelTime.Subtract(allowedTime);

                    if (difference.TotalSeconds < 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static TimeSpan Dijkstra(string start, string end, Dictionary<string, Node> graph)
        {
            // reset distances
            foreach (var city in graph)
            {
                city.Value.TravelTime = decimal.MaxValue;
            }

            HashSet<string> added = new HashSet<string>();
            HashSet<string> visited = new HashSet<string>();

            graph[start].TravelTime = 0;

            BinaryHeap<Node> priorityQueue = new BinaryHeap<Node>();

            priorityQueue.Insert(graph[start]);
            visited.Add(start);

            while (priorityQueue.Count > 0)
            {
                Node currentNode = priorityQueue.ExtractMin();
                visited.Add(currentNode.Name);

                if (currentNode.Name == end)
                {
                    break;
                }

                foreach (var edge in currentNode.Edges)
                {
                    if (!visited.Contains(edge.Child))
                    {
                        if (!added.Contains(edge.Child))
                        {
                            priorityQueue.Insert(graph[edge.Child]);
                            added.Add(edge.Child);
                        }

                        decimal currentTravelTime = currentNode.TravelTime + edge.TravelTime;
                        if (graph[edge.Child].TravelTime > currentTravelTime)
                        {
                            graph[edge.Child].TravelTime = currentTravelTime;
                            priorityQueue.Reorder(graph[edge.Child]);
                        }
                    }
                }
            }

            if (graph[end].TravelTime == decimal.MaxValue)
            {
                return new TimeSpan(0, 0, 0);
            }

            int hours = (int)graph[end].TravelTime;
            decimal rest = (graph[end].TravelTime - hours) * 60;

            int minutes = (int)rest;

            rest = rest - minutes;
            int seconds = (int)Math.Round(rest * 60);

            TimeSpan timeTraveled = new TimeSpan(hours, minutes, seconds);

            return timeTraveled;
        }

        private static void ReadCars()
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "End")
                {
                    break;
                }

                string[] parameters = line.Split();
                string city = parameters[0];
                string licensePlate = parameters[1];
                DateTime time = DateTime.Parse(parameters[2]);

                KeyValuePair<DateTime, string> sighting = new KeyValuePair<DateTime, string>(time, city);

                if (!cars.ContainsKey(licensePlate))
                {
                    cars.Add(licensePlate, new List<KeyValuePair<DateTime, string>>());
                }

                cars[licensePlate].Add(sighting);
            }
        }

        private static void ReadRoads()
        {
            Console.ReadLine();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "Records:")
                {
                    break;
                }

                string[] parameters = line.Split();
                string parent = parameters[0];
                string child = parameters[1];
                decimal distance = decimal.Parse(parameters[2]);
                decimal maxSpeed = decimal.Parse(parameters[3]);
                decimal travelTime = distance / maxSpeed;

                Edge edge = new Edge(parent, child, travelTime);
                Edge reverse = new Edge(child, parent, travelTime);

                if (!graph.ContainsKey(parent))
                {
                    graph.Add(parent, new Node(parent, decimal.MaxValue));
                }

                if (!graph.ContainsKey(child))
                {
                    graph.Add(child, new Node(child, decimal.MaxValue));
                }

                graph[parent].Edges.Add(edge);
                graph[child].Edges.Add(reverse);
            }
        }
    }
}
