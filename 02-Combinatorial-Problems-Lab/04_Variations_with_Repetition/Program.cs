namespace _04_Variations_with_Repetition
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static string[] elements;
        private static string[] variations;
        private static List<string> result;
        private static int k;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());

            variations = new string[k];
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
                variations[index] = elements[i];
                Variations(index + 1);
            }
        }
    }
}
