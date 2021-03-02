namespace _02_Nested_Loops_To_Recursion
{
    using System;

    class NestedLoops
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintLoops(n);
        }

        private static void PrintLoops(int n)
        {
            int index = 0;
            int[] vector = new int[n];

            GenerateLoops(index, vector);
        }

        private static void GenerateLoops(int index, int[] vector)
        {
            if (index >= vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
            }
            else
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    vector[index] = i + 1;
                    GenerateLoops(index + 1, vector);
                }
            }
        }
    }
}
