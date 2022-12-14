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
        MyLogger logger = new MyLogger();
        //Журнал посещений
        
        public static string ir_;
        public static string ie_;
        public static string iv_;
        public static int idn_;
        public static string state;
        //Посетители

        //Резиденты

        //События
        public Editer()
        {
            InitializeComponent();
        }
        DB db = new DB();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.openconn();
                string cmd = "UPDATE `magazine` SET  `id_resident` = @ir, `id_event` = @ie, `id_visiter` = @iv  WHERE `magazine`. `id_note` = @id_n";
                MySqlCommand command = new MySqlCommand(cmd,db.getconn());
               
                command.Parameters.Add("@ir", MySqlDbType.VarChar).Value = id_resident_mv.Text;
                command.Parameters.Add("@ie", MySqlDbType.VarChar).Value = id_event_mv.Text;
                command.Parameters.Add("@iv", MySqlDbType.Int32).Value = id_vis_mv.Text;
                command.Parameters.Add("@id_n", MySqlDbType.Int32).Value = idn_;
                command.ExecuteNonQuery();
                db.closedconn();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message);
                logger.Error(ex.ToString());
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            if (state == "dbj1")
            {
                dbj1_edit.Visibility = Visibility.Visible;
                id_vis_mv.Text = iv_;
                id_resident_mv.Text = ir_;
                id_event_mv.Text = ie_;
            }

        }
    }
}
