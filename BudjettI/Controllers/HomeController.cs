using BudjettI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Login = BudjettI.Models.Login;


// toimiva versio 
namespace BudjettI.Controllers
{
    public class HomeController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ulos";
            }
            else ViewBag.LoggedStatus = "Sisällä";
            return View();
        }

        public ActionResult Info()
        {
           

                ViewBag.Message = "Sovelluksen sivusto";

                return View();
            
            
        }

        public ActionResult Yhteystiedot()
        {
            ViewBag.Message = "Yhteystietomme";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Login LoginModel)
        {

            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Login.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Onnistunut kirjautuminen";
                ViewBag.LoggedStatus = "Kirjautunut:";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Epäonnistunut kirjautuminen";
                ViewBag.LoggedStatus = "Ulos";
                LoginModel.LoginErrorMessage = "Väärä sähköpostiosoite tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {

            Session.Abandon();
            ViewBag.LoggedStatus = "Ulos";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle

        }
    }
}




//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult Info()
//        {
//            ViewBag.Message = "käytä tätä tarvittaessa. Ei ole nyt käytössä";
//            return View();
//        }

//        public ActionResult Yhteystiedot()
//        {
//            ViewBag.Message = "Valitse yhteydenottotapasi alla olevista vaihtoehdoista";

//            return View();
//        }

//        public ActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]

//        public ActionResult Authorize(Login LoginModel)

//        {

//        //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä

//        //var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == hashedLoginUsername && x.PassWord == hashedLoginPassword);

//        var LoggedUser = db.Login.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);

//        if (LoggedUser != null)

//            {

//                ViewBag.LoginMessage = "Successfull login";

//                ViewBag.LoggedStatus = "In";

//                //Lisätään

//                ViewBag.LoginError = 0;

//                Session["UserName"] = LoggedUser.UserName;

//                Session["LoginID"] = LoggedUser.LoginID;

//                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index

//            }

//            else

//            {

//                ViewBag.LoginMessage = "Login unsuccessfull";

//                ViewBag.LoggedStatus = "Out";

//                //Lisätään

//                ViewBag.LoginError = 1;

//                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";

//                //Login > Index

//                return View("Index", LoginModel);

//            }

//        }

//        public ActionResult LogOut()

//        {

//            Session.Abandon();

//            ViewBag.LoggedStatus = "Out";

//            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle

//        }

//    }
//}