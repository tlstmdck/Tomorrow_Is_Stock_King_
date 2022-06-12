using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands
{
    public class TakeLoanCommand : ICommand
    {
        GameTurnVM GameTurnVM { get; set; }
        public TakeLoanCommand(GameTurnVM vm)
        {
            GameTurnVM = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null) return true;
            if ((string)parameter == "") return false;

            long request = long.Parse((string)parameter);
            if (request > GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.CurCanTakeLoan)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            GameTurnVM.TakeLoan(long.Parse((string)parameter));
        }
    }
}
