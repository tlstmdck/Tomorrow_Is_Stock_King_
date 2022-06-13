using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands
{
    public class LevelBtnCommand : ICommand
    {
        public GameTurnVM GameTurnVM { get; set; }
        public LevelBtnCommand(GameTurnVM vm)
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
            GameTurnVM.SettingVM.setLevel(int.Parse((string)parameter));

            if (GameTurnVM.SoundVM.SoundDataToShow.IsTurnOnEffect)
            {
                GameTurnVM.SoundVM.playClickSound();
            }
        }
    }
}
