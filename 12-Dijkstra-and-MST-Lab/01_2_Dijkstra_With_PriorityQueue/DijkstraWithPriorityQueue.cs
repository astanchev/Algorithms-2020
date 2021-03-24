namespace _01_2_Dijkstra_With_PriorityQueue
{
    using System.Collections.Generic;
    using System.Linq;

    public class DijkstraWithPriorityQueue
    {
        private static int?[] previous;
        private static bool[] visited;

        public static List<int> DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode)
        {
            //Initialize the previous[] and visited[] arrays
            previous = new int?[graph.Count];
            visited = new bool[graph.Count];
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();

            //Set DistanceFromStart to Infinity
            foreach (var pair in graph)
            {
                pair.Key.DistanceFromStart = double.PositiveInfinity;
            }

            //set the distance to the source node to 0 and Enqueue it in the priority queue
            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);

            //Find Nearest Unvisited Node from the source
            FindNearestUnvisited(graph, priorityQueue, destinationNode);

            //No path found from sourceNode to destinationNode
            if (double.IsInfinity(destinationNode.DistanceFromStart))
            {
                return null;
            }

            //Reconstruct Shortest Path
            List<int> path = FindPath(destinationNode);

            return path;
        }

        //Reconstruct the shortest path from sourceNode to destinationNode
        private static List<int> FindPath(Node destinationNode)
        {
            Stack<int> path = new Stack<int>();
            int? current = destinationNode.Id;

            while (current != null)
            {
                path.Push(current.Value);
                current = previous[current.Value];
            }

            return path.ToList();
        }

        private static void FindNearestUnvisited(Dictionary<Node, Dictionary<Node, int>> graph, PriorityQueue<Node> priorityQueue, Node destinationNode)
        {
            while (priorityQueue.Count > 0)
            {
                //Get the smallest element in the priority queue
                var currentNode = priorityQueue.ExtractMin();

                if (currentNode == destinationNode)
                {
                    //break if the destinationNode is found
                    break;
                }

                //check all of neighboring nodes
                foreach (var edge in graph[currentNode])
                {
                    //if we find an unvisited node we add it to the priority queue and mark it as visited
                    if (!visited[edge.Key.Id])
                    {
                        priorityQueue.Enqueue(edge.Key);
                        visited[edge.Key.Id] = true;
                    }

                    //Improve Shortest Distances
                    double distance = currentNode.DistanceFromStart + edge.Value;

                    //if we have found a better distance to neighboring node                     
                    if (distance < edge.Key.DistanceFromStart)
                    {
                        //we change its distance
                        edge.Key.DistanceFromStart = distance;
                        //set the current node as its previous
                        previous[edge.Key.Id] = currentNode.Id;
                        //call the DecreaseKey method
                        priorityQueue.DecreaseKey(edge.Key);
                    }
                }
            }
        }
    }
}
