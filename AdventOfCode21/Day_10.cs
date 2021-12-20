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
            List<long> scores = new();
            List<char> openers = new List<char> { '(', '[', '{', '<' };
            List<char> closers = new List<char> { ')', ']', '}', '>' };
            Stack<char> chunkStack = new();
            foreach (string line in input.ToList()) // remove corrupted lines
            {
                foreach (char c in line)
                {
                    if (openers.Contains(c))
                    {
                        chunkStack.Push(c);
                    }
                    else if (closers.IndexOf(c) == openers.IndexOf(chunkStack.Peek()))
                    {
                        chunkStack.Pop();
                    }
                    else
                    {
                        input.Remove(line);
                        break;
                    }
                }
            }
            chunkStack.Clear();
            foreach (string line in input)
            {
                foreach (char c in line) // get stack of "unclosed openers"
                {
                    if (openers.Contains(c))
                    {
                        chunkStack.Push(c);
                    }
                    else if (closers.IndexOf(c) == openers.IndexOf(chunkStack.Peek()))
                    {
                        chunkStack.Pop();
                    }
                }
                long score = 0;
                while (chunkStack.Count > 0)
                {
                    score *= 5;
                    score += openers.IndexOf(chunkStack.Pop()) + 1;
                }
                scores.Add(score);
            }
            scores.Sort();
            return new(scores[scores.Count() / 2].ToString());
        }
    }
}
