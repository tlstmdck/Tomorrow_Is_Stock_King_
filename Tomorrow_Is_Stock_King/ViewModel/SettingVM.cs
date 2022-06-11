using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class SettingVM
    {
        public SettingData SettingDataToShow { get; set; }
        public PlayerData PlayerDataToShow { get; set; }
        public List<PlayerData> AIsDataToShow { get; set; }
        public LevelBtnCommand LevelBtnCommand { get; set; }
        public UpdateTimeCommand UpdateTimeCommand { get; set; }
        public StartBtnCommand StartBtnCommand { get; set; }

        public SettingVM()
        {
            SettingDataToShow = new SettingData();
            PlayerDataToShow = new PlayerData();
            AIsDataToShow = new List<PlayerData>();
            LevelBtnCommand = new LevelBtnCommand(this);
            UpdateTimeCommand = new UpdateTimeCommand(this);
            StartBtnCommand = new StartBtnCommand(this);
        }

        public void setLevel(int lev)
        {
            SettingDataToShow.Level = lev;
            if(lev == 1)
            {
                PlayerDataToShow.CurMoney = 30000000;
                PlayerDataToShow.TotalMoney = 30000000;
            }
            else if(lev == 2)
            {
                PlayerDataToShow.CurMoney = 20000000;
                PlayerDataToShow.TotalMoney = 20000000;
            }
            else
            {
                PlayerDataToShow.CurMoney = 10000000;
                PlayerDataToShow.TotalMoney = 10000000;
            }
        }
        public void setUpdateTime(int time)
        {
            SettingDataToShow.UpdateTime = time;
        }
        public void setName(string name)
        {
            PlayerDataToShow.Name = name;
        }
        public void setMoney(long money)
        {
            PlayerDataToShow.CurMoney = money;
        }
    }
}
