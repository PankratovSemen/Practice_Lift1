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


        //password = root;
        MySqlConnection conn = new MySqlConnection("server = 127.0.0.1; port = 3307; username = root; database = lift");

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
