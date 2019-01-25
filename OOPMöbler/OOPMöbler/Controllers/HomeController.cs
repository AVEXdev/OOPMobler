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
        // Initiera en möbellista och hämtar data 
        public List<Möbel> möbellist = Möbel.GetData();
        // Deklarerar vår användardata som vi även kommer spara
        public UserData userdata;
        // Våra meny knappar där vi även kollar user id om använder är inloggad, annars skickar till inloggningen
        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
                // Här sparar vi användarens data
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
        // I varukorgen vill vi även ta med våran lista och data (Viewmodel VM)
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
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View(VM);
        }
        // Våran köp controller där vi kör foreach loop för varje möbel i listan, och om id't stämmer så läggs det till BuyCount, och antalet av den
        // tar vi bort (++ och --)
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
            // Här hämtar även id't för användaren som är inloggad när vi köper en produkt
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View("Cart", VM);
        }
        // Eftersom utcheckningen är en "action" i o med att vi inte tar med någon variabel (ex. int __)
        public ActionResult Checkout()
        {
            // Hämtar användaren, vi tömmer varukorgen (null), sedan sparar vi och skickar med VM tillbaka till Index sidan
            userdata = UserData.GetUserData((int)Session["UserId"]);
            userdata.ShoppingCart = null;
            Models.UserData.SaveUserData(userdata);
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View("Index", VM);
        }
        // Lägga till en till vara som ligger i varukorgen (ej använd på sidan)
        public ActionResult Plus(int id)
        {
            foreach (Models.Möbel möbel in möbellist)
            {
                if (möbel.Id == id)
                {
                    // Tar bort en från totala count, och lägger till en i BuyCount där vi visar hur många vi har köpt
                    möbel.Count--;
                    möbel.BuyCount++;
                    Models.Möbel.SaveData(möbellist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if (userdata.ShoppingCart == null)
                    {
                        userdata.ShoppingCart = new List<Models.UserData.Buy>();
                    }
                    // Lägger till i varukorgen
                    userdata.ShoppingCart.Add(new Models.UserData.Buy { Id = möbel.Id });
                    // Sparar sedan det vi ändrat
                    Models.UserData.SaveUserData(userdata);
                }
            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View("Cart", VM);
        }
        // Samma som i Plus, fast här tar vi bort en vara
        public ActionResult Minus(int id)
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
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(möbellist, userdata);
            return View("Cart", VM);
        }
    }
}