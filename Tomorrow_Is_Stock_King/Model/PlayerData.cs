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
        }
    }
}
