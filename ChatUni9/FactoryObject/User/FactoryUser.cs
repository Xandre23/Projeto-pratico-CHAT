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
        public UserViewModel Factory(string email, DataTable dataTable)
        {
            var user = new UserViewModel();
            foreach (DataRow item in dataTable.Rows)
            {
                user.ID = Convert.ToInt32(item["id"]);
                user.Email = Convert.ToString(item["email"]);
                user.Nome = Convert.ToString(item["nome"]);
                user.Sobrenome = Convert.ToString(item["sobrenome"]);
                user.Sexo = Convert.ToString(item["sexo"]);



            }
            return user;
        }



    }
}
