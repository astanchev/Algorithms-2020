namespace _07_Sum_of_Coins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class SumOfCoins
    {
        private static SortedDictionary<int, int> solutions;
        static readonly int MAX = 100000;

        // dp array to memoize the results
        static int[] dp = new int[MAX + 1];

        // List to store the result
        static List<int> denomination = new List<int>();

        static void Main(string[] args)
        {
            int[] coins = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int target = int.Parse(Console.ReadLine());

            solutions = new SortedDictionary<int, int>(
                                                      Comparer<int>.Create((x, y) => y.CompareTo(x)));

            countMinCoinsUtil(target, coins, coins.Length);

            PrintSolutions();
        }

        private static void PrintSolutions()
        {
            if (solutions.Count == 0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine($"Number of coins to take: {solutions.Values.Sum()}");
                foreach (var (coin, count) in solutions)
                {
                    Console.WriteLine($"{count} coin(s) with value {coin}");
                }
            }
        }

        static int countMinCoins(int target, int[] coins, int coinsLen)
        {
            // Base case
            if (target == 0)
            {
                dp[0] = 0;
                return 0;
            }

            // If previously computed
            // subproblem occurred
            if (dp[target] != -1)
            {
                return dp[target];
            }

            // Initialize result
            int ret = int.MaxValue;

            // Try every coin that has smaller
            // value than n
            for (int i = 0; i < coinsLen; i++)
            {
                if (coins[i] <= target)
                {
                    int x = countMinCoins(target - coins[i], coins, coinsLen);

                    // Check for int.MaxValue to avoid
                    // overflow and see if result
                    // can be minimized
                    if (x != int.MaxValue)
                    {
                        ret = Math.Min(ret, 1 + x);
                    }
                }
            }

            // Memoizing value of current state
            dp[target] = ret;
            return ret;
        }

        // Function to find the possible
        // combination of coins to make
        // the sum equal to X
        static void findSolution(int target, int[] coins, int coinsLen)
        {
            // Base Case
            if (target == 0)
            {
                // Print Solutions
                foreach (int it in denomination)
                {
                    if (!solutions.ContainsKey(it))
                    {
                        solutions[it] = 0;
                    }

                     solutions[it]++;
                }

                return;
            }

            for (int i = 0; i < coinsLen; i++)
            {
                // Try every coin that has
                // value smaller than n
                if (target - coins[i] >= 0 && dp[target - coins[i]] + 1 == dp[target])
                {
                    // Add current denominations
                    denomination.Add(coins[i]);

                    // Backtrack
                    findSolution(target - coins[i], coins, coinsLen);
                    break;
                }
            }
        }

        // Function to find the minimum
        // combinations of coins for value X
        static void countMinCoinsUtil(int target, int[] coins, int coinsLen)
        {
            // Initialize dp with -1
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = -1;
            }

            // Min coins
            int isPossible = countMinCoins(target, coins, coinsLen);

            // If no solution exists
            if (isPossible == int.MaxValue)
            {
                return;
            }

            // Backtrack to find the solution
            else
            {
                findSolution(target, coins, coinsLen);
            }
        }
    }
}
