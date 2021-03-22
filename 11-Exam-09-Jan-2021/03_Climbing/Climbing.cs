namespace _03_Climbing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Climbing
    {
        private static int[,] matrix;
        private static int[,] sums;
        private static Stack<int> path;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new int[rows, cols];
            sums = new int[rows, cols];
            path = new Stack<int>();

            ReadMatrix();

            FindSums();

            //PrintSums();

            FindPath();

            Console.WriteLine(sums[0, 0]);
            Console.WriteLine(string.Join(" ", path));
        }

        private static void FindPath()
        {
            var row = 0;
            var col = 0;

            path.Push(matrix[row, col]);

            while (row < sums.GetLength(0) - 1 && col < sums.GetLength(1) - 1)
            {
                var lower = sums[row + 1, col];
                var right = sums[row, col + 1];

                if (lower > right)
                {
                    row++;
                }
                else
                {
                    col++;
                }

                path.Push(matrix[row, col]);
            }

            while (row < sums.GetLength(0) - 1)
            {
                path.Push(matrix[++row, col]);
            }

            while (col < sums.GetLength(1) - 1)
            {
                path.Push(matrix[row, ++col]);
            }
        }

        private static void PrintSums()
        {
            for (int row = 0; row < sums.GetLength(0); row++)
            {
                for (int col = 0; col < sums.GetLength(1); col++)
                {
                    if (col > 0)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(sums[row, col]);
                }

                Console.WriteLine();
            }
        }

        private static void FindSums()
        {
            sums[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1] = matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];

            FillLastRowSums();

            FillLastColSums();

            FillRestSums();
        }

        private static void FillRestSums()
        {
            var lastCol = matrix.GetLength(1) - 1;
            var lastRow = matrix.GetLength(0) - 1;

            for (int row = lastRow - 1; row >= 0; row--)
            {
                for (int col = lastCol - 1; col >= 0; col--)
                {
                    var lowerCell = sums[row + 1, col];
                    var rightCell = sums[row, col + 1];

                    sums[row, col] = Math.Max(lowerCell, rightCell) + matrix[row, col];
                }
            }
        }

        private static void FillLastColSums()
        {
            var lastCol = matrix.GetLength(1) - 1;
            var lastRow = matrix.GetLength(0) - 1;

            for (int row = lastRow - 1; row >= 0; row--)
            {
                sums[row, lastCol] = sums[row + 1, lastCol] + matrix[row, lastCol];
            }
        }

        private static void FillLastRowSums()
        {
            var lastCol = matrix.GetLength(1) - 1;
            var lastRow = matrix.GetLength(0) - 1;

            for (int col = lastCol - 1; col >= 0; col--)
            {
                sums[lastRow, col] = sums[lastRow, col + 1] + matrix[lastRow, col];
            }
        }

        private static void ReadMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
    }
}
