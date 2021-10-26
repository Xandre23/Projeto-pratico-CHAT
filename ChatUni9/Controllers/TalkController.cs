using ChatUni9.DAO.Talk;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    public class TalkController : Controller
    {
       
        public IActionResult Index()
        {           
            return View();            
        }
        [HttpGet]
        public IActionResult Talk(int userID)
        {
            var talkDAO = new TalkDAO();
            return PartialView("/Views/Talk/_Talk.cshtml");
        }
    }
  
}
