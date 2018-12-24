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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
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


            MyBL.Instance.addTester(new Tester( "111", firstNameTextBox.Text,lastNameTextBox.Text, birthdayDatePicker.DisplayDate, new Address(" ", 0, " sd"), (BE.Gender)genderComboBox.SelectedValue,"8",1,1,(BE.CarType)1,(BE.GearBox)1, temp_workHour,0 ));



            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}