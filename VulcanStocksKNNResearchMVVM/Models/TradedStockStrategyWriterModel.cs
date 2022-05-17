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
        }


    }
}
