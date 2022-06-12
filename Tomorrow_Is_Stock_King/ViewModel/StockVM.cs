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
        

        private string selectedStock;   //ex> 삼성전자string
        public string SelectedStock
        {
            get { return selectedStock; }
            set { selectedStock = value; GetStockGraph(); }
        }
        private Tuple<string, double> trendsDataToShow;
        public Tuple<string, double> TrendsDataToShow
        {
            get { return trendsDataToShow; }
            set { trendsDataToShow = value; OnPropertyChanged("TrendsDataToShow"); }
        }
        private List<double> stockrateList;
        public List<double> StockrateList
        {
            get { return stockrateList; }
            set { stockrateList = value; }
        }
        public GraphVM GraphVM { get; set; }
        public StockVM()
        {
            StockDataToShow = new List<Item>();
            RealStockTurnList = new List<List<Item>>();
            TurnList = new List<List<Item>>();
            StockrateList = new List<double>();
            Companies = new ObservableCollection<string>();
            GraphVM = new GraphVM();
            
            Item = new Item() { Clpr = "0", ItmsNm =""};

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
                    stock = RealStockTurnList[RealStockTurnList.Count - 1][i];
                    clprnull++;
                }
                StockDataToShow.Add(stock);

            }


            List<Item> temp = new List<Item>();
            for (int i = 0; i < StockDataToShow.Count; i++)
            {
                temp.Add(StockDataToShow[i]);
            }
            RealStockTurnList.Add(temp);  //1턴 완성
            if (RealStockTurnList.Count == 2)
            {
                TurnList.Add(temp);
                GetStockRateList(RealStockTurnList, clprnull);
            }
            // 실제 주식데이터가지고 계산
            if (RealStockTurnList.Count > 2)
            {
                TurnList.Add(temp);
                Random rand = new Random();
                for (int i = 0; i < Companies.Count; i++)
                {
                    int ran = rand.Next(1, 11);
                    int Turnnum;
                    if (ran > 5)
                    {
                        if (StockrateList[i] > 0)
                        {
                            Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr)) + (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * StockrateList[i]);
                        }
                        else
                        {
                            Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr)) - (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * StockrateList[i]);
                        }
                    }
                    else
                    {
                        if (StockrateList[i] > 0)
                        {
                            Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr)) - (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * StockrateList[i]);
                        }
                        else
                        {
                            Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr)) + (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * StockrateList[i]);
                        }
                    }
                    TurnList[TurnList.Count - 1][i].Clpr = Turnnum.ToString();

                }
                StockrateList.Clear();
                GetStockRateList(RealStockTurnList, clprnull);
                GetTrendsData();
                
            }

            GraphVM.ChangeData(TurnList, 0);
            StockDataToShow.Clear();
        }

        private void GetStockRateList(List<List<Item>> realStockTurnList, int clprnull)
        {
            Random rand = new Random();
            for (int i=0; i<Companies.Count; i++)
            {
                double Stocknum1;
                double Stocknum2;
                double Stocknum3;
                if (clprnull == Companies.Count) //공휴일, 주말
                {
                    StockrateList.Add(rand.NextDouble() * (0.05 - 0.03) + 0.03);
                }
                else        //평일
                {
                    Stocknum1 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - 2][i].Clpr);
                    Stocknum2 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - 1][i].Clpr);
                    Stocknum3 = Stocknum1 - Stocknum2;

                    StockrateList.Add(Stocknum3 / Stocknum1);

                }
            }
        }

        private void GetTrendsData()  //주식 증가시 수정필요
        {
            List<double> sum = new List<double>();
            int index = 1;  //카테고리당 주식 갯수
            for (int i = 1; i < 5; i++)
            {
                double sum_num = 0;
                for (int j = (i * index) - index; j < i * index; j++)
                {
                    sum_num += StockrateList[j];
                }
                sum.Add(sum_num);
            }
            Random rand = new Random();
            int companyindex = rand.Next(1, 5);
            switch (companyindex)
            {
                case 1:
                    TrendsDataToShow = new Tuple<string, double>("금융", sum[companyindex-1]);
                    MessageBox.Show(TrendsDataToShow.Item1);
                    break;
                case 2:
                    TrendsDataToShow = new Tuple<string, double>("IT", sum[companyindex - 1]);
                    MessageBox.Show(TrendsDataToShow.Item1);
                    break;
                case 3:
                    TrendsDataToShow = new Tuple<string, double>("제조", sum[companyindex - 1]);
                    MessageBox.Show(TrendsDataToShow.Item1);
                    break;
                case 4:
                    TrendsDataToShow = new Tuple<string, double>("화학", sum[companyindex - 1]);
                    MessageBox.Show(TrendsDataToShow.Item1);
                    break;
                default:
                    break;

            }
        }
        public void GetStockGraph()
        {

            int index = Companies.IndexOf(selectedStock);
            if (index < 0)
            {
                index = 0;
            }
            for (int i = 0; i < TurnList.Count; i++)
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
