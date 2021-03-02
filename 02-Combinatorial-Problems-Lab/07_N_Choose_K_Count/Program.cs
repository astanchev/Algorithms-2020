namespace _07_N_Choose_K_Count
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.WriteLine(GetBinom(n, k));
        }

        private static long GetBinom(int n, int k)
        {
            if (n <= 1 || k == 0 || k == n)
            {
                return 1;
            }

            return GetBinom(n - 1, k) + GetBinom(n - 1, k - 1);
        }
    }
}
