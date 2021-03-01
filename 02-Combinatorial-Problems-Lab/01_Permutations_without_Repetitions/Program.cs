namespace _01_Permutations_without_Repetitions
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static string[] elements;
        private static string[] permutations;
        private static List<string> result;
        private static bool[] used;

        //static void Main(string[] args)
        //{
        //    elements = Console.ReadLine().Split();
        //    permutations = new string[elements.Length];
        //    used = new bool[elements.Length];
        //    result = new List<string>();

        //    Permute(0);

        //    Console.WriteLine(string.Join(Environment.NewLine, result));
        //}

        private static void Permute(int permutationsIndex)
        {
            if (permutationsIndex >= elements.Length)
            {
                result.Add(string.Join(' ', permutations));
                return;
            }

            for (int elementsIndex = 0; elementsIndex < elements.Length; elementsIndex++)
            {
                if (!used[elementsIndex])
                {
                    used[elementsIndex] = true;
                    permutations[permutationsIndex] = elements[elementsIndex];
                    Permute(permutationsIndex + 1);
                    used[elementsIndex] = false;
                }
            }
        }
    }
}
