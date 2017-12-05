using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByNewLines(this string input)
        {
            return input.Split(new[] { System.Environment.NewLine
    }, System.StringSplitOptions.None);
        }
    }
}
