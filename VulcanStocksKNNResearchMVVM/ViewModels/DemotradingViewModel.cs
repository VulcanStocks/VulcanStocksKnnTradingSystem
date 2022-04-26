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
        private float _riskRation = 1.3f;
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
                    NotifyOfPropertyChange(() => _accountBalance);
                }
                else if (value == "")
                {
                    _accountBalance = 0;
                    NotifyOfPropertyChange(() => _accountBalance);
                }

            }
        }

        public string RiskRation
        {
            get { return _riskRation.ToString(); }
            set
            {

                float x;
                if (float.TryParse(value, out x))
                {
                    _riskRation = x;
                    NotifyOfPropertyChange(() => RiskRation);
                }
                else if (value == "")
                {
                    _riskRation = 0;
                    NotifyOfPropertyChange(() => RiskRation);
                }
                

            }
        }

    }
    
}
