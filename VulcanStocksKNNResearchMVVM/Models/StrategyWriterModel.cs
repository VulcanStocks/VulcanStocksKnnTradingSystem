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

        internal List<StrategyModel> TradedStockList = new List<StrategyModel>();

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
        internal int Length { get; set; }
        
        internal int DataStart = 0;
        internal int DataEnd = 0;



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
            DataEnd = File.ReadAllLines(Path).Length - 1;

            Setup();
            FillStrategy();
            WriteStrategy();

               
        }

        internal void Setup()
        {
            Data = File.ReadAllLines(Path);


            Length = DataEnd - DataStart;
            DataSet = new string[Length][];
            for (int i = DataStart; i < DataEnd; i++)
            {
                int index = i - DataStart;
                string[] temp = Data[i].Split(',');
                DataSet[index] = new string[temp.Length];

                for (int j = 0; j < temp.Length; j++)
                {
                    DataSet[index][j] = temp[j];
                }
            }
        }
        internal virtual void FillStrategy()
        {
            float[] price = new float[Length];
            float[] volume = new float[Length];
            float?[] axisX = new float?[Length];
            float?[] axisY = new float?[Length];

            price = GetPriceArray();
            volume = GetVolumeArray();
            axisX = GetAxisValue(price, volume, IndicatorsXselected);
            axisY = GetAxisValue(price, volume, IndicatorsYselected);


            Strategy = new string [DataSet.Length,5];
            for (int i = 1; i < DataSet.Length; i++)
            {

                Strategy [i,0] = price[i].ToString().Replace(',', '.');
                Strategy [i,1] = axisX[i].ToString().Replace(',', '.');
                Strategy [i,2] = axisY[i].ToString().Replace(',', '.');

                (bool isValid, int time) = CheckIfValid(i,price[i]);
                Strategy [i,3] = isValid.ToString();
                Strategy[i,4] = time.ToString();

                try
                {
                    TradedStockList.Add(new StrategyModel { Price = price[i], IndicatorsXselected = (float)axisX[i], IndicatorsYselected = (float)axisY[i], IsValid = isValid, TimeToValidate = time });
                }
                catch (Exception) { }

            }
        }

        internal float?[] GetAxisValue(float[] price, float[] volume, string axisItem)
        {
            if (axisItem == "RSI.cs")
            {

                RSI rsiCalc = new RSI();
                return rsiCalc.Calculate(price, Length);
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
                float?[] zero = new float?[Length];
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
            float[] priceArray = new float[Length];

            for (int i = 1; i < Length; i++)
            {
                Console.WriteLine("I: "+i);
                Console.WriteLine(DataSet[i][4]);
                priceArray[i] = float.Parse(DataSet[i][4].Replace('.',','));
            }

            return priceArray;
        }

        internal float[] GetVolumeArray()
        {
            // returns an array of volume values
            float[] volumeArray = new float[Length];

            for (int i = 1; i < Length; i++)
            {
                volumeArray[i] = float.Parse(DataSet[i][6].Replace('.', ','));
            }

            return volumeArray;
        }

        internal (bool, int) CheckIfValid(int i, float price)
        {
            int time = 0;
            // Check if price is higher than target or lower than stoploss
            while (true)
            {
                if (i >= DataSet.Length)
                {
                    return (false, time);
                }
                float localPrice = float.Parse(DataSet[i][4].Replace('.', ','));
                float temp = ((localPrice / price) - 1) * 100;
                
                if (temp >= Target )
                {
                    return (true, time);
                }
                else if(temp <= -StopLoss)
                {
                    return (false, time);
                }
                i++;
                time++;
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
                writer.WriteLine($"Price,{IndicatorsXselected},{IndicatorsYselected},IsValid, TimeToValidate");

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
