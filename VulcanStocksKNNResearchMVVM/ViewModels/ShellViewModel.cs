using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using VulcanStocksKNNResearchMVVM.Views;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.AllActive
    {
        public ShellViewModel()
        {

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
        }

        public void Demotrade_Click()
        {
            Notch = "Demotrade";
            ShellView.ActiveItem d = new HomePageViewModel();
        }

        public void Manual_Click()
        {
            Notch = "Manual entry";
        }

        public void FBE_Click()
        {
            Notch = "Best entry";
        }

        public void Settings_Click()
        {
            Notch = "Settings";
        }

        public void Close_Click()
        {
            System.Environment.Exit(0);
        }



    }
}
