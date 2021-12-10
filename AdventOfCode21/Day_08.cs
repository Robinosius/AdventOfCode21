using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.Collections;

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
            long sum = 0;
            int[,] numbers = { 
                {1,1,1,0,1,1,1 },
                {0,0,1,0,0,1,0 },
                {1,0,1,1,1,0,1 },
                {1,0,1,1,0,1,1 },
                {0,1,1,1,0,1,0 },
                {1,1,0,1,0,1,1 },
                {1,1,0,1,1,1,1 },
                {1,0,1,0,0,1,0 },
                {1,1,1,1,1,1,1 },
                {1,1,1,1,0,1,1 }
            };
            Dictionary<char, char> decoder = new();
            foreach (var line in input)
            {
                decoder.Clear();
                string[] digits = line.Split(" | ")[0].Split(" ");
                string number = line.Split(" | ")[1];
                // get codes for numbers with unique number of bits 1, 4 and 7 (8 is useless because all wires are used)
                char[] n1 = digits.Where(var => var.Length == 2).First().ToCharArray();
                char[] n7 = digits.Where(var => var.Length == 3).First().ToCharArray();
                char[] n4 = digits.Where(var => var.Length == 4).First().ToCharArray();
                decoder.Add('a', n7.Except(n1).First());
                decoder.Add('f', ' ');
            }
            return new(sum.ToString());
        }
    }
}
