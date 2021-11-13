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

        public async Task<IList<UserViewModel>> ReceiveRequest(int ID)
        {
            var command = new MySqlCommand();
            command.CommandText = ("select * from solicitacoes where  id_usuario_receptor  like @id_usuario_receptor ");
            command.Parameters.AddWithValue("@id_usuario_receptor", "%" + ID + "%");
            

            var dataTable = await Select(command);
            var factoryUser = new FactoryUser();
            var user = factoryUser.Factory(dataTable);

            return user;
        }

        public async Task Accept(int ID)
        {
            var command = new MySqlCommand();
            //atualizar o id para 1 onde a descricao for aceito
            command.CommandText = "UPDATE status_solicitacao SET id = @id WHERE id=@id";
            command.Parameters.AddWithValue("@id",ID).Value = 1;

            command.ExecuteNonQuery();
             await Update(command);
        }

        public async Task Delete(int senderID)
        {
            var command = new MySqlCommand();
            command.CommandText = "DELETE FROM solicitacoes WHERE id_usuario_emissor=@id_usuario_emissor";
            command.Parameters.AddWithValue("@id_usuario_emissor", senderID);

            command.ExecuteNonQuery();
            await Delete(command);
        }
    }
}
