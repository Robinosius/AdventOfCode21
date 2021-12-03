using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace AdventOfCode21
{
    class Program
    {
        static void Main(string[] args)
        {
            Day3_2();
        }

        static IEnumerable<string> ReadFile(string name)
        {
            string folder = "..\\..\\..\\..\\Input\\";
            var input = File.ReadAllLines(folder + name);
            return input;
        }

        static void Day1_1()
        {
            List<string> input = new(ReadFile("day1.txt"));
            List<int> numbers = input
                .Select(s => Convert.ToInt32(s))
                .ToList();
            int count = 0;
            for(int i = 1; i < input.Count; i++)
            {
                if(numbers[i] > numbers[i - 1])
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        static void Day1_2()
        {
            List<string> input = new(ReadFile("day1.txt"));
            List<int> numbers = input
                .Select(s => Convert.ToInt32(s))
                .ToList();
            int count = 0;
            int sum = numbers[0] + numbers[1] + numbers[2];
            for(int i = 3; i < numbers.Count; i++)
            {
                var newSum = sum - numbers[i - 3] + numbers[i];
                if(newSum > sum)
                {
                    count++;
                }
                sum = newSum;
            }
            Console.WriteLine(count);
        }

        static void Day2_1()
        {
            var input = ReadFile("day2.txt");
            int[] position = new int[] { 0, 0 };
            Dictionary<string, Action<int[], int>> commands = new() {
                { "up", (int[] pos, int val) => pos[1] -= val},
                { "down", (int[] pos, int val) => pos[1] += val },
                {"forward", (int[] pos, int val) => pos[0] += val }
            };
            foreach(var command in input)
            {
                string direction = command.Split(" ")[0];
                int distance = Convert.ToInt32(command.Split(" ")[1]);
                commands[direction](position, distance);                
            }
            Console.WriteLine($"x: {position[0]}, y: {position[1]}, x*y: {position[0] * position[1]}");
        }

        static void Day2_2()
        {
            var input = ReadFile("day2.txt");
            int[] position = { 0, 0, 0};
            Dictionary<string, Action<int[], int>> commands = new()
            {
                { "up", (int[] pos, int val) => pos[2] -= val },
                { "down", (int[] pos, int val) => pos[2] += val },
                { "forward", (int[] pos, int val) => {pos[0] += val; pos[1] += (pos[2] * val); } }
            };
            foreach (var command in input)
            {
                string direction = command.Split(" ")[0];
                int distance = Convert.ToInt32(command.Split(" ")[1]);
                commands[direction](position, distance);
            }
            Console.WriteLine($"x: {position[0]}, y: {position[1]}, x*y: {position[0] * position[1]}");
        }

        static void Day3_1()
        {
            var input = ReadFile("day3.txt").ToList();
            BitArray _gamma = new(Enumerable.Repeat(false, 12).ToArray());
            BitArray _epsilon = new(Enumerable.Repeat(true, 12).ToArray());
            for (int i = 0; i < input[0].Count(); i++) //digits
            {
                int trueCount = 0;
                for (int j = 0; j < input.Count(); j++) //lines
                {
                    if (input[j][i] == '1')
                    {
                        trueCount++;
                    }
                }
                if (trueCount > input.Count / 2)
                {
                    _gamma[i] = true;
                    _epsilon[i] = false;
                }
            }
            double gamma = 0;
            double epsilon = 0;
            for (int i = 0; i < _epsilon.Count; i++)
            {
                if(_gamma[i]) { gamma += Math.Pow(2, (double)(_epsilon.Count - i - 1)); }
                if(_epsilon[i]) { epsilon += Math.Pow(2, (double)(_epsilon.Count - i - 1)); }
            }
            Console.WriteLine($"Gamma: {gamma}, Epsilon: {epsilon}, Multiplied: {gamma * epsilon}");
        }

        static void Day3_2()
        {
            var oxInput = ReadFile("day3.txt").ToList();
            var carbInput = new List<string>(oxInput);

            var i = 0;
            while(oxInput.Count > 1)
            {
                int c0 = oxInput.Where(val => val[i] == '0').Count();
                int c1 = oxInput.Where(val => val[i] == '1').Count();
                char mostCommon = c1 >= c0 ? '1' : '0';
                oxInput = oxInput.Where(val => val[i] == mostCommon).ToList();
                i++;
            }

            i = 0;
            while(carbInput.Count > 1)
            {
                int c0 = carbInput.Where(val => val[i] == '0').Count();
                int c1 = carbInput.Where(val => val[i] == '1').Count();
                char leastCommon = c0 <= c1 ? '0' : '1';
                carbInput = carbInput.Where(val => val[i] == leastCommon).ToList();
                i++;
            }

            double carbon = 0;
            double oxy = 0;
            for (int j = 0; j < oxInput[0].Count(); j++)
            {
                if (carbInput[0][j] == '1') { carbon += Math.Pow(2, (double)(carbInput[0].Count() - j - 1)); }
                if (oxInput[0][j] == '1') { oxy += Math.Pow(2, (double)(oxInput[0].Count() - j - 1)); }
            }
            Console.WriteLine($"Oxygen Generator Rating: {oxy}, CO2 Scrubber Rating: {carbon}, Life Support Rating: {oxy * carbon}");
        }         
    }
}
