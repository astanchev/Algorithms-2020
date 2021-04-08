namespace _03_Generating_0_1_Vectors
{
    using System;

    class Generating01Vectors
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintVectors(n);
        }

        private static void PrintVectors(int n)
        {
            int index = 0;
            int[] vector = new int[n];

            Gen01(index, vector);
        }

        private static void Gen01(int index, int[] vector)
        {
            if (index >= vector.Length)
            {
                Console.WriteLine(string.Join("", vector));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;
                    Gen01(index + 1, vector);
                }
            }
        }
    }
}
