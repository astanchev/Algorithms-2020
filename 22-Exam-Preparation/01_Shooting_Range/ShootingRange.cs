﻿namespace _01_Shooting_Range
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShootingRange
    {
        public static void Main(string[] args)
        {
            int[] values = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();
            bool[] marked = new bool[values.Length];

            int target = int.Parse(Console.ReadLine());

            GenerateSequences(0, target, values, marked);
        }

        private static void GenerateSequences(int index, int target, int[] values, bool[] marked)
        {
            int score = GetScore(values, marked);

            if (score == target)
            {
                Print(values, marked);
            }

            if (index >= values.Length || score >= target)
            {
                return;
            }

            HashSet<int> swapped = new HashSet<int>();
            for (int i = index; i < values.Length; i++)
            {
                if (!swapped.Contains(values[i]))
                {
                    Swap(index, i, values);
                    marked[index] = true;

                    GenerateSequences(index + 1, target, values, marked);

                    Swap(index, i, values);
                    marked[index] = false;

                    swapped.Add(values[i]);
                }
            }
        }

        private static void Swap(int i, int j, int[] values)
        {
            var temp = values[i];
            values[i] = values[j];
            values[j] = temp;
        }

        private static void Print(int[] values, bool[] marked)
        {
            var result = values.Where((v, i) => marked[i] == true);

            Console.WriteLine(string.Join(" ", result));
        }

        private static int GetScore(int[] values, bool[] marked)
        {
            int score = 0;
            int multiplier = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (marked[i])
                {
                    multiplier++;
                    score += values[i] * multiplier;
                }
            }

            return score;
        }
    }
}
