using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_10 : BaseDay
    {
        private List<string> input;

        public Day_10()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            long corruptScore = 0;
            List<char> openers = new List<char> { '(', '[', '{', '<' };
            List<char> closers = new List<char> { ')', ']', '}', '>' };
            Dictionary<char, int> scores = new() { 
                { ')', 3 },
                { ']', 57 }, 
                { '}', 1197 },
                { '>', 25137 }
            };
            Stack<char> chunkStack = new();
            foreach(var line in input)
            {
                foreach(char c in line)
                {
                    if (openers.Contains(c))
                    {
                        chunkStack.Push(c);
                    } 
                    else if(closers.IndexOf(c) == openers.IndexOf(chunkStack.Peek()))
                    {
                        chunkStack.Pop();
                    }
                    else
                    {
                        corruptScore += scores[c];
                        break;
                    }
                }
            }
            return new(corruptScore.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            return new("");
        }
    }
}
