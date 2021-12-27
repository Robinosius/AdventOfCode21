using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_13 : BaseDay
    {
        private List<string> input;

        public Day_13()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            List<int> xValues = new();
            List<int> yValues = new();
            List<string> foldingInstructions = new();
            foreach(var line in input)
            {
                if(line.StartsWith("fold"))
                {
                    foldingInstructions.Add(line.Split(' ')[2]);
                } 
                else if(line != "")
                {
                    xValues.Add(Int32.Parse(line.Split(',')[0]));
                    yValues.Add(Int32.Parse(line.Split(',')[1]));
                }                
            }
            bool[,] paper = new bool[xValues.Max() + 1, yValues.Max() + 1];
            for(int i = 0; i < xValues.Count; i++)
            {
                paper[xValues[i], yValues[i]] = true;
            }
            var folded = Fold(paper, foldingInstructions[0]);
            return new(CountDots(folded).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<int> xValues = new();
            List<int> yValues = new();
            List<string> foldingInstructions = new();
            foreach (var line in input)
            {
                if (line.StartsWith("fold"))
                {
                    foldingInstructions.Add(line.Split(' ')[2]);
                }
                else if (line != "")
                {
                    xValues.Add(Int32.Parse(line.Split(',')[0]));
                    yValues.Add(Int32.Parse(line.Split(',')[1]));
                }
            }
            bool[,] paper = new bool[xValues.Max() + 1, yValues.Max() + 1];
            for (int i = 0; i < xValues.Count; i++)
            {
                paper[xValues[i], yValues[i]] = true;
            }

            foreach(var instruction in foldingInstructions)
            {
                paper = Fold(paper, instruction);
                
            }
            //PrintCode(paper);
            return new(CountDots(paper).ToString());
        }

        public bool[,] Fold(bool[,] paper, string instruction)
        {
            char direction = instruction.Split('=')[0][0];
            int position = Int32.Parse(instruction.Split('=')[1]);
            bool[,] newPaper = new bool[0,0];
            if (direction == 'y')
            {
                for(int i = 1; i < paper.GetLength(1); i++)
                {
                    if(position - i < 0 || position + i >= paper.GetLength(1))
                    {
                        break;
                    }
                    for(int j = 0; j < paper.GetLength(0); j++)
                    {                        
                        paper[j, position - i] = paper[j, position - i] || paper[j, position + i];
                    }
                }
                newPaper = new bool[paper.GetLength(0), position + 1];
                for(int i = 0; i < paper.GetLength(0); i++)
                {
                    for (int j = 0; j <= position; j++)
                    {
                        newPaper[i, j] = paper[i, j];
                    }
                }
            }
            else
            {
                for(int i = 1; i < paper.GetLength(0); i++)
                {
                    if (position - i < 0 || position + i >= paper.GetLength(0))
                    {
                        break;
                    }
                    for (int j = 0; j < paper.GetLength(1); j++)
                    {
                        paper[position - i, j] = paper[position - i,j] || paper[position + i,j];
                    }
                }
                newPaper = new bool[position + 1, paper.GetLength(1)];
                for (int i = 0; i <= position; i++)
                {
                    for (int j = 0; j < paper.GetLength(1); j++)
                    {
                        newPaper[i, j] = paper[i, j];
                    }
                }
            }
            return newPaper;
        }

        public int CountDots(bool[,] paper)
        {
            int count = 0;
            for(int i = 0; i < paper.GetLength(0); i++)
            {
                for(int j = 0; j < paper.GetLength(1); j++)
                {
                    if (paper[i, j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void PrintCode(bool[,] paper)
        {
           
            for(int i = 0; i < paper.GetLength(1); i++)
            {
                for(int j = 0; j < paper.GetLength(0); j++)
                {
                    if (paper[j,i])
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine(); ;
            }
        }
    }
}
