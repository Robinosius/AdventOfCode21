using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_11 : BaseDay
    {
        private List<string> input;

        public Day_11()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int[,] energyLevels = new int[input.Count, input[0].Count()];
            for(int i = 0; i < input.Count; i++)
            {
                for(int j = 0; j < input[0].Count(); j++)
                {
                    energyLevels[i, j] = Int32.Parse(input[i][j].ToString());
                }
            }
            // perform steps
            long totalFlashCount = 0;
            for(int step = 0; step < 100; step++)
            {
                IncreaseEnergyLevel(energyLevels);
                totalFlashCount += Flash(energyLevels);
            }
            return new (totalFlashCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        public void IncreaseEnergyLevel(int[,] energyLevels)
        {
            for(int i = 0; i < energyLevels.GetLength(0); i++)
            {
                for(int j = 0; j < energyLevels.GetLength(1); j++)
                {
                    energyLevels[i, j]++;
                }
            }
        }

        public int Flash(int[,] energyLevels)
        {
            int flashCount = 0;
            bool finished = false;
            List<string> hasFlashed = new();
            while (!finished)
            {
                finished = true;
                for (int i = 0; i < energyLevels.GetLength(0); i++)
                {
                    for (int j = 0; j < energyLevels.GetLength(1); j++)
                    {
                        if(energyLevels[i,j] > 9 && !hasFlashed.Contains($"{i} {j}"))
                        {
                            flashCount++;
                            hasFlashed.Add($"{i} {j}");
                            finished = false;
                            IncreaseSurroundingValues(energyLevels, i, j);
                        }
                    }
                }
            }
            ResetFlashedValues(hasFlashed, energyLevels);
            return flashCount;
        }

        public void IncreaseSurroundingValues(int[,] energyLevels, int x, int y)
        {
            for(int i = x - 1; i < x + 2; i++)
            {
                for(int j = y - 1; j < y + 2; j++)
                {
                    if(i >= 0 && i < energyLevels.GetLength(0) && j >= 0 && j < energyLevels.GetLength(1))
                    {
                        energyLevels[i, j]++;
                    }
                }
            }
        }

        public void ResetFlashedValues(List<string> hasFlashed, int[,] energyLevels)
        {
            foreach(var flash in hasFlashed)
            {
                energyLevels[Int32.Parse(flash.Split(' ')[0]), Int32.Parse(flash.Split(' ')[1])] = 0;

            }
        }
    }    
}
