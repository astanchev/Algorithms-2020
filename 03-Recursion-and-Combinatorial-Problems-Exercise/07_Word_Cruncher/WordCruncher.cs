namespace _07_Word_Cruncher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WordCruncher
    {
        private static Dictionary<int, List<string>> wordsByIndex;
        private static List<string> selectedWords;
        private static HashSet<string> results;

        private static string target;
        private static string current;

        public static void Main()
        {
            var words = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            target = Console.ReadLine();

            selectedWords = new List<string>();
            wordsByIndex = new Dictionary<int, List<string>>();
            results = new HashSet<string>();

            foreach (var word in words)
            {
                if (!target.Contains(word))
                {
                    continue;
                }

                var index = target.IndexOf(word);

                while (index >= 0)
                {
                    if (!wordsByIndex.ContainsKey(index))
                    {
                        wordsByIndex.Add(index, new List<string>());
                    }

                    wordsByIndex[index].Add(word);

                    index = target.IndexOf(word, index + 1);
                }
            }

            current = string.Empty;

            GenSolutions(0, words);

            Console.WriteLine(string.Join(Environment.NewLine, results));
        }

        private static void GenSolutions(int startIndex, List<string> words)
        {
            if (startIndex >= target.Length)
            {
                results.Add(string.Join(" ", selectedWords));

                return;
            }

            if (!wordsByIndex.ContainsKey(startIndex))
            {
                return;
            }

            foreach (var word in wordsByIndex[startIndex])
            {
                if (words.Contains(word))
                {
                    current += word;
                    selectedWords.Add(word);
                    words.Remove(word);

                    GenSolutions(current.Length, words);

                    selectedWords.RemoveAt(selectedWords.Count - 1);
                    current = current.Remove(current.Length - word.Length);
                    words.Add(word);
                }
            }
        }
    }
}
