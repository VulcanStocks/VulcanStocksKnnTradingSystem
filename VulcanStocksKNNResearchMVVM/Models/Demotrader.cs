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

        private int Target { get; set; }
        private int Stoploss { get; set; }

        private int TotalDaysTraded { get; set; }
        private int WinningsLosses { get; set; }
        private int TotalWinnings { get; set; }
        private int TotalLosses { get; set; }
        private int TradesTaken { get; set; }
        private int TotalPercentageGain { get; set; }
        private int Profit { get; set; }
        private int CurrentBalanceAmount { get; set; }
        private int InitialBalanceAmount { get; set; }

        

        public Demotrader(List<StrategyModel> TradedStockList, List<TestedDataModel> TestedStockList, int InitialBalanceAmount, int Target, int Stoploss)
        {
            this.TradedStockList = TradedStockList;
            this.TestedStockList = TestedStockList;
            this.Target = Target;
            this.Stoploss = Stoploss;

            this.InitialBalanceAmount = InitialBalanceAmount;
            this.CurrentBalanceAmount = InitialBalanceAmount;
            this.TotalDaysTraded = TradedStockList.Count();
            Console.WriteLine("Total Days Traded: " + TotalDaysTraded);
            Console.WriteLine("----------------------------------------------------");
        }

        public (int,int,int,int,int,int,int,int,int) Run()
        {
            Trade();
            GetResults();

            return (TotalDaysTraded, WinningsLosses, TotalWinnings, TotalLosses, TradesTaken, TotalPercentageGain, Profit, CurrentBalanceAmount, InitialBalanceAmount);
        }



        private void Trade()
        {
            Console.WriteLine(TotalDaysTraded);
            for (int i = 0; i < TotalDaysTraded; i++)
            {
                CheckDay(TradedStockList[i]);
            }
        }

        private void CheckDay(StrategyModel day)
        {
            for (int i = 0; i < TestedStockList.Count; i++)
            {
                if(day.IndicatorsXselected == TestedStockList[i].IndicatorX && day.IndicatorsYselected == TestedStockList[i].IndicatorY)
                {
                    TakeTrade(day);
                }
            }
        }

        private void TakeTrade(StrategyModel day)
        {
            TradesTaken++;
            if(day.IsValid)
            {
                TotalWinnings++;
                CurrentBalanceAmount = CurrentBalanceAmount * (1 + Target/100);
            }
            else
            {
                WinningsLosses--;
                CurrentBalanceAmount = CurrentBalanceAmount * (1 - Stoploss/100);
            }
            Console.WriteLine("Trade");
        }
        private void GetResults()
        {
            Console.WriteLine(TotalWinnings + " " + TotalLosses);
            WinningsLosses = TotalWinnings/TotalLosses;
            TotalPercentageGain = CurrentBalanceAmount / InitialBalanceAmount * 100;
            Profit = CurrentBalanceAmount - InitialBalanceAmount;
        }

    }
}
