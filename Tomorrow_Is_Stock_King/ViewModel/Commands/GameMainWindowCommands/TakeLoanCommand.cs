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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GameTurnVM.TakeLoan();
        }
    }
}
