using Microsoft.Win32;
using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для Editer.xaml
    /// </summary>
    public partial class Editer : Window
    {
        public Editer()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("server = 127.0.0.1; port = 3306; username = root; database = lift");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string cmd = "UPDATE `magazine` SET `date_close` = @dc WHERE `magazine`. `id_note` = @id_n ";
            MySqlCommand command = new MySqlCommand();


        }
    }
}
