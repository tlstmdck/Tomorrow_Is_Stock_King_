using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GoMainCheckWindowCommands
{
    public class YesBtnCommand : ICommand
    {
        public SoundVM SoundVM { get; set; }
        public YesBtnCommand(SoundVM vm)
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

            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            Application.Current.MainWindow = mainwindow;

            foreach(Window window in System.Windows.Application.Current.Windows)
            {
                if(window.Title != "MainWindow")
                {
                    window.Close();
                }
            }
        }
    }
}
