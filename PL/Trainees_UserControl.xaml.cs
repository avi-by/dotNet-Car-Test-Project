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
    /// Interaction logic for Trainees_UserControl.xaml
    /// </summary>
    public partial class Trainees_UserControl : UserControl
    {

        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        private List<Trainee> allTraineeList;
        private List<Trainee> currentUseList;
        public Trainees_UserControl()
        {
            InitializeComponent();
            bl.TraineeEvent += TraineeEvent;
            string[] SortByValues = { "firstName", "lastName", "id", "number of lesson", "gender", "age", "school name", "car type", "teacher name" };
            allTraineeList = bl.getAllTrainees(); //To prevent from get the all list every simple action, save the whole list until the original list change (in the event this variable will get the new list)
            currentUseList = allTraineeList; //on this variable all the changes will be done
            ComboBoxSortBy.ItemsSource = SortByValues;
          bl.addTrainee(new Trainee("12345670", "yosef", "machanaim", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
          bl.addTrainee(new Trainee("12345671", "hagai", "sugerman", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 25, "99999999", new Address("yehosua", 11, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
          bl.addTrainee(new Trainee("12345672", "moshe", "shauli", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 30, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
          bl.addTrainee(new Trainee("12345673", "david", "bar-hai", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
          bl.addTrainee(new Trainee("12345674", "yehonatan", "yosef", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));



        }

        private void TraineeEvent(object sender, EventArgs e)
        {
            allTraineeList = bl.getAllTrainees();
            findAndSort();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            findAndSort();



            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.M
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void PbUpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            if (traineeDataGrid.SelectedItem == null) return;
            var selectedPerson = (traineeDataGrid.SelectedItem) as Trainee;

            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(selectedPerson);

            //  updateTesterWindow.DataContext = selectedPerson;


            updateTraineeWindow.ShowDialog();
        }

        private void PbAddTrainee_Click(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.ShowDialog();
        }

        private void MenuItem_Click_delete(object sender, RoutedEventArgs e)
        {
            #region message 'are you sure?'
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete the trainee?", "", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
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

            if (traineeDataGrid.SelectedItem == null) return;
            try
            {
                bl.deleteTrainee((traineeDataGrid.SelectedItem as Trainee));
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MenuItem_Click_add(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.ShowDialog();
        }

        private void MenuItem_Click_edit(object sender, RoutedEventArgs e)
        {
            if (traineeDataGrid.SelectedItem == null) return;
            var selectedPerson = (traineeDataGrid.SelectedItem) as Trainee;

            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(selectedPerson);

            //  updateTesterWindow.DataContext = selectedPerson;


            updateTraineeWindow.ShowDialog();
        }

        /// <summary>
        /// sort the data grid by the sort mod in the combo box
        /// </summary>
        private void sortContaxDataByComboBox()
        {
            if (ComboBoxSortBy.SelectedItem == null)
            {
                traineeDataGrid.DataContext = currentUseList;
                return;
            }
           
            var allTrainee =currentUseList;
            IOrderedEnumerable<Trainee> sortTrainee = null;
            switch (ComboBoxSortBy.SelectedItem.ToString())
            {
                case "firstName":
                    sortTrainee = from item in allTrainee orderby item.FirstName select item;
                    break;
                case "lastName":
                    sortTrainee = from item in allTrainee orderby item.LastName select item;
                    break;
                case "id":
                    sortTrainee = from item in allTrainee orderby item.Id select item;
                    break;
                case "number of lesson":
                    sortTrainee = from item in allTrainee orderby item.NumberOfLesson select item;
                    break;
                case "gender":
                    sortTrainee = from item in allTrainee orderby item.Gender select item;
                    break;
                case "age":
                    sortTrainee = from item in allTrainee orderby item.Age select item;
                    break;
                case "school name":
                    sortTrainee = from item in allTrainee orderby item.SchoolName select item;
                    break;
                case "car type":
                    sortTrainee = from item in allTrainee orderby item.CarType select item;
                    break;
                case "teacher name":
                    sortTrainee = from item in allTrainee orderby item.TeacherName select item;
                    break;
            }
            traineeDataGrid.DataContext = sortTrainee;
        }

        private void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null) return;
            sortContaxDataByComboBox();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findAndSort();
        }

        /// <summary>
        /// search for trainee with id, first name or last name with the string in the sort text box then sort by the sort mod
        /// </summary>
        private void findAndSort()
        {
            if (TextBoxSearch.Text == "")
                currentUseList = allTraineeList;
            else
                currentUseList = (from i in allTraineeList
                                  where i.FirstName.Contains(TextBoxSearch.Text) || i.LastName.Contains(TextBoxSearch.Text) || i.Id.Contains(TextBoxSearch.Text)
                                  select i).ToList();
            sortContaxDataByComboBox();
        }
    }
}
