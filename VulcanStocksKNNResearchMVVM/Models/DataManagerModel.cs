using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DataManagerModel
    {
        string ticker;
      
        string downloadURL;


        public bool DownloadStockData(string ticker)
        {
            this.ticker = ticker;
            downloadURL = "https://query1.finance.yahoo.com/v7/finance/download/" + ticker + "?period1=345427200&period2=1649548800&interval=1d&events=history&includeAdjustedClose=true";
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(downloadURL, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv");
            }
            catch (Exception)  
            {
                return false;
            }
            return true;
        }

        public FileInfo[] ReadDownloadedFiles()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\");
            return di.GetFiles("*.csv");
        }

        public FileInfo[] ReadIndicators()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Indicators\\");
            return di.GetFiles("*.cs");
        }



    }
}
