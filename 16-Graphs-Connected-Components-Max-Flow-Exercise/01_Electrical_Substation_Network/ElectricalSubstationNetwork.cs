namespace _01_Electrical_Substation_Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ElectricalSubstationNetwork
    {
        private static List<List<int>> stronglyConnectedComponents;
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static bool[] visited;
        private static Stack<int> stack = new Stack<int>();

        public static void Main(string[] args)
        {
            graph = ReadInput();
            visited = new bool[graph.Length];
            stronglyConnectedComponents = new List<List<int>>();

            BuildReverseGraph();

            //Traverse the graph with DFS and push all nodes in the stack
            //in post-order (on return from the recursion)
            for (int node = 0; node < graph.Length; node++)
            {
                Dfs(node);
            }

            //Traverse the nodes from the stack and perform reverse DFS
            //to find strongly connected components
            FindStronglyConnected();

            foreach (var component in stronglyConnectedComponents)
            {
                component.Reverse();
                Console.WriteLine($"{string.Join(", ", component)}");
            }
        }

        private static void FindStronglyConnected()
        {
            visited = new bool[reversedGraph.Length];

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (!visited[node])
                {
                    stronglyConnectedComponents.Add(new List<int>());
                    ReverseDfs(node);
                }
            }
        }

        private static void BuildReverseGraph()
        {
            reversedGraph = new List<int>[graph.Length];

            for (int node = 0; node < reversedGraph.Length; node++)
            {
                reversedGraph[node] = new List<int>();
            }

            for (int node = 0; node < graph.Length; node++)
            {
                foreach (var child in graph[node])
                {
                    //add the reverse edge
                    reversedGraph[child].Add(node);
                }
            }
        }

        private static void ReverseDfs(int node)
        {
            if (visited[node])
            {
                return;
            }

            //Mark node as visites
            visited[node] = true;

            foreach (var child in reversedGraph[node])
            {
                if (!visited[child])
                {
                    ReverseDfs(child);
                }
            }

            //Add current node to the last list of strongly connected components
            stronglyConnectedComponents.Last().Add(node);
        }

        private static void Dfs(int node)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    Dfs(child);
                }
            }

            //add node to stack
            stack.Push(node);
        }

        private static List<int>[] ReadInput()
        {
            var nodes = int.Parse(Console.ReadLine());
            var lines = int.Parse(Console.ReadLine());

            var result = new List<int>[nodes];
            for (int node = 0; node < nodes; node++)
            {
                result[node] = new List<int>();
            }

            for (int i = 0; i < lines; i++)
            {
                int[] lineParts = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                result[lineParts[0]].AddRange(lineParts.Skip(1));
            }

            return result;
        }
    }
}
