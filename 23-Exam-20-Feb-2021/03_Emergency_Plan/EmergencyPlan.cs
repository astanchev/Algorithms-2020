namespace _03_Emergency_Plan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wintellect.PowerCollections;

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class EmergencyPlan
    {
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static int[] prev;

        public static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var ExitRooms = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

            var edgesCount = int.Parse(Console.ReadLine());

            edgesByNode = ReadGraph(edgesCount);

            var inputTime = Console.ReadLine()
                .Split(':')
                .Select(int.Parse)
                .ToArray();

            var timeInSecondsToSafeExit = inputTime[1] + (inputTime[0] * 60);

            var end = ExitRooms;

            for (int room = 0; room < nodesCount; room++)
            {
                if (ExitRooms.Contains(room))
                {
                    continue;
                }

                if (!edgesByNode.ContainsKey(room))
                {
                    Console.WriteLine($"Unreachable {room} (N/A)");
                    continue;
                }

                var start = room;

                var maxNode = edgesByNode.Keys.Max();

                int[] distances = FillDistances(edgesCount);

                FillPrev(nodesCount);

                distances[start] = 0;
                prev[start] = -1;

                CalculateDistances(end, start, distances);

                int minTimeExit = FindMinTimeToExit(end, distances);

                bool isThereExit = HasExit(end);

                if (minTimeExit < 1)
                {
                    continue;
                }

                minTimeExit = PrintRoomDetails(timeInSecondsToSafeExit, room, minTimeExit, isThereExit);
            }
        }

        private static int PrintRoomDetails(int timeInSecondsToSafeExit, int room, int minTimeExit, bool isThereExit)
        {
            if (isThereExit)
            {
                if (minTimeExit > timeInSecondsToSafeExit)
                {
                    int hours = minTimeExit / 3600;
                    int mins = (minTimeExit % 3600) / 60;
                    minTimeExit = minTimeExit % 60;

                    Console.WriteLine($"Unsafe {room} ({hours:D2}:{mins:D2}:{minTimeExit:D2})");
                }
                else
                {
                    int hours = minTimeExit / 3600;
                    int mins = (minTimeExit % 3600) / 60;
                    minTimeExit = minTimeExit % 60;

                    Console.WriteLine($"Safe {room} ({hours:D2}:{mins:D2}:{minTimeExit:D2})");
                }
            }
            else
            {
                Console.WriteLine($"Unreachable {room} (N/A)");
            }

            return minTimeExit;
        }

        private static bool HasExit(int[] end)
        {
            var isThereExit = false;

            foreach (var end1 in end)
            {
                isThereExit = prev[end1] != int.MaxValue;

                if (isThereExit == true)
                {
                    break;
                }
            }

            return isThereExit;
        }

        private static int FindMinTimeToExit(int[] end, int[] distances)
        {
            var minTimeExit = 0;

            if (end.Length > 1)
            {
                var timeOfEnds = new List<int>();

                foreach (var end1 in end)
                {
                    timeOfEnds.Add(distances[end1]);
                }

                minTimeExit = timeOfEnds.Min();
            }
            else
            {
                minTimeExit = distances[end[0]];
            }

            return minTimeExit;
        }

        private static void CalculateDistances(int[] end, int start, int[] distances)
        {
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            queue.Add(start);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();

                var children = edgesByNode[minNode];

                if (end.Contains(minNode))
                {
                    break;
                }

                foreach (var child in children)
                {
                    var childNode = child.First == minNode ? child.Second : child.First;

                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = child.Weight + distances[minNode];

                    if (newDistance < distances[childNode])
                    {
                        distances[childNode] = newDistance;
                        prev[childNode] = minNode;
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }
            }
        }

        private static void FillPrev(int nodesCount)
        {
            prev = new int[nodesCount + 1];
            for (int j = 0; j < prev.Length; j++)
            {
                prev[j] = int.MaxValue;
            }
        }

        private static int[] FillDistances(int edgesCount)
        {
            var distances = new int[edgesCount + 1];
            for (int j = 0; j < distances.Length; j++)
            {
                distances[j] = int.MaxValue;
            }

            return distances;
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split();

                var inputTime = edgeData[2].Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                var firstNode = int.Parse(edgeData[0]);
                var secondNode = int.Parse(edgeData[1]);
                var weight = inputTime[1] + (inputTime[0] * 60);

                if (!result.ContainsKey(firstNode))
                {
                    result.Add(firstNode, new List<Edge>());
                }

                if (!result.ContainsKey(secondNode))
                {
                    result.Add(secondNode, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight,
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }

            return result;
        }
    }
}
