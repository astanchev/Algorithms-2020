namespace _01_Reverse_Array
{
    using System;
    using System.Linq;

    class ReverseArray
    {
        private static int[] arr;
        private static int[] result;

        static void Main(string[] args)
        {
            arr = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            //result = new int[arr.Length];

            //FillResult(arr.Length - 1, 0);

            Reverse(0);

            Console.WriteLine(string.Join(' ', arr));
        }

        private static void Reverse(int startIndex)
        {
            if (startIndex >= arr.Length / 2)
            {
                return;
            }

            var endIndex = arr.Length - 1 - startIndex;

            Swap(startIndex, endIndex);

            Reverse(startIndex + 1);
        }

        private static void Swap(int startIndex, int endIndex)
        {
            var temp = arr[startIndex];
            arr[startIndex] = arr[endIndex];
            arr[endIndex] = temp;
        }

        private static void FillResult(int originalIdx, int resultIdx)
        {
            if (originalIdx < 0)
            {
                return;
            }

            result[resultIdx] = arr[originalIdx];

            FillResult(originalIdx - 1, resultIdx + 1);
        }
    }
}
