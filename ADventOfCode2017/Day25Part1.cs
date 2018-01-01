using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode2017
{


    public class Day25Part1 : ISolution
    {
        public int Solve()
        {
            var tape = new Dictionary<int, bool>();
            int cursor = 0;
            //Begin in state A
            char state = 'A';

            //Perform a diagnostic checksum after 12964419 steps.
            for (int steps = 0; steps < 12964419; steps++)
            {
                var currentValue = tape.ContainsKey(cursor) ? tape[cursor] : false;
                switch (state)
                {
                    case 'A':
                        /*
In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state F.
*/
                        if (!currentValue)
                        {
                            tape[cursor] = true;
                            cursor++;
                            state = 'B';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor++;
                            state = 'F';
                        }
                        break;

                    case 'B':
                        /*
In state B:
  If the current value is 0:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state C.
*/
                        if (!currentValue)
                        {
                            tape[cursor] = false;
                            cursor--;
                            state = 'B';
                        }
                        else
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'C';
                        }
                        break;

                    case 'C':
                        /*
In In state C:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state D.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state C.
*/
                        if (!currentValue)
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'D';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor++;
                            state = 'C';
                        }
                        break;

                    case 'D':
                        /*
In state D:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state E.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.
    */
                        if (!currentValue)
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'E';
                        }
                        else
                        {
                            tape[cursor] = true;
                            cursor++;
                            state = 'A';
                        }
                        break;

                    case 'E':
                        /*
In In state E:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state F.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state D.
*/
                        if (!currentValue)
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'F';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor--;
                            state = 'D';
                        }
                        break;

                    case 'F':
                        /*
In state F:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state E.
*/
                        if (!currentValue)
                        {
                            tape[cursor] = true;
                            cursor++;
                            state = 'A';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor--;
                            state = 'E';
                        }
                        break;


                }
            }



            return tape.Count(kvp => kvp.Value);
        }

    }
}
 