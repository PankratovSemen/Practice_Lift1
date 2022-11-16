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


using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для Ais.xaml
    /// </summary>
    public partial class Ais : Window
    {
        public string logV;
        public string roled;
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
            string cmd = "SELECT * FROM magazine"; //Выделение таблицы бд
            MySqlCommand command = new MySqlCommand(cmd,db.getconn());
            command.ExecuteNonQuery();

            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("magazine");
            dataAdp.Fill(dt);
            
            dbj1.ItemsSource = dt.DefaultView;//Заполнение dataGrid базой данных
            
            db.closedconn();
           MainWindow mainWindow = new MainWindow();
           

            if(roled!= "Администратор") //Распределение прав доступа к компонентам для роли администратор
            {
                delbtn.Visibility=Visibility.Hidden;
            }

            //Заполнение значениями из таблицы events базы данных
            db.openconn();
            cmd = "SELECT * FROM events";
            MySqlCommand command1 = new MySqlCommand(cmd, db.getconn());
            command1.ExecuteNonQuery();

            MySqlDataAdapter dataAdp1 = new MySqlDataAdapter(command1);
            DataSet ds = new DataSet();
            dataAdp1.Fill(ds, "events");

            event_combo.ItemsSource = ds.Tables[0].DefaultView;
            event_combo.DisplayMemberPath = ds.Tables[0].Columns["Title_event"].ToString();
            event_combo.SelectedValuePath = ds.Tables[0].Columns["Title_event"].ToString();


            //Заполнение значениями из таблицы residents базы данных
            cmd = "SELECT * FROM residents";
            MySqlCommand command2 = new MySqlCommand(cmd, db.getconn());
            command2.ExecuteNonQuery();

            MySqlDataAdapter dataAdp2 = new MySqlDataAdapter(command2);
            DataSet ds1 = new DataSet();
            dataAdp2.Fill(ds1, "residents");

            resident_combo.ItemsSource = ds1.Tables[0].DefaultView;
            resident_combo.DisplayMemberPath = ds1.Tables[0].Columns["title"].ToString();
            resident_combo.SelectedValuePath = ds1.Tables[0].Columns["title"].ToString();
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

            //Заполнение значениями из таблицы events базы данных
            db.openconn();
            cmd = "SELECT * FROM events";
            MySqlCommand command1 = new MySqlCommand(cmd, db.getconn());
            command1.ExecuteNonQuery();

            MySqlDataAdapter dataAdp1 = new MySqlDataAdapter(command1);
            DataSet ds = new DataSet();
            dataAdp1.Fill(ds, "events");

            event_combo.ItemsSource = ds.Tables[0].DefaultView;
            event_combo.DisplayMemberPath = ds.Tables[0].Columns["Title_event"].ToString();
            event_combo.SelectedValuePath = ds.Tables[0].Columns["Title_event"].ToString();


            //Заполнение значениями из таблицы residents базы данных
            cmd = "SELECT * FROM residents";
            MySqlCommand command2 = new MySqlCommand(cmd, db.getconn());
            command2.ExecuteNonQuery();

            MySqlDataAdapter dataAdp2 = new MySqlDataAdapter(command2);
            DataSet ds1 = new DataSet();
            dataAdp2.Fill(ds1, "residents");

            resident_combo.ItemsSource = ds1.Tables[0].DefaultView;
            resident_combo.DisplayMemberPath = ds1.Tables[0].Columns["title"].ToString();
            resident_combo.SelectedValuePath = ds1.Tables[0].Columns["title"].ToString();
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
                    else if(lists.Text=="Событие")
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
                    else if(lists.Text=="Резидент")
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
                string ev = event_combo.Text;
            string re = resident_combo.Text;
            
            if (re == "")
            {
                re = null;
                MessageBox.Show("re null");
            }
                if (ev == "")
                {
                    ev = null;
                    MessageBox.Show("ev null");
                }
                DB db = new DB();
                db.openconn();
                string cmd = "INSERT INTO `magazine` (`id_visiter`, `id_event`, `id_resident`, `date_open`) VALUES (@vis, @eve, @res, @op);";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@vis", MySqlDbType.VarChar).Value = id_vis_tb.Text;

            command.Parameters.Add("@eve", MySqlDbType.VarChar).Value = ev;
            command.Parameters.Add("@res", MySqlDbType.VarChar).Value = re ;
                command.Parameters.Add("@op", MySqlDbType.DateTime).Value = DateTime.Now;
                 command.ExecuteScalar();
                db.closedconn();
                MessageBox.Show("Запись создана");
                
                id_vis_tb.Clear();
                
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                logger.Error("Ошибка в окне вахтера: \n " + ex);
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
            MessageBox.Show("Запись обновлена");
            id_vis_tb_close.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (roled == "Администратор")
            {
                DB db = new DB();
                db.openconn();

                for (int i = 0; i < selection_ch.Count; i++)
                {
                    string cmd = "DELETE FROM magazine WHERE id_note = @del";
                    MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                    command.Parameters.Add("@del", MySqlDbType.Int32).Value = selection_ch[i].ToString();
                    command.ExecuteNonQuery();


                }
                db.closedconn();
                MessageBox.Show(selection_ch[0]);
            }
            else
                delbtn.Visibility = Visibility.Hidden;
        }
    }
}
