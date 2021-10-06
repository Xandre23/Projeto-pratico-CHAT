using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.DAO
{
    public class ExecuteCommandMySQL: ConnectionStringManager
    {        

        public async Task Insert(MySqlCommand command)
        {
            MySqlConnection conn = new MySqlConnection(GetConnectionString());
            try
            {
                await conn.OpenAsync();
                command.Connection = conn;
                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await conn.CloseAsync();
            }
        }

        public async Task Select(MySqlCommand command)
        {

        }

        public async Task Update(MySqlCommand command)
        {

        }

        public async Task Delete(MySqlCommand command)
        {

        }
    }
}
