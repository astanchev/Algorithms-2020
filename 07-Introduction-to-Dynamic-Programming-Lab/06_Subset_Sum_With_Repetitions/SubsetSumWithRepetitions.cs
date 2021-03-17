namespace _06_Subset_Sum_With_Repetitions
{
    using System;
    using System.Collections.Generic;

    class SubsetSumWithRepetitions
    {
        static void Main(string[] args)
        {
            var nums = new[] { 3, 5, 2 };
            var target = 17;

            bool[] sums = CalcPossibleSums(nums, target);

            Console.WriteLine(sums[target]);

            var subset = FindSubset(nums, target, sums);

            subset.Sort();

            Console.WriteLine(string.Join(" ", subset));
        }

        static bool[] CalcPossibleSums(int[] nums, int targetSum)
        {
            var possible = new bool[targetSum + 1];
            possible[0] = true;

            for (int sum = 0; sum < possible.Length; sum++)
            {
                if (!possible[sum])
                {
                    continue;
                }

                foreach (var num in nums)
                {
                    var newSum = sum + num;

                    if (newSum <= targetSum)
                    {
                        possible[newSum] = true;
                    }
                }
            }

            return possible;
        }

        static List<int> FindSubset(int[] nums, int targetSum, bool[] possibleSums)
        {
            var subset = new List<int>();
            
            while (targetSum > 0)
            {
                foreach (var num in nums)
                {
                    var newSum = targetSum - num;
            
                    if (newSum >= 0 && possibleSums[newSum])
                    {
                        targetSum = newSum;
                        subset.Add(num);
                    }
                }
            }

            return subset;
        }
    }
}
