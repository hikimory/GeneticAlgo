using GeneticAlgo.Chromosomes;
using GeneticAlgo.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Crossovers
{
    public class Crossover
    {
        #region Properties
        public double MixProbability { get; set; }
        #endregion

        #region Constructors
        public Crossover(double mixProbability)
        {
            MixProbability = mixProbability;
        }

        public Crossover() : this(0.75)
        {
        }
        #endregion

        #region Methods
        public IList<MyChromosome> Cross(IList<MyChromosome> parents)
        {
            if (parents == null)
            {
                throw new ArgumentNullException("No selected parents");
            }

            if (parents.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(parents), "The number of parents should be the same of ParentsNumber.");
            }

            return PerformCross(parents);
        }
        private IList<MyChromosome> PerformCross(IList<MyChromosome> parents)
        {
            List<MyChromosome> result = new List<MyChromosome>();
            for (int i = 0; i < parents.Count - 1; i++)
            {
                var child = CreateChildren(parents[i], parents[i + 1]);
                result.AddRange(child);
            }
            //for (int i = 0; i < parents.Count - 1; i+=2)
            //{
            //    var child = CreateChildren(parents[i], parents[i + 1]);
            //    result.AddRange(child);
            //}
            return result;
        }

        private List<MyChromosome> CreateChildren(MyChromosome firstParent, MyChromosome secondParent)
        {
            var firstChild = firstParent.CreateNew();
            var secondChild = secondParent.CreateNew();

            for (int i = 0; i < firstParent.Rows; i++)
            {
                if (RandomizationProvider.Current.NextDouble() < MixProbability)
                {
                    firstChild.ReplaceGenesRow(i, secondParent.GetGenes());
                    secondChild.ReplaceGenesRow(i, firstParent.GetGenes());
                }
                else
                {
                    firstChild.ReplaceGenesRow(i, firstParent.GetGenes());
                    secondChild.ReplaceGenesRow(i, secondParent.GetGenes());
                }
            }
            for (int i = 0; i < firstParent.Сolumns; i++)
            {
                if (RandomizationProvider.Current.NextDouble() < MixProbability)
                {
                    firstChild.ReplaceGenesColumn(i, secondParent.GetGenes());
                    secondChild.ReplaceGenesColumn(i, firstParent.GetGenes());
                }
                else
                {
                    firstChild.ReplaceGenesColumn(i, firstParent.GetGenes());
                    secondChild.ReplaceGenesColumn(i, secondParent.GetGenes());
                }
            }
            return new List<MyChromosome> { firstChild, secondChild };
        }
        #endregion
    }
}
