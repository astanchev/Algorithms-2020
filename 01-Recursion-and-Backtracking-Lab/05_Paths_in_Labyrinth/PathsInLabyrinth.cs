namespace _05_Paths_in_Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class PathsInLabyrinth
    {
        private static char freeCell = '-';
        private static char wallCell = '*';
        private static char exitCell = 'e';
        private static char visitedCell = 'v';
        private static char upCell = 'U';
        private static char downCell = 'D';
        private static char rightCell = 'R';
        private static char leftCell = 'L';
        private static int[,] labyrinth;
        private static List<string> solutions;
        private static List<char> tempPath;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            labyrinth = new int[rows, cols];
            solutions = new List<string>();
            tempPath = new List<char>();

            FillLabyrinth(rows, cols);

            FindAllPaths(0, 0, '\0');

            Console.WriteLine(string.Join(Environment.NewLine, solutions));
        }

        private static void FindAllPaths(int row, int col, char direction)
        {
            if (IsOutside(row, col) ||
                IsWall(row, col) ||
                IsViseted(row, col))
            {
                return;
            }

            tempPath.Add(direction);

            if (IsSolution(row, col))
            {
                solutions.Add(string.Join("", tempPath));

                tempPath.RemoveAt(tempPath.Count - 1);

                return;
            }

            labyrinth[row, col] = visitedCell;

            FindAllPaths(row - 1, col, upCell);
            FindAllPaths(row + 1, col, downCell);
            FindAllPaths(row, col + 1, rightCell);
            FindAllPaths(row, col - 1, leftCell);

            tempPath.RemoveAt(tempPath.Count - 1);

            labyrinth[row, col] = freeCell;
        }

        private static bool IsSolution(int row, int col)
        {
            return labyrinth[row, col] == exitCell;
        }

        private static bool IsViseted(int row, int col)
        {
            return labyrinth[row, col] == visitedCell;
        }

        private static bool IsWall(int row, int col)
        {
            return labyrinth[row, col] == wallCell;
        }

        private static bool IsOutside(int row, int col)
        {
            return row < 0 ||
                    row >= labyrinth.GetLength(0) ||
                    col < 0 ||
                    col >= labyrinth.GetLength(1);
        }

        private static void FillLabyrinth(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine().ToCharArray();

                for (int j = 0; j < cols; j++)
                {
                    labyrinth[i, j] = line[j];
                }
            }
        }
    }
}
