using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Terminations
{
    public abstract class TerminationBase : ITermination
    {
        #region Fields
        private bool m_hasReached;
        #endregion

        #region Methods
        public bool HasReached(GeneticAlgorithm geneticAlgorithm)
        {
            m_hasReached = PerformHasReached(geneticAlgorithm);
            return m_hasReached;
        }

        public override string ToString()
        {
            return $"{GetType().Name} (HasReached: {m_hasReached})";
        }

        protected abstract bool PerformHasReached(GeneticAlgorithm geneticAlgorithm);
        #endregion
    }
}
