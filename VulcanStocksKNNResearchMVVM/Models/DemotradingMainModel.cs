using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotradingMainModel
    {
        //load strategy into list
        //Write strategy from selected stockdata to trade
        public string StrategyPath { get; set; }
        
        public void Run(string strategyPath)
        {
            StrategyPath = strategy;

        }   

        private void LoadStrategy()
        {
            
        }     
    }
}
