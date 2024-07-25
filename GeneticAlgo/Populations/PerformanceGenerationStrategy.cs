using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Populations
{
    [DisplayName("Performance")]
    public class PerformanceGenerationStrategy : IGenerationStrategy
    {
        #region Constructors
        public PerformanceGenerationStrategy()
        {
            GenerationsNumber = 1;
        }

        public PerformanceGenerationStrategy(int generationsNumber)
        {
            GenerationsNumber = generationsNumber;
        }
        #endregion

        #region Properties
        public int GenerationsNumber { get; set; }
        #endregion

        #region Methods
        public void RegisterNewGeneration(MyPopulation population)
        {

            if (population.Generations.Count > GenerationsNumber)
            {
                population.Generations.RemoveAt(0);
            }
        }
        #endregion
    }
}
