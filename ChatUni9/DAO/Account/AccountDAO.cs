using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChatUni9.FactoryObject.User;

namespace ChatUni9.DAO.Account
{

    public class AccountDAO : ExecuteCommandMySQL
    {
        public async Task<UserViewModel> Login(string email)
        {
            var command = new MySqlCommand();
            command.CommandText = ("select * from usuario where  email=@email");
            command.Parameters.AddWithValue("@email", email);

            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var user = factoryUser.Factory(dataTable);
            return user;
        }

        public async Task Create(UserViewModel user)
        {
            var command = new MySqlCommand();
            command.CommandText = "insert into usuario (nome, sobrenome, email, senha, sexo) values(@nome, @sobrenome, @email, @senha, @sexo)";

            command.Parameters.AddWithValue("@nome", user.Nome);
            command.Parameters.AddWithValue("@sobrenome", user.Sobrenome);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@senha", user.Senha);
            command.Parameters.AddWithValue("@sexo", user.Sexo);

            await Insert(command);
        }
    }
}
