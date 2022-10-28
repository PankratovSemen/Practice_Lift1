using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Liftais
{
    internal class DB
    {
        MySqlConnection conn = new MySqlConnection("server = 192.168.0.150; port = 3307; username = root; password = root; database = lift");

        public void openconn()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

        }
        public void closedconn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        public MySqlConnection getconn()
        {
            return conn;
        }
    } 
}
