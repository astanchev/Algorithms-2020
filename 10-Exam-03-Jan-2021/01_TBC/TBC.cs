namespace _01_TBC
{
    using System;
    using System.Collections.Generic;

    public class Node
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }


    public class TBC
    {
        private static char[,] matrix;
        private static bool[,] visited;
        private static int totalAreas = 0;
        private static char tunel = 't';

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new char[rows, cols];
            visited = new bool[rows, cols];

            ReadMatrix(rows, cols);

            FindAreas();

            Console.WriteLine(totalAreas);
        }

        private static void FindAreas()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (visited[row, col] || matrix[row, col] != tunel)
                    {
                        continue;
                    }

                    DFS(row, col);

                    totalAreas++;
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

            //Left/Up -> row - 1, col - 1
            if (IsInside(row - 1, col - 1) &&
                IsChild(row, col, row - 1, col - 1) &&
                !IsVisited(row - 1, col - 1))
            {
                children.Add(new Node { Row = row - 1, Col = col - 1 });
            }

            //Left/Down -> row + 1, col - 1
            if (IsInside(row + 1, col - 1) &&
                IsChild(row, col, row + 1, col - 1) &&
                !IsVisited(row + 1, col - 1))
            {
                children.Add(new Node { Row = row + 1, Col = col - 1 });
            }

            //Right/Up -> row - 1, col + 1
            if (IsInside(row - 1, col + 1) &&
                IsChild(row, col, row - 1, col + 1) &&
                !IsVisited(row - 1, col + 1))
            {
                children.Add(new Node { Row = row - 1, Col = col + 1 });
            }

            //Right/Down -> row + 1, col + 1
            if (IsInside(row + 1, col + 1) &&
                IsChild(row, col, row + 1, col + 1) &&
                !IsVisited(row + 1, col + 1))
            {
                children.Add(new Node { Row = row + 1, Col = col + 1 });
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
