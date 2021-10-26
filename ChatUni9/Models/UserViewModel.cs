namespace ChatUni9.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Sexo { get; set; }
        public string TokenGoogle { get; }
        public string TokenFacebook { get; set; }
    }   
}
