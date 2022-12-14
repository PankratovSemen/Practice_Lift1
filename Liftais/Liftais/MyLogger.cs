using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace Liftais
{
    internal class MyLogger
    {
        public static string username;
        MySqlConnection conn = new MySqlConnection("server = 127.0.0.1; port = 3306; username = root; database = lift");
        public void Error(string message)
        {

            try {
                conn.Open();
                string cmd = "INSERT INTO logs(date,user,message) VALUE (@d,@u,@m)";
                MySqlCommand command = new MySqlCommand(cmd, conn);
                command.Parameters.Add("@d", MySqlDbType.Date).Value = DateTime.Now;
                command.Parameters.Add("@u", MySqlDbType.VarChar).Value = username;
                command.Parameters.Add("@m", MySqlDbType.Text).Value = message;
                command.ExecuteNonQuery();
                conn.Close();
                message = "\n " + DateTime.Now + "ERROR" + " " + message + "\n";
                DateTime dateTime = DateTime.Now;
                string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString();
                string path = $"logs\\{dat}.txt";
                FileInfo fileInfo = new FileInfo(path);
                File.AppendAllText(path, message);
            }
            catch
            {
                message = "\n " + DateTime.Now + "ERROR" + " " + message + "\n";
                DateTime dateTime = DateTime.Now;
                string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString();
                string path = $"logs\\{dat}.txt";
                FileInfo fileInfo = new FileInfo(path);
                File.AppendAllText(path, message);
            }
        }
        public void Info(string message)
        {

            try
            {
                conn.Open();
                string cmd = "INSERT INTO logs(date,user,message) VALUE (@d,@u,@m)";
                MySqlCommand command = new MySqlCommand(cmd, conn);
                command.Parameters.Add("@d", MySqlDbType.Date).Value = DateTime.Now;
                command.Parameters.Add("@u", MySqlDbType.VarChar).Value = username;
                command.Parameters.Add("@m", MySqlDbType.Text).Value = message;
                command.ExecuteNonQuery();
                conn.Close();
                message = "\n " + DateTime.Now + "INFO" + " " + message + "\n";
                DateTime dateTime = DateTime.Now;
                string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString();
                string path = $"logs\\{dat}.txt";
                FileInfo fileInfo = new FileInfo(path);
                File.AppendAllText(path, message);
            }
            catch
            {
                message = "\n " + DateTime.Now + "INFO" + " " + message + "\n";
                DateTime dateTime = DateTime.Now;
                string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString();
                string path = $"logs\\{dat}.txt";
                FileInfo fileInfo = new FileInfo(path);
                File.AppendAllText(path, message);
            }
        }
        public void ErrorFile(string message)
        {

            DateTime dateTime = DateTime.Now;
            string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() +"-" + dateTime.Year.ToString();
            message = "\n " + DateTime.Now + " ERROR"+ " " +  message + "\n" ;
            string path = $"logs\\{dat}.txt";
            FileInfo fileInfo = new FileInfo(path);
            File.AppendAllText(path, message);
        }

        public void InfoFile(string message)
        {

            DateTime dateTime = DateTime.Now;
            string dat = dateTime.Day.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Year.ToString();
            message = "\n " + DateTime.Now + " INFO" + " " + message + "\n";
            string path = $"logs\\{dat}.txt";
            FileInfo fileInfo = new FileInfo(path);
            File.AppendAllText(path, message);
        }
    }
}
