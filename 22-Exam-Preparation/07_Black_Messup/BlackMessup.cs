namespace _07_Black_Messup
{
    using System;
    using System.Collections.Generic;

    class Atom : IComparable<Atom>
    {
        public string Name { get; set; }

        public int Mass { get; set; }

        public int Decay { get; set; }

        public int CompareTo(Atom other)
        {
            return other.Mass.CompareTo(this.Mass);
        }
    }

    public class BlackMessup
    {
        static Dictionary<string, HashSet<string>> graph = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, Atom> atoms = new Dictionary<string, Atom>();

        public static void Main()
        {
            var totalAtoms = int.Parse(Console.ReadLine());
            var totalConnections = int.Parse(Console.ReadLine());

            FillGraph(totalAtoms);

            AddAtomConnections(totalConnections);

            var molecules = FindConnectedComponents();

            var valueBestMolecule = FindBestMoleculeValue(molecules);

            Console.WriteLine(valueBestMolecule);
        }

        static int FindBestMoleculeValue(List<SortedSet<Atom>> molecules)
        {
            var max = 0;

            foreach (var molecule in molecules)
            {
                var score = GetValue(molecule);

                if (score > max)
                {
                    max = score;
                }
            }

            return max;
        }

        private static void AddAtomConnections(int totalConnections)
        {
            for (int i = 0; i < totalConnections; i++)
            {
                var args = Console.ReadLine().Split();
                var from = args[0];
                var to = args[1];

                graph[from].Add(to);
                graph[to].Add(from);
            }
        }

        private static void FillGraph(int totalAtoms)
        {
            for (int i = 0; i < totalAtoms; i++)
            {
                var atomArgs = Console.ReadLine().Split();

                var atom = new Atom
                {
                    Name = atomArgs[0],
                    Mass = int.Parse(atomArgs[1]),
                    Decay = int.Parse(atomArgs[2]),
                };

                atoms.Add(atom.Name, atom);
                graph[atom.Name] = new HashSet<string>();
            }
        }

        private static int GetValue(SortedSet<Atom> molecule)
        {
            var maxDecay = 1;
            var score = 0;
            var count = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    maxDecay = atom.Decay;
                    score += atom.Mass;
                    count++;
                }
                else if (maxDecay > count)
                {
                    score += atom.Mass;
                    count++;
                }
            }

            return score;
        }

        static List<SortedSet<Atom>> FindConnectedComponents()
        {
            //for every molecule which atoms are included
            var molecules = new List<SortedSet<Atom>>();
            var visited = new HashSet<string>();
            var index = 0;

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    molecules.Add(new SortedSet<Atom>());
                    Dfs(node, node, visited, molecules, index);
                    index++;
                }
            }

            return molecules;
        }

        static void Dfs(
                        string node, 
                        string parent, 
                        HashSet<string> visited, 
                        List<SortedSet<Atom>> molecules, 
                        int index)
        {
            visited.Add(node);
            molecules[index].Add(atoms[node]);

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child) && child != parent)
                {
                    Dfs(child, node, visited, molecules, index);
                }
            }
        }
    }
}
