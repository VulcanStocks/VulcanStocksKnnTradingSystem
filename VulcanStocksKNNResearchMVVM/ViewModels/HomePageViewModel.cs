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
        DataManagerModel dataManager = new DataManagerModel();
        StrategyWriterModel strategyWriter = new StrategyWriterModel();
        //member variables
        private string _ticker;
        private BindableCollection<String> _stockData = new BindableCollection<String>();
        private BindableCollection<String> _indicatorsX = new BindableCollection<String>();
        private BindableCollection<String> _indicatorsY = new BindableCollection<String>();
        private string _selectedStock;
        private int _target = 10;
        private int _stopLoss = -5;
        private string _strategyName = "";
        private string _indicatorsXselected;
        private string _indicatorsYselected;



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


        public string IndicatorsXselected
        {
            get { return _indicatorsXselected; }
            set
            {
                _indicatorsXselected = value;
                NotifyOfPropertyChange(() => IndicatorsXselected);
            }
        }

        public string IndicatorsYselected
        {
            get { return _indicatorsYselected; }
            set
            {
                _indicatorsYselected = value;
                NotifyOfPropertyChange(() => IndicatorsYselected);
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
                if(int.TryParse(value, out x) )
                {
                    _target = x;
                    NotifyOfPropertyChange(() => Target);
                }
                else if (value == "")
                {
                    _target = 0;
                    NotifyOfPropertyChange(() => Target);
                }
                
            }
        }

        public string StopLoss
        {
            get { return _stopLoss.ToString(); }
            set 
            { 
                int x;
                if(int.TryParse(value, out x))
                {
                    _stopLoss = x;
                    NotifyOfPropertyChange(() => StopLoss);
                }
                else if (value == "")
                {
                    _stopLoss = 0;
                    NotifyOfPropertyChange(() => StopLoss);
                }
                else if (value.Contains("-"))
                {
                    _stopLoss = -1;
                    NotifyOfPropertyChange(() => StopLoss);
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
            strategyWriter.Write(
                SelectedStock, 
                int.Parse(StopLoss), 
                int.Parse(Target), 
                StrategyName,
                IndicatorsXselected,
                IndicatorsYselected, false);
        }



    }
}
