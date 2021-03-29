namespace _02_Max_Flow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MaxFlow
    {
        private static int[][] graph;
        private static int[] parent;

        public static int FindMaxFlow(int[][] targetGraph, int start, int end)
        {
            graph = targetGraph;
            parent = Enumerable.Repeat(-1, graph.Length).ToArray();

            var maxFlow = 0;

            while (BFS(start, end))
            {
                // find max Flow
                var pathFlow = int.MaxValue;

                var currentNode = end;

                while (currentNode != start)
                {
                    var prevNode = parent[currentNode];
                    
                    //Modify pathflow to hold the smallest edge capacity in the path
                    var currentFlow = graph[prevNode][currentNode];

                    if (currentFlow > 0 && currentFlow < pathFlow)
                    {
                        pathFlow = currentFlow;
                    }

                    currentNode = prevNode;
                }

                maxFlow += pathFlow;

                // Reconstruct Path
                currentNode = end;

                while (currentNode != start)
                {
                    var prevNode = parent[currentNode];

                    graph[prevNode][currentNode] -= pathFlow;
                    //Add the path flow amount to reverse edge
                    graph[currentNode][prevNode] += pathFlow;

                    currentNode = prevNode;
                }
            }

            return maxFlow;
        }

        private static bool BFS(int start, int end)
        {
            var visited = new bool[graph.Length];

            //Fill the parents array during BFS
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (int child = 0; child < graph[node].Length; child++)
                {
                    if (graph[node][child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);
                        parent[child] = node;
                        visited[child] = true;
                    }
                }
            }

            //return true if there is a path to the end
            return visited[end];
        }
    }
}
