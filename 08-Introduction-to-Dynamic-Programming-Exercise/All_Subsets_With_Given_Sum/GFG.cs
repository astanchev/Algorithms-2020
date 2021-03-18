namespace All_Subsets_With_Given_Sum
{
    // C# program to print all subsets with given sum 
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class GFG
    {
        private static List<List<int>> subsets;

        // Driver code 
        public static void Main()
        {
            subsets = new List<List<int>>();

            int[] arr = { 2, 5, 8, 4, 6, 11 };
            int sum = 13;
            int length = arr.Length;

            //FindAllSubsets(arr, length, sum);

            //new solution
            SumUp(arr.ToList(), sum);

            PrintAllSubsets();
        }

        private static void PrintAllSubsets()
        {
            foreach (var subset in subsets)
            {
                Console.WriteLine(string.Join(" ", subset));
            }
        }


        // The list subset stores current subset. 
        static void FindAllSubsetsRec(int[] arr, int len, List<int> subset, int sum)
        {
            // If remaining sum is 0, then print all 
            // elements of current subset. 
            if (sum == 0)
            {
                subsets.Add(subset);
                return;
            }

            // If no remaining elements, 
            if (len == 0)
            {
                return;
            }

            // We consider two cases for every element. 
            // a) We do not include last element. 
            // b) We include last element in current subset. 
            FindAllSubsetsRec(arr, len - 1, subset, sum);

            List<int> newSubset = new List<int>(subset);
            newSubset.Add(arr[len - 1]);

            FindAllSubsetsRec(arr, len - 1, newSubset, sum - arr[len - 1]);
        }

        // Wrapper over printAllSubsetsRec() 
        static void FindAllSubsets(int[] arr, int length, int sum)
        {
            List<int> subset = new List<int>();

            FindAllSubsetsRec(arr, length, subset, sum);
        }

        //new solution
        private static void SumUp(List<int> numbers, int target)
        {
            SumUpRecursive(numbers, target, new List<int>());
        }

        //new solution
        private static void SumUpRecursive(List<int> numbers, int target, List<int> partial)
        {
            int sum = 0;

            foreach (int num in partial)
            {
                sum += num;
            }

            if (sum == target)
            {
                subsets.Add(partial);
            }

            if (sum >= target)
            {
                return;
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int num = numbers[i];

                for (int j = i + 1; j < numbers.Count; j++)
                {
                    remaining.Add(numbers[j]);
                }

                List<int> partialRec = new List<int>(partial);
                partialRec.Add(num);

                SumUpRecursive(remaining, target, partialRec);
            }
        }

    }
}
