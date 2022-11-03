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
                //Excel.Application excel1 = new Excel.Application();
                //excel1.Visible = true;
                //Excel.Workbook workbook = excel1.Workbooks.Add(System.Reflection.Missing.Value);
                //Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];
                //var path = Path.Com(Environment.CurrentDirectory, "Export", "data.xls");
                var wb = new XLWorkbook();
                var sh = wb.Worksheets.Add("Export");
                for (int j = 0; j < dbj1.Columns.Count; j++)
                {

                    
                    sh.Cell(1, j+1).SetValue(dbj1.Columns[j].Header);
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
                                sh.Cell(a-j + 2, i+2 ).SetValue(row[i]);

                            if (visiter_ch.Contains(row[i+1].ToString()))
                            {
                                sh.Cell(a-j + 2, i + 3).SetValue(row[i + 1]);
                            }
                           
                            if (resident_ch.Contains(row[i+3].ToString()))
                            {
                                sh.Cell(a-j+2, i + 5).SetValue(row[i + 3]);
                            }
                            
                            if (events_ch.Contains(row[i+2].ToString()) )
                            {
                                if (events_ch.Contains(row[i + 2].ToString()) != selection_ch.Contains(row[i]))
                                {
                                    sh.Cell(a - j + 2, i + 4).SetValue(row[i + 2]);
                                }


                            }
                            if (open_ch.Contains(row[i+4].ToString()))
                            {
                                sh.Cell(a-j+2, i +6).SetValue(row[i + 4]);
                            }
                            
                            if (close_ch.Contains(row[i+5].ToString()))
                            {
                                sh.Cell(a-j+2, i + 7).SetValue(row[i + 5]);
                                q = 0;
                            }
                            

                        }
                        else if (a==0)
                        {
                            sh.Cell(j + 2, i + 2).SetValue(row[i]);
                        }

                        wb.SaveAs("C:\\Users\\Семён\\Documents\\Export_AIS\\data.xlsx");




                       


                       




                    }

                }

                
            }

            catch(Exception ex)
            {
                logger.Error("Ошибка:  " + ex);
            }
           
        }
    }
}
