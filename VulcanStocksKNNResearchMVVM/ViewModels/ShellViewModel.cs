using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.AllActive
    {
        public ShellViewModel()
        {
            
        }

        public void Logo_Click()
        {

        }

        public void Demotrade_Click()
        {

        }

        public void Manual_Click()
        {

        }

        public void FBE_Click()
        {

        }

        public void Settings_Click()
        {

        }

        public void Close_Click()
        {
            System.Environment.Exit(0);
        }



    }
}
