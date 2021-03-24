namespace _01_Bellman_Ford
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    public class BellmanFord
    {
        private static List<Edge> edges;
        private static double[] distances;
        private static int[] prev;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();

            ReadEdges(edgesCount);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            PrepareCollections(nodes);

            distances[source] = 0;

            RelaxEdges(nodes);

            var hasNegativeCycle = CheckForNegativeCycle();

            if (hasNegativeCycle)
            {
                Console.WriteLine("Negative Cycle Detected");
            }
            else
            {
                var path = ReconstructPath(destination);

                Console.WriteLine(string.Join(" ", path));
                Console.WriteLine(distances[destination]);
            }
        }

        private static void PrepareCollections(int nodes)
        {
            distances = new double[nodes + 1];
            prev = new int[nodes + 1];

            Array.Fill(prev, -1);
            Array.Fill(distances, double.PositiveInfinity);
        }

        private static Stack<int> ReconstructPath(int destination)
        {
            var path = new Stack<int>();
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static bool CheckForNegativeCycle()
        {
            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(edge.From))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    return true;
                }
            }

            return false;
        }

        private static void RelaxEdges(int nodes)
        {
            //All edges are relaxed n-1 times, where n is number of nodes
            for (int i = 0; i < nodes - 1; i++)
            {
                var updated = false;

                //Relax all edges
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    //Relaxation
                    var newDistance = distances[edge.From] + edge.Weight;
                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;

                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                edges.Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }
    }
}
