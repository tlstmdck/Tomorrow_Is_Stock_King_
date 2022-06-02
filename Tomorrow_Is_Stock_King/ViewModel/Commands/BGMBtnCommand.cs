using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tomorrow_Is_Stock_King.Model;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands
{
    public class BGMBtnCommand : ICommand
    {
        private SoundVM VM { get; set; }

        public BGMBtnCommand(SoundVM vm)
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
            if (VM.SoundDataToShow.IsTurnOnBgm)
            {
                VM.setBgm(false);
            }
            else
            {
                VM.setBgm(true);
            }
        }
    }
}
