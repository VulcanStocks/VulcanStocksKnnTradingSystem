﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DataImporter
    {
        string ticker;
        string downloadURL;

        public void DownloadStockData(string ticker)
        {
            this.ticker = ticker;
            downloadURL = "https://query1.finance.yahoo.com/v7/finance/download/" + ticker + "?period1=1383782400&period2=1649548800&interval=1d&events=history&includeAdjustedClose=true";
            WebClient client = new WebClient();
            client.DownloadFile(downloadURL, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\StockData\\" + ticker + ".csv");
        }

    }
}