using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators
{
    public class RSI
    {
        private static int RSI_PERIOD = 14;
        public Queue<float> activeTime = new Queue<float>();
        private float[] PriceChange = new float[RSI_PERIOD];
        private float[] UpMove = new float[RSI_PERIOD];
        private float[] DownMove = new float[RSI_PERIOD];
        private float AvgGain;
        private float AvgLoss;
        private float? RS;
        private float? Rsi;
        internal bool redy = false;

        public float?[] Calculate(float[] price, int DataL)
        {
            float?[] rSi = new float?[DataL];

            for (int i = 0; i < DataL; i++)
            {
                rSi[i] = (Main(i, price[i]));
            }

            return rSi;

        }
        public float? Main(int i, float price)
        {
            activeTime.Enqueue(price);
            Console.WriteLine("I: " +i);
            if (i > RSI_PERIOD)
            {
                float x = activeTime.Dequeue();

                GetPriceChange(price);
                GetAvgGain();
                GetAvgLoss();
                GetRS();
                GetRSI();
                redy = true;
            }
            else return 0;
            return Rsi;
        }
        public void GetPriceChange(float price)
        {
            for (int j = 0; j < RSI_PERIOD-1; j++)
            {
                PriceChange[j] = activeTime.ElementAt(j) - activeTime.ElementAt(j + 1);
                if (PriceChange[j] > 0)
                {
                    UpMove[j] = PriceChange[j];
                    DownMove[j] = 0;
                }
                else
                {
                    UpMove[j] = 0;
                    DownMove[j] = Math.Abs(PriceChange[j]);
                }
            }
        }


        public void GetAvgGain()
        {
            AvgGain = UpMove.Sum() / RSI_PERIOD;
        }
        public void GetAvgLoss()
        {
            AvgLoss = DownMove.Sum() / RSI_PERIOD;
        }
        public void GetRS()
        {
            RS = AvgGain / AvgLoss;
            Console.WriteLine(RS);

        }
        public void GetRSI()
        {
            Rsi = 100 - (100 / (1 + RS));
        }

    }
}

