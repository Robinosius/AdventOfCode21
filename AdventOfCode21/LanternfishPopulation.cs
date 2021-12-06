using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{
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
            for(int j = 0; j < steps; j++)
            {
                Step();
            }
            long size = 0;
            for(int i = 0; i < population.Length; i++)
            {
                size += population[i];
            }
            return size;
        }

        public void Step()
        {
            long newFish = population[0];
            for(int i = 0; i < population.Length - 1; i++)
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
            foreach(var fish in initial)
            {
                population.Add(new Lanternfish(fish));
            }
        }

        public int SizeAfterNSteps(int steps)
        {
            for(int i = 0; i < steps; i++)
            {
                Step();
            }
            return population.Count;
        }

        public void Step()
        {
            var initialSize = population.Count;
            for(int i = 0; i < initialSize; i++)
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
            if(counter == 0)
            {
                counter = 6;
                return true;
            }
            counter--;
            return false;
        }
    }
}
