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

        private float Target { get; set; }
        private float Stoploss { get; set; }
        private int KnnTestRatio { get; set; }
        private int TotalDaysTraded { get; set; }
        private float WinningsLosses { get; set; }
        private int TotalWinnings { get; set; }
        private int TotalLosses { get; set; }
        private int TradesTaken { get; set; }
        private int TotalPercentageGain { get; set; }
        private double Profit { get; set; }
        private double CurrentBalanceAmount { get; set; }
        private double InitialBalanceAmount { get; set; }
        private float CapitalRisk { get; set; }
        private int LocalTimeForValidation { get; set; }

        public Demotrader(List<StrategyModel> TradedStockList, List<TestedDataModel> TestedStockList, int InitialBalanceAmount, int Target, int Stoploss, int KnnTestRatio, int CapitalRisk)
        {
            this.TradedStockList = TradedStockList;
            this.TestedStockList = TestedStockList;
            this.Target = Target;
            this.Stoploss = Stoploss;
            this.KnnTestRatio = KnnTestRatio;
            this.CapitalRisk = CapitalRisk;
            LocalTimeForValidation = 0;

            this.InitialBalanceAmount = InitialBalanceAmount;
            this.CurrentBalanceAmount = InitialBalanceAmount;
            this.TotalDaysTraded = TradedStockList.Count();
        }

        public (int,float,int,int,int,int,double,double,int) Run()
        {

            Trade();
            GetResults();

            return (TotalDaysTraded, WinningsLosses, TotalWinnings, TotalLosses, TradesTaken, TotalPercentageGain, Profit, CurrentBalanceAmount, (int)InitialBalanceAmount);
        }



        private void Trade()
        {
            for (int i = 0; i < TotalDaysTraded; i++)
            {
                CheckDay(TradedStockList[i]);
            }
        }

        private void CheckDay(StrategyModel day)
        {
            for (int i = 0; i < TestedStockList.Count; i++)
            {
                if(LocalTimeForValidation > 0)
                {
                    Console.WriteLine("LocalTimeForValidation: " + LocalTimeForValidation);
                    LocalTimeForValidation--;
                    continue;
                }

                int dayX = (int)Math.Round(day.IndicatorsXselected);
                int dayY = (int)Math.Round(day.IndicatorsYselected);
                int distance =(int)Math.Round (Math.Sqrt(Math.Pow(dayX - TestedStockList[i].IndicatorX, 2) + Math.Pow(dayY - TestedStockList[i].IndicatorY, 2)));
                if(distance < KnnTestRatio)
                {
                    Console.WriteLine("Trade");
                    TakeTrade(day);
                    break;
                }
            }
        }

        private void TakeTrade(StrategyModel day)
        {
            TradesTaken++;
            LocalTimeForValidation = day.TimeToValidate;
            double tradedCapital = (CurrentBalanceAmount*(CapitalRisk/100));
            if(day.IsValid)
            {
                TotalWinnings++;
                CurrentBalanceAmount = CurrentBalanceAmount - tradedCapital +
                (tradedCapital * (1 + (Target/100)));
            }
            else
            {
                TotalLosses++;
                CurrentBalanceAmount = CurrentBalanceAmount - tradedCapital +
                (tradedCapital * (1 - (Stoploss/100)));
            }
            Console.WriteLine(CurrentBalanceAmount);


        }
        private void GetResults()
        {
            WinningsLosses = (float)TotalWinnings/(float)TotalLosses;
            TotalPercentageGain = (int)Math.Round(((CurrentBalanceAmount - InitialBalanceAmount)/InitialBalanceAmount) * 100);
            Profit = (int)Math.Round(CurrentBalanceAmount - InitialBalanceAmount);
        }

    }
}
