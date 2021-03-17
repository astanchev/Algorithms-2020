namespace _01_Fibonacci
{
    using System;
    using System.Collections.Generic;

    class Fibonacci
    {
        private static long[] results;
        private static Dictionary<int, long> cache;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            results = new long[n + 1];
            cache = new Dictionary<int, long>();

            //long res = FindFibbonacci(n);
            long res = FindFibonacciDict(n);

            Console.WriteLine(res);
        }

        private static long FindFibonacciDict(int n)
        {
            if (cache.ContainsKey(n))
            {
                return cache[n];
            }

            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            cache[n] = FindFibonacciDict(n - 1) + FindFibonacciDict(n - 2);

            return cache[n];
        }

        private static long FindFibbonacci(int n)
        {
            if (results[n] != 0)
            {
                return results[n];
            }

            if (n == 0)
            {
                results[n] = 0;
                return 0;
            }

            if (n == 1)
            {
                results[n] = 1;
                return 1;
            }

            results[n] = FindFibbonacci(n - 1) + FindFibbonacci(n - 2);

            return results[n];
        }


    }
}
