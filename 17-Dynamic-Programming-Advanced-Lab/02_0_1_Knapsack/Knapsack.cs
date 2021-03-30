namespace _02_0_1_Knapsack
{
    using System;
    using System.Collections.Generic;

    public class Item
    {
        public Item(string name, int weight, int value)
        {
            this.Name = name;
            this.Weight = weight;
            this.Value = value;
        }

        public string Name { get; set; }

        public int Value { get; set; }

        public int Weight { get; set; }
    }

    public class Knapsack
    {
        private static List<Item> items;
        private static SortedSet<string> selectedItems;
        private static int[,] tablePrices;
        private static int usedWeight = 0;
        private static int totalValue = 0;

        public static void Main(string[] args)
        {
            int maxCapacity = int.Parse(Console.ReadLine());
            selectedItems = new SortedSet<string>();
            items = new List<Item>();

            ReadInput();

            tablePrices = new int[items.Count + 1, maxCapacity + 1];

            FillTablePrices();

            FindSelectedItems();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine($"Total Weight: {usedWeight}");
            Console.WriteLine($"Total Value: {totalValue}");
            Console.WriteLine(string.Join(Environment.NewLine, selectedItems));
        }

        private static void FindSelectedItems()
        {
            var row = tablePrices.GetLength(0) - 1;
            var col = tablePrices.GetLength(1) - 1;

            while (row > 0 && col > 0)
            {
                if (tablePrices[row, col] != tablePrices[row - 1, col])
                {
                    var selectedItem = items[row - 1];

                    selectedItems.Add(selectedItem.Name);
                    usedWeight += selectedItem.Weight;
                    totalValue += selectedItem.Value;

                    col -= selectedItem.Weight;
                }

                row -= 1;
            }
        }

        private static void FillTablePrices()
        {
            for (int itemIndex = 1; itemIndex < tablePrices.GetLength(0); itemIndex++)
            {
                var currentItem = items[itemIndex - 1];

                for (int capacity = 1; capacity < tablePrices.GetLength(1); capacity++)
                {
                    var excluded = tablePrices[itemIndex - 1, capacity];

                    if (capacity < currentItem.Weight)
                    {
                        tablePrices[itemIndex, capacity] = excluded;
                    }
                    else
                    {
                        var included = currentItem.Value + tablePrices[itemIndex - 1, capacity - currentItem.Weight];

                        tablePrices[itemIndex, capacity] = Math.Max(excluded, included);
                    }
                }
            }
        }

        private static void ReadInput()
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "end")
                {
                    break;
                }

                var lineParts = line.Split();

                var item = new Item(
                                lineParts[0],
                                int.Parse(lineParts[1]),
                                int.Parse(lineParts[2]));

                items.Add(item);
            }
        }
    }
}
