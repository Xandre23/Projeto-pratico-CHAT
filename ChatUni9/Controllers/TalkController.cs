using ChatUni9.DAO.Account;
using ChatUni9.DAO.Talk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    [Authorize]
    public class TalkController : Controller
    {

        public async Task<IActionResult> Index()
        {
            int loggedInUserID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
            var accountDAO = new AccountDAO();
            var contacts = await accountDAO.GetListContacts(loggedInUserID);
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Talk(int userID)
        {
            var talkDAO = new TalkDAO();
            int loggedInUserID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
            ViewBag.LoggedInUserID = loggedInUserID;
            var listTalk = await talkDAO.GetMessages(userID, loggedInUserID);
            return PartialView("/Views/Talk/_Talk.cshtml", listTalk);
        }
    }
}
