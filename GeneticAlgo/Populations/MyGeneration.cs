using GeneticAlgo.Chromosomes;

namespace GeneticAlgo.Populations
{
    public class MyGeneration
    {
        public MyGeneration(int number, IList<MyChromosome> chromosomes)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    $"Generation number {number} is invalid. Generation number should be positive and start in 1.");
            }

            if (chromosomes == null || chromosomes.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(chromosomes), "A generation should have at least 2 chromosomes.");
            }

            Number = number;
            CreationDate = DateTime.Now;
            Chromosomes = chromosomes;
        }

        #region Properties
        public int Number { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IList<MyChromosome> Chromosomes { get; internal set; }

        public MyChromosome BestChromosome { get; internal set; }
        #endregion

        #region Methods
        public void FindBestChromosome(int chromosomesNumber)
        {
            Chromosomes = Chromosomes
                .Where(ValidateChromosome)
                .OrderByDescending(c => c.Fitness.Value)
                .ToList();

            if (Chromosomes.Count > chromosomesNumber)
            {
                Chromosomes = Chromosomes.Take(chromosomesNumber).ToList();
            }

            BestChromosome = Chromosomes.First();
        }

        private static bool ValidateChromosome(MyChromosome chromosome)
        {
            if (!chromosome.Fitness.HasValue)
            {
                throw new InvalidOperationException("There is unknown problem in current generation, because a chromosome has no fitness value.");
            }

            return true;
        }

        #endregion
    }
}
