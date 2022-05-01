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
        public string IndicatorsXselected { get; set; }
        public string IndicatorsYselected { get; set; }
        public bool IsValid { get; set; }
    }
}
