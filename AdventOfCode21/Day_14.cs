using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_14 : BaseDay
    {
        private List<string> input;

        public Day_14()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            var copy = new List<string>(input);
            string polymer = copy[0];
            copy.RemoveRange(0, 2);
            List<string> pairs = copy.Select(line => line.Split(" -> ")[0]).ToList();
            List<char> insertions = copy.Select(line => line.Split(" -> ")[1][0]).ToList();

            Dictionary<string, long> pairCount = new();
            Dictionary<char, long> charCount = new();

            for (int i = 1; i < polymer.Count(); i++)
            {
                char first = polymer[i - 1];
                char second = polymer[i];
                string pair = first.ToString() + second.ToString();
                if (!pairCount.ContainsKey(pair))
                {
                    pairCount.Add(pair, 1);
                }
                else
                {
                    pairCount[pair]++;
                }
                if (!charCount.ContainsKey(first))
                {
                    charCount.Add(first, 0);
                }
                if (i == polymer.Count() - 1 && !charCount.ContainsKey(second))
                {
                    charCount.Add(second, 1);
                }
                charCount[first]++;
            }

            Dictionary<string, long> pairBuffer = new();
            int steps = 10;
            for (int i = 0; i < steps; i++)
            {
                pairBuffer.Clear(); // pairs added in this step, added to pair dict at end of step
                foreach (var pair in pairCount.Keys)
                {
                    if (pairs.Contains(pair))
                    {
                        char first = pair[0];
                        char second = pair[1];
                        char insert = insertions[pairs.IndexOf(pair)];
                        string p1 = first.ToString() + insert.ToString();
                        string p2 = insert.ToString() + second.ToString();
                        if (!pairBuffer.ContainsKey(p1))
                        {
                            pairBuffer.Add(p1, 0);
                        }
                        if (!pairBuffer.ContainsKey(p2))
                        {
                            pairBuffer.Add(p2, 0);
                        }
                        if (!charCount.ContainsKey(insert))
                        {
                            charCount.Add(insert, 0);
                        }
                        pairBuffer[p1] += pairCount[pair];
                        pairBuffer[p2] += pairCount[pair];
                        charCount[insert] += pairCount[pair];
                        if (pairCount.ContainsKey(pair))
                        {
                            pairCount[pair] = 0;
                        }
                    }
                }
                foreach (var pair in pairBuffer.Keys)
                {
                    if (!pairCount.ContainsKey(pair))
                    {
                        pairCount.Add(pair, 0);
                    }
                    pairCount[pair] += pairBuffer[pair];
                }
            }
            return new((charCount.Values.Max() - charCount.Values.Min()).ToString());
        }   

        public override ValueTask<string> Solve_2()
        {
            var copy = new List<string>(input);
            string polymer = copy[0];
            copy.RemoveRange(0, 2);
            List<string> pairs = copy.Select(line => line.Split(" -> ")[0]).ToList();
            List<char> insertions = copy.Select(line => line.Split(" -> ")[1][0]).ToList();

            Dictionary<string, long> pairCount = new();
            Dictionary<char, long> charCount = new();

            for (int i = 1; i < polymer.Count(); i++)
            {
                char first = polymer[i - 1];
                char second = polymer[i];
                string pair = first.ToString() + second.ToString();
                if (!pairCount.ContainsKey(pair))
                {
                    pairCount.Add(pair, 1);
                }
                else
                {
                    pairCount[pair]++;
                }
                if (!charCount.ContainsKey(first))
                {
                    charCount.Add(first, 0);
                }
                if (i == polymer.Count() - 1 && !charCount.ContainsKey(second))
                {
                    charCount.Add(second, 1);
                }
                charCount[first]++;                
            }

            Dictionary<string, long> pairBuffer = new();
            List<string> toRemove = new();
            int steps = 40;
            for (int i = 0; i < steps; i++)
            {
                pairBuffer.Clear(); // pairs added in this step, added to pair dict at end of step
                foreach (var pair in pairCount.Keys)
                {
                    if (pairs.Contains(pair))
                    {
                        char first = pair[0];
                        char second = pair[1];
                        char insert = insertions[pairs.IndexOf(pair)];
                        string p1 = first.ToString() + insert.ToString();
                        string p2 = insert.ToString() + second.ToString();
                        if (!pairBuffer.ContainsKey(p1))
                        {
                            pairBuffer.Add(p1, 0);
                        }
                        if (!pairBuffer.ContainsKey(p2))
                        {
                            pairBuffer.Add(p2, 0);
                        }
                        if (!charCount.ContainsKey(insert))
                        {
                            charCount.Add(insert, 0);
                        }
                        pairBuffer[p1] += pairCount[pair];
                        pairBuffer[p2] += pairCount[pair];
                        charCount[insert] += pairCount[pair];
                        pairCount[pair] = 0;
                    }
                }
                foreach (var pair in pairBuffer.Keys)
                {
                    if (!pairCount.ContainsKey(pair))
                    {
                        pairCount.Add(pair, 0);
                    }
                    pairCount[pair] += pairBuffer[pair];
                }
            }
            return new((charCount.Values.Max() - charCount.Values.Min()).ToString());
        }

    }
}
