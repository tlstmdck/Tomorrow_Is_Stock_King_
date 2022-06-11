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
        public RepaymentCommand RepaymentCommand { get; set; }
        public TakeLoanCommand TakeLoanCommand { get; set; }
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
            RepaymentCommand = new RepaymentCommand(this);
            TakeLoanCommand = new TakeLoanCommand(this);
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
        }

        public void BuyStock(string buyCnt)
        {
            int buyCount = int.Parse(buyCnt);
            string stockName = StockVM.Item.ItmsNm;

            // 주식 구입으로 인해 현재 보유금액에서 산 만큼 뺌
            SettingVM.PlayerVM.PlayerDataToShow.CurMoney -= buyCount * long.Parse(StockVM.Item.Clpr);

            // 구매한 주식을 보유 주식 dictionary에 넣음
            if (SettingVM.PlayerVM.PlayerDataToShow.Stocks.ContainsKey(stockName))
            {
                SettingVM.PlayerVM.PlayerDataToShow.Stocks[stockName] += buyCount;
            }
            else {
                SettingVM.PlayerVM.PlayerDataToShow.Stocks.Add(stockName, buyCount);
            }
        }
        
        public void SellStock(string buyCnt)
        {
            int buyCount = int.Parse(buyCnt);
            SettingVM.PlayerVM.PlayerDataToShow.CurMoney += buyCount * long.Parse(StockVM.Item.Clpr);

            SettingVM.PlayerVM.PlayerDataToShow.Stocks.Remove(StockVM.Item.ItmsNm);
        }

        public void TakeLoan()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
