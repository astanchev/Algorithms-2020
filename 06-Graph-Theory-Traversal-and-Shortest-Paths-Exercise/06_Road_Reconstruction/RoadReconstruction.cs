namespace _06_Road_Reconstruction
{
    using System;
    using System.Collections.Generic;
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

    public class RoadReconstruction
    {
        private static Dictionary<string, HashSet<string>> graph;
        private static List<Edge> edges;
        private static HashSet<Edge> importantEdges;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var e = int.Parse(Console.ReadLine());
            importantEdges = new HashSet<Edge>(new EdgeComparer());
            graph = new Dictionary<string, HashSet<string>>();
            edges = new List<Edge>();

            ReadInput(e);

            FindImportantEdges();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine($"Important streets:");

            foreach (var edge in importantEdges)
            {
                Console.WriteLine($"{edge.From} {edge.To}");
            }
        }

        private static void FindImportantEdges()
        {
            foreach (var edge in edges)
            {
                var from = edge.From;
                var to = edge.To;

                graph[from].Remove(to);
                graph[to].Remove(from);

                if (IsImportant(edge.From, edge.To))
                {
                    importantEdges.Add(edge);
                }

                graph[from].Add(to);
                graph[to].Add(from);
            }
        }

        private static bool IsImportant(string from, string to)
        {
            var queue = new Queue<string>();
            queue.Enqueue(from);

            var visited = new HashSet<string> { from };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == to)
                {
                    return false;
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

            return true;
        }

        private static void ReadInput(int e)
        {
            for (int i = 0; i < e; i++)
            {
                var line = Console.ReadLine().Split(" - ");
                var node = line[0];
                var child = line[1];

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new HashSet<string>();
                }

                if (!graph.ContainsKey(child))
                {
                    graph[child] = new HashSet<string>();
                }

                graph[node].Add(child);
                graph[child].Add(node);
                edges.Add(new Edge(node, child));
            }
        }
    }
}
