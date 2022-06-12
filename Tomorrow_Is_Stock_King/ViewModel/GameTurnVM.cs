using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using Tomorrow_Is_Stock_King.ViewModel.Commands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Converters;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class GameTurnVM : INotifyPropertyChanged
    {
        public SettingVM SettingVM { get; set; }
        public StockVM StockVM { get; set; }
        public TurnSkipBtnCommand TurnSkipBtnCommand { get; set; }
        public BuyStockCommand BuyStockCommand { get; set; }
        public SellStockCommand SellStockCommand { get; set; }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public GameTurnVM()
        {
            SettingVM = new SettingVM();
            StockVM = new StockVM();
            TurnSkipBtnCommand = new TurnSkipBtnCommand(this);
            BuyStockCommand = new BuyStockCommand(this);
            SellStockCommand = new SellStockCommand(this);
            StockVM.GetCompanies();

            Date = new DateTime(2022, 6, 7);
            StockVM.GetStock(Date.ToString("yyyyMMdd"));
            Date = Date.AddDays(1);
            StockVM.GetStock(Date.ToString("yyyyMMdd"));

        }
        public void NextTurn()
        {
            Date = Date.AddDays(1);
            StockVM.GetStock(Date.ToString("yyyyMMdd"));
            SettingVM.NextTurn();
            if(SettingVM.PlayerVM.PlayerDataToShow.Stocks.Count > 0)
            {
                StockVM.GraphVM.UpdateListStockData(SettingVM.PlayerVM.PlayerDataToShow.Stocks);
            }
            
        }

        public void BuyStock(string buyCnt)
        {
            int buyCount = int.Parse(buyCnt);
            string stockName = StockVM.Item.ItmsNm;
            int stock_num = SettingVM.PlayerVM.PlayerDataToShow.Stocks.Count;
            // 주식 구입으로 인해 현재 보유금액에서 산 만큼 뺌
            SettingVM.PlayerVM.PlayerDataToShow.CurMoney -= buyCount * long.Parse(StockVM.Item.Clpr);

            // 구매한 주식을 보유 주식 dictionary에 넣음
            if (SettingVM.PlayerVM.PlayerDataToShow.Stocks.ContainsKey(stockName))
            {
                SettingVM.PlayerVM.PlayerDataToShow.Stocks[stockName] += buyCount;
                StockVM.GraphVM.UpdateListStockData(SettingVM.PlayerVM.PlayerDataToShow.Stocks);
            }
            else {
                SettingVM.PlayerVM.PlayerDataToShow.Stocks.Add(stockName, buyCount);
                StockVM.GraphVM.AddListStockData(SettingVM.PlayerVM.PlayerDataToShow.Stocks);
            }
        }
        
        public void SellStock(string buyCnt)
        {
            int buyCount = int.Parse(buyCnt);
            string stockName = StockVM.Item.ItmsNm;
            int stock_num = SettingVM.PlayerVM.PlayerDataToShow.Stocks.Count;

            if (SettingVM.PlayerVM.PlayerDataToShow.Stocks[stockName] <= buyCount)  // 전부 판매하거나 그 이상값으로 판매하려고 할 시
            {
                buyCount = SettingVM.PlayerVM.PlayerDataToShow.Stocks[stockName];                   //구매수를 내주식 보유량으로 제한
                SettingVM.PlayerVM.PlayerDataToShow.CurMoney += buyCount * long.Parse(StockVM.Item.Clpr);   //돈 받기
                SettingVM.PlayerVM.PlayerDataToShow.Stocks.Remove(StockVM.Item.ItmsNm);         //보유주식에서 제거
                StockVM.GraphVM.RemoveListStockData(StockVM.Item.ItmsNm);       //리스트에서 제거
            }
            else        //일부만 판매
            {
                SettingVM.PlayerVM.PlayerDataToShow.Stocks[stockName] -= buyCount;      //보유주식에서 판 주식수만큼 제거
                StockVM.GraphVM.UpdateListStockData(SettingVM.PlayerVM.PlayerDataToShow.Stocks);    //단순 리스트 업데이트
            }

            

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
