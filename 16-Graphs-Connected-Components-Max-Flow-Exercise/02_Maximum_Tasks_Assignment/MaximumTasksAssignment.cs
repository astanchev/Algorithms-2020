namespace _02_Maximum_Tasks_Assignment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MaximumTasksAssignment
    {
        private static int[][] graph;
        private static int[] parents;
        private static SortedSet<string> result;

        public static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int tasks = int.Parse(Console.ReadLine());

            graph = ReadInput(people, tasks);

            parents = Enumerable.Repeat(-1, graph.Length).ToArray();

            var (start, end) = (0, graph.Length - 1);

            FindMaxTasks(start, end);

            //ExtractResultBFS(start, end, people);

            //Extract result with traversal of а matrix
            ExtractResult(people, tasks);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void ExtractResult(int people, int tasks)
        {
            result = new SortedSet<string>();

            for (int person = 1; person <= people; person++)
            {
                for (int task = people + 1; task <= people + tasks; task++)
                {
                    if (graph[task][person] > 0)
                    {
                        result.Add($"{(char)(person - 1 + 'A')}-{task - people}");
                    }
                }
            }
        }

        private static void ExtractResultBFS(int start, int end, int people)
        {
            var queue = new Queue<int>();
            result = new SortedSet<string>();
            var visited = new bool[graph.Length];

            queue.Enqueue(end);
            visited[end] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (int child = 0; child < graph[node].Length; child++)
                {
                    if (graph[node][child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;

                        if (node != end && node != start &&
                            child != end && child != start)
                        {
                            result.Add($"{(char)(child - 1 + 'A')}-{node - people}");
                        }
                    }
                }
            }
        }

        private static void FindMaxTasks(int start, int end)
        {
            while (BFS(start, end))
            {
                var currentNode = end;

                while (currentNode != start)
                {
                    var prevNode = parents[currentNode];

                    graph[prevNode][currentNode] = 0;
                    graph[currentNode][prevNode] = 1;

                    currentNode = prevNode;
                }
            }
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

                if (node == end)
                {
                    return true;
                }

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
