﻿using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.DAO.Account
{
    public class AccountDAO : ExecuteCommandMySQL
    {
        public async Task<UserViewModel> Login(string email, string password)
        {

            var command = new MySqlCommand();
            command.CommandText = ("select count * from usuario where  =  @email, @senha");
           
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", password);
            await Select(command);
           


            var listUsers = new List<UserViewModel>();

            listUsers.AddRange(new List<UserViewModel>()
            {
                new UserViewModel()
                {
                    ID = 1,
                    Nome = "Leonardo",
                    Email = "leonardo.amorim253@gmail.com",
                    Senha = "123"
                },
                new UserViewModel()
                {
                    ID = 2,
                    Nome = "Xandre",
                    Email = "xandre@gmail.com",
                    Senha = "123"
                    
                }
            });

            var user = listUsers.Where(u => u.Email == email && u.Senha == password);

            return user.FirstOrDefault();
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
