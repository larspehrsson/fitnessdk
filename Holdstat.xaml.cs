using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace FitnessDK
{
    /// <summary>
    ///     Interaction logic for Holdstat.xaml
    /// </summary>
    public partial class Holdstat : Window
    {
        private readonly CustomerViewModel _CustomViewModel;

        public Holdstat(ref CustomerViewModel CustomViewModel)
        {
            _CustomViewModel = CustomViewModel;

            InitializeComponent();

            var startdato = DateTime.Today.AddYears(-1);

            var firstdata = _CustomViewModel.StatCollection[0].tidspunkt;
            var lastdata = _CustomViewModel.StatCollection[_CustomViewModel.StatCollection.Count-1].tidspunkt;
            if (firstdata > startdato)
                startdato = firstdata;
            CustomViewModel.StatStartSlut(startdato,lastdata);

            seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Hold",
                    Values = LoadData()
                }
            };

            XFormatter = val => new DateTime((long) val).ToString("dddd dd MMM yyyy");
            YFormatter = val => val.ToString("N1");

            DataContext = this;
        }

        public SeriesCollection seriesCollection { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private ChartValues<DateTimePoint> LoadData()
        {
            var values = new ChartValues<DateTimePoint>();

            foreach (var f in _CustomViewModel.StatCollection)
            {
                values.Add(new DateTimePoint(f.tidspunkt, f.antal));
            }

            return values;
        }
    }
}