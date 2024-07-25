using GeneticAlgo.Randomizations;

namespace GeneticAlgo.Chromosomes
{
    public class MyChromosome
    {
        private enum Direction
        {
            Row,
            Column
        }

        private MyGene[,] _genes;
        private int _сolumns;
        private int _rows;

        public MyChromosome(int rows, int columns)
        {
            _rows = rows;
            _сolumns = columns;
            _genes = new MyGene[_rows, _сolumns]; ;
        }

        public double? Fitness { get; set; }
        public int Сolumns => _сolumns;
        public int Rows => _rows;
        public MyGene[,] Genes => _genes;

        public static bool operator ==(MyChromosome first, MyChromosome second)
        {
            if (Object.ReferenceEquals(first, second))
            {
                return true;
            }

            if (((object)first == null) || ((object)second == null))
            {
                return false;
            }

            return first.CompareTo(second) == 0;
        }

        public static bool operator !=(MyChromosome first, MyChromosome second)
        {
            return !(first == second);
        }

        public static bool operator <(MyChromosome first, MyChromosome second)
        {
            if (Object.ReferenceEquals(first, second))
            {
                return false;
            }

            if ((object)first == null)
            {
                return true;
            }

            if ((object)second == null)
            {
                return false;
            }

            return first.CompareTo(second) < 0;
        }

        public static bool operator >(MyChromosome first, MyChromosome second)
        {
            return !(first == second) && !(first < second);
        }

        public void GenerateGenes(Data data)
        {
            int i = 0;
            while (i < _сolumns && i < _rows)
            {
                if(i < _сolumns)
                {
                    var combines = data.ColumnValues[i];
                    int index = 0;
                    if (combines.Count() > 1)
                    {
                        index = RandomizationProvider.Current.Next(0, combines.Count());
                    }

                    int[] combine = combines[index];
                    for (int j = 0; j < combine.Length; j++)
                    {
                        _genes[j, i].Value = combine[j];
                    }
                }
                if (i < _rows)
                {
                    var combines = data.RowValues[i];
                    int index = 0;
                    if (combines.Count() > 1)
                    {
                        index = RandomizationProvider.Current.Next(0, combines.Count());
                    }

                    int[] combine = combines[index];

                    for (int j = 0; j < combine.Length; j++)
                    {
                        _genes[i, j].Value = combine[j];
                    }
                }
                i++;
            }

            //for (int i = 0; i < _сolumns; i++)
            //{
            //    var combines = data.ColumnValues[i];
            //    int index = 0;
            //    if (combines.Count() > 1)
            //    {
            //        index = RandomizationProvider.Current.Next(0, combines.Count());
            //    }

            //    int[] combine = combines[index];
            //    for (int j = 0; j < combine.Length; j++)
            //    {
            //       _genes[j, i].Value = combine[j];
            //    }
            //}

            //for (int i = 0; i < _rows; i++)
            //{
            //    var combines = data.RowValues[i];
            //    int index = 0;
            //    if (combines.Count() > 1)
            //    {
            //        index = RandomizationProvider.Current.Next(0, combines.Count());
            //    }

            //    int[] combine = combines[index];

            //    for (int j = 0; j < combine.Length; j++)
            //    {
            //        _genes[i, j].Value = combine[j];
            //    }
            //}
        }

        public void EvaluateFitness(Data data)
        {
            int len = _rows + _сolumns;
            int correct = 0;

            for (int i = 0; i < _rows; i++)
            {
                if (IsValueCorrect(ReadLine(i, _сolumns, Direction.Row), data.RowSequences[i]))
                    correct++;
            }

            for (int i = 0; i < _сolumns; i++)
            {
                if (IsValueCorrect(ReadLine(i, _rows, Direction.Column), data.ColumnSequences[i]))
                    correct++;
            }
            Fitness = (double)correct / (double)len;
        }

        private bool IsValueCorrect(List<int> val, int[] arr)
        {
            bool isLengthEqual = arr.Length == val.Count;
            bool areValuesEqual = arr.SequenceEqual(val);

            return isLengthEqual && areValuesEqual;
        }

        public void PrintGenes(int a, int b)
        {
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (_genes[i, j].Value == 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    
                    Console.Write("██");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        private List<int> ReadLine(int ind, int range, Direction dirc)
        {
            List<int> result = new List<int>();
            int count = 0;

            for (int i = 0; i < range; i++)
            {
                if (dirc == Direction.Column ? _genes[i, ind].Value == 1 : _genes[ind, i].Value == 1)
                {
                    count++;
                }
                else if (count > 0)
                {
                    result.Add(count);
                    count = 0;
                }
            }
            if (count > 0)
            {
                result.Add(count);
            }
            return result;
        }

        public MyChromosome CreateNew()
        {
            return new MyChromosome(_rows, _сolumns);
        }

        public virtual MyChromosome Clone()
        {
            var clone = CreateNew();
            clone.Fitness = Fitness;
            return clone;
        }

        public MyGene GetGene(int row, int col)
        {
            return _genes[row, col];
        }

        public MyGene[,] GetGenes()
        {
            return _genes;
        }

        public void ReplaceGenesRow(int ind, MyGene[,] genes)
        {
            for (int i = 0; i < _сolumns; i++)
            {
                _genes[ind, i].Value = genes[ind, i].Value;
            }
        }

        public void ReplaceGenesColumn(int ind, MyGene[,] genes)
        {
            for (int i = 0; i < _rows; i++)
            {
                _genes[i, ind].Value = genes[i, ind].Value;
            }
        }

        public bool MutateGenesRow(int i, Data data)
        {
            if (data.RowValues[i].Count < 2) return false;
            var combines = data.RowValues[i];
            int index = RandomizationProvider.Current.Next(0, combines.Count());
            int[] combine = combines[index];

            for (int j = 0; j < combine.Length; j++)
            {
                _genes[i, j].Value = combine[j];
            }
            return true;
        }

        public bool MutateGenesColumn(int i, Data data)
        {
            if(data.ColumnValues[i].Count < 2) return false;
            var combines = data.ColumnValues[i];
            int index = RandomizationProvider.Current.Next(0, combines.Count());
            int[] combine = combines[index];

            for (int j = 0; j < combine.Length; j++)
            {
                _genes[j, i].Value = combine[j];
            }
            return true;
        }


        public int CompareTo(MyChromosome other)
        {
            if (other == null)
            {
                return -1;
            }

            var otherFitness = other.Fitness;

            if (Fitness == otherFitness)
            {
                return 0;
            }

            return Fitness > otherFitness ? 1 : -1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as MyChromosome;

            if (other == null)
            {
                return false;
            }

            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Fitness.GetHashCode();
        }
    }
}