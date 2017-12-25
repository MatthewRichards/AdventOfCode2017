using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode2017
{


  public class Day22Part2 : ISolution
  {
    public enum Direction
    {
      Up,
      Down,
      Right,
      Left
    }

    public enum InfectionLevel
    {
      Clean = 0,
      Weakened = 1,
      Infected = 2,
      Flagged = 3
    }

    public int Solve()
    {
      var infectionLevels = new Dictionary<Tuple<int, int>, InfectionLevel>();

      var map =Input.SplitByNewLines().Select(row => row.Select(cell => cell == '#').ToArray()).ToArray();

      for (int i = 0; i < map.Length; i++)
      {
        for (int j = 0; j < map[i].Length; j++)
        {
          if (map[i][j])
          {
            infectionLevels.Add(Tuple.Create(j, i), InfectionLevel.Infected);
          }
        }
      }

      var direction = Direction.Up;
      int xLocation = map.Length / 2;
      int yLocation = map[0].Length / 2;
      int infectionsCaused = 0;

      for (int burst = 0; burst < 10000000; burst++)
      {
        var currentLocation = Tuple.Create(xLocation, yLocation);

        var infectionLevel = infectionLevels.ContainsKey(currentLocation)
          ? infectionLevels[currentLocation]
          : InfectionLevel.Clean;
        //Console.WriteLine($"Starting burst {burst} heading {direction} at {currentLocation} which is {infectionLevel}...");

        switch (infectionLevel)
        {
          case InfectionLevel.Clean:
            // Turn left, and weaken node
            switch (direction)
            {
              case Direction.Down:
                direction = Direction.Right;
                break;

              case Direction.Left:
                direction = Direction.Down;
                break;

              case Direction.Up:
                direction = Direction.Left;
                break;

              case Direction.Right:
                direction = Direction.Up;
                break;
            }

            infectionLevels[currentLocation] = InfectionLevel.Weakened;
            break;

            case InfectionLevel.Weakened:
          // No turn, and infect node
          infectionLevels[currentLocation] = InfectionLevel.Infected;
            infectionsCaused++;
            break;

          case InfectionLevel.Infected:
            // Turn right, and flag node
            switch (direction)
            {
              case Direction.Down:
                direction = Direction.Left;
                break;

              case Direction.Left:
                direction = Direction.Up;
                break;

              case Direction.Up:
                direction = Direction.Right;
                break;

              case Direction.Right:
                direction = Direction.Down;
                break;
            }
            infectionLevels[currentLocation] = InfectionLevel.Flagged;
            break;
            case InfectionLevel.Flagged:
            // Reverse direction, and clean node
            switch (direction)
            {
              case Direction.Down:
                direction = Direction.Up;
                break;

              case Direction.Up:
                direction = Direction.Down;
                break;

              case Direction.Left:
                direction = Direction.Right;
                break;

              case Direction.Right:
                direction = Direction.Left;
                break;
            }
            infectionLevels[currentLocation] = InfectionLevel.Clean;
            break;

        }
        
        // Move forward one

        switch (direction)
        {
          case Direction.Down:
            yLocation++;
            break;

          case Direction.Up:
            yLocation--;
            break;

          case Direction.Right:
            xLocation++;
            break;

          case Direction.Left:
            xLocation--;
            break;
        }
      }

      return infectionsCaused;
    }

    private static string TestInput = @"..#
#..
...";

    private static
      string Input = @"...###.#.#.##...##.#..##.
.#...#..##.#.#..##.#.####
#..#.#...######.....#####
.###.#####.#...#.##.##...
.#.#.##......#....#.#.#..
....##.##.#..##.#...#....
#...###...#.###.#.#......
..#..#.....##..####..##.#
#...#..####.#####...#.##.
###.#.#..#..#...##.#..#..
.....##..###.##.#.....#..
#.....#...#.###.##.##...#
.#.##.##.##.#.#####.##...
##.#.###..#.####....#.#..
#.##.#...#.###.#.####..##
#.##..#..##..#.##.####.##
#.##.#....###.#.#......#.
.##..#.##..###.#..#...###
#..#.#.#####.....#.#.#...
.#####..###.#.#.##..#....
###..#..#..##...#.#.##...
..##....##.####.....#.#.#
..###.##...#..#.#####.###
####.########.#.#..##.#.#
#####.#..##...####.#..#..";

  }

}
 