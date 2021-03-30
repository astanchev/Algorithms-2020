namespace _04_Longest_Zigzag_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestZigzagSubsequence
    {
        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            var maxResult = 0;
            var maxIndexRow = 0;
            var maxIndexCol = 0;

            //First row are LZS which ends with number bigger then previous
            //Second row are LZS which ends with number smaller then previous
            var dp = new int[numbers.Length, 2];

            //Indexes of the previous best solution - for reconstruction of the path
            var prev = new int[numbers.Length, 2];

            //If sequence is with 1 number the both subsequences ar with length 1
            dp[0, 0] = dp[0, 1] = 1;

            //For the first number there is no index for prev
            prev[0, 0] = prev[0, 1] = -1;

            for (int currentIndex = 1; currentIndex < numbers.Length; currentIndex++)
            {
                for (int prevIndex = 0; prevIndex < currentIndex; prevIndex++)
                {
                    var currentNumber = numbers[currentIndex];
                    var prevNumber = numbers[prevIndex];

                    if (currentNumber > prevNumber &&       //We check for the first row 
                        dp[currentIndex, 0] < dp[prevIndex, 1] + 1) //(curent number is bigger than previous) 
                    {                                               //so we compare first row number with second row prevIndex number
                        dp[currentIndex, 0] = dp[prevIndex, 1] + 1;

                        prev[currentIndex, 0] = prevIndex;
                    }

                    if (currentNumber < prevNumber &&       //We check for the second row 
                        dp[currentIndex, 1] < dp[prevIndex, 0] + 1) //(curent number is smaller than previous)
                    {                                               // so we compare second row number with first row prevIndex number
                        dp[currentIndex, 1] = dp[prevIndex, 0] + 1;

                        prev[currentIndex, 1] = prevIndex;
                    }
                }

                if (dp[currentIndex, 0] > maxResult)
                {
                    maxResult = dp[currentIndex, 0];
                    maxIndexRow = currentIndex;
                    maxIndexCol = 0;
                }

                if (dp[currentIndex, 1] > maxResult)
                {
                    maxResult = dp[currentIndex, 1];
                    maxIndexRow = currentIndex;
                    maxIndexCol = 1;
                }
            }

            var result = new Stack<int>();

            while (maxIndexRow >= 0)
            {
                result.Push(numbers[maxIndexRow]);

                maxIndexRow = prev[maxIndexRow, maxIndexCol];

                if (maxIndexCol == 1)
                {
                    maxIndexCol = 0;
                }
                else
                {
                    maxIndexCol = 1;
                }
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
