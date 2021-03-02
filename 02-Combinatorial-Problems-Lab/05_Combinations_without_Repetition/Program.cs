namespace _05_Combinations_without_Repetition
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static string[] elements;
        private static string[] combinations;
        private static List<string> result;
        private static int k;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());

            combinations = new string[k];
            result = new List<string>();

            Combinations(0, 0);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void Combinations(int combIdx, int elementStartIdx)
        {
            if (combIdx >= combinations.Length)
            {
                result.Add(string.Join(' ', combinations));
                return;
            }

            for (int i = elementStartIdx; i < elements.Length; i++)
            {
                combinations[combIdx] = elements[i];
                Combinations(combIdx + 1, i + 1);
            }
        }
    }
}
