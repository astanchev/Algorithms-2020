namespace _05_Break_Cycles
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Edge
    {
        public Edge(string from, string to)
        {
            this.From = from;
            this.To = to;
        }

        public string From { get; set; }

        public string To { get; set; }
    }

    public class EdgeComparer : IEqualityComparer<Edge>
    {
        public bool Equals(Edge x, Edge y)
        {
            if (x.To.Equals(y.From) && x.From.Equals(y.To))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Edge obj)
        {
            return obj.From.GetHashCode() + obj.To.GetHashCode();
        }
    }

    public class BreakCycles
    {
        private static Dictionary<string, List<string>> graph;
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var removedEdges = new HashSet<Edge>(new EdgeComparer());
            graph = new Dictionary<string, List<string>>();
            edges = new List<Edge>();


            ReadInput(n);

            edges = edges
                .OrderBy(e => e.From)
                .ThenBy(e => e.To)
                .ToList();

            FindEdgesToRemove(removedEdges);

            PrintResult(removedEdges);
        }

        private static void PrintResult(HashSet<Edge> removedEdges)
        {
            Console.WriteLine($"Edges to remove: {removedEdges.Count}");
            foreach (var edge in removedEdges)
            {
                Console.WriteLine($"{edge.From} - {edge.To}");
            }
        }

        private static void FindEdgesToRemove(HashSet<Edge> removedEdges)
        {
            foreach (var edge in edges)
            {
                var from = edge.From;
                var to = edge.To;

                graph[from].Remove(to);
                graph[to].Remove(from);

                if (HasPath(from, to))
                {
                    removedEdges.Add(edge);
                }
                else
                {
                    graph[from].Add(to);
                    graph[to].Add(from);
                }
            }
        }

        private static bool HasPath(string from, string to)
        {
            var queue = new Queue<string>();
            queue.Enqueue(from);

            var visited = new HashSet<string> { from };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == to)
                {
                    return true;
                }

                foreach (var child in graph[node])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    visited.Add(child);
                    queue.Enqueue(child);
                }
            }

            return false;
        }

        private static void ReadInput(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine().Split(" -> ");
                var node = line[0];
                var children = line[1].Split(" ");

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                foreach (var child in children)
                {
                    graph[node].Add(child);
                    edges.Add(new Edge(node, child));
                }
            }
        }
    }
}
