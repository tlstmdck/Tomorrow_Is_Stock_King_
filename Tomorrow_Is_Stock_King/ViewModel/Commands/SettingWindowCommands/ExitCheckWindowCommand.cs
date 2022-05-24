using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.View.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.SettingWindowCommands
{
    internal class ExitCheckWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExitCheckWindow exitcheckwindow = new ExitCheckWindow();
            exitcheckwindow.ShowDialog();
        }
    }
}
