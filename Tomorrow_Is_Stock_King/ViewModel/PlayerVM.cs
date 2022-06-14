using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Tomorrow_Is_Stock_King.Model;
using Tomorrow_Is_Stock_King.ViewModel.Converters;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class PlayerVM
    {
        public List<AIsData> AIsDataToShow { get; set; }
        public PlayerData PlayerDataToShow { get; set; }
        public List<Pair> PlayersData { get; set; }
        public ObservableCollection<string> PlayersDataToShow { get; set; }
        public int Level { get; set; }
        public PlayerVM()
        {
            AIsDataToShow = new List<AIsData>();
            PlayersDataToShow = new ObservableCollection<string>();
            PlayersData = new List<Pair>();
            PlayerDataToShow = new PlayerData();

            for (int i = 1; i <= 9; i++)
            {
                AIsDataToShow.Add(new AIsData(i.ToString(), 30000000 * i));
            }
        }
        
        public void SortPlayers()
        {
            PlayersDataToShow.Clear();
            PlayersData.Clear();

            PlayersData.Add(new Pair(PlayerDataToShow.TotalMoney, PlayerDataToShow.Name));
            for (int i = 0; i < AIsDataToShow.Count; i++)
            {
                PlayersData.Add(new Pair(AIsDataToShow[i].TotalMoney, AIsDataToShow[i].Name));
            }
            PlayersData = PlayersData.OrderByDescending(x => x.First).ToList();

            for (int i = 0; i < 10; i++)
            {
                PlayersDataToShow.Add("       " + PlayersData[i].Second.ToString() + "\n" + String.Format("{0:#,0}", PlayersData[i].First) + "원");
            }
        }

        public void UpdateAIsMoney()
        {
            Random random = new Random();

            for(int i = 0;i < AIsDataToShow.Count; i++)
            {
                int rand = random.Next(1,11); // 0이면 증가, 1이면 감소
                double ran = random.NextDouble() / 100;  // 0~1% 사이로 돈이 증가 또는 감소

                switch (Level)
                {
                    case 1: // 쉬움난이도 -> 40%로 AI돈 증가
                        if (rand < 5) // 60% 확률로 
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 + ran));
                        }
                        else
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 - ran));
                        }
                        break;

                    case 2: // 보통난이도 -> 50%로 AI돈 증가
                        if (rand < 6)
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 + ran));
                        }
                        else
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 - ran));
                        }
                        break;

                    default: // 어려움난이도 -> 60%로 AI돈 증가
                        if (rand < 7)
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 + ran));
                        }
                        else
                        {
                            AIsDataToShow[i].TotalMoney = Convert.ToInt64(AIsDataToShow[i].TotalMoney * (1 - ran));
                        }
                        break;
                }
            }

            SortPlayers();
        }
        
        public void UpdateChangeRate()
        {
            PlayerDataToShow.TotalMoneyChangeRate = (((double)(PlayerDataToShow.TotalMoney - PlayerDataToShow.BeforeTotalMoney) / PlayerDataToShow.BeforeTotalMoney) * 100);
        }
    }
}
