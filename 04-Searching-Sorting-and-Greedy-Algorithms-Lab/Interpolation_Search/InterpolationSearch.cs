namespace Interpolation_Search
{
    using System;
    using System.Linq;

    class InterpolationSearch
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var searchedNumber = int.Parse(Console.ReadLine());

            var index = Search(numbers, searchedNumber, 0, numbers.Length - 1);

            Console.WriteLine(index);
        }

        private static object Search(int[] numbers, int searchedNumber, int start, int end)
        {
            while (numbers[start] <= searchedNumber && numbers[end] >= searchedNumber)
            {
                int mid = start + (searchedNumber - numbers[start]) * 
                                    (end - start) / 
                                    (numbers[end] - numbers[start]);

                if (numbers[mid] < searchedNumber)
                {
                    start = mid + 1;
                }
                else if (numbers[mid] > searchedNumber)
                {
                    end = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            if (numbers[start] == searchedNumber)
            {
                return start;
            }
            else
            {
                return -1;
            }
        }
    }
}
