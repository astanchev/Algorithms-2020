namespace _03_Variations_without_Repetitions
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static string[] elements;
        private static string[] variations;
        private static List<string> result;
        private static bool[] used;
        private static int k;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());

            variations = new string[k];
            used = new bool[elements.Length];
            result = new List<string>();

            Variations(0);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void Variations(int index)
        {
            if (index >= variations.Length)
            {
                result.Add(string.Join(' ', variations));
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    variations[index] = elements[i];
                    Variations(index + 1);
                    used[i] = false;
                }
            }
        }
    }
}
