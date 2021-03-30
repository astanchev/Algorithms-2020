namespace _01_Rod_Cutting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RodCutting
    {
        private static int[] prices;
        private static int[] bestPrice;
        private static int[] bestCombo;
        private static List<int> result;

        public static void Main()
        {
            prices = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            int ways = int.Parse(Console.ReadLine());

            bestPrice = new int[prices.Length];
            //For solution with recursion
            //Array.Fill(bestPrice, -1);

            bestCombo = new int[prices.Length];

            result = new List<int>();

            //For solution with recursion
            //var price = CutRodRecursion(ways);

            var price = CutRodIterative(ways);

            ReconstructSolution(ways);

            Console.WriteLine(price);
            Console.WriteLine(string.Join(" ", result));
        }

        private static int CutRodRecursion(int n)
        {
            if (bestPrice[n] >= 0)
            {
                return bestPrice[n];
            }

            if (n == 0)
            {
                return 0;
            }

            var currentBest = bestPrice[n];

            for (int i = 1; i <= n; i++)
            {
                currentBest = Math.Max(currentBest, prices[i] + CutRodRecursion(n - i));

                if (currentBest > bestPrice[n])
                {
                    bestPrice[n] = currentBest;
                    bestCombo[n] = i;
                }
            }

            return bestPrice[n];
        }

        private static int CutRodIterative(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                int currentBest = bestPrice[i];

                for (int j = 1; j <= i; j++)
                {
                    currentBest = Math.Max(bestPrice[i], prices[j] + bestPrice[i - j]);

                    if (currentBest > bestPrice[i])
                    {
                        bestPrice[i] = currentBest;
                        bestCombo[i] = j;
                    }
                }
            }

            return bestPrice[n];
        }

        private static void ReconstructSolution(int n)
        {
            while (bestCombo[n] != 0)
            {
                result.Add(bestCombo[n]);
                n = n - bestCombo[n];
            }
        }

    }
}
