using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands
{
    internal class BackBtnCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = mainwindow;
        }
    }
}
