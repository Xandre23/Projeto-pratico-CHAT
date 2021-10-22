using ChatUni9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
            command.Parameters.AddWithValue("@mensagem", talk.Menssage);
            command.Parameters.AddWithValue("@data_hora", talk.DateTime);
            command.Parameters.AddWithValue("@visualizado", talk.Visualized);

            await Insert(command);
        }
    }
}
