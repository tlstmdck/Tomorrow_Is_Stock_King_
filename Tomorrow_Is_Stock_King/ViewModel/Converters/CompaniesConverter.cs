using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tomorrow_Is_Stock_King.ViewModel.Converters
{
    public class CompaniesConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> Companies = (ObservableCollection<string>)values[0];
            ObservableCollection<string> result = new ObservableCollection<string>();
            int index = 5;  //주식 증가시 수정필요
            switch(values[1].ToString())
            {
                case "금융":
                    for(int i=0; i<1*index; i++)
                    {
                        result.Add(Companies[i]);
                    }
                    break;
                case "IT":
                    for (int i = 1 * index; i < 2 * index; i++)
                    {
                        result.Add(Companies[i]);
                    }
                    break;
                case "제조":
                    for (int i = 2 * index; i < 3 * index; i++)
                    {
                        result.Add(Companies[i]);
                    }
                    break;
                case "화학":
                    for (int i = 3 * index; i < 4 * index; i++)
                    {
                        result.Add(Companies[i]);
                    }
                    break;
            }
            return result;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
