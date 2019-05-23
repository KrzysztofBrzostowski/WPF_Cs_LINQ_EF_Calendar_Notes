using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Globalization;
using WPF_Calendar_With_Notes.CommonTypes;
using Microsoft.Practices.Unity;
using WPF_Calendar_With_Notes.ViewModel;


namespace WPF_Calendar_With_Notes
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IListener
    {
        private ApplicationViewModel _ViewModel;
        public CalendarEngine Engine { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var container = new UnityContainer();

            //http://stackoverflow.com/questions/6283270/unity-container-registertypetfrom-tto-issues

            //http://stackoverflow.com/questions/2911539/registertype-with-an-interface-in-unitycontainer
            //when using ContainerControlledLifetimeManager then anyway you'll have to use 
            //container.RegisterInstance<IDeviceImporter>(container.Resolve<IImporter>()); 
            //container.RegisterType<IImporter, Importer1>();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
            this.Closed += new EventHandler(MainWindow_Closed);
        }


        private IEventBroker m_Broker;
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var kultura = new System.Globalization.CultureInfo("pl-PL");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = kultura;
            //System.Threading.Thread.CurrentThread.CurrentCulture = kultura;      

            this.Top = 125.0;

            var culture = CultureInfo.InstalledUICulture;
            string lang = culture.DisplayName;

            m_Broker = new EventBroker();
            m_Broker.RegisterFor(EventType.LanguageChanged, this);
            m_Broker.RegisterFor(EventType.SelectedDateChanged, this);

            Engine = new CalendarEngine(m_Broker);

            _ViewModel = new ApplicationViewModel(Engine, m_Broker, dataGrid1);
            this.DataContext = _ViewModel;

            bEditSelected.IsEnabled = false;
            bDeleteSelectedNote.IsEnabled = false;

            string cultureInformation = CultureInfo.CurrentCulture.Name;

            if (!cultureInformation.Equals("pl-PL"))
                _ViewModel.LanguageChangeCommand.Execute("en-GB");
        }

        List<DateTime> m_DtList;
        public void NotifyMe(EventType type, object data)
        {
            if (type == EventType.LanguageChanged)
            {
                var lDGridBindingExpr = lDataGridDescription.GetBindingExpression(Label.ContentProperty);
                if (lDGridBindingExpr != null) lDGridBindingExpr.UpdateTarget();

                var tBlockBindingExpr = DateTextBlock.GetBindingExpression(TextBlock.TextProperty);
                if (tBlockBindingExpr != null) tBlockBindingExpr.UpdateTarget();

                var tBlock1BindingExpr = textBlock1.GetBindingExpression(TextBlock.TextProperty);
                if (tBlock1BindingExpr != null) tBlock1BindingExpr.UpdateTarget();
            }
            else
                if (type == EventType.SelectedDateChanged)
                {
                    m_DtList = ((IData)data).DtList;

                    //foreach (var item in m_DtList)
                    //{
                    //    DateTime dt = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                    //    MainCalendar.SelectedDates.Add(dt);
                    //}

                    //menu1.Focus();
                    //bAddNote.Focus();                
                }
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.Quit,
                Properties.Resources.QuitTitle, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            Engine.m_notesDB.Dispose();

            Loaded -= MainWindow_Loaded;
            Closing -= MainWindow_Closing;
            Closed -= MainWindow_Closed;

            m_Broker.UnregisterFromAll(this);
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //uzyć Binding do zmiany "IsEnabled"
            if (Engine.Positions.Count == 0)
            {
                bEditSelected.IsEnabled = false;
                bDeleteSelectedNote.IsEnabled = false;
            }
            else
            {
                bEditSelected.IsEnabled = true;
                bDeleteSelectedNote.IsEnabled = true;
            }
        }

        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var PosOfDay = e.Row.Item as PositionOfDay;

            if (PosOfDay.CurrentHour != PosOfDay.OldHour ||
                PosOfDay.CurrentMinute != PosOfDay.OldMinute ||
                !PosOfDay.CurrentNote.Equals(PosOfDay.OldNote) ||
                !PosOfDay.CurrentUser.Equals(PosOfDay.OldUser)
                )
            {
                ActionResult saveRes = Engine.RemoveNoteFromDB(PosOfDay.OldHour, PosOfDay.OldMinute);
                if (!saveRes.IsSuccess)
                {
                    MessageBox.Show(saveRes.ErrorMsg, "Error");
                }

                var fodg = new FieldsOfDataGrid()
                {
                    Hour = PosOfDay.CurrentHour,
                    Minute = PosOfDay.CurrentMinute,
                    Note = PosOfDay.CurrentNote,
                    User = PosOfDay.CurrentUser
                };

                ActionResult addNoteResult = Engine.AddNoteToDB(fodg);
                if (!addNoteResult.IsSuccess)
                {
                    MessageBox.Show(addNoteResult.ErrorMsg, "Error");
                }
            }
            Engine.UpdateOfPositions();
        }

        private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var Grid = (DataGrid)sender;
            if (e.Key == Key.Delete)
            {
                foreach (var row in Grid.SelectedItems)
                {
                    PositionOfDay PosOfDay = row as PositionOfDay;
                    if (PosOfDay != null)
                    {
                        ActionResult saveRes = Engine.RemoveNoteFromDB(PosOfDay.CurrentHour, PosOfDay.CurrentMinute);
                        if (!saveRes.IsSuccess)
                        {
                            MessageBox.Show(saveRes.ErrorMsg, "Error");
                        }
                    }
                }

                Engine.UpdateOfPositions();
            }
            else
                if (e.Key == Key.Escape)
                {
                    PositionOfDay PosOfDay = Grid.SelectedItem as PositionOfDay;
                    if (PosOfDay != null)
                    {
                        ActionResult saveRes = Engine.RemoveNoteFromDB(PosOfDay.CurrentHour, PosOfDay.CurrentMinute);
                        if (!saveRes.IsSuccess)
                        {
                            MessageBox.Show(saveRes.ErrorMsg, "Error");
                        }

                        if (!string.IsNullOrWhiteSpace(PosOfDay.OldNote))
                        {
                            var fodg = new FieldsOfDataGrid()
                            {
                                Hour = PosOfDay.OldHour,
                                Minute = PosOfDay.OldMinute,
                                Note = PosOfDay.OldNote,
                                User = PosOfDay.OldUser
                            };

                            ActionResult addNoteResult = Engine.AddNoteToDB(fodg);
                            if (!addNoteResult.IsSuccess)
                            {
                                MessageBox.Show(addNoteResult.ErrorMsg, "Error");
                            }

                        }
                        Engine.UpdateOfPositions();
                    }
                }
        }

        private void bQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //W kontrolce Calendar, po kliknięciu
        //kolekcja dat przestaje byc zaznaczona
        //rozwiazanie: 1. Watek uaktualniajacy lub 2. powiadomienie MainWindow o potrzebie zaznaczenia
        //Żadne z nich nie jest idealne,obecnie wybrałem rozwiązanie 2.
        private void MainCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_DtList != null)
            {
                foreach (var item in m_DtList)
                {
                    DateTime dt = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                    MainCalendar.SelectedDates.Add(dt);
                }
            }

            //Focus w kalendarzu nie pracuje prawidlowo:
            //po opuszczeniu kontrolki Calendar, focus znika
            menu1.Focus();
            bAddNote.Focus();
        }
    }
}
