using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Populations
{
    public interface IGenerationStrategy
    {
        #region Methods
        void RegisterNewGeneration(MyPopulation population);
        #endregion
    }
}
