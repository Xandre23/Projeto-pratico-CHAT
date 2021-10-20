using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ChatUni9
{
    public class ConnectionStringManager
    {
        protected string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

             string connectionString = builder
                .Build()
                .GetSection("ConnectionStrings")
                .GetSection("DefaultConnection")
                .Value;
            return connectionString;
        }        
    }
}