namespace _08_N_Choose_K
{
    using System;

    public class NChooseK
    {
        public static void Main(string[] args)
        {
            var set = new int[] { 1, 2, 3, 4 };
            var vector = new int[2];

            GenCombs(set, vector, 0, -1);
        }

        private static void GenCombs(int[] set, int[] vector, int index, int border)
        {
            if (index >= vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
                return;
            }
            else
            {                
                for (int i = border + 1; i < set.Length; i++)
                {
                    vector[index] = set[i];
                    GenCombs(set, vector, index + 1, i);
                }
            }
        }
    }

}
