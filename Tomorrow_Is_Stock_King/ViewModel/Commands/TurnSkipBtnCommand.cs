using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands
{
    public class TurnSkipBtnCommand : ICommand
    {
        public GameTurnVM GameTurnVM { get; set; }

        public TurnSkipBtnCommand(GameTurnVM vm)
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

            GameTurnVM.NextTurn();
        }
    }
}
