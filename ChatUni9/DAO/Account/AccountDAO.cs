﻿using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChatUni9.FactoryObject.User;
using System.Security.Claims;

namespace ChatUni9.DAO.Account
{

    public class AccountDAO : ExecuteCommandMySQL
    {
        internal async Task<UserViewModel> Login(string email)
        {
            var command = new MySqlCommand();
            command.CommandText = ("select * from usuario where email=@email");
            command.Parameters.AddWithValue("@email", email);

            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var user = factoryUser.Factory(dataTable);
            return user.FirstOrDefault();
        }

        internal async Task<UserViewModel> Get(int id)
        {
            var command = new MySqlCommand();
            command.CommandText = ("select * from usuario where id=@userid");
            command.Parameters.AddWithValue("@userid", id);

            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var user = factoryUser.Factory(dataTable);
            return user.FirstOrDefault();
        }

        internal async Task Create(UserViewModel user)
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

        internal async Task<IList<UserViewModel>> Search(string nome, int userID)
        {
            var command = new MySqlCommand();
            command.CommandText = @"SELECT 
                *
            FROM
                usuario
            WHERE
                usuario.nome LIKE @nome
                    AND usuario.id NOT IN(SELECT
                        id_usuario_emissor
                    FROM
                        solicitacoes
                    WHERE
                        id_usuario_emissor = usuario.id and id_usuario_receptor = @userid)
                    AND usuario.id NOT IN(SELECT
                        id_usuario_receptor
                    FROM
                        solicitacoes
                    WHERE
                        id_usuario_receptor = usuario.id and id_usuario_emissor = @userid) limit 6";
            command.Parameters.AddWithValue("@nome","%"+nome+"%");
            command.Parameters.AddWithValue("@userid", userID);

            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var user = factoryUser.Factory(dataTable);

            return user;
        }

        internal async Task UpdateLastSeen(int userID, DateTime lastSeen)
        {
            var command = new MySqlCommand();
            command.CommandText = "update usuario set visto_por_ultimo = @lastseen where id = @userID";
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@lastseen", lastSeen);

            await Update(command);
        }
    }
}

