namespace _02_Areas_in_Matrix
{
    using System;
    using System.Collections.Generic;

    public class Node
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }


    class AreasInMatrix
    {
        private static char[,] matrix;
        private static bool[,] visited;
        private static SortedDictionary<char, int> areas;
        private static int totalAreas;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            totalAreas = 0;

            matrix = new char[rows, cols];
            visited = new bool[rows, cols];
            areas = new SortedDictionary<char, int>();

            ReadMatrix(rows, cols);

            FindAreas();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine($"Areas: {totalAreas}");

            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static void FindAreas()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (visited[row, col])
                    {
                        continue;
                    }

                    DFS(row, col);

                    totalAreas++;

                    var key = matrix[row, col];

                    if (!areas.ContainsKey(key))
                    {
                        areas[key] = 0;
                    }

                    areas[key]++;
                }
            }
        }

        private static void DFS(int row, int col)
        {
            visited[row, col] = true;
            var children = GetChildren(row, col);

            foreach (var child in children)
            {
                if (visited[child.Row, child.Col])
                {
                    continue;
                }

                DFS(child.Row, child.Col);
            }

        }

        private static List<Node> GetChildren(int row, int col)
        {
            var children = new List<Node>();

            //Up -> row - 1, col
            if (IsInside(row - 1, col) &&
                IsChild(row, col, row - 1, col) &&
                !IsVisited(row - 1, col))
            {
                children.Add(new Node { Row = row - 1, Col = col });
            }

            //Down -> row + 1, col
            if (IsInside(row + 1, col) &&
                IsChild(row, col, row + 1, col) &&
                !IsVisited(row + 1, col))
            {
                children.Add(new Node { Row = row + 1, Col = col });
            }

            //Right -> row, col + 1
            if (IsInside(row, col + 1) &&
                IsChild(row, col, row, col + 1) &&
                !IsVisited(row, col + 1))
            {
                children.Add(new Node { Row = row, Col = col + 1 });
            }

            //Left -> row, col - 1
            if (IsInside(row, col - 1) &&
                IsChild(row, col, row, col - 1) &&
                !IsVisited(row, col - 1))
            {
                children.Add(new Node { Row = row, Col = col - 1 });
            }

            return children;
        }

        private static bool IsVisited(int row, int col)
        {
            return visited[row, col];
        }

        private static bool IsChild(int parentRow, int parentCol, int childRow, int childCol)
        {
            return matrix[parentRow, parentCol] == matrix[childRow, childCol];
        }

        private static bool IsInside(int row, int col)
        {
            return row >= 0 &&
                    row < matrix.GetLength(0) &&
                    col >= 0 &&
                    col < matrix.GetLength(1);

        }

        private static void ReadMatrix(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                var line = Console.ReadLine();

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
    }
}
