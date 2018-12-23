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
            
            genderComboBox.Items.Add(BE.Gender.MALE);
            genderComboBox.Items.Add(BE.Gender.FEMALE);

            birthdayDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BL.MyBL.Instance.getMaximumAge());
            birthdayDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BL.MyBL.Instance.getMinimumAgeOfTester());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        private void pbAddTester_Click(object sender, RoutedEventArgs e)
        {

            // MyBL.Instance.addTester(new Tester(firstNameTextBox.Text, birthdayDatePicker.DisplayDate, lastNameTextBox.Text ));
            MyBL.Instance.addTester(new Tester("111", firstNameTextBox.Text,lastNameTextBox.Text, birthdayDatePicker.DisplayDate, new Address(" ", 0, " sd"), (BE.Gender)genderComboBox.SelectedValue ));



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