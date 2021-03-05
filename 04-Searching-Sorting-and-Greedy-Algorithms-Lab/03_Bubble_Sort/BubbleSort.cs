namespace _03_Bubble_Sort
{
    using System;
    using System.Linq;

    class BubbleSort
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Sort2(numbers);

            Console.WriteLine();

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void Sort(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        Swap(numbers, j, j + 1);
                    }
                }
            }
        }

        private static void Sort2(int[] numbers)
        {
            var isSorted = false;
            var i = 0;

            while (!isSorted)
            {
                isSorted = true;
            
                for (int j = 1; j < numbers.Length - i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        isSorted = false;
                        Swap(numbers, j - 1, j);
                    }
                }

                i += 1;
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
