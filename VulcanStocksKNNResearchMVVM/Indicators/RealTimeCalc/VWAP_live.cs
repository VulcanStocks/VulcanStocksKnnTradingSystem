using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators.RealTimeCalc
{
    public class VWAP_live : VWAP
    {
        public Queue<float> activePrice = new Queue<float>();
        public Queue<float> activeVolume = new Queue<float>();
        public Queue<float> VWAPQUEUE = new Queue<float>();


        private float priceToday;
        private float volumeToday;
        private float VWAP_TODAY;
        private float VWAP_STANDART_TODAY;
        private float VWAP_NORNAL;

        public float Get(float price, float volume)
        {
            activePrice.Enqueue(price);
            activeVolume.Enqueue(volume);
            priceToday = price;
            volumeToday = volume;
            if (activeVolume.Count >= VwapPeriod)
            {
                activePrice.Dequeue();
                activePrice.Dequeue();
            }
            GetStandardDerivation();
            return CheckToday(NormalizedPriceToday());
        }

        public void Calc()
        {
            float sum = 0;
            float sumVolume = 0;
            for (int i = 0; i < activePrice.Count; i++)
            {
                
                sum += activePrice.ElementAt(i) * activeVolume.ElementAt(i);
                sumVolume += activeVolume.ElementAt(i);
                
            }
            VWAP_TODAY = sum / sumVolume;
            VWAPQUEUE.Enqueue(VWAP_TODAY);
           
        }

        internal override void GetStandardDerivation()
        {
            //get standard derivation of vwap for each day

            float mean = (VWAPQUEUE.Sum() / VwapPeriod);
            float sum = 0;
            for (int i = 0; i < VwapPeriod; i++)
            {
                sum += VWAPQUEUE.ElementAt(i) - mean;
            }
            VWAP_STANDART_TODAY = (float)Math.Sqrt(sum / (VwapPeriod-1));
        }

        internal float NormalizedPriceToday()
        {
            float standardMax = VWAP_TODAY + (2 * VWAP_STANDART_TODAY);
            float standardMin = VWAP_TODAY - (2 * VWAP_STANDART_TODAY);
            float range = standardMax - standardMin;
            VWAP_NORNAL = ((priceToday - standardMin) / range) * 100;
            return VWAP_NORNAL;
        }

        internal float CheckToday(float NormalizedPrice)
        {
            if (NormalizedPrice > 100) NormalizedPrice = 100;
            if (NormalizedPrice < 0) NormalizedPrice = 0;

            return NormalizedPrice;
        }
    }
}
