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
        StrategyWriter strategyWriter = new StrategyWriter();
        //member variables
        private string _ticker;
        private BindableCollection<String> _stockData = new BindableCollection<String>();
        private BindableCollection<String> _indicatorsX = new BindableCollection<String>();
        private BindableCollection<String> _indicatorsY = new BindableCollection<String>();
        private string _selectedStock;
        private int _target = 10;
        private int _stopLoss = -5;
        private string _strategyName = "";



        public HomePageViewModel()
        {
            LoadStockdata(dataManager.ReadDownloadedFiles());
            LoadIndicators(dataManager.ReadIndicators());
        }

        //properties
        public string Ticker
        {
            get 
            { 
                
                return _ticker; 
                
            }
            set 
            { 
                _ticker = value; 
                NotifyOfPropertyChange(() => Ticker);
            }
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
        
        public string Target
        {
            get { return _target.ToString(); }
            set 
            { 
                int x;
                if(int.TryParse(value, out x))
                {
                    _target = x;
                    NotifyOfPropertyChange(() => Target);
                }    
            }
        }

        public string StoppLoss
        {
            get { return _stopLoss.ToString(); }
            set 
            { 
                int x;
                if(int.TryParse(value, out x))
                {
                    _stopLoss = x;
                    NotifyOfPropertyChange(() => StoppLoss);
                    MessageBox.Show("StoppLoss");
                }    
            }
        }
        
        public string StrategyName
        {
            get { return _strategyName; }
            set { _strategyName = value; 
                NotifyOfPropertyChange(() => StrategyName);
            }
        }
        
        public BindableCollection<String> StockData
        {
            get { return _stockData; }
            set { _stockData = value; }
        }
        public BindableCollection<String> IndicatorsX
        {
            get { return _indicatorsX; }
            set { _indicatorsX = value; }
        }

        public BindableCollection<String> IndicatorsY
        {
            get { return _indicatorsY; }
            set { _indicatorsY = value; }
        }
        //functions
        public void BTImport()
        {
            if(dataManager.DownloadStockData(Ticker))
            {
                LoadStockdata(dataManager.ReadDownloadedFiles());
                string item = Ticker + ".csv";
                SelectedStock = item;
            }
            else
            {
                MessageBox.Show("Invalid ticker");
            }
            
        }

        public void LoadStockdata(FileInfo[] Finfo)
        {
            StockData.Clear();
            foreach (var item in Finfo)
            {
                StockData.Add(item.ToString());
            }
        }

        public void LoadIndicators(FileInfo[] Finfo)
        {
            IndicatorsX.Clear();
            IndicatorsY.Clear();
            
            foreach (var item in Finfo)
            {
                IndicatorsX.Add(item.ToString());
                IndicatorsY.Add(item.ToString());
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

        public void Write()
        {
            strategyWriter.Write(Ticker);
        }



    }
}
