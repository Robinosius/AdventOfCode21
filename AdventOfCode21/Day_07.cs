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
            long[] consumptions = new long[positions.Max() - positions.Min() + 1];
            consumptions[0] = 0;
            consumptions[1] = 1;
            for(int i = 2; i < consumptions.Length; i++)
            {
                consumptions[i] = consumptions[i - 1] + i;
            }

            for (int i = positions.Min(); i < positions.Max(); i++)
            {
                newDistance = 0;
                positions.ForEach(val => newDistance += consumptions[Math.Abs(val - i)]);
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
    }
}
