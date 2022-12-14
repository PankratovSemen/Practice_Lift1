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
using System.Reflection;
using System.Net;
using System.Globalization;

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        

        private static MyLogger logger =new MyLogger();
        
       
        private void SingIn_Click(object sender, RoutedEventArgs y)
        {
           
            try
            {
                
               
                String logins = login.Text;
                String passwords = Password1.Password;
                
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                
                MySqlCommand command = new MySqlCommand("SELECT role FROM usr WHERE login = @lg AND password = @ps", db.getconn());
                command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = logins;
                command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = passwords;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                
                if (table.Rows.Count > 0)
                {
                    
                    Ais ais1 = new Ais();
                    this.Hide();
                    ais1.logV = login.Text;
                    MyLogger.username = login.Text;
                    var g = table.Rows[0].ItemArray[0];
                    ais1.roled = g.ToString();
                    ais1.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль", "Error");
                    logger.ErrorFile("Неправильный логин или пароль\n" + "Аккаунт: " +logins );
                }
                    

                db.closedconn();
            }
            catch (Exception e)
            {
                logger.ErrorFile("Ошибка " + e);
                MessageBox.Show("Ошибка подключения к базе данных" , "Ошибка");
            }
            finally
            {
               
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updater up = new updater();

            up.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
