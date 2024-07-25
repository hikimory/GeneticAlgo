using GeneticAlgo.Chromosomes;
using GeneticAlgo.Populations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneticAlgo.Selections
{
    public sealed class Selection
    {
        #region Fields
        private readonly int m_minNumberChromosomes;
        #endregion

        #region Constructors
        public Selection() : this(-1)
        {
        }

        public Selection(int minNumberChromosomes)
        {
            m_minNumberChromosomes = minNumberChromosomes;
        }
        #endregion

        #region ISelection implementation
        public IList<MyChromosome> SelectChromosomes(MyGeneration generation)
        {
            if (generation.Chromosomes.Any(c => !c.Fitness.HasValue))
            {
                throw new ArgumentNullException(nameof(generation.Chromosomes));
            }

            return PerformSelectChromosomes(m_minNumberChromosomes, generation);
        }

        private IList<MyChromosome> PerformSelectChromosomes(int number, MyGeneration generation)
        {
            var ordered = generation.Chromosomes.OrderByDescending(c => c.Fitness);
            if(number > 0)
            {
                return ordered.Take(number).ToList();
            }
            return ordered.ToList();
        }
        #endregion
    }
}
