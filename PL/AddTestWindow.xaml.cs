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
using BE;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        public AddTestWindow()
        {
            InitializeComponent();
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));

            ComboBoxItem hour_9 = new ComboBoxItem();
            hour_9.Content = "9:00";
            hourComboBox.Items.Add(hour_9);

            ComboBoxItem hour_10 = new ComboBoxItem();
            hour_10.Content = "10:00";
            hourComboBox.Items.Add(hour_10);

            ComboBoxItem hour_11 = new ComboBoxItem();
            hour_11.Content = "11:00";
            hourComboBox.Items.Add(hour_11);

            ComboBoxItem hour_12 = new ComboBoxItem();
            hour_12.Content = "12:00";
            hourComboBox.Items.Add(hour_12);

            ComboBoxItem hour_13 = new ComboBoxItem();
            hour_13.Content = "13:00";
            hourComboBox.Items.Add(hour_13);

            ComboBoxItem hour_14 = new ComboBoxItem();
            hour_14.Content = "14:00";
            hourComboBox.Items.Add(hour_14);

            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void Add_Test_Click(object sender, RoutedEventArgs e)
        {

            if (AllFieldOK())
            {

                MyBL.Instance.AddTest(new Test(TesterIdTextBox.Text, TraineeIdTextBox.Text,  (GearBox)gearBoxComboBox.SelectedItem, (CarType)carTypeComboBox.SelectedItem, Date_DatePicker.DisplayDate, new DateTime(), new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text), test1_ReverseParkingCheckBox.IsChecked,test2_KeepingSafeDistanceCheckBox.IsChecked,test3_UsingMirrorsCheckBox.IsChecked,test4_UsingTurnSignalsCheckBox.IsChecked,test5_LegalSpeedCheckBox.IsChecked, succeededCheckBox.IsChecked, notesTextBox.Text));


                this.Close();


            }
        }

        private bool AllFieldOK()
        {
            string msg = "problems:\n";
            long temp = 0;
            bool flag = false;

            //if (ID.Text.Length < 8 || !long.TryParse(ID.Text, out temp)) //the id need at least 8 digits and only digits so it can be convert to int
            //{
            //    msg += "--the id need at least 8 digits and only digits\n";
            //    labelID.Foreground = Brushes.Red;
            //    flag = true;
            //}
            //else
            //{
            //    labelID.Foreground = Brushes.Black;
            //}

            if (houseNumberTextBox.Text == "" || !long.TryParse(houseNumberTextBox.Text, out temp) || temp < 1)
            {
                msg += "--house number could contain digits only\n";
                labelhouseNumber.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelhouseNumber.Foreground = Brushes.Black;
            }

            


          
            //if (addressTexBox.Text == "" || !int.TryParse(address[2], out int temp3)||temp3<1) //address [2] is the house number and he need to be more then 0
            //{
            //    msg += "--need address, city street house number separated by a comma, house number must be a digit and bigger then 0 \n";
            //    labelAddress.Foreground = Brushes.Red;
            //    flag = true;
            //}
            //else
            //{
            //    labelAddress.Foreground = Brushes.Black;
            //}

            if (flag)
            {
                MessageBox.Show(msg, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

                return false;
            }
            return true;
        }

        private void Date_DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;
            DateTime start = bl.NearestOpenDate();
            DateTime end = start.AddMonths(3);
            picker.DisplayDateStart = start;
            picker.DisplayDateEnd = end;
            picker.BlackoutDates.Clear();
            var x = from item in bl.allTheTestAtRange(start,end) select item.Date;
            //the loop check every date in the 3 month from the first open date if day availble, if not disable them
            while (end>=start)
            {
                if (x.Contains(start))
                {
                    picker.BlackoutDates.Add(new CalendarDateRange(start));
                }
                start = start.AddDays(1);
            }
        }

        private void Date_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count>0)
            {
                DateTime selctedDate = (DateTime)e.AddedItems[0];
                if(!bl.isAvailableDate(selctedDate))
                {
                    MessageBox.Show("this day unavailbale");
                    ((DatePicker)sender).SelectedDate = null;
                }
            }
        }

        private void TesterIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Tester temp;
            if (MyBL.Instance.isAvailableAtDate(TesterIdTextBox.Text, Date_DatePicker.DisplayDate))
            {
                int i = 0;
                for (i = 0; i < 5; i++)
                {
                   // MyBL.get
                }

            }
        }
    }
}


    