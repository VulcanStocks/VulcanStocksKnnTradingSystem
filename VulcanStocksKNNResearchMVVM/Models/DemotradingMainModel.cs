using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotradingMainModel
    {
        private string StrategyPath { get; set; }
        private string[] TargetStoploss { get; set; }
        private string[] Columns { get; set; }

        List<StrategyModel> StrategyList = new List<StrategyModel>();

        public void Run(string strategyPath)
        {
            StrategyPath = strategyPath;
        }

        private void ImportStrategy()
        {
            string[][] DataSet;
            string[] Data;

            Data = File.ReadAllLines(StrategyPath);

            DataSet = new string[Data.Length][];
            for (int i = 0; i < Data.Length; i++)
            {
                string[] temp = Data[i].Split(',');
                DataSet[i] = new string[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    DataSet[i][j] = temp[j];
                }
            }

            LoadStrategyList(DataSet, Data.Length);
        }

        private void LoadStrategyList(string[][] DataSet, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (i <= 2)
                {
                    switch (i)
                    {
                        case 1:
                            TargetStoploss = DataSet[i];
                            break;
                        case 2:
                            Columns = DataSet[i];
                            break;
                    }
                }
                else
                {
                    StrategyList.Add(new StrategyModel{ Price = float.Parse(DataSet[i][0]), IndicatorsXselected = DataSet[i][1], IndicatorsYselected = DataSet[i][2], IsValid = bool.Parse(DataSet[i][3]) });
                }
            }
        }
    }
}


        
            