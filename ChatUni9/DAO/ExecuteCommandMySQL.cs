using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.DAO
{
    public class ExecuteCommandMySQL : ConnectionStringManager
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

        public async Task<DataTable> Select(MySqlCommand command)
        {
            MySqlConnection conn = new MySqlConnection(GetConnectionString());
            try
            {
                await conn.OpenAsync();
                command.Connection = conn;
                var mysqlData = new MySqlDataAdapter();
                mysqlData.SelectCommand = command;
                var dataTable = new DataTable();

                mysqlData.Fill(dataTable);
                return dataTable;
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

        public async Task Update(MySqlCommand command)
        {

        }

        public async Task Delete(MySqlCommand command)
        {

        }
    }
}
