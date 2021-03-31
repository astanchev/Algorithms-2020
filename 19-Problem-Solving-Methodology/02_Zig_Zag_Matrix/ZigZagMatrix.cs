namespace _02_Zig_Zag_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ZigZagMatrix
    {
        private static int[][] matrix;
        private static int[,] maxPaths;
        private static int[,] previousRowIndex;

        public static void Main(string[] args)
        {
            int numberOfRows = int.Parse(Console.ReadLine());
            int numberOfColumns = int.Parse(Console.ReadLine());

            matrix = new int[numberOfRows][];
            ReadMatrix(numberOfRows);

            maxPaths = new int[numberOfRows, numberOfColumns];
            previousRowIndex = new int[numberOfRows, numberOfColumns];

            FillMaxPaths(numberOfRows, numberOfColumns);

            var maxRowIndex = GetLastRowIndexOfPath(numberOfColumns);

            var path = RecoverMaxPath(numberOfColumns, maxRowIndex);

            Console.WriteLine($"{path.Sum()} = {string.Join(" + ", path)}");
        }

        private static List<int> RecoverMaxPath(int numberOfColumns, int maxRowIndex)
        {
            List<int> path = new List<int>();
            int columnIndex = numberOfColumns - 1;

            while (columnIndex >= 0)
            {
                path.Add(matrix[maxRowIndex][columnIndex]);
                maxRowIndex = previousRowIndex[maxRowIndex, columnIndex];

                columnIndex--;
            }

            path.Reverse();

            return path;
        }

        private static int GetLastRowIndexOfPath(int numberOfColumns)
        {
            int currentRowIndex = -1;
            int globalMax = 0;

            for (int row = 0; row < maxPaths.GetLength(0); row++)
            {
                if (maxPaths[row, numberOfColumns - 1] > globalMax)
                {
                    globalMax = maxPaths[row, numberOfColumns - 1];
                    currentRowIndex = row;
                }
            }

            return currentRowIndex;
        }

        private static void FillMaxPaths(int numberOfRows, int numberOfColumns)
        {
            for (int row = 1; row < numberOfRows; row++)
            {
                maxPaths[row, 0] = matrix[row][0];
            }

            for (int col = 1; col < numberOfColumns; col++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    int previousMax = 0;

                    if (col % 2 != 0)
                    {
                        for (int i = row + 1; i < numberOfRows; i++)
                        {
                            if (maxPaths[i, col - 1] > previousMax)
                            {
                                previousMax = maxPaths[i, col - 1];
                                previousRowIndex[row, col] = i;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < row; i++)
                        {
                            if (maxPaths[i, col - 1] > previousMax)
                            {
                                previousMax = maxPaths[i, col - 1];
                                previousRowIndex[row, col] = i;
                            }
                        }
                    }

                    maxPaths[row, col] = previousMax + matrix[row][col];
                }
            }
        }

        private static void ReadMatrix(int numberOfRows)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }
        }
    }
}
