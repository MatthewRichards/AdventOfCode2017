using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode2017
{
  public class Day18Part1 : ISolution
  {
    public int Solve()
    {
      var instructions = Input.SplitByNewLines().ToArray();
      var registers = new Dictionary<string, long>();
      long lastSound = 0;
      long recoveredSound = 0;

      for(int index=0; index < instructions.Length; index++)
      {
        var instruction = instructions[index];
        var cmd = instruction.Substring(0, 3);
        var operands = instruction.Substring(4).Split(' ');

        foreach (var operand in operands)
        {
          if (!registers.ContainsKey(operand))
          {
            registers[operand] = 0;
          }
        }


        int intValue;
        var values = operands.Select(operand => int.TryParse(operand, out intValue) ? intValue : registers[operand]).ToArray();

        switch (cmd)
        {
          case "snd":
            lastSound = values[0];
            break;

          case "set":
            registers[operands[0]] = values[1];
            break;

          case "add":
            registers[operands[0]] += values[1];
            break;

          case "mul":
            registers[operands[0]] *= values[1];
            break;

          case "mod":
            registers[operands[0]] = registers[operands[0]] % values[1];
            break;

          case "rcv":
            if (values[0] != 0)
            {
              recoveredSound = lastSound;
              return (int)lastSound;//  191 too low - needed longs rather than ints
            }
            break;

          case "jgz":
            if (values[0] > 0) index += (int)values[1]-1;
            break;
        }
      }

      return 42;
    }

    private static string TestInput = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";

    private static string Input = @"set i 31
set a 1
mul p 17
jgz p p
mul a 2
add i -1
jgz i -2
add a -1
set i 127
set p 316
mul p 8505
mod p a
mul p 129749
add p 12345
mod p a
set b p
mod b 10000
snd b
add i -1
jgz i -9
jgz a 3
rcv b
jgz b -1
set f 0
set i 126
rcv a
rcv b
set p a
mul p -1
add p b
jgz p 4
snd a
set a b
jgz 1 3
snd b
set f 1
add i -1
jgz i -11
snd a
jgz f -16
jgz a -19";


  }
}