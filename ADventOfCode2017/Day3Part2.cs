using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day3Part2 : ISolution
    {
        enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }

        public int Solve()
        {
            int targetSquare = 361527;

            int currentSquare = 1;
            int howRight = 0;
            int howUp = 0;
            int spiralSize = 1;
            int howSpiralled = 0;
            Direction direction = Direction.Right;

            // We store (howRight, howUp) => value
            var storedValues = new Dictionary<Tuple<int, int>, int>();

            storedValues[Tuple.Create(0, 0)] = 1; // By definition

            while (currentSquare < targetSquare)
            {
                currentSquare++;

                switch (direction)
                {
                    case Direction.Right:
                        howRight++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Up;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Up:
                        howUp++;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Left;
                            howSpiralled = 0;
                            spiralSize++;
                        }
                        break;

                    case Direction.Left:
                        howRight--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Down;
                            howSpiralled = 0;
                        }
                        break;

                    case Direction.Down:
                        howUp--;
                        howSpiralled++;

                        if (howSpiralled >= spiralSize)
                        {
                            direction = Direction.Right;
                            howSpiralled = 0;
                            spiralSize++;
                        }

                        break;
                }

                // Find all the adjacent squares that have values, add them up, 
                // and store them in this square, which shouldn't already have a value


                var thisSquare = Tuple.Create(howRight, howUp);

                if (storedValues.ContainsKey(thisSquare)) throw new InvalidOperationException("Oops, tried to store a value twice");

                var adjacentSquares = new[]
                {Tuple.Create(howRight +1,howUp),
                Tuple.Create(howRight -1,howUp),
                Tuple.Create(howRight, howUp+1),
                Tuple.Create(howRight, howUp-1),
                Tuple.Create(howRight+1, howUp+1),
                Tuple.Create(howRight +1,howUp-1),
                Tuple.Create(howRight-1,howUp+1),
                Tuple.Create(howRight-1,howUp-1)};

                var valueToStore = adjacentSquares
                    .Where(square => storedValues.ContainsKey(square))
                    .Sum(square => storedValues[square]);
                Console.WriteLine($"Square:{currentSquare} at Right: {howRight}, Up: {howUp}, storing value: {valueToStore}");
                storedValues[thisSquare] = valueToStore;

                if (valueToStore > targetSquare)
                {
                    return valueToStore;
                }
            }

            return -1;

        }
    }
}