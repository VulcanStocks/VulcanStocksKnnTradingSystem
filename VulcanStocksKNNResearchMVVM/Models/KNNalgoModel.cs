using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VulcanStocksKNNResearchMVVM.Models;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class KnnAlgoModel
    {
        public int KnnTestRatio { get; set; }
        List<TestedDataModel> TestedDataList = new List<TestedDataModel>();
        List<StrategyModel> StrategyList = new List<StrategyModel>();
        public List<TestedDataModel> GetValues(List<StrategyModel> strategyList, int KnnTestRatio)
        {
            this.KnnTestRatio = KnnTestRatio;
            this.StrategyList = strategyList;
            TestedDataList.Clear();
            for (int IndicatorX = 0; IndicatorX < 100; IndicatorX++)
            {
                for (int IndicatorY = 0; IndicatorY < 100; IndicatorY++)
                {
                    TestedDataList.Add(TestKNN(IndicatorX, IndicatorY));
                }
            }
            return SortTestedDataList(TestedDataList);
        }

        public TestedDataModel TestKNN(int IndicatorX, int IndicatorY)
        {
            int TotalTrue = 0;
            int TotalFalse = 0;

            foreach (var item in StrategyList)
            {
                if(Math.Sqrt(Math.Pow(item.IndicatorsXselected - IndicatorX, 2) + Math.Pow(item.IndicatorsYselected - IndicatorY, 2)) < KnnTestRatio)
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

            TestedDataModel temp = new TestedDataModel();
            temp.Total = TotalTrue + TotalFalse;
            temp.TotalTrue = TotalTrue;
            temp.TotalFalse = TotalFalse;
            temp.RiskRatio = (float)TotalTrue / (float)TotalFalse;
            temp.IndicatorX = IndicatorX;
            temp.IndicatorY = IndicatorY;

            return temp;
        }

        private List<TestedDataModel> SortTestedDataList(List<TestedDataModel> TestedDataList)
        {
            List<TestedDataModel> SortedList = new List<TestedDataModel>();
            for (int i = 0; i < TestedDataList.Count; i++)
            {
                SortedList.Add(TestedDataList.Min());
                TestedDataList.Remove(TestedDataList.Min());
            }
            return SortedList;
        }
    }
}
