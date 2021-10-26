using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ChatUni9.DAO
{
    public class ExecuteCommandMySQL : ConnectionStringManager
    {

        protected async Task Insert(MySqlCommand command)
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

        protected async Task<DataTable> Select(MySqlCommand command)
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

        protected async Task Update(MySqlCommand command)
        {

        }

        protected async Task Delete(MySqlCommand command)
        {

        }
    }
}
