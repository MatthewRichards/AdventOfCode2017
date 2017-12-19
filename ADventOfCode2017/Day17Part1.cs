using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode2017
{
  public class Day17Part1 : ISolution
  {
    public int Solve()
    {
      var buffer = new CircularBuffer();
      int stepSize = 370;

      for (int numberToInsert = 1; numberToInsert <= 2017; numberToInsert++)
      {
        for (int i = 0; i < stepSize; i++)
          {
            // Step forward...

            buffer.StepForward();
          }

        // Insert the next value, and use as the current position
        buffer.CurrentPosition = buffer.CurrentPosition.InsertAfter(numberToInsert);

      }

      return buffer.CurrentPosition.Next.Value;

    }

  }

  public class CircularBuffer
  {

    public BufferNode CurrentPosition { get; set; }
    public CircularBuffer()
    {
      CurrentPosition = new BufferNode() {Value = 0};
      CurrentPosition.Next = CurrentPosition;
    }

    public void StepForward()
    {
      CurrentPosition = CurrentPosition.Next;
    }

    public class BufferNode
    {
      public int Value { get; set; }
      public BufferNode Next { get; set; }

      public BufferNode InsertAfter(int newValue)
      {
        var newNode = new BufferNode {Value = newValue, Next = Next};
        this.Next = newNode;
        return newNode;

      }
    }
  }
}