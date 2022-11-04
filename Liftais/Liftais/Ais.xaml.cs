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
using NLog;
using Excel = Microsoft.Office.Interop.Excel;
using NLog;
using ClosedXML.Excel;



namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для Ais.xaml
    /// </summary>
    public partial class Ais : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Ais()
        {
            InitializeComponent();
        }
        List<string> selection_ch = new List<string>();
        List<string> visiter_ch = new List<string>();
        List<string> resident_ch = new List<string>();
        List<string> events_ch = new List<string>();
        List<string> open_ch = new List<string>();
        List<string> close_ch = new List<string>();
        //При нажатии на иконку отерывается панель меню
        private void ms1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            left_panel.Visibility = Visibility.Visible;
        }
           
        //Если указатель мыши покинет панель меню,то меню закроется 
        private void left_panel_MouseLeave(object sender, MouseEventArgs e)
        {
            left_panel.Visibility=Visibility.Hidden;
           
            
        }

       

        

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //При загрузке формы вывести таблицу журнал посетителей
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
            //При активном окне обновлять полностью таблицу
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
            //Поиск по таблице базы данных журнал посещения
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

                    lists.Text = "";
                }
                else if (Search.Text != "")
                {
                    

                    if(lists.Text== "Номер записи")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM magazine WHERE id_note LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("magazine");
                        dataAdp.Fill(dt);
                        dbj1.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else if(lists.Text == "Номер посетителя")
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
                    else if(lists.Text=="Номер события")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM magazine WHERE id_event LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("magazine");
                        dataAdp.Fill(dt);
                        dbj1.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else if(lists.Text=="Номер резидента")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM magazine WHERE id_resident LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("magazine");
                        dataAdp.Fill(dt);
                        dbj1.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else
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

        int a = 0;
        private void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                //Проверка checkbox и добавление элементов в список для последующего использования
                CheckBox selch = (CheckBox)sender;

                if (selch.IsChecked == false)
                {
                    selch.IsChecked = true;
                    DataRowView row = (DataRowView)dbj1.SelectedItems[0];

                   
                    if (selection_ch.Contains(row["id_note"].ToString()) == false)
                    {
                        
                        selection_ch.Add(row["id_note"].ToString());
                        visiter_ch.Add(row["id_visiter"].ToString());
                        resident_ch.Add(row["id_resident"].ToString());
                        events_ch.Add(row["id_event"].ToString());
                        open_ch.Add(row["date_open"].ToString());
                        close_ch.Add(row["date_close"].ToString());
                        MessageBox.Show("Элемент №: " + row["id_note"].ToString() + " добавлен в список");
                        a++;
                        counts.Content = "Элементов" + a;
                    }
                    else
                    {
                        selection_ch.Remove(row["id_note"].ToString());
                        visiter_ch.Remove(row["id_visiter"].ToString());
                        resident_ch.Remove(row["id_resident"].ToString());
                        events_ch.Remove(row["id_event"].ToString());
                        open_ch.Remove(row["date_open"].ToString());
                        close_ch.Remove(row["date_close"].ToString());
                        MessageBox.Show("Элемент №: " + row["id_note"].ToString() + " удален из списка");
                        a-=1;
                        counts.Content = "Элементов: " + a;
                    }
                        
                    
                }

                
            }
            catch
            {
                MessageBox.Show("Внимание \n Выделите строку");

            }


            
            

        }

        private void btnexp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var wb = new XLWorkbook();
                var sh = wb.Worksheets.Add("Export");
                for (int j = 0; j < dbj1.Columns.Count; j++)
                {

                    
                    sh.Cell(1, j+1).SetValue(dbj1.Columns[j].Header);
                    sh.Cell(1,j+1).Style.Font.Bold = true;
                    sh.Columns().AdjustToContents();
                    sh.Rows().AdjustToContents();
                }

                int q = 1;
                for (int i = 0; i < dbj1.Columns.Count; i++)
                {
                   

                    for (int j = 0; j < dbj1.Items.Count; j++)
                    {
                       
                        int w = i + 1;
                       
                        
                        DataRowView row = (DataRowView)dbj1.Items[j];
                        if (selection_ch.Contains(row[i].ToString()))
                        {




                            
                                MessageBox.Show(row[i].ToString());
                                sh.Cell(j + 1, i+2 ).SetValue(row[i]);

                            if (visiter_ch.Contains(row[i+1].ToString()))
                            {
                                sh.Cell(j + 1, i + 3).SetValue(row[i + 1]);
                            }
                           
                            if (resident_ch.Contains(row[i+3].ToString()))
                            {
                                sh.Cell(j+1, i + 5).SetValue(row[i + 3]);
                            }
                            
                            if (events_ch.Contains(row[i+2].ToString()) )
                            {
                                if (events_ch.Contains(row[i + 2].ToString()) != selection_ch.Contains(row[i]))
                                {
                                    sh.Cell(j + 1, i + 4).SetValue(row[i + 2]);
                                }


                            }
                            if (open_ch.Contains(row[i+4].ToString()))
                            {
                                sh.Cell(j+1, i +6).SetValue(row[i + 4]);
                            }
                            
                            if (close_ch.Contains(row[i+5].ToString()))
                            {
                                sh.Cell(j+1, i + 7).SetValue(row[i + 5]);
                               
                                
                            }
                            

                        }
                        else if (a==0)
                        {
                            sh.Cell(j + 2, i + 2).SetValue(row[i]);
                            

                        }
                        
                        wb.SaveAs("Export_AIS\\data.xlsx");
                        if (q == 1)
                        {
                            MessageBox.Show("Таблица экспортирована");
                            q++;
                        }
                        










                    }

                }
                


            }

            catch(Exception ex)
            {
                logger.Error("Ошибка:  " + ex);
            }
           
        }

        private void JP_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dbj1.Visibility = Visibility.Hidden;
            sep.Visibility = Visibility.Hidden;
            ExpP.Visibility = Visibility.Hidden;
            Reg_vis.Foreground = Brushes.Blue;
            JP.Foreground = Brushes.White;
            cr_notes.Visibility = Visibility.Visible;
        }

        private void JP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dbj1.Visibility = Visibility.Visible;
            sep.Visibility = Visibility.Visible;
            ExpP.Visibility = Visibility.Visible;
            Reg_vis.Foreground = Brushes.White;
            JP.Foreground = Brushes.Blue;
            cr_notes.Visibility = Visibility.Hidden;
        }

        private void create_note_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.openconn();
                string cmd = "INSERT INTO `magazine` (`id_visiter`, `id_event`, `id_resident`, `date_open`) VALUES (@vis, @eve, @res, @op);";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@vis", MySqlDbType.VarChar).Value = id_vis_tb.Text;
                command.Parameters.Add("@eve", MySqlDbType.VarChar).Value = null;
                command.Parameters.Add("@res", MySqlDbType.VarChar).Value = null;
                command.Parameters.Add("@op", MySqlDbType.DateTime).Value = DateTime.Now;
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                DataTable dt = new DataTable("magazine");
                dataAdp.SelectCommand = command;
                dataAdp.Fill(dt);
                db.closedconn();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void create_date_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT id_note FROM `magazine` ORDER BY id_visiter=@vis DESC LIMIT 1";

            MySqlCommand command = new MySqlCommand(cmd, db.getconn());
            command.Parameters.Add("@vis", MySqlDbType.Int32).Value = id_vis_tb_close.Text;
            command.ExecuteNonQuery();
            MySqlDataAdapter dataAd = new MySqlDataAdapter(command);
            dataAd.SelectCommand = command;
            
            DataTable dt = new DataTable("closer");
            dataAd.Fill(dt);
            int last_note = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    last_note = Convert.ToInt32(item);
                   
                }
            }

            cmd = "UPDATE `magazine` SET `date_close` = @dc WHERE `magazine`. `id_note` = @id_n";
            MySqlCommand comm = new MySqlCommand(cmd, db.getconn());
            comm.Parameters.Add("@dc", MySqlDbType.DateTime).Value = DateTime.Now;
            comm.Parameters.Add("@id_n", MySqlDbType.Int32).Value = last_note;
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(comm);
            DataTable dt1 = new DataTable("up");
            dataAdp.SelectCommand = comm;
            dataAdp.Fill(dt1);
            db.closedconn();
        }
    }
}
