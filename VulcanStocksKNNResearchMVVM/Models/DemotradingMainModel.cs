using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotradingMainModel : BacktestingMainModel
    {
        DemotraderModel demotrader = new DemotraderModel();
        public (List<TestedDataModel>, string[] ) start(string StrategyPath, string Ticker, int KnnTestRatio, float AccountBalance, float RiskRatio, int CapitalRisk, int StatisticalCertainty, int _strategyStart, int _strategyEnd, int _stockStart, int _stockEnd)
        {
            Train(StrategyPath, Ticker, KnnTestRatio,  AccountBalance,  RiskRatio,  CapitalRisk,  StatisticalCertainty,  _strategyStart,  _strategyEnd,  _stockStart,  _stockEnd);
            return (TestedStockList, Columns);
        }

    }
}
