﻿using ChatUni9.DAO.Account;
using ChatUni9.FactoryObject.Talk;
using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatUni9.DAO.Talk
{
    public class TalkDAO: ExecuteCommandMySQL
    {
        public async Task InsetMessage(TalkViewModel talk)
        {
            var command = new MySqlCommand();
            command.CommandText = ("insert into conversas (id_usuario_emissor, id_usuario_receptor, mensagem, data_hora, visualizado) values(@id_usuario_emissor, @id_usuario_receptor, @mensagem, @data_hora, @visualizado)");
            command.Parameters.AddWithValue("@id_usuario_emissor", talk.IDUserIssuer);
            command.Parameters.AddWithValue("@id_usuario_receptor", talk.IDUserReceiver);
            command.Parameters.AddWithValue("@mensagem", talk.Message);
            command.Parameters.AddWithValue("@data_hora", talk.DateTime);
            command.Parameters.AddWithValue("@visualizado", talk.Visualized);

            await Insert(command);
        }

        internal async Task<UserViewModel> GetMessages(int idContact, int loggedInUserID)
        {
            var accountDAO = new AccountDAO();
            var user = await accountDAO.Get(idContact);
            var command = new MySqlCommand();

            command.CommandText = (@"SELECT 
                conversas.*
            FROM
                conversas
                    INNER JOIN
                usuario ON usuario.id IN (conversas.id_usuario_emissor , conversas.id_usuario_receptor)
                    INNER JOIN
                solicitacoes ON usuario.id IN (solicitacoes.id_usuario_emissor , solicitacoes.id_usuario_receptor)
            WHERE
                solicitacoes.status = 1
            AND (conversas.id_usuario_emissor = @idcontact AND conversas.id_usuario_receptor = @loggedinuserid)
            OR (conversas.id_usuario_emissor = @loggedinuserid AND conversas.id_usuario_receptor = @idcontact)
            GROUP BY conversas.id ORDER BY conversas.data_hora ASC");

            command.Parameters.AddWithValue("@idcontact", idContact);
            command.Parameters.AddWithValue("@loggedinuserid", loggedInUserID);

            var dataTable = await Select(command);
            var factoryObject = new FactoryTalk();
            user.Talk = factoryObject.Factory(dataTable);
            return user;
        }
    }
}
