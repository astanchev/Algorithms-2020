namespace _02_Paths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Paths
    {
        private static Dictionary<int, List<int>> graph;
        private static List<string> paths;

        static void Main(string[] args)
        {
            int numberNodes = int.Parse(Console.ReadLine());
            graph = new Dictionary<int, List<int>>();
            paths = new List<string>();

            ReadGraph(numberNodes);

            GeneratePaths(numberNodes);

            Console.WriteLine(string.Join(Environment.NewLine, paths));
        }

        private static void GeneratePaths(int numberNodes)
        {
            for (int i = 0; i < numberNodes - 1; i++)
            {
                var path = new List<int>();
                AddPaths(i, path);
            }
        }

        private static void AddPaths(int start, List<int> path)
        {
            if (path.Contains(start))
            {
                return;
            }

            path.Add(start);

            foreach (var child in graph[start])
            {
                AddPaths(child, path);

                if (graph[child].Count == 0)
                {
                    paths.Add(string.Join(" ", path));
                }
                
                var last = path.Last();
                path.Remove(last);
            }

        }

        private static void ReadGraph(int numberNodes)
        {
            for (int i = 0; i < numberNodes; i++)
            {
                graph[i] = new List<int>();
                var childrenLine = Console.ReadLine();

                if (!string.IsNullOrEmpty(childrenLine))
                {
                    var children = childrenLine
                                        .Split(" ")
                                        .Select(int.Parse);

                    foreach (var child in children)
                    {
                        graph[i].Add(child);
                    }
                }
            }
        }
    }
}
