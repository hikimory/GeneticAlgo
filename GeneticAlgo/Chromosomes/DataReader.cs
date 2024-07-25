using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Chromosomes
{
    public class DataReader
    {
        private string _filename;
        public DataReader(string fileName)
        {
            _filename = fileName;
        }
        public (int[][] rowValues, int[][] columnValues) GetDataFromFile()
        {
            int[][] columnValues = null;
            int[][] rowValues = null;
            using (StreamReader sr = new StreamReader(_filename))
            {
                int columns = 0, rows = 0;
                string line;
                if ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(' ');
                    columns = int.Parse(values[0]);
                    rows = int.Parse(values[1]);
                }

                columnValues = new int[columns][];
                rowValues = new int[rows][];

                ReadValues(sr, columnValues, columns);
                ReadValues(sr, rowValues, rows);
            }
            return (rowValues, columnValues);
        }

        private void ReadValues(StreamReader sr, int[][] arr, int size)
        {
            int i = 0; string line;
            while (i < size && (line = sr.ReadLine()) != null)
            {
                string[] sequences = line.Split(' ');
                arr[i] = new int[sequences.Length];
                for (int j = 0; j < sequences.Length; j++)
                {
                    arr[i][j] = int.Parse(sequences[j]);
                }
                i++;
            }
        }
    }
}
