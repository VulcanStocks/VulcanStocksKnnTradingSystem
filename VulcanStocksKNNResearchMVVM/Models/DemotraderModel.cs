using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeStockPriceLibrary;
using VulcanStocksKNNResearchMVVM.Indicators.RealTimeCalc;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotraderModel
    {
        //indicators
        ADJ_VOLUME_live aDJ_VOLUME_Live = new ADJ_VOLUME_live();
        RSI_live rSI_live = new RSI_live();
        VWAP_live vWAP_Live = new VWAP_live();
        Scraper scraper = new Scraper();
        public List<TestedDataModel> TestedStockList { get; set; }

        public List<double> holding = new List<double>();
        public string[] COLUMNS { get; set; } //1,2 important columns
        private int TimeOnTrade;
        private float Balance;
        private float IndicatorX;
        private float IndicatorY;
        private double Price;
        private double TotalVolume;
        private double Volume;
        private string Ticker;
        private int Count;
        private int Stoploss = 1;
        private int Target = 1;
        private int KnnTestRatio = 2;



        public float TakeTrade(float Balance, string IndicatorXselected, string IndicatorYselected, string Ticker, int count)
        {
            this.Balance = Balance;
            this.COLUMNS = COLUMNS;

            this.Ticker = Ticker;
            Count = count;

            GetPrice();
            CheckIfSell();

            bool temp;
            bool temp2;

            (IndicatorX, temp) = GetIndicators(IndicatorXselected);
            (IndicatorY, temp2) = GetIndicators(IndicatorYselected);

            if(temp2 && temp)
            {
                Proceed();
            }

            Console.WriteLine (IndicatorX + " " + IndicatorY + " " + Price + " " + Volume );
            return Balance;
        }

        private void GetPrice()
        {
            Price = scraper.GetPrice(Ticker);
            double temp = scraper.GetVolume(Ticker);
            Volume = temp - TotalVolume;
            TotalVolume = temp;
        }
        private (float, bool) GetIndicators(string IndicatorSelected)
        {
            Console.WriteLine(IndicatorSelected);
            if(IndicatorSelected == "ADJ_VOLUME,cs")
            {
                return (aDJ_VOLUME_Live.Get(),false);
            }
            else if(IndicatorSelected == "RSI,cs")
            {
                return rSI_live.Get(Count,(float)Price);
            }
            else if(IndicatorSelected == "VWAP,cs")
            {
                return vWAP_Live.Get((float)Price, (float)Volume);
            }
            else
            {
                return (0, false);
            }

        }

        private void Proceed()
        {
            for (int i = 0; i < TestedStockList.Count; i++)
            {
                int dayX = (int)Math.Round(IndicatorX);
                int dayY = (int)Math.Round(IndicatorY);
                int distance = (int)Math.Round(Math.Sqrt(Math.Pow(dayX - TestedStockList[i].IndicatorX, 2) + Math.Pow(dayY - TestedStockList[i].IndicatorY, 2)));
                if (distance <= KnnTestRatio)
                {
                    TakeTrade();
                    break;
                }
            }
        }
        private void TakeTrade()
        {
            if(Balance > Price)
            {
                holding.Add(Price);
                Balance =- (float)Price;
                Console.WriteLine("Trade!!");

            }
        }
        
        private void CheckIfSell()
        {
            for (int i = 0; i < holding.Count; i++)
            {
                if ((((Price / holding[i]) -1) *100) <= Stoploss)
                {
                    Balance = +(float)holding[i];
                    holding.RemoveAt(i);
                    Console.WriteLine("Sell!!");
                }
                else if ((((Price / holding[i]) - 1) * 100) >= Target)
                {
                    Balance = +(float)holding[i];
                    holding.RemoveAt(i);
                    Console.WriteLine("Sell!!");
                }
            }
        }

    }



}
