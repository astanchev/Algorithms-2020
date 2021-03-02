namespace _06_Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Cinema
    {
        private static List<string> initialInput;
        private static string[] arrangedSeats;
        private static HashSet<int> lockedSeats;
        private static List<string> result;

        static void Main(string[] args)
        {
            lockedSeats = new HashSet<int>();
            result = new List<string>();

            FillInputData();

            PermuteRestNames(0);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void FillInputData()
        {
            initialInput = Console.ReadLine().Split(", ").ToList();
            arrangedSeats = new string[initialInput.Count];

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "generate")
                {
                    return;
                }

                string name = input.Split(" - ")[0];
                int seat = int.Parse(input.Split(" - ")[1]);

                lockedSeats.Add(seat);
                arrangedSeats[seat - 1] = name;
                initialInput.Remove(name);
            }
        }

        private static void PermuteRestNames(int permutationsIndex)
        {
            if (permutationsIndex >= initialInput.Count)
            {
                int initialInputIdx = 0;

                for (int i = 0; i < arrangedSeats.Length; i++)
                {
                    if (!lockedSeats.Contains(i + 1))
                    {
                        arrangedSeats[i] = initialInput[initialInputIdx++];
                    }
                }

                result.Add(string.Join(" ", arrangedSeats));
                return;
            }

            PermuteRestNames(permutationsIndex + 1);

            for (int elementsIndex = permutationsIndex + 1; elementsIndex < initialInput.Count; elementsIndex++)
            {
                Swap(permutationsIndex, elementsIndex);
                PermuteRestNames(permutationsIndex + 1);
                Swap(permutationsIndex, elementsIndex);
            }
        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            var temp = initialInput[firstIndex];
            initialInput[firstIndex] = initialInput[secondIndex];
            initialInput[secondIndex] = temp;
        }
    }
}
