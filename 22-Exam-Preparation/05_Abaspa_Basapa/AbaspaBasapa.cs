namespace _05_Abaspa_Basapa
{
    using System;
    using System.Collections.Generic;

    public class AbaspaBasapa
    {
        public static void Main(string[] args)
        {
            var firstStr = Console.ReadLine();
            var secondStr = Console.ReadLine();

            var lcs = new int[firstStr.Length, secondStr.Length];
            var max = 0;
            var maxI = 0;
            var maxJ = 0;

            for (int i = 0; i < firstStr.Length; i++)
            {
                for (int j = 0; j < secondStr.Length; j++)
                {
                    if (firstStr[i] == secondStr[j])
                    {
                        var result = 1;

                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            result = lcs[i - 1, j - 1] + 1;
                        }

                        lcs[i, j] = result;
                    }
                    else
                    {
                        lcs[i, j] = 0;
                    }

                    if (lcs[i, j] > max)
                    {
                        max = lcs[i, j];
                        maxI = i;
                        maxJ = j;
                    }
                }
            }

            var finalResult = new Stack<char>();

            while (maxI >= 0 && maxJ >= 0 && max != 0)
            {
                finalResult.Push(firstStr[maxI]);
                maxI--;
                maxJ--;
                max--;
            }

            Console.WriteLine(string.Join(string.Empty, finalResult));
        }
    }
}
