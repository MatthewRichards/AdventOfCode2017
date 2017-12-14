using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day14Part1 : ISolution
    {
        public int Solve()
        {
            Console.WriteLine(HexStringToBitString("a0c2017"));
            int squaresUsed = 0;//1010 0000 1100 0010 0000 0001 0111
                                //a    0    c    2    0    1    7....
                                //1010 0000 1100 0010 0000 0001 0111 0000....

            for (int row = 0; row < 128; row++)
            {
                var rowHash = (Input + "-" + row).CalculateKnotHash();
                var rowBits = rowHash.SelectMany(HexToBits);

                if(rowBits.Count()!= 128)
                {
                    throw new InvalidOperationException("Oops, knot hash was the wrong length!");
                }

                /*
                foreach(bool bit in rowBits.Take(8))
                {
                    Console.Write(bit ? "#" : ".");
                }
                Console.WriteLine();
            */
                squaresUsed += rowBits.Sum(bit => bit ? 1 : 0);
            }

            return squaresUsed;

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