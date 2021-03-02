namespace _03_Combinations_with_Repetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class CombinationsWithRepetition
    {
        private static int[] elements;
        private static int[] combinations;
        private static List<string> result;
        private static int k;
        private static int n;

        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());
            k = int.Parse(Console.ReadLine());

            elements = Enumerable.Range(1, n).ToArray();

            combinations = new int[k];
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
                Combinations(combIdx + 1, i);
            }
        }
    }
}
