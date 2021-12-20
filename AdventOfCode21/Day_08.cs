using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.Collections;
using System.Text.RegularExpressions;

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
            char[] segments = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            List<string> numbers = new List<string>{
                "abcefg",
                "cf",
                "acdeg",
                "acdfg",
                "bcdf",
                "abdfg",
                "abdefg",
                "acf",
                "abcdefg",
                "abcdfg"
            };
            Dictionary<char, char> decoder = new();
            string numberString = "";
            string temp = "";
            foreach (var line in input) // link input values to output values
            {
                decoder.Clear();
                numberString = "";
                string[] digits = line.Split(" | ")[0].Split(" ");
                string joined = Regex.Replace(line.Split(" | ")[0], @" ", "" );
                string[] number = line.Split(" | ")[1].Split(" ");
                // get codes for numbers with unique number of bits 1, 4 and 7 (8 is useless because all wires are used)
                string n1 = digits.Where(var => var.Length == 2).First().ToString(); // char array for number 1
                string n7 = digits.Where(var => var.Length == 3).First().ToString(); // --------------------- 7
                string n4 = digits.Where(var => var.Length == 4).First().ToString(); // --------------------- 4
                decoder.Add('a', n7.Except(n1).First());
                decoder.Add('f', joined.Where(var => joined.Count(c => c == var) == 9).First());
                decoder.Add('c', n1.Where(var => var != decoder['f']).First());
                decoder.Add('b', joined.Where(var => joined.Count(c => c == var) == 6).First());
                decoder.Add('e', joined.Where(var => joined.Count(c => c == var) == 4).First());
                var sixLength = digits.Where(var => var.Length == 6).ToList();
                decoder.Add('d', joined.Where(var => joined.Where(c => c == var).Count() == 7 && sixLength.Count(c => c.Contains(var)) == 2).First());
                decoder.Add('g', segments.Except(decoder.Values.ToArray()).First());

                // decode number
                foreach(string str in number) // each number as string
                {
                    temp = "";
                    
                    foreach(char c in str)
                    {
                        temp += decoder.FirstOrDefault(var => var.Value == c).Key;
                    }
                    temp = String.Concat(temp.OrderBy(x => x));
                    numberString += numbers.IndexOf(temp);
                }
                sum += Int32.Parse(numberString);
            }
            return new(sum.ToString());
        }
    }
}
