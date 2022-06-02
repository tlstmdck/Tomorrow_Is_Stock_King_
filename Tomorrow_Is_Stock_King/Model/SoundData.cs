using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomorrow_Is_Stock_King.Model
{
     public class SoundData : INotifyPropertyChanged
     {
        private bool isTurnOnBgm;

        public bool IsTurnOnBgm
        {
            get { return isTurnOnBgm; }
            set { isTurnOnBgm = value; OnPropertyChanged("IsTurnOnBgm"); }
        }

        private bool isTurnOnEffect;

        public bool IsTurnOnEffect
        {
            get { return isTurnOnEffect; }
            set { isTurnOnEffect = value; OnPropertyChanged("IsTurnOnEffect"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // 추후의 사용을 위해 미리 구현
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public SoundData()
        {
            IsTurnOnBgm = true;
            IsTurnOnEffect = true;
        }
    }
}
