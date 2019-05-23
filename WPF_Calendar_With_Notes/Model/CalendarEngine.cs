using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using WPF_Calendar_With_Notes.Utilities;
using System.Data.Entity;
using WPF_Calendar_With_Notes.CommonTypes;
using WPF_Calendar_With_Notes.DAL;

namespace WPF_Calendar_With_Notes
{
    public class CalendarEngine : INotifyPropertyChanged, IListener, IData
    {
        private bool _isDataBaseOK;

        private string _DataBaseState;
        public string DataBaseState
        {
            get { return _DataBaseState; }
            set { _DataBaseState = value; WyslijPowiadomienie("DataBaseState"); }
        }

        private string _LanguageName = "Culture Info: " + CultureInfo.CurrentUICulture.DisplayName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set
            {
                _LanguageName = value;
                WyslijPowiadomienie("LanguageName");
            }
        }

        private int _NumberOfPositionForDataGrid;
        public int NumberOfPositionForDataGrid
        {
            get { return _NumberOfPositionForDataGrid; }
            set
            {
                _NumberOfPositionForDataGrid = value;
                WyslijPowiadomienie("NumberOfPositionForDataGrid");
            }
        }

        private List<DateTime> _DtList = new List<DateTime>();
        public List<DateTime> DtList
        {
            get
            {
                return _DtList;
            }
        }

        private DateTime _SelectedDate;
        public DateTime Selected_Date
        {
            get
            {
                return _SelectedDate;
            }

            set
            {
                if (value != _SelectedDate)
                {
                    _SelectedDate = value;

                    //W kontrolce Calendar, po kliknięciu
                    //kolekcja dat przestaje byc zaznaczona
                    //rozwiazanie: 1. Watek uaktualniajacy lub 2. powiadomienie MainWindow o potrzebie zaznaczenia
                    DtList.Clear();
                    DtList.Add(_SelectedDate);
                    foreach (var item in m_notesDB.BrokerNotes)
                    {
                        DtList.Add(item.Date);
                    }

                    m_Broker.FireEvent(EventType.SelectedDateChanged, this);

                    UpdateOfPositions();
                }
            }
        }

        private ObservableCollection<PositionOfDay> _Positions;
        public ObservableCollection<PositionOfDay> Positions { get { return _Positions; } }

        private IEventBroker m_Broker;
        public INotesContext<Note> m_notesDB;
        public CalendarEngine(IEventBroker broker)
        {
            m_Broker = broker;
            m_Broker.RegisterFor(EventType.LanguageChanged, this);
            _Positions = new ObservableCollection<PositionOfDay>();            

            try
            {
                m_notesDB = DBSingleton<RealNotesContext>.Instancja;
                var notes = m_notesDB.BrokerNotes.Count();
                _isDataBaseOK = true;
                //DataBaseState = Properties.Resources.DataBaseStateOK;
            }
            catch(Exception e)
            {
                m_notesDB = DBSingleton<FakeNotesContext>.Instancja;
                _isDataBaseOK = false;
                //DataBaseState = Properties.Resources.DataBaseStateFails;
            }
            finally
            {
                if (_isDataBaseOK)
                {
                    DataBaseState = Properties.Resources.DataBaseStateOK;
                }else
                {
                    DataBaseState = Properties.Resources.DataBaseStateFails;
                }
            }

            DateTime dt_tmp = DateTime.Now;
            Selected_Date = new DateTime(dt_tmp.Year, dt_tmp.Month, dt_tmp.Day);
        }


        public void NotifyMe(EventType type, object data)
        {
            if (type == EventType.LanguageChanged)
            {
                LanguageName = "Culture Info: " + CultureInfo.CurrentUICulture.DisplayName;
                if (_isDataBaseOK)
                {
                    DataBaseState = Properties.Resources.DataBaseStateOK;
                }
                else
                {
                    DataBaseState = Properties.Resources.DataBaseStateFails;
                }
            }
        }

        public void UpdateOfPositions()
        {
            if (Positions.Count != 0)
                Positions.Clear();

            IEnumerable<FieldsOfDataGrid> _list = GetNotesForSelectedDay(_SelectedDate);

            if (_list.Count() == 0)
            {
                NumberOfPositionForDataGrid = 0;
            }
            else
            {
                int i = 0;
                foreach (var rowOfDataGrid in _list)
                {
                    Positions.Add(new PositionOfDay()
                    {
                        CurrentHour = rowOfDataGrid.Hour,
                        OldHour = rowOfDataGrid.Hour,
                        CurrentMinute = rowOfDataGrid.Minute,
                        OldMinute = rowOfDataGrid.Minute,
                        CurrentNote = rowOfDataGrid.Note,
                        OldNote = rowOfDataGrid.Note,

                        CurrentUser = rowOfDataGrid.User,
                        OldUser = rowOfDataGrid.User,

                        NumberOfPosition = i++,
                        DateTimeVal = new DateTime(Selected_Date.Year, Selected_Date.Month, Selected_Date.Day,
                            rowOfDataGrid.Hour, rowOfDataGrid.Minute, 0)
                    });
                }

                NumberOfPositionForDataGrid = Positions.Count;
            }
        }


        public IEnumerable<FieldsOfDataGrid> GetNotesForSelectedDay(DateTime date)
        {
            List<FieldsOfDataGrid> retList1 = new List<FieldsOfDataGrid>();

            foreach (var item in m_notesDB.BrokerNotes.Where(item => item.Date.Year == date.Year &&
                                                        item.Date.Month == date.Month &&
                                                        item.Date.Day == date.Day
                                                        ).ToList())
            {
                retList1.Add(new FieldsOfDataGrid()
                {
                    Hour = (short)item.Date.Hour,
                    Minute = (short)item.Date.Minute,
                    Note = item.Message,
                    User = item.User
                });
            }

            var retList2 = retList1.OrderBy(item => item.Hour).ThenBy(item => item.Minute);

            return retList2;
        }


        public ActionResult AddNoteToDB(FieldsOfDataGrid fodg)
        {
            int numberOfNotes = NumberOfNotesFor(Selected_Date, fodg.Hour, fodg.Minute);
            if (numberOfNotes > 0)
            {
                return ActionResult.CreateFailResult(string.Format("There is already Note for Hour Minute {0}:{1}", fodg.Hour, fodg.Minute), ErrorType.DataAlreadyPresent);
            }

            return AddNoteToDB_hlp(Selected_Date, fodg);
        }

        public int NumberOfNotesFor(DateTime date, short hour, short minute)
        {
            int count = m_notesDB.BrokerNotes.Where(item =>
                item.Date.Year == date.Year
                &&
                item.Date.Month == date.Month
                &&
                item.Date.Day == date.Day
                &&
                item.Date.Hour == hour
                &&
                item.Date.Minute == minute
                ).ToList().Count;

            return count;
        }

        private ActionResult AddNoteToDB_hlp(DateTime _dt, FieldsOfDataGrid fodg)
        {
            //n.Note_Id = Guid.NewGuid();//baza sdf
            //m_DBNotatki.Database.ExecuteSqlCommand("delete from NotatkaEncja");

            Note n = new Note()
            {
                Message = fodg.Note,
                User = fodg.User,
                Date = new DateTime(_dt.Year, _dt.Month, _dt.Day, fodg.Hour, fodg.Minute, 0)
            };

            m_notesDB.Add(n);

            try
            {
                m_notesDB.SaveChanges();
            }
            catch (Exception e)
            {
                return ActionResult.CreateFailResult(e.Message,ErrorType.DataSavingFailedWhileAdding);
            }

            return ActionResult.CreateSuccessResult();
        }

        public ActionResult RemoveNoteFromDB(short hour, short minute)
        {
            return RemoveNoteFromDBhlp(Selected_Date, hour, minute);
        }

        private ActionResult RemoveNoteFromDBhlp(DateTime date, short hour, short minute)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            var notesList = m_notesDB.BrokerNotes.Where(item => item.Date.Year == year && item.Date.Month == month && item.Date.Day == day && item.Date.Hour == hour && item.Date.Minute == minute).ToList();

            foreach (var item in notesList)
            {
                m_notesDB.Remove(item);
            }

            try
            {
                m_notesDB.SaveChanges();
            }
            catch (Exception e)
            {
                return ActionResult.CreateFailResult(e.Message, ErrorType.DataSavingFailedWhileRemoving);
            }

            return ActionResult.CreateSuccessResult();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void WyslijPowiadomienie(string NazwaWlasciwosci)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(NazwaWlasciwosci));
            }
        }

        ~CalendarEngine()
        {
            m_Broker.UnregisterFromAll(this);
        }

    }
}
