namespace _03_Path_Finder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class PathFinder
    {
        private static Dictionary<int, List<int>> graph;

        static void Main(string[] args)
        {
            int numberNodes = int.Parse(Console.ReadLine());
            graph = new Dictionary<int, List<int>>();

            ReadGraph(numberNodes);

            int numberPaths = int.Parse(Console.ReadLine());

            ReadPaths(numberPaths);
        }

        private static void ReadPaths(int numberPaths)
        {
            for (int i = 0; i < numberPaths; i++)
            {
                var path = Console.ReadLine()
                                        .Split(" ")
                                        .Select(int.Parse)
                                        .ToArray();

                PrintIfIsPath(path);
            }
        }

        private static void PrintIfIsPath(int[] path)
        {
            int parent = path[0];
            int step = 1;
            bool isPath = IsPath(path, parent, step);

            if (isPath)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }
        }

        private static bool IsPath(int[] path, int parent, int step)
        {
            if (step >= path.Length)
            {
                return true;
            }

            int child = path[step];

            if (!graph[parent].Contains(child))
            {
                return false;
            }

            return IsPath(path, child, step + 1);
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
