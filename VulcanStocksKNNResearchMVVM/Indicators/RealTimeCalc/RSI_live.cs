using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Indicators.RealTimeCalc
{
    internal class RSI_live : RSI
    {
        public float Get(int i,float price)
        {
            return (float)Main(i, price);
        }   
    }
}
