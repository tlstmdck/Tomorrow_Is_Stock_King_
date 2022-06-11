using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    public class StocktoMoneyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)values[0] == "")
            {
                values[0] = "0";
            }
            int stock_num = Int32.Parse((string)values[0]);
            int stock_clpr = Int32.Parse((string)values[1]);
            long result = (stock_clpr * stock_num);
            string money_str = values[2].ToString().Replace(",", "");
            
            int usermoney = Int32.Parse(money_str);
            if(result < 0)
            {
                result = long.MaxValue;
            }
            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] target = new string[3];
            int result = Int32.Parse(value.ToString());
            if(result == Int32.Parse(targetTypes[2].ToString()))
            {
                int stock_num = Int32.Parse(targetTypes[0].ToString());
                int stock_clpr = Int32.Parse(targetTypes[1].ToString());
                stock_num = result / stock_clpr;
                target[0] = stock_num.ToString();
            }
            else
            {
                target[0] = targetTypes[0].ToString();
            }
            target[1] = targetTypes[1].ToString();
            target[2] = targetTypes[2].ToString();
            return target;
        }
    }
}
