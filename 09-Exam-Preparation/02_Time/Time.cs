namespace _02_Time
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Time
    {
        private static int[,] table;
        private static Stack<int> subsequence;

        static void Main(string[] args)
        {
            var nums1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var nums2 = Console.ReadLine().Split().Select(int.Parse).ToArray();

            table = new int[nums1.Length + 1, nums2.Length + 1];            
            subsequence = new Stack<int>();

            FillTableWithSubCount(nums1, nums2);

            FillSubsequence(nums1, nums2);

            PrintSubsequence();

            PrintLength();
        }

        private static void FillTableWithSubCount(int[] nums1, int[] nums2)
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                for (int col = 1; col < table.GetLength(1); col++)
                {
                    if (nums1[row - 1] == nums2[col - 1])
                    {
                        table[row, col] = table[row - 1, col - 1] + 1;
                    }
                    else
                    {
                        table[row, col] = Math.Max(table[row - 1, col], table[row, col - 1]);
                    }
                }
            }
        }

        private static void PrintLength()
        {
            Console.WriteLine($"{table[table.GetLength(0) - 1, table.GetLength(1) - 1]}");
        }

         private static void PrintSubsequence()
        {
            Console.WriteLine(string.Join(" ", subsequence));
        }

        private static void FillSubsequence(int[] nums1, int[] nums2)
        {
            var row = nums1.Length;
            var col = nums2.Length;

            while (row > 0 && col > 0)
            {
                if (nums1[row - 1] == nums2[col - 1] &&
                    table[row - 1, col - 1] == table[row, col] - 1)
                {
                    row -= 1;
                    col -= 1;

                    subsequence.Push(nums1[row]);
                }
                else if (table[row - 1, col] > table[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }
        }
    }
}
