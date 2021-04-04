namespace _08_Greatest_Strategy
{
    using System;
    using System.Collections.Generic;

    public class GreatestStrategy
    {
        static Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();
        //copy of the graph in which we remove connections
        static Dictionary<int, HashSet<int>> disconnected = new Dictionary<int, HashSet<int>>();

        public static void Main()
        {
            var args = Console.ReadLine().Split();

            var nodes = int.Parse(args[0]);
            var connections = int.Parse(args[1]);
            var root = int.Parse(args[2]);

            FillGraphs(nodes, connections);

            Dfs(root, root, new HashSet<int>());

            var max = FindMax();

            Console.WriteLine(max);
        }

        private static int FindMax()
        {
            var visited = new HashSet<int>();
            var max = 0;

            foreach (var node in disconnected.Keys)
            {
                if (!visited.Contains(node))
                {
                    var value = GetValue(node, visited);

                    if (value > max)
                    {
                        max = value;
                    }
                }
            }

            return max;
        }

        private static void FillGraphs(int nodes, int connections)
        {
            for (int i = 1; i <= nodes; i++)
            {
                graph[i] = new HashSet<int>();
                disconnected[i] = new HashSet<int>();
            }

            for (int i = 0; i < connections; i++)
            {
                var args = Console.ReadLine().Split();
                var from = int.Parse(args[0]);
                var to = int.Parse(args[1]);

                graph[from].Add(to);
                graph[to].Add(from);

                disconnected[from].Add(to);
                disconnected[to].Add(from);
            }
        }

        static int GetValue(int node, HashSet<int> visited)
        {
            visited.Add(node);
            var value = node;

            foreach (var child in disconnected[node])
            {
                if (!visited.Contains(child))
                {
                    value += GetValue(child, visited);
                }
            }

            return value;
        }

        static int Dfs(int node, int parent, HashSet<int> visited)
        {
            visited.Add(node);

            //if there are no children
            var totalNodes = 1;

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child) && child != parent)
                {
                    var subtreeNodes = Dfs(child, node, visited);

                    // if subtree nodes are even we disconnect the subtree
                    if (subtreeNodes % 2 == 0) 
                    {
                        disconnected[node].Remove(child);
                        disconnected[child].Remove(node);
                    }

                    totalNodes += subtreeNodes;
                }
            }

            return totalNodes;
        }
    }
}
