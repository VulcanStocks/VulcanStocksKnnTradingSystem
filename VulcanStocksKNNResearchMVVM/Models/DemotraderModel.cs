using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulcanStocksKNNResearchMVVM.Models
{
    public class DemotraderModel
    {
        List<TestedDataModel> TestedStockList = new List<TestedDataModel>();

        public DemotraderModel(List<TestedDataModel> TestedStockList)
        {
            this.TestedStockList = TestedStockList;
        }
    }
}
