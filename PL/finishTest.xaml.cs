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
using BL;
using BE;


namespace PL
{
    /// <summary>
    /// Interaction logic for finishTest.xaml
    /// </summary>
    public partial class FinishTest : Window
    {
        Test tes;
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        public FinishTest()
        {
            
            InitializeComponent();
        //    tes = ((Test)addTestGrid.DataContext);
        }

        private void succeededCheckBox_Click(object sender, RoutedEventArgs e)
        {

      //      bl.completedTest(tes);
        }
    }
}
