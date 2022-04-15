using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VulcanStocksKNNResearchMVVM.Models
{
    public class StrategyWriter
    {
        private string Path;

        private string[][] DataSet;
        private string[] Data { get; set; }
        private string Ticker { get; set; }
        private string[][] Strategy { get; set; }

        public void Write(string ticker)
        {
            Path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv";
            Ticker = ticker;

            Setup();
            FillStrategy();

        }

        private void Setup()
        {
            Data = File.ReadAllLines(Path);

            DataSet = new string[Data.Length][];
            for (int i = 0; i < Data.Length; i++)
            {
                string[] temp = Data[i].Split(',');
                DataSet[i] = new string[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    DataSet[i][j] = temp[j];
                }
            }
        }
        private void FillStrategy()
        {

        }


    }
}
