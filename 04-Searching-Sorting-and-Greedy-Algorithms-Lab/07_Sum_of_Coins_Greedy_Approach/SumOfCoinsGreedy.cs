namespace _07_Sum_of_Coins_Greedy_Approach
{
    using System;
    using System.Linq;
    using System.Text;

    //not greedy solution - takes best overall, not local solution - not for Judge
    class SumOfCoinsGreedy
    {
        static void Main(string[] args)
        {
            var coins = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var target = int.Parse(Console.ReadLine());

            var sortedCoins = coins.OrderByDescending(c => c).ToList();

            var counter = 0;
            var coinsIndex = 0;
            var sb = new StringBuilder();

            while (target > 0 && coinsIndex < sortedCoins.Count)
            {
                var currentCoin = sortedCoins[coinsIndex++];
                var coinsCount = target / currentCoin;

                if (coinsCount > 0)
                {
                    counter += coinsCount;
                    target -= currentCoin * coinsCount;
                    sb.AppendLine($"{coinsCount} coin(s) with value {currentCoin}");
                }
            }

            if (target > 0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine($"Number of coins to take: {counter}");
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
