namespace _02_Travelling_Policeman
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TravellingPoliceman
    {
        private static StringBuilder builder = new StringBuilder();
        private static List<Item> items;

        public static void Main(string[] args)
        {
            int fuel = int.Parse(Console.ReadLine());

            items = new List<Item>();

            ReadInput();

            List<Item> total = new List<Item>();
            total = Fill(fuel);

            total.Reverse();

            builder.AppendLine(String.Join(" -> ", total.Select(i => i.Name)))
                .AppendLine($"Total pokemons caught -> {total.Sum(i => i.Count)}")
                .AppendLine($"Total car damage -> {total.Sum(i => i.Damage)}")
                .Append($"Fuel Left -> {fuel - total.Sum(i => i.Length)}");

            Console.WriteLine(builder.ToString());
        }

        private static List<Item> Fill(int fuel)
        {
            int[,] values = new int[items.Count + 1, fuel + 1];
            bool[,] isItemIncluded = new bool[items.Count + 1, fuel + 1];
            List<Item> itemsTaken = new List<Item>();

            for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                int row = itemIndex + 1;
                Item item = items[itemIndex];

                for (int c = 1; c < fuel; c++)
                {
                    int excludedValue = values[row - 1, c];
                    int includedValue = 0;

                    if (item.Length <= c)
                    {
                        includedValue = (item.Count * 10 - item.Damage) + values[row - 1, c - item.Length];
                    }

                    if (includedValue > excludedValue)
                    {
                        values[row, c] = includedValue;
                        isItemIncluded[row, c] = true;
                    }
                    else
                    {
                        values[row, c] = excludedValue;
                    }
                }
            }

            int tempCap = fuel;

            for (int i = items.Count - 1; i > 0; i--)
            {
                if (!isItemIncluded[i, tempCap])
                {
                    continue;
                }

                Item item = items[i - 1];
                itemsTaken.Add(item);

                tempCap = tempCap - item.Length;
            }

            return itemsTaken;
        }

        private static void ReadInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    break;
                }

                string[] tokens = input.Split(',').ToArray();

                Item item = new Item(tokens[0],
                                int.Parse(tokens[1]),
                                int.Parse(tokens[2]),
                                int.Parse(tokens[3]));

                items.Add(item);
            }
        }
    }
}
