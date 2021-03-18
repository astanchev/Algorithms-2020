namespace _02_Dividing_Presents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class DividingPresents
    {
        private static int[] presents;
        private static Dictionary<int,int> sums;

        static void Main(string[] args)
        {
            presents = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            sums = CalcSums();

            var totalSum = presents.Sum();
            int middle = (int)Math.Ceiling(totalSum * 1.0 / 2);

            var bobScore = sums.Keys
                            .Where(s => s >= middle)
                            .OrderBy(s => s)
                            .FirstOrDefault();

            var alanScore = totalSum - bobScore;

            var alanPresents = GetPresents(alanScore);

            PrintResult(bobScore, alanScore, alanPresents);

        }

        private static void PrintResult(int bobScore, int alanScore, List<int> alanPresents)
        {
            Console.WriteLine($"Difference: {bobScore - alanScore}");
            Console.WriteLine($"Alan:{alanScore} Bob:{bobScore}");
            Console.WriteLine($"Alan takes: {string.Join(" ", alanPresents)}");
            Console.WriteLine($"Bob takes the rest.");
        }

        private static List<int> GetPresents(int alanScore)
        {
            var subset = new List<int>();

            while (alanScore > 0)
            {
                var lastSum = sums[alanScore];
                subset.Add(lastSum);
                alanScore -= lastSum;
            }

            return subset;
        }

        private static Dictionary<int,int> CalcSums()
        {
            var possibleSums = new Dictionary<int, int> { { 0, 0 } };

            foreach (var present in presents)
            {
                var newSums = new Dictionary<int, int>();

                foreach (var sum in possibleSums.Keys)
                {
                    var newSum = sum + present;

                    if (!possibleSums.ContainsKey(newSum))
                    {
                        newSums.Add(newSum, present);
                    }
                }

                foreach (var sum in newSums)
                {
                    possibleSums.Add(sum.Key, sum.Value);
                }
            }

            return possibleSums;
        }
    }
}
