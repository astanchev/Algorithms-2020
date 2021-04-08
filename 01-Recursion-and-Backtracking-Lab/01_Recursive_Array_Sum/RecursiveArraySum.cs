namespace _01_Recursive_Array_Sum
{
    using System;
    using System.Linq;

    class RecursiveArraySum
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                        .Split(' ')
                        .Select(int.Parse)
                        .ToArray();

            Console.WriteLine(PrintRecursionSum(arr, 0));
        }

        private static int PrintRecursionSum(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return 0;
            }

            return arr[index] + PrintRecursionSum(arr, index + 1);
        }
    }
}
