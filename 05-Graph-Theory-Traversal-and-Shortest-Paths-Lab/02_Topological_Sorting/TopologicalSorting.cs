namespace _02_Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
        
    class TopologicalSorting
    {
        private static Dictionary<string, List<string>> graph;
        private static Dictionary<string, int> predecessorCount;
        private static List<string> sorted;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;

        static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();
            predecessorCount = new Dictionary<string, int>();
            sorted = new List<string>();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();

            var lines = int.Parse(Console.ReadLine());

            ReadGraph(lines);

            // Source Removal Algorithm
            GetPredecessorCount();
            TopologicalSort();
            PrintResult();

            //DFS Algorithm - 66/100 for judge
            //PrintTopSortResult();
        }        

        private static void ReadGraph(int lines)
        {
            for (int i = 0; i < lines; i++)
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
        
        // Source Removal Algorithm
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

        // Source Removal Algorithm
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

        // Source Removal Algorithm
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

        //DFS Algorithm
        private static ICollection<string> TopSort()
        {
            var sortedDFS = new LinkedList<string>();

            foreach (var node in graph.Keys)
            {
                TopSortDFS(node, sortedDFS);
            }

            return sortedDFS;
        }

        //DFS Algorithm
        private static void TopSortDFS(string node, LinkedList<string> sortedDFS)
        {
            if (cycles.Contains(node))
            {
                throw new InvalidOperationException();
            }

            if (visited.Contains(node))
            {
                return;
            }

            cycles.Add(node);
            visited.Add(node);

            foreach (var child in graph[node])
            {
                TopSortDFS(child, sortedDFS);
            }

            cycles.Remove(node);
            sortedDFS.AddFirst(node);
        }

        //DFS Algorithm
        private static void PrintTopSortResult()
        {
            try
            {
                var result = TopSort();

                Console.WriteLine($"Topological sorting: {string.Join(", ", result)}");
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine("Invalid topological sorting");
            }
        }
    }
}
