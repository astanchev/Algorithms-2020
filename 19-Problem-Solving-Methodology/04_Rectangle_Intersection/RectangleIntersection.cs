namespace _04_Rectangle_Intersection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RectangleIntersection
    {
        private static List<Rectangle> rectangles;
        private static List<int> xCoordinates;
        private static List<Rectangle>[] rectanglesByXCoordinates;
        private static long result = 0;

        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            rectangles = new List<Rectangle>();
            xCoordinates = new List<int>();

            ReadInput(n);

            ArrangeRectsByX();

            for (int rectXIndex = 0; rectXIndex < rectanglesByXCoordinates.Count(); rectXIndex++)
            {
                if (rectanglesByXCoordinates[rectXIndex].Count() < 2)
                {
                    continue;
                }

                var yCoordinates = FillYCoordinates(rectXIndex);

                yCoordinates.Sort();
                var overlapped = FindOverlapped(rectXIndex, yCoordinates);

                for (int rectYIndex = 0; rectYIndex < overlapped.Count(); rectYIndex++)
                {
                    if (overlapped[rectYIndex] >= 2)
                    {
                        int xSide = xCoordinates[rectXIndex + 1] - xCoordinates[rectXIndex];
                        int ySide = yCoordinates[rectYIndex + 1] - yCoordinates[rectYIndex];

                        result += xSide * ySide;
                    }
                }
            }

            Console.WriteLine(result);
        }

        private static int[] FindOverlapped(int rectXIndex, List<int> yCoordinates)
        {
            int[] overlapped = new int[yCoordinates.Count - 1];

            foreach (var rectangle in rectanglesByXCoordinates[rectXIndex])
            {
                for (int rectYIndex = 0; rectYIndex < yCoordinates.Count; rectYIndex++)
                {
                    if (rectangle.MaxY <= yCoordinates[rectYIndex] || rectangle.MinY >= yCoordinates[rectYIndex + 1])
                    {
                        continue;
                    }
                    overlapped[rectYIndex]++;
                }
            }

            return overlapped;
        }

        private static List<int> FillYCoordinates(int rectIndex)
        {
            List<int> yCoordinates = new List<int>();

            foreach (var rectangle in rectanglesByXCoordinates[rectIndex])
            {
                yCoordinates.Add(rectangle.MinY);
                yCoordinates.Add(rectangle.MaxY);
            }

            return yCoordinates;
        }

        private static void ArrangeRectsByX()
        {
            xCoordinates.Sort();
            rectanglesByXCoordinates = new List<Rectangle>[xCoordinates.Count - 1];

            for (int i = 0; i < xCoordinates.Count - 1; i++)
            {
                rectanglesByXCoordinates[i] = new List<Rectangle>();
            }

            foreach (var rectangle in rectangles)
            {
                for (int i = 0; i < rectanglesByXCoordinates.Count(); i++)
                {
                    if (rectangle.MaxX > xCoordinates[i] && rectangle.MinX < xCoordinates[i + 1])
                    {
                        rectanglesByXCoordinates[i].Add(rectangle);
                    }
                }
            }
        }

        private static void ReadInput(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var coordinates = Console.ReadLine()
                                            .Split(' ')
                                            .Select(int.Parse)
                                            .ToArray();
                xCoordinates.Add(coordinates[0]);
                xCoordinates.Add(coordinates[1]);

                var rectangle = new Rectangle(coordinates[0], coordinates[1], coordinates[2], coordinates[3]);

                rectangles.Add(rectangle);
            }
        }
    }
}
