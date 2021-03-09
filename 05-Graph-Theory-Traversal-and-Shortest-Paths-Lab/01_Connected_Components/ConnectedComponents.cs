namespace _01_Connected_Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ConnectedComponents
    {
        private static List<int>[] graph;
        private static bool[] visited;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            graph = new List<int>[n];
            visited = new bool[n];

            FillData(n);

            for (int node = 0; node < graph.Length; node++)
            {
                if (visited[node])
                {
                    continue;
                }

                List<int> component = new List<int>();

                DFS(node, component);

                Console.WriteLine($"Connected component: {string.Join(" ", component)}");
            }
        }

        private static void DFS(int node, List<int> component)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, component);
            }

            component.Add(node);
        }

        private static void FillData(int n)
        {
            for (int node = 0; node < n; node++)
            {
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    graph[node] = new List<int>();
                }
                else
                {
                    List<int> children = line.Split().Select(int.Parse).ToList();

                    graph[node] = children;
                }
            }
        }
    }
}
