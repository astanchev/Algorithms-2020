namespace _02_Max_Flow
{
    using System;
    using System.Linq;

    public class MaxFlowMain
    {
        public static void Main(string[] args)
        {
            //var graph = new int[][]
            //{
            //    new int[] { 0, 10, 10, 0, 0, 0 },
            //    new int[] { 0, 0, 2, 4, 8, 0},
            //    new int[] { 0, 0, 0, 0, 9, 0},
            //    new int[] { 0, 0, 0, 0, 0, 10 },
            //    new int[] { 0, 0, 0, 6, 0, 10 },
            //    new int[] { 0, 0, 0, 0, 0, 0 },
            //};

            int[][] graph = ReadInput();

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            var maxFlow = MaxFlow.FindMaxFlow(graph, start, end);
            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static int[][] ReadInput()
        {
            int nodes = int.Parse(Console.ReadLine());
            int[][] result = new int[nodes][];

            for (int i = 0; i < nodes; i++)
            {
                result[i] = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
            }

            return result;
        }
    }
}
