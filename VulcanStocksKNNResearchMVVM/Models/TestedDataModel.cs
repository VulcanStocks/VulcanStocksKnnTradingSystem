using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class TestedDataModel
    {
        public int Total { get; set; }
        public int TotalTrue { get; set; }
        public int TotalFalse { get; set; }
        public float RiskRatio { get; set; }
        public float IndicatorsX { get; set; }
        public float IndicatorsY { get; set; }
    }
}
