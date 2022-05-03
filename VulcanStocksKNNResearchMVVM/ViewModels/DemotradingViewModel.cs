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

        private void StartTraining()
        {
            trader.Run(SelectedStrategy, SelectedStockToTrade);
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
