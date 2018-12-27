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
        public Trainees_UserControl()
        {
            InitializeComponent();
            MyBL.Instance.addTrainee(new Trainee("12345678", "yosef", "machanaim", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
            MyBL.Instance.addTrainee(new Trainee("12345678", "hagai", "sugerman", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 25, "99999999", new Address("yehosua", 11, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
            MyBL.Instance.addTrainee(new Trainee("12345678", "moshe", "shauli", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 30, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
            MyBL.Instance.addTrainee(new Trainee("12345678", "david", "bar-hai", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
            MyBL.Instance.addTrainee(new Trainee("12345678", "yehonatan", "yosef", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));



        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            traineeDataGrid.DataContext = MyBL.Instance.getAllTrainees();



            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.M
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }
    }
}
