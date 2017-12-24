using System;
using System.Linq;

namespace AdventOfCode2017
{
  internal class Day21Pattern
  {
    private bool[][] Pattern { get; set; }

    private static readonly bool[][] InititalPattern =
    {
      new[] {false, true, false},
      new[] {false, false, true},
      new[] {true, true, true}
    };

    public Day21Pattern()
    {
      Pattern = InititalPattern;
    }

    public override string ToString()
    {
      return string.Join(Environment.NewLine,
        Pattern.Select(row => string.Join("", row.Select(cell => cell ? "#" : "."))));
    }

    public Day21Pattern(bool[][] pattern)
    {
      Pattern = pattern;
    }

    public Day21Pattern(Day21Pattern[][] componentPatterns)
    {
      var componentSize = componentPatterns[0][0].Size;
      Pattern = new bool[componentSize * componentPatterns.Length][];

      for (int i = 0; i < componentSize * componentPatterns[0].Length; i++)
      {
        Pattern[i] = new bool[componentSize * componentPatterns[0].Length];

        for (int j = 0; j < componentSize * componentPatterns[0].Length; j++)
        {
          Pattern[i][j] =
            componentPatterns[i / componentSize][j / componentSize].Pattern[i % componentSize][j % componentSize];
        }
      }

    }

    public int Size => Pattern.Length;

    public Day21Pattern[][] BreakIntoSquares()
    {
      var eachSquareSize = (Size % 2 == 0 ? 2 : 3);
      Day21Pattern[][] squares = new Day21Pattern[Size / eachSquareSize][];
      for (int i = 0; i < Size / eachSquareSize; i++)
      {
        squares[i] = new Day21Pattern[Size / eachSquareSize];
        for (int j = 0; j < Size / eachSquareSize; j++)
        {
          bool[][] thisSquarePattern = new bool[eachSquareSize][];

          for (int k = 0; k < eachSquareSize; k++)
          {
            thisSquarePattern[k] = new bool[eachSquareSize];
            for (int l = 0; l < eachSquareSize; l++)
            {
              thisSquarePattern[k][l] = Pattern[i * eachSquareSize + k][j * eachSquareSize + l];
            }

            squares[i][j] = new Day21Pattern(thisSquarePattern);
          }
        }
      }

      return squares;
    }

    public string ToEncodedPattern()
    {
      // Each pattern is written concisely: rows are listed as single units, ordered top-down, and separated by slashes.
      /*
       * ../.#  =  ..
          .#
       */

      return string.Join("/", Pattern.Select(row => string.Join("", row.Select(cell => cell ? '#' : '.').ToArray())));
    }

    public Day21Pattern RotateLeft()
    {
      // The lazy approach for one who ealises that there are only two possible sizes to rotate...
      switch (Size)
      {
        case 2:
          return new Day21Pattern(new[]
          {
            new[] {Pattern[0][1], Pattern[1][1]}, new[] {Pattern[0][0], Pattern[1][0]}
          });
        case 3:
          return new Day21Pattern(new[]
          {
            new[] {Pattern[0][2], Pattern[1][2], Pattern[2][2]},
            new[] {Pattern[0][1], Pattern[1][1], Pattern[2][1]},
            new[] {Pattern[0][0], Pattern[1][0], Pattern[2][0]}
          });

        default:
          throw new InvalidOperationException("Oops, that size isn't supposed to exist!");
      }
    }
    public Day21Pattern RotateRight()
    {
      return this.RotateLeft().RotateLeft().RotateLeft();
    }

    public Day21Pattern FlipHorizontal()
    {
      return new Day21Pattern(
        Pattern.Select(row => row.Reverse().ToArray()).ToArray());
    }

    public Day21Pattern FlipVertical()
    {
      return new Day21Pattern(
        Pattern.Reverse().ToArray());
    }

    public int CountOnPixels()
    {
      return Pattern.Sum(row => row.Sum(cell => cell ? 1 : 0));
    }
  }
}
