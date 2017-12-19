using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
  public class Day18Part2 : ISolution
  {
    public int Solve()
    {
      var queueFor0 = new ConcurrentQueue<long>();
      var queueFor1 = new ConcurrentQueue<long>();

      var task0 = Task.Run(() => ExecuteInstructions(0, queueFor0, queueFor1));
      var task1 = Task.Run(() => ExecuteInstructions(1, queueFor1, queueFor0));

      //Console.WriteLine(task0.Result);
      //Console.WriteLine(task1.Result);
      Thread.Sleep(10000);
      return 42;
    }

    private static int ExecuteInstructions(int programId, ConcurrentQueue<long> incomingMessages, ConcurrentQueue<long> outgoingMessages)
    {
      var howManySends = 0;
      var instructions = Input.SplitByNewLines().ToArray();
      var registers = new Dictionary<string, long>();
      registers["p"] = programId;
      for (int index = 0; index < instructions.Length; index++)
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
        var values =
          operands.Select(operand => int.TryParse(operand, out intValue) ? intValue : registers[operand]).ToArray();

        switch (cmd)
        {
          case "snd":
            outgoingMessages.Enqueue(values[0]);
            Thread.Sleep(100);
            howManySends++;
            
              Console.WriteLine($"I'm program {programId}, and I just sent value number {howManySends}, which was {values[0]}"); // 127 is too low
            
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
            //if (values[0] != 0) // Ah yes, this is no longer needed...
            {
              long receivedValue;
              while (!incomingMessages.TryDequeue(out receivedValue))
              {
                // Spin-wait... Because hey, why not
                Console.WriteLine($"Program {programId} is waiting for messages");
                Thread.Sleep(2000);
              }

              registers[operands[0]] = receivedValue;
              Console.WriteLine(
                $"Program {programId} has received message {receivedValue} and stored it in register {operands[0]}");
            }
            
            break;

          case "jgz":
            if (values[0] > 0) index += (int) values[1] - 1;
            break;
        }
      }

      Console.WriteLine($"Program {programId} just ran out of instructions");
      return 42;
    }

    private static string TestInput = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

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