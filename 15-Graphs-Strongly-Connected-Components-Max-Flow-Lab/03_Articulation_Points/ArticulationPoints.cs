namespace _03_Articulation_Points
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ArticulationPoints
    {
        private static List<int>[] graph;
        private static List<int> articulationPoints;

        private static bool[] visited;
        private static int[] depths;
        private static int[] lowpoint;
        private static int?[] parent;

        public static List<int> FindArticulationPoints(List<int>[] targetGraph)
        {
            graph = targetGraph;
            visited = new bool[targetGraph.Length];
            depths = new int[targetGraph.Length];
            lowpoint = new int[targetGraph.Length];
            parent = new int?[targetGraph.Length];
            articulationPoints = new List<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }

            return articulationPoints;
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowpoint[node] = depth;

            int countChildren = 0;
            bool isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;
                    FindArticulationPoints(child, depth + 1);
                    countChildren++;

                    if (lowpoint[child] >= depths[node])
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (child != parent[node])
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depths[child]);
                }
            }

            if ((parent[node] != null && isArticulationPoint) ||
                (parent[node] == null && countChildren > 1))
            {
                articulationPoints.Add(node);
            }
        }
    }
}
