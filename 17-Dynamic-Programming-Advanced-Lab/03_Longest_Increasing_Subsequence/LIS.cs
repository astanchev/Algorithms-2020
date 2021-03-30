namespace _03_Longest_Increasing_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LIS
    {
        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();
            var len = new int[numbers.Length];
            var prev = new int[numbers.Length];
            Array.Fill(prev, -1);

            Stack<int> result = new Stack<int>();

            var maxLen = 0;
            var maxSolutionIndex = -1;

            for (int current = 0; current < numbers.Length; current++)
            {
                var currentLen = 1;
                var currentNumber = numbers[current];;

                for (int lenIndex = 0; lenIndex < current; lenIndex++)
                {
                    var prevNumber = numbers[lenIndex];
                    var prevLen = len[lenIndex];

                    if (currentNumber > prevNumber && currentLen < prevLen + 1)
                    {
                        currentLen = prevLen + 1;
                        prev[current] = lenIndex;
                    }
                }

                len[current] = currentLen;

                if (maxLen < currentLen)
                {
                    maxLen = currentLen;
                    maxSolutionIndex = current;
                }
            }

            while (maxSolutionIndex != -1)
            {
                var currentNumber = numbers[maxSolutionIndex];
                result.Push(currentNumber);
                maxSolutionIndex = prev[maxSolutionIndex];
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
