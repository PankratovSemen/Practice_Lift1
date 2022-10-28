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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

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
        }
           

        private void left_panel_MouseLeave(object sender, MouseEventArgs e)
        {
            left_panel.Visibility=Visibility.Hidden;
           
            
        }

       

        

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT * FROM magazine";
            MySqlCommand command = new MySqlCommand(cmd,db.getconn());
            command.ExecuteNonQuery();

            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("magazine");
            dataAdp.Fill(dt);
            dbj1.ItemsSource = dt.DefaultView;
            
            db.closedconn();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT * FROM magazine";
            MySqlCommand command = new MySqlCommand(cmd, db.getconn());
            command.ExecuteNonQuery();

            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("magazine");
            dataAdp.Fill(dt);
            dbj1.ItemsSource = dt.DefaultView;

            db.closedconn();
        }

        

        private void Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                if (Search.Text == "")
                {
                    DB db = new DB();
                    db.openconn();
                    string cmd = "SELECT * FROM magazine";
                    MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                    command.ExecuteNonQuery();

                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable("magazine");
                    dataAdp.Fill(dt);
                    dbj1.ItemsSource = dt.DefaultView;

                    db.closedconn();
                }
                else if (Search.Text != "")
                {
                    DB db = new DB();
                    db.openconn();
                    string cmd = "SELECT * FROM magazine WHERE id_visiter LIKE @ser";
                    MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                    command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search.Text;
                    command.ExecuteNonQuery();
                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable("magazine");
                    dataAdp.Fill(dt);
                    dbj1.ItemsSource = dt.DefaultView;

                    db.closedconn();
                }
            }
            
        }

        private void Search_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }

        private void Search_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }

        private void Search_MouseMove(object sender, MouseEventArgs e)
        {
            Search.Text = "";
        }
    }
}
