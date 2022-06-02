using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class SoundVM
    {
        public SoundData SoundDataToShow { get; set; }
        public BGMBtnCommand BGMBtnCommand { get; set; }
        public EffectBtnCommand EffectBtnCommand { get; set; }

        public SoundVM()
        {
            SoundDataToShow = new SoundData();
            BGMBtnCommand = new BGMBtnCommand(this);
            EffectBtnCommand = new EffectBtnCommand(this);
        }

        public void setBgm(bool isTrunOnBgm)
        {
            if (isTrunOnBgm) SoundDataToShow.IsTurnOnBgm = true;
            else SoundDataToShow.IsTurnOnBgm = false;
        }
        public void setEffect(bool isTrunOnEffect)
        {
            if (isTrunOnEffect) SoundDataToShow.IsTurnOnEffect = true;
            else SoundDataToShow.IsTurnOnEffect = false;
        }
    }
}
