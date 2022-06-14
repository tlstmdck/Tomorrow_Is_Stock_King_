using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GameMainWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.GoMainCheckWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.MainWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.SettingWindowCommands;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class SoundVM
    {
        public SoundData SoundDataToShow { get; set; }
        public BGMBtnCommand BGMBtnCommand { get; set; }
        public EffectBtnCommand EffectBtnCommand { get; set; }
        public NewGameBtnCommand NewGameBtnCommand { get; set; }
        public ExitBtnCommand ExitBtnCommand { get; set; }
        public BackBtnCommand BackBtnCommand { get; set; }
        public GoSettingWindowBtnCommand GoSettingWindowBtnCommand { get; set; }
        public ResumeBtnCommand ResumeBtnCommand { get; set; }
        public GoMainBtnCommand GoMainBtnCommand { get; set; }
        public ExitCheckWindowCommand ExitCheckWindowCommand { get; set; }
        public YesBtnCommand YesBtnCommand { get; set; }
        public NoBtnCommand NoBtnCommand { get; set; }
        public ShowHelpWindowCommand ShowHelpWindowCommand { get; set; }
        public MediaPlayer BackGroundMusic { get; set; }
        public MediaPlayer ClickSound { get; set; }

        public SoundVM()
        {
            SoundDataToShow = new SoundData();
            BGMBtnCommand = new BGMBtnCommand(this);
            EffectBtnCommand = new EffectBtnCommand(this);
            NewGameBtnCommand = new NewGameBtnCommand(this);
            ExitBtnCommand = new ExitBtnCommand(this);
            BackBtnCommand = new BackBtnCommand(this);
            GoSettingWindowBtnCommand = new GoSettingWindowBtnCommand(this);
            ResumeBtnCommand = new ResumeBtnCommand(this);
            GoMainBtnCommand = new GoMainBtnCommand(this);
            ExitCheckWindowCommand = new ExitCheckWindowCommand(this);
            YesBtnCommand = new YesBtnCommand(this);
            NoBtnCommand = new NoBtnCommand(this);
            ShowHelpWindowCommand = new ShowHelpWindowCommand(this);

            BackGroundMusic = new MediaPlayer();
            BackGroundMusic.Open(new Uri("../../Sounds/BGM.mp3", UriKind.Relative));
            BackGroundMusic.MediaEnded += new EventHandler(BGMEnded);
            BackGroundMusic.Play();

            ClickSound = new MediaPlayer();
            ClickSound.Open(new Uri("../../Sounds/Click.mp3", UriKind.Relative));
        }
        public void BGMEnded(object sender, EventArgs e)
        {
            BackGroundMusic.Position = TimeSpan.Zero;
            BackGroundMusic.Play();
       }

        public void setBgm(bool isTrunOnBgm)
        {
            if (isTrunOnBgm)
            {
                SoundDataToShow.IsTurnOnBgm = true;
                BackGroundMusic.Play();
            }

            else
            {
                SoundDataToShow.IsTurnOnBgm = false;
                BackGroundMusic.Pause();
            }
        }
        public void setEffect(bool isTrunOnEffect)
        {
            if (isTrunOnEffect)
            {
                SoundDataToShow.IsTurnOnEffect = true;
            }
            else
            {
                SoundDataToShow.IsTurnOnEffect = false;
            }
        }

        public void playClickSound()
        {
            ClickSound.Play();
            ClickSound.Position = TimeSpan.Zero;
        }
    }
}
