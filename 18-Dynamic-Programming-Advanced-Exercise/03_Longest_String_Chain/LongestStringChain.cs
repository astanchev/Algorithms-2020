namespace _03_Longest_String_Chain
{
    using System;
    using System.Collections.Generic;

    public class LongestStringChain
    {
        public static void Main(string[] args)
        {
            var words = Console.ReadLine().Split();

            var len = new int[words.Length];
            var prevs = new int[words.Length];

            var bestLen = 0;
            var lastIndex = 0;

            for (int current = 0; current < words.Length; current++)
            {
                len[current] = 1;
                prevs[current] = -1;

                var currWord = words[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevWord = words[prev];

                    if (prevWord.Length < currWord.Length &&
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

            var stack = new Stack<string>();

            while (lastIndex != -1)
            {
                stack.Push(words[lastIndex]);
                lastIndex = prevs[lastIndex];
            }

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}
