namespace _03_Prims_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrimsAlgorithm
    {
        public static HashSet<int> visited = new HashSet<int>();
        public static List<Edge> spanningTree = new List<Edge>();
        public static Dictionary<int, List<Edge>> vertexToEdges = new Dictionary<int, List<Edge>>();

        public static List<Edge> Prim(List<Edge> edges)
        {
            var vertices = edges
                               .Select(e => e.StartNode)
                               .Union(edges.Select(e => e.EndNode))
                               .Distinct()
                               .OrderBy(e => e)
                               .ToList();

            ExtractVertexToEdges(edges);

            foreach (var vertex in vertices)
            {
                if (!visited.Contains(vertex))
                {
                    PrimAlg(vertex);
                }
            }

            return spanningTree;
        }

        public static void PrimAlg(int startingVertex)
        {
            visited.Add(startingVertex);

            var priorityQueue = new PriorityQueue<int, Edge>();

            vertexToEdges[startingVertex].ForEach(e => priorityQueue.Enqueue(e.Weight, e));

            while (priorityQueue.Count != 0)
            {
                var minEdge = priorityQueue.DequeueValue();

                var nonTreeVertex = GetNonTreeVertex(minEdge.StartNode, minEdge.EndNode);

                if (nonTreeVertex == -1)
                {
                    continue;
                }

                visited.Add(nonTreeVertex);
                spanningTree.Add(minEdge);

                vertexToEdges[nonTreeVertex].ForEach(e => priorityQueue.Enqueue(e.Weight, e));
            }
        }

        private static void ExtractVertexToEdges(List<Edge> edges)
        {
            foreach (var edge in edges)
            {
                if (!vertexToEdges.ContainsKey(edge.StartNode))
                {
                    vertexToEdges[edge.StartNode] = new List<Edge>();
                }

                if (!vertexToEdges.ContainsKey(edge.EndNode))
                {
                    vertexToEdges[edge.EndNode] = new List<Edge>();
                }

                vertexToEdges[edge.StartNode].Add(edge);
                vertexToEdges[edge.EndNode].Add(edge);
            }
        }

        private static int GetNonTreeVertex(int first, int second)
        {
            var nonTreeNode = -1;

            if (visited.Contains(first) &&
                    !visited.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (visited.Contains(second) &&
                !visited.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }
    }
}
