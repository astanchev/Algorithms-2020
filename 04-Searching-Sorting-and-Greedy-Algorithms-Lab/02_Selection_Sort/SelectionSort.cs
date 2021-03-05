namespace _02_Selection_Sort
{
    using System;
    using System.Linq;

    class SelectionSort
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Sort(numbers, 0, numbers.Length - 1);

            Console.WriteLine();

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void Sort(int[] numbers, int v1, int v2)
        {
            for (int index = 0; index < numbers.Length; index++)
            {
                var min = index;

                for (int curr = index + 1; curr < numbers.Length; curr++)
                {
                    if (numbers[curr] < numbers[min])
                    {
                        min = curr;
                    }
                }

                Swap(numbers, index, min);
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
