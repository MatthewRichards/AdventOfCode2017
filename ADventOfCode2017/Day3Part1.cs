using System;

namespace AdventOfCode2017
{
    public class Day3Part1 : ISolution
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

            }

            int answer = Math.Abs(howRight) + Math.Abs(howUp);
            Console.WriteLine($"Answer is {answer} because we've gone right {howRight} and up {howUp}");
            return answer;


        }
    }
}