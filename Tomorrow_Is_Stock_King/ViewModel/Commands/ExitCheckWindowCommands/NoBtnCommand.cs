using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.ExitCheckWindowCommands
{
    public class NoBtnCommand : ICommand
    {
        public GameTurnVM GameTurnVM { get; set; }
        public NoBtnCommand(GameTurnVM vm)
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

            var curwindow = (Window)parameter;
            curwindow.Close();
        }
    }
}
