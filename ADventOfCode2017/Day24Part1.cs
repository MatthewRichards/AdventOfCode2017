using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
  public class Day24Part1 : ISolution
  {

    private class Day24Chain
    {
      public Day24Chain()
      {
        Components = new HashSet<Tuple<int, int>>();
        ChainValues = new List<int>();
      }

      public Day24Chain(Tuple<int, int> initialComponent):this()
      {
        Components.Add(initialComponent);
        ChainValues.Add(0);
        ChainValues.Add(initialComponent.Item1 == 0 ? initialComponent.Item2 : initialComponent.Item1);
      }

      public Day24Chain(Day24Chain chainToCloneAndExtend, Tuple<int, int> additionalComponent):this()
      {
        chainToCloneAndExtend.Components.ToList().ForEach(c => Components.Add(c));
        ChainValues.AddRange(chainToCloneAndExtend.ChainValues);
        var currentEnd = ChainValues.Last();
        var newEnd = additionalComponent.Item1 == currentEnd ? additionalComponent.Item2 : additionalComponent.Item1;
        ChainValues.Add(newEnd);
        Components.Add(additionalComponent);
      }

      public HashSet<Tuple<int, int>> Components { get; }
      public List<int> ChainValues { get; }
    }
    public int Solve()
    {
      var components = Input.SplitByNewLines().Select(c => c.Split('/')).Select(cs => Tuple.Create(int.Parse(cs[0]), int.Parse(cs[1]))).ToArray().ToArray();

      var validStarters = components.Where(c => c.Item1 == 0 || c.Item2 == 0);
      var currentChains = validStarters.Select(c => new Day24Chain(c));
      bool haveChangedSomething = true;

      while (haveChangedSomething)
      {
        haveChangedSomething = false;
      var newChains = new List<Day24Chain>();

        foreach (var chain in currentChains)
        {
          // Create a load of new chains which have all the possible extensions

          var endValue = chain.ChainValues.Last();
          var possibleNextSteps =
            components.Except(chain.Components).Where(c => c.Item1 == endValue || c.Item2 == endValue);

          if (possibleNextSteps.Count() > 0)
          {
            foreach (var component in possibleNextSteps)
            {
              haveChangedSomething = true;
              newChains.Add(new Day24Chain(chain, component));
            }
          }
          else
          {
            newChains.Add(chain); // Just keep the one we already have
          }
        }

          currentChains = newChains;
      }


      return currentChains.Select(chain => chain.Components.Sum(c => c.Item1 + c.Item2)).Max();
    }

    private static string TestInput = @"0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10";

    private static string Input = @"48/5
25/10
35/49
34/41
35/35
47/35
34/46
47/23
28/8
27/21
40/11
22/50
48/42
38/17
50/33
13/13
22/33
17/29
50/0
20/47
28/0
42/4
46/22
19/35
17/22
33/37
47/7
35/20
8/36
24/34
6/7
7/43
45/37
21/31
37/26
16/5
11/14
7/23
2/23
3/25
20/20
18/20
19/34
25/46
41/24
0/33
3/7
49/38
47/22
44/15
24/21
10/35
6/21
14/50";


  }
}