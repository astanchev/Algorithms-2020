namespace _01_Super_Set
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SuperSet
    {
        private static int[] elements;
        private static List<string> result;

        static void Main(string[] args)
        {
            elements = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            result = new List<string>();

            GenerateSets();

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void GenerateSets()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                int[] combinations = new int[i+1];

                Combinations(0, 0, combinations);
            }
        }

        private static void Combinations(int combIdx, int elementStartIdx, int[] combinations)
        {
            if (combIdx >= combinations.Length)
            {
                result.Add(string.Join(" ", combinations));
                return;
            }

            for (int i = elementStartIdx; i < elements.Length; i++)
            {
                combinations[combIdx] = elements[i];
                Combinations(combIdx + 1, i + 1, combinations);
            }
        }
    }
}
