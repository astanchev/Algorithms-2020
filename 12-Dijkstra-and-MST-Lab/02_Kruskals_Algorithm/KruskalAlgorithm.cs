namespace _02_Kruskals_Algorithm
{
    using System.Collections.Generic;

    public class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            // Initialize parents
            var parent = new int[numberOfVertices + 1];

            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

            // Kruskal's algorithm
            var spanningTree = new List<Edge>();
            edges.Sort();

            foreach (var edge in edges)
            {
                int rootStartNode = FindRoot(edge.StartNode, parent);
                int rootEndNode = FindRoot(edge.EndNode, parent);

                if (rootStartNode != rootEndNode) // No cycle
                {
                    spanningTree.Add(edge);
                    parent[rootStartNode] = rootEndNode;
                }
            }

            return spanningTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            int root = node;

            while (parent[root] != root)
            {
                root = parent[root];
            }

            // Optimize the path to root
            // Attach each path node directly to the root
            while (node != root)
            {
                var oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }
    }
}
