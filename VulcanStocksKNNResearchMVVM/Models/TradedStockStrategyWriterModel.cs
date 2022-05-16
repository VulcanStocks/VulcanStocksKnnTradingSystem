using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VulcanStocksKNNResearchMVVM.Models;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class TradedStockStrategyWriterModel : StrategyWriterModel
    {
        List<StrategyModel> TradedStockList = new List<StrategyModel>();
        public List<StrategyModel> Get(string ticker, int StopLoss, int target, string StrategyName, string IndicatorsXselected, string IndicatorsYselected)
        {
            Write(ticker, StopLoss, target, StrategyName, IndicatorsXselected, IndicatorsYselected);
            return TradedStockList;
        }
        public override void Write(string ticker, int StopLoss, int target, string StrategyName, string indicatorsXselected, string indicatorsYselected)
        {
            if (ticker.Contains(".csv"))
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
            
            IndicatorsXselected = indicatorsXselected.Replace(',','.');
            IndicatorsYselected = indicatorsYselected.Replace(',', '.');
            
            Setup();
            FillStrategy();
            WriteStrategy();         
        }

        internal override void FillStrategy()
        {
            float[] price = new float[Data.Length];
            float[] volume = new float[Data.Length];
            float?[] axisX = new float?[Data.Length];
            float?[] axisY = new float?[Data.Length];

            price = GetPriceArray();
            volume = GetVolumeArray();
            axisX = GetAxisValue(price, volume, IndicatorsXselected);
            axisY = GetAxisValue(price, volume, IndicatorsYselected);

            Strategy = new string[DataSet.Length, 4];

            for (int i = 1; i < DataSet.Length; i++)
            {

                Strategy[i, 0] = price[i].ToString();
                Strategy[i, 1] = axisX[i].ToString();
                Strategy[i, 2] = axisY[i].ToString();
                Strategy[i, 3] = CheckIfValid(i, price[i]).ToString();


            }
        }

        internal override void WriteStrategy()
        {
            //write strategy into csv matrix 

            for (int i = 0; i < Strategy.Length; i++)
            {
                Console.WriteLine("-------");
                Console.WriteLine(Strategy.Length);
                Console.WriteLine(Strategy[i, 0] + " " + Strategy[i, 1] + " " + Strategy[i, 2] + " " + Strategy[i, 3]);
                TradedStockList.Add(new StrategyModel { Price = float.Parse(Strategy[i, 0]), IndicatorsXselected = float.Parse(Strategy[i, 1]), IndicatorsYselected = float.Parse(Strategy[i, 2]), IsValid = bool.Parse(Strategy[i, 3]) });
            }

            Console.WriteLine("Strategy written");


        }
    }
}
