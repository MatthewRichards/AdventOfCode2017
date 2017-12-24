using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
  internal class Day21RuleBook
  {
    private Dictionary<string, bool[][]> rules = new Dictionary<string, bool[][]>();

    public Day21RuleBook(string definitions)
    {
      foreach (var definition in definitions.SplitByNewLines())
      {
        int arrowPos = definition.IndexOf("=>", StringComparison.InvariantCulture);

        var leftSide = definition.Substring(0, arrowPos - 1);
        var rightSide = definition.Substring(arrowPos + 3);

        var rightSideAsRows = rightSide.Split('/');
        var rightSideAsPattern = rightSideAsRows.Select(row => row.Select(pixel => pixel == '#').ToArray()).ToArray();

        rules.Add(leftSide, rightSideAsPattern);
      }

      // Example pattern: ../.# => ##./#../...
    }

    public Day21Pattern EnhancePattern(Day21Pattern pattern)
    {
      var encodedPatterns = new[]
      {
        pattern.ToEncodedPattern(),
        pattern.FlipHorizontal().ToEncodedPattern(),
        pattern.FlipHorizontal().RotateLeft().ToEncodedPattern(),
        pattern.FlipVertical().RotateRight().ToEncodedPattern(),
        pattern.FlipVertical().RotateLeft().ToEncodedPattern(),
        pattern.FlipHorizontal().RotateRight().ToEncodedPattern(),
        pattern.FlipVertical().ToEncodedPattern(),
        pattern.RotateLeft().ToEncodedPattern(),
        pattern.RotateLeft().RotateLeft().ToEncodedPattern(),
        pattern.RotateRight().ToEncodedPattern()
      };

      foreach (var encodedPattern in encodedPatterns)
      {
        if (rules.ContainsKey(encodedPattern))
        {
          return new Day21Pattern(rules[encodedPattern]);
        }
      }


      throw new InvalidOperationException("Oops, found no matching rule! Something went wrong...");
    }
  }
}