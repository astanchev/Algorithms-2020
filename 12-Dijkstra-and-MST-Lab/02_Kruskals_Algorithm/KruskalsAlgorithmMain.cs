namespace _02_Kruskals_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KruskalsAlgorithmMain
    {
        public static void Main(string[] args)
        {
            int numberOfEdges = int.Parse(Console.ReadLine());
            var graphEdges = ReadEdges(numberOfEdges);

            var vertices = graphEdges
                               .Select(e => e.StartNode)
                               .Union(graphEdges.Select(e => e.EndNode))
                               .Distinct()
                               .ToList();

            var minimumSpanningForest = KruskalAlgorithm.Kruskal(vertices.Count, graphEdges);

            Console.WriteLine(string.Join(Environment.NewLine, minimumSpanningForest));
        }

        private static List<Edge> ReadEdges(int numberOfEdges)
        {
            var list = new List<Edge>();

            for (int i = 0; i < numberOfEdges; i++)
            {
                var parts = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var edge = new Edge(parts[0], parts[1], parts[2]);
                list.Add(edge);
            }

            return list;
        }
    }
}
