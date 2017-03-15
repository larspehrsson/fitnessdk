using System;
using System.Globalization;
using LiteDB;

namespace FitnessDK
{
    public class FavoritCenter
    {
        public int Id { get; set; }
        public string center { get; set; }
    }

    public class FavoritHold
    {
        public int Id { get; set; }
        public string hold { get; set; }
        public bool fravalgt { get; set; }
    }


    public class Hold
    {
        public int Id { get; set; }

        [BsonIndex]
        public string holdnavn { get; set; }

        [BsonIndex]
        public DateTime tidspunkt { get; set; }

        public string instruktør { get; set; }
        public int varighed { get; set; }
        public string niveau { get; set; }
        public string tilmeldurl { get; set; }
        [BsonIndex]
        public string center { get; set; }

        public bool isevent { get; set; }

        public string ugedag
        {
            get
            {
                CalendarWeekRule weekRule = CalendarWeekRule.FirstFourDayWeek;
                DayOfWeek firstWeekDay = DayOfWeek.Monday;
                Calendar calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;

                int currentWeek = calendar.GetWeekOfYear(tidspunkt, weekRule, firstWeekDay);
                return tidspunkt.Date.ToString("dddd") + " i uge " + currentWeek;
            }
        }
    }

    public class HoldChanges : Hold
    {
        public string Change { get; set; }
    }

    public class Stat
    {
        public DateTime tidspunkt { get; set; }
        public int antal { get; set; }
        public string status { get; set; }
    }
}