using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VulcanStocksKNNResearchMVVM.Indicators;
using System.Threading.Tasks;
using System.Windows;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class StrategyWriterModel
    {
        internal string Path;

        internal string[][] DataSet;
        internal string[] Data { get; set; }
        internal string Ticker { get; set; }
        internal string[,] Strategy { get; set; }
        internal int StopLoss { get; set; }
        internal int Target { get; set; }
        internal string IndicatorsXselected { get; set; }
        internal string IndicatorsYselected { get; set; }
        internal string StrategyName { get; set; }



        public virtual void Write(string ticker, int StopLoss, int target, string StrategyName, string IndicatorsXselected, string IndicatorsYselected)
        {
            if(ticker.Contains(".csv"))
            {
                Path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker;
            }
            else
            {
                Path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv";
            }
            
            Ticker = ticker;
            this.StopLoss = StopLoss;
            Target = target;
            this.StrategyName = StrategyName;
            this.IndicatorsXselected = IndicatorsXselected;
            this.IndicatorsYselected = IndicatorsYselected;

            Setup();
            FillStrategy();
            WriteStrategy();
           

        }

        internal void Setup()
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
        internal void FillStrategy()
        {
            float[] price = new float[Data.Length];
            float[] volume = new float[Data.Length];
            float?[] axisX = new float?[Data.Length];
            float?[] axisY = new float?[Data.Length];

            price = GetPriceArray();
            volume = GetVolumeArray();
            axisX = GetAxisValue(price, volume, IndicatorsXselected);
            axisY = GetAxisValue(price, volume, IndicatorsYselected);

            Strategy = new string [DataSet.Length,4];
            for (int i = 1; i < DataSet.Length; i++)
            {
                Strategy [i,0] = price[i].ToString().Replace(',', '.');
                Strategy [i,1] = axisX[i].ToString().Replace(',', '.');
                Strategy [i,2] = axisY[i].ToString().Replace(',', '.');
                Strategy [i,3] = CheckIfValid(i,price[i]).ToString().Replace(',', '.');
            }
        }

        internal float?[] GetAxisValue(float[] price, float[] volume, string axisItem)
        {
            if (axisItem == "RSI.cs")
            {
                RSI rsiCalc = new RSI();
                return rsiCalc.Calculate(price, Data.Length);
            }
            else if(axisItem == "ADJ_VOLUME.cs")
            {
                ADJ_VOLUME normVolumeCalc = new ADJ_VOLUME(volume);
                return normVolumeCalc.Calculate();
            }
            else if(axisItem == "VWAP.cs")
            {
                VWAP vwapCalc = new VWAP(price, volume);
                return vwapCalc.Main();
            }
            else
            {
                float?[] zero = new float?[Data.Length];
                for (int i = 0; i < zero.Length; i++)
                {
                    zero[i] = 0;
                }
                return zero;
            }
        }

        internal float[] GetPriceArray()
        {
            // return price array
            float[] priceArray = new float[Data.Length];

            for (int i = 1; i < Data.Length; i++)
            {
                priceArray[i] = float.Parse(DataSet[i][4].Replace('.',','));
            }

            return priceArray;
        }

        internal float[] GetVolumeArray()
        {
            // returns an array of volume values
            float[] volumeArray = new float[Data.Length];

            for (int i = 1; i < Data.Length; i++)
            {
                volumeArray[i] = float.Parse(DataSet[i][6].Replace('.', ','));
            }

            return volumeArray;
        }

        internal bool CheckIfValid(int i, float price)
        {
            // Check if price is higher than target or lower than stoploss
            while(true)
            {
                if (i >= DataSet.Length)
                {
                    return false;
                }
                float localPrice = float.Parse(DataSet[i][4].Replace('.', ','));
                float temp = ((localPrice / price) - 1) * 100;
                if (temp >= Target )
                {
                    return true;
                }
                else if(temp <= StopLoss)
                {
                    return false;
                }
                i++;
            }
            
        }

        internal virtual void WriteStrategy()
        {
            //write strategy into csv matrix 
            try
            {
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Strategies\\" + StrategyName + ".csv";

                StreamWriter writer = new StreamWriter(path);
                
                writer.WriteLine("Target,Stoploss");
                writer.WriteLine(Target + "," + StopLoss);
                writer.WriteLine($"Price,{IndicatorsXselected},{IndicatorsYselected},IsValid");

                for (int i = 15; i < Strategy.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < Strategy.GetLength(1); j++)
                    {
                        writer.Write(Strategy[i, j] + ",");
                    }
                    writer.WriteLine();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please close the file before writing");
            }
            

        }



    }
}
