using ChatUni9.DAO;
using ChatUni9.DAO.Account;
using ChatUni9.FactoryObject.User;
using ChatUni9.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                if (string.IsNullOrEmpty(name))
                {
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
        [HttpGet]
        public async Task<IActionResult> ReceiveRequest()
        {
            try
            {
                var accountDAO = new ContactDAO();
                int ID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
                var user = await accountDAO.ReceiveRequest(ID);
                return PartialView("/Views/Contact/_ListSolicitations.cshtml", user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPost]
        public async Task Accept(int ID)
        {
            try
            {
                var accountDAO = new ContactDAO();
                await accountDAO.Accept(ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        public async Task Refuse(int ID)
        {
            try
            {
                var accountDAO = new ContactDAO();
                await accountDAO.Delete(ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(string filter)
        {
            try
            {
                var contactDAO = new ContactDAO();
                int ID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
                var contacts = await contactDAO.GetListContacts(ID, filter);
                ViewBag.OnlineUsers = ConnectedUserViewModel.Ids;
                return PartialView("/Views/Contact/_Contacts.cshtml", contacts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}



