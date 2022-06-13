using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands.StockListTabCommands
{
    public class ViewMoneyListCommand : ICommand
    {
        public GameTurnVM GameTurnVM { get; set; }
        public ViewMoneyListCommand(GameTurnVM gameTurnVM)
        {
            GameTurnVM = gameTurnVM;
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
            GameTurnVM.StockVM.GraphVM.UpdateListMoneyData(GameTurnVM.StockVM.TurnList, GameTurnVM.StockVM.Companies, GameTurnVM.SettingVM.PlayerVM.PlayerDataToShow.Stocks);
        }
    }
}
