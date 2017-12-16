using System;

namespace AdventOfCode2017
{
    public class Day15Part1 : ISolution
    {
        public int Solve()
        {
            var generatorA = new Generator(722, 16807);
            var generatorB = new Generator(354, 48271);

            int matches = 0;

            for (int i = 0; i < 40000000; i++)
            {
                var valueA = generatorA.Next();
                var valueB = generatorB.Next();
                var valueABits = ((int)valueA).ToBitString().PadLeft(16, '0');
                var valueBBits = ((int)valueB).ToBitString().PadLeft(16, '0');


                if (valueABits.Substring(valueABits.Length-16) == valueBBits.Substring(valueBBits.Length-16))
                {
                    matches++;
                }
            }

            return matches;

        }
        
    }



    class Generator
    {
        private long PreviousValue { get; set; }
        private long Factor { get; set; }
        private const long MagicNumber = 2147483647;
        public Generator(long previousValue, long factor)
        {
            PreviousValue = previousValue;
            Factor = factor;

        }

        public long Next()
        {
            var nextValue = (PreviousValue * Factor) % MagicNumber;
            PreviousValue = nextValue;
            return nextValue;
        }
    }
}