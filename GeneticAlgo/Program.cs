using GeneticAlgo.Chromosomes;
using GeneticAlgo.Crossovers;
using GeneticAlgo.Mutations;
using GeneticAlgo.Populations;
using GeneticAlgo.Selections;
using GeneticAlgo.Terminations;
using GeneticAlgo.Utilities;

namespace GeneticAlgo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();
            var selection = new Selection();
            var crosover = new Crossover(0.75);
            var mutate = new Mutation(data, 0.45);
            var population = new MyPopulation(600, 600, data);
            var ga = new GeneticAlgorithm(population, selection, crosover, mutate);
            //ga.Termination = new GenerationNumberTermination(300);
            ga.Termination = new FitnessThresholdTermination(1.0);
            ga.Start();
            Console.WriteLine();
            ga.Population.BestChromosome.PrintGenes(data.RowValues.Length, data.ColumnValues.Length);
        }

        private static Data GetData()
        {
            DataReader reader = new DataReader("Test.txt");
            var tuple = reader.GetDataFromFile();
            var rowCombines = GenerateCombines(tuple.rowValues, tuple.columnValues.Length);
            var colCombines = GenerateCombines(tuple.columnValues, tuple.rowValues.Length);
            return new Data(rowCombines, colCombines, tuple.rowValues, tuple.columnValues);
        }

        private static List<int[]>[] GenerateCombines(int[][] arr, int range)
        {
            List<int[]>[] result = new List<int[]>[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                List<int[]> comb = CombinationsGenerator.Generate(arr[i], range);
                result[i] = comb;
            }
            return result;
        }
    }
}