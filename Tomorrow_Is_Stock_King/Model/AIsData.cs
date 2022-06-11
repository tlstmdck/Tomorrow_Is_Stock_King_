using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomorrow_Is_Stock_King.Model
{
    public class AIsData : INotifyPropertyChanged
    {
        private long totalMoney;
        public long TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; OnPropertyChanged("TotalMoney"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public AIsData(String idx, long money)
        {
            Name = "AI " + idx;
            TotalMoney = money;
        }
    }
}
