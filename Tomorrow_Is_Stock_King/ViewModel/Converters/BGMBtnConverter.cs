using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    public class BGMBtnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTurnOnBgm = (bool)value;
            if (isTurnOnBgm)
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/BgmOnIcon.png";
            }
            else
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/BgmOffIcon.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTurnOnBgm = (bool)value;
            if (isTurnOnBgm)
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/BGMOnIcon.png";
            }
            else
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/BGMOffIcon.png";
            }
        }
    }
}
