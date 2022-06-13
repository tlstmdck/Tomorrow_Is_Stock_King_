using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Tomorrow_Is_Stock_King.Model
{
    public class PlayerData : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private long curMoney;
        public long CurMoney
        {
            get { return curMoney; }
            set { curMoney = value; OnPropertyChanged("CurMoney"); }
        }

        private long stockMoney;
        public long StockMoney
        {
            get { return stockMoney; }
            set { stockMoney = value; OnPropertyChanged("StockMoney"); }
        }

        private long totalMoney;
        public long TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; OnPropertyChanged("TotalMoney"); }
        }

        private Dictionary<string, int> stocks;
        public Dictionary<string, int> Stocks
        {
            get { return stocks; }
            set { stocks = value; OnPropertyChanged("Stocks"); }
        }

        private long loanMoney;
        public long LoanMoney
        {
            get { return loanMoney; }
            set { loanMoney = value; OnPropertyChanged("LoanMoney"); }
        }

        private long canTakeMaxLoan;
        public long CanTakeMaxLoan
        {
            get { return canTakeMaxLoan; }
            set { canTakeMaxLoan = value; OnPropertyChanged("CanTakeMaxLoan"); }
        }

        private long curCanTakeLoan;
        public long CurCanTakeLoan
        {
            get { return curCanTakeLoan; }
            set { curCanTakeLoan = value; OnPropertyChanged("CurCanTakeLoan"); }
        }

        private long befoeTotalMoney;
        public long BeforeTotalMoney
        {
            get { return befoeTotalMoney; }
            set { befoeTotalMoney = value; OnPropertyChanged("BeforeTotalMoney"); }
        }

        private double totalMoneyChangeRate;
        public double TotalMoneyChangeRate
        {
            get { return totalMoneyChangeRate; }
            set { totalMoneyChangeRate = value; OnPropertyChanged("TotalMoneyChangeRate"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public PlayerData()
        {
            Name = "";
            CurMoney = 30000000;
            StockMoney = 0;
            TotalMoney = 30000000;
            Stocks = new Dictionary<string, int>();
            LoanMoney = 0;
            CanTakeMaxLoan = (long)(TotalMoney * 0.9);
            CurCanTakeLoan = CanTakeMaxLoan;
            BeforeTotalMoney = TotalMoney;
            TotalMoneyChangeRate = 0;
        }
    }
}
