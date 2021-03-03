namespace _08_School_Teams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SchoolTeams
    {        
        private static int girlsComb = 3;
        private static int boysComb = 2;

        static void Main(string[] args)
        {
            var girlsResult = new List<string>();
            var boysResult = new List<string>();

            var girls = Console.ReadLine().Split(", "); 
            var boys = Console.ReadLine().Split(", "); 

            var girlsCombinations = new string[girlsComb];
            var boysCombinations = new string[boysComb];

            Combinations(0, 0, girlsCombinations, girls, girlsResult);
            Combinations(0, 0, boysCombinations, boys, boysResult);

            PrintResults(girlsResult, boysResult);
        }

        private static void PrintResults(List<string> girlsResult, List<string> boysResult)
        {
            foreach (var girlComb in girlsResult)
            {
                foreach (var boyComb in boysResult)
                {
                    Console.WriteLine(girlComb + ", " + boyComb);
                }
            }
        }

        private static void Combinations(
            int combIdx, 
            int elementStartIdx,  
            string[] combinations, 
            string[] elements, 
            List<string> res)
        {
            if (combIdx >= combinations.Length)
            {
                res.Add(string.Join(", ", combinations.ToArray()));
                return;
            }

            for (int i = elementStartIdx; i < elements.Length; i++)
            {
                combinations[combIdx] = elements[i];
                Combinations(combIdx + 1, i + 1, combinations, elements, res);
            }
        }
    }
}
