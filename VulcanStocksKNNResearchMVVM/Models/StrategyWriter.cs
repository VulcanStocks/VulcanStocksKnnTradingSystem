using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VulcanStocksKNNResearchMVVM.Indicators;
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
        public int StoppLoss { get; set; }
        public int Target { get; set; }
        public void Write(string ticker, int stoppLoss, int target)
        {
            Path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv";
            Ticker = ticker;
            StoppLoss = stoppLoss;
            Target = target;

            Setup();
            FillStrategy();
            WriteStrategy();

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
            RSI rsi = new RSI();
            Strategy = new string [DataSet.Length][];
            for (int i = 1; i < DataSet.Length; i++)
            {
                Strategy[i] = new string[3];

                string[] temp = DataSet[i];

                float price = float.Parse(temp[4].Replace('.',','));
                float? RSI = rsi.Calculate(i, price);
                bool IsValid = CheckIfValid(i, price);

                Strategy [i][0] = price.ToString();
                Strategy [i][1] = RSI.ToString();
                Strategy [i][2] = IsValid.ToString();

            }
        }

        private bool CheckIfValid(int i, float price)
        {
            int localTime = i;
            while(true)
            {
                if (localTime >= DataSet.Length)
                {
                    return false;
                }
                float localPrice = float.Parse(DataSet[localTime][4].Replace('.', ','));
                float temp = ((localPrice / price) - 1) * 100;
                Console.WriteLine(localPrice);
                if (temp >= Target )
                {
                    return true;
                }
                else if(temp <= StoppLoss)
                {
                    return false;
                }
                localTime++;
            }
            
        }

        private void WriteStrategy()
        {
            //write strategy into csv matrix 

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + Ticker + "Strategy.csv";
            string[] lines = new string[Strategy.Length];
            for (int i = 0; i < Strategy.Length; i++)
            {
                Console.WriteLine(Strategy[i][0]);
                Console.WriteLine(Strategy[i][2]);
                Console.WriteLine(Strategy[i][3]);

                Console.WriteLine(string.Join(",", Strategy[i]));
                lines[i] = string.Join(",", Strategy[i]);
            }
            File.WriteAllLines(path, lines);    

        }



    }
}
