namespace _02_Cheap_Town_Tour
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

    public class CheapTownTour
    {
        private static List<Edge> edges;
        private static int[] parents;
        private static int budget = 0;

        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var e = int.Parse(Console.ReadLine());

            edges = ReadEdges(e);

            PrepareParents(n);

            FindBudget();

            Console.WriteLine($"Total cost: {budget}");
        }

        private static void FindBudget()
        {
            foreach (var edge in edges)
            {
                var firstRoot = GetParent(edge.First);
                var secondRoot = GetParent(edge.Second);

                if (firstRoot != secondRoot)
                {
                    parents[firstRoot] = secondRoot;
                    budget += edge.Weight;
                }
            }
        }

        private static int GetParent(int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static void PrepareParents(int nodes)
        {
            parents = new int[nodes];

            for (int node = 0; node < parents.Length; node++)
            {
                parents[node] = node;
            }
        }

        private static List<Edge> ReadEdges(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
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

            return result
                .OrderBy(x => x.Weight)
                .ToList();
        }
    }
}
