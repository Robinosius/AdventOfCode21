using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;
using System.IO;

namespace AdventOfCode21
{
    class Day_02 : BaseDay
    {
        private List<string> input;

        public Day_02()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int[] position = new int[] { 0, 0 };
            Dictionary<string, Action<int[], int>> commands = new()
            {
                { "up", (int[] pos, int val) => pos[1] -= val },
                { "down", (int[] pos, int val) => pos[1] += val },
                { "forward", (int[] pos, int val) => pos[0] += val }
            };
            foreach (var command in input)
            {
                string direction = command.Split(" ")[0];
                int distance = Convert.ToInt32(command.Split(" ")[1]);
                commands[direction](position, distance);
            }
            return new($"x: {position[0]}, y: {position[1]}, x*y: {position[0] * position[1]}");
        }

        public override ValueTask<string> Solve_2()
        {
            int[] position = { 0, 0, 0 };
            Dictionary<string, Action<int[], int>> commands = new()
            {
                { "up", (int[] pos, int val) => pos[2] -= val },
                { "down", (int[] pos, int val) => pos[2] += val },
                { "forward", (int[] pos, int val) => { pos[0] += val; pos[1] += (pos[2] * val); } }
            };
            foreach (var command in input)
            {
                string direction = command.Split(" ")[0];
                int distance = Convert.ToInt32(command.Split(" ")[1]);
                commands[direction](position, distance);
            }
            return new($"x: {position[0]}, y: {position[1]}, x*y: {position[0] * position[1]}");
        }
    }
}
