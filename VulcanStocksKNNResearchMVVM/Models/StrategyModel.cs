using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class StrategyModel
    {
        public float Price { get; set; }
        public float IndicatorsXselected { get; set; }
        public float IndicatorsYselected { get; set; }
        public bool IsValid { get; set; }
        public int TimeToValidate { get; set; }
    }
}
