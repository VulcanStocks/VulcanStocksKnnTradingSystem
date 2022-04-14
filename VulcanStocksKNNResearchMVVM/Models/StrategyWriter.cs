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
        private string dataPath;
        private string[][] dataSet;
        private string[] data { get; set; }

        public void Write(string ticker)
        {
            dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv";
            Setup();

        }

        private void Setup()
        {
            data = File.ReadAllLines(dataPath);

            dataSet = new string[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                string[] temp = data[i].Split(',');
                dataSet[i] = new string[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    dataSet[i][j] = temp[j];
                }
            }
        }


    }
}
