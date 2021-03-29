namespace _03_Articulation_Points
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticulationPointsMain
    {
        public static void Main(string[] args)
        {
            //var graph = new List<int>[]
            //{
            //    new List<int>() {1, 2, 6, 7, 9},      // children of node 0
            //    new List<int>() {0, 6},               // children of node 1
            //    new List<int>() {0, 7},               // children of node 2
            //    new List<int>() {4},                  // children of node 3
            //    new List<int>() {3, 6, 10},           // children of node 4
            //    new List<int>() {7},                  // children of node 5
            //    new List<int>() {0, 1, 4, 8, 10, 11}, // children of node 6
            //    new List<int>() {0, 2, 5, 9},         // children of node 7
            //    new List<int>() {6, 11},              // children of node 8
            //    new List<int>() {0, 7},               // children of node 9
            //    new List<int>() {4, 6},               // children of node 10
            //    new List<int>() {6, 8},               // children of node 11
            //};

            List<int>[] graph = ReadInput();

            var articulationPoints = ArticulationPoints.FindArticulationPoints(graph);
            Console.WriteLine("Articulation points: " +
                string.Join(", ", articulationPoints));
        }

        private static List<int>[] ReadInput()
        {
            var nodes = int.Parse(Console.ReadLine());
            var lines = int.Parse(Console.ReadLine());

            var result = new List<int>[nodes];
            for (int node = 0; node < nodes; node++)
            {
                result[node] = new List<int>();
            }

            for (int i = 0; i < lines; i++)
            {
                int[] lineParts = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                result[lineParts[0]].AddRange(lineParts.Skip(1));
            }

            return result;
        }
    }
}
