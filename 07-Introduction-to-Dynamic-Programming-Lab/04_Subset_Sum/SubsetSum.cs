namespace _04_Subset_Sum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SubsetSum
    {
        static void Main(string[] args)
        {
            var nums = new[] { 3, 5, 1, 4, 2 };
            var target = 7;

            var sums = GetAllPossibleSums(nums);

            PrintAllPossibleSums(sums);

            PrintIsSumContainsTarget(target, sums);
        }

        private static HashSet<int> GetAllPossibleSums(int[] nums)
        {
            var sums = new HashSet<int> { 0 };

            foreach (var num in nums)
            {
                var newSums = new HashSet<int>();

                foreach (var sum in sums)
                {
                    var newSum = sum + num;
                    newSums.Add(newSum);
                }

                sums.UnionWith(newSums);
            }

            if (!nums.Contains(0))
            {
                sums.Remove(0);
            }

            return sums;
        }

        private static void PrintIsSumContainsTarget(int target, HashSet<int> sums)
        {
            Console.WriteLine(sums.Contains(target));
        }

        private static void PrintAllPossibleSums(HashSet<int> sums)
        {
            Console.WriteLine(string.Join(" ", sums));
        }
    }
}
