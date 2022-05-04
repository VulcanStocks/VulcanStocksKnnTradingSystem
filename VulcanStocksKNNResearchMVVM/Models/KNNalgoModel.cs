using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VulcanStocksKNNResearchMVVM.Models;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class KNNalgoModel
    {
        public int KnnTestRatio { get; set; }
        List<TestedDataModel> TestedDataList = new List<TestedDataModel>();
        List<StrategyModel> StrategyList = new List<StrategyModel>();
        public List<Models.TestedDataModel> GetValues(List<StrategyModel> strategyList, int KnnTestRatio)
        {
            this.KnnTestRatio = KnnTestRatio;
            this.StrategyList = strategyList;
            TestedDataList.Clear();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    
                }
            }
            return TestedDataList;
        }

        public (int, int, int, float, float, float) TestKNN(int i, int j)
        {
            int TotalTrue = 0;
            int TotalFalse = 0;

            foreach (var item in StrategyList)
            {
                if(Math.Sqrt(Math.Pow(item.IndicatorsXselected - i, 2) + Math.Pow(item.IndicatorsYselected - j, 2)) < item.Price)
                {
                    if(item.IsValid)
                    {
                        TotalTrue++;
                    }
                    else
                    {
                        TotalFalse++;
                    }
                }

            }
            return (TotalTrue+TotalFalse, TotalTrue, TotalFalse, TotalTrue/TotalFalse, i, j);
        }
    }
}
