namespace _01_Binary_Search
{
    using System;
    using System.Linq;

    class BinarySearch
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var searchedNumber = int.Parse(Console.ReadLine());

            var index = BinarySearchRecursion(numbers, searchedNumber, 0, numbers.Length - 1);

            Console.WriteLine(index);
        }

        private static object BinarySearchIteration(int[] numbers, int searchedNumber, int start, int end)
        {
            while (end >= start)
            {
                int middle = (start + end) / 2;

                if (numbers[middle] > searchedNumber)
                {
                    return BinarySearchRecursion(numbers, searchedNumber, start, middle - 1);
                }
                else if (numbers[middle] < searchedNumber)
                {
                    return BinarySearchRecursion(numbers, searchedNumber, middle + 1, end);
                }
                else
                {
                    return middle;
                }
            }

            return -1;
        }

        private static object BinarySearchRecursion(int[] numbers, int searchedNumber, int start, int end)
        {
            if (end < start)
            {
                return -1;
            }

            int middle = (start + end) / 2;

            if (numbers[middle] > searchedNumber)
            {
                return BinarySearchRecursion(numbers, searchedNumber, start, middle - 1);
            }
            else if (numbers[middle] < searchedNumber)
            {
                return BinarySearchRecursion(numbers, searchedNumber, middle + 1, end);
            }
            else
            {
                return middle;
            }
        }
    }
}
