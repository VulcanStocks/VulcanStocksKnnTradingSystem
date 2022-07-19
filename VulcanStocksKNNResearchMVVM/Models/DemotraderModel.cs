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
        public string[] COLUMNS { get; set; } //1,2 important columns
        private bool IsOnTrade;
        private int TimeOnTrade;
        private float Balance;
        private float IndicatorX;
        private float IndicatorY;
        private double Price;
        private double TotalVolume;
        private double Volume;
        private string Ticker;
        private int Count;

        
        public float TakeTrade(float Balance, string IndicatorXselected, string IndicatorYselected, string Ticker, int count)
        {
            this.Balance = Balance;
            this.COLUMNS = COLUMNS;

            this.Ticker = Ticker;
            Count = count;

            GetPrice();
            IndicatorX = GetIndicators(IndicatorXselected);
            IndicatorY = GetIndicators(IndicatorYselected);
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
        private float GetIndicators(string IndicatorSelected)
        {
            Console.WriteLine(IndicatorSelected);
            if(IndicatorSelected == "ADJ_VOLUME,cs")
            {
                return aDJ_VOLUME_Live.Get();
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
                return 0;
            }

        }

    }
}
