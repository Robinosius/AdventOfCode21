using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.IO;

namespace AdventOfCode21
{
    class Day_01 : BaseDay
    {
        private List<string> input;

        public Day_01()
        {
            this.input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            List<int> numbers = input
                .Select(s => Convert.ToInt32(s))
                .ToList();
            int count = 0;
            for (int i = 1; i < input.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    count++;
                }
            }
            return new(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<int> numbers = input
                .Select(s => Convert.ToInt32(s))
                .ToList();
            int count = 0;
            int sum = numbers[0] + numbers[1] + numbers[2];
            for (int i = 3; i < numbers.Count; i++)
            {
                var newSum = sum - numbers[i - 3] + numbers[i];
                if (newSum > sum)
                {
                    count++;
                }
                sum = newSum;
            }
            return new(count.ToString());
        }
    }
}
