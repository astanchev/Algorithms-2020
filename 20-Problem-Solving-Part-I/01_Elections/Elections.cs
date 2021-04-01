namespace _01_Elections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Elections
    {
        static void Main(string[] args)
        {
            var neededSeats = int.Parse(Console.ReadLine());
            var parties = int.Parse(Console.ReadLine());
            var votes = new int[parties];

            FillVotes(parties, votes);

            var sums = GetAllPossibleSums(votes);

            var combinations = sums.Where(s => s >= neededSeats).ToArray();

            Console.WriteLine(combinations.Length);
        }

        private static List<int> GetAllPossibleSums(int[] nums)
        {
            var sums = new List<int> { 0 };

            foreach (var num in nums)
            {
                var newSums = new List<int>();

                foreach (var sum in sums)
                {
                    var newSum = sum + num;
                    newSums.Add(newSum);
                }

                sums.AddRange(newSums);
            }

            return sums;
        }

        private static void FillVotes(int parties, int[] votes)
        {
            for (var i = 0; i < parties; i++)
            {
                votes[i] = int.Parse(Console.ReadLine());
            }
        }
    }
}
