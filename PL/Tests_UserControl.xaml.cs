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
using System.Collections.Specialized;
using BE;
using BL;

namespace PL
{
    
    /// <summary>
    /// Interaction logic for Tests_UserControl.xaml
    /// </summary>
    public partial class Tests_UserControl : UserControl
    {
       // private MyBL bl=BL.MyBL.Instance;
        private IBL bl = BL.FactoryBL.GetBL(Configuration.BLType);
        public Tests_UserControl()
        {
            InitializeComponent();
            bl.TestEvent += TestEvent;
            string[] SortByValues = { "id","car type","tester id","trainee id","date"};
            ComboBoxSortBy.ItemsSource = SortByValues;


        }

        private void TestEvent(object sender, EventArgs e)
        {
            sortContaxDataByComboBox();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded_2(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            //try
            //{
            //    bl.AddTest(new Test("12345666", "12345670", new DateTime(2019, 2, 3), new Address("hacotel", 5, "jerusalem"), GearBox.Manual, CarType.PrivetCar));

            //}
            //catch (Exception msg)
            //{

            //    MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            sortContaxDataByComboBox();
        }

      
        private void testDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (testDataGrid.SelectedItem == null) return;
            dateCalendar.DisplayDate = (testDataGrid.SelectedItem as Test).Date.Date;
        }

 
        private void PbAdd_Test_Click(object sender, RoutedEventArgs e)
        {
            AddTestWindow addTestWindow = new AddTestWindow();
            addTestWindow.ShowDialog();
        }

        private void MenuItem_add_Click(object sender, RoutedEventArgs e)
        {
            AddTestWindow addTestWindow = new AddTestWindow();
            addTestWindow.ShowDialog();
        }

        private void MenuItem_delete_Click(object sender, RoutedEventArgs e)
        {
            #region message 'are you sure?'
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete the test?", "", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
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

            if (testDataGrid.SelectedItem == null) return;
            try
            {
                bl.DeleteTest((testDataGrid.SelectedItem as Test));
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sortContaxDataByComboBox()
        {
            if (ComboBoxSortBy.SelectedItem == null)
            {
                testDataGrid.DataContext = bl.getAllTests();
                return;
            }
            var allTest = bl.getAllTests();
            IOrderedEnumerable<Test> sortTest = null;
            switch (ComboBoxSortBy.SelectedItem.ToString())
            {
                case "date":
                    sortTest = from item in allTest orderby item.Date select item;
                    break;
                case "tester id":
                    sortTest = from item in allTest orderby item.TesterId select item;
                    break;
                case "trainee id":
                    sortTest = from item in allTest orderby item.TraineeId select item;
                    break;
                case "id":
                    sortTest = from item in allTest orderby item.Id select item;
                    break;
                case "car type":
                    sortTest = from item in allTest orderby item.Car select item;
                    break;
               
            }
            testDataGrid.DataContext = sortTest;
        }

        private void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortContaxDataByComboBox();
        }
    }
}
