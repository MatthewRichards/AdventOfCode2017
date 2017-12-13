using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day13Part1 : ISolution
    {
        public int Solve()
        {
            // Parse the input
            var inputLines = Input.SplitByNewLines().Select(line => line.Split(':'));
            var depthsAndRanges = inputLines.Select(line => Tuple.Create(int.Parse(line[0]), int.Parse(line[1])));

            var maxDepth = depthsAndRanges.Max(tup => tup.Item1);
            var rangeByDepth = new int[maxDepth+1];

            foreach (var depthAndRange in depthsAndRanges)
            {
                rangeByDepth[depthAndRange.Item1] = depthAndRange.Item2;
            }


            // Simulate the security scanner
            var scannerLevels = new int[maxDepth + 1]; // Initially at position 0 i.e. the top of each layer
            var scannerDirection = new bool[maxDepth + 1]; // False means move down, which is the starting state

            int myDepth = -1; // My packet hasn't started moving yet
            int picosecond = -1; // So we start at zero
            int severity = 0;

            while(myDepth < maxDepth)
            {
                myDepth++;
                picosecond++;

                if (rangeByDepth[myDepth] != 0 && scannerLevels[myDepth] == 0)
                {
                    // Oops, caught!
                    Console.WriteLine($"Caught at depth {myDepth}");
                    severity += (myDepth * rangeByDepth[myDepth]);
                }

                // Now the scanners all move
                for(int d=0;d<scannerLevels.Length;d++)
                {
                    if (rangeByDepth[d] == 0) continue;

                    scannerLevels[d] = scannerLevels[d] + (scannerDirection[d] ? -1 : 1);

                    if (scannerLevels[d] == rangeByDepth[d]-1) scannerDirection[d] = true;
                    if (scannerLevels[d] == 0) scannerDirection[d] = false;
                }
            }



            return severity;
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