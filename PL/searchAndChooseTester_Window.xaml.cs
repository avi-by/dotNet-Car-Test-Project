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

namespace PL
{
    /// <summary>
    /// Interaction logic for searchAndChooseTester_Window.xaml
    /// </summary>
    public partial class searchAndChooseTester_Window : Window
    {
        private Window sender;

        public searchAndChooseTester_Window()
        {
            InitializeComponent();
        }

        public searchAndChooseTester_Window(AddTestWindow addTestWindow)
        {
            InitializeComponent();
            //maybe we will need this in order to return to the addWindow and not to update window 
            sender = addTestWindow;
        }





         



        
    }
}
