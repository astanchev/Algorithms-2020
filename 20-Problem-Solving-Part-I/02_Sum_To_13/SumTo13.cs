namespace _02_Sum_To_13
{
    using System;
    using System.Linq;

    class SumTo13
    {
        static void Main(string[] args)
        {
            const int targetNumber = 13;

            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int a = numbers[0];
            int b = numbers[1];
            int c = numbers[2];

            if (a + b + c == targetNumber ||
                a + b - c == targetNumber ||
                a - b + c == targetNumber ||
                a - b - c == targetNumber ||
                -a + b + c == targetNumber ||
                -a - b + c == targetNumber ||
                -a + b - c == targetNumber ||
                -a - b - c == targetNumber)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
