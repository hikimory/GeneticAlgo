using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Terminations
{
    [DisplayName("Generation Number")]
    public class GenerationNumberTermination : TerminationBase
    {
        #region Constructors
        public GenerationNumberTermination() : this(100)
        {
        }

        public GenerationNumberTermination(int expectedGenerationNumber)
        {
            ExpectedGenerationNumber = expectedGenerationNumber;
        }
        #endregion

        #region Properties
        public int ExpectedGenerationNumber { get; set; }
        #endregion

        #region Methods
        protected override bool PerformHasReached(GeneticAlgorithm geneticAlgorithm)
        {
            return geneticAlgorithm.GenerationsNumber >= ExpectedGenerationNumber;
        }
        #endregion
    }
}
