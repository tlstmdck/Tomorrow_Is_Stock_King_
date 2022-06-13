using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands
{
    public class StartBtnCommand : ICommand
    {
        GameTurnVM GameTurnVM { get; set; }
        public StartBtnCommand(GameTurnVM vm)
        {
            GameTurnVM = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (GameTurnVM.SoundVM.SoundDataToShow.IsTurnOnEffect)
            {
                GameTurnVM.SoundVM.playClickSound();
            }

            GameMainWindow gamemainwindow = new GameMainWindow();
            gamemainwindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = gamemainwindow;

            GameTurnVM.SettingVM.setPlayerName((string)parameter);
            GameTurnVM.SettingVM.PlayerVM.SortPlayers();
        }
    }
}
