namespace _07_N_Choose_K_Count
{
    using System;

    class NChooseKCount
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.WriteLine(Binom(n, k));
        }

        // With recursion
        private static long GetBinom(int n, int k)
        {
            if (n <= 1 || k == 0 || k == n)
            {
                return 1;
            }

            return GetBinom(n - 1, k) + GetBinom(n - 1, k - 1);
        }

        // With iteration
        private static long Binom(int n, int k)
        {
            int res = 1;

            if (k > n - k)
            {
                k = n - k;
            }

            for (int i = 0; i < k; ++i)
            {
                res *= (n - i);
                res /= (i + 1);
            }

            return res;
        }
    }
}
