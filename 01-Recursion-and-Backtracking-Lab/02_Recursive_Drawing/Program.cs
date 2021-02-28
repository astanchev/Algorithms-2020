namespace _02_Recursive_Drawing
{
    using System;

    class Program
    {
        private static char firstChar = '*';
        private static char secondChar = '#';

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintFigure(n);
        }

        private static void PrintFigure(int n)
        {
            if (n == 0)
            {
                return;
            }

            Console.WriteLine(new string(firstChar, n));

            PrintFigure(n - 1);

            Console.WriteLine(new string(secondChar, n));
        }
    }
}
