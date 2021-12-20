using AoCHelper;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{
    class Day_09 : BaseDay
    {
        private List<string> input;

        public Day_09()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int riskLevel = 0;
            int[,] heightMap = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count(); j++)
                {
                    heightMap[i, j] = Int32.Parse(input[i][j].ToString());
                }                
            }
            List<int> neighbors = new();
            for(int i = 0; i < heightMap.GetLength(0); i++)
            {
                for (int j = 0; j < heightMap.GetLength(1); j++)
                {
                    neighbors.Clear();
                    neighbors.Add(i > 0 ? heightMap[i - 1, j] : 10); // top
                    neighbors.Add(i < heightMap.GetLength(1) - 1 ? heightMap[i + 1, j] : 10); // bottom
                    neighbors.Add(j > 0 ? heightMap[i , j - 1] : 10); // left
                    neighbors.Add(j < heightMap.GetLength(1) - 1 ? heightMap[i , j + 1] : 10); //right
                    if(heightMap[i,j] < neighbors.Min())
                    {
                        riskLevel += 1 + heightMap[i, j];
                    }
                }
            }
            return new(riskLevel.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            return new("");
        }
    }
}
