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
    /// Interaction logic for UpdateTestComboboxAddress.xaml
    /// </summary>
    public partial class UpdateTestComboboxAddress : Window
    {
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        Test orginaltest; //save the orginal test for his testID
        public UpdateTestComboboxAddress(Test test)
        {
            InitializeComponent();
            orginaltest = test;
           
        }


        /// <summary>
        /// show the name of the traine in the text box and reset the date in the date picker by this trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_traineeChoosing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_traineeChoosing.SelectedItem != null)
            {
                string first_name = (cb_traineeChoosing.SelectedItem as Trainee).FirstName;
                string last_name = (cb_traineeChoosing.SelectedItem as Trainee).LastName;
                tb_traineeName.Text = first_name + " " + last_name;
                resetDate();
            }
        }
        #region date
        /// <summary>
        /// reset date to availble date for the selected trainee by his car and gerabox
        /// </summary>
        private void resetDate()
        {
            try
            {
                if (cb_traineeChoosing.SelectedItem != null)
                    Date_DatePicker.IsEnabled = true;
                DatePicker picker = Date_DatePicker;
                picker.SelectedDate = null;
                Trainee trainee = cb_traineeChoosing.SelectedItem as Trainee;
                DateTime start = bl.NearestOpenDateByspecialization(trainee.CarType, trainee.GearBox, null);
                DateTime end = start.AddMonths(3);
                picker.DisplayDateStart = start;
                picker.DisplayDateEnd = end;
                picker.BlackoutDates.Clear();
                //the loop check every date in the 3 month from the first open date if day availble, if not disable them

                while (end >= start)
                {
                    if (start.DayOfWeek == DayOfWeek.Friday || start.DayOfWeek == DayOfWeek.Saturday
                        || bl.testersAvailableAtDateBySpecialization(start, trainee.CarType, trainee.GearBox).Count == 0)
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
            cb_testerChoosing.IsEnabled = false;

        }

        /// <summary>
        /// reset the hour combo box with value for the selected date that mutch the need of the trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Date_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {


            if (e.AddedItems.Count > 0)
            {
                DateTime selctedDate = (DateTime)e.AddedItems[0];
                if (!bl.isAvailableDate(selctedDate))
                {
                    MessageBox.Show("this day unavailable");
                    ((DatePicker)sender).SelectedDate = null;

                }
            }

            if (Date_DatePicker.SelectedDate == null)
            {
                cb_testerChoosing.DataContext = null;
                tb_testerName.Text = "(the tester name)"; //delete the name of the last selected tester fro the text box
                cb_testerChoosing.IsEnabled = false;
                return;
            }
            //if there is a empty slot at this date
            hourComboBox.IsEnabled = true;
            //reset the hour combo box

            hourComboBox.Items.Clear();
            //insert the empty hour at this day for this trainee
            var lis = bl.testersAvailableAtDateBySpecialization((DateTime)Date_DatePicker.SelectedDate, ((Trainee)cb_traineeChoosing.SelectedItem).CarType, ((Trainee)cb_traineeChoosing.SelectedItem).GearBox);
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
        }
        #endregion

        /// <summary>
        /// reset the tester combo box by trainee date and hour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            dateAndHour = new DateTime(Date_DatePicker.SelectedDate.Value.Year,
                                     Date_DatePicker.SelectedDate.Value.Month,
                                     Date_DatePicker.SelectedDate.Value.Day,
                                     int.Parse(((string)(((ComboBoxItem)hourComboBox.SelectedValue).Content)).Substring(0, 2)), 0, 0);
            cb_testerChoosing.IsEnabled = true;
            cb_testerChoosing.DataContext = bl.testersAvailableAtDateAndHourBySpecialization(dateAndHour, cb_traineeChoosing.SelectedItem as Trainee);
            tb_testerName.Text = "(the tester name)";
        }

        /// <summary>
        /// show the name of the tester in the tester text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_testerChoosing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_testerChoosing.SelectedItem != null)
            {
                string first_name = (cb_testerChoosing.SelectedItem as Tester).FirstName;
                string last_name = (cb_testerChoosing.SelectedItem as Tester).LastName;
                tb_testerName.Text = first_name + " " + last_name;
            }
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
                    Address address = new Address(comboBoxStreet.Text, int.Parse(houseNumberTextBox.Text), comboBoxCity.Text);
                    if (!bl.atAvailbleDistance(tester.Id, address))
                        throw new Exception("the tester is too far from the address");
                    orginaltest.TesterId = tester.Id;
                    orginaltest.TraineeId = trainee.Id;
                    orginaltest.Date = DateAndHour;
                    orginaltest.Address = address;
                    orginaltest.GearBox = trainee.GearBox;
                    orginaltest.Car = trainee.CarType;

                    bl.AddTest(new Test(tester.Id, trainee.Id, DateAndHour, address, trainee.GearBox, trainee.CarType));
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

            if (comboBoxStreet.SelectedItem == null)
            {
                msg += "--you need to enter a street\n";
                labelStreet.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelStreet.Foreground = Brushes.Black;
            }

            if (comboBoxCity.SelectedItem == null)
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

        private void comboBoxCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxStreet.IsEnabled = true;
            comboBoxStreet.ItemsSource = Configuration.street[comboBoxCity.SelectedItem as string];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Test test = orginaltest;
            var allTraineeList = (from i in bl.getAllTrainees() //if the trainee end his test, cant be posible to regist him again
                                  where !bl.isPassed(i.Id)
                                  select i).ToList();
            var trainee = allTraineeList.Find(i => i.Id == test.TraineeId);
            cb_traineeChoosing.ItemsSource = allTraineeList;
            cb_traineeChoosing.SelectedItem = trainee;
            houseNumberTextBox.Text = test.Address.houseNumber.ToString();
            comboBoxCity.ItemsSource = Configuration.city;
            comboBoxCity.SelectedItem = Configuration.city.Find(i => i == test.Address.city);
            comboBoxStreet.ItemsSource = Configuration.street[test.Address.city];
            comboBoxStreet.SelectedItem = Configuration.street[test.Address.city].Find(I => I == test.Address.street);
            Date_DatePicker.SelectedDate = test.Date;
            hourComboBox.Items.Clear();
            var lis = bl.testersAvailableAtDateBySpecialization((DateTime)Date_DatePicker.SelectedDate, ((Trainee)cb_traineeChoosing.SelectedItem).CarType, ((Trainee)cb_traineeChoosing.SelectedItem).GearBox);
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
            var allTesterAvailable = bl.testersAvailableAtDateAndHourBySpecialization(test.Date, cb_traineeChoosing.SelectedItem as Trainee);
            var tester = bl.getAllTester().Find(i => i.Id == test.TesterId);
            allTesterAvailable.Add(tester);
            cb_testerChoosing.DataContext = allTesterAvailable;
            cb_testerChoosing.SelectedItem = tester;
            cb_testerChoosing.IsEnabled = true;

        }
    }

}

