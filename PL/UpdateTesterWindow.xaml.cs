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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UpdateTesterWindow : Window
    {
        
        public UpdateTesterWindow()
        {
            InitializeComponent();

            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));

            birthdayDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BL.MyBL.Instance.getMaximumAge());
            birthdayDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BL.MyBL.Instance.getMinimumAgeOfTester());
            
        }

        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
           
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
                //Tester temp= (new Tester("111", firstNameTextBox.Text, lastNameTextBox.Text, birthdayDatePicker.DisplayDate, new Address(" ", 0, " sd"), (BE.Gender)genderComboBox.SelectedValue, "8", 1, 1, (BE.CarType)1, (BE.GearBox)1, temp_workHour, 0))
                MyBL.Instance.updateTester(new Tester(ID.Text, firstNameTextBox.Text, lastNameTextBox.Text, birthdayDatePicker.DisplayDate, new Address(" ", 0, " sd"), (BE.Gender)genderComboBox.SelectedValue, phoneNumberTextBox.Text,int.Parse( expYearsTextBox.Text),int.Parse( maxTestInWeekTextBox.Text), (BE.CarType)carTypeComboBox.SelectedValue, (BE.GearBox)gearBoxComboBox.SelectedValue, temp_workHour, 0));
                this.Close();
            }
        }

        private bool AllFieldOK(bool [][] temp_workHour)
        {
            string msg = "problems:\n";
            int temp=0;
            bool flag = false,workHourFlag=false;

            //if (ID.Text.Length < 8 || !int.TryParse(ID.Text, out temp)) //the id need at least 8 digits and only digits so it can be convert to int
            //{
            //    msg += "--the id need at least 8 digits and only digits\n";
            //    labelID.Foreground = Brushes.Red;
            //    flag = true;
            //}
            //else
            //{
            //    labelID.Foreground = Brushes.Black;
            //}

            if (firstNameTextBox.Text=="")
            {
                msg += "--need first name\n";
                labelFirstName.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelFirstName.Foreground= Brushes.Black;
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

            if (maxTestInWeekTextBox.Text == ""|| !int.TryParse(maxTestInWeekTextBox.Text, out temp)||temp<1)//need only digits and must work atleast one time at week
            {
                msg += "--need work hour, digits only\n";
                labelMaxTestInWeek.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelMaxTestInWeek.Foreground = Brushes.Black;
            }

            if (birthdayDatePicker.DisplayDate==null)
            {
                msg += "--need birth day\n";
                labelBirthDay.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelBirthDay.Foreground = Brushes.Black;
            }

            if (expYearsTextBox.Text == "" || !int.TryParse(expYearsTextBox.Text, out temp))
            {
                msg += "--need exp years,digits only\n";
                labelExpYears.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelExpYears.Foreground = Brushes.Black;
            }

            if (phoneNumberTextBox.Text == "" || !int.TryParse(phoneNumberTextBox.Text, out temp)||phoneNumberTextBox.Text.Length<7)
            {
                msg += "--need phone number, digits only, at least 7 digits\n";
                labelPhoneNumber.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelPhoneNumber.Foreground = Brushes.Black;
            }


            //foreach (bool[] arr in temp_workHour)
            //{
            //    foreach (bool item in arr)
            //    {
            //        if (item)
            //            workHourFlag = true;
            //        break;
            //    }
            //    if (workHourFlag)
            //        break;
            //}
            //if(!workHourFlag)
            //{
            //    msg += "--need work hour, at least one\n";
            //    labelWorkHours.Foreground = Brushes.Red;
            //    return false;
            //}
            //else
            //{
            //    labelWorkHours.Foreground = Brushes.Black;
            //}

            if (flag)
            {
                MessageBox.Show(msg, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

                return false;
            }
           return true;
        }
    }
}
