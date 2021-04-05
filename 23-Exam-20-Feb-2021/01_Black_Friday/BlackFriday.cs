namespace _01_Black_Friday
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class BlackFriday
    {
        private static List<Edge> edges;
        private static int[] roots;

        public static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            edges = ReadEdges(edgesCount);

            var sortedEdges = edges
                .OrderBy(e => e.Weight)
                .ToList();

            CreateRoots(nodesCount);

            int totalCost = FindTotalCost(sortedEdges);

            Console.WriteLine(totalCost);
        }

        private static int FindTotalCost(List<Edge> sortedEdges)
        {
            var totalCost = 0;

            foreach (var edge in sortedEdges.ToList())
            {
                var firstRoot = GetRoot(edge.First);
                var secondRoot = GetRoot(edge.Second);

                if (firstRoot != secondRoot)
                {
                    roots[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }

            return totalCost;
        }

        private static void CreateRoots(int nodesCount)
        {
            roots = new int[nodesCount];
            for (int node = 0; node < nodesCount; node++)
            {
                roots[node] = node;
            }
        }

        private static int GetRoot(int node)
        {
            while (node != roots[node])
            {
                node = roots[node];
            }

            return node;
        }

        private static List<Edge> ReadEdges(int edgesCount)
        {
            var result = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                result.Add(new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                });
            }

            return result;
        }
    }
}
