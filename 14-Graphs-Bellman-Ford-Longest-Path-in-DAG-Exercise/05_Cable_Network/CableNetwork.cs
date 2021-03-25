namespace _05_Cable_Network
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

    public class CableNetwork
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> spanningTree;
        private static int totalBudget = 0;
        private static int usedBudget = 0;

        public static void Main(string[] args)
        {
            totalBudget = int.Parse(Console.ReadLine());
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<Edge>>();
            spanningTree = new HashSet<int>();

            ReadGraph(edges);

            Prim();

            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static void Prim()
        {
            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            queue.AddMany(spanningTree.SelectMany(n => graph[n]));

            while (queue.Count > 0)
            {
                var minEdge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(minEdge.First, minEdge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (totalBudget >= minEdge.Weight)
                {
                    totalBudget -= minEdge.Weight;
                    usedBudget += minEdge.Weight;
                    spanningTree.Add(nonTreeNode);
                    queue.AddMany(graph[nonTreeNode]);
                }
                else
                {
                    break;
                }
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (spanningTree.Contains(first) &&
                    !spanningTree.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (spanningTree.Contains(second) &&
                !spanningTree.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        private static void ReadGraph(int e)
        {
            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .ToArray();

                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);

                if (!graph.ContainsKey(first))
                {
                    graph.Add(first, new List<Edge>());
                }

                if (!graph.ContainsKey(second))
                {
                    graph.Add(second, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                graph[first].Add(edge);
                graph[second].Add(edge);

                if (edgeData.Length > 3)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }
            }
        }
    }
}
