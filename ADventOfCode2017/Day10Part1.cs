using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day10Part1 : ISolution
    {
        public int Solve()
        {
            var list = Enumerable.Range(0, 256).ToArray();
            int currentPosition = 0;
            int skipSize = 0;
            var lengths = Input.Split(',').Select(int.Parse);

            foreach (var length in lengths)
            {
                Reverse(list, currentPosition, length);

                currentPosition += length + skipSize;
                skipSize++;
                while (currentPosition >= list.Length) currentPosition -= list.Length;

            }

            return list[0] * list[1];

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

        private static string TestInput = @"3, 4, 1, 5";
        private static string Input = @"206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3";
    }
}