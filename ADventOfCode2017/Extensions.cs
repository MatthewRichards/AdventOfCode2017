using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByNewLines(this string input)
        {
            return input.Split(new[] { System.Environment.NewLine
    }, System.StringSplitOptions.None);
        }

        public static string CalculateKnotHash(this string input)
        {
            var list = Enumerable.Range(0, 256).ToArray();
            int currentPosition = 0;
            int skipSize = 0;
            var lengths = input.Select(c => (int)c).Concat(new int[] { 17, 31, 73, 47, 23 });

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
            var denseHash = new int[sparseHash.Length / 16];

            for (int i = 0; i < denseHash.Length; i++)
            {
                var hashValue = sparseHash[i * 16];
                for (int j = 1; j < 16; j++)
                {
                    hashValue = hashValue ^ sparseHash[i * 16 + j];
                }

                denseHash[i] = hashValue;
            }

            var result = string.Join("", denseHash.SelectMany(v => v.ToString("X2")).ToArray());
            return result;
        }

        private static void Reverse(int[] list, int start, int length)
        {
            for (int i = 0; i < length / 2; i++)
            {
                var oneEnd = start + i;
                while (oneEnd >= list.Length) oneEnd -= list.Length;
                var otherEnd = start + length - 1 - i;
                while (otherEnd >= list.Length) otherEnd -= list.Length;

                var swap = list[oneEnd];
                list[oneEnd] = list[otherEnd];
                list[otherEnd] = swap;
            }
        }
    }
}