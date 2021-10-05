using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace ChatUni9
{
    public class Conexao
    {

        public MySqlConnection Open()
        {
            string connStr = "server=chatuni9db.mysql.database.azure.com;userid=admin_db;password=Aky406BraKF;database=chat_uninove";

            //string connStr = "Server=chatuni9db.mysql.database.azure.com;Database=chat_uninove;Uid=admin_db;Password=Aky406BraKF;";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand();


            try
            {
                Console.WriteLine("Connectin to MYSQL...");
                conn.Open();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Feito.");
            return conn;
        }


    }






}