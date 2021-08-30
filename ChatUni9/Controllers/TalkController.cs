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
    }
}
