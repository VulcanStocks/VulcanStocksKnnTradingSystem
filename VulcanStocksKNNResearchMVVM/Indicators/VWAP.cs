using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators
{
    public class VWAP
    {
        
        private float[] price { get; set; }
        private float[] Volume { get; set; }
        private float[] Vwap { get; set; }
        private float[] CurrentVwap { get; set; }
        private int VwapPeriod = 14;
        private float[] VwapStandardDerivation { get; set; }
        private float?[] NormalizedVwap { get; set; }

        public VWAP(float[] price, float[] volume)
        {
            this.price = price;
            this.Volume = volume;
            VwapStandardDerivation = new float[price.Length];
        }

        public float?[] Main()
        {   
            GetVwap();
            GetStandardDerivation();
            return Check(NormalizedPrice());
        }

        private void GetVwap()
        {
            //calculate a 14 day vwap for each day

            Vwap = new float[price.Length];

            for (int i = 0; i < price.Length; i++)
            {
                float sum = 0;
                float sumVolume = 0;
                for (int j = 0; j < VwapPeriod; j++)
                {
                    if (i - j >= 0)
                    {
                        sum += price[i - j] * Volume[i - j];
                        sumVolume += Volume[i - j];
                    }
                }
                Vwap[i] = sum / sumVolume;
            }

            
        }

        private void GetStandardDerivation()
        {
            //get standard derivation of vwap for each day

            for (int i = 0; i < Vwap.Length; i++)
            {
                float sum = 0;
                for (int j = 0; j < VwapPeriod; j++)
                {
                    if (i - j >= 0)
                    {
                        sum += (Vwap[i] - Vwap[i - j]) * (Vwap[i] - Vwap[i - j]);
                    }
                }
                VwapStandardDerivation[i] = (float)Math.Sqrt(sum / VwapPeriod);
            }
        }
       
        private float?[] NormalizedPrice()
        {
            //normalize price between standard derivation cloud 
            //2 standard derivation up and 2 standard derivation down

            NormalizedVwap = new float?[price.Length];

            for (int i = 0; i < price.Length; i++)
            {
                float standardMax = Vwap[i] + (2 * VwapStandardDerivation[i]);
                float standardMin = Vwap[i] - (2 * VwapStandardDerivation[i]);
                float range = standardMax - standardMin;

                NormalizedVwap[i] = ((price[i] - standardMin) / range) * 100;
            }

            return NormalizedVwap;


        }

        private float?[] Check(float?[] NormalizedPrice)
        {
            for (int i = 0; i < NormalizedPrice.Length; i++)
            {
                if (NormalizedPrice[i] > 100) NormalizedPrice[i] = 100;
                if(NormalizedPrice[i] < 0) NormalizedPrice[i] = 0;
            }

            return NormalizedPrice;
        }
        
    }
}
