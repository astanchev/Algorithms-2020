namespace _01_Strongly_Connected_Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StronglyConnectedComponents
    {
        private static List<List<int>> stronglyConnectedComponents;
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static bool[] visited;
        private static Stack<int> stack = new Stack<int>();

        public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
        {
            graph = targetGraph;
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

            return stronglyConnectedComponents;
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
    }
}
