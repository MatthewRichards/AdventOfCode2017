using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day6Part2 : ISolution
    {
        public int Solve()
        {
            var memory = Input.Split('\t').Select(int.Parse).ToArray();
            var seenConfigurations = new List<string>();


            int attempts = 0;
            string targetConfiguration = "";

            var memoryCopy = string.Join(" ", memory.Select(num => num.ToString()).ToArray());
            while (targetConfiguration != memoryCopy)
            {
                if (targetConfiguration == "" && seenConfigurations.Contains(memoryCopy))
                {
                    targetConfiguration = memoryCopy;
                }

                seenConfigurations.Add(memoryCopy);
                Reallocate(memory);
                if (targetConfiguration != "") attempts++;
                memoryCopy = string.Join(" ", memory.Select(num => num.ToString()).ToArray());
            }

            return attempts;
        }

        public void Reallocate(int[] memory)
        {
            int bankToRedistribute = Array.IndexOf(memory, memory.Max());
            int amountToRedistribute = memory[bankToRedistribute];
            memory[bankToRedistribute] = 0;

            int nextIndex = bankToRedistribute;
            while (amountToRedistribute > 0)
            {
                nextIndex++;
                if (nextIndex >= memory.Length) nextIndex = 0;

                memory[nextIndex]++;
                amountToRedistribute--;
            }

        }

        private static string TestInput = "0\t2\t7\t0";
        private static string Input = "2	8	8	5	4	2	3	1	5	5	1	2	15	13	5	14";
    }
}