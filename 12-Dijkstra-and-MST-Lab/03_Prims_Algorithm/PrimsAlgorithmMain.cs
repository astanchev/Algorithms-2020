namespace _03_Prims_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wintellect.PowerCollections;

    // Wintellect OrderedBag<T> solution
    public class EdgeWintellect
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class PrimsAlgorithmMain
    {
        // Wintellect OrderedBag<T> solution
        private static Dictionary<int, List<EdgeWintellect>> edgesByNode;
        private static HashSet<int> forest;

        public static void Main(string[] args)
        {
            // Wintellect OrderedBag<T> solution

            var e = int.Parse(Console.ReadLine());

            edgesByNode = ReadGraph(e);
            forest = new HashSet<int>();

            foreach (var node in edgesByNode.Keys)
            {
                if (!forest.Contains(node))
                {
                    Prim(node);
                }
            }

            // Custom Priority queue solution

            //int numberOfEdges = int.Parse(Console.ReadLine());
            //var graphEdges = ReadEdges(numberOfEdges);

            //var minimumSpanningForest = PrimsAlgorithm.Prim(graphEdges);

            //Console.WriteLine(string.Join(Environment.NewLine, minimumSpanningForest));
        }

        // Wintellect OrderedBag<T> solution
        private static void Prim(int node)
        {
            forest.Add(node);

            var queue = new OrderedBag<EdgeWintellect>(
                edgesByNode[node],
                Comparer<EdgeWintellect>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");

                forest.Add(nonTreeNode);
                queue.AddMany(edgesByNode[nonTreeNode]);
            }
        }

        // Wintellect OrderedBag<T> solution
        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (forest.Contains(first) &&
                    !forest.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (forest.Contains(second) &&
                !forest.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        // Wintellect OrderedBag<T> solution
        private static Dictionary<int, List<EdgeWintellect>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<EdgeWintellect>>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(first))
                {
                    result.Add(first, new List<EdgeWintellect>());
                }

                if (!result.ContainsKey(second))
                {
                    result.Add(second, new List<EdgeWintellect>());
                }

                var edge = new EdgeWintellect
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }

        // Custom Priority queue solution
        //private static List<Edge> ReadEdges(int numberOfEdges)
        //{
        //    var list = new List<Edge>();

        //    for (int i = 0; i < numberOfEdges; i++)
        //    {
        //        var parts = Console.ReadLine()
        //            .Split(", ")
        //            .Select(int.Parse)
        //            .ToArray();

        //        var edge = new Edge(parts[0], parts[1], parts[2]);
        //        list.Add(edge);
        //    }

        //    return list;
        //}


    }
}
