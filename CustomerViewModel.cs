using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using LiteDB;
using OfficeOpenXml;

namespace FitnessDK
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly LiteDatabase _db;
        private readonly LiteCollection<FavoritCenter> _favoritCentreCollection;
        private readonly LiteCollection<FavoritHold> _favoritHoldCollection;
        private readonly LiteCollection<Hold> _holddataCollection;
        private List<HoldChanges> _holdChangesesCollection;

        private readonly object statLock = new object();
        private readonly object dbLock = new object();

        private List<string> _classesList;
        private DateTime _dato;

        private List<string> _favoritCenterList;
        private List<FavoritHold> _favoritHoldList;
        private string _search = "";
        private DateTime _slutTime;

        private DateTime _starTime;

        private List<Stat> _statList = new List<Stat>();
        private bool _viewEvents;

        public CustomerViewModel()
        {
            search = "";
            var dbname = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Fitnessdk");
            if (!Directory.Exists(dbname))
                Directory.CreateDirectory(dbname);
            dbname = Path.Combine(dbname, "fitnessdata.db");
            //if (!File.Exists(dbname))
            //    File.Copy(@"FitnessData.db", dbname);
            //dbname = "fitnessdata.db"; //TODO
            _db = new LiteDatabase(dbname);
            //_db.Shrink();
            _holddataCollection = _db.GetCollection<Hold>("holddata");
            _holddataCollection.EnsureIndex("tidspunkt");
            _favoritCentreCollection = _db.GetCollection<FavoritCenter>("favoritcentre");
            _favoritHoldCollection = _db.GetCollection<FavoritHold>("favorithold");
            FavoritHoldList = _db.GetCollection<FavoritHold>("favorithold").FindAll().ToList();
            FavoritCenterList = _db.GetCollection<FavoritCenter>("favoritcentre").FindAll().Select(c => c.center).ToList();
            _classesList = new List<string>();
        }



        public ObservableCollection<FavoritHold> FavoritHoldCollection
        {
            get
            {
                var favoritter = _favoritHoldCollection.FindAll().ToList();
                foreach (var hold in ClassesCollection)
                    if (!favoritter.Any(c => c.hold == hold))
                    {
                        var nythold = new FavoritHold { hold = hold, fravalgt = false };
                        favoritter.Add(nythold);
                        //_favoritHoldCollection.Insert(nythold);
                    }

                return new ObservableCollection<FavoritHold>(favoritter.OrderBy(c => c.hold));
            }
        }


        public ICollectionView CentersCollection
        {
            get
            {
                var view =
                    CollectionViewSource.GetDefaultView(
                        new ObservableCollection<string>(
                            TotalHoldList.Select(c => c.center).Distinct().OrderBy(c => c).ToList()));
                //view.Filter = o => (o as Member).Name.Contains((sender as TextBox).Text);
                return view;
            }
        }

        public ICollectionView CustomersCollection
        {
            get
            {
                var view = CollectionViewSource.GetDefaultView(new ObservableCollection<Hold>(FiltrerList()));
                //view.GroupDescriptions.Clear();
                //view.GroupDescriptions.Add(new PropertyGroupDescription("ugedag"));
                //view.GroupDescriptions.Add(new PropertyGroupDescription("Complete"));
                //using (view.DeferRefresh())
                //{
                //    PropertyInfo pinfo = typeof(object).GetProperty(Hold);

                //    //put your changes in sorting and grouping here
                //    view.GroupDescriptions.Add(new PropertyGroupDescription(pinfo));
                //}
                return view;
            }
        }


        public ObservableCollection<string> ClassesCollection
        {
            get
            {
                IOrderedEnumerable<string> ClassList;
                if (dato == DateTime.MinValue)
                    ClassList =
                        TotalHoldList.Where(c => c.tidspunkt >= DateTime.Today)
                            .Select(c => c.holdnavn)
                            .Distinct()
                            .OrderBy(c => c);
                else
                    ClassList =
                        TotalHoldList.Where(c => c.tidspunkt >= dato && c.tidspunkt < dato.AddDays(1))
                            .Select(c => c.holdnavn)
                            .Distinct()
                            .OrderBy(c => c);
                return new ObservableCollection<string>(ClassList);
            }
        }

        public ObservableCollection<Stat> StatCollection
        {
            get
            {
                return new ObservableCollection<Stat>(_statList.OrderBy(c => c.tidspunkt));
            }
        }


        public int HoldPåDag(DateTime dato)
        {
            lock (dbLock)
            {
                var ant = _totalHoldList.Count(c => c.tidspunkt >= dato.Date && c.tidspunkt <= dato.Date.AddDays(1));
                return ant;
            }

            //return _holddataCollection.Find(
            //    Query.And(
            //        Query.GTE("tidspunkt", dato),
            //        Query.LT("tidspunkt", dato.AddDays(1))
            //        )).Count();
        }

        private List<Hold> _totalHoldList = new List<Hold>();

        private List<Hold> TotalHoldList
        {
            get
            {
                if (!_totalHoldList.Any())
                    _totalHoldList = _holddataCollection.FindAll().ToList();//.Where(c => c.tidspunkt >= DateTime.Today).ToList();

                if (_dato == DateTime.MinValue)
                    return _totalHoldList;

                //if (_dato >= DateTime.Today)
                return _totalHoldList.Where(c => c.tidspunkt >= _dato && c.tidspunkt <= _dato.AddDays(1)).ToList();

                //return _holddataCollection.Find(
                //    Query.And(
                //        Query.GTE("tidspunkt", _dato),
                //        Query.LT("tidspunkt", _dato.AddDays(1))
                //    )).ToList();
            }
            set
            {
                _totalHoldList = value;
            }
        }

        //public void opdaterazure()
        //{
        //    FitnessdkEntities fitness = new FitnessdkEntities();
        //    foreach (var post in TotalHoldList.Where(c => c.tidspunkt >= DateTime.Today).OrderBy(c => c.tidspunkt))
        //    {
        //        var hold = fitness.hold.FirstOrDefault(c => c.Id == post.Id);
        //        if (hold != null)
        //            fitness.hold.Remove(hold);

        //        fitness.hold.Add(
        //            new hold
        //                {
        //                    center = post.center,
        //                    holdnavn = post.holdnavn,
        //                    Id = post.Id,
        //                    instruktør = post.instruktør,
        //                    isevent = post.isevent,
        //                    niveau = post.niveau,
        //                    tidspunkt = post.tidspunkt,
        //                    tilmeldurl = post.tilmeldurl,
        //                    varighed = post.varighed,
        //                });
        //    fitness.SaveChanges();
        //    }
        //}

        public string search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged("CustomersCollection");
            }
        }

        public List<string> ClassesList
        {
            get { return _classesList; }
            set
            {
                _classesList = value;
                OnPropertyChanged("CustomersCollection");
            }
        }

        //public List<string> title { get; set; }

        public List<string> FavoritCenterList
        {
            get { return _favoritCenterList; }
            set
            {
                _favoritCenterList = value;
                OnPropertyChanged("CustomersCollection");
            }
        }

        public List<FavoritHold> FavoritHoldList
        {
            get { return _favoritHoldList; }
            set
            {
                _favoritHoldList = value;
                OnPropertyChanged("CustomersCollection");
            }
        }


        public DateTime dato
        {
            get { return _dato; }
            set
            {
                _dato = value;
                OnPropertyChanged("CustomersCollection");
                OnPropertyChanged("FavoritHoldCollection");

            }
        }

        public List<string> FravalgtHoldList()
        {
            var favoritter = _favoritHoldCollection.FindAll().ToList().Where(c => c.fravalgt).Select(c => c.hold);
            return favoritter.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void StatStartSlut(DateTime start, DateTime slut)
        {
            _starTime = start;
            _slutTime = slut;

            _statList =
                _totalHoldList.Where(c => c.tidspunkt.Date >= _starTime.Date && c.tidspunkt.Date <= _slutTime.Date)
                    .OrderBy(c => c.tidspunkt)
                    .GroupBy(c => c.tidspunkt.Date)
                    .Select(group =>
                        new Stat
                        {
                            tidspunkt = group.Key,
                            antal = group.Count(),
                            status = ""
                        }).ToList();

            for (var i = _starTime; i <= _slutTime; i = i.AddDays(1))
            {
                if (!_statList.Any(c => c.tidspunkt == i.Date))
                {
                    _statList.Add(new Stat
                    {
                        tidspunkt = i,
                        antal = 0,
                        status = ""
                    });
                }
            }
            OnPropertyChanged("StatCollection");
        }

        //public void ClearStat()
        //{
        //    foreach (var statpost in _statList)
        //    {
        //        statpost.status = "";
        //    }
        //    OnPropertyChanged("StatCollection");
        //}

        public void SetStat(DateTime dato, string status, int antal = -1)
        {
            lock (statLock)
            {

                var statpost = _statList.FirstOrDefault(c => c.tidspunkt.Date == dato.Date);
                if (statpost == null)
                {
                    _statList.Add(new Stat
                    {
                        tidspunkt = dato,
                        status = status
                    });
                }
                else
                {
                    statpost.status = status;
                    if (antal > -1)
                        statpost.antal = antal;
                }

                OnPropertyChanged("StatCollection");
            }
        }

        public string GetStat(DateTime dato)
        {
            var statpost = _statList.FirstOrDefault(c => c.tidspunkt.Date == dato.Date);
            if (statpost == null)
            {
                return "";
            }
            return statpost.status;
        }

        public void FindEvents()
        {
            foreach (var hold in _holddataCollection.FindAll().Where(c => c.isevent && c.isevent && c.tidspunkt >= DateTime.Today))
            {
                hold.isevent = false;
                _holddataCollection.Update(hold);
            }

            var list2 =
                _holddataCollection.FindAll()
                .Where(c => c.tidspunkt >= DateTime.Today)
                    .GroupBy(c => new { c.holdnavn, c.center, c.tidspunkt.DayOfWeek })
                    .Where(grp => grp.Count() <= 1)
                    .SelectMany(c => c).ToList();

            foreach (var hold in list2)
            {
                if (!hold.isevent)
                {
                    hold.isevent = true;
                    _holddataCollection.Update(hold);
                }
            }

            var list =
                _holddataCollection.FindAll()
                    .Where(c => c.holdnavn.ToLower().Contains("event") || c.holdnavn.ToLower().Contains("release")).Where(c => c.isevent == false).ToList();

            foreach (var hold in list)
            {
                hold.isevent = true;
                _holddataCollection.Update(hold);
            }

            //foreach (var hold in _holddataCollection.FindAll().ToList().Where(c => c.tidspunkt >= DateTime.Today).OrderBy(c => c.tidspunkt))
            //{
            //    var nexthold =
            //        TotalHoldList.Any(
            //            c =>
            //                c.holdnavn == hold.holdnavn && c.center == hold.center &&
            //                c.tidspunkt == hold.tidspunkt.AddDays(7));
            //    var prevhold =
            //        TotalHoldList.Any(
            //            c =>
            //                c.holdnavn == hold.holdnavn && c.center == hold.center &&
            //                c.tidspunkt == hold.tidspunkt.AddDays(-7));

            //    var isevent = !prevhold && !nexthold;
            //    if (hold.isevent != isevent)
            //    {
            //        hold.isevent = isevent;
            //        _holddataCollection.Update(hold);
            //    }
            //    hold.isevent = !prevhold && !nexthold;
            //}
        }


        public void Eksport()
        {
            ExcelPackage package = new ExcelPackage();
            var fileName = @"fitnessdk.xlsx";

            if (File.Exists(fileName))
                File.Delete(fileName);

            // Create the file using the FileInfo object
            var file = new FileInfo(fileName);

            // Create the package
            package = new ExcelPackage(file);

            // add a new worksheet to the empty workbook
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("fitness dk");
            //worksheet.View.FreezePanes(1, 2);

            int rowNumber = 1;
            var j = 1;
            worksheet.Cells[rowNumber, j++].Value = "holdnavn";
            worksheet.Cells[rowNumber, j++].Value = "tidspunkt";
            worksheet.Cells[rowNumber, j++].Value = "center";
            worksheet.Cells[rowNumber, j++].Value = "instruktør";
            worksheet.Cells[rowNumber, j++].Value = "varighed";
            worksheet.Cells[rowNumber, j++].Value = "niveau";
            worksheet.Cells[rowNumber, j++].Value = "event";

            foreach (var Hold in TotalHoldList.OrderBy(c => c.tidspunkt))
            {
                rowNumber++;
                j = 1;

                worksheet.Cells[rowNumber, j++].Value = Hold.holdnavn;
                worksheet.Cells[rowNumber, j++].Value = Hold.tidspunkt;
                worksheet.Cells[rowNumber, j++].Value = Hold.center;
                worksheet.Cells[rowNumber, j++].Value = Hold.instruktør;
                worksheet.Cells[rowNumber, j++].Value = Hold.varighed;
                worksheet.Cells[rowNumber, j++].Value = Hold.niveau;
                worksheet.Cells[rowNumber, j++].Value = Hold.isevent;
            }

            worksheet.Column(2).Style.Numberformat.Format = "yyyy-mm-dd HH:MM";

            //var range2 = worksheet.Cells["A:C"];
            var range2 = worksheet.Cells[1, 1, rowNumber, j - 1];  // [FromRow, FromCol, ToRow, ToCol] l

            range2.AutoFitColumns();

            // add the excel table entity
            var table = worksheet.Tables.Add(range2, "data");
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium6;

            // Set some document properties
            package.Workbook.Properties.Title = "Fitnessdk";
            //package.Workbook.Properties.Author = "Lars Pehrsson";
            //package.Workbook.Properties.Company = "DSA";

            // save our new workbook and we are done!
            package.Save();

            System.Diagnostics.Process.Start(fileName);
        }


        public void ViewEvents(bool vieweventsonly)
        {
            _viewEvents = vieweventsonly;

            OnPropertyChanged("CustomersCollection");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void OpdaterFavoritHold(FavoritHold hold)
        {
            var list = _favoritHoldCollection.FindAll().Where(c => c.fravalgt).ToList();
            var slethold = list.FirstOrDefault(c => c.hold == hold.hold);
            if (slethold != null)
                list.Remove(slethold);
            if (hold.fravalgt)
                list.Add(hold);

            _db.DropCollection("favorithold");
            foreach (var gemhold in list.Where(c => c.fravalgt))
                _favoritHoldCollection.Insert(gemhold);

            OnPropertyChanged("HoldCollection");
            OnPropertyChanged("FavoritHoldCollection");
        }


        public List<string> HentFavoritCenter()
        {
            return _favoritCentreCollection.FindAll().Select(c => c.center).ToList();
        }

        public void SætFavoritCenter(List<string> centreList)
        {
            _db.DropCollection("favoritcentre");
            foreach (var centernavn in centreList)
                _favoritCentreCollection.Insert(new FavoritCenter { center = centernavn });
            OnPropertyChanged("CentersCollection");
        }

        public void ClearHoldChangesCollection()
        {
            _holdChangesesCollection = new List<HoldChanges>();
        }


        public ObservableCollection<HoldChanges> HoldChangesCollection
        {
            get
            {
                return new ObservableCollection<HoldChanges>(_holdChangesesCollection.OrderBy(c => c.tidspunkt).ThenBy(c => c.center));
            }
        }


        public void AddHold(List<Hold> newHoldLlist, DateTime dato)
        {
            lock (dbLock)
            {
                var oldholdlist = _totalHoldList.Where(c => c.tidspunkt >= dato && c.tidspunkt <= dato.AddDays(1)).ToList();

                if (oldholdlist.Count > 0)
                {
                    foreach (var oldhold in oldholdlist)
                    {
                        var newhold = newHoldLlist.FirstOrDefault(c =>
                            c.center == oldhold.center &&
                            c.holdnavn == oldhold.holdnavn && c.instruktør != oldhold.instruktør &&
                            c.tidspunkt == oldhold.tidspunkt);
                        if (newhold != null)
                        {
                            HoldChanges ændring = new HoldChanges
                            {
                                tidspunkt = oldhold.tidspunkt,
                                holdnavn = oldhold.holdnavn,
                                center = oldhold.center,
                                instruktør = oldhold.instruktør + " -> " + newhold.instruktør,
                                niveau = oldhold.niveau,
                                varighed = oldhold.varighed,
                                Change = "Instruktør"
                            };
                            _holdChangesesCollection.Add(ændring);
                            OnPropertyChanged("HoldChangesCollection");
                        }

                        if (
                            !newHoldLlist.Any(
                                c =>
                                    c.holdnavn == oldhold.holdnavn && c.center == oldhold.center &&
                                    c.tidspunkt == oldhold.tidspunkt))
                        {
                            HoldChanges ændring = new HoldChanges
                            {
                                tidspunkt = oldhold.tidspunkt,
                                holdnavn = oldhold.holdnavn,
                                center = oldhold.center,
                                instruktør = oldhold.instruktør,
                                niveau = oldhold.niveau,
                                varighed = oldhold.varighed,
                                Change = "Slettet"
                            };
                            _holdChangesesCollection.Add(ændring);
                            OnPropertyChanged("HoldChangesCollection");
                        }
                    }

                    foreach (var newhold in newHoldLlist)
                    {
                        if (
                            !oldholdlist.Any(
                                c =>
                                    c.holdnavn == newhold.holdnavn && c.center == newhold.center &&
                                    c.tidspunkt == newhold.tidspunkt))
                        {
                            HoldChanges ændring = new HoldChanges
                            {
                                tidspunkt = newhold.tidspunkt,
                                holdnavn = newhold.holdnavn,
                                center = newhold.center,
                                instruktør = newhold.instruktør,
                                niveau = newhold.niveau,
                                varighed = newhold.varighed,
                                Change = "Tilføjet"
                            };
                            _holdChangesesCollection.Add(ændring);
                            OnPropertyChanged("HoldChangesCollection");
                        }
                    }

                    foreach (var slet in oldholdlist)
                        _holddataCollection.Delete(slet.Id);

                    foreach (var slethold in oldholdlist)
                        _totalHoldList.Remove(slethold);
                }

                using (var trans = _db.BeginTrans())
                {
                    foreach (var hold in newHoldLlist)
                        _holddataCollection.Insert(hold);
                    trans.Commit();
                }

                _totalHoldList.AddRange(newHoldLlist);

                SetStat(dato, "Opdateret", newHoldLlist.Count);
                OnPropertyChanged("StatCollection");
                if (_starTime == _slutTime)
                    OnPropertyChanged("CustomersCollection");
            }
        }


        public void Refresh()
        {
            OnPropertyChanged("CustomersCollection");
        }


        private IEnumerable<Hold> FiltrerList()
        {
            var fravalgt = FravalgtHoldList();
            return TotalHoldList.Where(c =>
                (ClassesList.Count == 0 || ClassesList.Contains(c.holdnavn)) &&
                (FavoritCenterList.Count == 0 || FavoritCenterList.Contains(c.center)) &&
                (
                  (dato != DateTime.MinValue && c.tidspunkt >= dato && c.tidspunkt < dato.AddDays(1)) ||
                  (dato == DateTime.MinValue && c.tidspunkt >= DateTime.Now)
                ) &&
                 (_viewEvents == false || c.isevent) &&
                 (!fravalgt.Contains(c.holdnavn) &&
                 (c.center.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                  c.instruktør.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                  c.ugedag.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                  c.niveau.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                  c.holdnavn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                ).OrderBy(c => c.tidspunkt).ThenBy(c => c.center).ThenBy(c => c.holdnavn);
        }
    }
}