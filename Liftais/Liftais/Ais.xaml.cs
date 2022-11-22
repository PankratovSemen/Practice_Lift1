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
using DocumentFormat.OpenXml.Office.Word;

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для Ais.xaml
    /// </summary>
    public partial class Ais : Window
    {
        public int visits = 0;
        public string logV;
        public string roled;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Ais()
        {
            InitializeComponent();
        }
        List<string> selection_ch = new List<string>();
       

        List<string> visiter_id_ch = new List<string>();
        List<string> residents_id_ch = new List<string>();
        
        //При нажатии на иконку отерывается панель меню
        private void ms1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            left_panel.Visibility = Visibility.Visible;
        }

        //Если указатель мыши покинет панель меню,то меню закроется 
        private void left_panel_MouseLeave(object sender, MouseEventArgs e)
        {
            left_panel.Visibility = Visibility.Hidden;


        }
        int f = 0;





        //Загрузка формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search.Visibility = Visibility.Visible;
            lists.Visibility = Visibility.Visible;

            dbj1.Visibility = Visibility.Visible;
            db_visiters.Visibility = Visibility.Hidden;
            JP.Visibility = Visibility.Visible;
            Reg_vis.Visibility = Visibility.Visible;
            Visiter_View.Visibility = Visibility.Hidden;
            Reg_visiters_note.Visibility = Visibility.Hidden;
            Search_visiters.Visibility = Visibility.Hidden;
            select_visiters.Visibility = Visibility.Hidden;
            //При загрузке формы вывести таблицу журнал посетителей
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT * FROM magazine"; //Выделение таблицы бд
            MySqlCommand command = new MySqlCommand(cmd, db.getconn());
            command.ExecuteNonQuery();

            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("magazine");
            dataAdp.Fill(dt);

            dbj1.ItemsSource = dt.DefaultView;//Заполнение dataGrid базой данных

            db.closedconn();
            MainWindow mainWindow = new MainWindow();


            if (roled != "Администратор") //Распределение прав доступа к компонентам для роли администратор
            {
                delbtn.Visibility = Visibility.Hidden;
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

            Search.Visibility = Visibility.Visible;
            lists.Visibility = Visibility.Visible;


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

            //Обновление таблицы resident
            

            db.openconn();
            string cmd1 = "SELECT * FROM residents";
            MySqlCommand command3 = new MySqlCommand(cmd1, db.getconn());
            command3.ExecuteNonQuery();

            MySqlDataAdapter dataAdp3 = new MySqlDataAdapter(command3);
            DataTable dt3 = new DataTable("residents");
            dataAdp3.Fill(dt3);
            db_resident.ItemsSource = dt3.DefaultView;

            db.closedconn();

            //Обновление таблицы visiters
            db.openconn();
            string cmd4 = "SELECT * FROM visiter";
            MySqlCommand command4 = new MySqlCommand(cmd4, db.getconn());
            command4.ExecuteNonQuery();

            MySqlDataAdapter dataAdp4 = new MySqlDataAdapter(command4);
            DataTable dt4 = new DataTable("visiter");
            dataAdp4.Fill(dt4);
            db_visiters.ItemsSource = dt4.DefaultView;
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


                    if (lists.Text == "Номер записи")
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
                    else if (lists.Text == "Номер посетителя")
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
                    else if (lists.Text == "Событие")
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
                    else if (lists.Text == "Резидент")
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
                //Выделение строк
                //Проверка checkbox и добавление элементов в список для последующего использования
                CheckBox selch = (CheckBox)sender;

                if (selch.IsChecked == false)
                {
                    selch.IsChecked = true;
                    DataRowView row = (DataRowView)dbj1.SelectedItems[0];


                    if (selection_ch.Contains(row["id_note"].ToString()) == false)
                    {

                        selection_ch.Add(row["id_note"].ToString());
                       
                        MessageBox.Show("Элемент №: " + row["id_note"].ToString() + " добавлен в список");
                        a++;
                        counts.Content = "Элементов" + a;
                    }
                    else
                    {
                        selection_ch.Remove(row["id_note"].ToString());
                        
                        MessageBox.Show("Элемент №: " + row["id_note"].ToString() + " удален из списка");
                        a -= 1;
                        counts.Content = "Элементов: " + a;
                    }


                }


            }
            catch(Exception ex)
            {
                logger.Error("Ошибка выделения таблицы: " + ex);
                MessageBox.Show("Внимание \n Выделите строку");

            }





        }

        private void btnexp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dbj1.Visibility == Visibility.Visible)
                {
                    //Экспорт в Excel
                    var wb = new XLWorkbook();
                    var sh = wb.Worksheets.Add("Export");
                    for (int j = 0; j < dbj1.Columns.Count; j++)
                    {


                        sh.Cell(1, j + 1).SetValue(dbj1.Columns[j].Header);
                        sh.Cell(1, j + 1).Style.Font.Bold = true;
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
                                sh.Cell(j + a, i + 2).SetValue(row[i]);

                                
                                
                                    sh.Cell(j + a, i + 3).SetValue(row[i + 1]);
                                

                               
                                    sh.Cell(j + a, i + 5).SetValue(row[i + 3]);
                                

                               
                                    
                                    sh.Cell(j + a, i + 4).SetValue(row[i + 2]);
                                    


                                
                                
                                   sh.Cell(j + a, i + 6).SetValue(row[i + 4]);
                                

                               
                                
                                   sh.Cell(j + a, i + 7).SetValue(row[i + 5]);


                                


                            }
                            else if (a == 0)
                            {
                                sh.Cell(j + 2, i + 2).SetValue(row[i]);


                            }

                            wb.SaveAs("Export_AIS\\Журнал_посещений.xlsx");
                            if (q == 1)
                            {
                                MessageBox.Show("Таблица экспортирована");
                                q++;
                            }
                        }
                    }
                }
                else if (db_resident.Visibility == Visibility.Visible)
                {
                    //Экспорт в Excel
                    MessageBox.Show(f.ToString());
                    var wb = new XLWorkbook();
                    var sh = wb.Worksheets.Add("Export");
                    for (int j = 0; j < db_resident.Columns.Count; j++)
                    {


                        sh.Cell(1, j + 1).SetValue(db_resident.Columns[j].Header);
                        sh.Cell(1, j + 1).Style.Font.Bold = true;
                        sh.Columns().AdjustToContents();
                        sh.Rows().AdjustToContents();
                    }

                    int q = 1;
                    for (int i = 0; i < db_resident.Columns.Count; i++)
                    {


                        for (int j = 0; j < db_resident.Items.Count; j++)
                        {

                            int w = i + 1;


                            DataRowView row = (DataRowView)db_resident.Items[j];
                            if (residents_id_ch.Contains(row[i].ToString()))
                            {
                                sh.Cell((f + j)+1 , i + 2).SetValue(row[i]);
                                sh.Cell((f + j)+1 , i + 3).SetValue(row[i + 1]);
                                sh.Cell((f + j)+1 , i + 4).SetValue(row[i + 2]);
                                sh.Cell((f + j)+1, i + 5).SetValue(row[i + 3]);
                                
                                MessageBox.Show(row[i].ToString());
                               
                                
                             
                                
                                
                                
                                

                                


                            }
                            else if (f == 0)
                            {
                                sh.Cell(j + 2, i + 2).SetValue(row[i]);


                            }

                            wb.SaveAs("Export_AIS\\Резиденты.xlsx");
                            if (q == 1)
                            {
                                MessageBox.Show("Таблица экспортирована");
                                q++;
                            }
                        }
                    }
                }
                else if (db_visiters.Visibility == Visibility.Visible)
                {
                    //Экспорт в Excel
                   
                    var wb = new XLWorkbook();
                    var sh = wb.Worksheets.Add("Export");
                    for (int j = 0; j < db_visiters.Columns.Count; j++)
                    {


                        sh.Cell(1, j + 1).SetValue(db_visiters.Columns[j].Header);
                        sh.Cell(1, j + 1).Style.Font.Bold = true;
                        sh.Columns().AdjustToContents();
                        sh.Rows().AdjustToContents();
                    }

                    int q = 1;
                    for (int i = 0; i < db_visiters.Columns.Count; i++)
                    {


                        for (int j = 0; j < db_visiters.Items.Count; j++)
                        {

                            int w = i + 1;


                            DataRowView row = (DataRowView)db_visiters.Items[j];
                            if (visiter_id_ch.Contains(row[i].ToString()))
                            {
                                sh.Cell(b + j, i + 2).SetValue(row[i]);
                                sh.Cell(b + j, i + 3).SetValue(row[i + 1]);
                                sh.Cell(b + j, i + 4).SetValue(row[i + 2]);
                                sh.Cell(b + j, i + 5).SetValue(row[i + 3]);
                                sh.Cell(b + j, i + 6).SetValue(row[i + 4]);
                                sh.Cell(b + j, i + 7).SetValue(row[i + 5]);
                                sh.Cell(b + j, i + 8).SetValue(row[i + 6]);
                                sh.Cell(b + j, i + 9).SetValue(row[i + 7]);
                                sh.Cell(b + j, i + 10).SetValue(row[i + 8]);
                                sh.Cell(b + j, i + 11).SetValue(row[i + 9]);
                                sh.Cell(b + j, i + 12).SetValue(row[i + 10]);
                                MessageBox.Show(row[i].ToString());











                            }
                            else if (b == 0)
                            {
                                sh.Cell(j + 2, i + 2).SetValue(row[i]);


                            }

                            wb.SaveAs("Export_AIS\\Посетители.xlsx");
                            if (q == 1)
                            {
                                MessageBox.Show("Таблица экспортирована");
                                q++;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
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
        //Создание записей в таблице magazine
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
                command.Parameters.Add("@res", MySqlDbType.VarChar).Value = re;
                command.Parameters.Add("@op", MySqlDbType.DateTime).Value = DateTime.Now;
                command.ExecuteScalar();
                db.closedconn();
                MessageBox.Show("Запись создана");

                id_vis_tb.Clear();

            }

            catch (Exception ex)
            {
                //Запись ошибки в лог файл
                logger.Error("Ошибка в окне вахтера: \n " + ex);
            }
        }

        private void create_date_Click(object sender, RoutedEventArgs e)
        {
            //Заполнение даты выхода по номеру посетителя с помощью обновления записи
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT MAX(id_note) FROM `magazine` WHERE id_visiter = 1144 ORDER BY id_visiter LIMIT 1;";

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
            //Удаление записей из таблицы 
            if (roled == "Администратор")
            {
                DB db = new DB();
                db.openconn();

                if (dbj1.Visibility == Visibility.Visible)
                {
                    for (int i = 0; i < selection_ch.Count; i++)
                    {
                        string cmd = "DELETE FROM magazine WHERE id_note = @del";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@del", MySqlDbType.Int32).Value = selection_ch[i].ToString();
                        command.ExecuteNonQuery();
                        //selection_ch.RemoveAt(i);
                       
                        a--;
                        
                    }

                    counts.Content = "Элементов: " + a;
                    MessageBox.Show("Удаление завершено");
                    //Вывод обновленной таблицы
                    string cmd1 = "SELECT * FROM magazine";
                    MySqlCommand command1 = new MySqlCommand(cmd1, db.getconn());
                    command1.ExecuteNonQuery();
                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command1);
                    DataTable dt = new DataTable("magazine");
                    dataAdp.Fill(dt);
                    dbj1.ItemsSource = dt.DefaultView;




                }
                else if (db_visiters.Visibility == Visibility.Visible)
                {
                    for (int i = 0; i < visiter_id_ch.Count; i++)
                    {
                        string cmd = "DELETE FROM visiter WHERE id_visiter = @del";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@del", MySqlDbType.Int32).Value = visiter_id_ch[i].ToString();
                        command.ExecuteNonQuery();
                        //visiter_id_ch.RemoveAt(i);
                        b--;
                        counts.Content = "Элементов: " + b;
                    }
                    MessageBox.Show("Удаление завершено");
                    string cmd1 = "SELECT * FROM visiter";
                    MySqlCommand command1 = new MySqlCommand(cmd1, db.getconn());
                    command1.ExecuteNonQuery();
                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command1);
                    DataTable dt = new DataTable("visiters");
                    dataAdp.Fill(dt);
                    db_visiters.ItemsSource = dt.DefaultView;


                }

                else if (db_resident.Visibility == Visibility.Visible)
                {
                    for (int i = 0; i < residents_id_ch.Count; i++)
                    {
                        string cmd = "DELETE FROM residents WHERE id_resident = @del";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@del", MySqlDbType.Int32).Value = residents_id_ch[i].ToString();
                        command.ExecuteNonQuery();
                        
                        f--;
                        counts.Content = "Элементов: " + b;
                    }
                    MessageBox.Show("Удаление завершено");
                    string cmd1 = "SELECT * FROM residents";
                    MySqlCommand command1 = new MySqlCommand(cmd1, db.getconn());
                    command1.ExecuteNonQuery();
                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command1);
                    DataTable dt = new DataTable("residents");
                    dataAdp.Fill(dt);
                    db_resident.ItemsSource = dt.DefaultView;
                }

                
                db.closedconn();
              
            }
            else
                delbtn.Visibility = Visibility.Hidden;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Переход в меню Посетители
            dbj1.Visibility = Visibility.Hidden;
            cr_notes.Visibility = Visibility.Hidden;
            lists.Visibility = Visibility.Hidden;
            JP.Visibility = Visibility.Hidden;
            Reg_vis.Visibility = Visibility.Hidden;
            db_resident.Visibility = Visibility.Hidden;
            Visiter_View.Visibility = Visibility.Visible;
            Reg_visiters_note.Visibility = Visibility.Visible;
            visits++;
            db_visiters.Visibility = Visibility.Visible;
            select_visiters.Visibility = Visibility.Visible;
            Search_visiters.Visibility = Visibility.Visible;
            Search_resident.Visibility = Visibility.Hidden;
            select_resident.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            resident_View.Visibility = Visibility.Hidden;
            create_residents.Visibility = Visibility.Hidden;
            cr_notes_residents_menu.Visibility = Visibility.Hidden;
            DB db = new DB();
            db.openconn();
            string cmd = "SELECT * FROM visiter";
            MySqlCommand command = new MySqlCommand(cmd, db.getconn());
            command.ExecuteNonQuery();

            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("visiter");
            dataAdp.Fill(dt);
            db_visiters.ItemsSource = dt.DefaultView;

        }

        private void TextBlock_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //Переход в меню журнал посещений
            lists.Visibility = Visibility.Visible;
            dbj1.Visibility = Visibility.Visible;
            cr_notes.Visibility = Visibility.Hidden;
            JP.Visibility = Visibility.Visible;
            Reg_vis.Visibility = Visibility.Visible;
            db_visiters.Visibility = Visibility.Hidden;
            visits = 0;
            Visiter_View.Visibility = Visibility.Hidden;
            Reg_visiters_note.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Visible;
            create_residents.Visibility = Visibility.Hidden;
            select_visiters.Visibility = Visibility.Hidden;
            Search_visiters.Visibility = Visibility.Hidden;
            Search_resident.Visibility = Visibility.Hidden;
            select_resident.Visibility = Visibility.Hidden;
            resident_View.Visibility = Visibility.Hidden;
            cr_notes_residents_menu.Visibility = Visibility.Hidden;
        }


        int b = 0;
        private void selch1_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            try
            {

                //Проверка checkbox и добавление элементов в список для последующего использования
                CheckBox selch1 = (CheckBox)sender;

                if (selch1.IsChecked == false)
                {
                    selch1.IsChecked = true;
                    DataRowView row = (DataRowView)db_visiters.SelectedItems[0];


                    if (visiter_id_ch.Contains(row["id_visiter"].ToString()) == false)
                    {

                        visiter_id_ch.Add(row["id_visiter"].ToString());
                       
                        MessageBox.Show("Элемент №: " + row["id_visiter"].ToString() + " добавлен в список");
                        b++;
                        counts.Content = "Элементов" + b;
                    }
                    else
                    {
                        selch1.IsChecked = false;
                        visiter_id_ch.Remove(row["id_visiter"].ToString());
                        
                        MessageBox.Show("Элемент №: " + row["id_visiter"].ToString() + " удален из списка");
                        b -= 1;
                        counts.Content = "Элементов: " + b;
                    }


                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка: \n" + ex);
                MessageBox.Show("Ошибка: Выделите строку");
            }

        }

        private void Visiter_View_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Открытие вкладки Просмотр посетителей
            db_visiters.Visibility = Visibility.Visible;
            lists.Visibility = Visibility.Hidden;
            select_visiters.Visibility = Visibility.Visible;
            dbj1.Visibility = Visibility.Hidden;
            cr_notes.Visibility = Visibility.Hidden;
            ExpP.Visibility = Visibility.Visible;
            sep.Visibility = Visibility.Visible;
            cr_notes_visiter_visiters.Visibility = Visibility.Hidden;
        }

        private void Reg_visiters_note_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExpP.Visibility = Visibility.Hidden;
            sep.Visibility = Visibility.Hidden;
            db_visiters.Visibility = Visibility.Hidden;
            cr_notes_visiter_visiters.Visibility = Visibility.Visible;
        }

        private void Search_visiters_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string s = Search_visiters.Text;
            if (e.Key == Key.Enter)
            {


                if (Search_visiters.Text == "")
                {
                    DB db = new DB();
                    db.openconn();
                    string cmd = "SELECT * FROM visiter";
                    MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                    command.ExecuteNonQuery();

                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable("visiter");
                    dataAdp.Fill(dt);
                    db_visiters.ItemsSource = dt.DefaultView;

                    db.closedconn();

                    select_visiters.Text = "";
                    Search_visiters.Text = "Поиск";
                }
                else if (Search_visiters.Text != "")
                {


                    if (select_visiters.Text == "Номер посетителя")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM visiter WHERE id_visiter LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_visiters.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else if (select_visiters.Text == "Фамилия")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM visiter WHERE surname LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_visiters.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else if (select_visiters.Text == "Имя")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM visiter WHERE name LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_visiters.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    else if (select_visiters.Text == "Отчество")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM visiter WHERE middle_name LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_visiters.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    
                    else if (select_visiters.Text == "Возраст")
                    {
                        
                        DB db = new DB();
                        db.openconn();
                        
                        int agev = Convert.ToInt32(s);
                        
                        string cmd = "SELECT * FROM visiter WHERE TIMESTAMPDIFF(YEAR,birthday, CURDATE()) = @age";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@age", MySqlDbType.Int32).Value = agev;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }

                    else if (select_visiters.Text == "Возраст до")
                    {

                        DB db = new DB();
                        db.openconn();

                        int agev = Convert.ToInt32(s);

                        string cmd = "SELECT * FROM visiter WHERE TIMESTAMPDIFF(YEAR,birthday, CURDATE()) < @age";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@age", MySqlDbType.Int32).Value = agev;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }

                    else if (select_visiters.Text == "Возраст после")
                    {

                        DB db = new DB();
                        db.openconn();

                        int agev = Convert.ToInt32(s);

                        string cmd = "SELECT * FROM visiter WHERE TIMESTAMPDIFF(YEAR,birthday, CURDATE()) > @age";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@age", MySqlDbType.Int32).Value = agev;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("visiters");
                        dataAdp.Fill(dt);
                        db_visiters.ItemsSource = dt.DefaultView;

                        db.closedconn();
                    }
                    Search_visiters.Text = "Поиск";
                }
            }
        }

        private void Search_visiters_MouseMove(object sender, MouseEventArgs e)
        {
            Search_visiters.Text = "";
        }

        private void cr_note_visiter_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                DB db = new DB();
                db.openconn();
                //Изменение формата даты
                string birth = Convert.ToDateTime(birthday_visiters.Text).ToString("yyyy-MM-dd");
                //Изменение формата данных
                string date_join_convert = Convert.ToDateTime(date_join_visiters.Text).ToString("yyyy-MM-dd");
                string cmd = "INSERT INTO visiter(id_visiter,surname,name,middle_name,birthday,phone,email,place,social_networks,find_us,date_join) VALUE(@id,@sur,@nam,@mn,@birth,@phone,@email,@place,@sn,@find_us,@date_join)";
                MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id_visiter_visiters.Text;
                command.Parameters.Add("@sur", MySqlDbType.VarChar).Value = surname_visiters.Text;
                command.Parameters.Add("@nam", MySqlDbType.VarChar).Value = name_visiters.Text;
                command.Parameters.Add("@mn", MySqlDbType.VarChar).Value = middle_name_visiters.Text;
                command.Parameters.Add("@birth", MySqlDbType.VarChar).Value = birth;
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone_visiters.Text;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email_visiters.Text;
                command.Parameters.Add("@place", MySqlDbType.VarChar).Value = place_visiters.Text;
                command.Parameters.Add("@sn", MySqlDbType.VarChar).Value = social_net_visiters.Text;
                command.Parameters.Add("@find_us", MySqlDbType.VarChar).Value = find_us_visiters.Text;
                command.Parameters.Add("@date_join", MySqlDbType.VarChar).Value = date_join_convert;
                command.ExecuteNonQuery();

                id_visiter_visiters.Clear();
                surname_visiters.Clear();
                name_visiters.Clear();
                middle_name_visiters.Clear();
                phone_visiters.Clear();
                email_visiters.Clear();
                place_visiters.Clear();
                social_net_visiters.Clear();
                find_us_visiters.Clear();
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка создания записи в модуле Посетители" + ex);
            }
               

        }

        private void resident_menu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Переход в меню резиденты
            JP.Visibility = Visibility.Hidden;
            Reg_vis.Visibility = Visibility.Hidden;
            Visiter_View.Visibility = Visibility.Hidden;
            Reg_visiters_note.Visibility = Visibility.Hidden;
            dbj1.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Hidden;
            lists.Visibility = Visibility.Hidden;
            cr_notes.Visibility = Visibility.Hidden;
            db_visiters.Visibility = Visibility.Hidden;
            Search_visiters.Visibility = Visibility.Hidden;
            select_visiters.Visibility = Visibility.Hidden;
            cr_notes_visiter_visiters.Visibility= Visibility.Hidden;
            Visiter_View.Visibility = Visibility.Hidden;
            resident_View.Visibility = Visibility.Visible;
            create_residents.Visibility = Visibility.Visible;
            sep.Visibility = Visibility.Visible;
            ExpP.Visibility = Visibility.Visible;
            db_resident.Visibility = Visibility.Visible;
            resident_View.Visibility = Visibility.Visible;
            Search_resident.Visibility = Visibility.Visible;
            select_resident.Visibility = Visibility.Visible;
            Visiter_View.Visibility = Visibility.Hidden;
            cr_notes_residents_menu.Visibility = Visibility.Hidden;

            //Открытие таблицы БД

            DB db = new DB();
            db.openconn();

            string cmd = "SELECT * FROM residents";
            MySqlCommand command = new MySqlCommand(cmd, db.getconn());
            command.ExecuteNonQuery();

            DataTable dt = new DataTable("resident");
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dt);

            db_resident.ItemsSource = dt.DefaultView;

            db.closedconn();
        }

        
        private void selch2_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CheckBox selch2 = (CheckBox)sender;
                DataRowView row1 = (DataRowView)db_resident.SelectedItems[0];

                if (selch2.IsChecked == false)
                {
                    selch2.IsChecked = true;
                    


                    if (residents_id_ch.Contains(row1["id_resident"].ToString()) == false)
                    {

                        residents_id_ch.Add(row1["id_resident"].ToString());

                        MessageBox.Show("Элемент №: " + row1["id_resident"].ToString() + " добавлен в список");
                        f++;
                        counts.Content = "Элементов" + f;
                    }
                    else
                    {
                        selection_ch.Remove(row1["id_resident"].ToString());

                        MessageBox.Show("Элемент №: " + row1["id_resident"].ToString() + " удален из списка");
                        f -= 1;
                        counts.Content = "Элементов: " + f;
                    }


                }
            }

            catch ( Exception ex)
            {
                MessageBox.Show("Внимание выделите строку");
                logger.Error("Ошибка выделения " + ex);
            }
        }

        private void Search_resident_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                if (Search_resident.Text == "")
                {
                    DB db = new DB();
                    db.openconn();
                    string cmd = "SELECT * FROM residents";
                    MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                    command.ExecuteNonQuery();

                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable("residents");
                    dataAdp.Fill(dt);
                    db_resident.ItemsSource = dt.DefaultView;

                    db.closedconn();

                    select_resident.Text = "";
                    Search_resident.Text = "Поиск";
                }
                else if (Search_resident.Text != "")
                {


                    if (select_resident.Text == "Номер резидента")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM residents WHERE id_resident LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_resident.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("resident");
                        dataAdp.Fill(dt);
                        db_resident.ItemsSource = dt.DefaultView;

                        db.closedconn();
                        Search_resident.Text = "Поиск";
                    }
                    else if (select_resident.Text == "Название клуба")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM residents WHERE title LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_resident.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("resident");
                        dataAdp.Fill(dt);
                        db_resident.ItemsSource = dt.DefaultView;

                        db.closedconn();
                        Search_resident.Text = "Поиск";
                    }
                    else if (select_resident.Text == "Руководитель")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM residents WHERE teamlead LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_resident.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("resident");
                        dataAdp.Fill(dt);
                        db_resident.ItemsSource = dt.DefaultView;

                        db.closedconn();
                        Search_resident.Text = "Поиск";
                    }
                    else if (select_resident.Text == "Вид деятельности")
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM residents WHERE type_activity LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_resident.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("resident");
                        dataAdp.Fill(dt);
                        db_resident.ItemsSource = dt.DefaultView;

                        db.closedconn();
                        Search_resident.Text = "Поиск";
                    }
                    else
                    {
                        DB db = new DB();
                        db.openconn();
                        string cmd = "SELECT * FROM residents WHERE title LIKE @ser";
                        MySqlCommand command = new MySqlCommand(cmd, db.getconn());
                        command.Parameters.Add("@ser", MySqlDbType.VarChar).Value = Search_resident.Text;
                        command.ExecuteNonQuery();
                        MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("resident");
                        dataAdp.Fill(dt);
                        db_resident.ItemsSource = dt.DefaultView;

                        db.closedconn();
                        Search_resident.Text = "Поиск";
                    }
                }
            }
        }

        private void Search_resident_MouseMove(object sender, MouseEventArgs e)
        {
            Search_resident.Text = "";
        }

        private void resident_View_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DB db = new DB();
            db.openconn();
            string cmd1 = "SELECT * FROM residents";
            MySqlCommand command3 = new MySqlCommand(cmd1, db.getconn());
            command3.ExecuteNonQuery();

            MySqlDataAdapter dataAdp3 = new MySqlDataAdapter(command3);
            DataTable dt3 = new DataTable("residents");
            dataAdp3.Fill(dt3);
            db_resident.ItemsSource = dt3.DefaultView;

            db.closedconn();


            ExpP.Visibility = Visibility.Visible;
            sep.Visibility = Visibility.Visible;
            db_resident.Visibility = Visibility.Visible;
            cr_notes_residents_menu.Visibility = Visibility.Hidden;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            db.openconn();
            string cmd = "INSERT INTO residents(title,type_activity,teamlead) VALUES(@title,@ta,@tl)";

            MySqlCommand command = new MySqlCommand(cmd,db.getconn());

            command.Parameters.Add("@title", MySqlDbType.VarChar).Value = title_residents_menu.Text;
            command.Parameters.Add("@ta", MySqlDbType.VarChar).Value = type_activity_resident.Text;
            command.Parameters.Add("@tl", MySqlDbType.VarChar).Value = teamlead_resident_menu.Text;
            command.ExecuteNonQuery();
            db.closedconn();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Открытие вкладки Создание резидентов
            db_resident.Visibility = Visibility.Hidden;
            ExpP.Visibility = Visibility.Hidden;
            sep.Visibility = Visibility.Hidden;

            cr_notes_residents_menu.Visibility = Visibility.Visible;
        }
    }

}
