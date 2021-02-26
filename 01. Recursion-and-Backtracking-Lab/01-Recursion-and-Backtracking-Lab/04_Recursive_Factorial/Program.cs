namespace _04_Recursive_Factorial
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintFactorial(n);
        }

        private static void PrintFactorial(int n)
        {
            Console.WriteLine(Factorial(n));
        }

        private static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * Factorial(n-1);
        }
    }
}
