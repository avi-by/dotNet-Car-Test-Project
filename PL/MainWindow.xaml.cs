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
using BL;
using BE;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyBL bl;
        public MainWindow()
        {
            InitializeComponent();
            bl = MyBL.Instance;
                  CreateDemoEntites();
         //   bl.addTester(new Tester("Nadav", new DateTime(1978, 11, 1)));
      //      lbTesters.DataContext = bl.getAllTester();
        //    testerDataGrid.DataContext = bl.getAllTester();
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

            bl.addTester(new Tester("12345666", "israel", "israeli",new DateTime(1985,1,1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.PrivetCar, BE.GearBox.Manual, temp_workHour, 10.5));
            bl.addTester(new Tester("12345667", "batia", "shmueli", new DateTime(1984, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.FEMALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));
            bl.addTester(new Tester("12345668", "eliyahu", "teomim", new DateTime(1990, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));
            bl.addTester(new Tester("12345669", "asa'el", "shalom", new DateTime(1989, 1, 1), new Address("hacotel", 5, "jerusalem"), BE.Gender.MALE, "02123456", 10, 15, BE.CarType.Truck, BE.GearBox.Auto, temp_workHour, 10.5));



        }

        private void pbTester_Click(object sender, RoutedEventArgs e)
        {
            AddTesterWindow addTesterWindow = new AddTesterWindow();
            addTesterWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
             testerDataGrid.DataContext = bl.getAllTester();
            
      
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));

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
        }

        private void TesterDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (testerDataGrid.SelectedItem == null) return;
            var selectedPerson = (testerDataGrid.SelectedItem) as Tester;
            //matrix1.DataContext = selectedPerson.WorkHour;


            matrix1.IsEnabled = false;
            matrix1.cb_sun_9.IsChecked =  (selectedPerson.WorkHour[0][0]) ? true : false ;
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

          //  updateTesterWindow.DataContext = selectedPerson;


            updateTesterWindow.ShowDialog();

        }

        private void Tests_UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}