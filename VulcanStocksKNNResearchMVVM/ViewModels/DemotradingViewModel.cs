using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using VulcanStocksKNNResearchMVVM.Models;
using System.Threading;
using System.Windows;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class DemotradingViewModel : Screen
    {
        private BindableCollection<String> _strategySelect = new BindableCollection<String>();
        private BindableCollection<String> _TimeFrame = new BindableCollection<String>();
        List<TestedDataModel> TestedStockList = new List<TestedDataModel>();

        private string _stockToTrade;

        private string _selectedStrategy;

        private int _accountBalance = 1000;

        private string _selectedTimeFrame;


        DataManagerModel dataManager = new DataManagerModel();
        DemotradingMainModel demotradingMainModel = new DemotradingMainModel();

        public bool RedyToTrade { get; set; }

        public DemotradingViewModel()
        {
            LoadLists(dataManager.ReadStrategySelect());
        }

        public string AccountBalance
        {
            get { return _accountBalance.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _accountBalance = x;
                    NotifyOfPropertyChange(() => AccountBalance);
                }
            }
        }


        public BindableCollection<String> TimeFrame
        {
            get { return _TimeFrame; }
            set
            {
                _TimeFrame = value;
            }
        }


        public string SelectedTimeFrame
        {
            get { return _selectedTimeFrame; }
            set { _selectedTimeFrame = value; }
        }


        public BindableCollection<String> StrategySelect
        {
            get { return _strategySelect; }
            set
            {
                _strategySelect = value;
            }
        }

        public string SelectedStrategy
        {
            get { return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Strategies\\" + _selectedStrategy; }
            set
            {
                _selectedStrategy = value;
                NotifyOfPropertyChange(() => SelectedStrategy);
            }
        }


        public string StockToTrade
        {
            get { return _stockToTrade; }
            set { _stockToTrade = value; }
        }


        private void LoadLists(FileInfo[] Finfo)
        {
            StrategySelect.Clear();
            foreach (var item in Finfo)
            {
                StrategySelect.Add(item.ToString());
            }
            TimeFrame.Clear();
            TimeFrame.Add("1s");
            TimeFrame.Add("30s");
            TimeFrame.Add("1m");
            TimeFrame.Add("5m");
            TimeFrame.Add("30m");
            TimeFrame.Add("1h");
        }

        public void StartTraining()
        {
            //string StrategyPath, string Ticker, int KnnTestRatio, int AccountBalance, float RiskRatio, int CapitalRisk, int StatisticalCertainty, int _strategyStart, int _strategyEnd, int _stockStart,int _stockEnd
            TestedStockList = demotradingMainModel.start(
                SelectedStrategy,
                StockToTrade,
                25,
                _accountBalance, 
                1,
                100,
                25,
                0,
                File.ReadAllLines(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Strategies\\" + _selectedStrategy).Length,
                0,
                0);
        }

        public void StartTrading()
        {
            RedyToTrade = true;
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        while (RedyToTrade)
                        {
                            DemotraderModel demotraderModel = new DemotraderModel(TestedStockList);

                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            });
            thread.IsBackground = true;
            thread.Start();
            
        }

        public void StopTrading()
        {
            RedyToTrade = false;
        }


    }
}
