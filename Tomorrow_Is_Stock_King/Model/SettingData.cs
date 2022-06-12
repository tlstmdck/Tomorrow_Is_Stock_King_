using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tomorrow_Is_Stock_King.Model
{
    public class SettingData : INotifyPropertyChanged
    {
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; OnPropertyChanged("Level"); }
        }

        private int updateTime;

        public int UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; OnPropertyChanged("UpdateTime"); }
        }

        private int turnCnt;

        public int TurnCnt
        {
            get { return turnCnt; }
            set { turnCnt = value; OnPropertyChanged("TurnCnt"); }
        }


        // 추후의 사용을 위해 미리 구현
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private int intformation;
        public int Information
        {
            get { return intformation; }
            set { intformation = value; OnPropertyChanged("Information"); }
        }

        private Dictionary<int, bool> popUpEvent;
        public Dictionary<int, bool> PopUpEvent
        {
            get { return popUpEvent; }
            set { popUpEvent = value; }
        }


        public SettingData()
        {
            Level = 1;
            UpdateTime = 10;
            TurnCnt = 0;
            intformation = 3;
            popUpEvent = new Dictionary<int, bool>();

            int turn = 5;
            Random random = new Random();
            for(int i = 0;i < 10; i++)
            {
                int rand = random.Next(2);
                bool flag = (rand == 0) ? true : false;
                PopUpEvent.Add(turn + i * 10, flag);
            }
        }
    }
}
