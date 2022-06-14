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
using Tomorrow_Is_Stock_King.ViewModel.Converters;

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
        public TrendsData TrendsDataToShow { get; set; }
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
            TrendsDataToShow = new TrendsData();
            Item = new Item() { Clpr = "0", ItmsNm = "", Rate = "0" };

        }
        public void GetCompanies()
        {
            //금융
            Companies.Add("KB금융");
            //Companies.Add("하나금융지주");
            //Companies.Add("우리금융지주");
            //Companies.Add("신한지주");
            //Companies.Add("기업은행");

            //IT
            Companies.Add("삼성전자");
            //Companies.Add("SK하이닉스");
            //Companies.Add("카카오");
            //Companies.Add("NAVER");
            //Companies.Add("삼성SDI");
            

            //제조
            Companies.Add("현대제철");
            //Companies.Add("대우건설");
            //Companies.Add("기아");
            //Companies.Add("현대모비스");
            //Companies.Add("GS리테일");

            //화학
            Companies.Add("SK케미칼");
            //Companies.Add("LG화학");
            //Companies.Add("금호석유");
            //Companies.Add("롯데케미칼");
            //Companies.Add("한화솔루션");
        }
        public void GetStock(string stock_date)  //턴종료마다 발동
        {
            int clprnull = 0;
            for (int i = 0; i < Companies.Count; i++)
            {
                Item stock = StockAPI.GetStockData(stock_date, Companies[i]);
                if (stock.Clpr == null)
                {
                    stock.Clpr = RealStockTurnList[RealStockTurnList.Count - 1][i].Clpr;
                    stock.ItmsNm = RealStockTurnList[RealStockTurnList.Count - 1][i].ItmsNm;
                    stock.Rate = RealStockTurnList[RealStockTurnList.Count - 1][i].Rate;
                    clprnull++;
                }
                StockDataToShow.Add(stock);

            }

            GetStockList(RealStockTurnList);
            if(RealStockTurnList.Count == 1)
            {
                GetStockList(TurnList);
            }
            if (RealStockTurnList.Count == 2)
            {
                
                GetStockRateList(RealStockTurnList, clprnull, 0);
            }
            // 실제 주식데이터가지고 계산
            if (RealStockTurnList.Count > 2)
            {

                GetStockList(TurnList);
                Random rand = new Random();
                for (int i = 0; i < Companies.Count; i++)
                {
                    double rate = StockrateList[i];
                    int Turnnum;
                    Turnnum = (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr)) + (int)(Double.Parse(TurnList[TurnList.Count - 2][i].Clpr) * rate);

                    TurnList[TurnList.Count - 1][i].Clpr = Turnnum.ToString();

                }
                StockrateList.Clear();
                GetStockRateList(RealStockTurnList, clprnull, 0);
                GetTrendsData();
                
            }

            GraphVM.ChangeData(TurnList, 0);
            StockDataToShow.Clear();
        }

        private void GetStockList(List<List<Item>> List)
        {
            List<Item> StockList = new List<Item>();
            for (int i=0; i<StockDataToShow.Count; i++)
            {
                StockList.Add(new Item());
                StockList[i].ItmsNm = StockDataToShow[i].ItmsNm;
                StockList[i].Clpr = StockDataToShow[i].Clpr;
                StockList[i].Rate = StockDataToShow[i].Rate;
            }
            List.Add(StockList);
        }

        private void GetStockRateList(List<List<Item>> realStockTurnList, int clprnull, int index)
        {
            Random rand = new Random();
            for (int i=0; i<Companies.Count; i++)
            {
                double Stocknum1;
                double Stocknum2;
                double Stocknum3;
                if (clprnull == Companies.Count) //공휴일, 주말
                {
                    Random random = new Random();
                    int ran = random.Next(1, 11);
                    if(ran < 6)
                    {
                        StockrateList.Add(rand.NextDouble() * (0.05 - 0.03) + 0.03);
                    }
                    else
                    {
                        StockrateList.Add((rand.NextDouble() * (0.05 - 0.03) + 0.03)* -1);
                    }
                    
                }
                else        //평일
                {
                    Stocknum1 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - (index + 2)][i].Clpr);
                    Stocknum2 = Double.Parse(RealStockTurnList[RealStockTurnList.Count - (index + 1)][i].Clpr);
                    Stocknum3 = Stocknum2 - Stocknum1;

                    StockrateList.Add(Stocknum3 / Stocknum1);

                }
            }
        }

        private void GetTrendsData()  
        {
            List<double> sum = new List<double>();
            int index = Companies.Count/4;  //카테고리당 주식 갯수
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
                    TrendsDataToShow.TrendsDataToShow_Name = "금융";
                    TrendsDataToShow.TrendsDataToShow_Rate = sum[companyindex - 1];
                    break;
                case 2:
                    TrendsDataToShow.TrendsDataToShow_Name = "IT";
                    TrendsDataToShow.TrendsDataToShow_Rate = sum[companyindex - 1];
                    break;
                case 3:
                    TrendsDataToShow.TrendsDataToShow_Name = "제조";
                    TrendsDataToShow.TrendsDataToShow_Rate = sum[companyindex - 1];
                    break;
                case 4:
                    TrendsDataToShow.TrendsDataToShow_Name = "화학";
                    TrendsDataToShow.TrendsDataToShow_Rate = sum[companyindex - 1];
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
            double Stocknum1 = Double.Parse(TurnList[TurnList.Count - 2][index].Clpr);
            double Stocknum2 = Double.Parse(TurnList[TurnList.Count - 1][index].Clpr);
            double Stocknum3 = Stocknum2 - Stocknum1;
            Item.Rate = String.Format("{0:0.00}", ((Stocknum3 / Stocknum1) * 100)) + "%";
            GraphVM.ChangeData(TurnList, index);
            //MessageBox.Show(Item.Clpr + "," + Item.ItmsNm + "," + TurnList.Count + "턴쨰");
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
