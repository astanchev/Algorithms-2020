namespace _08_Set_Cover_Greedy_Approach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SetCoverGreedy
    {
        static void Main(string[] args)
        {
            List<int> universe = Console.ReadLine().Split(", ").Select(int.Parse).ToList();
            int numSets = int.Parse(Console.ReadLine());

            List<int[]> sets = new List<int[]>();
            List<int[]> selectedSets = new List<int[]>();

            FillSets(numSets, sets);
            
            FindSolution(universe, sets, selectedSets);

            PrintSolution(selectedSets);

        }

        private static void PrintSolution(List<int[]> selectedSets)
        {
            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
            foreach (var set in selectedSets)
            {
                Console.WriteLine(string.Join(", ", set));
            }
        }

        private static void FindSolution(List<int> universe, List<int[]> sets, List<int[]> selectedSets)
        {
            while (universe.Count > 0)
            {
                int[] currentSet = sets
                    .OrderByDescending(
                                    s => s.Count(
                                        e => universe.Contains(e)))
                    .FirstOrDefault();

                selectedSets.Add(currentSet);
                sets.Remove(currentSet);

                foreach (var e in currentSet)
                {
                    universe.Remove(e);
                }
            }
        }

        private static void FillSets(int numSets, List<int[]> sets)
        {
            for (int i = 0; i < numSets; i++)
            {
                int[] currentSet = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                sets.Add(currentSet);
            }
        }
    }
}
