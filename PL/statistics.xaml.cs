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
    /// Interaction logic for statistics.xaml
    /// </summary>
    public partial class statistics : UserControl
    {
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        public statistics()
        {
            InitializeComponent();
            teacherDadaGrid.ItemsSource = bl.successesPercentage("teacher");
            testerDataGrid.ItemsSource = bl.successesPercentage("tester");
            schoolDataGrid.ItemsSource = bl.successesPercentage("school");
        }
    }
}
