using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
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
using System.Diagnostics;

namespace Liftais
{
    /// <summary>
    /// Логика взаимодействия для updater.xaml
    /// </summary>
    public partial class updater : Window
    {
        public updater()
        {
            InitializeComponent();
           
        }
        WebClient clients = new WebClient();
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var readver = clients.DownloadString("http://192.168.88.54/version.txt");
                this.Hide();
                MessageBox.Show(curver);
                if (Convert.ToDouble(curver,CultureInfo.InvariantCulture) == Convert.ToDouble(readver,CultureInfo.InvariantCulture))
                {
                    MessageBox.Show("У вас акутальная версия ПО");
                }

                else
                {
                    MessageBox.Show("Найдена новая версия. Обновление произойдет автоматически. Пожалуйста подождите", "Обновление", MessageBoxButton.OK);
                    this.Show();
                    clients.DownloadFile("http://192.168.f88.54/Liftais.exe", "Liftais1.exe");
                    ProcessStartInfo psi = new ProcessStartInfo();
                    //Имя запускаемого приложения
                    psi.FileName = "cmd.exe";
                    //команда, которую надо выполнить
                    psi.Arguments = @"/c taskkill /f /im Liftais.exe && timeout /t 1 && del Liftais.exe && ren Liftais1.exe Liftais.exe &&  Liftais.exe ";
                    //  /c - после выполнения команды консоль закроется
                    //  /к - не закрывать консоль после выполнения команды
                    Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления " + ex);
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }
    }
}
