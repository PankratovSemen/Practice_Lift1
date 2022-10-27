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

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для Ais.xaml
    /// </summary>
    public partial class Ais : Window
    {
        public Ais()
        {
            InitializeComponent();
        }

       
private void ms1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            left_panel.Visibility = Visibility.Visible;
            sp2.Visibility = Visibility.Collapsed;
            sep.Visibility = Visibility.Hidden;
            ExpP.Visibility = Visibility.Hidden;
        }

        private void left_panel_MouseLeave(object sender, MouseEventArgs e)
        {
            left_panel.Visibility=Visibility.Hidden;
           
            sp2.Visibility = Visibility.Visible;
            sep.Visibility = Visibility.Visible;
            ExpP.Visibility = Visibility.Visible;
        }

        private void Search_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = " ";
                
        }

        private void Search_MouseMove(object sender, MouseEventArgs e)
        {
            Search.Text = " ";
        }

        private void Search_MouseLeave(object sender, MouseEventArgs e)
        {
            Search.Text = "Поиск";
        }
    }
}
