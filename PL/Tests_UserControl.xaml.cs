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
    /// Interaction logic for Tests_UserControl.xaml
    /// </summary>
    public partial class Tests_UserControl : UserControl
    {
        private MyBL bl=BL.MyBL.Instance;

        public Tests_UserControl()
        {
            InitializeComponent();
           
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
            testDataGrid.DataContext = bl.getAllTests();
        }

       

        private void testDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (testDataGrid.SelectedItem == null) return;
            dateCalendar.DisplayDate = (testDataGrid.SelectedItem as Test).Date.Date;
        }
    }
}
