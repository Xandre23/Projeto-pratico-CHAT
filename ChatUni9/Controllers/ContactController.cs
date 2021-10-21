using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return PartialView("/Views/Contact/_AddContact.cshtml");
        }
    }
}
