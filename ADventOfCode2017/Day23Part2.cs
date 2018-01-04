using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
  public class Day23Part2 : ISolution
  {
    public int SolveByJustWritingMyOwnVersionOfTheInputCodeAttempt2()
    {
      int mainLoopIterations = 0;
      long
        a = 1,
  b = 0,
  c = 0,
  d = 0,
  e = 0,
  f = 0,
  g = 0,
  h = 0;

      b = 99;// set b 99
      c = b;// set c b
            //Invariant skip instruction: jnz a 2
            //Invariant always skipped:jnz 1 5
      b *= 100;// mul b 100
      b -= -100000;// sub b -100000
      c = b;// set c b
      c -= -17000;// sub c - 17000
      while (true)
      {
        f = 1;// set f 1
        d = 2;// set d 2
        while (true)
        {
          e = 2;// set e 2
          while (true)
          {
            if (mainLoopIterations++ % 2000000000 == 0)
            {
              Console.Out.WriteLine("     b          c          d          e          f          g          h");
            }

            if (mainLoopIterations % 100000000 == 0)
            {
              Console.Out.WriteLine($"{b,10}{c,10}{d,10}{e,10}{f,10}{g,10}{h,10}");
            }

            g = d;// set g d
            g *= e;// mul g e
            g -= b;// sub g b
            if (g == 0) //jnz g 2
            {
              f = 0;//set f 0
            }
            e -= -1;// sub e -1
            g = e;// set g e
            g -= b;// sub g b
            if (g == 0) break; //jnz g -8
          }
          d -= -1;//sub d -1
          g = d;// set g d
          g -= b;// sub g b
          if (g == 0) break;// jnz g -13
        }
        if (f == 0)
        { //jnz f 2
          h -= -1;// sub h -1
        }
        g = b;// set g b
        g -= c;// sub g c
        if (g == 0)
        {//jnz g 2
         //Invariant skip to end: jnz 1 3
          return (int)h;
        }
        b -= -17;// sub b -17
      } //jnz 1 - 23
    }

    public int SolveByJustWritingMyOwnVersionOfTheInputCodeAttempt1()
    {
      
      int 
        b = 0,
        c = 0,
        d = 0,
        e = 0,
        f = 0,
        g = 0,
        h = 0;

      b = 109900;
      c = 126900;

      while(true)
      {
        f = 1;
        d = 2;
        do
        {
          e = 2;

          do
          {
            if ((d * e) == b) f = 0;

            e++;
            g = e - b;

          }
          while (g != 0);

          d++;
          g = d - b;
        }
        while (g != 0);

        if (f == 0)
        {
          h++;
        }

        g = b;
        g -= c;

        if (g == 0)
        {
          return h; // 937 is too high...
        }

        b += 17;

      }
    }

    public int Solve()
    {
      return SolveByJustWritingMyOwnVersionOfTheInputCodeAttempt2();
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