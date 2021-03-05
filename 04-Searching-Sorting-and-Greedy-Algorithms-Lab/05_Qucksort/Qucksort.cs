namespace _05_Qucksort
{
    using System;
    using System.Linq;

    class Qucksort
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Sort(numbers, 0, numbers.Length - 1);

            Console.WriteLine();

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void Sort(int[] numbers, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int pivot = start;
            int left = start + 1;
            int right = end;

            while (left <= right)
            {
                if (numbers[left] > numbers[pivot] && numbers[right] < numbers[pivot])
                {
                    Swap(numbers, left, right);
                }

                if (numbers[left] <= numbers[pivot])
                {
                    left++;
                }

                if (numbers[right] >= numbers[pivot])
                {
                    right--;
                }
            }

            Swap(numbers, pivot, right);

            bool isLeftSubArrSmaller = (right - 1) - start < end - (right + 1);

            if (isLeftSubArrSmaller)
            {
                Sort(numbers, start, right - 1);
                Sort(numbers, right + 1, end);
            }
            else
            {
                Sort(numbers, right + 1, end);
                Sort(numbers, start, right - 1);
            }
        }

        private static void Swap(int[] numbers, int firstIndex, int secondIndex)
        {
            int temp = numbers[firstIndex];
            numbers[firstIndex] = numbers[secondIndex];
            numbers[secondIndex] = temp;
        }
    }
}
