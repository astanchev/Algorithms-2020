namespace _01_Shelter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Shelter
    {
        private static int[] work;
        private static int[] bfsDist;
        private static int capacity;
        private static int endNode;
        private static List<int>[] edges;
        private static int[][] capacities;
        private static int[][] distanceMatrix;

        public static void Main(string[] args)
        {
            var tokens = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int soldiersCount = tokens[0];
            int bunkersCount = tokens[1];
            capacity = tokens[2];

            Point[] soldiers = new Point[soldiersCount + 1];
            var distances = new List<int>();

            for (int i = 1; i <= soldiersCount; i++)
            {
                var solderInput = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                soldiers[i] = new Point(solderInput[0], solderInput[1]);
            }

            distanceMatrix = new int[bunkersCount + 1][];

            for (int i = 1; i <= bunkersCount; i++)
            {
                var bunkerInput = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                distanceMatrix[i] = new int[soldiersCount + 1];

                for (int j = 1; j <= soldiersCount; j++)
                {
                    int distanceX = bunkerInput[0] - soldiers[j].X;
                    int distanceY = bunkerInput[1] - soldiers[j].Y;
                    int distance = distanceX * distanceX + distanceY * distanceY;

                    distances.Add(distance);

                    distanceMatrix[i][j] = distance;
                }
            }

            endNode = soldiersCount + bunkersCount + 1;
            work = new int[endNode + 1];
            bfsDist = new int[endNode + 1];
            capacities = new int[endNode + 1][];

            for (int i = 0; i <= endNode; i++)
            {
                capacities[i] = new int[endNode + 1];
            }

            distances.Sort();

            int bestDistance = distances[distances.Count - 1];
            int low = 0;
            int high = distances.Count - 1;

            while (low <= high)
            {
                int mid = (high + low) / 2;
                int res = DinicConstrained(distances[mid], soldiersCount, bunkersCount);
                if (res == soldiersCount)
                {
                    bestDistance = Math.Min(bestDistance, distances[mid]);
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            Console.WriteLine($"{Math.Sqrt(bestDistance):F6}");
        }

        private static int DinicConstrained(int maxWeight, int soldiersCount, int bunkersCount)
        {
            edges = new List<int>[endNode + 1];
            edges[0] = new List<int>();

            for (int i = 1; i <= soldiersCount; i++)
            {
                edges[i] = new List<int>();
                edges[0].Add(i);
                edges[i].Add(0);
                capacities[0][i] = 1;
                capacities[i][0] = 0;
            }

            edges[endNode] = new List<int>();

            for (int i = 1; i <= bunkersCount; i++)
            {
                edges[soldiersCount + i] = new List<int>();
                edges[soldiersCount + i].Add(endNode);
                edges[endNode].Add(soldiersCount + i);
                capacities[soldiersCount + i][endNode] = capacity;
                capacities[endNode][soldiersCount + i] = 0;

                for (int j = 1; j <= soldiersCount; j++)
                {
                    if (distanceMatrix[i][j] <= maxWeight)
                    {
                        edges[j].Add(soldiersCount + i);
                        edges[soldiersCount + i].Add(j);
                        capacities[j][soldiersCount + i] = 1;
                        capacities[soldiersCount + i][j] = 0;
                    }
                }
            }

            return Dinic(0, endNode);
        }

        private static int Dinic(int source, int destination)
        {
            int result = 0;

            while (Bfs(source, destination))
            {
                for (int i = 0; i < work.Length; i++)
                {
                    work[i] = 0;
                }

                int delta;

                do
                {
                    delta = Dfs(source, int.MaxValue);
                    result += delta;
                } while (delta != 0);
            }

            return result;
        }

        private static bool Bfs(int source, int destination)
        {
            for (int i = 0; i < bfsDist.Length; i++)
            {
                bfsDist[i] = -1;
            }

            bfsDist[source] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                int cuurentNode = queue.Dequeue();
                for (int i = 0; i < edges[cuurentNode].Count; i++)
                {
                    int child = edges[cuurentNode][i];
                    if (bfsDist[child] < 0 && capacities[cuurentNode][child] > 0)
                    {
                        bfsDist[child] = bfsDist[cuurentNode] + 1;
                        queue.Enqueue(child);
                    }
                }
            }

            return bfsDist[destination] >= 0;
        }

        private static int Dfs(int source, int flow)
        {
            if (source == endNode)
            {
                return flow;
            }

            for (int i = work[source]; i < edges[source].Count; i++, work[source]++)
            {
                int child = edges[source][i];

                if (capacities[source][child] <= 0)
                {
                    continue;
                }

                if (bfsDist[child] == bfsDist[source] + 1)
                {
                    int augmentationPathFlow = Dfs(child, Math.Min(flow, capacities[source][child]));

                    if (augmentationPathFlow > 0)
                    {
                        capacities[source][child] -= augmentationPathFlow;
                        capacities[child][source] += augmentationPathFlow;
                        return augmentationPathFlow;
                    }
                }
            }

            return 0;
        }
    }
}
