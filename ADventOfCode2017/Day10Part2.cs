using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day10Part2 : ISolution
    {
        public int Solve()
        {
            var list = Enumerable.Range(0, 256).ToArray();
            int currentPosition = 0;
            int skipSize = 0;
            var lengths = Input.Select(c => (int)c).Concat(new int[] { 17, 31, 73, 47, 23 });

            for (int round = 0; round < 64; round++)
            {
                foreach (var length in lengths)
                {
                    Reverse(list, currentPosition, length);

                    currentPosition += length + skipSize;
                    skipSize++;
                    while (currentPosition >= list.Length) currentPosition -= list.Length;

                }
            }

            var sparseHash = list;
            /* Test the xoring algorithm
            sparseHash[0] = 65;
            sparseHash[1] = 27;
            sparseHash[2] = 9;
            sparseHash[3] = 1;
            sparseHash[4] = 4;
            sparseHash[5] = 3;
            sparseHash[6] = 40;
            sparseHash[7] = 50;
            sparseHash[8] = 91;
            sparseHash[9] = 7;
            sparseHash[10] = 6;
            sparseHash[11] = 0;
            sparseHash[12] = 2;
            sparseHash[13] = 5;
            sparseHash[14] = 68;
            sparseHash[15] = 22;
            */
            var denseHash = new int[sparseHash.Length / 16];

            for (int i =0; i<denseHash.Length;i++)
            {
                var hashValue = sparseHash[i * 16];
                for (int j=1;j<16;j++)
                {
                    hashValue = hashValue ^ sparseHash[i * 16 + j];
                }

                denseHash[i] = hashValue;
            }

            var result = string.Join("", denseHash.SelectMany(v => v.ToString("X2")).ToArray());

            Console.WriteLine(result); // Another whoopsie on the int return type of my interface
            return 42;

        }

        private void Reverse(int[] list, int start, int length)
        {
            for(int i=0;i<length/2;i++)
            {
                var oneEnd = start + i;
                while (oneEnd >= list.Length) oneEnd -= list.Length;
                var otherEnd = start + length-1-i;
                while (otherEnd >= list.Length) otherEnd -= list.Length;

                var swap = list[oneEnd];
                list[oneEnd] = list[otherEnd];
                list[otherEnd] = swap;
            }
        }

        private static string TestInput = @"AoC 2017";
        private static string Input = @"206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3";
    }
}