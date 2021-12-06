using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_05 : BaseDay
    {
        private List<string> input;

        public Day_05()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int[,] grid = new int[1000, 1000];
            foreach (var command in input)
            {
                var split = command.Split(" -> ");
                int[] p1 = { Int32.Parse(split[0].Split(",")[0]), Int32.Parse(split[0].Split(",")[1]) };
                int[] p2 = { Int32.Parse(split[1].Split(",")[0]), Int32.Parse(split[1].Split(",")[1]) };
                int[] vector = { Math.Sign(p2[0] - p1[0]), Math.Sign(p2[1] - p1[1]) };
                if ((vector[0] != 0 ^ vector[1] != 0) || Math.Abs(vector[0]) == Math.Abs(vector[1]))
                {
                    while (!Enumerable.SequenceEqual(p1, p2))
                    {
                        grid[p1[0], p1[1]]++;
                        p1[0] += vector[0];
                        p1[1] += vector[1];
                    }
                    grid[p2[0], p2[1]]++;
                }
            }

            int count = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (grid[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }
            return new(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int[,] grid = new int[1000, 1000];
            foreach (var command in input)
            {
                var split = command.Split(" -> ");
                int[] p1 = { Int32.Parse(split[0].Split(",")[0]), Int32.Parse(split[0].Split(",")[1]) };
                int[] p2 = { Int32.Parse(split[1].Split(",")[0]), Int32.Parse(split[1].Split(",")[1]) };
                int[] vector = { Math.Sign(p2[0] - p1[0]), Math.Sign(p2[1] - p1[1]) };
                if ((vector[0] != 0 ^ vector[1] != 0) || Math.Abs(vector[0]) == Math.Abs(vector[1]))
                {
                    while (!Enumerable.SequenceEqual(p1, p2))
                    {
                        grid[p1[0], p1[1]]++;
                        p1[0] += vector[0];
                        p1[1] += vector[1];
                    }
                    grid[p2[0], p2[1]]++;
                }
            }

            int count = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (grid[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }
            return new(count.ToString());
        }
    }
}
