using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day13Part2 : ISolution
    {
        public int Solve()
        {
            // Parse the input
            var inputLines = Input.SplitByNewLines().Select(line => line.Split(':'));
            var depthsAndRanges = inputLines.Select(line => Tuple.Create(int.Parse(line[0]), int.Parse(line[1])));

            var maxDepth = depthsAndRanges.Max(tup => tup.Item1);
            var rangeByDepth = new int[maxDepth + 1];
            var dangerousStarts = new HashSet<int>();

            foreach (var depthAndRange in depthsAndRanges)
            {
                rangeByDepth[depthAndRange.Item1] = depthAndRange.Item2;
            }


            int picoseconds = 0;

                var scannerLevels = new int[maxDepth + 1]; // Initially at position 0 i.e. the top of each layer
                var scannerDirection = new bool[maxDepth + 1]; // False means move down, which is the starting state


            while (picoseconds < 10000000) // Hmm, arbitrary large number...
            {
                picoseconds++;

                for (int d = 0; d < scannerLevels.Length; d++)
                {
                    if (rangeByDepth[d] == 0) continue; // No scanner on this level

                    scannerLevels[d] = scannerLevels[d] + (scannerDirection[d] ? -1 : 1);

                    if (scannerLevels[d] == rangeByDepth[d] - 1)
                    {
                        // Change direction at the bottom
                        scannerDirection[d] = true;
                    }
                    if (scannerLevels[d] == 0)
                    {
                        // Change direction at the top
                        // The next picosecond is a dangerous one to be entering this layer so don't do that!
                        dangerousStarts.Add(picoseconds-d);
                        scannerDirection[d] = false;
                    }


                }
            }

            return Enumerable.Range(0, dangerousStarts.Max()).Where(v => !dangerousStarts.Contains(v)).Min(); 
        }


        private static string TestInput = @"0: 3
1: 2
4: 4
6: 4";
        private static string Input = @"0: 3
1: 2
2: 6
4: 4
6: 4
8: 10
10: 6
12: 8
14: 5
16: 6
18: 8
20: 8
22: 12
24: 6
26: 9
28: 8
30: 8
32: 10
34: 12
36: 12
38: 8
40: 12
42: 12
44: 14
46: 12
48: 12
50: 12
52: 12
54: 14
56: 14
58: 14
60: 12
62: 14
64: 14
66: 17
68: 14
72: 18
74: 14
76: 20
78: 14
82: 18
86: 14
90: 18
92: 14";
    }
}