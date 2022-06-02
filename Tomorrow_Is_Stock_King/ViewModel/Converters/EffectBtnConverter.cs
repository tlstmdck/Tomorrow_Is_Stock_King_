using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    internal class EffectBtnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTurnOnEffect = (bool)value;
            if (isTurnOnEffect)
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOnIcon.png";
            }
            else
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOffIcon.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTurnOnEffect = (bool)value;
            if (isTurnOnEffect)
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOnIcon.png";
            }
            else
            {
                return "pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOffIcon.png";
            }
        }
    }
}
