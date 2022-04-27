using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VulcanStocksKNNResearchMVVM.Models;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class DemotradingViewModel : Screen
    {
        DataManager dataManager = new DataManager();

        private BindableCollection<String> _strategySelect = new BindableCollection<String>();
        private string _selectedStrategy;
        private int _knntestRadios = 25;
        private int _accountBalance = 10000;
        private string _riskRatioS = "1,3";
        private float _riskRatio;
        private int _capitalRisk;
        public BindableCollection<String> StrategySelect
        {
            get { return _strategySelect; }
            set { _strategySelect = value; }
        }

        public string SelectedStrategy
        {
            get { return _selectedStrategy; }
            set { _selectedStrategy = value;
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

        


    }
    
}
