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
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
          
            try
            {
                if(string.IsNullOrEmpty(name)){
                    name = "a";
                }
                var accountDAO = new AccountDAO();
                var user = await accountDAO.Search(name);


                return PartialView("/Views/Contact/_AddContact.cshtml", user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task SendSolitation(int ID)
        {
            try
            {

                var accountDAO = new ContactDAO();
                var senderID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
                await accountDAO.SendSolitation(ID, senderID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }













}