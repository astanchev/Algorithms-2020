namespace _06_Shortest_Path
{
    using System;
    using System.Collections.Generic;

    public class ShortestPath
    {
        private static  List<string> paths = new List<string>();
        private static  List<int> unknownIndexes = new List<int>();
        private static  char[] Directions = { 'L', 'R', 'S' };

        public static void Main(string[] args)
        {
            var directions = Console.ReadLine()
                .ToCharArray();

            for (var i = 0; i < directions.Length; i++)
            {
                if (directions[i].Equals('*'))
                {
                    unknownIndexes.Add(i);
                }
            }

            GetPossiblePaths(directions, 0);

            Console.WriteLine(paths.Count);

            Console.WriteLine(string.Join(Environment.NewLine, paths));
        }

        private static void GetPossiblePaths(char[] directions, int unknownIndex)
        {
            if (unknownIndex == unknownIndexes.Count)
            {
                paths.Add(string.Concat(directions));
                return;
            }

            var index = unknownIndexes[unknownIndex];

            foreach (var direction in Directions)
            {
                directions[index] = direction;
                GetPossiblePaths(directions, unknownIndex + 1);
            }
        }
    }
}
