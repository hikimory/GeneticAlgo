using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Terminations
{
    [DisplayName("Fitness Threshold")]
    public class FitnessThresholdTermination : TerminationBase
    {
        #region Constructors
        public FitnessThresholdTermination() : this(1.00)
        {
        }

        public FitnessThresholdTermination(double expectedFitness)
        {
            ExpectedFitness = expectedFitness;
        }
        #endregion

        #region Properties
        public double ExpectedFitness { get; set; }
        #endregion

        #region implemented abstract members of TerminationBase
        protected override bool PerformHasReached(GeneticAlgorithm geneticAlgorithm)
        {
            return geneticAlgorithm.BestChromosome.Fitness >= ExpectedFitness;
        }
        #endregion
    }
}
