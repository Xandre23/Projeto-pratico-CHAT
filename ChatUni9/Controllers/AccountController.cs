using ChatUni9.DAO.Account;
using ChatUni9.FactoryObject.User;
using ChatUni9.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        [HttpPost]
        public async Task Create(UserViewModel user)
        {
            try
            {
                var accountDAO = new AccountDAO();
                await accountDAO.Create(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public String msg = "";
        [HttpPost]

       





        public async Task<IActionResult> Login(string email, string password)
        {
             async Task Login(UserViewModel user)
            {
                try
                {
                    var accountDAO = new AccountDAO();
                    await accountDAO.Login(email, password);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            var accountDAO = new AccountDAO();

            var user = await accountDAO.Login(email, password);
            if (email == email && password == password )
            {
           
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, email, password));
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Redirect("/Talk");
            }
            else
            {
                this.msg = "Login ou senha invalidos";
            }
            return Redirect("/index");
        }



    }
}


    

