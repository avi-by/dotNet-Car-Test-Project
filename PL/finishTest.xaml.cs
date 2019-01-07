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
        Test testResult;
        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        public FinishTest(Test test)
        {
            testResult = test;
            InitializeComponent();
            DataContext = test;
        }

        private void succeededCheckBox_Click(object sender, RoutedEventArgs e)
        {

      //      bl.completedTest(tes);
        }

        private void buttonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (!allFieldsOK())
                return;
            testResult.Test1_ReverseParking = test1_ReverseParkingCheckBox.IsChecked;
            testResult.Test2_KeepingSafeDistance = test2_KeepingSafeDistanceCheckBox.IsChecked;
            testResult.Test3_UsingMirrors = test3_UsingMirrorsCheckBox.IsChecked;
            testResult.Test4_UsingTurnSignals = test4_UsingTurnSignalsCheckBox.IsChecked;
            testResult.Test5_LegalSpeed = test5_LegalSpeedCheckBox.IsChecked;
            testResult.Notes = notesTextBox.Text;
            try
            {
                bl.completedTest(testResult);
                Close();
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private bool allFieldsOK()
        {
            bool flag = false;
            string errorMsg = "problems:\n";
            if (test1_ReverseParkingCheckBox.IsChecked == null)
            {
                errorMsg += "need test 1 result\n";
                flag = true;
                labelTest1_ReverseParking.Foreground = Brushes.Red;
            }
            else
            {
                labelTest1_ReverseParking.Foreground = Brushes.Black;
            }
            if (test2_KeepingSafeDistanceCheckBox.IsChecked == null)
            {
                errorMsg += "need test 2 result\n";
                flag = true;
                labelTest2_KeepingSafeDistance.Foreground = Brushes.Red;
            }
            else
            {
                labelTest2_KeepingSafeDistance.Foreground = Brushes.Black;
            }
            if (test3_UsingMirrorsCheckBox.IsChecked == null)
            {
                errorMsg += "need test 3 result\n";
                flag = true;
                labelTest3_UsingMirrors.Foreground = Brushes.Red;
            }
            else
            {
                labelTest3_UsingMirrors.Foreground = Brushes.Black;
            }
            if (test4_UsingTurnSignalsCheckBox.IsChecked == null)
            {
                errorMsg += "need test 4 result\n";
                flag = true;
                labelTest4_UsingTurnSignals.Foreground = Brushes.Red;
            }
            else
            {
                labelTest4_UsingTurnSignals.Foreground = Brushes.Black;
            }
            if (test5_LegalSpeedCheckBox.IsChecked == null)
            {
                errorMsg += "need test 5 result\n";
                flag = true;
                labelTest5_LegalSpeed.Foreground = Brushes.Red;
            }
            else
            {
                labelTest5_LegalSpeed.Foreground = Brushes.Black;
            }
            if (flag)
            {
                MessageBox.Show(errorMsg, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return false;
            }
            return true;
        }
    }
}
