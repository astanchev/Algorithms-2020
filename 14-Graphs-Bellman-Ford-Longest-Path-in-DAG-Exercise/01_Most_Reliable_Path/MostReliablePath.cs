namespace _01_Most_Reliable_Path
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wintellect.PowerCollections;

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class MostReliablePath
    {
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static double[] percentages;
        private static int[] prev;

        public static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            edgesByNode = ReadGraph(edges);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            PrepareCollections();

            percentages[start] = 100;

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => 
                (int)(percentages[s] - percentages[f])));

            queue.Add(start);

            queue = FillNearestNodePath(end, queue);

            PrintPath(end);
        }

        private static void PrintPath(int end)
        {
            if (percentages[end] == -1)
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                Console.WriteLine($"Most reliable path reliability: {percentages[end]:F2}%");

                Stack<int> path = RestorePath(end);

                Console.WriteLine(string.Join(" -> ", path));
            }
        }

        private static Stack<int> RestorePath(int end)
        {
            var path = new Stack<int>();

            var node = end;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static OrderedBag<int> FillNearestNodePath(int end, OrderedBag<int> queue)
        {
            while (queue.Count > 0)
            {
                var maxNode = queue.RemoveFirst();
                var children = edgesByNode[maxNode];

                if (maxNode == end || percentages[maxNode] == -1)
                {
                    break;
                }

                foreach (var child in children)
                {
                    var childNode = child.First == maxNode
                        ? child.Second
                        : child.First;

                    if (percentages[childNode] == -1)
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = child.Weight * percentages[maxNode] / 100;

                    if (newDistance > percentages[childNode])
                    {
                        percentages[childNode] = newDistance;

                        prev[childNode] = maxNode;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => 
                                (int)(percentages[s] - percentages[f])));
                    }
                }
            }

            return queue;
        }

        private static void PrepareCollections()
        {
            percentages = new double[edgesByNode.Count];
            prev = new int[edgesByNode.Count];

            percentages = Enumerable.Repeat<double>(-1, edgesByNode.Count).ToArray();
            prev = Enumerable.Repeat<int>(-1, edgesByNode.Count).ToArray();
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(firstNode))
                {
                    result.Add(firstNode, new List<Edge>());
                }

                if (!result.ContainsKey(secondNode))
                {
                    result.Add(secondNode, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }

            return result;
        }
    }
}
