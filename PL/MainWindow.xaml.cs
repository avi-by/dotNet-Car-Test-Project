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
       
        public MainWindow()
        {
            InitializeComponent();
           
        }

        

        private void Funny_animation_MediaEnded(object sender, RoutedEventArgs e)
        {

            funny_animation.Position = TimeSpan.FromMilliseconds(1);

            
        }

        private void Trainees_UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}