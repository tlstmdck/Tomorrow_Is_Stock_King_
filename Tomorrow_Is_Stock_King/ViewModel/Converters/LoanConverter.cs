using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    public class LoanConverter : IValueConverter
    {
        GameTurnVM GameTurnVM { get; set; }
        public LoanConverter(GameTurnVM vm)
        {
            GameTurnVM = vm;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long totalMoney = (long)value;
            long loanMoney = GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.LoanMoney;
            if(totalMoney > 10000000)
            {
                return String.Format("{0:#,0}", (totalMoney * 0.9 - loanMoney).ToString());
            }
            else
            {
                return String.Format("{0:#,0}", "10000000");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
