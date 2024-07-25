namespace GeneticAlgo.Utilities
{
    public static class CombinationsGenerator
    {
        public static List<int[]> Generate(int[] sequences, int length)
        {
            int[] resultArray = new int[length];
            List<int[]> allCombinations = new List<int[]>();

            FindAllCombinations(resultArray, sequences, 0, 0, allCombinations);

            return allCombinations;
        }

        static void FindAllCombinations(int[] resultArray, int[] sequences, int seqIndex, int startIndex, List<int[]> allCombinations)
        {
            if (seqIndex == sequences.Length)
            {
                int[] combination = new int[resultArray.Length];
                Array.Copy(resultArray, combination, resultArray.Length);
                allCombinations.Add(combination);
                return;
            }

            int seqLen = sequences[seqIndex];
            for (int i = startIndex; i <= resultArray.Length - seqLen; i++)
            {
                if (IsPlaceFree(resultArray, i, seqLen))
                {
                    PlaceSequence(resultArray, i, seqLen, 1);

                    FindAllCombinations(resultArray, sequences, seqIndex + 1, i + seqLen + 1, allCombinations);

                    PlaceSequence(resultArray, i, seqLen, 0);
                }
            }
        }

        static bool IsPlaceFree(int[] array, int start, int length)
        {
            for (int i = start; i < start + length; i++)
            {
                if (array[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void PlaceSequence(int[] array, int start, int length, int value)
        {
            for (int i = start; i < start + length; i++)
            {
                array[i] = value;
            }
        }
    }
}
