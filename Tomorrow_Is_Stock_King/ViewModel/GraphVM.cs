using System;
using System.Collections.Generic;
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
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }
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

            XFormatter = val => new DateTime((long)val).ToString("dd MMM");
            YFormatter = val => val.ToString("C");

            AddListStockData();
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

            for(int i=0; i< TurnList.Count; i++)
            {
                values.Add(new DateTimePoint(DateTime.Now.AddDays(i), Double.Parse(TurnList[i][index].Clpr)));
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

        public void AddListStockData()
        {
            List<PieSeries> pieSeries = new List<PieSeries>();
            ListSeriesCollection = new SeriesCollection
            {
                 new PieSeries
                {
                    Title = "삼성전자",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "카카오",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "네이버",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "SK하이닉스",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                }
            };
        }
        public void RemoveListStockData()
        {

        }
    }

}
