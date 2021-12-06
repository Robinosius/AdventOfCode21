using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.Collections;

namespace AdventOfCode21
{
    class Day_03 : BaseDay
    {
        private List<string> input;

        public Day_03()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
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
                if (_gamma[i]) { gamma += Math.Pow(2, (double)(_epsilon.Count - i - 1)); }
                if (_epsilon[i]) { epsilon += Math.Pow(2, (double)(_epsilon.Count - i - 1)); }
            }
            return new($"Gamma: {gamma}, Epsilon: {epsilon}, Multiplied: {gamma * epsilon}");
        }

        public override ValueTask<string> Solve_2()
        {
            var oxInput = input;
            var carbInput = new List<string>(oxInput);

            var i = 0;
            while (oxInput.Count > 1)
            {
                int c0 = oxInput.Where(val => val[i] == '0').Count();
                int c1 = oxInput.Where(val => val[i] == '1').Count();
                char mostCommon = c1 >= c0 ? '1' : '0';
                oxInput = oxInput.Where(val => val[i] == mostCommon).ToList();
                i++;
            }

            i = 0;
            while (carbInput.Count > 1)
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
            return new($"Oxygen Generator Rating: {oxy}, CO2 Scrubber Rating: {carbon}, Life Support Rating: {oxy * carbon}");
        }
    }
}
