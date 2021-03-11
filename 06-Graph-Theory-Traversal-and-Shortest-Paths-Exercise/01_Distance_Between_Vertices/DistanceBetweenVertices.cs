namespace _01_Distance_Between_Vertices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class DistanceBetweenVertices
    {
        private static Dictionary<int, List<int>> graph;
        private static HashSet<int> visited;
        private static Dictionary<int, int> parents;

        static void Main(string[] args)
        {
            int numberNodes = int.Parse(Console.ReadLine());
            int pairs = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>();

            ReadGraph(numberNodes);

            for (int i = 0; i < pairs; i++)
            {
                visited = new HashSet<int>();
                parents = new Dictionary<int, int>();

                int[] pair = Console.ReadLine()
                    .Split("-")
                    .Select(int.Parse)
                    .ToArray();

                int startNode = pair[0];
                int destinationNode = pair[1];

                int path = BFS(startNode, destinationNode);
                Console.WriteLine($"{{{startNode}, {destinationNode}}} -> {path}");

                //int steps = GetSteps(startNode, destinationNode);
                //Console.WriteLine($"{{{startNode}, {destinationNode}}} -> {steps}");
            }
        }

        private static int BFS(int startNode, int destinationNode)
        {
            if (visited.Contains(startNode))
            {
                return -1;
            }

            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            visited.Add(startNode);
            parents[startNode] = -1;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();


                if (node == destinationNode)
                {
                    var path = ReconstructPath(destinationNode);

                    return path.Count - 1;
                }

                foreach (var child in graph[node])
                {
                    if (!visited.Contains(child))
                    {
                        parents[child] = node;
                        queue.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }

            return -1;
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

        private static void ReadGraph(int numberNodes)
        {
            for (int i = 0; i < numberNodes; i++)
            {
                var edge = Console.ReadLine().Split(":");
                var from = int.Parse(edge[0]);

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<int>();
                }

                if (!string.IsNullOrEmpty(edge[1]))
                {
                    var children = edge[1].Split(" ").Select(int.Parse);

                    foreach (var child in children)
                    {
                        graph[from].Add(child);
                    }
                }
            }
        }
       
        //Second solution
        private static int GetSteps(int startNode, int destinationNode)
        {
            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            var stepsToNode = new Dictionary<int, int> { { startNode, 0 } };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destinationNode)
                {
                    return stepsToNode[node];
                }

                foreach (var child in graph[node])
                {
                    if (!stepsToNode.ContainsKey(child))
                    {
                        queue.Enqueue(child);
                        stepsToNode[child] = stepsToNode[node] + 1;
                    }
                }
            }

            return -1;
        }
    }
}
