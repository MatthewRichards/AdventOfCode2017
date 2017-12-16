using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day14Part2 : ISolution
    {
        public int Solve()
        {
            // grid[row][col] = false for empty, true for used
            bool[][] grid = new bool[128][];

            // Calculate the hashes and fill in the grid
            for (int row = 0; row < 128; row++)
            {
                var rowHash = (Input + "-" + row).CalculateKnotHash();
                var rowBits = rowHash.SelectMany(HexToBits);
                grid[row] = rowBits.ToArray();

            }

            // Now we have the complete grid; we need to count the regions
            // regions[row][col] indicates which region we've put grid[row][col] in so far
            // We'll assign regions arbitrarily, and then merge them together where we can, and count the number of results
            // Time will tell whether this a good idea
            int[][] regions = new int[128][];
            int nextRegion = 1;

            for(int i=0;i<128;i++)
            {
                regions[i] = new int[128];
                for(int j=0;j<128;j++)
                {
                    if (grid[i][j]) regions[i][j] = nextRegion++;
                }
            }

            bool didSomeMerging = true;
            while (didSomeMerging)
            {
                didSomeMerging = false;

                // Merge adjacent regions together repeatedly until there's nothing more to do
                for (int i = 0; i < 128; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        if (grid[i][j])
                        {
                            if (i < 127 && grid[i + 1][j] && (regions[i][j] != regions[i + 1][j]))
                            {
                                MergeRegions(regions, regions[i][j], regions[i + 1][j]);
                                didSomeMerging = true;
                            }

                            if (j < 127 && grid[i][j + 1] && (regions[i][j] != regions[i][j + 1]))
                            {
                                MergeRegions(regions, regions[i][j], regions[i][j + 1]);
                                didSomeMerging = true;

                            }
                        }
                    }
                }
            }
            return regions.SelectMany(row => row).Distinct().Where(region => region!= 0).Count();

        }

        private void MergeRegions(int[][] regions, int region1, int region2)
        {
            if (region1==0 || region2==0)
            {
                throw new InvalidOperationException("Oops, merging with the non-region...");
            }
            for(int i=0;i<128;i++)
            {
                for(int j=0;j<128;j++)
                {
                    if (regions[i][j] == region2) regions[i][j] = region1;
                }
            }
        }

        private static string HexStringToBitString(string hexString)
        {
            return string.Join("", hexString.SelectMany(HexToBits).Select(bit => bit ? "1" : "0").ToArray());
        }

        private static bool[] HexToBits(char hexChar)
        {
            var intValue = int.Parse(hexChar + "", System.Globalization.NumberStyles.HexNumber);

            var ret = new bool[4];
            for (int i=3;i>=0;i--)
            {
                if (intValue % 2 != 0) ret[i] = true;

                intValue /= 2;
            }

            return ret;
        }
        
        private static string TestInput = @"flqrgnkx";
        private static string Input = @"ljoxqyyw";
    }
}