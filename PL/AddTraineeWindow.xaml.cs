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
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        public AddTraineeWindow()
        {

            InitializeComponent();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(CarType));

            birthdayDatePicker.DisplayDateStart = DateTime.Now.AddYears(-bl.getMaximumAge());
            birthdayDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-bl.getMinimumAgeOfTester());

            city.ItemsSource = Configuration.city;
            streetComboBox.IsEnabled = false;
        }

        private void Add_UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {

           

           if (AllFieldOK())
            {

                try
                {
                    bl.addTrainee(new Trainee(ID.Text, firstNameTextBox.Text, lastNameTextBox.Text, birthdayDatePicker.SelectedDate.Value, tbSchoolName.Text, tb_teachername.Text, int.Parse(tbNumberOfLesson.Text), phoneNumberTextBox.Text, new Address(streetComboBox.Text, int.Parse(houseNumberTextBox.Text), city.Text), (BE.Gender)genderComboBox.SelectedValue, (BE.CarType)carTypeComboBox.SelectedValue, (BE.GearBox)gearBoxComboBox.SelectedValue));
                    this.Close();
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

            if (ID.Text.Length < 8 || !long.TryParse(ID.Text, out temp)) //the id need at least 8 digits and only digits so it can be convert to int
            {
                msg += "--the id need at least 8 digits and only digits\n";
                labelID.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelID.Foreground = Brushes.Black;
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

            if (streetComboBox.SelectedItem==null)
            {
                msg += "--need street\n";
                labelStreet.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelStreet.Foreground = Brushes.Black;
            }

            if (city.SelectedItem==null)
            {
                msg += "--need city\n";
                labelCity.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelCity.Foreground = Brushes.Black;
            }

            if (tbSchoolName.Text == "")
            {
                msg += "--need school name\n";
                labelSchoolName.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelSchoolName.Foreground = Brushes.Black;
            }

            if (tb_teachername.Text == "")
            {
                msg += "--need teacher name\n";
                labelTeacherName.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelTeacherName.Foreground = Brushes.Black;
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

            if (genderComboBox.SelectedItem==null)
            {
                msg += "--need gender\n";
                labelGender.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelGender.Foreground = Brushes.Black;
            }

            if (carTypeComboBox.SelectedItem == null)
            {
                msg += "--need car type\n";
                labelCarType.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelCarType.Foreground = Brushes.Black;
            }


            if (gearBoxComboBox.SelectedItem == null)
            {
                msg += "--need gearbox\n";
                labelGearBox.Foreground = Brushes.Red;
                flag = true;
            }
            else
            {
                labelGearBox.Foreground = Brushes.Black;
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



            if (flag)
            {
                MessageBox.Show(msg, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

                return false;
            }
            return true;
        }

        private void city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            streetComboBox.IsEnabled = true;
            streetComboBox.ItemsSource = Configuration.street[city.SelectedItem as string];

        }
    }
}


