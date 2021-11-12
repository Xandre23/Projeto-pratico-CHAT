using ChatUni9.FactoryObject.User;
using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.DAO
{
    public class ContactDAO : ExecuteCommandMySQL
    {
        public async Task SendSolitation(int senderID, int ID)
        {
            var command = new MySqlCommand();
            command.CommandText = "insert into solicitacoes (id_usuario_emissor, id_usuario_receptor) values (@id_usuario_emissor, @id_usuario_receptor)";

            command.Parameters.AddWithValue("@id_usuario_emissor", senderID);
            command.Parameters.AddWithValue("@id_usuario_receptor", ID);

            await Insert(command);

        }
        internal async Task<IList<UserViewModel>> GetListContacts(int userID)
        {
            var command = new MySqlCommand();
            command.CommandText = (@"SELECT 
                usuario.id,
                usuario.nome,
                usuario.sobrenome,
                usuario.email,
                usuario.senha,
                usuario.sexo,
                solicitacoes.id_usuario_emissor,
                solicitacoes.id_usuario_receptor,
                solicitacoes.status
            FROM
                usuario
                    INNER JOIN
                solicitacoes ON usuario.id IN(solicitacoes.id_usuario_emissor , solicitacoes.id_usuario_receptor)
            WHERE
                solicitacoes.status = 1
                AND usuario.id != @userID
                AND (solicitacoes.id_usuario_emissor = @userID
                OR solicitacoes.id_usuario_emissor != @userID)
                AND (solicitacoes.id_usuario_receptor = @userID
                OR solicitacoes.id_usuario_receptor != @userID)
            ORDER BY usuario.nome ASC");
            command.Parameters.AddWithValue("@userID", userID);


            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var contacts = factoryUser.Factory(dataTable);

            return contacts;
        }
    }
}
