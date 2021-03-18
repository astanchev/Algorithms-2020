namespace _07_Minimum_Edit_Distance
{
    using System;

    class Minimum_Edit_Distance
    {
        private static int[,] table;

        static void Main(string[] args)
        {
            int replaceCost = int.Parse(Console.ReadLine());
            int insertCost = int.Parse(Console.ReadLine());
            int deleteCost = int.Parse(Console.ReadLine());

            var str1 = Console.ReadLine();
            var str2 = Console.ReadLine();

            table = new int[str1.Length + 1, str2.Length + 1];

            FillTable(str1, str2, replaceCost, deleteCost, insertCost);

            PrintResult(str1, str2);
        }

        private static void FillTable(string str1, string str2, int replaceCost, int deleteCost, int insertCost)
        {
            FillFirstRow(deleteCost);

            FillFirstColumn(insertCost);

            FillRestOfTheTable(str1, str2, replaceCost, deleteCost, insertCost);
        }

        private static void FillRestOfTheTable(string str1, string str2, int replaceCost, int deleteCost, int insertCost)
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                for (int col = 1; col < table.GetLength(1); col++)
                {
                    var cost = str1[row - 1] == str2[col - 1] ? 0 : replaceCost;

                    var delete = table[row - 1, col] + deleteCost;
                    var replace = table[row - 1, col - 1] + cost;
                    var insert = table[row, col - 1] + insertCost;

                    table[row, col] = Math.Min(Math.Min(delete, insert), replace);
                }
            }
        }

        private static void FillFirstColumn(int insertCost)
        {
            for (int col = 1; col < table.GetLength(1); col++)
            {
                table[0, col] = col * insertCost;
            }
        }

        private static void FillFirstRow(int deleteCost)
        {
            for (int row = 1; row < table.GetLength(0); row++)
            {
                table[row, 0] = row * deleteCost;
            }
        }

        private static void PrintResult(string str1, string str2)
        {
            Console.WriteLine($"Minimum edit distance: {table[str1.Length, str2.Length]}");
        }
    }
}
