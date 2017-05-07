//højklik på holdliste for
//højklik på holdliste for
//højklik på holdliste for
//	1. tilmeld holdliste
//	2. tilføj til kalender

//Højreklik på classeliste for at markere som uønsket
// https://raw.github.com/larspehrsson/fitnessdk/master/publish/setup.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace FitnessDK
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>filg
    public partial class MainWindow : Window
    {
        public CustomerViewModel CustomViewModel = new CustomerViewModel();
        private static DateTime? _valgtdato;
        private static string head = "";
        private readonly BackgroundWorker backgroundWorker1;
        private bool forceupdate = false;

        private DateTime slutDate;

        //private Thread thread;
        //private Window1 splashWindow = new Window1();

        private DateTime startDate;

        private bool TempDisableCenterSelection = false;

        public MainWindow()
        {
            //splashWindow.Show();

            InitializeComponent();

            // thread = new Thread(() =>
            //{
            //    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

            //    var window = new Window1();

            //    // When the window closes, shut down the dispatcher
            //    window.Closed += (s, args) => Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

            //    window.Show();

            //    // Start the Dispatcher Processing
            //    Dispatcher.Run();
            //});
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();

            DataContext = CustomViewModel;

            VælgFavoritCentre();

            backgroundWorker1 = new BackgroundWorker
            {
                //WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            //backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;

            //var view = CustomViewModel.CustomersCollection;
            //using (view.DeferRefresh())
            //{
            //    view.GroupDescriptions.Clear();
            //    view.GroupDescriptions.Add(new PropertyGroupDescription("ugedag"));
            //}
            //dataGrid.ItemsSource = view;
            //CustomViewModel.opdaterazure();
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //CustomViewModel.ClearStat();
            var worker = sender as BackgroundWorker;

            head = HentDataFraFitnessDK.GetHeader();

            var days = Enumerable
                .Range(0, (slutDate - startDate).Days + 1) // check the rounding
                .Select(i => startDate.Date.AddDays(i)).ToList();

            Parallel.ForEach(days, new ParallelOptions { MaxDegreeOfParallelism = 5 }, day => OpdaterData(day));

            CustomViewModel.FindEvents();
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OpdaterButton.IsEnabled = true;
            CustomViewModel.Refresh();
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            _valgtdato = _valgtdato?.AddDays(-1) ?? DateTime.Today;
            DatePicker.SelectedDate = _valgtdato;
        }

        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            _valgtdato = _valgtdato?.AddDays(1) ?? DateTime.Today;
            DatePicker.SelectedDate = _valgtdato;
        }

        private void CenterLB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TempDisableCenterSelection)
                return;
            gemFavoritter.Content = centerLB.SelectedItems.Count == 0 ? "Hent favoritter" : "Gem favoritter";
            CustomViewModel.FavoritCenterList =
                (from object centernavn in centerLB.SelectedItems select centernavn.ToString()).ToList();
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _valgtdato = DatePicker.SelectedDate.GetValueOrDefault();
            CustomViewModel.dato = _valgtdato.GetValueOrDefault();

            if (_valgtdato == DateTime.MinValue)
                _valgtdato = null;

            OpdaterButton.Content = DatePicker.SelectedDate.GetValueOrDefault() == DateTime.MinValue
                ? "Hent"
                : "Genopfrisk";
        }

        private void EventCheckBoxOnClick(object sender, RoutedEventArgs e)
        {
            CustomViewModel.ViewEvents(eventCB.IsChecked.GetValueOrDefault());
        }

        private void gemFavoritter_Click(object sender, RoutedEventArgs e)
        {
            if (centerLB.SelectedItems.Count > 0)
            {
                CustomViewModel.SætFavoritCenter(
                    (from object centernavn in centerLB.SelectedItems select centernavn.ToString()).ToList());
            }
            else
            {
                VælgFavoritCentre();
            }
        }

        private void HoldDG_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomViewModel.ClassesList =
                (from FavoritHold holdnavn in holdLB.SelectedItems select holdnavn.hold).ToList();
        }

        private void HoldLB_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var hold = (FavoritHold)holdLB.SelectedItem;
            if (hold == null) return;

            hold.fravalgt = !hold.fravalgt;

            CustomViewModel.OpdaterFavoritHold(hold);
            //CustomViewModel.SætFavoritHold();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //thread.Abort();
            //splashWindow.Close();
        }
        private void OpdaterButtonClick(object sender, RoutedEventArgs e)
        {
            OpdaterButton.IsEnabled = false;
            forceupdate = System.Windows.Forms.Control.ModifierKeys == Keys.Shift;

            CustomViewModel.ClearHoldChangesCollection();

            if (_valgtdato != null)
            {
                startDate = _valgtdato.Value;
                slutDate = startDate;
                //if (startDate < DateTime.Today)
                //    slutDate = DateTime.Today; //  startDate.AddDays(35); //TODO
            }
            else
            {
                startDate = DateTime.Now.AddDays(0);
                slutDate = startDate.AddDays(35);
            }
            CustomViewModel.StatStartSlut(startDate, slutDate);

            var opdaterWindow = new Opdater(ref CustomViewModel);
            opdaterWindow.Show();

            var changesWindow = new HoldChangesWindow(ref CustomViewModel);
            changesWindow.Show();

            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            else
                startAsyncButton_Click(null, null);
        }

        private void OpdaterData(DateTime dato)
        {
            if ((_valgtdato == DateTime.MinValue || _valgtdato == null) &&
                (dato > DateTime.Now.AddDays(2) || dato < DateTime.Today) &&
                CustomViewModel.HoldPåDag(dato) > 0 &&
                !forceupdate)
            {
                if (dato.Date == new DateTime(2017, 05, 20))
                    Console.WriteLine("er");
                CustomViewModel.SetStat(dato, "Springes over");
                return;
            }

            if (_valgtdato != DateTime.MinValue && _valgtdato < DateTime.Today && CustomViewModel.HoldPåDag(dato) > 1 
                &&              !forceupdate)
            {
                CustomViewModel.SetStat(dato, "Springes over");
                return;
            }
            CustomViewModel.SetStat(dato, "Henter data fra fitness dk");
            var responseContent = HentDataFraFitnessDK.GetXML(head, dato);

            if (responseContent == "error")
            {
                CustomViewModel.SetStat(dato, "Fejl ved hent");
                return;
            }

            //worker.ReportProgress(behandlet * 100 / antal);

            using (var reader = new JsonTextReader(new StringReader(responseContent)))
            {
                while (reader.Read())
                {
                    if (reader.Value?.ToString().Length > 1000)
                    {
                        var xmlstr = reader.Value.ToString();
                        ParseXML(xmlstr, dato.Date);
                    }
                }
            }
            CustomViewModel.SetStat(dato, "Opdateret");
        }

        // http://stackoverflow.com/questions/13172134/linq-to-xml-select-new-purchaseorder
        private void ParseXML(string xmlstr, DateTime dato)
        {
            CustomViewModel.SetStat(dato, "Behandler data");

            var xDocument = XDocument.Parse(xmlstr);

            var items2 = (from g in xDocument.Root.Elements("ul").Elements("li").Elements("ul").Elements("li")
                          select g).ToList();

            var HoldList = new List<Hold>();
            foreach (var element in items2)
            {
                var Hold = new Hold();

                var divitems = element.Elements("div").ToList();
                foreach (var div in divitems)
                {
                    var xx = div.Attribute("class").Value;
                    var værdi = div.Value.Replace("\n", "").Trim();
                    if (xx == "title")
                        Hold.holdnavn = værdi;
                    if (xx == "time")
                    {
                        var ts = DateTime.ParseExact(værdi, "HH.mm", null).TimeOfDay;
                        Hold.tidspunkt = dato.Add(ts);
                    }
                    if (xx == "center")
                        Hold.center = værdi;
                    if (xx == "duration")
                        Hold.varighed = int.Parse(værdi);
                    if (xx == "instructor")
                        Hold.instruktør = værdi;
                    if (xx == "level")
                        Hold.niveau = værdi;
                    if (xx == "buttons")
                    {
                        var url = div.ToString().IndexOf("href");
                        if (url > 0)
                        {
                            var url2 = div.ToString().Substring(url + 7, div.ToString().IndexOf("\"", url) - 10);
                            Hold.tilmeldurl = "https://www.fitnessdk.dk/" + url2;
                        }
                    }
                }
                HoldList.Add(Hold);
            }
            //CustomViewModel.SletHold(dato);
            CustomViewModel.AddHold(HoldList, dato);
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void StatButton_OnClick(object sender, RoutedEventArgs e)
        {
            startDate = DateTime.Now.AddDays(0);
            slutDate = startDate.AddDays(35);
            CustomViewModel.StatStartSlut(startDate, slutDate);

            var holdstatwindow = new Holdstat(ref CustomViewModel);
            holdstatwindow.Show();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CustomViewModel.search = SearchBox.Text;
            SearchBox.Focus();
        }

        private void UIElement_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    //dataGrid.ItemsSource = FiltrerList();
            //    e.Handled = true;
            //    SearchBox.Focus();
            //}
        }

        private void VælgFavoritCentre()
        {
            TempDisableCenterSelection = true;
            foreach (var center in CustomViewModel.HentFavoritCenter())
            {
                centerLB.SelectedItems.Add(center);
            }
            TempDisableCenterSelection = false;
            CenterLB_OnSelectionChanged(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomViewModel.Eksport();

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);
            var srcType = src.GetType();
            if (srcType == typeof(System.Windows.Controls.ListViewItem) || srcType == typeof(GridViewRowPresenter))
            {
                var hold = (Hold)dataGrid.SelectedItem;
                if (hold != null)
                    System.Diagnostics.Process.Start(hold.tilmeldurl);
            }
        }
    }
}