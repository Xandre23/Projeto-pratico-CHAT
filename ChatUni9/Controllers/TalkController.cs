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
        public IActionResult Index()
        {           
            return View();            
        }
        [HttpGet]
        public async Task< IActionResult> Talk(int userID)
        {
            var talkDAO = new TalkDAO();
            int loggedInUserID = Convert.ToInt32(this.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value);
            ViewBag.LoggedInUserID = loggedInUserID;
            var listTalk = await talkDAO.GetMessages(userID, loggedInUserID);
            return PartialView("/Views/Talk/_Talk.cshtml", listTalk);
        }
    }  
}
