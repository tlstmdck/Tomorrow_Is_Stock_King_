using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class SettingVM
    {
        public SoundData SoundDataToShow { get; set; }
        public SettingData SettingDataToShow { get; set; }
        public BGMBtnCommand BGMBtnCommand { get; set; }
        public EffectBtnCommand EffectBtnCommand { get; set; }

        public SettingVM()
        {
            SettingDataToShow = new SettingData();
        }
    }
}
