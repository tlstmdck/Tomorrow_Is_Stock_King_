﻿using System;
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
        public LevelBtnCommand LevelBtnCommand { get; set; }
        public UpdateTimeCommand UpdateTimeCommand { get; set; }
        public StartBtnCommand StartBtnCommand { get; set; }
        public GetInformationCommand GetInformationCommand { get; set; }

        public SettingVM()
        {
            SettingDataToShow = new SettingData();
            PlayerVM = new PlayerVM();
            LevelBtnCommand = new LevelBtnCommand(this);
            UpdateTimeCommand = new UpdateTimeCommand(this);
            StartBtnCommand = new StartBtnCommand(this);
            GetInformationCommand = new GetInformationCommand(this);
        }

        public void setLevel(int lev)
        {
            SettingDataToShow.Level = lev;
            if(lev == 1)
            {
                PlayerVM.PlayerDataToShow.CurMoney = 30000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 30000000;
            }
            else if(lev == 2)
            {
                PlayerVM.PlayerDataToShow.CurMoney = 20000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 20000000;
            }
            else
            {
                PlayerVM.PlayerDataToShow.CurMoney = 10000000;
                PlayerVM.PlayerDataToShow.TotalMoney = 10000000;
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
        public void setUpdateTime(int time)
        {
            SettingDataToShow.UpdateTime = time;
        }

        public void setCompany()
        {
            //SettingDataToShow._Events[SettingDataToShow.EventNum].Title = SettingDataToShow._Events[SettingDataToShow.EventNum].Title.Insert(0, SettingDataToShow.EventTarget.ToString() + " ");
            //SettingDataToShow._Events[SettingDataToShow.EventNum].Content = SettingDataToShow._Events[SettingDataToShow.EventNum].Content.Insert(0, SettingDataToShow.EventTarget.ToString() + " ");

            SettingDataToShow.EventImg = SettingDataToShow._Events[SettingDataToShow.EventNum].ImgSrc;
            SettingDataToShow.EventTitle = SettingDataToShow.EventTarget.ToString() + " " + SettingDataToShow._Events[SettingDataToShow.EventNum].Title;
            SettingDataToShow.EventContent = SettingDataToShow.EventTarget.ToString() + " " + SettingDataToShow._Events[SettingDataToShow.EventNum].Content;
        }
    }
}
