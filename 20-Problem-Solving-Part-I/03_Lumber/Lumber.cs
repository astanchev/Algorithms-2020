namespace _03_Lumber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lumber
    {
        private static int count;
        private static List<Log> logs;
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] countConnected;
        private static List<string> result;

        public static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var logsCount = input[0];
            var queriesCount = input[1];

            logs = new List<Log>();
            graph = new List<int>[logsCount + 1];
            result = new List<string>();

            CreateLogGraph(logsCount);

            visited = new bool[logsCount + 1];
            countConnected = new int[logsCount + 1];

            for (var logIndex = 1; logIndex <= logsCount; logIndex++)
            {
                if (visited[logIndex])
                {
                    continue;
                }

                DFS(logIndex);
                count++;
            }

            CheckQueries(queriesCount);

            Console.Write(string.Join(Environment.NewLine, result));
        }

        private static void CheckQueries(int queriesCount)
        {
            for (var i = 0; i < queriesCount; i++)
            {
                var query = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                result.Add(countConnected[query[0]] == countConnected[query[1]] ? "YES" : "NO");
            }
        }

        private static void CreateLogGraph(int logsCount)
        {
            for (var i = 1; i <= logsCount; i++)
            {
                var currentLog = new Log(Console.ReadLine(), i);

                graph[i] = new List<int>();

                foreach (var log in logs)
                {
                    if (!log.Overlap(currentLog))
                    {
                        continue;
                    }

                    graph[log.Index].Add(i);
                    graph[i].Add(log.Index);
                }

                logs.Add(currentLog);
            }
        }

        private static void DFS(int log)
        {
            visited[log] = true;
            countConnected[log] = count;

            foreach (var child in graph[log])
            {
                if (!visited[child])
                {
                    DFS(child);
                }
            }
        }
    }
}
