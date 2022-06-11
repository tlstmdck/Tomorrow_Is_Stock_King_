using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands
{
    public class BuyStockCommand : ICommand
    {
        GameTurnVM GameTurnVM { get; set; }
        public BuyStockCommand(GameTurnVM vm)
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
            if ((string)parameter == "" || (string)parameter == "0") return false;

            long cnt  = long.Parse((string)parameter);
            if(cnt * long.Parse(GameTurnVM.StockVM.Item.Clpr) > GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.CurMoney)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            GameTurnVM.BuyStock((string)parameter);
        }
    }
}
