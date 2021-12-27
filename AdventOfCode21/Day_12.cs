using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_12 : BaseDay
    {
        private List<string> input;

        public Day_12()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            Dictionary<string, Cave> caveSystem = new();
            foreach(var line in input)
            {
                var split = line.Split('-');
                string startCave = split[0];
                string endCave = split[1];
                if (!caveSystem.ContainsKey(startCave))
                {
                    caveSystem.Add(startCave, new Cave(startCave, Char.IsLower(startCave[0])));
                }
                if (!caveSystem.ContainsKey(endCave))
                {
                    caveSystem.Add(endCave, new Cave(endCave, Char.IsLower(endCave[0])));
                }
                caveSystem[startCave].AddConnectedCave(caveSystem[endCave]);
                caveSystem[endCave].AddConnectedCave(caveSystem[startCave]);                
            }
            Stack<Cave> path = new();
            path.Push(caveSystem["start"]);
            int pathCount = 0;
            FindPaths_1(path, ref pathCount, caveSystem["end"]) ;            
            return new(pathCount.ToString());
        }

        public void FindPaths_1(Stack<Cave> path, ref int pathCount, Cave end)
        {
            foreach (var cave in path.Peek().ConnectedCaves)
            {
                if (cave.IsSmall && path.Contains(cave))
                {
                    continue; // do not visit small cave again, discard this branching
                }
                if (cave.Name == end.Name)
                {
                    pathCount++;
                    continue;
                }
                var newPath = new Stack<Cave>(path);
                newPath.Push(cave);
                FindPaths_1(newPath, ref pathCount, end);
            }
        }

        public override ValueTask<string> Solve_2()
        {
            Dictionary<string, Cave> caveSystem = new();
            foreach (var line in input)
            {
                var split = line.Split('-');
                string startCave = split[0];
                string endCave = split[1];
                if (!caveSystem.ContainsKey(startCave))
                {
                    caveSystem.Add(startCave, new Cave(startCave, Char.IsLower(startCave[0])));
                }
                if (!caveSystem.ContainsKey(endCave))
                {
                    caveSystem.Add(endCave, new Cave(endCave, Char.IsLower(endCave[0])));
                }
                caveSystem[startCave].AddConnectedCave(caveSystem[endCave]);
                caveSystem[endCave].AddConnectedCave(caveSystem[startCave]);
            }
            Stack<Cave> path = new();
            path.Push(caveSystem["start"]);
            long pathCount = 0;
            FindPaths_2(path, ref pathCount, false, caveSystem["end"]);
            return new(pathCount.ToString());
        }

        public void FindPaths_2(Stack<Cave> path, ref long pathCount, bool twiceVisited, Cave end)
        {
            foreach (var cave in path.Peek().ConnectedCaves)
            {
                if (cave.Name == end.Name)
                {
                    pathCount++;
                    continue;
                }
                if (cave.Name == "start")
                {
                    continue;
                }
                if (cave.IsSmall && path.Contains(cave))
                {
                    if(twiceVisited)
                    {
                        continue;
                    }
                    twiceVisited = true; // a small cave is now added to the path for the 2nd time
                    var newPath1 = new Stack<Cave>(path);
                    var newPath2 = new Stack<Cave>(path);
                    newPath2.Push(cave);
                    FindPaths_2(newPath2, ref pathCount, twiceVisited, end);
                    continue;
                }                
                var newPath = new Stack<Cave>(path);
                newPath.Push(cave);
                FindPaths_2(newPath, ref pathCount, twiceVisited, end);
            }
        }

        public void PrintPath(Stack<Cave> path)
        {
            while(path.Count > 0)
            {
                Console.Write(path.Pop().Name);
            }
            Console.WriteLine();
        }
    }    

    public class Cave
    {
        private string name;
        private bool isSmall;
        List<Cave> connectedCaves;

        public Cave(string name, bool isSmall)
        {
            this.name = name;
            this.isSmall = isSmall; 
            this.connectedCaves = new();
        }
         
        public string Name { get => name; }

        public bool IsSmall { get => isSmall; }

        public List<Cave> ConnectedCaves { get => connectedCaves; }

        public void AddConnectedCave(Cave predecessor)
        {
            this.connectedCaves.Add(predecessor);
        }
    }
}
