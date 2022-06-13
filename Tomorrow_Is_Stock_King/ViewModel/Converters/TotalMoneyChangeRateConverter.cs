using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    internal class TotalMoneyChangeRateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rate = (double)value;
            string str = "";
            if(rate > 0)
            {
                str = "(+" + Math.Round(rate, 2).ToString() + "%)";
            }
            else if(rate < 0)
            {
                str = "(" + Math.Round(rate, 2).ToString() + "%)";
            }
            else
            {
                str = "0%";
            }

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
