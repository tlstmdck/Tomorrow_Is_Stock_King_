using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Commands.StartSettingWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class PlayerVM
    {
        public List<AIsData> AIsDataToShow { get; set; }
        public PlayerData PlayerDataToShow { get; set; }
        public List<Pair> PlayersData { get; set; }
        public ObservableCollection<string> PlayersDataToShow { get; set; }
       
        public PlayerVM()
        {
            AIsDataToShow = new List<AIsData>();
            PlayersDataToShow = new ObservableCollection<string>();
            PlayersData = new List<Pair>();
            for (int i = 1; i <= 9; i++)
            {
                AIsDataToShow.Add(new AIsData(i.ToString(), (long)500000000 * i));
            }
            PlayerDataToShow = new PlayerData();
        }
        
        public void sortPlayers()
        {
            PlayersDataToShow.Clear();
            PlayersData.Clear();

            PlayersData.Add(new Pair(PlayerDataToShow.TotalMoney, PlayerDataToShow.Name));
            MessageBox.Show(PlayerDataToShow.TotalMoney + " 이름 : " + PlayerDataToShow.Name);
            for (int i = 0; i < AIsDataToShow.Count; i++)
            {
                MessageBox.Show(AIsDataToShow[i].TotalMoney + " 이름 : " + AIsDataToShow[i].Name);
                PlayersData.Add(new Pair(AIsDataToShow[i].TotalMoney, AIsDataToShow[i].Name));
            }
            PlayersData = PlayersData.OrderByDescending(x => x.First).ToList();

            for (int i = 0; i < 10; i++)
            {
                PlayersDataToShow.Add(PlayersData[i].Second.ToString());
            }
        }

        public void UpdateAIsMoney()
        {
            Random random = new Random();
            
            for(int i = 0;i < AIsDataToShow.Count; i++)
            {
                int ran = random.Next(1, 11);

                if(ran <= 5)
                {
                    AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * 1.03);
                }
                else
                {
                    AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * 0.97);
                }
                MessageBox.Show(AIsDataToShow[i].Name + " 돈: " + AIsDataToShow[i].TotalMoney.ToString());
            }

            sortPlayers();
        }
    }
}
