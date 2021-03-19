namespace _01_Two_Minutes_to_Midnight
{
    using System;
    using System.Collections.Generic;

    class TwoMinutesToMidnight
    {
        private static Dictionary<string, long> cache;

        static void Main(string[] args)
        {
             int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            cache = new Dictionary<string, long>();

            Console.WriteLine(GetBinom(n, k));
        }

        private static long GetBinom(int n, int k)
        {
            var id = $"{n} {k}";

            if (cache.ContainsKey(id))
            {
                return cache[id];
            }

            if (n <= 1 || k == 0 || k == n)
            {
                return 1;
            }

            var result = GetBinom(n - 1, k) + GetBinom(n - 1, k - 1);
            cache[id] = result;

            return result;
        }
    }
}
