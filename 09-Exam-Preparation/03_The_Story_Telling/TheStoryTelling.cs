namespace _03_The_Story_Telling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class TheStoryTelling
    {
        private static Dictionary<string, List<string>> graph;
        
        private static HashSet<string> visited;
        private static HashSet<string> cycles;

        static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();            
            visited = new HashSet<string>();
            cycles = new HashSet<string>();

            ReadGraph();

            PrintTopSortResult();
        }

        private static void ReadGraph()
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "End")
                {
                    return;
                }

                var parts = line.Split("->", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0].TrimEnd();

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                if (parts.Length > 1)
                {
                    graph[node] = parts[1]
                        .TrimStart()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Where(e => e != "")
                        .ToList();
                }
            }
        }

        private static List<string> TopSort()
        {
            var sortedDFS = new Stack<string>();

            foreach (var node in graph.Keys)
            {
                TopSortDFS(node, sortedDFS);
            }

            return sortedDFS.ToList();
        }

        private static void TopSortDFS(string node, Stack<string> sortedDFS)
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
            sortedDFS.Push(node);
        }

        private static void PrintTopSortResult()
        {
            try
            {
                var result = TopSort();

                Console.WriteLine($"{string.Join(" ", result)}");
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine("Invalid sorting");
            }
        }
    }
}
