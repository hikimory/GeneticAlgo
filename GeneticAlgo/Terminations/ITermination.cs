using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Terminations
{
    public interface ITermination
    {
        #region Methods
        bool HasReached(GeneticAlgorithm geneticAlgorithm);
        #endregion
    }
}
