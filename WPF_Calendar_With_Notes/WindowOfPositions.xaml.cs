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

namespace WPF_Calendar_With_Notes
{
    /// <summary>
    /// Interaction logic for WindowOfPositions.xaml
    /// </summary>
    public partial class WindowOfPositions : Window
    {
        private PositionOfDay _PositionOfDay;        
        private DateTime _Date;
        private CalendarEngine _CalendarEngine;

        public WindowOfPositions()
        {
            InitializeComponent();
        }

        public WindowOfPositions(PositionOfDay position, CalendarEngine engine)
            : this()
        {
            _PositionOfDay = position;
            DataContext = _PositionOfDay;

            _CalendarEngine = engine;
            _Date = _CalendarEngine.Selected_Date;

            tbDate.Text = _Date.ToString().Remove(10) + _Date.ToString(" (dddd)");

            tbUser.Text = _PositionOfDay.CurrentUser;
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_PositionOfDay.CurrentNote))
            {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.NoteEmpty, Properties.Resources.NoteEmptyTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                    if (result == MessageBoxResult.No)
                    {
                        this.DialogResult = false;
                        this.Close();
                    }
                    else
                        return;
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

}
