namespace _06_Merge_Sort
{
    using System;
    using System.Linq;

    class MergeSort
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var sorted = Sort2(numbers);

            Console.WriteLine();

            Console.WriteLine(string.Join(" ", sorted));
        }

        private static int[] Sort(int[] numbers)
        {
            if (numbers.Length == 1)
            {
                return numbers;
            }

            int midIdx = numbers.Length / 2;
            var leftHalf = numbers.Take(midIdx).ToArray();
            var rightHalf = numbers.Skip(midIdx).ToArray();

            return MergeArrays(Sort(leftHalf), Sort(rightHalf));
        }

        private static int[] MergeArrays(int[] leftHalf, int[] rightHalf)
        {
            int[] merged = new int[leftHalf.Length + rightHalf.Length];

            int mergedIdx = 0;
            int leftIdx = 0;
            int rightIdx = 0;

            while (leftIdx < leftHalf.Length && rightIdx < rightHalf.Length)
            {
                if (leftHalf[leftIdx] < rightHalf[rightIdx])
                {
                    merged[mergedIdx] = leftHalf[leftIdx];
                    leftIdx++;
                }
                else
                {
                    merged[mergedIdx] = rightHalf[rightIdx];
                    rightIdx++;
                }

                mergedIdx++;
            }

            while (leftIdx < leftHalf.Length)
            {
                merged[mergedIdx] = leftHalf[leftIdx];
                leftIdx++;
                mergedIdx++;
            }

            while (rightIdx < rightHalf.Length)
            {
                merged[mergedIdx] = rightHalf[rightIdx];
                rightIdx++;
                mergedIdx++;
            }

            return merged;
        }

        public static int[] Sort2(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            var copy = new int[array.Length];
            Array.Copy(array, copy, array.Length);

            MergeSortHelper(array, copy, 0, array.Length - 1);

            return array;
        }

        public static void MergeSortHelper(int[] source, int[] copy, int leftIdx, int rightIdx)
        {
            if (leftIdx >= rightIdx)
            {
                return;
            }

            var middleIdx = (leftIdx + rightIdx) / 2;

            MergeSortHelper(copy, source, leftIdx, middleIdx);
            MergeSortHelper(copy, source, middleIdx + 1, rightIdx);

            MergeArrays2(source, copy, leftIdx, middleIdx, rightIdx);
        }

        public static void MergeArrays2(int[] source, int[] copy, int startIdx, int middleIdx, int endIdx)
        {
            var sourceIdx = startIdx;
            var leftIdx = startIdx; var rightIdx = middleIdx + 1;

            while (leftIdx <= middleIdx && rightIdx <= endIdx)
            {
                if (copy[leftIdx] < copy[rightIdx])
                    source[sourceIdx++] = copy[leftIdx++];
                else
                    source[sourceIdx++] = copy[rightIdx++];
            }

            while (leftIdx <= middleIdx)
            {
                source[sourceIdx] = copy[leftIdx];
                leftIdx += 1;
                sourceIdx += 1;
            }

            while (rightIdx <= endIdx)
            {
                source[sourceIdx] = copy[rightIdx];
                rightIdx += 1;
                sourceIdx += 1;
            }

        }

    }
}
