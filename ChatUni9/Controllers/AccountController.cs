using ChatUni9.DAO.Account;
using ChatUni9.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        Conexao conexao = new Conexao();

        public String mensagem = "";
        [HttpPost]
        public void Create(UserViewModel user)
        {




            var conexao = new Conexao().Open();
            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = "insert into usuario (nome, sobrenome, email, senha, sexo) values(@nome, @sobrenome, @email, @senha, @sexo)";

            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@sobrenome", user.Sobrenome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@senha", user.Senha);
            cmd.Parameters.AddWithValue("@sexo", user.Sexo);
            try
            {


                cmd.ExecuteNonQuery();
                conexao.Close();


                this.mensagem = "Cadastrado com sucesso";

            }
            catch (MySqlException e)
            {
                this.mensagem = "Erro de cadastro";
            }

        }
    

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var accountDAO = new AccountDAO();
            var user = await accountDAO.Login(email, password);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return Redirect("/Talk");
        }
    }
}
