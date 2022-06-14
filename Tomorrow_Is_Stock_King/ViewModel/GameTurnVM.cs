using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.View.Windows;
using Tomorrow_Is_Stock_King.ViewModel.Commands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.ExitCheckWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands.StockListTabCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class GameTurnVM : INotifyPropertyChanged
    {
        public SettingVM SettingVM { get; set; }
        public StockVM StockVM { get; set; }
        public SoundVM SoundVM { get; set; }
        public TurnSkipBtnCommand TurnSkipBtnCommand { get; set; }
        public BuyStockCommand BuyStockCommand { get; set; }
        public SellStockCommand SellStockCommand { get; set; }
        public RepaymentCommand RepaymentCommand { get; set; }
        public TakeLoanCommand TakeLoanCommand { get; set; }
        public ViewStockListCommand ViewStockListCommand { get; set; }
        public ViewMoneyListCommand ViewMoneyListCommand { get; set; }
        public GetInformationCommand GetInformationCommand { get; set; }
        public LevelBtnCommand LevelBtnCommand { get; set; }
        public StartBtnCommand StartBtnCommand { get; set; }
        public YesBtnCommand YesBtnCommand { get; set; }
        public NoBtnCommand NoBtnCommand { get; set; }
        private DateTime date;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public GameTurnVM()
        {
            SettingVM = new SettingVM();
            StockVM = new StockVM();
            SoundVM = new SoundVM();
            TurnSkipBtnCommand = new TurnSkipBtnCommand(this);
            BuyStockCommand = new BuyStockCommand(this);
            SellStockCommand = new SellStockCommand(this);
            RepaymentCommand = new RepaymentCommand(this);
            TakeLoanCommand = new TakeLoanCommand(this);
            ViewStockListCommand = new ViewStockListCommand(this);
            ViewMoneyListCommand = new ViewMoneyListCommand(this);
            GetInformationCommand = new GetInformationCommand(this);
            LevelBtnCommand = new LevelBtnCommand(this);
            StartBtnCommand = new StartBtnCommand(this);
            YesBtnCommand = new YesBtnCommand(this);
            NoBtnCommand = new NoBtnCommand(this);
            StockVM.GetCompanies();
            Random random = new Random();

            int ran = random.Next(1,400);

            Date = new DateTime(2020, 2, 1);
            Date = Date.AddDays(ran);
            
            while (true)
            {
                StockVM.GetStock(Date.ToString("yyyyMMdd"));
                Date = Date.AddDays(1);

                if (StockVM.RealStockTurnList.Count != 0)
                    break;
            }
            
            StockVM.GetStock(Date.ToString("yyyyMMdd"));
            Date = Date.AddDays(1);
            StockVM.GetStock(Date.ToString("yyyyMMdd"));

            StockVM.SelectedStock = StockVM.Companies[0];
        }
        public void NextTurn()
        {
            // 턴증가
            SettingVM.SettingDataToShow.TurnCnt++;

            Date = Date.AddDays(1);
            StockVM.GetStock(Date.ToString("yyyyMMdd"));
            StockVM.SelectedStock = StockVM.SelectedStock;

            if (SettingVM.SettingDataToShow.TurnCnt == 100)
            {
                GameEnd();
            }

            if (SettingVM.SettingDataToShow.TurnCnt % 10 == 5)
            {
                PopUpEvent();
            }

            UpdateMoney();
            SettingVM.PlayerVM.UpdateAIsMoney();
            if (SettingVM.PlayerVM.PlayerDataToShow.Stocks.Count > 0)
            {
                StockVM.GraphVM.UpdateListStockData(SettingVM.PlayerVM.PlayerDataToShow.Stocks);
            }
            GetInterest();
        }
        private void GameEnd()
        {
            ResultWindow resultwindow = new ResultWindow();
            resultwindow.Show();

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.Title != "ResultWindow")
                {
                    window.Close();
                }
            }
        }

        private void PopUpEvent()
        {
            // 이미 정해진 턴에 특정 회사와 긍정 혹은 부정적인 이벤트가 정해지기 때문에 또 이벤트 넘버를 정할 필요 없다.
            SettingVM.SettingDataToShow.EventTarget = (int)SettingVM.SettingDataToShow.PopUpEvent[SettingVM.SettingDataToShow.TurnCnt].First;

            UpdateEventStock(SettingVM.SettingDataToShow.EventTarget, (int)SettingVM.SettingDataToShow.PopUpEvent[SettingVM.SettingDataToShow.TurnCnt].Second);
            SettingVM.SettingDataToShow.EventCompany = StockVM.Companies[SettingVM.SettingDataToShow.EventTarget];
            SettingVM.setCompany(SettingVM.SettingDataToShow.EventCompany);
            StockVM.GetStockGraph();
            EventPopupWindow eventpopupwindow = new EventPopupWindow();
            eventpopupwindow.ShowDialog();
        }
        private void UpdateEventStock(int eventTarget, int isGood)
        {
            Random random = new Random();
            Double Eventrate = random.NextDouble() * (0.3 - 0.2) + 0.2;
            if(isGood == 0)    //증가 이벤트
            {
                SettingVM.SettingDataToShow.EventNum = random.Next(0, 2);
                StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr = ((int)(Double.Parse(StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr) + Double.Parse(StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr) * Eventrate)).ToString();
            }
            else        //감소 이벤트
            {
                SettingVM.SettingDataToShow.EventNum = random.Next(2, 4);
                StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr = ((int)(Double.Parse(StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr) - Double.Parse(StockVM.TurnList[StockVM.TurnList.Count - 1][eventTarget].Clpr) * Eventrate)).ToString();
            }
        }
        private void UpdateMoney()
        {
            SettingVM.PlayerVM.PlayerDataToShow.BeforeTotalMoney = SettingVM.PlayerVM.PlayerDataToShow.TotalMoney;
            long sum = 0;
            foreach(KeyValuePair<string, int> each in SettingVM.PlayerVM.PlayerDataToShow.Stocks)
            {
                string companyName = each.Key;
                int ownCnt = each.Value;

                foreach(Item item in StockVM.TurnList[SettingVM.SettingDataToShow.TurnCnt + 1])
                {
                    if(item.ItmsNm == companyName)
                    {
                        sum += long.Parse(item.Clpr) * (long)ownCnt;
                        break;
                    }
                }
            }
            SettingVM.PlayerVM.PlayerDataToShow.StockMoney = sum;
            SettingVM.PlayerVM.PlayerDataToShow.TotalMoney = SettingVM.PlayerVM.PlayerDataToShow.StockMoney + SettingVM.PlayerVM.PlayerDataToShow.CurMoney;
            SettingVM.PlayerVM.UpdateChangeRate();
        }
        private void GetInterest()
        {
            SettingVM.PlayerVM.PlayerDataToShow.LoanMoney += (long)(SettingVM.PlayerVM.PlayerDataToShow.LoanMoney * 0.01);
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

        public void TakeLoan(long request)
        {
            // 현재 대출금액, 현재금액, 전체금액 증가
            SettingVM.PlayerVM.PlayerDataToShow.LoanMoney += request;
            SettingVM.PlayerVM.PlayerDataToShow.CurMoney += request;
            SettingVM.PlayerVM.PlayerDataToShow.TotalMoney += request;
            // 현재 대출 가능한 금액
            SettingVM.PlayerVM.PlayerDataToShow.CurCanTakeLoan -= request;
        }

        public void RepaymentLoan(long request)
        {
            // 현재 대출금액, 현재금액, 전체금액 감사
            SettingVM.PlayerVM.PlayerDataToShow.LoanMoney -= request;
            SettingVM.PlayerVM.PlayerDataToShow.CurMoney -= request;
            SettingVM.PlayerVM.PlayerDataToShow.TotalMoney -= request;
            // 현재 대출 가능한 금액
            SettingVM.PlayerVM.PlayerDataToShow.CurCanTakeLoan += request;
        }

        public void updateCanLoan(bool flag, long request)
        {
            SettingVM.PlayerVM.PlayerDataToShow.CanTakeMaxLoan = (long)((SettingVM.PlayerVM.PlayerDataToShow.TotalMoney - SettingVM.PlayerVM.PlayerDataToShow.LoanMoney) * 0.9);
            SettingVM.PlayerVM.PlayerDataToShow.CurCanTakeLoan = SettingVM.PlayerVM.PlayerDataToShow.CanTakeMaxLoan - SettingVM.PlayerVM.PlayerDataToShow.LoanMoney;
        }
    }
}
