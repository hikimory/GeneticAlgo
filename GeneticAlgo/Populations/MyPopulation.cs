using GeneticAlgo.Chromosomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeneticAlgo.Populations
{
    public class MyPopulation
    {
        public MyPopulation(int minSize, int maxSize, Data data)
        {
            if (minSize < 2)
            {
                throw new ArgumentOutOfRangeException("minSize", "The minimum size for a population is 2 chromosomes.");
            }

            if (maxSize < minSize)
            {
                throw new ArgumentOutOfRangeException("maxSize", "The maximum size for a population should be equal or greater than minimum size.");
            }

            CreationDate = DateTime.Now;
            MinSize = minSize;
            MaxSize = maxSize;
            this.data = data;
            Generations = new List<MyGeneration>();
            GenerationStrategy = new PerformanceGenerationStrategy(10);
        }

        #region Events
        public event EventHandler BestChromosomeChanged;
        #endregion

        #region Properties
        public DateTime CreationDate { get; protected set; }

        public IList<MyGeneration> Generations { get; protected set; }

        public Data data { get; protected set; }

        public MyGeneration CurrentGeneration { get; protected set; }

        public int GenerationsNumber { get; protected set; }


        public int MinSize { get; set; }


        public int MaxSize { get; set; }

        public MyChromosome BestChromosome { get; protected set; }

        public IGenerationStrategy GenerationStrategy { get; set; }

        #endregion

        #region Public methods

        public void CreateInitialGeneration()
        {
            Generations = new List<MyGeneration>();
            GenerationsNumber = 0;
            BestChromosome = null;

            var chromosomes = new List<MyChromosome>();
            for (int i = 0; i < MinSize; i++)
            {
                var c = new MyChromosome(data.RowValues.Length, data.ColumnValues.Length);
                c.GenerateGenes(data);
                chromosomes.Add(c);
            }
            CreateNewGeneration(chromosomes);
        }

        public void CreateNewGeneration(IList<MyChromosome> chromosomes)
        {
            chromosomes.ValidateGenes();
            CurrentGeneration = new MyGeneration(++GenerationsNumber, chromosomes);
            EvaluateFitness();
            Generations.Add(CurrentGeneration);
            GenerationStrategy.RegisterNewGeneration(this);
        }

        public void EvaluateFitness()
        {
            foreach (var chromosome in CurrentGeneration.Chromosomes)
            {
                chromosome.EvaluateFitness(data);
            }
            FindBestChromosome();
        }

        private void FindBestChromosome()
        {
            CurrentGeneration.FindBestChromosome(MaxSize);

            if (BestChromosome == null)
            {
                BestChromosome = CurrentGeneration.BestChromosome;
                return;
            }

            if (BestChromosome.Fitness < CurrentGeneration.BestChromosome.Fitness)
            {
                BestChromosome = CurrentGeneration.BestChromosome;

                OnBestChromosomeChanged(EventArgs.Empty);
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"BestChromosome = {BestChromosome.Fitness.Value:F2}");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Generations = {GenerationsNumber}");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"CurrentGeneration = {CurrentGeneration.BestChromosome.Fitness.Value:F2}");
        }
        #endregion

        #region Protected methods
        protected virtual void OnBestChromosomeChanged(EventArgs args)
        {
            BestChromosomeChanged?.Invoke(this, args);
        }
        #endregion
    }
}
