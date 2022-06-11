using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class PlayerVM
    {
        public List<AIsData> AIsDataToShow { get; set; }
        public PlayerData PlayerDataToShow { get; set; }
        
        public PlayerVM()
        {
            AIsDataToShow = new List<AIsData>();
            for(int i = 1;i <= 9; i++)
            {
                AIsDataToShow.Add(new AIsData(i.ToString(), 500000000 * i));
            }
            PlayerDataToShow = new PlayerData();
        }
    }
}
