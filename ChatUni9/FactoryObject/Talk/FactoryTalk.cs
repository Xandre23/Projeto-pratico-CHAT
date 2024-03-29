﻿using ChatUni9.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.FactoryObject.Talk
{
    public class FactoryTalk
    {
        public IList<TalkViewModel> Factory(DataTable dataTable)
        {
            var listTalk = new List<TalkViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                if (!string.IsNullOrEmpty(Convert.ToString( item["id"])))
                {
                    listTalk.Add(new TalkViewModel()
                    {
                        ID = Convert.ToInt32(item["id"]),
                        IDUserIssuer = Convert.ToInt32(item["id_usuario_emissor"]),
                        IDUserReceiver = Convert.ToInt32(item["id_usuario_receptor"]),
                        Message = Convert.ToString(item["mensagem"]),
                        DateTime = Convert.ToDateTime(item["data_hora"]),
                        Visualized = Convert.ToBoolean(item["visualizado"])
                    });
                }               
            }
            return listTalk;
        }
    }
}
