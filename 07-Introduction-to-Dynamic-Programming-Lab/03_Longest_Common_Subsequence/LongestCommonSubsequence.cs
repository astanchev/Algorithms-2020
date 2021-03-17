namespace _03_Longest_Common_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LongestCommonSubsequence
    {
        private static int[,] table;
        private static Stack<char> subsequence;

        static void Main(string[] args)
        {
            var str1 = Console.ReadLine();
            var str2 = Console.ReadLine();

            table = new int[str1.Length + 1, str2.Length + 1];
            subsequence = new Stack<char>();

            FillTableWithSubCount(str1, str2);

            PrintLength();

            FillSubsequence(str1, str2);

            PrintSubsequence();
        }

        private static void PrintSubsequence()
        {
            Console.WriteLine(string.Join(" ", subsequence));
        }

        private static void FillSubsequence(string str1, string str2)
        {
            var row = str1.Length;
            var col = str2.Length;

            while (row > 0 && col > 0)
            {
                if (str1[row - 1] == str2[col - 1])
                {
                    row -= 1;
                    col -= 1;

                    subsequence.Push(str1[row]);
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

        private static void PrintLength()
        {
            Console.WriteLine(table[table.GetLength(0) - 1, table.GetLength(1) - 1]);
        }

        private static void FillTableWithSubCount(string str1, string str2)
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                for (int col = 1; col < table.GetLength(1); col++)
                {
                    if (str1[row - 1] == str2[col - 1])
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
    }
}
