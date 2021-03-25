namespace _04_Big_Trip
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

    public class BigTrip
    {
        private static List<Edge>[] graph;
        private static double[] distances;        
        private static int[] prev;

        public static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(nodes, edgesCount);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();

            // Initialize distances with negative Infinity
            PrepareCollections();
            distances[source] = 0;

            //Topological sort all vertices
            //and find distances in that order
            CalculateDistances(sortedNodes, distances);

            var path = ReconstructPath(destination);

            Console.WriteLine(distances[destination]);
            Console.WriteLine(string.Join(" ", path));
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

        private static void PrepareCollections()
        {
            distances = new double[graph.Length + 1];
            prev = new int[graph.Length + 1];

            Array.Fill(prev, -1);
            Array.Fill(distances, double.NegativeInfinity);
        }

        private static void CalculateDistances(Stack<int> sortedNodes, double[] distances)
        {
            // do for every node U in topological order
            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                //do for every adjacent node V of U
                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }
        }

        private static void ReadGraph(int nodes, int edgesCount)
        {
            graph = new List<Edge>[nodes + 1];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }

        private static Stack<int> TopologicalSorting()
        {
            var visited = new bool[graph.Length];
            var sorted = new Stack<int>();

            for (int node = 1; node < graph.Length; node++)
            {
                DFS(node, visited, sorted);
            }

            return sorted;
        }

        private static void DFS(int node, bool[] visited, Stack<int> sorted)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, sorted);
            }

            sorted.Push(node);
        }
    }
}
