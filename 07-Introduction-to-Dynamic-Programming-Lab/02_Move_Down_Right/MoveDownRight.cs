namespace _02_Move_Down_Right
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class MoveDownRight
    {
        private static int[,] matrix;
        private static int[,] sums;
        private static Stack<string> path;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new int[rows, cols];
            sums = new int[rows, cols];
            path = new Stack<string>();

            ReadMatrix();

            FindSums();

            //PrintSums();

            FindPath();

            Console.WriteLine(string.Join(" ", path));
        }

        private static void FindPath()
        {
            var row = sums.GetLength(0) - 1;
            var col = sums.GetLength(1) - 1;

            path.Push($"[{row}, {col}]");

            while (row > 0 && col > 0)
            {
                var upper = sums[row - 1, col];
                var left = sums[row, col - 1];

                if (upper > left)
                {
                    row--;
                }
                else
                {
                    col--;
                }

                path.Push($"[{row}, {col}]");
            }

            while (row > 0)
            {
                path.Push($"[{--row}, {col}]");
            }

            while (col > 0)
            {
                path.Push($"[{row}, {--col}]");
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
            sums[0, 0] = matrix[0, 0];

            FillFirstRowSums();

            FillFirstColSums();

            FillRestSums();
        }

        private static void FillRestSums()
        {
            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                for (int col = 1; col < matrix.GetLength(1); col++)
                {
                    var upperCell = sums[row - 1, col];
                    var leftCell = sums[row, col - 1];

                    sums[row, col] = Math.Max(upperCell, leftCell) + matrix[row, col];
                }
            }
        }

        private static void FillFirstColSums()
        {
            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                sums[row, 0] = sums[row - 1, 0] + matrix[row, 0];
            }
        }

        private static void FillFirstRowSums()
        {
            for (int col = 1; col < matrix.GetLength(1); col++)
            {
                sums[0, col] = sums[0, col - 1] + matrix[0, col];
            }
        }

        private static void ReadMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
    }
}
