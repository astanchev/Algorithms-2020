namespace _05_Guitar
{
    using System;
    using System.Linq;

    public class Guitar
    {
        private static bool[,] volumesMatrix;

        public static void Main(string[] args)
        {
            var volumeChanges = Console.ReadLine()
                                        .Split(", ")
                                        .Select(int.Parse)
                                        .ToArray();

            var startVolume = int.Parse(Console.ReadLine());
            var maxVolume = int.Parse(Console.ReadLine());

            // row = song -> col = volume +/-  change interval (if is in [0, maxinterval])
            volumesMatrix = new bool[volumeChanges.Length + 1, maxVolume + 1];
            volumesMatrix[0, startVolume] = true;

            FillVolumeMatrix(volumeChanges, maxVolume);

            var finalSongMaxVolume = -1;

            for (var i = maxVolume; i >= 0; i--)
            {
                if (!volumesMatrix[volumeChanges.Length, i])
                {
                    continue;
                }

                finalSongMaxVolume = i;
                break;
            }

            Console.WriteLine(finalSongMaxVolume);
        }

        private static void FillVolumeMatrix(int[] volumeChanges, int maxVolume)
        {
            for (var i = 0; i < volumeChanges.Length; i++)
            {
                var changed = false;
                var changeVolume = volumeChanges[i];

                for (var j = 0; j <= maxVolume; j++)
                {
                    if (!volumesMatrix[i, j])
                    {
                        continue;
                    }

                    if (j + changeVolume <= maxVolume)
                    {
                        volumesMatrix[i + 1, j + changeVolume] = true;
                        changed = true;
                    }

                    if (j - changeVolume >= 0)
                    {
                        volumesMatrix[i + 1, j - changeVolume] = true;
                        changed = true;
                    }
                }

                if (!changed)
                {
                    break;
                }
            }
        }
    }
}
