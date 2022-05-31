using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands
{
    public class EffectBtnCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Image img = (Image)parameter;
            if (img.Source.ToString().Equals("pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOnIcon.png"))
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOffIcon.png"));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOnIcon.png"));
            }
        }
    }
}
