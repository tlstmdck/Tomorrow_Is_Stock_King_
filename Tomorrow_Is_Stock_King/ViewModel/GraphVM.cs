using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Tomorrow_Is_Stock_King.Model;

namespace Tomorrow_Is_Stock_King.ViewModel
{
    public class GraphVM : INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;


        public StockVM StockVM { get; set; }

        private SeriesCollection chartseriesCollection;
        public SeriesCollection ChartSeriesCollection
        {
            get { return chartseriesCollection; }
            set { chartseriesCollection = value; OnPropertyChanged(); }
        }
        private SeriesCollection listseriesCollection;
        public SeriesCollection ListSeriesCollection
        {
            get { return listseriesCollection; }
            set { listseriesCollection = value; OnPropertyChanged(); }
        }
        public List<string> StrList { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private int turnnum;
        public GraphVM()
        {
            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(33, 148, 241), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

            ZoomingMode = ZoomingOptions.X;
            XFormatter = val => (Int32.Parse(new DateTime((long)val).ToString("yyy"))-1).ToString();
            YFormatter = val => val.ToString("C");

            ListSeriesCollection = new SeriesCollection();
            StrList = new List<string>();
            turnnum = 0;
        }
        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged();
            }
        }
        private ChartValues<DateTimePoint> GetData()
        {
            var r = new Random();
            var trend = 100;
            var values = new ChartValues<DateTimePoint>();

            for (var i = 0; i < 100; i++)
            {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(new DateTimePoint(DateTime.Now.AddDays(i), trend + r.Next(0, 10)));
            }

            return values;
        }
        private ChartValues<DateTimePoint> GetData(List<List<Item>> TurnList, int index)
        {
            var values = new ChartValues<DateTimePoint>();
            
            for (int i=0; i< TurnList.Count; i++)
            {
                DateTime turndate = new DateTime(0);
                values.Add(new DateTimePoint(turndate.AddYears(i), Double.Parse(TurnList[i][index].Clpr)));
            }
            return values;
        }

        public void ChangeData(List<List<Item>> TurnList, int index)
        {
            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(33, 148, 241), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));
            ChartSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = GetData(TurnList, index),
                    Fill = gradientBrush,
                    StrokeThickness = 1,
                    PointGeometrySize = 0
                }
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddListStockData(Dictionary<string, int> stocks)
        {
            
            foreach(string key in stocks.Keys)
            {
                if (!StrList.Contains(key))
                {
                    StrList.Add(key);
                }
                
                
            }
            for(int i=ListSeriesCollection.Count; i<stocks.Count; i++)
            {
                PieSeries temp = new PieSeries { Title = StrList[i], Values = new ChartValues<ObservableValue> { new ObservableValue(stocks[StrList[i]]) }, DataLabels = true };
                ListSeriesCollection.Add(temp);
                
            }
            
        }
        public void RemoveListStockData(string itemnms)
        {
            int index = StrList.IndexOf(itemnms);
            if(ListSeriesCollection.Count > 0)
            {
                ListSeriesCollection.RemoveAt(index);
                StrList.Remove(itemnms);
            }
        }
        public void UpdateListStockData(Dictionary<string, int> stocks)
        {
            int index = 0;
            foreach(var series in ListSeriesCollection)
            {
                
                foreach(var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = stocks[StrList[index]];
                }
                index++;
            }
        }
        public void UpdateListMoneyData(List<List<Item>> turnlist, ObservableCollection<string> companies, Dictionary<string, int> stocks)
        {
            int StrList_index = 0;
            foreach (var series in ListSeriesCollection)
            {

                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    int stock_index = companies.IndexOf(StrList[StrList_index]);
                    int Clpr_num = Int32.Parse(turnlist[turnlist.Count - 1][stock_index].Clpr);
                    observable.Value = Clpr_num * stocks[StrList[StrList_index]];
                }
                StrList_index++;
            }
        }
    }

}
