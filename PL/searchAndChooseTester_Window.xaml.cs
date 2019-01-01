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
    /// Interaction logic for searchAndChooseTester_Window.xaml
    /// </summary>
    public partial class searchAndChooseTester_Window : Window
    {
        private Window sender_window;

        public searchAndChooseTester_Window()
        {
            InitializeComponent();
        }

        public searchAndChooseTester_Window(AddTestWindow addTestWindow)
        {
            InitializeComponent();
            //maybe we will need this in order to return to the addWindow and not to update window 
            sender_window = addTestWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));

            testerDataGrid.DataContext = MyBL.Instance.getAllTester();
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (testerDataGrid.SelectedItem == null)
                return;
            if (sender_window is AddTestWindow)
            {
                (sender_window as AddTestWindow).TesterIdTextBox.Text = (testerDataGrid.SelectedItem as Tester).Id;
                this.Close();
            }

            
            //if (sender_window is UpdateTestWindow)
            //{
            //    (sender_window as UpdateTestWindow).TesterIdTextBox.Text = (testerDataGrid.SelectedItem as Tester).Id;
            //    this.Close();
            //}

        }


        private void TesterDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (testerDataGrid.SelectedItem == null) return;
            var selectedPerson = (testerDataGrid.SelectedItem) as Tester;
            //matrix1.DataContext = selectedPerson.WorkHour;


            matrix1.IsEnabled = false;
            matrix1.cb_sun_9.IsChecked = (selectedPerson.WorkHour[0][0]) ? true : false;
            matrix1.cb_sun_10.IsChecked = (selectedPerson.WorkHour[0][1]) ? true : false;
            matrix1.cb_sun_11.IsChecked = (selectedPerson.WorkHour[0][2]) ? true : false;
            matrix1.cb_sun_12.IsChecked = (selectedPerson.WorkHour[0][3]) ? true : false;
            matrix1.cb_sun_13.IsChecked = (selectedPerson.WorkHour[0][4]) ? true : false;
            matrix1.cb_sun_14.IsChecked = (selectedPerson.WorkHour[0][5]) ? true : false;

            matrix1.cb_mon_9.IsChecked = (selectedPerson.WorkHour[1][0]) ? true : false;
            matrix1.cb_mon_10.IsChecked = (selectedPerson.WorkHour[1][1]) ? true : false;
            matrix1.cb_mon_11.IsChecked = (selectedPerson.WorkHour[1][2]) ? true : false;
            matrix1.cb_mon_12.IsChecked = (selectedPerson.WorkHour[1][3]) ? true : false;
            matrix1.cb_mon_13.IsChecked = (selectedPerson.WorkHour[1][4]) ? true : false;
            matrix1.cb_mon_14.IsChecked = (selectedPerson.WorkHour[1][5]) ? true : false;

            matrix1.cb_tue_9.IsChecked = (selectedPerson.WorkHour[2][0]) ? true : false;
            matrix1.cb_tue_10.IsChecked = (selectedPerson.WorkHour[2][1]) ? true : false;
            matrix1.cb_tue_11.IsChecked = (selectedPerson.WorkHour[2][2]) ? true : false;
            matrix1.cb_tue_12.IsChecked = (selectedPerson.WorkHour[2][3]) ? true : false;
            matrix1.cb_tue_13.IsChecked = (selectedPerson.WorkHour[2][4]) ? true : false;
            matrix1.cb_tue_14.IsChecked = (selectedPerson.WorkHour[2][5]) ? true : false;

            matrix1.cb_wed_9.IsChecked = (selectedPerson.WorkHour[3][0]) ? true : false;
            matrix1.cb_wed_10.IsChecked = (selectedPerson.WorkHour[3][1]) ? true : false;
            matrix1.cb_wed_11.IsChecked = (selectedPerson.WorkHour[3][2]) ? true : false;
            matrix1.cb_wed_12.IsChecked = (selectedPerson.WorkHour[3][3]) ? true : false;
            matrix1.cb_wed_13.IsChecked = (selectedPerson.WorkHour[3][4]) ? true : false;
            matrix1.cb_wed_14.IsChecked = (selectedPerson.WorkHour[3][5]) ? true : false;

            matrix1.cb_thu_9.IsChecked = (selectedPerson.WorkHour[4][0]) ? true : false;
            matrix1.cb_thu_10.IsChecked = (selectedPerson.WorkHour[4][1]) ? true : false;
            matrix1.cb_thu_11.IsChecked = (selectedPerson.WorkHour[4][2]) ? true : false;
            matrix1.cb_thu_12.IsChecked = (selectedPerson.WorkHour[4][3]) ? true : false;
            matrix1.cb_thu_13.IsChecked = (selectedPerson.WorkHour[4][4]) ? true : false;
            matrix1.cb_thu_14.IsChecked = (selectedPerson.WorkHour[4][5]) ? true : false;


            

        }

        private void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null) return;
            sortContaxDataByComboBox();
        }

        private void sortContaxDataByComboBox()
        {
            if (ComboBoxSortBy.SelectedItem == null)
            {
                testerDataGrid.DataContext = MyBL.Instance.getAllTester();
                return;
            }
            var allTester = MyBL.Instance.getAllTester();
            IOrderedEnumerable<Tester> sortTester = null;
            switch (ComboBoxSortBy.SelectedItem.ToString())
            {
                case "firstName":
                    sortTester = from item in allTester orderby item.FirstName select item;
                    break;
                case "lastName":
                    sortTester = from item in allTester orderby item.LastName select item;
                    break;
                case "id":
                    sortTester = from item in allTester orderby item.Id select item;
                    break;
                case "max test in week":
                    sortTester = from item in allTester orderby item.MaxTestInWeek select item;
                    break;
                case "gender":
                    sortTester = from item in allTester orderby item.Gender select item;
                    break;
                case "age":
                    sortTester = from item in allTester orderby item.Age select item;
                    break;
                case "exp years":
                    sortTester = from item in allTester orderby item.ExpYears select item;
                    break;
                case "car type":
                    sortTester = from item in allTester orderby item.CarType select item;
                    break;
                case "max distance":
                    sortTester = from item in allTester orderby item.Distance select item;
                    break;
            }
            testerDataGrid.DataContext = sortTester;
        }
    }
}
