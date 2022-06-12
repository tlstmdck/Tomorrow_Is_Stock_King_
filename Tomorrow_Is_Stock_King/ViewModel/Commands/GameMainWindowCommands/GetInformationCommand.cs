using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands
{
    public class GetInformationCommand : ICommand
    {
        SettingVM SettingVM { get; set; }
        public GetInformationCommand(SettingVM vm)
        {
            SettingVM = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(SettingVM.SettingDataToShow.Information == 0)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            SettingVM.SettingDataToShow.Information--;

            foreach(KeyValuePair<int, bool> pair in SettingVM.SettingDataToShow.PopUpEvent)
            {
                if(pair.Key > SettingVM.SettingDataToShow.TurnCnt)
                {
                    string eventStr = pair.Value ? "떡상합니다." : "떡락합니다.";
                    MessageBox.Show(pair.Key - SettingVM.SettingDataToShow.TurnCnt + "턴 뒤 " + eventStr);
                    break;
                }
            }
        }
    }
}
