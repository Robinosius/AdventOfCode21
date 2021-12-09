using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_08 : BaseDay
    {
        private List<string> input;

        public Day_08()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int count = 0;
            int[] counts = { 2, 3, 4, 7 };
            foreach(string line in input)
            {
                count += line.Split(" | ")[1].Split(" ").Where(var => counts.Contains(var.Length)).Count();
            }
            return new(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            return new("");
        }
    }
}
