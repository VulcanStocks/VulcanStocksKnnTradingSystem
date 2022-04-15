using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators
{
    public class RSI
    {
        public Queue<float> activeTime = new Queue<float>();
        private float[] PriceChange = new float[14];
        private float[] UpMove = new float[14];
        private float[] DownMove = new float[14];
        private float AvgGain;
        private float AvgLoss;
        private float? RS;
        private float? Rsi;

        public float? Calculate(int i, float price)
        {
            activeTime.Enqueue(price);

            if (i > 14)
            {
                float x = activeTime.Dequeue();

                GetPriceChange(price);
                GetAvgGain();
                GetAvgLoss();
                GetRS();
                GetRSI();

            }

            return Rsi;
        }
        public void GetPriceChange(float price)
        {
            for (int j = 0; j < 13; j++)
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
            AvgGain = UpMove.Sum() / 14;
        }
        public void GetAvgLoss()
        {
            AvgLoss = DownMove.Sum() / 14;
        }
        public void GetRS()
        {
            RS = AvgGain / AvgLoss;
        }
        public void GetRSI()
        {
            Rsi = 100 - (100 / (1 + RS));
        }

    }
}
}
