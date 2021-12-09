using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_06 : BaseDay
    {
        string input;

        public Day_06()
        {
            input = File.ReadAllLines(InputFilePath).First();
        }

        public override ValueTask<string> Solve_1()
        {
            List<int> initialPopulation = input.Split(",").Select(val => Int32.Parse(val)).ToList();
            //var population = new LanternfishPopulation(initialPopulation);
            var population = new LanternfishWorldDominationLight(initialPopulation);
            return new(population.SizeAfterNSteps(80).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<int> initialPopulation = input.Split(",").Select(val => Int32.Parse(val)).ToList();
            var population = new LanternfishWorldDomination(initialPopulation);
            return new(population.SizeAfterNSteps(256).ToString());
        }
    }

    class LanternfishWorldDomination
    {
        long[] population = new long[9];

        public LanternfishWorldDomination(List<int> initial)
        {
            foreach (var fish in initial)
            {
                population[fish]++;
            }
        }

        public long SizeAfterNSteps(int steps)
        {
            for (int j = 0; j < steps; j++)
            {
                Step();
            }
            long size = 0;
            for (int i = 0; i < population.Length; i++)
            {
                size += population[i];
            }
            return size;
        }

        public void Step()
        {
            long newFish = population[0];
            for (int i = 0; i < population.Length - 1; i++)
            {
                population[i] = population[i + 1];
            }
            population[6] += newFish;
            population[8] = newFish;
        }
    }


    class LanternfishWorldDominationLight
    {
        int[] population = new int[9];

        public LanternfishWorldDominationLight(List<int> initial)
        {
            foreach (var fish in initial)
            {
                population[fish]++;
            }
        }

        public int SizeAfterNSteps(int steps)
        {
            for (int j = 0; j < steps; j++)
            {
                Step();
            }
            int size = 0;
            for (int i = 0; i < population.Length; i++)
            {
                size += population[i];
            }
            return size;
        }

        public void Step()
        {
            int newFish = population[0];
            for (int i = 0; i < population.Length - 1; i++)
            {
                population[i] = population[i + 1];
            }
            population[6] += newFish;
            population[8] = newFish;
        }
    }


    class LanternfishPopulation
    {
        List<Lanternfish> population;

        public LanternfishPopulation(List<int> initial)
        {
            this.population = new();
            foreach (var fish in initial)
            {
                population.Add(new Lanternfish(fish));
            }
        }

        public int SizeAfterNSteps(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                Step();
            }
            return population.Count;
        }

        public void Step()
        {
            var initialSize = population.Count;
            for (int i = 0; i < initialSize; i++)
            {
                var newFish = population[i].Age();
                if (newFish)
                {
                    population.Add(new Lanternfish(8));
                }
            }
        }
    }

    class Lanternfish
    {
        int counter;

        public Lanternfish(int initialTimer)
        {
            this.counter = initialTimer;
        }

        public bool Age()
        {
            if (counter == 0)
            {
                counter = 6;
                return true;
            }
            counter--;
            return false;
        }
    }
}
