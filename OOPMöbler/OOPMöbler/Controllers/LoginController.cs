﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOPMöbler.Models;

namespace OOPMöbler.Controllers
{
    public class LoginController : Controller
    {
        // Inloggningscontrollern för inloggningssidan där vi sparar email, lösen och kollar ifall det stämmer
        public ActionResult Index()
        {
            if (HttpContext.Request.RequestType == "POST")
            {
                var Email = Request.Form["email"];
                var Password = Request.Form["password"];
                var CurrentUser = UserData.GetUserData(Email);
                if (CurrentUser != null && CurrentUser.Password == Password)
                {
                    Session["UserId"] = CurrentUser.Id;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}