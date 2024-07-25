using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Chromosomes
{
    public static class ChromosomeExtensions
    {
        public static void ValidateGenes(this IList<MyChromosome> chromosomes)
        {
            foreach (var item in chromosomes)
            {
                ValidateGenes(item);
            }
        }

        public static void ValidateGenes(this MyChromosome chromosome)
        {
            var genes = chromosome.GetGenes();
            for (int i = 0; i < chromosome.Rows; i++)
            {
                for (int j = 0; j < chromosome.Сolumns; j++)
                {
                    if (genes[i, j].Value == null)
                    {
                        throw new InvalidOperationException($"The chromosome '{chromosome.GetType().Name}' is generating genes with null value.");
                    }
                }
            }
        }
    }
}
