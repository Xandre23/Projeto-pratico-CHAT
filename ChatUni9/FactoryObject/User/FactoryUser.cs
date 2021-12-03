using ChatUni9.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.FactoryObject.User
{
    public class FactoryUser
    {
        public IList<UserViewModel> Factory(DataTable dataTable)
        {
            var list = new List<UserViewModel>();
           
            foreach (DataRow item in dataTable.Rows)
            {
                var user = new UserViewModel();
                user.ID = Convert.ToInt32(item["id"]);
                user.Email = Convert.ToString(item["email"]);   
                user.Senha = Convert.ToString(item["senha"]);
                user.Nome = Convert.ToString(item["nome"]);
                user.Sobrenome = Convert.ToString(item["sobrenome"]);
                user.LastSeen = Convert.ToDateTime(item["visto_por_ultimo"]);
                user.Sexo = Convert.ToString(item["sexo"]);
                list.Add(user);
            }
            return list;
        }
    }
}
