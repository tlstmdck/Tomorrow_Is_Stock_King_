using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tomorrow_Is_Stock_King.Model;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands
{
    public class EffectBtnCommand : ICommand
    {
        SoundVM VM { get; set; }
        public EffectBtnCommand(SoundVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (VM.SoundDataToShow.IsTurnOnEffect)
            {
                //img.Source = new BitmapImage(new Uri("pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOffIcon.png"));
                VM.setEffect(false);
            }
            else
            {
                //img.Source = new BitmapImage(new Uri("pack://application:,,,/Tomorrow_Is_Stock_King;component/Images/Icons/EffectOnIcon.png"));
                VM.setEffect(true);
            }
        }
    }
}
