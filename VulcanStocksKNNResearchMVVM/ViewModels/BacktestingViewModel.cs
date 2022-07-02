using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VulcanStocksKNNResearchMVVM.Models;
using System.Threading.Tasks;
using System.IO;
using Caliburn.Micro;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class BacktestingViewModel : Screen
    {
        DataManagerModel dataManager = new DataManagerModel();
        DemotradingMainModel trader = new DemotradingMainModel();

        //User inputs
        private BindableCollection<String> _strategySelect = new BindableCollection<String>();
        private BindableCollection<String> _stockToTrade = new BindableCollection<String>();        
        private string _selectedStrategy;
        private string _selectedStockToTrade;
        private int _knntestRadios = 5;
        private int _accountBalance = 100;
        private float _riskRatio = 1f;
        private int _capitalRisk = 100; 
        private int _statisticalCertainty = 50;
        
        private int _strategyStart;
        private int _strategyEnd;
        private int _stockStart;
        private int _stockEnd;

        //results
        private int _totalDaysTraded; 
        private float _winningsLosses; 
        private int _totalWinnings; 
        private int _totalLosses; 
        private int _tradesTaken; 
        private int _totalPercentageGain; 
        private double _profit;
        private double _currentBalanceAmount;
        private int _initialBalanceAmount;

        public BacktestingViewModel()
        {
            LoadStockdata(dataManager.ReadDownloadedFiles());
            LoadStrategySelect(dataManager.ReadStrategySelect());
        }

        public BindableCollection<String> StrategySelect
        {
            get { return _strategySelect; }
            set 
            { 
                _strategySelect = value;
            }
        }

        public BindableCollection<String> StockToTrade
        {
            get { return _stockToTrade; }
            set { _stockToTrade = value; }
        }

        public string SelectedStockToTrade
        {
            get { return _selectedStockToTrade; }
            set { _selectedStockToTrade = value;
                NotifyOfPropertyChange(() => SelectedStockToTrade);
                _stockStart = 0;
                _stockEnd = File.ReadAllLines(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + _selectedStockToTrade).Length;
                NotifyOfPropertyChange(() => StockStart);
                NotifyOfPropertyChange(() => StockEnd);
            }
        }
        public string SelectedStrategy
        {
            get { return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Strategies\\" + _selectedStrategy; }
            set
            {
                _selectedStrategy = value;
                NotifyOfPropertyChange(() => SelectedStrategy);
                _strategyStart = 0;
                _strategyEnd = File.ReadAllLines(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Strategies\\" + _selectedStrategy).Length;
                NotifyOfPropertyChange(() => StrategyStart);
                NotifyOfPropertyChange(() => StrategyEnd);
            }
        }
        
        public string StrategyStart
        {
            get { return _strategyStart.ToString(); }
            set
            {
                int.TryParse(value, out _strategyStart);
            }
        }


        public string StrategyEnd
        {
            get { return _strategyEnd.ToString(); }
            set { int.TryParse(value, out _strategyEnd); }
        }


        public string StockStart
        {
            get { return _stockStart.ToString(); }
            set
            {
                int.TryParse(value, out _stockStart);
            }
        }


        public string StockEnd
        {
            get { return _stockEnd.ToString(); }
            set { int.TryParse(value, out _stockEnd); }
        }


        public string KnntestRadios
        {
            get { return _knntestRadios.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _knntestRadios = x;
                    NotifyOfPropertyChange(() => KnntestRadios);
                }
                else if (value == "")
                {
                    _knntestRadios = 0;
                    NotifyOfPropertyChange(() => KnntestRadios);
                }

            }
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

        string _riskRatioString = "1";
        public string RiskRatio
        {
            get { return _riskRatioString; }
            set
            {
                _riskRatioString = value;
                try
                {
                    _riskRatio = float.Parse(value);

                }
                catch (Exception)
                {
                    Console.WriteLine("Error parsing RiskRatio");
                }
                NotifyOfPropertyChange(() => RiskRatio);
            }
        }

        public string CapitalRisk
        {
            get { return _capitalRisk.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _capitalRisk = x;
                    NotifyOfPropertyChange(() => CapitalRisk);
                }


            }
        }
        
        public string StatisticalCertainty
        {
            get { return _statisticalCertainty.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _statisticalCertainty = x;
                    NotifyOfPropertyChange(() => StatisticalCertainty);
                }
            }
        }

        public string TotalDaysTraded
        {
            get { return _totalDaysTraded.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _totalDaysTraded = x;
                    NotifyOfPropertyChange(() => TotalDaysTraded);
                }
                else if (value == "")
                {
                    _totalDaysTraded = 0;
                    NotifyOfPropertyChange(() => TotalDaysTraded);
                }

            }
        }

        public string WinningsLosses
        {
            get { return _winningsLosses.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _winningsLosses = x;
                    NotifyOfPropertyChange(() => WinningsLosses);
                }
                else if (value == "")
                {
                    _winningsLosses = 0;
                    NotifyOfPropertyChange(() => WinningsLosses);
                }

            }
        }

        public string TotalWinnings
        {
            get { return _totalWinnings.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _totalWinnings = x;
                    NotifyOfPropertyChange(() => TotalWinnings);
                }
                else if (value == "")
                {
                    _totalWinnings = 0;
                    NotifyOfPropertyChange(() => TotalWinnings);
                }

            }
        }


        public string TotalLosses
        {
            get { return _totalLosses.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _totalLosses = x;
                    NotifyOfPropertyChange(() => TotalLosses);
                }
                else if (value == "")
                {
                    _totalLosses = 0;
                    NotifyOfPropertyChange(() => TotalLosses);
                }

            }
        }

        public string TradesTaken
        {
            get { return _tradesTaken.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _tradesTaken = x;
                    NotifyOfPropertyChange(() => TradesTaken);
                }
                else if (value == "")
                {
                    _tradesTaken = 0;
                    NotifyOfPropertyChange(() => TradesTaken);
                }

            }
        }
        public string TotalPercentageGain
        {
            get { return _totalPercentageGain.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _totalPercentageGain = x;
                    NotifyOfPropertyChange(() => TotalPercentageGain);
                }
                else if (value == "")
                {
                    _totalPercentageGain = 0;
                    NotifyOfPropertyChange(() => TotalPercentageGain);
                }

            }
        }

        public string Profit
        {
            get { return _profit.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _profit = x;
                    NotifyOfPropertyChange(() => Profit);
                }
                else if (value == "")
                {
                    _profit = 0;
                    NotifyOfPropertyChange(() => Profit);
                }

            }
        }


        public string CurrentBalanceAmount
        {
            get { return _currentBalanceAmount.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _currentBalanceAmount = x;
                    NotifyOfPropertyChange(() => CurrentBalanceAmount);
                }
                else if (value == "")
                {
                    _currentBalanceAmount = 0;
                    NotifyOfPropertyChange(() => CurrentBalanceAmount);
                }

            }
        }

        public string InitialBalanceAmount
        {
            get { return _initialBalanceAmount.ToString(); }
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    _initialBalanceAmount = x;
                    NotifyOfPropertyChange(() => InitialBalanceAmount);
                }
                else if (value == "")
                {
                    _initialBalanceAmount = 0;
                    NotifyOfPropertyChange(() => InitialBalanceAmount);
                }

            }
        }



        public void StartTraining()
        {
            trader.Train(SelectedStrategy, SelectedStockToTrade, int.Parse(KnntestRadios), int.Parse(AccountBalance), _riskRatio, _capitalRisk, _statisticalCertainty, _strategyStart, _strategyEnd, _stockStart, _stockEnd);
        }

        public void StartTrading()
        {

            (
                _totalDaysTraded, 
                _winningsLosses, 
                _totalWinnings, 
                _totalLosses, _tradesTaken, 
                _totalPercentageGain, _profit, 
                _currentBalanceAmount, 
                _initialBalanceAmount               
                ) 
            = trader.Run();

            NotifyOfPropertyChange(() => TotalDaysTraded);
            NotifyOfPropertyChange(() => WinningsLosses);
            NotifyOfPropertyChange(() => TotalWinnings);
            NotifyOfPropertyChange(() => TotalLosses);
            NotifyOfPropertyChange(() => TradesTaken);
            NotifyOfPropertyChange(() => TotalPercentageGain);
            NotifyOfPropertyChange(() => Profit);
            NotifyOfPropertyChange(() => CurrentBalanceAmount);
            NotifyOfPropertyChange(() => InitialBalanceAmount);

            
        }

        private void LoadStockdata(FileInfo[] Finfo)
        {
            StockToTrade.Clear();
            foreach (var item in Finfo)
            {
                StockToTrade.Add(item.ToString());
            }
        }

        private void LoadStrategySelect(FileInfo[] Finfo)
        {
            StrategySelect.Clear();
            foreach (var item in Finfo)
            {
                StrategySelect.Add(item.ToString());
            }
        }
    }    
}
