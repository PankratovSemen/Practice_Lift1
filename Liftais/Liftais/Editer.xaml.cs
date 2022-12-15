using DocumentFormat.OpenXml.Vml;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static string ive_;
        public static string surn_;
        public static string name_;
        public static string mn_;
        public static string birth_;
        public static string ph_;
        public static string em_;
        public static string place_;
        public static string social_;
        public static string findus_;
        public static string dj_;
        public static string note_;
        //Резиденты
        public static string title_;
        public static string tya_;
        public static string teaml_;
        public static string id_res_;
        //События
        public static string id_event_;
        public static string Te_;
        public static string date_begin_;
        public static string organizer_;
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
                MessageBox.Show("Запись обновлена");
            }
            catch(Exception ex)
            {
                
                logger.Error(ex.ToString());
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            if (state == "dbj1")
            {
                dbj1_edit.Visibility = Visibility.Visible;
                db_visiters_edit.Visibility = Visibility.Hidden;
                db_resident_edit.Visibility = Visibility.Hidden;
                db_events_edit.Visibility = Visibility.Hidden;

                id_vis_mv.Text = iv_;
                id_resident_mv.Text = ir_;
                id_event_mv.Text = ie_;
                Title = "Измененить: Журнал посещений";
            }
            else if(state== "db_visiters")
            {
                id_visiter_vis_edit.IsEnabled = false;
                dbj1_edit.Visibility = Visibility.Hidden;
                db_visiters_edit.Visibility = Visibility.Visible;
                db_resident_edit.Visibility = Visibility.Hidden;
                db_events_edit.Visibility = Visibility.Hidden;
                id_visiter_vis_edit.Text = ive_;
                surname_vis_edit.Text = surn_;
                name_vis_edit.Text = name_;
                middle_name_vis_edit.Text = mn_;
                birth_vis_edit.Text=birth_;
                phone_vis_edit.Text = ph_;
                email_vis_edit.Text = em_;
                place_vis_edit.Text = place_;
                social_vis_edit.Text = social_;
                find_us_edit.Text = findus_;
                date_join_vis_edit.Text = dj_;
                notes_edit.Text = note_;
                Title = "Измененить: Посетители";
            }
            else if (state == "db_resident")
            {
                db_resident_edit.Visibility = Visibility.Visible;
                dbj1_edit.Visibility = Visibility.Hidden;
                db_visiters_edit.Visibility = Visibility.Hidden;
                db_events_edit.Visibility = Visibility.Hidden;
                title_res_edit.Text = title_;
                type_activity_res_edit.Text = tya_;
                teamlead_res_edit.Text = teaml_;
                Title = "Измененить: Резидент";
            }
            else if (state == "db_events")
            {
                db_resident_edit.Visibility = Visibility.Hidden;
                dbj1_edit.Visibility = Visibility.Hidden;
                db_visiters_edit.Visibility = Visibility.Hidden;
                db_events_edit.Visibility = Visibility.Visible;
                Title = "Измененить: События";
                title_event_edit.Text = Te_;
                date_begin_edit.Text = date_begin_;
                organizer_edit.Text = organizer_;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string date_birth_convert = Convert.ToDateTime(birth_vis_edit.Text).ToString("yyyy-MM-dd");
                string date_join_convert = Convert.ToDateTime(date_join_vis_edit.Text).ToString("yyyy-MM-dd");
                db.openconn();
                string cmd = "UPDATE `visiter` SET  `surname` = @surn, `name` = @iv, `middle_name` = @mn, `birthday` = @birth, `phone` =@ph, `email` = @em,`place`=@pl,`social_networks`=@sn,`find_us` = @fus,`date_join` = @dj,`for_notes` = @notes WHERE `visiter`. `id_visiter` = @id";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_visiter_vis_edit.Text;
                command.Parameters.Add("@surn", MySqlDbType.VarChar).Value = surname_vis_edit.Text;
                command.Parameters.Add("@iv", MySqlDbType.VarChar).Value = name_vis_edit.Text;
                command.Parameters.Add("@mn", MySqlDbType.VarChar).Value = middle_name_vis_edit.Text;
                command.Parameters.Add("@birth", MySqlDbType.Date).Value = date_birth_convert;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone_vis_edit.Text;
                command.Parameters.Add("@em", MySqlDbType.VarChar).Value = email_vis_edit.Text;
                command.Parameters.Add("@pl", MySqlDbType.VarChar).Value = place_vis_edit.Text;
                command.Parameters.Add("@sn", MySqlDbType.VarChar).Value = social_vis_edit.Text;
                command.Parameters.Add("@fus", MySqlDbType.VarChar).Value = find_us_edit.Text;
                command.Parameters.Add("@dj", MySqlDbType.Date).Value = date_join_convert;
                command.Parameters.Add("@notes", MySqlDbType.VarChar).Value = notes_edit.Text;
                command.ExecuteNonQuery();
                db.closedconn();
                MessageBox.Show("Запись обновлена");
            }
            catch(Exception ex)
            {
               
                logger.Error(ex.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                db.openconn();
                string cmd = "UPDATE `residents` SET `title` = @tit,`type_activity`= @ta,`teamlead` = @tem WHERE `residents`. `id_resident` = @id";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_res_;
                command.Parameters.Add("@tit", MySqlDbType.VarChar).Value = title_res_edit.Text;
                command.Parameters.Add("@ta", MySqlDbType.VarChar).Value = type_activity_res_edit.Text;
                command.Parameters.Add("@tem", MySqlDbType.VarChar).Value = teamlead_res_edit.Text;
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string date_begin_convert = Convert.ToDateTime(date_begin_edit.Text).ToString("yyyy-MM-dd");
                db.openconn();
                string cmd = "UPDATE `events` SET `Title_event` = @te,`date_begin`= @db,`organizer` = @org WHERE `events`. `id_event` = @id";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_event_;
                command.Parameters.Add("@te", MySqlDbType.VarChar).Value = title_event_edit.Text;
                command.Parameters.Add("@db", MySqlDbType.VarChar).Value = date_begin_convert;
                command.Parameters.Add("@org", MySqlDbType.VarChar).Value = organizer_edit.Text;
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                
            }
        }
    }
}
