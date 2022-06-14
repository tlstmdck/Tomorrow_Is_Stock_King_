using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands
{
    public class SellStockCommand : ICommand
    {
        GameTurnVM GameTurnVM { get; set; }
        public SellStockCommand(GameTurnVM vm)
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
            long number1 = Int64.Parse(GameTurnVM.StockVM.Item.Clpr);
            long number2 = Int64.Parse((string)parameter);
            long result = number2 / number1;
            if (!GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.Stocks.ContainsKey(GameTurnVM.StockVM.Item.ItmsNm))
            {
                return false;
            }
            if (GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.Stocks[GameTurnVM.StockVM.Item.ItmsNm] < result)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (GameTurnVM.SoundVM.SoundDataToShow.IsTurnOnEffect)
            {
                GameTurnVM.SoundVM.playClickSound();
            }
            long number1 = Int64.Parse(GameTurnVM.StockVM.Item.Clpr);
            long number2 = Int64.Parse((string)parameter);
            long result = number2 / number1;
            GameTurnVM.SellStock(result.ToString());
        }
    }
}
