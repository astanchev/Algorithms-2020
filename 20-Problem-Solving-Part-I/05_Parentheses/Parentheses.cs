namespace _05_Parentheses
{
    using System;
    using System.Text;

    class Parentheses
    {
        private static StringBuilder Result = new StringBuilder();

        static void Main(string[] args)
        {
            var pairs = int.Parse(Console.ReadLine());

            var output = new char[2 * pairs];

            output[0] = '(';
            output[1] = ')';

            BracketsBuilder(output, 1, pairs - 1, pairs, pairs);

            Console.Write(Result.ToString());
        }

        private static void BracketsBuilder(char[] output, int index, int open, int close, int pairs)
        {
            while (true)
            {
                if (index == 2 * pairs)
                {
                    Result.AppendLine(string.Join("", output));

                    return;
                }

                if (open != 0)
                {
                    output[index] = '(';

                    BracketsBuilder(output, index + 1, open - 1, close, pairs);
                }

                if (close == 0 || pairs - close + 1 > pairs - open)
                {
                    return;
                }

                output[index] = ')';

                index = index + 1;
                close = close - 1;
            }
        }
    }
}
