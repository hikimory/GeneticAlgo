using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneticAlgo.Utilities
{
    public class TablePrinter
    {
        private readonly string[] titles;
        private readonly List<int> lengths;
        private readonly List<string[]> rows = new List<string[]>();

        public TablePrinter(params string[] titles)
        {
            this.titles = titles;
            lengths = titles.Select(t => t.Length).ToList();
        }

        public void AddRow(params object[] row)
        {
            if (row.Length != titles.Length)
            {
                throw new System.Exception($"Added row length [{row.Length}] is not equal to title row length [{titles.Length}]");
            }

            var newRow = row.Select(o => o.ToString()).ToArray();
            rows.Add(newRow);

            for (int i = 0; i < titles.Length; i++)
            {
                if (newRow[i].Length > lengths[i])
                {
                    lengths[i] = newRow[i].Length;
                }
            }
        }

        public void Print()
        {
            PrintBorder();
            StringBuilder line = new StringBuilder();
            for (int i = 0; i < titles.Length; i++)
            {
                line.Append($"| {titles[i].PadRight(lengths[i])} ");
            }
            line.Append('|');
            System.Console.WriteLine(line.ToString());
            PrintBorder();
            foreach (var row in rows)
            {
                line.Clear();
                for (int i = 0; i < row.Length; i++)
                {
                    if (int.TryParse(row[i], out int n))
                    {
                        line.Append($"| {row[i].PadLeft(lengths[i])} "); // numbers are padded to the left
                    }
                    else
                    {
                        line.Append($"| {row[i].PadRight(lengths[i])} ");
                    }
                }
                line.Append('|');
                System.Console.WriteLine(line.ToString());
                PrintBorder();
            }
        }

        private void PrintBorder()
        {
            lengths.ForEach(l => System.Console.Write($"+-{new string('-', l)}-"));
            System.Console.WriteLine("+");
        }
    }
}
