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
            var input = Input;
            string result = input.CalculateKnotHash();

            Console.WriteLine(result); // Another whoopsie on the int return type of my interface
            return 42;

        }


        private static string TestInput = @"AoC 2017";
        private static string Input = @"206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3";
    }
}