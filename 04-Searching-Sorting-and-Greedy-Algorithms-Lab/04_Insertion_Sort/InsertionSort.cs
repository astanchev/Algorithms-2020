namespace _04_Insertion_Sort
{
    using System;
    using System.Linq;

    class InsertionSort
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Sort(numbers);

            Console.WriteLine();

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void Sort(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                int j = i;

                while (j > 0 && numbers[j] < numbers[j - 1])
                {
                    Swap(numbers, j, j - 1);
                    j--;
                }
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
