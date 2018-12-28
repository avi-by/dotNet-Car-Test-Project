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
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddTesterWindow.xaml
    /// </summary>
    public partial class AddTesterWindow : Window
    {
       
        public AddTesterWindow()
        {


            InitializeComponent();
            
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender)); 
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));
            
            birthdayDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BL.MyBL.Instance.getMaximumAge());
            birthdayDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BL.MyBL.Instance.getMinimumAgeOfTester());
        }



        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerDataGrid = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerDataGrid")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        private void pbAddTester_Click(object sender, RoutedEventArgs e)
        {

            // MyBL.Instance.addTester(new Tester(firstNameTextBox.Text, birthdayDatePicker.DisplayDate, lastNameTextBox.Text ));

            #region temp workHour from checkboxes
            //create_temporaty_workHour
            bool[][] temp_workHour = new bool[5][];
            for (int i = 0; i < temp_workHour.Length; i++)
            {
                temp_workHour[i] = new bool[6];
            }
           
            temp_workHour[0][0] = (bool)matrix.cb_sun_9.IsChecked;
            temp_workHour[0][1] = (bool)matrix.cb_sun_10.IsChecked;
            temp_workHour[0][2] = (bool)matrix.cb_sun_11.IsChecked;
            temp_workHour[0][3] = (bool)matrix.cb_sun_12.IsChecked;
            temp_workHour[0][4] = (bool)matrix.cb_sun_13.IsChecked;
            temp_workHour[0][5] = (bool)matrix.cb_sun_14.IsChecked;

            temp_workHour[1][0] = (bool)matrix.cb_mon_9.IsChecked;
            temp_workHour[1][1] = (bool)matrix.cb_mon_10.IsChecked;
            temp_workHour[1][2] = (bool)matrix.cb_mon_11.IsChecked;
            temp_workHour[1][3] = (bool)matrix.cb_mon_12.IsChecked;
            temp_workHour[1][4] = (bool)matrix.cb_mon_13.IsChecked;
            temp_workHour[1][5] = (bool)matrix.cb_mon_14.IsChecked;

            temp_workHour[2][0] = (bool)matrix.cb_tue_9.IsChecked;
            temp_workHour[2][1] = (bool)matrix.cb_tue_10.IsChecked;
            temp_workHour[2][2] = (bool)matrix.cb_tue_11.IsChecked;
            temp_workHour[2][3] = (bool)matrix.cb_tue_12.IsChecked;
            temp_workHour[2][4] = (bool)matrix.cb_tue_13.IsChecked;
            temp_workHour[2][5] = (bool)matrix.cb_tue_14.IsChecked;

            temp_workHour[3][0] = (bool)matrix.cb_wed_9.IsChecked;
            temp_workHour[3][1] = (bool)matrix.cb_wed_10.IsChecked;
            temp_workHour[3][2] = (bool)matrix.cb_wed_11.IsChecked;
            temp_workHour[3][3] = (bool)matrix.cb_wed_12.IsChecked;
            temp_workHour[3][4] = (bool)matrix.cb_wed_13.IsChecked;
            temp_workHour[3][5] = (bool)matrix.cb_wed_14.IsChecked;

            temp_workHour[4][0] = (bool)matrix.cb_thu_9.IsChecked;
            temp_workHour[4][1] = (bool)matrix.cb_thu_10.IsChecked;
            temp_workHour[4][2] = (bool)matrix.cb_thu_11.IsChecked;
            temp_workHour[4][3] = (bool)matrix.cb_thu_12.IsChecked;
            temp_workHour[4][4] = (bool)matrix.cb_thu_13.IsChecked;
            temp_workHour[4][5] = (bool)matrix.cb_thu_14.IsChecked;

            #endregion
            if (AllFieldOK(temp_workHour))
            {
                try
                {
                    MyBL.Instance.addTester(new Tester(ID.Text, firstNameTextBox.Text, lastNameTextBox.Text, birthdayDatePicker.DisplayDate, new Address(" ", 0, " sd"), (BE.Gender)genderComboBox.SelectedValue, "8", 1, 1, (BE.CarType)1, (BE.GearBox)1, temp_workHour, 0));
                }
                catch (Exception msg)
                {

                    MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Close();
            }


           
        }
        private bool AllFieldOK(bool[][] temp_workHour)
        {
            string msg = "problems:\n";
            long temp = 0;
            double temp2;
            bool flag = false, workHourFlag = false;

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

            if (streetTextBox.Text=="")
            {
                msg += "--need street\n";
                labelStreet.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelStreet.Foreground = Brushes.Black;
            }

            if (city.Text == "")
            {
                msg += "--need city\n";
                labelCity.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelCity.Foreground = Brushes.Black;
            }

            if (birthdayDatePicker.SelectedDate.HasValue == false)
            {
                msg += "--enter birthday\n";
                labelBirthDay.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                 labelBirthDay.Foreground = Brushes.Black;
            }

            if (genderComboBox.SelectedItem==null)
            {
                msg += "--enter gender\n";
                labelGender.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelGender.Foreground = Brushes.Black;
            }

            if (gearBoxComboBox.SelectedItem==null)
            {
                msg += "--enter gearBox\n";
                labelGearBox.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelGearBox.Foreground = Brushes.Black;
            }

            if (carTypeComboBox.SelectedItem == null)
            {
                msg += "--enter car type\n";
                labelCarType.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelCarType.Foreground = Brushes.Black;
            }

            if (firstNameTextBox.Text == "")
            {
                msg += "--need first name\n";
                labelFirstName.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelFirstName.Foreground = Brushes.Black;
            }

            if (lastNameTextBox.Text == "")
            {
                msg += "--need last name\n";
                labelLastName.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelLastName.Foreground = Brushes.Black;
            }

            if (maxTestInWeekTextBox.Text == "" || !long.TryParse(maxTestInWeekTextBox.Text, out temp) || temp < 1)//need only digits and must work atleast one time at week
            {
                msg += "--need work hour, digits only\n";
                labelMaxTestInWeek.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelMaxTestInWeek.Foreground = Brushes.Black;
            }


            if (expYearsTextBox.Text == "" || !long.TryParse(expYearsTextBox.Text, out temp))
            {
                msg += "--need exp years,digits only\n";
                labelExpYears.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelExpYears.Foreground = Brushes.Black;
            }

            if (phoneNumberTextBox.Text == "" || !long.TryParse(phoneNumberTextBox.Text, out temp) || phoneNumberTextBox.Text.Length < 7)
            {
                msg += "--need phone number, digits only, at least 7 digits\n";
                labelPhoneNumber.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelPhoneNumber.Foreground = Brushes.Black;
            }


            for (int i = 0; i < temp_workHour.GetLength(0); i++)
            {
                for (int j = 0; j < temp_workHour[i].Length; j++)
                {
                    if (temp_workHour[i][j])
                        workHourFlag = true;
                }
            }

            if (!workHourFlag)
            {
                msg += "--need work hour, at least one\n";
                labelWorkHours.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelWorkHours.Foreground = Brushes.Black;
            }

            if (distanceTexBox.Text == "" || !double.TryParse(distanceTexBox.Text, out temp2))
            {
                msg += "--need distance, digits only, cant be negative value \n";
                labelDistance.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelDistance.Foreground = Brushes.Black;
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


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}