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
    /// Interaction logic for UpfdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        Test orginaltest;
        public UpdateTestWindow()
        {
            InitializeComponent();
            cb_traineeChoosing.DataContext = (from i in bl.getAllTrainees() //if the trainee end his test, cant be posible to regist him again
                                              where !bl.isPassed(i.Id)
                                              select i).ToList();
            Date_DatePicker.IsEnabled = false;
            cb_testerChoosing.IsEnabled = false;

        }

        public UpdateTestWindow(Test test)
        {
            InitializeComponent();
            DataContext = test;
            orginaltest = test;
            var allTraineeList = (from i in bl.getAllTrainees() //if the trainee end his test, cant be posible to regist him again
                                  where !bl.isPassed(i.Id)
                                  select i).ToList();
            var trainee = allTraineeList.Find(i => i.Id == test.TraineeId);
            cb_traineeChoosing.ItemsSource = allTraineeList;
            cb_traineeChoosing.SelectedItem = trainee;
            houseNumberTextBox.Text = test.Address.houseNumber.ToString();
            streetTextBox.Text = test.Address.street;
            city.Text = test.Address.city;
            Date_DatePicker.IsEnabled = true;

            Date_DatePicker.SelectedDate = test.Date;
            hourComboBox.Items.Clear();
            var lis = bl.testersAvailableAtDateBySpecializationAndAddress((DateTime)Date_DatePicker.SelectedDate, ((Trainee)cb_traineeChoosing.SelectedItem).CarType, ((Trainee)cb_traineeChoosing.SelectedItem).GearBox, test.Address);
            for (int i = 0; i < 6; i++)
            {
                if (lis.Find(item => item.WorkHour[(int)((DateTime)Date_DatePicker.SelectedDate).DayOfWeek][i]) != null)
                {
                    switch (i)
                    {
                        case 0:
                            ComboBoxItem hour_9 = new ComboBoxItem();
                            hour_9.Content = "09:00";
                            hourComboBox.Items.Add(hour_9);
                            if (test.Date.Hour == 9)
                                hourComboBox.SelectedItem = hour_9;
                            break;
                        case 1:

                            ComboBoxItem hour_10 = new ComboBoxItem();
                            hour_10.Content = "10:00";
                            hourComboBox.Items.Add(hour_10);
                            if (test.Date.Hour == 10)
                                hourComboBox.SelectedItem = hour_10;
                            break;
                        case 2:
                            ComboBoxItem hour_11 = new ComboBoxItem();
                            hour_11.Content = "11:00";
                            hourComboBox.Items.Add(hour_11);
                            if (test.Date.Hour == 11)
                                hourComboBox.SelectedItem = hour_11;
                            break;
                        case 3:
                            ComboBoxItem hour_12 = new ComboBoxItem();
                            hour_12.Content = "12:00";
                            hourComboBox.Items.Add(hour_12);
                            if (test.Date.Hour == 12)
                                hourComboBox.SelectedItem = hour_12;
                            break;
                        case 4:
                            ComboBoxItem hour_13 = new ComboBoxItem();
                            hour_13.Content = "13:00";
                            hourComboBox.Items.Add(hour_13);
                            if (test.Date.Hour == 13)
                                hourComboBox.SelectedItem = hour_13;
                            break;
                        case 5:
                            ComboBoxItem hour_14 = new ComboBoxItem();
                            hour_14.Content = "14:00";
                            hourComboBox.Items.Add(hour_14);
                            if (test.Date.Hour == 14)
                                hourComboBox.SelectedItem = hour_14;
                            break;
                        default:
                            break;
                    }
                }
            }
            var allTesterAvailable = bl.testersAvailableAtDateAndHourBySpecializationAndAddress(test.Date, cb_traineeChoosing.SelectedItem as Trainee, test.Address);
            var tester = bl.getAllTester().Find(i => i.Id == test.TesterId);
            allTesterAvailable.Add(tester);
            cb_testerChoosing.DataContext = allTesterAvailable;
            cb_testerChoosing.SelectedItem = tester;
            originalHouseNum = test.Address.houseNumber.ToString();
            originalStreet = test.Address.street;
            originalCity = test.Address.city;
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
            if (Date_DatePicker == null && hourComboBox == null) return;

            if (AllFieldOK())
            {
                //set hour that selected
                DateTime DateAndHour;
                try
                {
                    DateAndHour = new DateTime(Date_DatePicker.SelectedDate.Value.Year,
                                       Date_DatePicker.SelectedDate.Value.Month,
                                       Date_DatePicker.SelectedDate.Value.Day,
                                       int.Parse(((ComboBoxItem)hourComboBox.SelectedValue).Content.ToString().Substring(0, 2)), 0, 0);



              
                    Trainee trainee = cb_traineeChoosing.SelectedItem as Trainee;
                    Tester tester = cb_testerChoosing.SelectedItem as Tester;
                    orginaltest.TesterId = tester.Id;
                    orginaltest.TraineeId = trainee.Id;
                    orginaltest.Date = DateAndHour;
                    orginaltest.Address = new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text);
                    orginaltest.GearBox = trainee.GearBox;
                    orginaltest.Car = trainee.CarType;

                    bl.updateTest(orginaltest);
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


            if (cb_traineeChoosing.SelectedItem == null)
            {
                msg += "--you need to chose a trainee\n";
                labelTraineeCho.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelTraineeCho.Foreground = Brushes.Black;
            }

            if (Date_DatePicker.SelectedDate == null)
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

            if (streetTextBox.Text == "")
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
            try
            {
                
                DatePicker picker = Date_DatePicker;
                Address address = new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text);
                Trainee trainee = cb_traineeChoosing.SelectedItem as Trainee;
                DateTime start = bl.NearestOpenDateBySpecializationAndAddress(trainee.CarType, trainee.GearBox, null, address);
                DateTime end = start.AddMonths(3);
                picker.DisplayDateStart = start;
                picker.DisplayDateEnd = end;
                picker.BlackoutDates.Clear();
                //the loop check every date in the 3 month from the first open date if day availble, if not disable them

                while (end >= start)
                {
                    if (Date_DatePicker.SelectedDate!=start&& (start.DayOfWeek == DayOfWeek.Friday || start.DayOfWeek == DayOfWeek.Saturday
                        || bl.testersAvailableAtDateBySpecializationAndAddress(start, trainee.CarType, trainee.GearBox, address).Count == 0))
                    {
                        picker.BlackoutDates.Add(new CalendarDateRange(start));
                    }
                    start = start.AddDays(1);
                }
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message);
//                Date_DatePicker.IsEnabled = false;
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

                }
            }

            if (Date_DatePicker.SelectedDate == null)
            {
                cb_testerChoosing.DataContext = null;
                tb_testerName.Text = "(the tester name)";
                cb_testerChoosing.IsEnabled = false;
                return;
            }
            //if there is a empty slot at this date
            hourComboBox.IsEnabled = true;
            //reset the hour combo box

            hourComboBox.Items.Clear();
            int houseNum;
            if (int.TryParse(houseNumberTextBox.Text, out houseNum) == false)
            {
                MessageBox.Show("the house number need to contain only digits!");
                return;
            }
            Address address = new Address(streetTextBox.Text, houseNum, city.Text);
            //insert the empty hour at this day for this trainee
            var lis = bl.testersAvailableAtDateBySpecializationAndAddress((DateTime)Date_DatePicker.SelectedDate, ((Trainee)cb_traineeChoosing.SelectedItem).CarType, ((Trainee)cb_traineeChoosing.SelectedItem).GearBox, address);
            for (int i = 0; i < 6; i++)
            {
                if (lis.Find(item => item.WorkHour[(int)((DateTime)Date_DatePicker.SelectedDate).DayOfWeek][i]) != null)
                {
                    switch (i)
                    {
                        case 0:
                            ComboBoxItem hour_9 = new ComboBoxItem();
                            hour_9.Content = "09:00";
                            hourComboBox.Items.Add(hour_9);
                            break;
                        case 1:

                            ComboBoxItem hour_10 = new ComboBoxItem();
                            hour_10.Content = "10:00";
                            hourComboBox.Items.Add(hour_10);
                            break;
                        case 2:
                            ComboBoxItem hour_11 = new ComboBoxItem();
                            hour_11.Content = "11:00";
                            hourComboBox.Items.Add(hour_11);
                            break;
                        case 3:
                            ComboBoxItem hour_12 = new ComboBoxItem();
                            hour_12.Content = "12:00";
                            hourComboBox.Items.Add(hour_12);
                            break;
                        case 4:
                            ComboBoxItem hour_13 = new ComboBoxItem();
                            hour_13.Content = "13:00";
                            hourComboBox.Items.Add(hour_13);
                            break;
                        case 5:
                            ComboBoxItem hour_14 = new ComboBoxItem();
                            hour_14.Content = "14:00";
                            hourComboBox.Items.Add(hour_14);
                            break;
                        default:
                            break;
                    }
                }
            }
            //DateTime dateAndHour;
            //dateAndHour = new DateTime(Date_DatePicker.SelectedDate.Value.Year,
            //                         Date_DatePicker.SelectedDate.Value.Month,
            //                         Date_DatePicker.SelectedDate.Value.Day,
            //                         hourComboBox.SelectedIndex + 9, 0, 0);
            //cb_testerChoosing.IsEnabled = true;
            //cb_testerChoosing.DataContext = bl.testersAvailableAtDateAndHourBySpecializationAndAddress(dateAndHour,cb_traineeChoosing.SelectedItem as Trainee,address);





        }


        private void Cb_traineeChoosing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string first_name = (cb_traineeChoosing.SelectedItem as Trainee).FirstName;
            string last_name = (cb_traineeChoosing.SelectedItem as Trainee).LastName;
            tb_traineeName.Text = first_name + " " + last_name;
            resetDate();
        }

        private void resetDate()
        {
            try
            {

                if (cb_traineeChoosing.SelectedItem != null && int.TryParse(houseNumberTextBox.Text, out int temp) != false && streetTextBox.Text != "" && city.Text != null)
                    Date_DatePicker.IsEnabled = true;
                else
                {
                    Date_DatePicker.IsEnabled = false;
                    hourComboBox.IsEnabled = false;
                    cb_testerChoosing.IsEnabled = false;
                    return;
                }
                DatePicker picker = Date_DatePicker;
                picker.SelectedDate = null;
                Address address = new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text);
                Trainee trainee = cb_traineeChoosing.SelectedItem as Trainee;
                DateTime start = bl.NearestOpenDateBySpecializationAndAddress(trainee.CarType, trainee.GearBox, null, address);
                DateTime end = start.AddMonths(3);
                picker.DisplayDateStart = start;
                picker.DisplayDateEnd = end;
                picker.BlackoutDates.Clear();
                //the loop check every date in the 3 month from the first open date if day availble, if not disable them

                while (end >= start)
                {
                    if (start.DayOfWeek == DayOfWeek.Friday || start.DayOfWeek == DayOfWeek.Saturday
                        || bl.testersAvailableAtDateBySpecializationAndAddress(start, trainee.CarType, trainee.GearBox, address).Count == 0)
                    {
                        picker.BlackoutDates.Add(new CalendarDateRange(start));
                    }
                    start = start.AddDays(1);
                }
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message);
                Date_DatePicker.IsEnabled = false;
            }
            hourComboBox.Items.Clear();

            hourComboBox.IsEnabled = false;
            //   cb_testerChoosing.ItemsSource=null;
            cb_testerChoosing.IsEnabled = false;
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
            int houseNum;
            if (int.TryParse(houseNumberTextBox.Text, out houseNum) == false)
            {
                MessageBox.Show("the house number need to contain only digits!");
                return;
            }
            DateTime dateAndHour;
            dateAndHour = new DateTime(Date_DatePicker.SelectedDate.Value.Year,
                                     Date_DatePicker.SelectedDate.Value.Month,
                                     Date_DatePicker.SelectedDate.Value.Day,
                                     int.Parse(((string)(((ComboBoxItem)hourComboBox.SelectedValue).Content)).Substring(0, 2)), 0, 0);
            cb_testerChoosing.IsEnabled = true;
            cb_testerChoosing.DataContext = bl.testersAvailableAtDateAndHourBySpecializationAndAddress(dateAndHour, cb_traineeChoosing.SelectedItem as Trainee, new Address(streetTextBox.Text, houseNum, city.Text));
            tb_testerName.Text = "(the tester name)";

        }

        private void houseNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (houseNumberTextBox.Text == "")
                return;
            if (houseNumberTextBox.Text != "" && int.TryParse(houseNumberTextBox.Text, out int temp) == false)
            {
                MessageBox.Show("the house number need to contain only digits!");
                Date_DatePicker.IsEnabled = false;
                hourComboBox.IsEnabled = false;
                cb_testerChoosing.IsEnabled = false;
                return;
            }
            if (houseNumberTextBox.Text != originalHouseNum && Date_DatePicker.IsEnabled == true)
            {
                originalHouseNum = houseNumberTextBox.Text;
                resetDate();
            }
            if (houseNumberTextBox.Text != "" && streetTextBox.Text != "" && city.Text != "" && cb_traineeChoosing.SelectedItem != null)
                resetDate();
            originalHouseNum = houseNumberTextBox.Text;

        }
        private string originalStreet = "";
        private void streetTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (houseNumberTextBox.Text != "" && int.TryParse(houseNumberTextBox.Text, out int temp) == false)
            //{
            //    MessageBox.Show("the house number need to contain only digits!");
            //    Date_DatePicker.IsEnabled = false;
            //    hourComboBox.IsEnabled = false;
            //    cb_testerChoosing.IsEnabled = false;
            //    return;
            //}
            if (originalStreet != streetTextBox.Text && Date_DatePicker.IsEnabled == true)
            {
                originalStreet = streetTextBox.Text;
                resetDate();
                return;
            }
            if (houseNumberTextBox.Text != "" && streetTextBox.Text != "" && city.Text != "" && cb_traineeChoosing.SelectedItem != null)
                resetDate();
            originalStreet = streetTextBox.Text;

        }
        private string originalCity = "";
        private void city_LostFocus(object sender, RoutedEventArgs e)
        {
            //    if (houseNumberTextBox.Text != "" && int.TryParse(houseNumberTextBox.Text, out int temp) == false)
            //    {
            //        MessageBox.Show("the house number need to contain only digits!");
            //        Date_DatePicker.IsEnabled = false;
            //        hourComboBox.IsEnabled = false;
            //        cb_testerChoosing.IsEnabled = false;
            //        return;
            //    }
            if (originalCity != city.Text && Date_DatePicker.IsEnabled == true) //if the address change
            {
                originalCity = city.Text;
                resetDate();
                return;
            }
            if (houseNumberTextBox.Text != "" && streetTextBox.Text != "" && city.Text != "" && cb_traineeChoosing.SelectedItem != null)//the first time the address insert
                resetDate();
            originalCity = city.Text;
        }
        private string originalHouseNum = "";
        private void houseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (houseNumberTextBox.Text != "" && int.TryParse(houseNumberTextBox.Text, out int temp) == false)
            {
                houseNumberTextBox.Text = originalHouseNum;
            }
            else
                originalHouseNum = houseNumberTextBox.Text;
        }

    }
}
