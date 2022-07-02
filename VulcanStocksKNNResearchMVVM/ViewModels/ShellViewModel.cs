using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using VulcanStocksKNNResearchMVVM.ViewModels;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class ShellViewModel : Conductor<object>
    { 
        public ShellViewModel()
        {
            ActivateItemAsync(new HomePageViewModel());
        }

        //Members

        private string _notch = "Homepage";

        //Properties

        public string Notch
        {
            get { return _notch; }
            set { 
                _notch = value;
                NotifyOfPropertyChange(() => Notch);
            }
        }


        //Side-button functions
        public void Logo_Click()
        {
            Notch = "Homepage";
            ActivateItemAsync(new HomePageViewModel());
        }

        public void Backtest_Click()
        {
            Notch = "Backtesting"; 
            ActivateItemAsync(new BacktestingViewModel());
        }

        public void Demo_Click()
        {
            Notch = "Demotrading";
            ActivateItemAsync(new DemotradingViewModel());
        }

        public void FBE_Click()
        {
            Notch = "Manual entry";
            ActivateItemAsync(new FbeViewModel());
        }

        public void Settings_Click()
        {
            Notch = "Settings";
            ActivateItemAsync(new SettingsViewModel());
        }

        public void Close_Click()
        {
            System.Environment.Exit(0);
        }



    }
}
