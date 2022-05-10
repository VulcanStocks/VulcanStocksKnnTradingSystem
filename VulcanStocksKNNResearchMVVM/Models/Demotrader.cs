using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VulcanStocksKNNResearchMVVM.Models;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class Demotrader
    {
        List<StrategyModel> TradedStockList = new List<StrategyModel>();
        List<TestedDataModel> TestedStockList = new List<TestedDataModel>();

        

        public Demotrader(List<StrategyModel> TradedStockList, List<TestedDataModel> TestedStockList)
        {
            this.TradedStockList = TradedStockList;
            this.TestedStockList = TestedStockList;
        }

        public void Run()
        {
            
        }
    }
}
