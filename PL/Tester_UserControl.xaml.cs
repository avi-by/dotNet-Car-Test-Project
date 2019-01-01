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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for Tester_UserControl.xaml
    /// </summary>
    public partial class Tester_UserControl : UserControl
    {
        private IBL bl;
        public Tester_UserControl()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL(Configuration.BLType);
            bl.TesterEvent += TesterEvent;
            string[] SortByValues = { "firstName", "lastName", "id", "max test in week", "gender", "age", "exp years", "car type", "max distance" };
            ComboBoxSortBy.ItemsSource = SortByValues;
            CreateDemoEntites();
        }

      
        private void TesterEvent(object sender, EventArgs e)
        {
            sortContaxDataByComboBox();
        }

        private void CreateDemoEntites()
        {


            bool[][] temp_workHour = new bool[5][];
            for (int i = 0; i < temp_workHour.Length; i++)
            {
                temp_workHour[i] = new bool[6];
            }
            for (var day = DayOfWeek.Sunday; day < DayOfWeek.Friday; day++)
                for (int hour = 0; hour < 6; hour++)
                {
                    if (hour % 2 == 0)
                        temp_workHour[(int)day][hour] = false;
                    else
                        temp_workHour[(int)day][hour] = true;

                }

            bl.addTester(new Tester("123456610", "israel", "israeli", new DateTime(1985, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.PrivetCar, BE.GearBox.Manual, temp_workHour, 10.5));
            bl.addTester(new Tester("123456611", "batia", "shmueli", new DateTime(1984, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.FEMALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));
            bl.addTester(new Tester("123456612", "eliyahu", "teomim", new DateTime(1990, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));
            bl.addTester(new Tester("123456613", "asa'el", "shalom", new DateTime(1989, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));



        }

        private void pbTester_Click(object sender, RoutedEventArgs e)
        {
            AddTesterWindow addTesterWindow = new AddTesterWindow();
            addTesterWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

            sortContaxDataByComboBox();

        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            testerDataGrid.DataContext = bl.getAllTester();
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }


        private void TesterDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //didnt work...
            //if (testerDataGrid.SelectedItem == null) return;
            // var selectedPerson = (testerDataGrid.SelectedItem) as Tester;
            // matrix1.DataContext = selectedPerson.WorkHour;


            //MessageBox.Show(string.Format("The Person you double cl
            if (testerDataGrid.SelectedItem == null) return;
            var selectedPerson = (testerDataGrid.SelectedItem) as Tester;

            UpdateTesterWindow updateTesterWindow = new UpdateTesterWindow(selectedPerson);
            updateTesterWindow.ShowDialog();
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


            // matrix1.DataContext =((testerDataGrid. ));

        }

        private void PbUpdateTester_Click(object sender, RoutedEventArgs e)
        {
            if (testerDataGrid.SelectedItem == null) return;
            var selectedPerson = (testerDataGrid.SelectedItem) as Tester;
            UpdateTesterWindow updateTesterWindow = new UpdateTesterWindow(selectedPerson);
            updateTesterWindow.ShowDialog();

        }

        private void Tests_UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_delete(object sender, RoutedEventArgs e)
        {
            #region message 'are you sure?'
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete the tester?", "", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
            switch (result)
            {
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Yes:
                    break;
                default:
                    break;

            }
            #endregion

            if (testerDataGrid.SelectedItem == null) return;
            try
            {
                bl.deleteTester((testerDataGrid.SelectedItem as Tester));
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MenuItem_Click_add(object sender, RoutedEventArgs e)
        {
            AddTesterWindow addTesterWindow = new AddTesterWindow();
            addTesterWindow.ShowDialog();
        }

        private void MenuItem_Click_edit(object sender, RoutedEventArgs e)
        {
            if (testerDataGrid.SelectedItem == null) return;
            var selectedPerson = (testerDataGrid.SelectedItem) as Tester;

            UpdateTesterWindow updateTesterWindow = new UpdateTesterWindow(selectedPerson);
            updateTesterWindow.ShowDialog();
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
                testerDataGrid.DataContext = bl.getAllTester();
                return;
            }
            var allTester = bl.getAllTester();
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
