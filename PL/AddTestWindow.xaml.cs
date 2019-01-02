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

            cb_traineeChoosing.DataContext = bl.getAllTrainees();
            cb_testerChoosing.IsEnabled = false;
         //   cb_testerChoosing.DataContext = bl.getAllTester();    //if you change it after the selection of the date, you dont need it now


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
                //set hour that selected
                DateTime DateAndHour;
                DateAndHour = new DateTime(Date_DatePicker.DisplayDate.Year,
                                     Date_DatePicker.DisplayDate.Month,
                                     Date_DatePicker.DisplayDate.Day,
                                     hourComboBox.SelectedIndex + 9, 0, 0);


                try
                {
                    Trainee trainee = cb_traineeChoosing.SelectedItem as Trainee;
                    Tester tester = cb_testerChoosing.SelectedItem as Tester;
                    bl.AddTest(new Test(tester.Id, trainee.Id, DateAndHour, new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text),trainee.GearBox, trainee.CarType ));
                    Close();
                }
                catch (Exception msg)
                {

                    MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                


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

            if (cb_traineeChoosing.SelectedItem == null )
            {
                msg += "--you need to chose a trainee\n";
                labelTraineeCho.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelTraineeCho.Foreground = Brushes.Black;
            }

            if (Date_DatePicker.SelectedDate== null)
            {
                msg += "--you need to chose a date\n";
                labelDate.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelDate.Foreground = Brushes.Black;
            }

            if (hourComboBox.SelectedItem == null)
            {
                msg += "--you need to chose a hour\n";
                hour.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                hour.Foreground = Brushes.Black;
            }

            if (cb_testerChoosing.SelectedItem == null)
            {
                msg += "--you need to chose a tester\n";
                labelTesterChoosing.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelTesterChoosing.Foreground = Brushes.Black;
            }

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

            if (streetTextBox.Text== "")
            {
                msg += "--you need to enter a street\n";
                labelStreet.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelStreet.Foreground = Brushes.Black;
            }

            if (city.Text == "")
            {
                msg += "--you need to enter a city\n";
                labelCity.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelCity.Foreground = Brushes.Black;
            }

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
            var x = from item in bl.allTheTestAtRange(start, end) select item.Date;
            //the loop check every date in the 3 month from the first open date if day availble, if not disable them
            while (end >= start)
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
            

            if (e.AddedItems.Count > 0)
            {
                DateTime selctedDate = (DateTime)e.AddedItems[0];
                if (!bl.isAvailableDate(selctedDate))
                {
                    MessageBox.Show("this day unavailbale");
                    ((DatePicker)sender).SelectedDate = null;
                    return;
                }
            }

            if (hourComboBox.SelectedItem == null || Date_DatePicker.SelectedDate == null)
            {
                cb_testerChoosing.DataContext = null;
                tb_testerName.Text = "(the tester name)";
                cb_testerChoosing.IsEnabled = false;
                return;
            }

            DateTime dateAndHour;
            dateAndHour = new DateTime(Date_DatePicker.DisplayDate.Year,
                                     Date_DatePicker.DisplayDate.Month,
                                     Date_DatePicker.DisplayDate.Day,
                                     hourComboBox.SelectedIndex + 9, 0, 0);
            cb_testerChoosing.IsEnabled = true;
            cb_testerChoosing.DataContext = bl.testersAvailableAtDateAndHourBySpecialization(dateAndHour,cb_traineeChoosing.SelectedItem as Trainee);





        }


        private void Cb_traineeChoosing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string first_name = (cb_traineeChoosing.SelectedItem as Trainee).FirstName;
            string last_name = (cb_traineeChoosing.SelectedItem as Trainee).LastName;
            tb_traineeName.Text = first_name + " " + last_name;
            DatePicker picker = Date_DatePicker;
            DateTime start = bl.NearestOpenDateByspecialization((cb_traineeChoosing.SelectedItem as Trainee).CarType, (cb_traineeChoosing.SelectedItem as Trainee).GearBox,null);
            DateTime end = start.AddMonths(3);
            picker.DisplayDateStart = start;
            picker.DisplayDateEnd = end;
            picker.BlackoutDates.Clear();
            var x = from item in bl.allTheTestAtRange(start, end) select item.Date;
            //the loop check every date in the 3 month from the first open date if day availble, if not disable them
            while (end >= start)
            {
                if (x.Contains(start))
                {
                    picker.BlackoutDates.Add(new CalendarDateRange(start));
                }
                start = start.AddDays(1);
            }
        }

        private void Cb_testerChoosing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_testerChoosing.SelectedItem != null)
            {
                string first_name = (cb_testerChoosing.SelectedItem as Tester).FirstName;
                string last_name = (cb_testerChoosing.SelectedItem as Tester).LastName;
                tb_testerName.Text = first_name + " " + last_name;
            }
        }

        private void HourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (hourComboBox.SelectedItem == null || Date_DatePicker.SelectedDate == null)
            {
                cb_testerChoosing.DataContext = null;
                tb_testerName.Text = "(the tester name)";
                cb_testerChoosing.IsEnabled = false;
                return;
            }

            DateTime dateAndHour;
            dateAndHour = new DateTime(Date_DatePicker.DisplayDate.Year,
                                     Date_DatePicker.DisplayDate.Month,
                                     Date_DatePicker.DisplayDate.Day,
                                     hourComboBox.SelectedIndex + 9, 0, 0);
            cb_testerChoosing.IsEnabled = true;
            cb_testerChoosing.DataContext = bl.testersAvailableAtDateAndHourBySpecialization(dateAndHour, cb_traineeChoosing.SelectedItem as Trainee);
            tb_testerName.Text = "(the tester name)";

        }

        //private void TesterIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //set tester name according to the id that entered.
        //    var the_tester= bl.getAllTester().Find(item => item.Id == TesterIdTextBox.Text);
        //    if (the_tester != null)
        //    {
        //        string names = the_tester.FirstName +" "+ the_tester.LastName;
        //        tb_testerName.Text = names;
        //    }
        //    else
        //        tb_testerName.Text = "(name of tester)";


        //    hourComboBox.SelectedItem = null;

        //    //set each combox item enabled only if the tester work in this hour  
        //    if (bl.isAvailableAtDate(TesterIdTextBox.Text, Date_DatePicker.DisplayDate))
        //    {


        //        DayOfWeek day = Date_DatePicker.DisplayDate.DayOfWeek;
        //        Tester temp = bl.getAllTester().Find(tester => tester.Id == TesterIdTextBox.Text);
        //        for (int i = 0; i < 6; i++)
        //            (hourComboBox.Items[i] as ComboBoxItem).IsEnabled = (temp.WorkHour[(int)day][i]);
        //    }
        //    else
        //    {

        //        for (int i = 0; i < 6; i++)
        //            (hourComboBox.Items[i] as ComboBoxItem).IsEnabled = false;

        //    }
        //}

        //private void Tb_traineeName_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //set trainee name according to the id that entered.
        //    var the_trainee = bl.getAllTrainees().Find(item => item.Id == TraineeIdTextBox.Text);
        //    if (the_trainee != null)
        //    {
        //        string names = the_trainee.FirstName + " " + the_trainee.LastName;
        //        tb_traineeName.Text = names;
        //    }
        //    else
        //        tb_traineeName.Text = "(name of trainee)";
        //}

        //private void Pb_chooseTrainee_Click(object sender, RoutedEventArgs e)
        //{
        //    searchAndChooseTrainee_Window searchAndChooseTrainee = new searchAndChooseTrainee_Window(this);
        //    searchAndChooseTrainee.Show();

        //}

        //private void Pb_chooseTester_Click(object sender, RoutedEventArgs e)
        //{
        //    searchAndChooseTester_Window searchAndChooseTester = new searchAndChooseTester_Window(this);
        //    searchAndChooseTester.Show();

        //}
    }
}


    