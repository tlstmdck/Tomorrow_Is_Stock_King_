using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class SettingVM
    {
        public SettingData SettingDataToShow { get; set; }
        public PlayerVM PlayerVM { get; set; }

        public SettingVM()
        {
            SettingDataToShow = new SettingData();
            PlayerVM = new PlayerVM();
        }

        public void setLevel(int lev)
        {
            SettingDataToShow.Level = lev;
            if(lev == 1)
            {
                PlayerVM.PlayerDataToShow.CurMoney = 100000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 100000000;
            }
            else if(lev == 2)
            {
                PlayerVM.PlayerDataToShow.CurMoney = 70000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 70000000;
            }
            else
            {
                PlayerVM.PlayerDataToShow.CurMoney = 50000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 50000000;
            }

            PlayerVM.AIsDataToShow.Clear();

            long aiStartMoney = 0;

            switch (lev)
            {
                case 1:
                    aiStartMoney = 30000000;
                    break;
                case 2:
                    aiStartMoney = 40000000;
                    break;
                default:
                    aiStartMoney = 50000000;
                    break;
            }

            for (int i = 1; i <= 9; i++)
            {
                PlayerVM.AIsDataToShow.Add(new AIsData(i.ToString(), (aiStartMoney * i)));
            }
        }
        public void setPlayerName(string name)
        {
            PlayerVM.PlayerDataToShow.Name = name;
        }
        public void setMoney(long money)
        {
            PlayerVM.PlayerDataToShow.CurMoney = money;
        }

        public void setCompany(string EventCompany)
        {
            SettingDataToShow.EventImg = SettingDataToShow._Events[SettingDataToShow.EventNum].ImgSrc;
            SettingDataToShow.EventTitle = EventCompany.ToString() + " " + SettingDataToShow._Events[SettingDataToShow.EventNum].Title;
            SettingDataToShow.EventContent = EventCompany.ToString() + " " + SettingDataToShow._Events[SettingDataToShow.EventNum].Content;
        }
    }
}
