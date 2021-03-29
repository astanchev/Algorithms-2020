namespace _02_Maximum_Tasks_Assignment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MaximumTasksAssignment
    {
        private static int[][] graph;
        private static int[] parents;

        public static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int tasks = int.Parse(Console.ReadLine());

            graph = ReadInput(people, tasks);
            parents = Enumerable.Repeat(-1, graph.Length).ToArray();

            
            
        }

        private static bool BFS(int start, int end)
        {
            var visited = new bool[graph.Length];

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
                        parents[child] = node;
                        visited[child] = true;
                    }
                }
            }

            return visited[end];
        }

        private static int[][] ReadInput(int people, int tasks)
        {
            int nodes = people + tasks + 2;

            //n = 3 columns: S(start) A B C 1 2 3 E(end)
            var result = new int[nodes][];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new int[nodes];
            }

            for (int i = 1; i <= people; i++)
            {
                result[0][i] = 1;
            }

            
            for (int i = 1; i <= tasks; i++)
            {
                result[i + people][result.Length - 1] = 1;
            }

            for (int person = 0; person < people; person++)
            {
                var line = Console.ReadLine();

                for (int task = 0; task < tasks; task++)
                {
                    if (line[task] == 'Y')
                    {
                        result[person + 1][task + people + 1] = 1;
                    }
                }
            }

            return result;
        }
    }
}
