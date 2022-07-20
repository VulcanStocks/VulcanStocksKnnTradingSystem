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
            if (activeVolume.Count > VwapPeriod)
            {
                activePrice.Dequeue();
                activeVolume.Dequeue();
            }
            else if (activeVolume.Count < VwapPeriod) return 0;
            
            Calc();
            GetStandardDerivation();
            float temp = CheckToday(NormalizedPriceToday());
            return temp;
        }

        public void Calc()
        {
            float sum = 0;
            float sumVolume = 0;
            for (int i = 0; i < activePrice.Count; i++)
            {
                sum += activePrice.ElementAt(i) * activeVolume.ElementAt(i)+1;
                sumVolume += activeVolume.ElementAt(i);
            }

            VWAP_TODAY = sum / sumVolume;
            VWAPQUEUE.Enqueue(VWAP_TODAY);
            Console.WriteLine("Today " + VWAP_TODAY);

        }

        internal override void GetStandardDerivation()
        {
            //get standard derivation of VWAP
            float sum = 0;
            float sumVolume = 0;
            for (int i = 0; i < activePrice.Count; i++)
            {
                sum += (activePrice.ElementAt(i) - VWAP_TODAY) * (activePrice.ElementAt(i) - VWAP_TODAY) * activeVolume.ElementAt(i);
                sumVolume += activeVolume.ElementAt(i);
            }
            VWAP_STANDART_TODAY = (float)Math.Sqrt(sum / sumVolume);
            Console.WriteLine("Standard derivation " +VWAP_STANDART_TODAY);
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
