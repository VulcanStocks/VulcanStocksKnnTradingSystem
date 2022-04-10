using System;
using System.Collections.Generic;
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
        DataImporter importer = new DataImporter();
        //member variables
        private string _tbImportTicker;

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
        
        //functions
        public void BTImport()
        {
            importer.DownloadStockData(TBimportTicker);
        }

        
        
    }
}
