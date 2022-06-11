using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.ViewModel.Commands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class GameTurnVM : INotifyPropertyChanged
    {
        public SettingVM SettingVM { get; set; }
        public StockVM StockVM { get; set; }
        public TurnSkipBtnCommand TurnSkipBtnCommand { get; set; }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /*
        private int timerValue;

        public int TimerValue
        {
            get { return timerValue; }
            set 
            {
                timerValue = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimerValue"));
                }
            }
        }
        */
        public GameTurnVM()
        {
            SettingVM = new SettingVM();
            StockVM = new StockVM();
            TurnSkipBtnCommand = new TurnSkipBtnCommand(this);

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
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
