namespace _02_Boxes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Box
    {
        public int Width { get; set; }

        public int Depth { get; set; }

        public int Height { get; set; }
    }

    public class Boxes
    {
        private static Box[] boxes;
        private static int[] len;
        private static int[] prevs;
        private static int bestLen = 0;
        private static int lastIndex = 0;

        public static void Main(string[] args)
        {
            var boxesCount = int.Parse(Console.ReadLine());

            boxes = new Box[boxesCount];

            FillBoxes(boxesCount);

            len = new int[boxes.Length];
            prevs = new int[boxes.Length];

            FindBestLen();

            Stack<Box> stack = FillStackBoxes();

            PrintStack(stack);
        }

        private static void PrintStack(Stack<Box> stack)
        {
            foreach (var box in stack)
            {
                Console.WriteLine($"{box.Width} {box.Depth} {box.Height}");
            }
        }

        private static Stack<Box> FillStackBoxes()
        {
            var stack = new Stack<Box>();

            while (lastIndex != -1)
            {
                stack.Push(boxes[lastIndex]);
                lastIndex = prevs[lastIndex];
            }

            return stack;
        }

        private static void FindBestLen()
        {
            for (int current = 0; current < boxes.Length; current++)
            {
                len[current] = 1;
                prevs[current] = -1;

                var currBox = boxes[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevBox = boxes[prev];

                    if (prevBox.Width < currBox.Width &&
                        prevBox.Depth < currBox.Depth &&
                        prevBox.Height < currBox.Height &&
                        len[prev] + 1 >= len[current])
                    {
                        len[current] = len[prev] + 1;
                        prevs[current] = prev;
                    }
                }

                if (len[current] > bestLen)
                {
                    bestLen = len[current];
                    lastIndex = current;
                }
            }
        }

        private static void FillBoxes(int boxesCount)
        {
            for (int i = 0; i < boxesCount; i++)
            {
                var boxData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var width = boxData[0];
                var depth = boxData[1];
                var height = boxData[2];

                var box = new Box
                {
                    Width = width,
                    Depth = depth,
                    Height = height
                };

                boxes[i] = box;
            }
        }
    }
}
