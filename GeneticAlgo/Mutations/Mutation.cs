using GeneticAlgo.Chromosomes;
using GeneticAlgo.Randomizations;

namespace GeneticAlgo.Mutations
{
    public class Mutation
    {
        #region Properties
        public double MutateProbability { get; set; }
        private Data Data { get; set; }
        #endregion

        #region Constructors
        public Mutation(Data data, double mutateProbability)
        {
            MutateProbability = mutateProbability;
            Data = data;
        }

        public Mutation(Data data) : this(data, 0.75)
        {
        }
        #endregion

        #region Methods
        public void Mutate(IList<MyChromosome> parents)
        {
            if (parents == null)
            {
                throw new ArgumentNullException("No selected parents");
            }

            PerformMutate(parents);
        }
        private void PerformMutate(IList<MyChromosome> parents)
        {
            //for (int i = 0; i < parents.Count; i++)
            //{
            //    for (int j = 0; j < parents[i].Rows; j++)
            //    {
            //        if (RandomizationProvider.Current.NextDouble() < MutateProbability)
            //        {
            //            parents[i].MutateGenesRow(j, Data);
            //        }
            //    }
            //    for (int j = 0; j < parents[i].Сolumns; j++)
            //    {
            //        if (RandomizationProvider.Current.NextDouble() < MutateProbability)
            //        {
            //            parents[i].MutateGenesColumn(j, Data);
            //        }
            //    }
            //}

            //Version 1
            for (int i = 0; i < parents.Count; i++)
            {
                if (RandomizationProvider.Current.NextDouble() < MutateProbability)
                {
                    int index = 0;
                    if (RandomizationProvider.Current.Next(0, 100) < 50)
                    {
                        do
                        {
                            index = RandomizationProvider.Current.Next(0, parents[i].Rows);
                        } while (!parents[i].MutateGenesRow(index, Data));
                    }
                    else
                    {
                        do
                        {
                            index = RandomizationProvider.Current.Next(0, parents[i].Сolumns);
                        } while (!parents[i].MutateGenesColumn(index, Data));
                    }
                }
            }
        }
        #endregion
    }
}
