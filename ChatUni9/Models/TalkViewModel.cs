using System;

namespace ChatUni9.Models
{
    public class TalkViewModel
    {
        public int ID { get; set; }
        public int IDUserIssuer { get; set; }
        public int IDUserReceiver { get; set; }
        public string Menssage { get; set; }
        public DateTime DateTime { get; set; }
        public bool Visualized { get; set; }
    }
}
