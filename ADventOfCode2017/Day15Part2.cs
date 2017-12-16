using System;

namespace AdventOfCode2017
{
    public class Day15Part2 : ISolution
    {
        public int Solve()
        {
            /* Test values
            var generatorA = new PickyGenerator(65, 16807, 4);
            var generatorB = new PickyGenerator(8921, 48271, 8);
            */
            
            var generatorA = new PickyGenerator(722, 16807, 4);
            var generatorB = new PickyGenerator(354, 48271, 8);
            

            int matches = 0;

            for (int i = 0; i < 5000000; i++)
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

            return matches; // 279 too low but matches someone else's answer, so something funny may be going on

        }
        
    }



    class PickyGenerator
    {
        private long PreviousValue { get; set; }
        private long Factor { get; set; }
        private int TargetMultipleOf { get; set; }
        private const long MagicNumber = 2147483647;
        public PickyGenerator(long previousValue, long factor, int targetMultipleOf)
        {
            PreviousValue = previousValue;
            Factor = factor;
            TargetMultipleOf = targetMultipleOf;

        }

        public long Next()
        {
            while (true)
            {
                var nextValue = (PreviousValue * Factor) % MagicNumber;
                PreviousValue = nextValue;

                if (nextValue % TargetMultipleOf == 0) return nextValue;
            }
        }
    }
}