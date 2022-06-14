using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomorrow_Is_Stock_King.ViewModel.Commands.ShowHelpWindowCommands;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    internal class PageNavigateVM : INotifyPropertyChanged
    {
        public PageNavigateCommand PageNavigateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        private List<string> pageNames;
        public List<string> PageNames
        {
            get { return pageNames; }
            set {
                pageNames = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PageNames"));
            }
        }

        private string currentPage;
        public string CurrentPage
        {
            get { return currentPage; }
            set {
                currentPage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        private int currentPageInd;
        public int CurrentPageInd
        {
            get { return currentPageInd; }
            set
            {
                currentPageInd = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPageInd"));
            }
        }

        private int totalPageCnt;
        public int TotalPageCnt
        {
            get { return totalPageCnt; }
            set
            {
                totalPageCnt = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPageCnt"));
            }
        }

        public PageNavigateVM()
        {
            PageNavigateCommand = new PageNavigateCommand(this);

            PageNames = new List<string>();
            PageNames.Add("");
            PageNames.Add("HelpWindowPages/FirstPage.xaml");
            PageNames.Add("HelpWindowPages/SecondPage.xaml");
            PageNames.Add("HelpWindowPages/ThirdPage.xaml");
            PageNames.Add("HelpWindowPages/FourthPage.xaml");
            PageNames.Add("HelpWindowPages/FifthPage.xaml");
            PageNames.Add("HelpWindowPages/SixthPage.xaml");
            PageNames.Add("HelpWindowPages/SeventhPage.xaml");
            PageNames.Add("HelpWindowPages/EighthPage.xaml");
            PageNames.Add("HelpWindowPages/NinthPage.xaml");

            CurrentPageInd = 1;
            TotalPageCnt = PageNames.Count - 1;
            NavigateTo(CurrentPageInd);
        }

        public void NavigateTo(int pageNum)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            if (pageNum > PageNames.Count - 1)
            {
                pageNum = PageNames.Count - 1;
            }
            CurrentPage = PageNames[pageNum];
            CurrentPageInd = pageNum;
        }
    }
}
