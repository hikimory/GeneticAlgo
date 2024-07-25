using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Chromosomes
{
    public class Data
    {
        List<int[]>[] _columnValues;
        List<int[]>[] _rowValues;
        int[][] _columnSequences;
        int[][] _rowSequences;

        public List<int[]>[] ColumnValues => _columnValues;
        public List<int[]>[] RowValues => _rowValues;

        public int[][] ColumnSequences => _columnSequences;
        public int[][] RowSequences => _rowSequences;

        public Data(List<int[]>[] rowValues, List<int[]>[] columnValues,
                    int[][] rowSequences, int[][] columnSequences) 
        {
            _rowValues = rowValues;
            _columnValues = columnValues;
            _rowSequences = rowSequences;
            _columnSequences = columnSequences;
        }
    }
}
