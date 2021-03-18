namespace _04_Sum_with_Limited_Coins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SumWithLimitedCoins
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var target = int.Parse(Console.ReadLine());

            var sums = CalcSums(numbers);
            Console.WriteLine(sums[target]);

            //var sumsCount = GetCount(numbers, target);
            //Console.WriteLine(sumsCount);
        }

        private static Dictionary<int, int> CalcSums(int[] numbers)
        {
            var result = new Dictionary<int, int> {{ 0, 1 } };

            foreach (var number in numbers)
            {
                var sums = result.Keys.ToArray();

                foreach (var sum in sums)
                {
                    var newSum = sum + number;

                    if (!result.ContainsKey(newSum))
                    {
                        result[newSum] = 0;
                    }

                    result[newSum]++;
                }
            }

            return result;
        }

        private static int GetCount(int[] numbers, int target)
        {
            var sums = new HashSet<int> { 0 };
            var count = 0;

            foreach (var number in numbers)
            {
                var newSums = new HashSet<int>();

                foreach (var sum in sums)
                {
                    var newSum = number + sum;
                    newSums.Add(newSum);

                    if (newSum == target)
                    {
                        count++;
                    }
                }

                sums.UnionWith(newSums);
            }

            return count;
        }
    }
}
