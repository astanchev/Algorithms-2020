namespace _01_Dijkstras_Algorithm
{
    using System.Collections.Generic;
    using System.Linq;

    // Dijkstra's Algorithm with Adjacency Matrix
    public class DijkstraWithoutQueue
    {
        private static bool[] used;
        private static int?[] previous;
        private static int[] distance;

        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            int graphLength = graph.GetLength(0);

            //Initialize the distance
            distance = new int[graphLength];
            for (int i = 0; i < graphLength; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[sourceNode] = 0;

            //Initialize the previous[] and used[] arrays
            used = new bool[graphLength];
            previous = new int?[graphLength];

            //Find Nearest Unvisited Node from the source
            FindNearestUnvisited(graph, graphLength);

            if (distance[destinationNode] == int.MaxValue)
            {
                //No path found from sourceNode to destinationNode
                return null;
            }

            //Reconstruct Shortest Path
            List<int> path = FindPath(destinationNode, previous);

            return path;
        }

        private static void FindNearestUnvisited(int[,] graph, int graphLength)
        {
            while (true)
            {
                int minDistance = int.MaxValue;
                int minNode = 0;

                for (int node = 0; node < graphLength; node++)
                {
                    if (!used[node] && distance[node] < minDistance)
                    {
                        minDistance = distance[node];
                        minNode = node;
                    }
                }

                if (minDistance == int.MaxValue)
                {
                    //No min distance node found --> algorithm finished
                    break;
                }

                used[minNode] = true;

                //Improve the distance[0...n-1] through minNode
                for (int i = 0; i < graphLength; i++)
                {
                    if (graph[minNode, i] > 0) //node i is conected to minNode
                    {
                        //Calculate new distance to i (shortest distance to minNode + distance between minNode and i)
                        int newDistance = minDistance + graph[minNode, i];

                        //Check if new distance is shorter than current shortest to i
                        if (newDistance < distance[i])
                        {
                            //Update shortest distance and previous(if necessary)
                            distance[i] = newDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }
        }

        //Reconstruct the shortest path from sourceNode to destinationNode
        private static List<int> FindPath(int destinationNode, int?[] previous)
        {
            var path = new Stack<int>();
            int? currentNode = destinationNode;

            while (currentNode != null)
            {
                path.Push(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            return path.ToList();
        }
    }
}
