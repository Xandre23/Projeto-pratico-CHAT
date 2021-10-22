using ChatUni9.DAO;
using ChatUni9.FactoryObject.User;
using ChatUni9.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
       
           
        }
    }


