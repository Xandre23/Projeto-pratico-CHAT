using ChatUni9.DAO.Account;
using ChatUni9.Models;
using ChatUni9.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
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
        public async Task<JsonResult> Login(string email, string password)
        {
            try
            {
                var hash = new Hash();
                string hashedPassword = hash.GenerateHashSHA512(password);
                var accountDAO = new AccountDAO();
                var user = await accountDAO.Login(email);
                if (user == null)
                {
                    var result = new HttpResponseViewlModel(Convert.ToInt32(HttpStatusCode.BadRequest), "Email não encontrado");
                    return Json(result);
                }
                if (!user.Senha.Equals(hashedPassword))
                {
                    var response = new HttpResponseViewlModel(Convert.ToInt32(HttpStatusCode.BadRequest), "Senha Incorreta");
                    return Json(response);
                }
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                var httpResponse = new HttpResponseViewlModel(Convert.ToInt32(HttpStatusCode.OK), string.Empty);
                return Json(httpResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IActionResult> Logout()
        {
            var accountDAO = new AccountDAO();
            int loggedInUserID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
            var lastSeen = DateTime.Now;
            await accountDAO.UpdateLastSeen(loggedInUserID, lastSeen);

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}






