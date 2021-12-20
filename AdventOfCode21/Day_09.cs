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
            int[,] heightMap = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Count(); j++)
                {
                    heightMap[i, j] = Int32.Parse(input[i][j].ToString());
                }
            }
            List<int> neighbors = new();
            List<int> basinSizes = new();
            for (int i = 0; i < heightMap.GetLength(0); i++)
            {
                for (int j = 0; j < heightMap.GetLength(1); j++)
                {
                    neighbors.Clear();
                    neighbors.Add(i > 0 ? heightMap[i - 1, j] : 10); // top
                    neighbors.Add(i < heightMap.GetLength(1) - 1 ? heightMap[i + 1, j] : 10); // bottom
                    neighbors.Add(j > 0 ? heightMap[i, j - 1] : 10); // left
                    neighbors.Add(j < heightMap.GetLength(1) - 1 ? heightMap[i, j + 1] : 10); //right
                    if (heightMap[i, j] < neighbors.Min())
                    {
                        var basin = GetBasin(heightMap, new int[] { i, j }, new List<int[]> { new int[] { i, j } }).Distinct(new SequenceEqualityComparer<int[]>()).ToList();
                        basinSizes.Add(basin.Count);
                    }
                }
            }
            basinSizes.Sort();
            return new((basinSizes[basinSizes.Count - 1] * basinSizes[basinSizes.Count - 2] * basinSizes[basinSizes.Count - 3]).ToString());
        }

        public List<int[]> GetBasin(int[,] heightMap, int[] point, List<int[]> basin)
        {
            if (point[0] == -1 || heightMap[point[0], point[1]] == 9 || basin.Contains(point)){
                return basin.Distinct().ToList();
            }
            basin.Add(point);            
            // get all neighboring bassin members, invalid = {-1, -1}
            int[] t = point[0] > 0 && heightMap[point[0] - 1, point[1]] > heightMap[point[0], point[1]] ? new int[] { point[0] - 1, point[1] } : new int[]{ -1, -1}; // top
            int[] b = point[0] < heightMap.GetLength(1) - 1 && heightMap[point[0] + 1, point[1]] > heightMap[point[0], point[1]] ? new int[] { point[0] + 1, point[1] } : new int[] { -1, -1 }; // bottom
            int[] l = point[1] > 0 && heightMap[point[0], point[1] - 1] > heightMap[point[0], point[1]] ? new int[] { point[0], point[1] - 1 } : new int[] { -1, -1 }; // left
            int[] r = point[1] < heightMap.GetLength(1) - 1 && heightMap[point[0], point[1] + 1] > heightMap[point[0], point[1]] ? new int[] { point[0], point[1] + 1 } : new int[] { -1, -1 }; //right
            heightMap[point[0], point[1]] = 9;
            return GetBasin(heightMap, t, basin)
                .Concat(GetBasin(heightMap, b, basin))
                .Concat(GetBasin(heightMap, l, basin))
                .Concat(GetBasin(heightMap, r, basin)).Distinct().ToList();
        }

        class SequenceEqualityComparer<T> : IEqualityComparer<int[]>
        {
            public bool Equals(int[] a, int[] b)
            {
                if (a == null) return b == null;
                if (b == null) return false;
                return a.SequenceEqual(b);
            }

            public int GetHashCode(int[] val)
            {
                return val.Where(v => v != null)
                        .Aggregate(0, (h, v) => h ^ v.GetHashCode());
            }
        }

    }
}
