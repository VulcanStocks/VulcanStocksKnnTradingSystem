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

        private float _accountBalance = 1000;
        private float _currentAccountBalance = 1000;


        private string _selectedTimeFrame;
        private int _timeTraded = 0;
        private bool HasTrained = false;

        public string[] COLUMNS { get; set; }



        DataManagerModel dataManager = new DataManagerModel();
        DemotradingMainModel demotradingMainModel = new DemotradingMainModel();
        DemotraderModel demotrader = new DemotraderModel();


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
                float x;
                if (float.TryParse(value, out x))
                {
                    _accountBalance = x;
                    _currentAccountBalance = x;
                    NotifyOfPropertyChange(() => CurrentAccountBalance);
                    NotifyOfPropertyChange(() => AccountBalance);
                }
            }
        }

        public float CurrentAccountBalance
        {
            get { return _currentAccountBalance; }
            set { _currentAccountBalance = value;
                NotifyOfPropertyChange(() => CurrentAccountBalance);
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
            set { _stockToTrade = value;
                NotifyOfPropertyChange(() => StockToTrade);
            }
        }



        public string TimeTraded
        {
            get { return _timeTraded.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _timeTraded = x;
                    NotifyOfPropertyChange(() => TimeTraded);
                }
            }
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
            TimeFrame.Add("15s");
            TimeFrame.Add("30s");
            TimeFrame.Add("1m");
            TimeFrame.Add("5m");
            TimeFrame.Add("30m");
            TimeFrame.Add("1h");
        }

        public void StartTraining()
        {
            //string StrategyPath, string Ticker, int KnnTestRatio, int AccountBalance, float RiskRatio, int CapitalRisk, int StatisticalCertainty, int _strategyStart, int _strategyEnd, int _stockStart,int _stockEnd
            (TestedStockList,COLUMNS) = demotradingMainModel.start(
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
            demotrader.TestedStockList = TestedStockList;
            HasTrained = true;
        }

        public void StartTrading()
        {
            int time = 1000;
            switch (SelectedTimeFrame)
            {
                case "1s":
                    time = 1000;
                    break;
                case "15s":
                    time = 15000;
                    break;
                case "30s":
                    time = 30000;
                    break;
                case "1m":
                    time = 60000;
                    break;
                case "5m":
                    time = 300000;
                    break;
                case "30m":
                    time = 1800000;
                    break;
                case "1h":
                    time = 3600000;
                    break;
            }
            if (HasTrained)
                RedyToTrade = true;
            else
                RedyToTrade = false;

            int count = 0;
            Thread thread = new Thread(() =>
            {
                try
                {
                    while (RedyToTrade)
                    {
                        Thread.Sleep(time);
   
                        count++;
                        TimeTraded = count.ToString();
                        CurrentAccountBalance = demotrader.TakeTrade(CurrentAccountBalance, COLUMNS[1], COLUMNS[2], StockToTrade, count);
                    }
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
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
