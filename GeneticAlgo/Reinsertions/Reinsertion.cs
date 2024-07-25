using GeneticAlgo.Chromosomes;
using GeneticAlgo.Populations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Reinsertions
{
    public class Reinsertion
    {
        #region Properties
        public bool CanCollapse { get; private set; }

        public bool CanExpand { get; private set; }
        #endregion

        public Reinsertion(bool canCollapse, bool canExpand)
        {
            CanCollapse = canCollapse;
            CanExpand = canExpand;
        }

        #region Methods
        public void SelectChromosomes(MyPopulation population, IList<MyChromosome> offspring, IList<MyChromosome> parents)
        {
            if (!CanExpand && offspring.Count < population.MinSize)
            {
                throw new Exception("Cannot expand the number of chromosome in population. Try another reinsertion!");
            }

            if (!CanCollapse && offspring.Count > population.MaxSize)
            {
                throw new Exception("Cannot collapse the number of chromosome in population. Try another reinsertion!");
            }

            PerformSelectChromosomes(population, offspring, parents);
        }

        private void PerformSelectChromosomes(MyPopulation population, IList<MyChromosome> offspring, IList<MyChromosome> parents)
        {
            var diff = population.MinSize - offspring.Count;

            if (diff > 0)
            {
                var bestParents = parents.OrderByDescending(p => p.Fitness).Take(diff).ToList();

                for (int i = 0; i < bestParents.Count; i++)
                {
                    offspring.Add(bestParents[i]);
                }
            }
        }
        #endregion
    }
}
