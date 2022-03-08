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


namespace MiAPR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MaxMin mainLogic;

        public MainWindow()
        {
            InitializeComponent();
            mainLogic = new MaxMin(MyCanva);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.DrawPoints();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainLogic.CreateNewCenter();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            while (mainLogic.CreateNewCenter());
            while (mainLogic.UpdateCenters());
        }
    }
}
