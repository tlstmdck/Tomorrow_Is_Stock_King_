using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.ShowHelpWindowCommands
{
    internal class PageNavigateCommand : ICommand
    {
        PageNavigateVM PageNavigateVM { get; set; }
        public PageNavigateCommand(PageNavigateVM vm)
        {
            PageNavigateVM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string name = (string)parameter;
            if (name == "right")
            {
                PageNavigateVM.NavigateTo(PageNavigateVM.CurrentPageInd + 1);
            }
            else
            {
                PageNavigateVM.NavigateTo(PageNavigateVM.CurrentPageInd - 1);
            }
        }
    }
}
