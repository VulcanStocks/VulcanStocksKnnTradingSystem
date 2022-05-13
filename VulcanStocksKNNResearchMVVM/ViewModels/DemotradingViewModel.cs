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
    public class DemotradingViewModel : Screen
    {
        DataManagerModel dataManager = new DataManagerModel();
        DemotradingMainModel trader = new DemotradingMainModel();

        private BindableCollection<String> _strategySelect = new BindableCollection<String>();
        private BindableCollection<String> _stockToTrade = new BindableCollection<String>();        
        private string _selectedStrategy;
        private string _selectedStockToTrade;
        private string _riskRatioS = "1,3";
        private int _knntestRadios = 25;
        private int _accountBalance = 10000;
        private float _riskRatio;
        private int _capitalRisk; 
        private int _statisticalCertainty; 

        //results
        private int _totalDaysTraded; 
        private int _winningsLosses; 
        private int _totalWinnings; 
        private int _totalLosses; 
        private int _tradesTaken; 
        private int _totalPercentageGain; 
        private int _profit;
        private int _currentBalanceAmount;
        private int _initialBalanceAmount;




        public DemotradingViewModel()
        {
            LoadStockdata(dataManager.ReadDownloadedFiles());
            LoadStrategySelect(dataManager.ReadStrategySelect());
        }

        public BindableCollection<String> StrategySelect
        {
            get { return _strategySelect; }
            set { _strategySelect = value; }
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
            }
        }
        public string SelectedStrategy
        {
            get { return _selectedStrategy; }
            set
            {
                _selectedStrategy = value;
                NotifyOfPropertyChange(() => SelectedStrategy);
            }
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

        public string RiskRatio
        {
            get { return _riskRatioS.ToString(); }
            set
            {                
                if(float.TryParse(value, out float x))
                {
                    _riskRatioS = value;
                    NotifyOfPropertyChange(() => RiskRatio);
                    _riskRatio = x;
                }
                else if (value == "")
                {
                    _riskRatioS = "0";
                    NotifyOfPropertyChange(() => RiskRatio);
                }


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
            trader.Train(SelectedStrategy, SelectedStockToTrade, int.Parse(KnntestRadios), int.Parse(AccountBalance), _riskRatio, _capitalRisk, _statisticalCertainty);
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
