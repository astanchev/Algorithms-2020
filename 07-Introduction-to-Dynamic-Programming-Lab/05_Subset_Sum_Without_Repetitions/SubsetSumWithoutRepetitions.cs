namespace _05_Subset_Sum_Without_Repetitions
{
    using System;
    using System.Collections.Generic;

    class SubsetSumWithoutRepetitions
    {
        static void Main(string[] args)
        {
            var nums = new[] { 3, 5, 1, 4, 2 };
            var target = 7;

            var possibleSums = CalcPossibleSums(nums);

            var firstPossibleSubset = FindSubset(target, possibleSums);

            PrintFirstPossibleSubset(firstPossibleSubset);
        }

        private static Dictionary<int, int> CalcPossibleSums(int[] nums)
        {
            var possibleSums = new Dictionary<int, int> { { 0, 0 } };

            foreach (var num in nums)
            {
                var newSums = new Dictionary<int, int>();

                foreach (var sum in possibleSums.Keys)
                {
                    var newSum = sum + num;
                    if (!possibleSums.ContainsKey(newSum))
                    {
                        newSums.Add(newSum, num);
                    }
                }

                foreach (var sum in newSums)
                {
                    possibleSums.Add(sum.Key, sum.Value);
                }
            }

            return possibleSums;
        }

        static List<int> FindSubset(int targetSum, IDictionary<int, int> possibleSums)
        {
            var subset = new List<int>();

            while (targetSum > 0)
            {
                var lastNum = possibleSums[targetSum];
                subset.Add(lastNum);
                targetSum -= lastNum;
            }

            subset.Reverse();

            return subset;
        }

        private static void PrintFirstPossibleSubset(List<int> firstPossibleSubset)
        {
            Console.WriteLine(string.Join(" ", firstPossibleSubset));
        }
    }
}
