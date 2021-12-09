using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_07 : BaseDay
    {
        string input;

        public Day_07()
        {
            input = File.ReadAllLines(InputFilePath).First();
        }

        public override ValueTask<string> Solve_1()
        {
            var positions = input.Split(",").Select(var => Int32.Parse(var)).ToList();
            long distance = long.MaxValue;
            long newDistance = 0;
            for (int i = positions.Min(); i < positions.Max(); i++)
            {                
                positions.ForEach(val => newDistance += Math.Abs(val - i));
                if(newDistance < distance)
                {
                    distance = newDistance;
                } else if (newDistance > distance)
                {
                    break;
                }
            }
            return new(distance.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var positions = input.Split(",").Select(var => Int32.Parse(var)).ToList();
            long distance = long.MaxValue;
            long newDistance = 0;
            for (int i = positions.Min(); i < positions.Max(); i++)
            {                
                positions.ForEach(val => newDistance += GetCrabConsumption(val, i));
                if (newDistance < distance)
                {
                    distance = newDistance;
                }
                else
                {
                    break;
                }
            }
            return new(distance.ToString());
        }

        public long GetCrabConsumption(int crabPos, int meetingPos)
        {
            int distance = Math.Abs(crabPos - meetingPos);
            long fuelConsumption = 0;
            for(int i = 1; i <= distance; i++)
            {
                fuelConsumption += i;
            }
            return fuelConsumption;
        }
    }
}
