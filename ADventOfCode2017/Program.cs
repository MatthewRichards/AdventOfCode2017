using System;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new Day23Part1();

            int answer = solver.Solve();

            Console.WriteLine($"Solution: {answer}");
            Console.In.ReadLine();
        }
    }
}
