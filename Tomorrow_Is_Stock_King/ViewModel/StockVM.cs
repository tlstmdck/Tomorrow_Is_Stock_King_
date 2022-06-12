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
        private List<List<Item>> realstockturnList;
        public List<List<Item>> RealStockTurnList
        {
            get { return realstockturnList; }
            set { realstockturnList = value; }
        }
        private List<List<Item>> turnList;
        public List<List<Item>> TurnList
        {
            get { return turnList; }
            set { turnList = value; }
        }
        private Item item;
        public Item Item
        {
            get { return item; }
            set { item = value; OnPropertyChanged("Item"); }
        }
        public List<Item> StockDataToShow { get; set; }
        public ObservableCollection<string> Companies { get; set; }
        public GraphVM GraphVM { get; set; }
        private string selectedStock;   //ex> 삼성전자string
        public string SelectedStock
        {
            get { return selectedStock; }
            set { selectedStock = value; GetStockGraph(); }
        }


        public StockVM()
        {
            StockDataToShow = new List<Item>();
            RealStockTurnList = new List<List<Item>>();
            TurnList = new List<List<Item>>();
            Companies = new ObservableCollection<string>();
            GraphVM = new GraphVM();
            

        }
        public void GetCompanies()
        {
            //금융
            Companies.Add("KB금융");

            //IT
            Companies.Add("카카오");

            //제조
            Companies.Add("삼성전자");
            //Companies.Add("SK하이닉스");

            //화학
            Companies.Add("SK이노베이션");
        }
        public void GetStock(string stock_date)  //턴종료마다 발동
        {
            int clprnull = 0;
            for (int i = 0; i < Companies.Count; i++)
            {
                var stock = StockAPI.GetStockData(stock_date, Companies[i]);
                if (stock.Clpr == null)
                {
                    stock = RealStockTurnList[RealStockTurnList.Count-1][i];
                    clprnull++;
                }
                StockDataToShow.Add(stock);

            }

            if(Item == null)
            {
                Item = StockDataToShow[0];
            }
            List<Item> temp = new List<Item>();
            for(int i=0; i<StockDataToShow.Count; i++)
            {
                temp.Add(StockDataToShow[i]);
            }
            RealStockTurnList.Add(temp);  //1턴 완성
            TurnList.Add(temp);

            // 실제 주식데이터가지고 계산
            if(RealStockTurnList.Count > 1)
            {
                Random rand = new Random();
                for (int i = 0; i < Companies.Count; i++)
                {
                    double Stocknum1; 
                    double Stocknum2;
                    double Stockrate;
                    if (clprnull == Companies.Count) //공휴일, 주말
                    {
                        Stockrate = rand.NextDouble()*(1.2 - 0.8) + 0.8;
                    }
                    else        //평일
                    {
                        Stocknum1 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - 2][i].Clpr);
                        Stocknum2 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - 1][i].Clpr);
                        Stockrate = (Stocknum1 / Stocknum2);
                    }
                    
                    int ran = rand.Next(1, 11);
                    int Turnnum;
                    if (ran > 6)
                    {
                        Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) / Stockrate);
                    }
                    else
                    {
                        Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * Stockrate);
                    }
                    TurnList[TurnList.Count - 1][i].Clpr = Turnnum.ToString();

                }
                
            }

            GraphVM.ChangeData(TurnList, 0);
            StockDataToShow.Clear();
        }
        public void GetStockGraph()
        {
            
            int index = Companies.IndexOf(selectedStock);
            if(index < 0)
            {
                index = 0;
            }
            for(int i=0; i<TurnList.Count; i++)
            {
                var temp = TurnList[i][index];
                Item.Clpr = temp.Clpr;
                Item.ItmsNm = temp.ItmsNm;
            }
            GraphVM.ChangeData(TurnList, index);
            MessageBox.Show(Item.Clpr + "," + Item.ItmsNm + "," + TurnList.Count + "턴쨰");
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
