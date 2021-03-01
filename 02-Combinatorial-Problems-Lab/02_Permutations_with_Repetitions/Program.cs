namespace _02_Permutations_with_Repetitions
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static string[] elements;
        private static List<string> result;

        static void Main()
        {
            elements = Console.ReadLine().Split();

            result = new List<string>();

            Permute(0);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void Permute(int permutationsIndex)
        {
            if (permutationsIndex >= elements.Length)
            {
                result.Add(string.Join(' ', elements));
                return;
            }

            Permute(permutationsIndex + 1);

            var swapped = new HashSet<string> { elements[permutationsIndex] };

            for (int elementsIndex = permutationsIndex + 1; elementsIndex < elements.Length; elementsIndex++)
            {
                if (!swapped.Contains(elements[elementsIndex])) 
                {
                    Swap(permutationsIndex, elementsIndex);
                    Permute(permutationsIndex + 1);
                    Swap(permutationsIndex, elementsIndex);
                    swapped.Add(elements[elementsIndex]);

                }
            }
        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            var temp = elements[firstIndex];
            elements[firstIndex] = elements[secondIndex];
            elements[secondIndex] = temp;
        }
    }
}
