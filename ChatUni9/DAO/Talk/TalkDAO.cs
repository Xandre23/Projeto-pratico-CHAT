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
            var command = new MySqlCommand();
            //command.CommandText = (@"SELECT 
            //    conversas.*,
            //    usuario.id as 'user_id',
            //    usuario.nome,
            //    usuario.sobrenome,
            //    usuario.visto_por_ultimo
            //from
            //    conversas
            //        inner join
            //    usuario
            //         INNER JOIN
            //    solicitacoes ON usuario.id IN (solicitacoes.id_usuario_emissor , solicitacoes.id_usuario_receptor)
            //WHERE
            //        (conversas.id_usuario_emissor = @loggedinuserid and usuario.id = @idcontact)
            //        or(conversas.id_usuario_emissor = @idcontact and usuario.id = @idcontact)
            //        or(conversas.id_usuario_receptor = @idcontact and usuario.id = @idcontact)
            //        or(conversas.id_usuario_receptor = @loggedinuserid and usuario.id = @idcontact)
            //        AND (solicitacoes.status = 1)");

            command.CommandText = @"SELECT 
                conversas.*,
                usuario.id AS 'user_id',
                usuario.nome,
                usuario.sobrenome,
                usuario.visto_por_ultimo
            FROM
                conversas
                    RIGHT JOIN
                usuario ON usuario.id IN (conversas.id_usuario_emissor , conversas.id_usuario_receptor)
                    INNER JOIN
                solicitacoes ON usuario.id IN (solicitacoes.id_usuario_emissor , solicitacoes.id_usuario_receptor)
            WHERE
                (conversas.id_usuario_emissor = @idcontact or conversas.id_usuario_emissor = @loggedinuserid or usuario.id = 1 and solicitacoes.status = 1) OR 
                (conversas.id_usuario_emissor = @loggedinuserid or conversas.id_usuario_emissor = @idcontact or usuario.id = 1 and solicitacoes.status = 1) OR 
                (conversas.id_usuario_receptor = @idcontact or conversas.id_usuario_receptor = @loggedinuserid or usuario.id = 1 and solicitacoes.status = 1) OR 
                (conversas.id_usuario_receptor = @loggedinuserid or conversas.id_usuario_receptor = @idcontact or usuario.id = 1 and solicitacoes.status = 1) group by conversas.id";


            command.Parameters.AddWithValue("@idcontact", idContact);
            command.Parameters.AddWithValue("@loggedinuserid", loggedInUserID);

            var dataTable = await Select(command);
            var factoryObject = new FactoryTalk();
            var conversationHistory = factoryObject.Factory(dataTable);

            return conversationHistory;
        }
    }
}
