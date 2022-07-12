using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators
{
    public class ADJ_VOLUME
    {
        public float[] Volume { get; set; }
        public int StandardMultiplier = 2;
        public float?[] NormVolume { get; set; }

        public float?[] Calculate(float[] volume)
        {
            Volume = volume;
            NormVolume = new float?[Volume.Length];



            float standardMax = Volume.Average() + (StandardMultiplier * GetStandardDeviation());
            float standardMin = Volume.Average() - (StandardMultiplier * GetStandardDeviation());
            float range = standardMax - standardMin;

            for (int i = 0; i < Volume.Length; i++)
            {
                NormVolume[i] = ((Volume[i] - standardMin) / range)*100;
            }
        
            return NormVolume;

        }

        private float GetStandardDeviation()
        {
            float sum = 0;
            float mean = Volume.Average();
            for (int i = 0; i < Volume.Length; i++)
            {
                sum += (Volume[i] - mean) * (Volume[i] - mean);
            }
            return (float)Math.Sqrt(sum / Volume.Length);
        }

        private float?[] GetNormVolume()
        {
            for (int i = 0; i < NormVolume.Length; i++)
            {
                if (NormVolume[i] > 100) NormVolume[i] = 100;
                if (NormVolume[i] < 0) NormVolume[i] = 0;
            }

            return NormVolume;
        }


    }
}
