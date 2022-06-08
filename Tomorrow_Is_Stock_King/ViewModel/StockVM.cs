using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.View.Usercontrol;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class StockVM
    {
        //public List<List<Item>> TurnList { get; set; }
        private Item item;
        public Item Item
        {
            get { return item; }
            set { item = value; OnPropertyChanged("Item"); }
        }
        public List<Item> StockDataToShow { get; set; }
        public ObservableCollection<string> Companies { get; set; }
        public StockGraphChartUserControl StockGraphChart { get; set; }
        private string selectedStock;   //ex> 삼성전자string
        public string SelectedStock
        {
            get { return selectedStock; }
            set { selectedStock = value; GetStockGraph(); }
        }


        public StockVM()
        {
            StockDataToShow = new List<Item>();
            Companies = new ObservableCollection<string>();
            GetCompanies();
            GetStock();
        }
        public void GetCompanies()
        {
            Companies.Add("삼성전자");
            Companies.Add("카카오");
            Companies.Add("SK하이닉스");
        }
        public void GetStock()
        {
            string stock_date = "20220603";
            for (int i = 0; i < Companies.Count; i++)
            {
                var stock = StockAPI.GetStockData(stock_date, Companies[i]);
                StockDataToShow.Add(stock);

            }
            Item = StockDataToShow[0];
            //TurnList.Add(StockDataToShow);  //1턴 완성
            //StockDataToShow.Clear();
        }
        private void GetStockGraph()
        {
            int index = Companies.IndexOf(selectedStock);

            var temp = StockDataToShow[index];
            Item.Clpr = temp.Clpr;
            Item.ItmsNm = temp.ItmsNm;
            item.SrtnCd = temp.SrtnCd;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
