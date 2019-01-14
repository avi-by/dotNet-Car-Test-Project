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

        private IBL bl = BL.FactoryBL.GetBL(Configuration.BLType);
        private List<Test> allTestList;
        private List<Test> currentUseList;

        public Tests_UserControl()
        {
            InitializeComponent();
            bl.TestEvent += TestEvent;
            allTestList = bl.getAllTests(); //To prevent from get the all list every simple action, save the whole list until the original list change (in the event this variable will get the new list)
            currentUseList = allTestList; //on this variable all the changes will be done
            string[] SortByValues = { "id","car type","tester id","trainee id","date"};
            ComboBoxSortBy.ItemsSource = SortByValues;


            if (FactoryBL.GetBL("myBL").getAllTests().Count==0)
            {
                //FactoryBL.GetBL("myBL").AddTest(new Test("123456610", "12345670", new DateTime(2019, 2, 10, 10, 0, 0), new Address("zefat", 5, "jeruslaem"), GearBox.Manual, CarType.PrivetCar));
                //FactoryBL.GetBL("myBL").AddTest(new Test("123456610", "12345671", new DateTime(2019, 2, 10, 14, 0, 0), new Address("zefat", 5, "jeruslaem"), GearBox.Manual, CarType.PrivetCar));
                //FactoryBL.GetBL("myBL").AddTest(new Test("123456610", "12345672", new DateTime(2019, 2, 10, 12, 0, 0), new Address("zefat", 5, "jeruslaem"), GearBox.Manual, CarType.PrivetCar));
                //FactoryBL.GetBL("myBL").AddTest(new Test("123456610", "12345673", new DateTime(2019, 2, 11, 10, 0, 0), new Address("zefat", 5, "jeruslaem"), GearBox.Manual, CarType.PrivetCar));
                //FactoryBL.GetBL("myBL").AddTest(new Test("123456610", "12345674", new DateTime(2019, 2, 11, 12, 0, 0), new Address("zefat", 5, "jeruslaem"), GearBox.Manual, CarType.PrivetCar));
            }



        }

        private void TestEvent(object sender, EventArgs e)
        {
            allTestList = bl.getAllTests();
            findAndSort();
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
            findAndSort();
        }

      
        private void testDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
            if (((testDataGrid.SelectedItem) as Test) == null) return;
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
            if ((testDataGrid.SelectedItem as Test).Succeeded!=null)
            {
                MessageBox.Show("cant delete finished test", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #region message 'are you sure?'
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete the test?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
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


        /// <summary>
        /// sort the data grid by the sort mod in the combo box
        /// </summary>
        private void sortContaxDataByComboBox()
        {
            if (ComboBoxSortBy.SelectedItem == null)
            {
                testDataGrid.DataContext = currentUseList;
                return;
            }
            var allTest = currentUseList;
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

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (testDataGrid.SelectedItem == null)
                return;
            if ((testDataGrid.SelectedItem as Test).Succeeded!=null)
            {
                MessageBox.Show("this test has finished already", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                FinishTest test = new FinishTest(testDataGrid.SelectedItem as Test);
                test.ShowDialog();
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findAndSort();
        }


        /// <summary>
        /// search for test with id, tester id or treainee id with the string in the sort text box then sort by the sort mod
        /// </summary>
        private void findAndSort()
        {
            if (TextBoxSearch.Text == "")
                currentUseList = allTestList;
            else
                currentUseList = (from i in allTestList
                                  where i.Id != null && i.Id.Contains(TextBoxSearch.Text) || i.TesterId.Contains(TextBoxSearch.Text) || i.TraineeId.Contains(TextBoxSearch.Text)
                                  select i).ToList();
            if (checkBoxFinishTestShow.IsChecked == false)
                currentUseList = (from i in currentUseList
                                  where i.Succeeded == null
                                  select i).ToList();
            sortContaxDataByComboBox();
        }

        private void checkBoxFinishTestShow_Checked(object sender, RoutedEventArgs e)
        {
            findAndSort();
        }

        private void checkBoxFinishTestShow_Unchecked(object sender, RoutedEventArgs e)
        {
            findAndSort();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (testDataGrid.SelectedItem == null) return;
            UpdateTestWindow updateTest = new UpdateTestWindow(testDataGrid.SelectedItem as Test);
            updateTest.ShowDialog();
        }
    }
}
