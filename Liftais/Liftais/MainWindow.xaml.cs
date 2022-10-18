using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            String logins = login.Text;
            String passwords = Password1.Password;
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM usr WHERE login = @lg AND password = @ps", db.getconn());
            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value= logins;
            command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = passwords;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                Ais ais1 = new Ais();
                ais1.Show();
                this.Close();
            }
            else
                MessageBox.Show("Неправильный логин или пароль","Error");
                


        }
    }
}
