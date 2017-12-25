using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
  public class Day23Part1 : ISolution
  {
    public int Solve()
    {
      var instructions = Input.SplitByNewLines().ToArray();
      var mulsInvoked = 0;
      var registers = new Dictionary<string, long>();
      registers["a"] = 0;
      registers["b"] = 0;
      registers["c"] = 0;
      registers["d"] = 0;
      registers["e"] = 0;
      registers["f"] = 0;
      registers["g"] = 0;
      registers["h"] = 0;

      for (int index = 0; index < instructions.Length; index++)
      {
        var instruction = instructions[index];
        var cmd = instruction.Substring(0, 3);
        var operands = instruction.Substring(4).Split(' ');


        int intValue;
        var values =
          operands.Select(operand => int.TryParse(operand, out intValue) ? intValue : registers[operand]).ToArray();

        switch (cmd)
        {

          case "set":
            registers[operands[0]] = values[1];
            break;

          case "sub":
            registers[operands[0]] -= values[1];
            break;

          case "mul":
            registers[operands[0]] *= values[1];
            mulsInvoked++;
            break;


          case "jnz":
            if (values[0] != 0) index += (int) values[1] - 1;
            break;
        }
      }

      return mulsInvoked;
    }

    private static string Input = @"set b 99
set c b
jnz a 2
jnz 1 5
mul b 100
sub b -100000
set c b
sub c -17000
set f 1
set d 2
set e 2
set g d
mul g e
sub g b
jnz g 2
set f 0
sub e -1
set g e
sub g b
jnz g -8
sub d -1
set g d
sub g b
jnz g -13
jnz f 2
sub h -1
set g b
sub g c
jnz g 2
jnz 1 3
sub b -17
jnz 1 -23";


  }
}