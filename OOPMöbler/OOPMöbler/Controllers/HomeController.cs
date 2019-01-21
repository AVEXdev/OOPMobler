using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOPMöbler.Models;

namespace OOPMöbler.Controllers
{
    public class HomeController : Controller
    {
        public List<Möbel> möbellist = Möbel.GetData();
        public UserData userdata;

        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
                Models.UserData.SaveUserData(userdata);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult Shop()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View(möbellist);
        }

        public ActionResult About()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult Cart()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        

        

        public ActionResult Buy(int id)
        {
            foreach (Models.Möbel möbel in möbellist)
            {
                if (möbel.Id == id)
                {
                    möbel.Count--;
                    möbel.BuyCount++;
                    Models.Möbel.SaveData(möbellist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if (userdata.ShoppingCart == null)
                    {
                        userdata.ShoppingCart = new List<Models.UserData.Buy>();
                    }
                    userdata.ShoppingCart.Add(new Models.UserData.Buy { Id = möbel.Id});
                    Models.UserData.SaveUserData(userdata);
                }
            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View("Cart", VM);
        }

        public ActionResult Return(int id)
        {
            foreach (Models.Möbel möbel in möbellist)
            {
                if (möbel.Id == id)
                {
                    möbel.Count++;
                    Models.Möbel.SaveData(möbellist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    var itemToRemove = userdata.ShoppingCart.FirstOrDefault(r => r.Id == id);
                    if (itemToRemove != null)
                    {
                        userdata.ShoppingCart.Remove(itemToRemove);
                        Models.UserData.SaveUserData(userdata);
                    }
                }
            }


            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);

            return View("Index", VM);
        }
    }
}