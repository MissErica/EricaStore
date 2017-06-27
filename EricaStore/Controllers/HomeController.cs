﻿using EricaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EricaStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
                
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            //TODO: send message to customer service
            ViewBag.Message = "Questions? Comments? Send us a message!";

            return View();
        }






        [HttpPost]
        public ActionResult Contact(string message)
        {
            ViewBag.Message = "Thanks, we got your message and someone will be contacting you shortly.";
            return View();
        }

        
       
    }
}