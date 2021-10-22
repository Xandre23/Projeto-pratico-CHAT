using ChatUni9.DAO;
using ChatUni9.DAO.Account;
using ChatUni9.FactoryObject.User;
using ChatUni9.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return PartialView("/Views/Contact/_AddContact.cshtml");

        }
        public class AccountController : Controller
        {


            public async Task<JsonResult> Search(string nome)
            {
                try
                {
                    var accountDAO = new AccountDAO();
                    var user = await accountDAO.Search(nome);

                    if (user.Nome.Contains(nome))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Nome));
                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var result = new HttpResponse(Convert.ToInt32(HttpStatusCode.OK), string.Empty);
                        return Json(nome);
                    }

                    else
                    {
                        var result = new HttpResponse(Convert.ToInt32(HttpStatusCode.BadRequest), "Nome não encontrado");
                        return Json(result);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}


    





