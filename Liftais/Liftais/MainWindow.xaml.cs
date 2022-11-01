using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using NLog;


namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private void SingIn_Click(object sender, RoutedEventArgs y)
        {
           
            try
            {
                logger.Debug("Инициализация программы");
                String logins = login.Text;
                String passwords = Password1.Password;
                
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                logger.Debug("Выполнение запроса Sql + Подключение к Базе данных");
                MySqlCommand command = new MySqlCommand("SELECT * FROM usr WHERE login = @lg AND password = @ps", db.getconn());
                command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = logins;
                command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = passwords;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                logger.Debug("Успешное подключение \n"+ "Аккаунт: " + logins);
                logger.Debug("Попытка входа");
                if (table.Rows.Count > 0)
                {
                    logger.Info("Успешный вход");
                    Ais ais1 = new Ais();
                    this.Hide();
                    ais1.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль", "Error");
                    logger.Info("Неправильный логин или пароль\n" + "Аккаунт: " +logins );
                }
                    

                db.closedconn();
            }
            catch (Exception e)
            {
                logger.Error("Ошибка " + e);
                MessageBox.Show("Ошибка подключения к базе данных" , "Ошибка");
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

        }
    }
}
