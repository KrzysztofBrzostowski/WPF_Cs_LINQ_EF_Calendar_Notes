using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes
{
    public class PositionOfDay : INotifyPropertyChanged, IDataErrorInfo
    {
        public PositionOfDay()
        {
        }

        private string _CurrentUser = "";
        public string CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                WyslijPowiadomienie("CurrentUser");
            }
        }

        private string _OldUser = "";
        public string OldUser
        {
            get { return _OldUser; }
            set
            {
                _OldUser = value;
            }
        }


        private int _NumberOfPosition;
        public int NumberOfPosition
        {
            get { return _NumberOfPosition; }
            set { _NumberOfPosition = value; }
        }


        private short _OldHour;
        public short OldHour
        {
            get { return _OldHour; }
            set { _OldHour = value; }
        }



        private short _CurrentHour;
        public short CurrentHour
        {
            get { return _CurrentHour; }
            set
            {
                _CurrentHour = value;
                WyslijPowiadomienie("CurrentHour");
            }
        }

        private short _OldMinute;
        public short OldMinute
        {
            get { return _OldMinute; }
            set { _OldMinute = value; }
        }


        private short _CurrentMinute;
        public short CurrentMinute
        {
            get { return _CurrentMinute; }
            set
            {
                _CurrentMinute = value;
                WyslijPowiadomienie("CurrentMinute");
            }
        }


        private string _OldNote = "";
        public string OldNote
        {
            get { return _OldNote; }
            set { _OldNote = value; }
        }


        private string _CurrentNote = "";
        public string CurrentNote
        {
            get { return _CurrentNote; }
            set
            {
                _CurrentNote = value;
                WyslijPowiadomienie("CurrentNote");
            }
        }

        private DateTime _DateTimeVal;
        public DateTime DateTimeVal
        {
            get { return _DateTimeVal; }
            set 
            { 
                _DateTimeVal = value;
                _CurrentHour = (short)_DateTimeVal.Hour;
                _CurrentMinute = (short)_DateTimeVal.Minute;
                WyslijPowiadomienie("DateTimeVal");
            }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        protected void WyslijPowiadomienie(string nazwaWlasciwosci)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(nazwaWlasciwosci));
            }
        }

        public string Error
        {
            get { return "Uncorrect or missing string"; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "CurrentHour":
                        if (CurrentHour < 0 || CurrentHour > 23)
                        {
                            return "Wpisz godzinę pomiędzy 0 i 23";
                        }
                        break;
                    case "CurrentMinute":
                        if (CurrentMinute < 0 || CurrentMinute > 59)
                        {
                            return "Wpisz minute z przedziału 0-59";
                        }
                        break;
                    case "CurrentNote":
                        if (CurrentNote == null) break;
                        if (CurrentNote.Length > 498)
                        {
                            return "Notatka za długa. Max długość notatki to 498 znaków";
                        }
                        break;

                }
                return "";

            }
        }
    }
}
