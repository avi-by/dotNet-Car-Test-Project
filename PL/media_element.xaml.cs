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

namespace PL
{
    /// <summary>
    /// Interaction logic for media_element.xaml
    /// </summary>
    public partial class media_element : UserControl
    {
        public media_element()
        {
            InitializeComponent();
        }

        private void Funny_animation_MediaEnded(object sender, RoutedEventArgs e)
        {
            funny_animation.Position = TimeSpan.FromMilliseconds(1);
        }  
       
    }
}
