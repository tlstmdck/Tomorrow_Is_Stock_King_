using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (parameter == null)
            {
                return true;
            }
            if(SettingVM.SettingDataToShow.Information == 0)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            SettingVM.SettingDataToShow.Information--;
        }
    }
}
