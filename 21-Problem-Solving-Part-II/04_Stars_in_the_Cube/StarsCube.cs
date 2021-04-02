namespace _04_Stars_in_the_Cube
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StarsCube
    {
        private static char[,,] cube;
        private static SortedDictionary<char, int> stars;

        public static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());

            cube = new char[size, size, size];
            stars = new SortedDictionary<char, int>();

            FillCube(size);

            FindStars(size);

            PrintStarsCount();
        }

        private static void PrintStarsCount()
        {
            Console.WriteLine(stars.Values.Sum());

            foreach (var star in stars)
            {
                Console.WriteLine($"{star.Key} -> {star.Value}");
            }
        }

        private static void FindStars(int size)
        {
            for (var layer = 1; layer < size - 1; layer++)
            {
                for (var row = 1; row < size - 1; row++)
                {
                    for (var col = 1; col < size - 1; col++)
                    {
                        var charToMatch = cube[layer, row, col];

                        if (cube[layer - 1, row, col] != charToMatch ||
                            cube[layer + 1, row, col] != charToMatch ||
                            cube[layer, row + 1, col] != charToMatch ||
                            cube[layer, row - 1, col] != charToMatch ||
                            cube[layer, row, col + 1] != charToMatch ||
                            cube[layer, row, col - 1] != charToMatch)
                        {
                            continue;
                        }

                        if (!stars.ContainsKey(charToMatch))
                        {
                            stars.Add(charToMatch, 0);
                        }

                        stars[charToMatch]++;
                    }
                }
            }
        }

        private static void FillCube(int size)
        {
            for (var layer = 0; layer < size; layer++)
            {
                var rowPart = Console.ReadLine()
                    .Split('|')
                    .Select(x => x.Trim())
                    .ToArray();

                for (var row = 0; row < size; row++)
                {
                    var colPart = rowPart[row]
                        .Split()
                        .Select(x => x[0])
                        .ToArray();

                    for (var col = 0; col < size; col++)
                    {
                        cube[layer, row, col] = colPart[col];
                    }
                }
            }
        }
    }
}
