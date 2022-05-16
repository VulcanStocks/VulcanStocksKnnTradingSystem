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
        public int Certainty { get; set; }
        public float AcceptedRiskRatio { get; set; }
        List<TestedDataModel> TestedDataList = new List<TestedDataModel>();
        List<StrategyModel> StrategyList = new List<StrategyModel>();
        public List<TestedDataModel> GetValues(List<StrategyModel> strategyList, int KnnTestRatio, int Certainty, float AcceptedRiskRatio)
        {
            this.KnnTestRatio = KnnTestRatio;
            this.StrategyList = strategyList;
            this.Certainty = Certainty;
            this.AcceptedRiskRatio = AcceptedRiskRatio;
            TestedDataList.Clear();
            for (int IndicatorX = 0; IndicatorX < 100; IndicatorX++)
            {
                for (int IndicatorY = 0; IndicatorY < 100; IndicatorY++)
                {
                    TestedDataList.Add(TestKNN(IndicatorX, IndicatorY));
                }
            }
            return GetAcceptedValues(TestedDataList);
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

        private List<TestedDataModel> GetAcceptedValues (List<TestedDataModel> TestedDataList)
        {
            List<TestedDataModel> AcceptedValues = new List<TestedDataModel>();

            foreach (TestedDataModel item in TestedDataList)
            {
                if(item.RiskRatio >= AcceptedRiskRatio && item.Total >= Certainty)
                {
                    Console.WriteLine(item.RiskRatio + " " + item.Total + " " + item.IndicatorX + " " + item.IndicatorY);
                    AcceptedValues.Add(item);
                }
            }
            return AcceptedValues;
        }
    }
}
