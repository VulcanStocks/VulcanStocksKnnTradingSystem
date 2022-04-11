using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private String _selectedStock;


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


        public String SelectedStock
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
            dataManager.DownloadStockData(TBimportTicker);
            ConvertFileInfo(dataManager.ReadDownloadedFiles());
            string item = TBimportTicker + ".csv";
            SelectedStock = item;
        }

        public void ConvertFileInfo(FileInfo[] Finfo)
        {
            StockData.Clear();
            foreach (var item in Finfo)
            {
                StockData.Add(item.ToString());
            }
        }

        
        
    }
}
