using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tomorrow_Is_Stock_King.View.Windows;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands
{
    public class GetInformationCommand : ICommand
    {
        int CompTurn { get; set; }
        GameTurnVM GameTurnVM { get; set; }
        public GetInformationCommand(GameTurnVM vm)
        {
            GameTurnVM = vm;
            CompTurn = -1;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(GameTurnVM.SettingVM.SettingDataToShow.Information == 0 || CompTurn == GameTurnVM.SettingVM.SettingDataToShow.TurnCnt)
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

            GameTurnVM.SettingVM.SettingDataToShow.Information--;

            //foreach(KeyValuePair<int, bool> pair in SettingVM.SettingDataToShow.PopUpEvent)
            //{
            //    if(pair.Key > SettingVM.SettingDataToShow.TurnCnt)
            //    {
            //        string eventStr = pair.Value ? "떡상합니다." : "떡락합니다.";
            //        MessageBox.Show(pair.Key - SettingVM.SettingDataToShow.TurnCnt + "턴 뒤 " + eventStr);
            //        break;
            //    }
            //}
            foreach(var item in GameTurnVM.SettingVM.SettingDataToShow.PopUpEvent)
            {
                if(GameTurnVM.SettingVM.SettingDataToShow.TurnCnt < item.Key)
                {
                    GameTurnVM.SettingVM.SettingDataToShow.EventTarget = (int)item.Value.First;
                    GameTurnVM.SettingVM.SettingDataToShow.EventCompany = GameTurnVM.StockVM.Companies[GameTurnVM.SettingVM.SettingDataToShow.EventTarget];
                    GameTurnVM.SettingVM.SettingDataToShow.GetIsGood = (int)item.Value.Second;
                    break;
                }
            }

            ShowInformationWindow showInformationwindow = new ShowInformationWindow();
            showInformationwindow.ShowDialog();

            CompTurn = GameTurnVM.SettingVM.SettingDataToShow.TurnCnt;
        }
    }
}
