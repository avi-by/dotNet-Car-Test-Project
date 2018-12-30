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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        public AddTraineeWindow()
        {
            InitializeComponent();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));

            birthdayDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BL.MyBL.Instance.getMaximumAge());
            birthdayDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BL.MyBL.Instance.getMinimumAgeOfTester());


        }

        private void Add_UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {

           

           if (AllFieldOK())
            {
                
                        MyBL.Instance.addTrainee(new Trainee(ID.Text, firstNameTextBox.Text, lastNameTextBox.Text, birthdayDatePicker.DisplayDate, tbSchoolName.Text, tb_teachername.Text, int.Parse(tbNumberOfLesson.Text), phoneNumberTextBox.Text, new Address(streetTextBox.Text, int.Parse(houseNumberTextBox.Text), city.Text), (BE.Gender)genderComboBox.SelectedValue, (BE.CarType)carTypeComboBox.SelectedValue, (BE.GearBox)gearBoxComboBox.SelectedValue));


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

            if (tbNumberOfLesson.Text == "" || !long.TryParse(tbNumberOfLesson.Text, out temp))
            {
                msg += "--need numberOfLesson, digits only\n";
                labelNumberOfLesson.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelNumberOfLesson.Foreground = Brushes.Black;
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
    }
}


