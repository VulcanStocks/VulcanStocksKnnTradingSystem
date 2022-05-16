using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using VulcanStocksKNNResearchMVVM.Models;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotradingMainModel
    {
        private string StrategyPath { get; set; }
        private string[] TargetStoploss { get; set; } 
        private string[] Columns { get; set; }
        private string Ticker { get; set; }
        private int KnnTestRatio { get; set; }
        private int AccountBalance { get; set; }
        private float RiskRatio { get; set; }
        private int CapitalRisk { get; set; }
        private int StatisticalCertainty { get; set; }

        //results
       

        private bool IsTrained = false;


        List<StrategyModel> StrategyList = new List<StrategyModel>();
        List<StrategyModel> TradedStockList = new List<StrategyModel>();
        List<TestedDataModel> TestedStockList = new List<TestedDataModel>();



        public void Train(string StrategyPath, string Ticker, int KnnTestRatio, int AccountBalance, float RiskRatio, int CapitalRisk, int StatisticalCertainty)
        {
            Console.WriteLine("Training Started");
            this.KnnTestRatio = KnnTestRatio;
            this.Ticker = Ticker;
            this.StrategyPath = StrategyPath;
            this.AccountBalance = AccountBalance;
            this.RiskRatio = RiskRatio;
            this.CapitalRisk = CapitalRisk;
            this.StatisticalCertainty = StatisticalCertainty;

            try
            {
                ImportStrategy();
                FindbestEntries();
                LoadTradedStockList();
                IsTrained = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public (int,int,int,int,int,int,int,int,int) Run()
        {
            if(IsTrained)
            {
                Demotrader demotrader = new Demotrader(TradedStockList, TestedStockList, AccountBalance, int.Parse(TargetStoploss[0]), int.Parse(TargetStoploss[1]));
                return demotrader.Run();
                
            }
            else {
                MessageBox.Show("Please train the model first");
                return (0,0,0,0,0,0,0,0,0);
            }
        }

        private void ImportStrategy()
        {
            string[][] DataSet;
            string[] Data;
            Console.WriteLine(StrategyPath);

            Data = File.ReadAllLines(StrategyPath);
            Console.WriteLine("Data imported");

            DataSet = new string[Data.Length][];
            for (int i = 0; i < Data.Length; i++)
            {
                string[] temp = Data[i].Split(',');
                DataSet[i] = new string[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {   
                    
                    temp[j] = temp[j].Replace('.', ',');
                    
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
                    try
                    {
                        StrategyList.Add(new StrategyModel { Price = float.Parse(DataSet[i][0]), IndicatorsXselected = float.Parse(DataSet[i][1]), IndicatorsYselected = float.Parse(DataSet[i][2]), IsValid = bool.Parse(DataSet[i][3]) });
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            }
        }

        private void LoadTradedStockList()
        {
            TradedStockStrategyWriterModel writer = new TradedStockStrategyWriterModel();
            TradedStockList = writer.Get(Ticker, int.Parse(TargetStoploss[0]),int.Parse(TargetStoploss[1]), "", Columns[1], Columns[2]);
        }

        private void FindbestEntries()
        {
            KnnAlgoModel knn = new KnnAlgoModel();
            TestedStockList = knn.GetValues(StrategyList, KnnTestRatio, StatisticalCertainty, RiskRatio);
            Console.WriteLine("-----------------");
            Console.WriteLine(TestedStockList.Count());
        }
    }
}
