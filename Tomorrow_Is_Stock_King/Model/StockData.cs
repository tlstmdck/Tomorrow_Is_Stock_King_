using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomorrow_Is_Stock_King.Model
{
    internal class StockData : INotifyPropertyChanged
    {
        private int stockCode;

        public int StockCode
        {
            get { return stockCode; }
            set { stockCode = value; }
        }

        private int price;


        public int Price
        {
            get { return price; }
            set { price = value; }
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
