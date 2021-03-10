namespace _03_Shortest_Path
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ShortestPath
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] parents;


        static void Main(string[] args)
        {
            int numberNodes = int.Parse(Console.ReadLine());
            int numberEdges = int.Parse(Console.ReadLine());

            graph = new List<int>[numberNodes + 1];
            visited = new bool[graph.Length];
            parents = new int[graph.Length];
            Array.Fill(parents, -1);

            ReadGraph(numberEdges);

            int startNode = int.Parse(Console.ReadLine());
            int destinationNode = int.Parse(Console.ReadLine());

            BFS(startNode, destinationNode);
        }

        private static void BFS(int startNode, int destinationNode)
        {
            if (visited[startNode])
            {
                return;
            }

            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            visited[startNode] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destinationNode)
                {
                    var path = ReconstructPath(destinationNode);

                    PrintPath(path);

                    return;
                }

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        parents[child] = node;
                        queue.Enqueue(child);
                        visited[child] = true;
                    }
                }
            }
        }

        private static void PrintPath(Stack<int> path)
        {
            Console.WriteLine($"Shortest path length is: {path.Count - 1}");
            Console.WriteLine($"{string.Join(" ", path)}");
        }

        private static Stack<int> ReconstructPath(int destinationNode)
        {
            var path = new Stack<int>();
            var index = destinationNode;

            while (index != -1)
            {
                path.Push(index);
                index = parents[index];
            }

            return path;
        }

        private static void ReadGraph(int numberEdges)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }
            
            for (int i = 0; i < numberEdges; i++)
            {
                int[] edge = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edge[0];
                var to = edge[1];

                graph[from].Add(to);
            }
        }
    }
}
