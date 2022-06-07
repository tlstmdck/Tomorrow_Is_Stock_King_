using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.View.Usercontrol;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class StockVM
    {
        public List<Item> StockDataToShow { get; set; }
        public ObservableCollection<string> Companies { get; set; }
        public StockGraphChartUserControl StockGraphChart { get; set; }
        public StockVM()
        {
            StockDataToShow = new List<Item>();
            Companies = new ObservableCollection<string>();


            Companies.Add("삼성전자");
            Companies.Add("카카오");
            Companies.Add("카카오");
        }
        public void GetStock()
        {
            string stock_date = "20220603";
            for (int i = 0; i < Companies.Count; i++)
            {
                var stock = StockAPI.GetStockData(stock_date, Companies[i]);
                StockDataToShow.Add(stock);

            }
            
        }
    }
}
