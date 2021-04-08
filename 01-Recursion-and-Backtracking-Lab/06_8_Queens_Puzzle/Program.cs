namespace _06_8_Queens_Puzzle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Program
    {
        private static bool[,] board;
        private static int boardDimension = 8;
        private static HashSet<int> atackedRows;
        private static HashSet<int> atackedCols;
        private static HashSet<int> atackedLeftDiagonals;
        private static HashSet<int> atackedRightDiagonals;
        private static List<string> solutions;

        static void Main(string[] args)
        {
            board = new bool[boardDimension, boardDimension];
            atackedRows = new HashSet<int>();
            atackedCols = new HashSet<int>();
            atackedLeftDiagonals = new HashSet<int>();
            atackedRightDiagonals = new HashSet<int>();
            solutions = new List<string>();

            PutQueens(0);

            PrintSolutions();
        }

        private static void PrintSolutions()
        {
            Console.WriteLine(string.Join('\n', solutions));
        }

        private static void PutQueens(int row)
        {
            if (row == board.GetLength(0))
            {
                AddSolution();
                return;
            }

            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (!IsAttacked(row, col))
                {
                    board[row, col] = true;
                    atackedRows.Add(row);
                    atackedCols.Add(col);
                    atackedLeftDiagonals.Add(row - col);
                    atackedRightDiagonals.Add(row + col);

                    PutQueens(row + 1);

                    board[row, col] = false;
                    atackedRows.Remove(row);
                    atackedCols.Remove(col);
                    atackedLeftDiagonals.Remove(row - col);
                    atackedRightDiagonals.Remove(row + col);
                }
            }
        }

        private static bool IsAttacked(int row, int col)
        {
            return atackedRows.Contains(row) ||
                    atackedCols.Contains(col) ||
                    atackedLeftDiagonals.Contains(row - col) ||
                    atackedRightDiagonals.Contains(row + col);
        }

        private static void AddSolution()
        {
            var sb = new StringBuilder();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (col > 0)
                    {
                        sb.Append(' ');
                    }

                    if (board[row, col] == true)
                    {
                        sb.Append('*');
                    }
                    else
                    {
                        sb.Append('-');
                    }
                }

                sb.AppendLine();
            }

            solutions.Add(sb.ToString());
        }
    }
}
