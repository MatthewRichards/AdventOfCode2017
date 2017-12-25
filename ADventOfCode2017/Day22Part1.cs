using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode2017
{


  public class Day22Part1 : ISolution
  {
    public enum Direction
    {
      Up,
      Down,
      Right,
      Left
    }

    public int Solve()
    {
      var infectedNodes = new HashSet<Tuple<int, int>>();

      var map = Input.SplitByNewLines().Select(row => row.Select(cell => cell == '#').ToArray()).ToArray();

      for (int i = 0; i < map.Length; i++)
      {
        for (int j = 0; j < map[i].Length; j++)
        {
          if (map[i][j])
          {
            infectedNodes.Add(Tuple.Create(j, i));
          }
        }
      }

      var direction = Direction.Up;
      int xLocation = map.Length / 2;
      int yLocation = map[0].Length / 2;
      int infectionsCaused = 0;

      for (int burst = 0; burst < 10000; burst++)
      {
        var currentLocation = Tuple.Create(xLocation, yLocation);

        //Console.WriteLine();
        //Console.WriteLine($"Starting burst {burst} at {currentLocation}...");
        if (infectedNodes.Contains(currentLocation))
        {
          // Turn right and clean current node
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
          infectedNodes.Remove(currentLocation);
          //Console.WriteLine($"Turned {direction} at {currentLocation}, and cured this node");
        }
        else
        {
          // Turn left and infect current node
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

          infectedNodes.Add(currentLocation);
          infectionsCaused++;
          //Console.WriteLine($"Turned {direction} at {currentLocation}, and infected this node, which is the {infectionsCaused} I've caused!");
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
 