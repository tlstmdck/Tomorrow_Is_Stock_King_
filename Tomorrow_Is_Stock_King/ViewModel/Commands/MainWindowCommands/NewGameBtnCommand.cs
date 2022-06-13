using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.View.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.MainWindowCommands
{
    public class NewGameBtnCommand : ICommand
    {
        SoundVM SoundVM { get; set; }
        public NewGameBtnCommand(SoundVM vm)
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

            StartSettingWindow startsettingwindow = new StartSettingWindow();
            startsettingwindow.Show();

            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = startsettingwindow;
        }
    }
}
