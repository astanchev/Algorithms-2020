namespace _05_Word_Differences
{
    using System;

    class Word_Differences
    {
        private static int[,] table;

        static void Main(string[] args)
        {
            var str1 = Console.ReadLine();
            var str2 = Console.ReadLine();

            table = new int[str1.Length + 1, str2.Length + 1];

            FillTable(str1, str2);

            PrintResult(str1, str2);
        }

        private static void PrintResult(string str1, string str2)
        {
            Console.WriteLine($"Deletions and Insertions: {table[str1.Length, str2.Length]}");
        }

        private static void FillTable(string str1, string str2)
        {
            FillFirstRow();

            FillFirstColumn();

            FillRestOfTheTable(str1, str2);
        }

        private static void FillRestOfTheTable(string str1, string str2)
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                for (int col = 1; col < table.GetLength(1); col++)
                {
                    if (str1[row - 1] == str2[col - 1])
                    {
                        table[row, col] = table[row - 1, col - 1];
                    }
                    else
                    {
                        table[row, col] = 1 + Math.Min(table[row, col - 1], table[row - 1, col]);
                    }
                }
            }
        }

        private static void FillFirstColumn()
        {
            for (int col = 1; col < table.GetLength(1); col++)
            {
                table[0, col] = col;
            }
        }

        private static void FillFirstRow()
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                table[row, 0] = row;
            }
        }
    }
}

//      ""  Y   M   C   A
//  ""  0   1   2   3   4
//  H   1   2   3   4   5
//  M   2   3   2   3   4
//  B   3   4   3   4   5
//  B   4   5   4   5   6

// If characters are different we take smaller between left and upper cell and add 1
// If characters are same we take the left-upper diagonal cell
