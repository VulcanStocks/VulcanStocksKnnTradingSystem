using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using VulcanStocksKNNResearchMVVM.Models;

namespace VulcanStocksKNNResearchMVVM.ViewModels
{
    public class HomePageViewModel : Screen
    {
        //objects
        DataManager dataManager = new DataManager();
        //member variables
        private string _tbImportTicker;

        private BindableCollection<String> _stockData = new BindableCollection<String>();

        private string _selectedStock;


        public HomePageViewModel()
        {
            ConvertFileInfo(dataManager.ReadDownloadedFiles());
        }

        //properties
        public string TBimportTicker
        {
            get 
            { 
                
                return _tbImportTicker; 
                
            }
            set 
            { 
                _tbImportTicker = value; 
                NotifyOfPropertyChange(() => TBimportTicker);
            }
        }

        public BindableCollection<String> StockData
        {
            get { return _stockData; }
            set { _stockData = value; }
        }


        public string SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                NotifyOfPropertyChange(() => SelectedStock);
            }
        }

        //functions
        public void BTImport()
        {
            if(dataManager.DownloadStockData(TBimportTicker))
            {
                ConvertFileInfo(dataManager.ReadDownloadedFiles());
                string item = TBimportTicker + ".csv";
                SelectedStock = item;
            }
            else
            {
                MessageBox.Show("Invalid ticker");
            }
            
        }

        public void ConvertFileInfo(FileInfo[] Finfo)
        {
            StockData.Clear();
            foreach (var item in Finfo)
            {
                StockData.Add(item.ToString());
            }
        }

        public void Instagram_Click()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/vulcanstocks/",
                UseShellExecute = true
            });
        }




    }
}
