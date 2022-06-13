using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.View.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.SettingWindowCommands
{
    public class ExitCheckWindowCommand : ICommand
    {
        public SoundVM SoundVM { get; set; }
        public ExitCheckWindowCommand(SoundVM vm)
        {
            SoundVM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (SoundVM.SoundDataToShow.IsTurnOnEffect)
            {
                SoundVM.playClickSound();
            }

            ExitCheckWindow exitcheckwindow = new ExitCheckWindow();
            exitcheckwindow.Owner = Application.Current.MainWindow;
            exitcheckwindow.ShowDialog();
        }
    }
}
