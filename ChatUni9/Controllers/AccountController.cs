using ChatUni9.DAO.Account;
using ChatUni9.Models;
using ChatUni9.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                var hash = new Hash();
                user.Senha = hash.GenerateHashSHA512(user.Senha);
                var accountDAO = new AccountDAO();
                await accountDAO.Create(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
