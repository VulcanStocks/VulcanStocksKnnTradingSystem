﻿using System;
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
        public override void Write(string ticker, int StopLoss, int target, string StrategyName, string IndicatorsXselected, string IndicatorsYselected)
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
            this.IndicatorsXselected = IndicatorsXselected;
            this.IndicatorsYselected = IndicatorsYselected;

            Setup();
            FillStrategy();
            WriteStrategy();         
        }

        internal override void WriteStrategy()
        {
            //write strategy into csv matrix 
            try
            {
                for (int i = 0; i < Strategy.Length; i++)
                {
                    TradedStockList.Add(new StrategyModel { Price = float.Parse(Strategy[i,0]), IndicatorsXselected = float.Parse(Strategy[i, 1]), IndicatorsYselected = float.Parse(Strategy[i, 2]), IsValid = bool.Parse(Strategy[i,3]) });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
    }
}