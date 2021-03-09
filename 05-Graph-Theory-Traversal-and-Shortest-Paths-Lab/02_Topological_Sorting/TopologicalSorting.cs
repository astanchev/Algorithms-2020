namespace _02_Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Source Removal Algorithm
    class TopologicalSorting
    {
        private static Dictionary<string, List<string>> graph;
        private static Dictionary<string, int> predecessorCount;
        private static List<string> sorted;

        static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();
            predecessorCount = new Dictionary<string, int>();
            sorted = new List<string>();

            var n = int.Parse(Console.ReadLine());

            ReadGraph(n);

            GetPredecessorCount();

            TopologicalSort();

            PrintResult();
        }

        private static void PrintResult()
        {
            if (predecessorCount.Count > 0)
            {
                Console.WriteLine("Invalid topological sorting");
            }
            else
            {
                Console.WriteLine($"Topological sorting: {string.Join(", ", sorted)}");
            }
        }

        private static void ReadGraph(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var parts = Console.ReadLine().Split("->", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0].TrimEnd();

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                if (parts.Length > 1)
                {
                    graph[node] = parts[1]
                        .TrimStart()
                        .Split(", ")
                        .Where(e => e != "")
                        .ToList();
                }

            }
        }

        private static void GetPredecessorCount()
        {
            foreach (var node in graph)
            {
                var key = node.Key;
                var children = node.Value;

                if (!predecessorCount.ContainsKey(key) && !string.IsNullOrEmpty(key))
                {
                    predecessorCount[key] = 0;
                }

                foreach (var child in children)
                {
                    if (!predecessorCount.ContainsKey(child) && !string.IsNullOrEmpty(child))
                    {
                        predecessorCount[child] = 0;
                    }

                    predecessorCount[child]++;
                }

            }
        }

        private static void TopologicalSort()
        {
            while (predecessorCount.Count > 0)
            {
                var node = predecessorCount.FirstOrDefault(d => d.Value == 0);

                if (node.Key == null)
                {
                    break;
                }

                foreach (var child in graph[node.Key])
                {
                    predecessorCount[child]--;
                }

                sorted.Add(node.Key);
                predecessorCount.Remove(node.Key);
            }
        }
    }
}
