using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomorrow_Is_Stock_King.Model
{
    public class TrendsData : INotifyPropertyChanged
    {
        private string trendsDataToShow_Name;
        public string TrendsDataToShow_Name
        {
            get { return trendsDataToShow_Name; }
            set { trendsDataToShow_Name = value; OnPropertyChanged("TrendsDataToShow_Name"); }
        }
        private double trendsDataToShow_Rate;
        public double TrendsDataToShow_Rate
        {
            get { return trendsDataToShow_Rate; }
            set { trendsDataToShow_Rate = value; OnPropertyChanged("TrendsDataToShow_Rate"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public TrendsData()
        {
            TrendsDataToShow_Name = "";
            TrendsDataToShow_Rate = 0;
        }
    }
}
