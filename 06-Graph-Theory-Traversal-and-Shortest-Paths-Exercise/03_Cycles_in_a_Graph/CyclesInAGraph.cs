namespace _03_Cycles_in_a_Graph
{
    using System;
    using System.Collections.Generic;

    class CyclesInAGraph
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;
        private static bool isCycled;

        static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();

            isCycled = false;

            ReadGraph();

            foreach (var node in graph.Keys)
            {
                DFS(node);

                if (isCycled)
                {
                    break;
                }
            }

            if (isCycled)
            {
                Console.WriteLine("Acyclic: No");
            }
            else
            {
                Console.WriteLine("Acyclic: Yes");
            }
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

                var parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0];
                var child = parts[1];

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                if (!graph.ContainsKey(child))
                {
                    graph[child] = new List<string>();
                }

                graph[node].Add(child);
            }
        }

        private static void DFS(string node)
        {
            if (cycles.Contains(node) || isCycled == true)
            {
                isCycled = true;
                return;
            }

            if (visited.Contains(node))
            {
                return;
            }

            cycles.Add(node);
            visited.Add(node);

            foreach (var child in graph[node])
            {
                DFS(child);
            }

            cycles.Remove(node);
        }
    }
}
