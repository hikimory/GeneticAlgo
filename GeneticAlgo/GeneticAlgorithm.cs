using GeneticAlgo.Chromosomes;
using GeneticAlgo.Crossovers;
using GeneticAlgo.Mutations;
using GeneticAlgo.Populations;
using GeneticAlgo.Reinsertions;
using GeneticAlgo.Selections;
using GeneticAlgo.Terminations;
using System.Diagnostics;

namespace GeneticAlgo
{
    public sealed class GeneticAlgorithm
    {
        #region Fields
        private bool m_stopRequested;
        private readonly object m_lock = new object();
        private Stopwatch m_stopwatch;
        #endregion              

        #region Constructors
        public GeneticAlgorithm(
                          MyPopulation population,
                          Selection selection,
                          Crossover crosover,
                          Mutation mutation)
        {
            Population = population;
            Selection = selection;
            Crossover = crosover;
            Mutation = mutation;
            Reinsertion = new Reinsertion(true, true);
            Termination = new GenerationNumberTermination(1);
            TimeEvolving = TimeSpan.Zero;
        }
        #endregion

        #region Properties
        public MyPopulation Population { get; private set; }
        public Selection Selection { get; set; }
        public Crossover Crossover { get; set; }
        public Mutation Mutation { get; set; }
        public Reinsertion Reinsertion { get; set; }
        public ITermination Termination { get; set; }
        public int GenerationsNumber => Population.GenerationsNumber;
        public MyChromosome BestChromosome => Population.BestChromosome;
        public TimeSpan TimeEvolving { get; private set; }

        #endregion

        #region Methods

        public void Start()
        {
            m_stopwatch = Stopwatch.StartNew();
            Population.CreateInitialGeneration();
            m_stopwatch.Stop();
            TimeEvolving = m_stopwatch.Elapsed;
            
            if (Population.GenerationsNumber == 0)
            {
                throw new InvalidOperationException("Attempt to resume a genetic algorithm which was not yet started.");
            }

            if (Population.GenerationsNumber > 1)
            {
                if (Termination.HasReached(this))
                {
                    throw new InvalidOperationException($"Attempt to resume a genetic algorithm with a termination ({Termination}) already reached. Please, specify a new termination or extend the current one.");
                }
            }
            RunAlgorithm();
        }
       
        public void RunAlgorithm()
        {
            try
            {
                if (CheckTermination()) return;
                bool terminationConditionReached = false;
                do
                {
                    m_stopwatch.Restart();
                    terminationConditionReached = EvolveOneGeneration();
                    m_stopwatch.Stop();
                    TimeEvolving += m_stopwatch.Elapsed;
                }
                while (!terminationConditionReached);
            }
            catch
            {
                throw;
            }
        }

        public void Stop()
        {
            if (Population.GenerationsNumber == 0)
            {
                throw new InvalidOperationException("Attempt to stop a genetic algorithm which was not yet started.");
            }

            lock (m_lock)
            {
                m_stopRequested = true;
            }
        }

        private bool EvolveOneGeneration()
        {
            var parents = SelectParents();
            var offspring = Cross(parents);
            Mutate(offspring);
            Reinsert(offspring, parents);
            Population.CreateNewGeneration(offspring);
            return CheckTermination();
        }

        private bool CheckTermination()
        {
            if (Termination.HasReached(this))
            {
                return true;
            }
            return false;
        }

        private IList<MyChromosome> SelectParents()
        {
            return Selection.SelectChromosomes(Population.CurrentGeneration);
        }

        private IList<MyChromosome> Cross(IList<MyChromosome> parents)
        {
            return Crossover.Cross(parents);
        }

        private void Mutate(IList<MyChromosome> chromosomes)
        {
            Mutation.Mutate(chromosomes);
        }

        private void Reinsert(IList<MyChromosome> offspring, IList<MyChromosome> parents)
        {
            Reinsertion.SelectChromosomes(Population, offspring, parents);
        }
        #endregion
    }
}
