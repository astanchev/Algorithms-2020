namespace _05_Connected_Areas_in_a_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Area
    {
        public int Size { get; set; }

        public int StartRow { get; set; }

        public int StartCol { get; set; }
    }


    public class ConnectedAreas
    {
        private static char freeCell = '-';
        private static char wallCell = '*';
        private static char visitedCell = 'v';
        private static char[,] matrix;
        private static List<Area> areas;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new char[rows, cols];
            areas = new List<Area>();

            FillMatrix(rows, cols);

            GetaAreas();

            PrintResult();
        }

        private static void PrintResult()
        {
            var sorted = areas
                        .OrderByDescending(a => a.Size)
                        .ThenBy(a => a.StartRow)
                        .ThenBy(a => a.StartCol)
                        .ToList();

            Console.WriteLine($"Total areas found: {sorted.Count}");

            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine($"Area #{i + 1} at ({sorted[i].StartRow}, {sorted[i].StartCol}), size: {sorted[i].Size}");
            }
        }

        private static void GetaAreas()
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == wallCell || matrix[r, c] == visitedCell)
                    {
                        continue;
                    }

                    int areaSize = GetaAreaSize(r, c);

                    Area area = new Area()
                    {
                        Size = areaSize,
                        StartRow = r,
                        StartCol = c,
                    };

                    areas.Add(area);
                }
            }
        }

        private static int GetaAreaSize(int row, int col)
        {
            if (IsOutside(row, col) ||
                IsVisited(row, col) ||
                IsWall(row, col))
            {
                return 0;
            }

            matrix[row, col] = visitedCell;

            int areaSize = 1 + 
                            GetaAreaSize(row + 1, col) +
                            GetaAreaSize(row - 1, col) +
                            GetaAreaSize(row, col + 1) +
                            GetaAreaSize(row, col - 1);

            return areaSize;
        }

        private static void FillMatrix(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine().ToCharArray();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
        }

        private static bool IsVisited(int row, int col)
        {
            return matrix[row, col] == visitedCell;
        }

        private static bool IsWall(int row, int col)
        {
            return matrix[row, col] == wallCell;
        }

        private static bool IsOutside(int row, int col)
        {
            return row < 0 ||
                    row >= matrix.GetLength(0) ||
                    col < 0 ||
                    col >= matrix.GetLength(1);
        }
    }
}
